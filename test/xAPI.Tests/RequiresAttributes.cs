using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using xAPI.Test.SampleProject.Data;

namespace xAPI.Tests
{
   [RequireASPNet(@"C:\Software Projects\Long Term Projects\xAPI\xAPI\test\xAPI.Test.SampleProject")]
   public class RequiresAttributes
   {

      [Fact]
      public async void HttpTest()
      {
         HttpClient client = new HttpClient();
         var response = await client.GetAsync("https://localhost:7199/helloworld");
         response.EnsureSuccessStatusCode();

         var responseJson = await response.Content.ReadAsStringAsync();
         var data = JsonConvert.DeserializeObject<HelloWorldData>(responseJson);

         Assert.Equal("Sean!", data.Name);
         Assert.Equal("Hello, man!", data.Greeting);
      }

   }
}
