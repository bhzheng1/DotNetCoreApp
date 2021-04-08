using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace StartupModule
{
    public class ConfigureMiddlewareContext
    {
        public ConfigureMiddlewareContext(IConfiguration configuration, IHostingEnvironment hostingEnvironment, IServiceProvider serviceProvider, StartupModuleOptions options)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            ServiceProvider = serviceProvider;
            Options = options;
        }
        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }
        public IServiceProvider ServiceProvider { get; }
        public StartupModuleOptions Options { get; }
    }
}