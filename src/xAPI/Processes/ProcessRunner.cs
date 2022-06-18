using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using xAPI.Clients;

namespace xAPI.Processes
{
   internal enum ProcessState
   {
      Starting,
      Started,
      Stopping,
      Stopped
   }

   /// <summary>
   /// Provides the methods required to,
   /// run the ASP.NET process that is currently
   /// being tested.
   /// </summary>
   internal class ProcessRunner
   {
      #region Private Variables

      private object methodLock = new object();
      private int openMethods = 0;

      #endregion

      #region Properties

      /// <summary>
      /// Gets and sets the name of the project file
      /// that we are trying to build and start.
      /// </summary>
      internal string ProjectFileName { get; set; }

      internal IHttpClient Client { get; private set; }
      internal ProcessState CurrentState { get; private set; }

      #endregion

      #region Constructor

      public ProcessRunner()
      {
         CurrentState = ProcessState.Stopped;
      }

      #endregion

      #region Internal Methods

      /// <summary>
      /// Starts the process running.
      /// Throws an error if the process is unable to be start.
      /// </summary>
      internal void Enter(MethodInfo method)
      {

      }

      /// <summary>
      /// Leave the method and tell stop the process
      /// if it is the only process left.
      /// </summary>
      internal void Leave(MethodInfo method)
      {

      }


      #endregion

   }
}
