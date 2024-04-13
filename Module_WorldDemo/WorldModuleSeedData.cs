using Module_WorldDemo.DataAccess;

namespace Module_WorldDemo;

internal static class WorldModuleSeedData
{
    internal static void SeedWorldDatabase(WorldDataBaseContext dataBaseContext)
    {
        if (!dataBaseContext.Companies.Any())
        {
        }

        if (!dataBaseContext.Products.Any())
        {
            
        }
    }
}