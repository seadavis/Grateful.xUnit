using xAPI.Fixtures;

namespace xAPI.Tests.Collections
{
   public class ControllerFailureFixture : HttpClientFixture
   {
      public ControllerFailureFixture() : base(@"C:\Software Projects\Long Term Projects\xAPI\xAPI\test\xAPI.Test.SampleProject.MissingInterface")
      {
      }
   }
}
