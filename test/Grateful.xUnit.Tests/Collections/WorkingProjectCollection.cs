using Xunit;

namespace Grateful.xUnit.Tests.Collections
{
   [CollectionDefinition("ASP.NET Working Collection")]
   public class WorkingProjectCollection : ICollectionFixture<WorkingProjectFixture>
   {
   }
}
