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
         // returns the data from the method info and the data attribute.
         // data attribute.GetNamedArgument for the route then crawling up the testMethod for the projectname.
         // that method should probably be factored into an Extensions class.

         return null;
      }

      public bool SupportsDiscoveryEnumeration(IAttributeInfo dataAttribute, IMethodInfo testMethod)
      {
         return false;
      }
   }
}
