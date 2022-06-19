using System.Diagnostics;
using Newtonsoft.Json;
using xAPI.Exceptions;

namespace xAPI.Processes
{
  
   /// <summary>
   /// Provides the methods required to,
   /// run the ASP.NET process that is currently
   /// being tested.
   /// </summary>
   internal class ProcessRunner
   {
      #region Private Variables

      private Process process;
      private string applicationUrl;

      #endregion

      #region Properties

      /// <summary>
      /// Gets and sets the name of the project file
      /// that we are trying to build and start.
      /// </summary>
      internal string ProjectPath { get; set; }

      internal HttpClient Client { get; private set; }

      #endregion

      #region Constructor

      public ProcessRunner(string projectPath)
      {
         ProjectPath = projectPath;

         var projectName = ProjectPath.Split('\\').Last();
         var launchSettingsFilePath = @$"{ProjectPath}\Properties\launchSettings.json";
         string launchSettings = null;

         try
         {
            launchSettings = File.ReadAllText(launchSettingsFilePath);
         }
         catch (FileNotFoundException)
         {
            throw new ProcessConfigurationException($"xAPI Requires a launchSettings.json file at: {launchSettingsFilePath}");
         }

         dynamic settings = JsonConvert.DeserializeObject<dynamic>(launchSettings);
         var applicationUrls = (string)(settings.profiles[projectName].applicationUrl);
         applicationUrl = applicationUrls.Split(';').First();
         Client = new HttpClient()
         {
            BaseAddress = new Uri(applicationUrl)
         };
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
         process.StartInfo.UseShellExecute = true;
         process.StartInfo.FileName = "dotnet";
         process.StartInfo.Arguments = $"run --project \"{ProjectPath}\"";
         process.Start();
         
      }

      /// <summary>
      /// Leave the method and tell stop the process
      /// if it is the only process left.
      /// </summary>
      internal void Kill()
      {
         process.Kill();
      }


      #endregion

   }
}
