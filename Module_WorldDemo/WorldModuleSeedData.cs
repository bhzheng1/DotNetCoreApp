using Module_WorldDemo.DataAccess;
using Module_WorldDemo.Entities;

namespace Module_WorldDemo;

internal static class WorldModuleSeedData
{
    internal static async Task SeedWorldDatabase(WorldDataBaseContext dataBaseContext)
    {
        if (!dataBaseContext.Companies.Any())
        {
            await dataBaseContext.Companies.AddRangeAsync([
                new Company() { Id = 1, Name = "Company 1", Address = "Address 1" },
                new Company() { Id = 2, Name = "Company 2", Address = "Address 2" },
                new Company() { Id = 3, Name = "Company 3", Address = "Address 3" }
            ]);
        }

        if (!dataBaseContext.Products.Any())
        {
            await dataBaseContext.Products.AddRangeAsync([
                new Product() { Id = 1, Name = "Product 1", Price = 100 },
                new Product() { Id = 2, Name = "Product 2", Price = 200 },
                new Product() { Id = 3, Name = "Product 3", Price = 300 }
            ]);
        }
        await dataBaseContext.SaveChangesAsync();
    }
}