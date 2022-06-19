using xAPI.Exceptions;
using xAPI.Tests.Collections;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using System;
using xAPI.Test.SampleProject.MissingInterface;

namespace xAPI.Tests
{
   /// <summary>
   /// Class that is used to test failures
   /// of a fixture that points to a project that fails to even build.
   /// </summary>
   public class PathologicalFixtures
   {
    

      [Fact]
      public async Task OnBuildErrorGettingClientThrowsException()
      {
         using (var sut = new FailingBuildFixture())
         {
            await Task.Delay(new TimeSpan(0, 0, 15));
            Assert.Throws<StartingProcessException>(() => sut.Client);
         } 
      }

      [Fact]
      public async Task OnControllerErrorGetThrowsException()
      {
         using (var sut = new ControllerFailureFixture())
         {
            await Assert.ThrowsAsync<ServerSideException>(async () =>
            {
               await sut.Client.Get<WeatherForecast>("weatherforecast");
            });
         }
      }

      [Fact]
      public async Task OnControllerErrorDeleteThrowsException()
      {
         using (var sut = new ControllerFailureFixture())
         {
            await Assert.ThrowsAsync<ServerSideException>(async () =>
            {
               await sut.Client.Delete("weatherforecast");
            });
         }
      }

      [Fact]
      public async Task OnControllerErrorPostThrowsException()
      {
         using (var sut = new ControllerFailureFixture())
         {
            await Assert.ThrowsAsync<ServerSideException>(async () =>
            {
               await sut.Client.Post<WeatherForecast, WeatherForecast>("weatherforecast", new WeatherForecast());
            });
         }
      }

      [Fact]
      public async Task OnControllerErrorGetMultipleTimesThrowsExceptions()
      {
         using (var sut = new ControllerFailureFixture())
         {
            await Assert.ThrowsAsync<ServerSideException>(async () =>
            {
               await sut.Client.Get<WeatherForecast>("weatherforecast");
            });

            await Assert.ThrowsAsync<ServerSideException>(async () =>
            {
               await sut.Client.Get<WeatherForecast>("weatherforecast");
            });
         }
         
      }

     
   }
}
