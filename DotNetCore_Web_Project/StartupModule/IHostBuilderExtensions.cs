using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace StartupModule
{
    public static class IHostBuilderExtensions
    {
        public static IHostBuilder UseStartupModule(this IHostBuilder builder)
            => UseStartupModule(builder, options => options.DiscoverStartupModules());

        public static IHostBuilder UseStartupModule(this IHostBuilder builder, params Assembly[] assemblies) 
            => UseStartupModule(builder, options => options.DiscoverStartupModules(assemblies));
        
        public static IHostBuilder UseStartupModule(this IHostBuilder builder, Action<StartupModuleOptions> configure) {
            if (builder == null) {
                throw new ArgumentNullException(nameof(builder));
            }

            if(configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            var options = new StartupModuleOptions();
            configure(options);

            if (options.StartupModules.Count == 0 && options.ApplicationInitializers.Count == 0) {
                return builder;
            }

            var runner = new StartupModuleRunner(options);
            builder.ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<Microsoft.AspNetCore.Hosting.IStartupFilter>(sp => ActivatorUtilities.CreateInstance<ModulesStartupFilter>(sp, runner));

                var configureServicesContext = new ConfigureServicesContext(hostContext.Configuration, hostContext.HostingEnvironment, options);
                runner.ConfigureServices(services, hostContext.Configuration, hostContext.HostingEnvironment);
            });

            return builder;
        }
    }
}
