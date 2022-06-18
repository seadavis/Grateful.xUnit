using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace xAPI.DataAttributes
{
   [DataDiscoverer("HttpClientDataDiscoverer", "xAPI.DataAttributes")]
   [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
   public sealed class HttpClientDataAttribute : DataAttribute
   {
      private string route;

      public HttpClientDataAttribute(string route)
      {
         this.route = route;
      }

      public override IEnumerable<object[]> GetData(MethodInfo testMethod)
      {
         throw new NotImplementedException();
      }
   }
}
