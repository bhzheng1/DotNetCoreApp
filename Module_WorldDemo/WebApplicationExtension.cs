using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Module_WorldDemo.DataAccess;

namespace Module_WorldDemo;

public static partial class WebApplicationExtension
{
    public static async Task EnsureModuleDatabaseUpdated(this IHost app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<WorldDataBaseContext>();
            await context.Database.MigrateAsync();
            await WorldModuleSeedData.SeedWorldDatabase(context);
        }
        await Task.CompletedTask;
    }
}