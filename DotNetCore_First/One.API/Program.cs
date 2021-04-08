using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace One.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var logger = NLogBuilder.ConfigureNLog(environment == "" ? "nlog.config" : $"nlog.{environment}.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.d/appsettings.json", optional: true, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.d/appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    context.HostingEnvironment.ConfigureNLog(context.HostingEnvironment.EnvironmentName==""? "nlog.config" : $"nlog.{context.HostingEnvironment.EnvironmentName}.config");
                })
                .UseStartup<Startup>()
                .ConfigureLogging(_ => _.ClearProviders().SetMinimumLevel(LogLevel.Trace))
                .UseNLog()
                .UseKestrel(options =>
                {
                    //options.Limits.MaxRequestBodySize = 262144000; //250MB
                    options.Limits.MaxRequestBodySize = 1048576000; // 1 GB
                    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10);
                });
    }
}
