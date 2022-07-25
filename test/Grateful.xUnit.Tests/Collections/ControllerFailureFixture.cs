using Grateful.xUnit.Fixtures;

namespace Grateful.xUnit.Tests.Collections
{
   public class ControllerFailureFixture : HttpClientFixture
   {
      public ControllerFailureFixture() : base(@"..\..\..\..\Grateful.xUnit.Test.SampleProject.MissingInterface")
      {
      }
   }
}
