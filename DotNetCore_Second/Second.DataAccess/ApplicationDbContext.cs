using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Second.DataAccess.Entities;
using Second.DataAccess.Entities.Configurations;

namespace Second.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public ApplicationDbContext(string conn):base()
        {

        }
        private static DbContextOptions GetDbContextOptions(string connectionString) {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<JobHistory> JobHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.LogTo(
                System.Console.WriteLine,
                LogLevel.Debug);
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region Generated Configuration
            modelBuilder.ApplyConfiguration(new CountriesConfig());
            modelBuilder.ApplyConfiguration(new DepartmentConfig());
            modelBuilder.ApplyConfiguration(new EmployeeConfig());
            modelBuilder.ApplyConfiguration(new JobHistoryConfig());
            modelBuilder.ApplyConfiguration(new JobsConfig());
            modelBuilder.ApplyConfiguration(new LocationsConfig());
            modelBuilder.ApplyConfiguration(new RegionsConfig());
            #endregion
        }
    }
}
