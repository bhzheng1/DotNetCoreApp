using Microsoft.EntityFrameworkCore;
using Module_WorldDemo.Entities;

namespace Module_WorldDemo.DataAccess;

public partial class ClDataBaseContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Product> Products { get; set; }
    public ClDataBaseContext(DbContextOptions<ClDataBaseContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        Seed(builder);
    }

    private static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product() { Id = 1, Name = "Product 1", Price = 100 },
            new Product() { Id = 2, Name = "Product 2", Price = 200 },
            new Product() { Id = 3, Name = "Product 3", Price = 300 }
        );

        modelBuilder.Entity<Company>().HasData(
            new Company() { Id = 1, Name = "Company 1", Address = "Address 1" },
            new Company() { Id = 2, Name = "Company 2", Address = "Address 2" },
            new Company() { Id = 3, Name = "Company 3", Address = "Address 3" }
        );
    }
}
