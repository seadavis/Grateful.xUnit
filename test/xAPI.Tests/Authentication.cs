using System;
using System.Threading.Tasks;
using Xunit;
using xAPI.Assertions;
using Microsoft.Identity.Client;
using System.Net.Http;
using System.Net.Http.Headers;
using xAPI.Tests.Collections;
using xAPI.Test.SampleProject.Data;
using FluentAssertions;

namespace xAPI.Tests
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
         var data = await _fixture.Client.GetAuthorized<HelloWorldData>("helloworld/api/auth");
         data.Should().BeOkWithData(new HelloWorldData()
         {
            Greeting = "Salutations!",
            Name = "Post Authorized User!"
         });
      }

      [Fact]
      public async Task DeleteAuthorized()
      {
         var data = await _fixture.Client.GetAuthorized<HelloWorldData>("helloworld/api/auth");
         data.Should().BeOkWithData(new HelloWorldData()
         {
            Greeting = "Salutations!",
            Name = "Delete Authorized User!"
         });
      }

   }
}
