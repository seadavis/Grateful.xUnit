using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xAPI.Exceptions
{
   /// <summary>
   /// Use this exception when we encounter
   /// an error on the server side.
   /// </summary>
   public class ServerSideException : Exception
   {

      public ServerSideException(string msg) : base(msg)
      {

      }

   }
}
