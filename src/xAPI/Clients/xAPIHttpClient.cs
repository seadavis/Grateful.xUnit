using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xAPI.Clients
{
   internal class xAPIHttpClient : IHttpClient
   {
      private HttpClient _httpClient;
      private string _route;

      public xAPIHttpClient(HttpClient client, string route)
      {
         _httpClient = client;
         _route = route;
      }

      /// <inheritdoc/>
      public async Task<T> Get<T>()
      {
         var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}{_route}");
         response.EnsureSuccessStatusCode();
         var responseJson = await response.Content.ReadAsStringAsync();
         var data = JsonConvert.DeserializeObject<T>(responseJson);
         return data;
      }
   }
}
