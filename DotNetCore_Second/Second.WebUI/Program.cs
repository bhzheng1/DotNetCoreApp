using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;

namespace Second.WebUI
{
    public class Program
    {
        public static Dictionary<string, string> arrayDict =
            new()
            {
                { "array:entries:0", "value0" },
                { "array:entries:1", "value1" },
                { "array:entries:2", "value2" },
                { "array:entries:4", "value4" },
                { "array:entries:5", "value5" }
            };
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateBootstrapLogger();
            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return;
            }
            finally
            {
                Log.CloseAndFlush();
            }
            return;
        }

        //https://github.com/dotnet/AspNetCore.Docs/tree/main/aspnetcore/fundamentals/configuration/index/samples/3.x/ConfigSample
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddInMemoryCollection(arrayDict);

                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile(
                        $"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    config.AddJsonFile(
                        "MyConfig.json", optional: true, reloadOnChange: true);
                    config.AddXmlFile(
                        "MyXML.xml", optional: true, reloadOnChange: true);
                    config.AddCommandLine(args);
                }).UseSerilog((context, services, configuration) => configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services)
                        .Enrich.FromLogContext()) // Full setup of serilog. We read log settings from appsettings.json
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
