using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using Grateful.xUnit.Config;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Grateful.xUnit.Exceptions;

namespace Grateful.xUnit.Processes
{

   /// <summary>
   /// Provides the methods required to,
   /// run the ASP.NET process that is currently
   /// being tested.
   /// </summary>
   internal class ProcessRunner
   {
      #region Private Variables

      private Process? process;
      private string applicationUrl;
      private int greatestFailureLine = 0;
      private List<string?> currentOutput;

      #endregion

      #region Properties

      /// <summary>
      /// Gets and sets the name of the project file
      /// that we are trying to build and start.
      /// </summary>
      internal string ProjectPath { get; set; }

      /// <summary>
      /// Application used for authorization
      /// </summary>
      internal IConfidentialClientApplication Application { get; private set; }

      internal ClientConfig ClientConfig { get; private set; }

      #endregion

      #region Constructor

      public ProcessRunner(string projectPath)
      {
         ProjectPath = projectPath;
         currentOutput = new List<string?>();

         var projectName = ProjectPath.Split('\\').Last();
         var launchSettingsFilePath = @$"{ProjectPath}\Properties\launchSettings.json";
         string launchSettings = null;

         try
         {
            launchSettings = File.ReadAllText(launchSettingsFilePath);
         }
         catch (FileNotFoundException)
         {
            throw new ProcessConfigurationException($"Grateful.xUnit Requires a launchSettings.json file at: {launchSettingsFilePath}");
         }


         dynamic? settings = JsonConvert.DeserializeObject<dynamic>(launchSettings);
         var applicationUrls = (string)settings.profiles[projectName].applicationUrl;
         applicationUrl = applicationUrls.Split(';').First();
         ClientConfig = BuildConfig();

         var authorityUri = new Uri($"https://login.microsoftonline.com/{ClientConfig.TenantId}");
         Application = ConfidentialClientApplicationBuilder.Create(ClientConfig.AppClientId)
                                                                              .WithClientSecret(ClientConfig.ClientSecret)
                                                                              .WithAuthority(authorityUri)
                                                                              .Build();

      }

      #endregion

      #region Internal Methods

      /// <summary>
      /// Starts the process running and constructs the corresponding
      /// HttpClient if no process is running.
      /// Otherwise just tells the process that there is one more method
      /// that needs to exit before it can be killed.
      /// Throws an error if the process is unable to be start.
      /// </summary>
      internal void Start()
      {
         process = new Process();
         process.StartInfo.FileName = "dotnet";
         process.StartInfo.Arguments = $"run --project \"{ProjectPath}\"";
         process.StartInfo.RedirectStandardOutput = true;
         process.StartInfo.RedirectStandardInput = true;
         process.StartInfo.RedirectStandardError = true;
         process.EnableRaisingEvents = true;
         process.OutputDataReceived += Process_OutputReceived;
         process.Start();
         process.BeginOutputReadLine();

      }

      /// <summary>
      /// Creates a client with 
      /// an authentication header.
      /// </summary>
      /// <param name="authHeader"></param>
      /// <returns></returns>
      internal HttpClient CreateClient(AuthenticationHeaderValue authHeader)
      {
         var client = new HttpClient()
         {
            BaseAddress = new Uri(applicationUrl)
         };

         client.DefaultRequestHeaders.Authorization = authHeader;
         return client;
      }

      /// <summary>
      /// Creates an Http Client without any headers.
      /// Written this way instead of a property
      /// so that we could add headers every time.
      /// </summary>
      /// <returns></returns>
      internal HttpClient CreateClient()
      {
         return new HttpClient()
         {
            BaseAddress = new Uri(applicationUrl)
         };
      }

      /// <summary>
      /// Checks for the latest unseen failure
      /// once the failure has been seen from this method
      /// it cannot be called again.
      /// </summary>
      /// <returns></returns>
      internal string? CheckForLatestFailure()
      {
         if (process == null)
            return null;

         if (greatestFailureLine == currentOutput.Count - 1)
            return null;

         var newestOutput = currentOutput.Take(new Range(greatestFailureLine, currentOutput.Count)).ToArray();
         var firstFailLine = Array.FindIndex(newestOutput, 0, newestOutput.Length, s => s?.StartsWith("fail") ?? false);

         if (firstFailLine >= 0)
         {
            var lastFailLine = Array.FindIndex(newestOutput,
                                               firstFailLine + 1,
                                               newestOutput.Length - (firstFailLine + 1),
                                               s => !(s?.StartsWith(" ") ?? false)) - 1;

            if (lastFailLine < 0)
               lastFailLine = newestOutput.Length - 1;

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = firstFailLine; i <= lastFailLine; i++)
            {
               stringBuilder.AppendLine(newestOutput[i]);
            }

            greatestFailureLine = lastFailLine;
            return stringBuilder.ToString();
         }

         return null;
      }

      internal bool HasExited()
      {
         return process?.HasExited ?? false;
      }

      internal string GetOutput()
      {
         return string.Join(Environment.NewLine, currentOutput);
      }

      /// <summary>
      /// Leave the method and tell stop the process
      /// if it is the only process left.
      /// </summary>
      internal void Kill()
      {
         process?.Kill();
         process?.WaitForExit();
      }


      #endregion

      #region Private Methods

      private void Process_OutputReceived(object sender, DataReceivedEventArgs e)
      {
         currentOutput.Add(e.Data);
      }

      private ClientConfig BuildConfig()
      {
         var directory = Directory.GetCurrentDirectory();
         var file = $"{directory}\\test.settings.json";
         var json = File.ReadAllText(file);
         return JsonConvert.DeserializeObject<ClientConfig>(json);
      }

      #endregion

   }
}
