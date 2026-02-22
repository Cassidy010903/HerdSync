using DAL.Configuration.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HerdSync.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EchoTest : ControllerBase
    {
        private readonly HerdsyncDBContext _context;

        public EchoTest(HerdsyncDBContext context)
        {
            _context = context;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<EchoTest> _logger;

        public EchoTest(ILogger<EchoTest> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("echo-test")]
        public async Task<IActionResult> EchoTestCall()
        {
            await _context.Database.ExecuteSqlRawAsync("SELECT 1");
            return Ok("Database reachable");
        }
    }
}