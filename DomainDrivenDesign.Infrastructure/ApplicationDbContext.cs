using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.Infrastructure;

public class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }
}