using Newtonsoft.Json;
using System.Text;
using Grateful.xUnit.Exceptions;
using System.Net;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;
using Grateful.xUnit.Clients;
using Grateful.xUnit.Processes;
using Xunit.Abstractions;

namespace Grateful.xUnit.Clients
{
   internal class GratefulHttpClient : IHttpClient
   {
      #region Private Variables

      private ProcessRunner _runner;
      private ITestOutputHelper _output;

      #endregion

      #region Public Constructor

      internal GratefulHttpClient(ProcessRunner runner, ITestOutputHelper testOuputHelper)
      {
         _runner = runner;  
         _output = testOuputHelper;
      }

      #endregion

      #region IHttpClient Implmentation

      /// <inheritdoc/>
      public Task<HttpResponse<T>> Get<T>(string route)
      {
         return Get<T>(route, null); 
      }

      /// <inheritdoc/>
      public Task<HttpResponse<T>> Post<T, P>(string route, P postData)
      {
        return Post<T, P>(route, postData, null);

      }
      /// <inheritdoc/>
      public Task<HttpStatusCode> Delete(string route)
      {
         return Delete(route, null);
      }

      public async Task<HttpStatusCode> DeleteAuthorized(string route)
      {
         var token = await Authorize();
         return await Delete(route, token);
      }

      public async Task<HttpResponse<T>> PostAuthorized<T, P>(string route, P postData)
      {
         var token = await Authorize();
         return await Post<T, P>(route, postData, token);
      }

      // <inheritdoc />
      public async Task<HttpResponse<T>> GetAuthorized<T>(string route)
      { 
         var token = await Authorize();
         return await Get<T>(route, token);
      }

      #endregion

      #region Private Methods

      /// <summary>
      /// Authorizes and returns the access token
      /// </summary>
      /// <returns></returns>
      private async Task<String> Authorize()
      {
       
         string[] scopes = new string[] { $"api://{_runner.ClientConfig.APIClientId}/.default" };
         var authenticationResult = await _runner.Application.AcquireTokenForClient(scopes)
                           .ExecuteAsync();
         

         var token = authenticationResult?.AccessToken;
         if (token == null)
            throw new Exception("Missing Access Token");

         _output?.WriteLine($"Using MSAL Token: {token} for Authentication");
         return token;
      }

      private void CheckForErrors(HttpResponseMessage response)
      {
         if (!response.IsSuccessStatusCode)
         {
            var latestErrorMessage = _runner.CheckForLatestFailure();
            if (latestErrorMessage != null)
               throw new ServerSideException(latestErrorMessage);
         }
      }

      private async Task<HttpResponse<T>> Get<T>(string route, string token = null)
      {
         using (var client = GetClient(token))
         {
            var response = await client.GetAsync(route);
            return await AnaylzeResponse<T>(response);
         }

      }

      private HttpClient GetClient(string token = null)
      {
         return token == null ? _runner.CreateClient()
                              : _runner.CreateClient(new AuthenticationHeaderValue("Bearer", token));
      }

         
     private async Task<HttpResponse<T>> Post<T, P>(string route, P postData, string? token = null)
      {
         var json = JsonConvert.SerializeObject(postData);
         var content = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

         using (var client = GetClient(token))
         {
            var response = await client.PostAsync(route, content);
            return await AnaylzeResponse<T>(response);
         }

      }
      

      private async Task<HttpStatusCode> Delete(string route, string token = null)
      {
         using (var client = GetClient(token))
         {
            var response = await client.DeleteAsync(route);
            CheckForErrors(response);
            return response.StatusCode;
         }
      }

      private async Task<HttpResponse<T>> AnaylzeResponse<T>(HttpResponseMessage response)
      {
         CheckForErrors(response);

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
