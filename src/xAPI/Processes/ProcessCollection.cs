using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using xAPI.Clients;

namespace xAPI.Processes
{
   /// <summary>
   /// Responsible for keeping track of which process is running and 
   /// which process needs to be killed.
   /// 
   /// Needed so that we can communication between the data attributes
   /// and the ASP.Net requires.
   /// </summary>
   internal class ProcessCollection
   {
      private ConcurrentDictionary<string, ProcessRunner> runners = new ConcurrentDictionary<string, ProcessRunner>();

      private static readonly Lazy<ProcessCollection> lazy =
        new Lazy<ProcessCollection>(() => new ProcessCollection());

      public ProcessCollection Instance { get { return lazy.Value; } }

      /// <summary>
      /// Gets the client that corresponds
      /// to the given project name.
      /// </summary>
      /// <param name="projectName"></param>
      public HttpClient GetClient(string projectName)
      {
         return null;
      }

      /// <summary>
      /// enters then given project for the given method
      /// </summary>
      /// <param name="projectName"></param>
      /// <param name="usingMethod"></param>
      public void Enter(string projectName, MethodInfo usingMethod)
      {
         // build a new process runner if it doesn't already exist
         // go in and add it to the list of methods being run.
      }

      /// <summary>
      /// Leave the given project for the given method.
      /// Shutting down the process if it is the last one.
      /// </summary>
      /// <param name="projectName"></param>
      /// <param name="usingMethod"></param>
      public void Leave(string projectName, MethodInfo usingMethod)
      {
         // Call leave
         // check if the process is killed
         // if it is remove it from runners.
      }

   }

   
}
