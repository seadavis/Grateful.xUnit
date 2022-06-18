using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xAPI.Clients
{
   internal class xAPIHttpClient : IHttpClient
   {

      public xAPIHttpClient(string baseUrl)
      {

      }

      /// <inheritdoc/>
      public T Get<T>(string route)
      {
         throw new NotImplementedException();
      }
   }
}
