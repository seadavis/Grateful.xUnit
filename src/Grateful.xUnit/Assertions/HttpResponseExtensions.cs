using Grateful.xUnit.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grateful.xUnit.Assertions
{
   public static class HttpResponseExtensions
   {

      public static HttpResponseAssertions<T> Should<T>(this HttpResponse<T> response)
      {
         return new HttpResponseAssertions<T>(response);
      }

   }
}
