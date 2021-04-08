using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using One.API.Client;

namespace One.UI
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        private readonly ILogger _logger;
        public Startup(IConfiguration configuration, IHostingEnvironment env, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _env = env;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Config our setting so it can be injected as IOptions<HostSettings>
            services.Configure<HostSettings>(Configuration.GetSection("HostSettings"));
            services.Configure<ClientSettings>(Configuration.GetSection("ClientSettings"));
            services.Configure<ActivityTimeoutSettings>(Configuration.GetSection("ActivityTimeoutSettings"));

            //add singleton for directly inject class
            services.AddSingleton(s => s.GetService<IOptions<HostSettings>>().Value);
            services.AddSingleton(s => s.GetService<IOptions<ClientSettings>>().Value);

            // Add functionality to inject other IOptions<T>
            services.AddOptions();
            // *If* you need access to generic IConfiguration this is **required**
            services.AddSingleton(Configuration);

            services.AddLocalization(_ => _.ResourcesPath = "Resources");
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
            {
                option.Cookie.HttpOnly = true;
                option.Cookie.SecurePolicy = _env.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always;
                option.Cookie.SameSite = SameSiteMode.Lax;
                option.Cookie.Name = "AuthCookieAspNetCore";
                option.LoginPath = "/CoreAuth/Login";
                option.LogoutPath = "/CoreAuth/Logout";
                option.EventsType = typeof(RevokeAuthenticationEvents);
            });

            services.AddHttpContextAccessor();
            services.AddTransient<MySecurityContext, MySecurityContext>();
            services.AddHttpClient<IOneApiClient, OneApiClient>().ConfigureHttpClient((s, c) =>
             {
                 var context = s.GetService<MySecurityContext>();
                 var userId = context.UserId;
                 var clientId = context.ClientId;

                 c.BaseAddress = new Uri(s.GetService<ClientSettings>().ApiHostUrl);
                 c.DefaultRequestHeaders.Add("userId", userId.ToString());
                 c.DefaultRequestHeaders.Add("clientId", clientId.ToString());
             });

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services
                .AddMvc()
                .AddViewLocalization(
                LanguageViewLocationExpanderFormat.Suffix,
                    opts => { opts.ResourcesPath = "Resources"; })
                .AddDataAnnotationsLocalization()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(3600);
            });

            services.AddAntiforgery(options =>
            {
                // Set Cookie properties using CookieBuilder properties.
                options.Cookie.Name = "__RequestVerificationToken";
                options.FormFieldName = "AntiforgeryFieldname";
                options.HeaderName = "X-CSRF-TOKEN-HEADERNAME";
                options.SuppressXFrameOptionsHeader = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseCookiePolicy();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
