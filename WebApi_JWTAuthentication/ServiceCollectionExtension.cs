using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApiJWTAuthentication.DataAccess.Entities;

namespace WebApiJWTAuthentication;

public static partial class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationDataAccess<T>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction) where T : DbContext
    {
        services.AddDbContext<T>(optionsAction);
        return services;
    }
    public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
    {

        return services;
    }
}
public static class HostExtensions
{
    public static async Task AddAdminUser(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(
                    new ApplicationRole
                    {
                        Name = "Admin",
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        NormalizedName = "Admin".ToUpper()
                    });
            }

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            if (await userManager.FindByNameAsync("Admin") == null)
            {
                await userManager.CreateAsync(
                    new ApplicationUser
                    {
                        Email = "Admin@mail.com",
                        UserName = "Admin",
                        SecurityStamp = Guid.NewGuid().ToString()
                    }, "Test!123");
                var user = await userManager.FindByNameAsync("Admin");
                await userManager.AddToRoleAsync(user!, "Admin");
            }
        }
    }
}
