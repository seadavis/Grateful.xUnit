using System.Threading.Tasks;
using Xunit;
using xAPI.Test.SampleProject.Data;
using xAPI.Tests.Collections;
using xAPI.Extensions;
using FluentAssertions;
using xAPI.Assertions;
using System.Net;

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
      public async Task Get()
      {
         var data = await _fixture.Client.Get<HelloWorldData>("helloworld");
         data.Should().BeOkWithData(new HelloWorldData()
         {
            Name = "Sean!",
            Greeting = "Hello, man!"
         });
      }

      [Fact]
      public async Task Delete()
      {
         var statusCode = await _fixture.Client.Delete("helloworld");
         Assert.Equal(HttpStatusCode.OK, statusCode);
      }

      [Fact]
      public async Task Post()
      {
         var data = await _fixture.Client.Post<HelloWorldData, HelloWorldData>("helloworld", new HelloWorldData()
         {
            Name = "Sean!",
            Greeting = "Hello, man!"
         });

         data.Should().BeOkWithData(new HelloWorldData()
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
      public async Task  Get_Repeat()
      {
         var data = await _fixture.Client.Get<HelloWorldData>("helloworld");
         data.Should().HaveCodeAndData(System.Net.HttpStatusCode.OK, new HelloWorldData()
         {
            Name = "Sean!",
            Greeting = "Hello, man!"
         });
      }

      [Fact]
    
      public async Task Get_Repeat2()
      {
         var data = await _fixture.Client.Get<HelloWorldData>("helloworld");
         data.Should().HaveCodeAndData(System.Net.HttpStatusCode.OK, new HelloWorldData()
         {
            Name = "Sean!",
            Greeting = "Hello, man!"
         });
      }
   }
}
