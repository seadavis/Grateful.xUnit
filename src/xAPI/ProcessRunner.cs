using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xAPI
{
   /// <summary>
   /// Provides the methods required to,
   /// run the ASP.NET process that is currently
   /// being tested.
   /// </summary>
   internal class ProcessRunner
   {
      #region Properties

      /// <summary>
      /// Gets and sets the name of the project file
      /// that we are trying to build and start.
      /// </summary>
      internal string ProjectFileName { get; set; }

      #endregion


      #region Internal Methods

      /// <summary>
      /// Starts the process running.
      /// Throws an error is the process is unable to be started for whatever reason.
      /// </summary>
      internal void Start()
      {

      }

      /// <summary>
      /// Kills the process that is currently running.
      /// </summary>
      internal void Kill()
      {

      }

      /// <summary>
      /// Method for checking whether or not that process is
      /// actually started.
      /// 
      /// For Example:
      /// <code>
      /// ProcessRunner r = new ProcessRunner();
      /// r.IsStarted();
      /// </code>
      /// would result in false.
      /// 
      /// But:
      /// 
      /// <code>
      /// ProcessRunner r = new ProcessRunner()
      /// r.Start()
      /// r.IsStarted()
      /// </code>
      /// 
      /// would result in true.
      /// 
      /// Is thread safe so callers do not need to worry about this being true or false.
      /// 
      /// </summary>
      internal bool IsStarted()
      {
         return false;
      }

      #endregion

   }
}
