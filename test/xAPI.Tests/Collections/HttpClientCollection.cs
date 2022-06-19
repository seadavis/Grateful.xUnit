using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace xAPI.Tests.Collections
{
   [CollectionDefinition("ASP.NET Working Collection")]
   public class HttpClientCollection : ICollectionFixture<WorkingProjectFixture>
   {
   }
}
