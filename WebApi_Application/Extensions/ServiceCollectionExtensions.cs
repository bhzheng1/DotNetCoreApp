using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi_Application.DataAccess;

namespace WebApi_Application.Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataBaseAccess(this IServiceCollection services, Action<DbContextOptionsBuilder> action)
        {
            return services.AddDbContext<WorldDataBaseContext>(action, ServiceLifetime.Singleton);
        }
    }
    public static class AppExtensions
    {
        public static async Task AddAdminUser(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (await roleManager.FindByNameAsync("Admin") == null)
                {
                    await roleManager.CreateAsync(
                        new IdentityRole
                        {
                            Name = "Admin",
                            ConcurrencyStamp = Guid.NewGuid().ToString(),
                            NormalizedName = "Admin".ToUpper()
                        });
                }

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                if (await userManager.FindByNameAsync("Admin") == null)
                {
                    await userManager.CreateAsync(
                        new IdentityUser
                        {
                            Email = "Admin@mail.com",
                            UserName = "Admin",
                            SecurityStamp = Guid.NewGuid().ToString()
                        }, "Test!123");
                    var user = await userManager.FindByNameAsync("Admin");
                    await userManager.AddToRoleAsync(user!, "Admin");
                }
            }

            await Task.CompletedTask;
        }
    }
}

