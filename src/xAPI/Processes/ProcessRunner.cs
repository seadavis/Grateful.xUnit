using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using xAPI.Clients;
using Newtonsoft.Json;

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

      // locks around creation of the methods leave and enter
      private object methodLock = new object();

      // the number of methods currently testing the
      // given process. 
      private int openMethods = 0;
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
         var launchSettings = File.ReadAllText(@$"{ProjectPath}\Properties\launchSettings.json");

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
      internal void Enter()
      {
         lock(methodLock)
         {
            if(openMethods == 0)
            {
               process = new Process();
               process.StartInfo.UseShellExecute = true;
               process.StartInfo.FileName = "dotnet";
               process.StartInfo.Arguments = $"run --project \"{ProjectPath}\"";
               var started = process.Start();

             
            }
            openMethods++;
         }

      }


      /// <summary>
      /// Returns True if the process is stopped.
      /// </summary>
      /// <returns></returns>
      internal bool IsStopped()
      {
         return process.HasExited;
      }

      /// <summary>
      /// Leave the method and tell stop the process
      /// if it is the only process left.
      /// </summary>
      internal void Leave()
      {
         lock (methodLock)
         {
            if(openMethods == 1)
            {
               process.Kill();
            }

            if (openMethods == 0)
               return;

            openMethods--;
         }
      }


      #endregion

   }
}
