using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xAPI.Fixtures;

namespace xAPI.Tests.Collections
{
   public class WorkingProjectFixture : HttpClientFixture
   {
      public WorkingProjectFixture() : base(@"..\..\..\..\xAPI.Test.SampleProject")
      {

      }

   }
}
