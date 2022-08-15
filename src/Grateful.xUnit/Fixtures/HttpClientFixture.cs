using Grateful.xUnit.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grateful.xUnit.Clients;
using Grateful.xUnit.Exceptions;
using Xunit.Abstractions;

namespace Grateful.xUnit.Fixtures
{
   // Base class for fixtures involving an
   // HttpClient
   public class HttpClientFixture : IDisposable
   {
      #region Private Variables

      private ProcessRunner _runner;
      

      #endregion

      #region Properties

      /// <summary>
      /// If not null passed into the HttpClient
      /// so that we can log messages to the console
      /// </summary>
      public ITestOutputHelper? OutputHelper { get; set; }

      /// <summary>
      /// The HttpClient.
      /// Throws an error if the process has exited in this amount of time.
      /// </summary>
      public IHttpClient Client
      {
         get
         {
            if (_runner.HasExited())
               throw new StartingProcessException(_runner.GetOutput());

            return new GratefulHttpClient(_runner, OutputHelper);
         }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Starts the ASP.Net process and builds the corresponding HttpClient.
      /// </summary>
      /// <param name="projectPath">The path to the .NET project that we
      /// wish to start up as a requirement for the test.
      /// 
      /// Should be the folder without the path
      /// Example:
      /// <code>
      /// HttpClientFixture(@"C:\Source\ASPProject");
      /// </code>
      /// 
      /// </param>
      public HttpClientFixture(string projectPath)
      {
         _runner = new ProcessRunner(projectPath);
         _runner.Start();
      }

      #endregion

      #region IDispoable



      public void Dispose()
      {
         _runner.Kill();
      }

      #endregion

   }
}
