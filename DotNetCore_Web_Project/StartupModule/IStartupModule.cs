using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

//模块化的实验思路，写个接口=>模块类继承该接口=>项目启动反射检索=>调用接口实现。
namespace StartupModule
{
    public interface IStartupModule
    {
        void ConfigureServices(IServiceCollection servicest, ConfigureServicesContext context);
        void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context);
    }
}
