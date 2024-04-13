using Microsoft.AspNetCore.Mvc;
using WebApi_GraphQL.Models;

namespace WebApi_GraphQL.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing",
        "Bracing",
        "Chilly",
        "Cool",
        "Mild",
        "Warm",
        "Balmy",
        "Hot",
        "Sweltering",
        "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(template: "getWeatherForecast", Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        var forecast = Enumerable
            .Range(1, 5)
            .Select(
                index =>
                    new WeatherForecast(
                        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        Random.Shared.Next(-20, 55),
                        Summaries[Random.Shared.Next(Summaries.Length)]
                    )
            )
            .ToArray();
        return forecast;
    }
}
