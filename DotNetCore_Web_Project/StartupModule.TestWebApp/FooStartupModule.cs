using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace StartupModule.TestWebApp
{
    public class FooStartupModule : IStartupModule
    {
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
            
        }

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            if (context.HostingEnvironment.IsDevelopment()) { 
                //TODO
            }

            if ((bool)context.Options.Settings["AddFoo"] == true) {
                //TODO
            }
        }
    }
}
