using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApi_Application.DataAccess
{
    public class AppDataBaseContext : IdentityDbContext<IdentityUser>
    {
        public AppDataBaseContext(DbContextOptions<AppDataBaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

