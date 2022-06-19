using xAPI.Exceptions;
using xAPI.Tests.Collections;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using System;

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
      public async Task OnBuildErrorGettingClientThrowsException()
      {
         sut = new FailingBuildFixture();
         await Task.Delay(new TimeSpan(0, 0, 15));
         Assert.Throws<StartingProcessException>(() => sut.Client);
      }

   }
}
