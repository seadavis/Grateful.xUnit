using System.Diagnostics;
using System.Reflection;
using Xunit.Sdk;
using Microsoft.Build.Evaluation;

namespace xAPI
{
   [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
   public class RequireASPNet : BeforeAfterTestAttribute
   {

      private Process process;

      public string FilePathProjectFile { get; }


      /// <summary>
      /// 
      /// </summary>
      /// <param name="filePath">The path to the project file that we want to run</param>
      public RequireASPNet(string projectFilePath)
      {
         FilePathProjectFile = projectFilePath;
      }

      public override void Before(MethodInfo methodUnderTest)
      {
         process = new Process();
         process.StartInfo.UseShellExecute = true;
         process.StartInfo.FileName = "dotnet";
         process.StartInfo.Arguments = $"run --project \"{FilePathProjectFile}\"";
         var started = process.Start();
      }

      public override void After(MethodInfo methodUnderTest)
      {
         process.Kill();
      }

   }
}