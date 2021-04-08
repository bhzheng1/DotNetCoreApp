using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using One.DAL;
using TestSupport.EfHelpers;
using TestSupport.EfSchemeCompare;
using TestSupport.Helpers;
using Xunit;
using Xunit.Abstractions;
using Xunit.Extensions.AssertExtensions;

namespace EfCore.Test
{
    public class CompareOracle
    {
        private readonly ITestOutputHelper _output;
        private readonly DbContextOptions<OracleDbContext> _options;
        private readonly string _connectionString;

        public CompareOracle(ITestOutputHelper output)
        {
            _output = output;
            _options=this.CreateUniqueClassOptions<OracleDbContext>();
            using (var context = new OracleDbContext(_options))
            {
                _connectionString = context.Database.GetDbConnection().ConnectionString;
                context.Database.EnsureCreated();
            }
        }

        [Fact]
        public void CompareDatabaseViaConnectionName()
        {
            const string connectionStringName = "OracleDb";
            var connectionString = AppSettings.GetConfiguration().GetConnectionString(connectionStringName);
            var optionsBuilder = new DbContextOptionsBuilder<OracleDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            var options = optionsBuilder.Options;

            using (var context = new OracleDbContext(options))
            {
                var comparer = new CompareEfSql();

                bool hasErrors = comparer.CompareEfWithDb
                    (connectionStringName, context); 

                //VERIFY
                hasErrors.ShouldBeFalse(comparer.GetAllErrors);
            }
        }
    }
}
