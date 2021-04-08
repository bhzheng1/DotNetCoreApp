using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Web;
using NSBus;
using One.DAL;
using CorrelationId;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using One.API.Utility;
using One.Business.hr.Country;
using One.DAL.Repositories;

namespace One.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration,IHostingEnvironment env)
        {
            Configuration = configuration;
            env.ConfigureNLog($"NLog.{env.EnvironmentName}.config");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            SqlMapper.Settings.CommandTimeout =
                Configuration.GetValue<int>("DatabaseSettings:DefaultCommandTimeOutDuration");
            services.AddCorrelationId();
            services.Configure<HostSettings>(Configuration.GetSection("HostSettings"));
            services.AddSingleton(s=>s.GetService<IOptions<HostSettings>>().Value);

            services.Configure<FormOptions>(_ =>
            {
                _.ValueLengthLimit = int.MaxValue;
                _.MultipartBodyLengthLimit = int.MaxValue;
            });
            services.AddCors(_ => { _.AddPolicy("One", policy => policy.WithOrigins("http://localhost:6542")); });
            services.AddCors(_ => { _.AddPolicy("One", policy => policy.WithOrigins("http://localhost:6524").AllowAnyHeader().AllowAnyMethod().AllowCredentials()); });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            var connectionString = Configuration.GetConnectionString("Oracle");
            services.AddDbContext<OracleDbContext>(_ => _.UseSqlServer(connectionString));
            services.AddSingleton(Configuration);


            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddTransient<RepositoryContext, RepositoryContext>();
            services.AddTransient<ICountryRepository,CountryRepository>();
            services.AddTransient<ICountryBusiness,CountryBusiness>();

            services.AddHttpContextAccessor();
            services.AddScoped<MySecurityContext, MySecurityContext>();

            //services.AddNServiceBus()
            //    .ConfigureEndpointName(Configuration.GetValue<string>("NServiceBus:EndpointName"))
            //    .ConfigureUniqueAddressName(Configuration.GetValue<string>("NServiceBus:UniqueAddressName"))
            //    .ConfigureRabbitMQConnectionString(Configuration.GetValue<string>("NServiceBus:RabbitMQConnectionString"))
            //    .ConfigureSecurityContextHeaders("userId","clientId")
            //    .AddPipelineBehavior(new RolePermissionsSetup(), "role permission setup")
            //    .ConfigurePersistence(_=>new OracleDbContext(_),connectionString,"dbo","",Option.None<IDatabaseInitializer>())
            //    .Build();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCorrelationId();
            app.UseCors(_ => _.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
