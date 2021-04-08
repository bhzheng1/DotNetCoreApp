using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace StartupModule
{
    public class ConfigureServicesContext
    {
        public ConfigureServicesContext(IConfiguration configuration,IHostingEnvironment hostingEnvironment, StartupModuleOptions options)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            Options = options;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }
        public StartupModuleOptions Options {get;}
    }
}
