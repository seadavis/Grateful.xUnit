using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Identity.Client;
using System.Net.Http;
using System.Net.Http.Headers;
using Grateful.xUnit.Tests.Collections;
using Grateful.xUnit.Test.SampleProject.Data;
using FluentAssertions;
using System.Net;
using Grateful.xUnit.Assertions;

namespace Grateful.xUnit.Tests
{
   [Collection("ASP.NET Working Collection")]
   public class Authentication
   {

      WorkingProjectFixture _fixture;

      public Authentication(WorkingProjectFixture fixture)
      {
         _fixture = fixture;
      }

      [Fact]
      public async Task GetAuthorized()
      {
        
         var data = await _fixture.Client.GetAuthorized<HelloWorldData>("helloworld/api/auth");
         data.Should().BeOkWithData(new HelloWorldData()
         {
            Greeting = "Salutations!",
            Name = "Authorized User!"
         });
      }

      [Fact]
      public async Task PostAuthorized()
      {

         var data = await _fixture.Client.PostAuthorized<HelloWorldData, HelloWorldData>("helloworld/api/auth", new HelloWorldData()
         {
            Name = "Sean!",
            Greeting = "Hello, man!"
         });
         data.Should().BeOkWithData(new HelloWorldData()
         {
            Greeting = "Salutations!",
            Name = "Post Authorized User!"
         });
      }

      [Fact]
      public async Task DeleteAuthorized()
      {
         var statusCode = await _fixture.Client.DeleteAuthorized("helloworld/api/auth");
         Assert.Equal(HttpStatusCode.OK, statusCode);
      }

   }
}
