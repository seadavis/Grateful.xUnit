using xAPI.Fixtures;

namespace xAPI.Tests.Collections
{
   public class ControllerFailureFixture : HttpClientFixture
   {
      public ControllerFailureFixture() : base(@"..\..\..\..\xAPI.Test.SampleProject.MissingInterface")
      {
      }
   }
}
