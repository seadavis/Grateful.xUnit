using System.Threading.Tasks;
using Xunit;
using Grateful.xUnit.Test.SampleProject.Data;
using Grateful.xUnit.Tests.Collections;
using Grateful.xUnit.Extensions;
using FluentAssertions;
using System.Net;
using Grateful.xUnit.Assertions;

namespace Grateful.xUnit.Tests
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
