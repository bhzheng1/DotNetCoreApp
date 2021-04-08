
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModelClassLibrary.CommonModels;
using System.Reflection;
using WebApplication_API.DbContexts;
using WebApplication_API.Repositories;
using WebApplication_API.Validators;

namespace WebApplication_API
{
    public class Startup
    {
        private IConfiguration _config;
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WorldDbContextMySQL>(options => options.UseMySQL(_config.GetConnectionString("mySQL")),ServiceLifetime.Singleton);
            services.AddDbContext<SakilaContextMSSQL>(options =>options.UseSqlServer(_config.GetConnectionString("msSQL")), ServiceLifetime.Singleton);
            services.AddDbContext<ContosoContext>(options => options.UseSqlServer(_config.GetConnectionString("Contoso")), ServiceLifetime.Singleton);
            var fakeDate = new FakeData();
            var emailConfig = new EmailConfig();
            _config.GetSection("EmailConfig").Bind(emailConfig);
            //Create singleton from instance
            services.AddSingleton<EmailConfig>(emailConfig);
            services.AddSingleton<FakeData>(fakeDate);
            services.AddAutoMapper(typeof(Startup));
            services.AddSingleton<ICategoryRepository,CategoryRepository>();
            services.AddSingleton<IAddressRepository, AddressRepository>();
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(BusinessValidationPipeline<,>));
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);

            services.AddHttpContextAccessor();
            services.AddControllers();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(_config["custom_key"]);
                });
                endpoints.MapControllers();
            });
        }
    }
}
