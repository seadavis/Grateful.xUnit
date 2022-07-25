using Grateful.xUnit.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grateful.xUnit.Tests.Collections
{
   public class WorkingProjectFixture : HttpClientFixture
   {
      public WorkingProjectFixture() : base(@"..\..\..\..\Grateful.xUnit.Test.SampleProject")
      {

      }

   }
}
