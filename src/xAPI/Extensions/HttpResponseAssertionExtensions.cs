using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using xAPI.Clients;
using Newtonsoft.Json;

namespace xAPI.Extensions
{
   public static class HttpResponseAssertionExtensions
   {
      public static HttpResponse<T> ShouldHaveStatus<T>(this HttpResponse<T> response, HttpStatusCode expectedStatus)
      {
         Assert.Equal(expectedStatus, response.Status);
         return response;
      }

      // Double checks that the data in the response is the same as the data
      // given here is they are both converted into Json
      public static HttpResponse<T> ShouldHaveDataJsonEqual<T>(this HttpResponse<T> response, object expectedData)
      {
         var expectedJson = JsonConvert.SerializeObject(expectedData);
         var actualJson = JsonConvert.SerializeObject(response.Data);

         Assert.Equal(expectedJson, actualJson);
         return response;
      }

   }
}
