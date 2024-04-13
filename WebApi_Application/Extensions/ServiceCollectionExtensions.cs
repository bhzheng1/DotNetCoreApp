using Microsoft.EntityFrameworkCore;
using WebApi_Application.DataAccess;

namespace WebApi_Application.Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataBaseAccess(this IServiceCollection services, Action<DbContextOptionsBuilder> action)
        {
            return services.AddDbContext<AppDataBaseContext>(action, ServiceLifetime.Singleton);
        }
    }
}

