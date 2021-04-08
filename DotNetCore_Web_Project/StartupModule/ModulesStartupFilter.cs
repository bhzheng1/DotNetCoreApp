using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace StartupModule
{
    class ModulesStartupFilter : Microsoft.AspNetCore.Hosting.IStartupFilter
    {
        private readonly StartupModuleRunner _runner;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ModulesStartupFilter(StartupModuleRunner runner, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _runner = runner;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder => {
                _runner.Configure(builder, _configuration, _hostingEnvironment);
                _runner.RunApplicationInitializers(builder.ApplicationServices).GetAwaiter().GetResult();
                next(builder);
            };
        }
    }
}
