using Microsoft.AspNetCore.Mvc;
using Webhook_ConsumerDemo.Models;
using Webhook_ConsumerDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<AgentService>();
builder.Services.AddHostedService<CallService>();
builder.Services.AddCors();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/// <summary>endpoint for callback in server</summary>
app.MapPost("/api/agentStatus", async (AgentService agentService, [FromBody] StatusChange status) =>
{
    var agent = agentService.Agents.FirstOrDefault(a => a.Id == status.AgentId);
    var callLog = new CallLog { Status = status.NewStatus, StatusStartTime = status.ChangedTime };
    agent?.Activities.Add(callLog);
    Console.WriteLine($"Agent {agent.Id} changed status to {status.NewStatus}");
});

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
