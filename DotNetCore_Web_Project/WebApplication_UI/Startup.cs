using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelperClassLibrary;
using HtmlTags;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Webapplication_API_Client;
using WebApplication_UI.Models;
using WebApplication_UI.Tags;

namespace WebApplication_UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            
            //session service
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(1);//You can set Time   
            });

            //get configuration from appsettings
            //service could be injected as IOptions<HostSettings> or add service directly to IoC container
            services.Configure<HostSettings>(Configuration.GetSection("HostSettings"));
            services.Configure<ClientSettings>(Configuration.GetSection("ClientSettings"));
            services.Configure<ActivityTimeoutSettings>(Configuration.GetSection("ActivityTimeoutSettings"));

            // Add functionality for injecting other IOptions<T>
            services.AddOptions();

            //add singleton service to IoC container
            services.AddSingleton(s => s.GetService<IOptions<HostSettings>>().Value);
            services.AddSingleton(s => s.GetService<IOptions<ClientSettings>>().Value);

            // If you need access to generic IConfiguration this is **required**
            services.AddSingleton(Configuration);

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddHttpContextAccessor();
            services.AddTransient<WebSecurityContext, WebSecurityContext>();
            services.AddScoped<RazorViewToStringRender,RazorViewToStringRender>();

            //add webapiclient to the service container 
            services.AddHttpClient<IWebApiClient, WebApiClient>().ConfigureHttpClient((s, c) =>
            {
                var context = s.GetService<WebSecurityContext>();
                var userId = context.UserId;
                var clientId = context.ClientId;

                c.BaseAddress = new Uri(s.GetService<ClientSettings>().ApiHostUrl);
                c.DefaultRequestHeaders.Add("userId", userId.ToString());
                c.DefaultRequestHeaders.Add("clientId", clientId.ToString());
            });
            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            
            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
