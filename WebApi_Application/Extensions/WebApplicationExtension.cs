using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi_Application.DataAccess;

namespace WebApi_Application.Extensions;

internal static class WebApplicationExtension
{
    internal static async Task EnsureAppDatabaseUpdated(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<AppDataBaseContext>();
            await context.Database.MigrateAsync();
            await AddAdminUser(services);
        }

        await Task.CompletedTask;
    }

    internal static async Task AddAdminUser(this IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        var roles = new IdentityRole[]
        {
            new()
            {
                Name = "Admin", ConcurrencyStamp = Guid.NewGuid().ToString(), NormalizedName = "Admin".ToUpper()
            },
            new()
            {
                Name = "User", ConcurrencyStamp = Guid.NewGuid().ToString(), NormalizedName = "User".ToUpper()
            },
            new()
            {
                Name = "Guest", ConcurrencyStamp = Guid.NewGuid().ToString(), NormalizedName = "Guest".ToUpper()
            }
        };

        var adminUser = new IdentityUser
        {
            Email = "Admin@mail.com",
            UserName = "Admin",
            SecurityStamp = Guid.NewGuid().ToString()
        };

        foreach (var role in roles)
        {
            if (await roleManager.FindByNameAsync(role.Name!) == null)
            {
                await roleManager.CreateAsync(role);
            }
        }

        if (await userManager.FindByNameAsync("Admin") == null)
        {
            await userManager.CreateAsync(adminUser, "Test!123");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
        await Task.CompletedTask;
    }
}