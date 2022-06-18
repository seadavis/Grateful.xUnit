using System.Diagnostics;
using System.Reflection;
using Xunit.Sdk;
using Newtonsoft.Json;

namespace xAPI
{
   [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
   public class RequireASPNet : BeforeAfterTestAttribute
   {

      private Process process;
      private string projectPath;


      /// <summary>
      /// Starts the ASP.Net process and builds the corresponding HttpClient.
      /// </summary>
      /// <param name="filePath">The path to the .NET project that we
      /// wish to start up as a requirement for the test.
      /// 
      /// Should be the folder without the path
      /// Example:
      /// <code>
      /// RequreASPNet(@"C:\Source\ASPProject")
      /// </code>
      /// 
      /// Will require that the ASP.NET project rooted at C:\Source\ASPProject\ be running.
      /// Note: the argument in the constructor is missing the trailing \
      /// </param>
      public RequireASPNet(string projectPath)
      {
         this.projectPath = projectPath;
      }

      public override void Before(MethodInfo methodUnderTest)
      {

         
         process = new Process();
         process.StartInfo.UseShellExecute = true;
         process.StartInfo.FileName = "dotnet";
         process.StartInfo.Arguments = $"run --project \"{projectPath}\"";
         var started = process.Start();

         var projectName = projectPath.Split('\\').Last();
         var launchSettings = File.ReadAllText(@$"{projectPath}\Properties\launchSettings.json");
         
         dynamic settings = JsonConvert.DeserializeObject<dynamic>(launchSettings);
         var applicationUrls = (string)(settings.profiles[projectName].applicationUrl);
         var applicationUrl = applicationUrls.Split(';').First();
          
      }

      public override void After(MethodInfo methodUnderTest)
      {
         process.Kill();
      }

   }
}