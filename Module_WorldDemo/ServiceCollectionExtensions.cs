using ClassLibrary_DataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ClassLibrary_DataAccess;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddClDataBaseAccess(this IServiceCollection services, Action<DbContextOptionsBuilder> action)
    {
        return services.AddDbContext<ClDataBaseContext>(action, ServiceLifetime.Singleton);
    }
    public static IServiceCollection AddDataAccessModuleDI(this IServiceCollection services)
    {
        services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
        services.AddScoped(typeof(ICompanyRepository), typeof(CompanyRepository));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ClDataBaseContext).Assembly));
        return services;
    }
}

public static partial class WebApplicationExtensions
{
    public static async Task EnsureDatabaseCreated(this IHost app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ClDataBaseContext>();
            await context.Database.EnsureCreatedAsync();
        }
        await Task.CompletedTask;
    }

}