using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace WebApi.ApiKeyAuthenticationClient
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration Configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .AddCommandLine(args)
                    .Build();
            var endPointAddress = "http://localhost:8080/";
            var client = new HttpClient();
            var apiKey = Convert.ToBase64String(Encoding.ASCII.GetBytes(Configuration.GetSection("ApiKey").Value));

            client.DefaultRequestHeaders.Add("ApiKey", apiKey);
            var apiUrl = $"{endPointAddress}api/WeatherForecast";
            var response = client.GetAsync(apiUrl).Result;
            var rst = response.Content.ReadAsStringAsync().Result;
            var weatherForecasts = JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(rst);
            foreach (var item in weatherForecasts)
            {
                Console.WriteLine(item.Summary);
            }
            Console.WriteLine("Hello World!");
        }
    }
}
