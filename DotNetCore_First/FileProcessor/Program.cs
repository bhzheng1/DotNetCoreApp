using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace FileProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                    builder.AddJsonFile("appsettings.d/appsettings.json", true, true)
                        .AddJsonFile($"appsettings.d/appsettings.{context.HostingEnvironment.EnvironmentName}.json",true,true))
                .UseStartup<StartUp>()
                .ConfigureLogging(_=>_.ClearProviders().SetMinimumLevel(LogLevel.Trace))
                .UseNLog();
    }
}
