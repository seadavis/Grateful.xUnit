using System.Reflection;
using Xunit.Sdk;

namespace xAPI
{
   [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
   public class RequireASPNet : BeforeAfterTestAttribute
   {

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

      }

      public override void After(MethodInfo methodUnderTest)
      {

      }

   }
}