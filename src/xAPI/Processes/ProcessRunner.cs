using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using xAPI.Clients;

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

      private object methodLock = new object();

      // the number of methods currently testing the
      // given process. 
      private int openMethods = 0;
      private Process process;

      #endregion

      #region Properties

      /// <summary>
      /// Gets and sets the name of the project file
      /// that we are trying to build and start.
      /// </summary>
      internal string ProjectFileName { get; set; }

      internal HttpClient Client { get; private set; }

      #endregion

      #region Constructor

      public ProcessRunner()
      {
         
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
