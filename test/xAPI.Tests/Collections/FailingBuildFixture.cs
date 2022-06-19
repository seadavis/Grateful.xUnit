using xAPI.Fixtures;

namespace xAPI.Tests.Collections
{
   /// <summary>
   /// Fixture that points to a failing build.
   /// </summary>
   public class FailingBuildFixture : HttpClientFixture
   {
      public FailingBuildFixture() : base(@"C:\Software Projects\Long Term Projects\xAPI\xAPI\test\xAPI.Test.Sample.Project.BuildFail")
      {
      }
   }
}
