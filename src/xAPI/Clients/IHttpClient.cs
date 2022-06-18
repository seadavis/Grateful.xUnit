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
      /// <param name="route">the route relative to the clients base URL we are grabbing</param>
      /// <returns>the data deserialized via Json through this client</returns>
      T Get<T>(string route);

   }
}
