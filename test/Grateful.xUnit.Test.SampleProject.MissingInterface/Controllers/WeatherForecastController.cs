using Microsoft.AspNetCore.Mvc;
using Grateful.xUnit.Test.SampleProject.MissingInterface.Interfaces;

namespace Grateful.xUnit.Test.SampleProject.MissingInterface.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class WeatherForecastController : ControllerBase
   {
      private static readonly string[] Summaries = new[]
      {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

      private readonly ILogger<WeatherForecastController> _logger;

      public WeatherForecastController(ILogger<WeatherForecastController> logger, IUseless useless)
      {
         _logger = logger;
      }

      [HttpPost]
      public WeatherForecast Post(WeatherForecast f)
      {
         return new WeatherForecast();
      }

      [HttpDelete]
      public void Delete()
      {

      }

      [HttpGet]
      public IEnumerable<WeatherForecast> Get()
      {
         return Enumerable.Range(1, 5).Select(index => new WeatherForecast
         {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
         })
         .ToArray();
      }
   }
}