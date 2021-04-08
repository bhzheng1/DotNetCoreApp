using Microsoft.EntityFrameworkCore;
using WebApplication_API.WorldModels;

//not necessary
namespace WebApplication_API.DbContexts
{
    public interface IWorldDbContextMySQL
    {
        DbSet<City> City { get; set; }
        DbSet<Country> Country { get; set; }
    }
}