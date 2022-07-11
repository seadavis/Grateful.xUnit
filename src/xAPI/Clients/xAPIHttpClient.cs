﻿using Newtonsoft.Json;
using System.Text;
using xAPI.Exceptions;
using xAPI.Processes;
using System.Net;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;

namespace xAPI.Clients
{
   internal class xAPIHttpClient : IHttpClient
   {
      #region Private Variables

      private ProcessRunner _runner;

      #endregion

      #region Public Constructor

      public xAPIHttpClient(ProcessRunner runner)
      {
         _runner = runner;  
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
         AuthenticationResult? authenticationResult = null;

         try
         {
            string[] scopes = new string[] { $"api://{_runner.ClientConfig.APIClientId}/.default" };
            authenticationResult = await _runner.Application.AcquireTokenForClient(scopes)
                              .ExecuteAsync();
         }
         catch (MsalUiRequiredException ex)
         {
            // The application doesn't have sufficient permissions.
            // - Did you declare enough app permissions during app creation?
            // - Did the tenant admin grant permissions to the application?
            int test = 5;
         }
         catch (MsalServiceException ex) when (ex.Message.Contains("AADSTS70011"))
         {
            // Invalid scope. The scope has to be in the form "https://resourceurl/.default"
            // Mitigation: Change the scope to be as expected.
            int test = 15;
         }


         var token = authenticationResult?.AccessToken;
         if (token == null)
            throw new Exception("Missing Access Token");

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
