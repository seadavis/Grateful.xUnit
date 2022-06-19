using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using xAPI.Test.SampleProject.Data;
using xAPI.Clients;
using xAPI.Fixtures;
using xAPI.Tests.Collections;

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
         Assert.Equal("Sean!", data.Name);
         Assert.Equal("Hello, man!", data.Greeting);
      }

      /*
       * The next few Tests exist to ensure repeated tests still work
       * Necessary because we make us of conurrency in the implmentation
       */

      [Fact]
      public async Task ClientTest_Repeat()
      {
         var data = await _fixture.Client.Get<HelloWorldData>("helloworld");
         Assert.Equal("Sean!", data.Name);
         Assert.Equal("Hello, man!", data.Greeting);
      }

      [Fact]
    
      public async Task ClientTest_Repeat2()
      {
         var data = await _fixture.Client.Get<HelloWorldData>("helloworld");
         Assert.Equal("Sean!", data.Name);
         Assert.Equal("Hello, man!", data.Greeting);
      }
   }
}
