using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace StartupModule.TestWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseStartupModule(_=>_.Settings["AddFoo"]=true)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
