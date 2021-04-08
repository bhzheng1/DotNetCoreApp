using Microsoft.EntityFrameworkCore;
using System.Data;
using One.DAL.Entities;
using One.DAL.Entities.Configurations;

namespace One.DAL
{
    public class OracleDbContext : DbContext
    {
        private readonly IDbConnection _dbConnection;

        public OracleDbContext(DbContextOptions options) : base(options)
        {
        }

        public OracleDbContext(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_dbConnection != null)
                optionsBuilder.UseSqlServer(_dbConnection.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<JobHistory> JobHistories { get; set; }

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
