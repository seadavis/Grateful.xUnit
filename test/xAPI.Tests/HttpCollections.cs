using System.Threading.Tasks;
using Xunit;
using xAPI.Test.SampleProject.Data;
using xAPI.Tests.Collections;
using xAPI.Extensions;

namespace xAPI.Tests
{
   [Collection("ASP.NET Working Collection")]
   public class HttpCollections
   {
      WorkingProjectFixture _fixture;
     
      public HttpCollections(WorkingProjectFixture fixture)
      {
         _fixture = fixture;
      }

      [Fact]
      public async Task ClientTest()
      {
         var data = await _fixture.Client.Get<HelloWorldData>("helloworld");
         data.ShouldHaveStatus(System.Net.HttpStatusCode.OK)
            .ShouldHaveDataJsonEqual(new HelloWorldData()
            {
               Name = "Sean!",
               Greeting = "Hello, man!"
            });
      }

      /*
       * The next few Tests exist to ensure repeated tests still work
       * Necessary because we make us of conurrency in the implmentation
       */

      [Fact]
      public async Task ClientTest_Repeat()
      {
         var data = await _fixture.Client.Get<HelloWorldData>("helloworld");
         data.ShouldHaveStatus(System.Net.HttpStatusCode.OK)
            .ShouldHaveDataJsonEqual(new HelloWorldData()
            {
               Name = "Sean!",
               Greeting = "Hello, man!"
            });
      }

      [Fact]
    
      public async Task ClientTest_Repeat2()
      {
         var data = await _fixture.Client.Get<HelloWorldData>("helloworld");
         data.ShouldHaveStatus(System.Net.HttpStatusCode.OK)
            .ShouldHaveDataJsonEqual(new HelloWorldData()
            {
               Name = "Sean!",
               Greeting = "Hello, man!"
            });
      }
   }
}
