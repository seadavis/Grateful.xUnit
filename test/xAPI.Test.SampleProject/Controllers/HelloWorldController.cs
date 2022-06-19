using Microsoft.AspNetCore.Mvc;
using xAPI.Test.SampleProject.Data;

namespace xAPI.Test.SampleProject.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class HelloWorldController : ControllerBase
   {
      public HelloWorldController()
      {
         
      }

      [HttpPost] 
      public HelloWorldData Post([FromBody] HelloWorldData data)
      {
         return data;
      }

      [HttpDelete]
      public int Delete()
      {
         return 0;
      }

      [HttpGet]
      public HelloWorldData Get()
      {
         return new HelloWorldData()
         {
            Greeting = "Hello, man!",
            Name = "Sean!"
         };
      }
   }
}