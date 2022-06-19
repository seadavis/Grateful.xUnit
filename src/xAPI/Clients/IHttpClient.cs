using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xAPI.Clients
{
   public interface IHttpClient
   {

      /// <summary>
      /// 
      /// </summary>
      /// <typeparam name="T">The type of data to deserialize into</typeparam>
      /// <param name="route">The name of the route we ant to call in the HttpClient</param>
      Task<T> Get<T>(string route);

   }
}
