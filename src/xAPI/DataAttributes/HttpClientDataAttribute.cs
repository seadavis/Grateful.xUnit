using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using xAPI.Clients;
using xAPI.Processes;
using Xunit.Sdk;

namespace xAPI.DataAttributes
{
  
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
         var requires = testMethod.DeclaringType.GetCustomAttribute<RequireASPNet>();
         var httpClient = ProcessCollection.Instance.GetClient(requires.ProjectPath);
         yield return new[] { new xAPIHttpClient(httpClient, route) };
      }
   }
}
