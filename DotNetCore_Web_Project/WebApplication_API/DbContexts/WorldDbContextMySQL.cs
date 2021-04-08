using Microsoft.EntityFrameworkCore;
using WebApplication_API.WorldModels;

namespace WebApplication_API.DbContexts
{
    public class WorldDbContextMySQL : DbContext, IWorldDbContextMySQL
    {
        public WorldDbContextMySQL(DbContextOptions<WorldDbContextMySQL> options)
            : base(options) { }

        public DbSet<Country> Country { get; set; }
        public DbSet<City> City { get; set; }
    }
}