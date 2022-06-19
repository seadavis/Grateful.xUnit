using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace xAPI.Clients
{
   /// <summary>
   /// Class that contains simplified
   /// response data for data of type "T"
   /// </summary>
   /// <typeparam name="T">The type of the response</typeparam>
   public class HttpResponse<T>
   {
      public HttpStatusCode Status { get; internal set; }

      public T Data { get; init; }

      internal HttpResponse()
      {

      }

   }
}
