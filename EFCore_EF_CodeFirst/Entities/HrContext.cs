using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotNetCore.Entities
{
    public class HrContext:DbContext
    {
        private static HrContext _hrContext;

        public static HrContext Instance
        {
            get
            {
                if (_hrContext == null)
                {
                    _hrContext = new HrContext();
                    var serviceProvider = _hrContext.GetInfrastructure<IServiceProvider>();
                    var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                    loggerFactory.AddProvider(new MyLoggerProvider());
                }

                return _hrContext;
            }
        }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code.
                optionsBuilder.UseMySql("server=localhost;userid=root;pwd=rootpw;port=3306;database=Movies;sslmode=none;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>(entity => { entity.HasKey(e => new {e.UserId,e.RoleId}); });
        }
    }
}
