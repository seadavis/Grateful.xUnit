using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xAPI.Clients
{
   internal class xAPIHttpClient : IHttpClient
   {
      private string baseUrl;
      private string route;

      public xAPIHttpClient(string baseUrl, string route)
      {

      }

      /// <inheritdoc/>
      public T Get<T>()
      {
         throw new NotImplementedException();
      }
   }
}
