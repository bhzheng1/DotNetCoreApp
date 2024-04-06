using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApi_Application.DataAccess
{
    public class WorldDataBaseContext : IdentityDbContext<IdentityUser>
    {
        public WorldDataBaseContext(DbContextOptions<WorldDataBaseContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            Seed(builder);
        }

        private static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin".ToUpper() },
                new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User".ToUpper() },
                new IdentityRole() { Name = "Guest", ConcurrencyStamp = "3", NormalizedName = "Guest".ToUpper() },
                new IdentityRole() { Name = "Vendor", ConcurrencyStamp = "4", NormalizedName = "Vendor".ToUpper() }
                );
        }
    }
}

