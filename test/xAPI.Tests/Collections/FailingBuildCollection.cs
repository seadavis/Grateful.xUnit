using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace xAPI.Tests.Collections
{
   [CollectionDefinition("ASP.NET Collection with a failing build ")]
   public class FailingBuildCollection : ICollectionFixture<FailingBuildFixture>
   {
   }
}
