using xAPI.Fixtures;

namespace xAPI.Tests.Collections
{
   /// <summary>
   /// Fixture that points to a failing build.
   /// </summary>
   public class FailingBuildFixture : HttpClientFixture
   {
      public FailingBuildFixture() : base(@"..\..\..\..\xAPI.Test.Sample.Project.BuildFail")
      {
      }
   }
}
