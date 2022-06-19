using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xAPI.Exceptions
{
   /// <summary>
   /// Use this exception when a process is 
   /// started but it doesn't even work in the first place.
   /// </summary>
   public class StartingProcessException : Exception
   {

      public StartingProcessException(string msg) : base(msg)
      {

      }

   }
}
