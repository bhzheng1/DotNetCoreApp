using Microsoft.EntityFrameworkCore;
using Module_WorldDemo.Entities;

namespace Module_WorldDemo.DataAccess;

public partial class WorldDataBaseContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Product> Products { get; set; }
    public WorldDataBaseContext(DbContextOptions<WorldDataBaseContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
