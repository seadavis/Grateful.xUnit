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
      #region Private Variables

      private HttpClient _httpClient;

      #endregion

      #region Public Constructor

      public xAPIHttpClient(HttpClient client)
      {
         _httpClient = client;
        
      }
      #endregion

      #region IHttpClient Implmentation

      /// <inheritdoc/>
      public async Task<HttpResponse<T>> Get<T>(string route)
      {
         var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}{route}");
         var responseJson = await response.Content.ReadAsStringAsync();
         return new HttpResponse<T>()
         {
            Data = JsonConvert.DeserializeObject<T>(responseJson),
            Status = response.StatusCode
         };
      }

      #endregion
   }
}
