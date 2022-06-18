using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace xAPI.DataAttributes
{
   public class HttpClientDataDiscoverer : IDataDiscoverer
   {
      public IEnumerable<object[]> GetData(IAttributeInfo dataAttribute, IMethodInfo testMethod)
      {
         throw new NotImplementedException();
      }

      public bool SupportsDiscoveryEnumeration(IAttributeInfo dataAttribute, IMethodInfo testMethod)
      {
         return false;
      }
   }
}
