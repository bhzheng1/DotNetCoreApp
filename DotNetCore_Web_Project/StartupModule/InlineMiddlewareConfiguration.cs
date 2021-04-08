using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

//模块化的实验思路，写个接口=>模块类继承该接口=>项目启动反射检索=>调用接口实现。
namespace StartupModule
{
    public class InlineMiddlewareConfiguration : IStartupModule
    {
        private readonly Action<IApplicationBuilder, ConfigureMiddlewareContext> _action;

        public InlineMiddlewareConfiguration(Action<IApplicationBuilder, ConfigureMiddlewareContext> action)
        {
            _action = action;
        }

        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context) => _action(app, context);

        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context) { }
    }
}
