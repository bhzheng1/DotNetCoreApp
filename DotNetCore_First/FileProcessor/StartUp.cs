using System;
using System.Net.Http;
using FileProcessor.ApiCaller;
using FileProcessor.ApiCaller.DocRepo;
using FileProcessor.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using NLog.Web;

namespace FileProcessor
{
    public class StartUp
    {
        public StartUp(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            env.ConfigureNLog($"NLog.{env.EnvironmentName}.config");
        }

        public IConfiguration Configuration { get; }

        //This method gets called by the runtime, using it to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                //check the user consent for non-essential cookies is needed for a given request
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton(_ => Configuration);

            services.AddMvc();

            services.Configure<AppConfig>(Configuration);

            services.AddScoped<HttpClient,HttpClient>(ctx=>new HttpClient()
            {
                DefaultRequestHeaders =
                {
                    {"userId",Guid.NewGuid().ToString() },
                    {"clientId",Guid.NewGuid().ToString() }
                }
            });

            services.AddTransient<IDocumentStorageApiCaller, DocumentStorageApiCaller>();
            services.AddHttpClient();
            services.AddHttpClient<DocRepoClient>();
            services.AddTransient<ITypedHttpClientFactory<DocRepoClient>, DocClientRepoFactory>();
        }
    }
}