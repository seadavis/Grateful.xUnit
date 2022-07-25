using Grateful.xUnit.Fixtures;

namespace Grateful.xUnit.Tests.Collections
{
   /// <summary>
   /// Fixture that points to a failing build.
   /// </summary>
   public class FailingBuildFixture : HttpClientFixture
   {
      public FailingBuildFixture() : base(@"..\..\..\..\Grateful.xUnit.Test.Sample.Project.BuildFail")
      {
      }
   }
}
