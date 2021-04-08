using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace StartupModule
{
    class StartupModuleRunner
    {
        private readonly StartupModuleOptions _options;
        public StartupModuleRunner(StartupModuleOptions options)
        {
            _options = options;
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            var ctx = new ConfigureServicesContext(configuration, hostingEnvironment, _options);

            foreach (var cfg in _options.StartupModules)
            {
                cfg.ConfigureServices(services, ctx);
            }
        }

        public void Configure(IApplicationBuilder app, IConfiguration configuration, IHostingEnvironment hostingEnvironment) {
            using var scope = app.ApplicationServices.CreateScope();
            var ctx = new ConfigureMiddlewareContext(configuration, hostingEnvironment, scope.ServiceProvider, _options);
            foreach (var cfg in _options.StartupModules)
            {
                cfg.Configure(app,ctx);
            }
        }

        public async Task RunApplicationInitializers(IServiceProvider serviceProvider) {
            using var scope = serviceProvider.CreateScope();
            var applicationInitializers = _options.ApplicationInitializers.Select(t=>{
                try
                {
                    return ActivatorUtilities.CreateInstance(scope.ServiceProvider, t);
                }
                catch (Exception ex)
                {

                    throw new InvalidOperationException($"Failed to create instace of {nameof(IApplicationInitializer)} '{t.Name}'.", ex);
                }
            }).Cast<IApplicationInitializer>();

            foreach (var initializer in applicationInitializers)
            {
                try
                {
                    await initializer.Invoke();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"An exception occured during the execution of {nameof(IApplicationInitializer)} '{initializer.GetType().Name}'.", ex);
                }
            }
        }
    }
}
