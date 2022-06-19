using xAPI.Tests.Collections;
using Xunit;

namespace xAPI.Tests
{
   /// <summary>
   /// Class that is used to test failures
   /// of a fixture that points to a project that fails to even build.
   /// </summary>
   public class PathologicalFixtures
   {
      private FailingBuildFixture sut;


      [Fact]
      public void ConstructorThrowsException()
      {
         sut = new FailingBuildFixture();
      }

   }
}
