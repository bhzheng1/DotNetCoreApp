namespace WebApi_JWTAuth_Resource_Server;
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
public static class WeatherForecastExtensions
{
    public static void AddWeatherForecast(this WebApplication app)
    {
        var weatherForecast = app.MapGroup("/weatherForecast");
        weatherForecast.MapGet("/", GetWeatherForecast);
    }

    static async Task<IResult> GetWeatherForecast()
    {
        var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
        var forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                )).ToArray();
        return TypedResults.Ok(forecast);
    }
}