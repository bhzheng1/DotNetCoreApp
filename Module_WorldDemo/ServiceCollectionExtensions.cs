using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Module_WorldDemo.DataAccess;

namespace Module_WorldDemo;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddWorldDataBaseAccess(this IServiceCollection services, Action<DbContextOptionsBuilder> action)
    {
        return services.AddDbContext<WorldDataBaseContext>(action, ServiceLifetime.Singleton);
    }
    public static IServiceCollection AddWorldModuleDependencies(this IServiceCollection services)
    {
        services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
        services.AddScoped(typeof(ICompanyRepository), typeof(CompanyRepository));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(WorldDataBaseContext).Assembly));
        return services;
    }
}