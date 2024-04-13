using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApiJWTAuthentication.DataAccess.Entities;
using WebApiJWTAuthentication.DataAccess.ContextConfigurations;

namespace WebApiJWTAuthentication.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().IsMemoryOptimized();
            modelBuilder.Entity<IdentityRole>().IsMemoryOptimized();

            InitalRoles(modelBuilder);

            var ids = new Guid[] { Guid.NewGuid(), Guid.NewGuid() };
            modelBuilder.ApplyConfiguration(new OwnerContextConfiguration(ids));
            modelBuilder.ApplyConfiguration(new AccountContextConfiguration(ids));
        }
        private void InitalRoles(ModelBuilder builder)
        {
            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole() { Id = Guid.NewGuid(), Name = "Admin", ConcurrencyStamp = Guid.NewGuid().ToString(), NormalizedName = "Admin".ToUpper() },
                new ApplicationRole() { Id = Guid.NewGuid(), Name = "User", ConcurrencyStamp = Guid.NewGuid().ToString(), NormalizedName = "User".ToUpper() }
                );

        }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
