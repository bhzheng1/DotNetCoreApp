using MassTransit;
using MassTransit_Contracts;
using Microsoft.AspNetCore.Mvc;

namespace MassTransit_Publisher.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IRequestClient<SubmitOrder> _client;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,
        IPublishEndpoint endpoint,
        IRequestClient<SubmitOrder> client)
    {
        _logger = logger;
        _publishEndpoint = endpoint;
        _client = client;
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

    [HttpGet(nameof(SendHello))]
    public async Task<IActionResult> SendHello()
    {
        await _publishEndpoint.Publish(new Hello { Value = "test" });
        return Ok();
    }

    [HttpPost(nameof(SubmitOrder))]
    public async Task<IActionResult> SubmitOrder(OrderModel order)
    {
        var response = await _client.GetResponse<OrderSubmissionAccepted>(new SubmitOrder
        {
            OrderId=order.OrderId,
            OrderNumber = order.OrderNumber,
            Timestamp = DateTimeOffset.Now
        });

        return Accepted(response.Message);
    }
}

public record OrderModel {
    public Guid OrderId { get; init; }
    public string OrderNumber { get; init; }
}

