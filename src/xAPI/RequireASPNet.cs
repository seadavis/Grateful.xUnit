using System.Diagnostics;
using System.Reflection;
using Xunit.Sdk;

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
         var folderPath = Path.GetDirectoryName(FilePathProjectFile);
         process = Process.Start(folderPath + "/bin/Debug/net6.0/xApi.Test.SampleProject.exe");
      }

      public override void After(MethodInfo methodUnderTest)
      {
         process.Kill();
      }

   }
}