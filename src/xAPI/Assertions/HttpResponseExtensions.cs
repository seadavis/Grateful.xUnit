using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xAPI.Clients;

namespace xAPI.Assertions
{
   public static class HttpResponseExtensions
   {

      public static HttpResponseAssertions<T> Should<T>(this HttpResponse<T> response)
      {
         return new HttpResponseAssertions<T>(response);
      }

   }
}
