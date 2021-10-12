using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Second.DataAccess;
using Second.DataAccess.Repositories;
using Second.DataAccess.sakila;
using Second.Model;
using Second.WebUI.Utils;
using System;
using System.IO;

namespace Second.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
            //C:\Code\DotNetCore_Project\DotNetCore_Second\Second.WebUI
            var contentRoot1 = env.ContentRootPath;
            //C:\Code\DotNetCore_Project\DotNetCore_Second\Second.WebUI
            var contentRoot2 = configuration.GetValue<string>(WebHostDefaults.ContentRootKey);
            //C:\Code\DotNetCore_Project\DotNetCore_Second\Second.WebUI\bin\Debug\net5.0\
            var contentRoot3 = AppContext.BaseDirectory;
            //C:\Code\DotNetCore_Project\DotNetCore_Second\Second.WebUI\bin\Debug\net5.0\
            var contentRoot4 = AppDomain.CurrentDomain.BaseDirectory;

            IronPdf.Installation.TempFolderPath = Path.Combine(AppContext.BaseDirectory,"IronPdfTemp");
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IMvcBuilder builder = services.AddRazorPages();

#if DEBUG
            if (Env.IsDevelopment())
            {
                builder.AddRazorRuntimeCompilation();
            }
#endif
            var defaultDb = Configuration.GetConnectionString("ApplicationDb");
            var sakila = Configuration.GetConnectionString("sakila");
            services.AddControllersWithViews();
            services.AddDbContext<ApplicationDbContext>(_ => _.UseSqlServer(defaultDb));

            //enable lazy loading
            services.AddDbContext<SakilaContext>(_ => _.UseLazyLoadingProxies().UseSqlServer(sakila));
            services.AddSingleton(s => Configuration);
            services.AddSingleton(s => new DbConfiguration
            {
                ApplicationDbConn = defaultDb,
                SakilaConn = sakila
            });
            services.AddTransient<RepositoryContext>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddDistributedMemoryCache();

            services.AddApplicationInsightsTelemetry();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
