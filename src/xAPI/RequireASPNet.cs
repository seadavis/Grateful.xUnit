using System.Diagnostics;
using System.Reflection;
using Xunit.Sdk;
using Newtonsoft.Json;
using xAPI.Processes;

namespace xAPI
{
   [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
   public class RequireASPNet : BeforeAfterTestAttribute
   {

      private Process process;
      
      public string ProjectPath { get; }

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
         ProjectPath = projectPath;
      }

      public override void Before(MethodInfo methodUnderTest)
      {
         ProcessCollection.Instance.Enter(ProjectPath); 
      }

      public override void After(MethodInfo methodUnderTest)
      {
         ProcessCollection.Instance.Leave(ProjectPath);
      }

   }
}