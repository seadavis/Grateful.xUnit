﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xAPI.Exceptions;
using xAPI.Processes;

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
      public async Task<HttpResponse<T>> Get<T>(string route)
      {
         var response = await _runner.Client.GetAsync(BuildAddress(route));
         return await AnaylzeResponse<T>(response);
      }


      public async Task<HttpResponse<T>> Post<T, P>(string route, P postData)
      {
         var json = JsonConvert.SerializeObject(postData);
         var content = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
         var response = await _runner.Client.PostAsync(BuildAddress(route), content);
         return await AnaylzeResponse<T>(response);
      }

      #endregion

      #region Private Methods

      private string BuildAddress(string route)
      {
         return $"{_runner.Client.BaseAddress}{route}";
      }

      private async Task<HttpResponse<T>> AnaylzeResponse<T>(HttpResponseMessage response)
      {
         if (!response.IsSuccessStatusCode)
         {
            var latestErrorMessage = _runner.CheckForLatestFailure();
            if (latestErrorMessage != null)
               throw new ServerSideException(latestErrorMessage);
         }

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
