using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Grateful.xUnit.Test.SampleProject.Data;

namespace Grateful.xUnit.Test.SampleProject.Controllers
{
  
   [ApiController]
   [Route("[controller]")]
   public class HelloWorldController : ControllerBase
   {
      public HelloWorldController()
      {

      }

      [Authorize]
      [HttpGet("api/auth")]
      public HelloWorldData GetAuthorized()
      {
         return new HelloWorldData()
         {
            Greeting = "Salutations!",
            Name = "Authorized User!"
         };
      }

      [Authorize]
      [HttpPost("api/auth")]
      public HelloWorldData PostAuthorized()
      {
         return new HelloWorldData()
         {
            Greeting = "Salutations!",
            Name = "Post Authorized User!"
         };
      }

      [Authorize]
      [HttpDelete("api/auth")]
      public HelloWorldData DeleteAuthorized()
      {
         return new HelloWorldData()
         {
            Greeting = "Salutations!",
            Name = "Delete Authorized User!"
         };
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