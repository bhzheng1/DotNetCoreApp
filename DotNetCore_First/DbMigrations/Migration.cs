using System.Data;
using System.Data.SqlClient;
using DbUp;
using DbUp.Builder;
using DbUp.Engine;

namespace DbMigrations
{
    public static class Migration
    {
        public static void CreateIfNeeded(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var connection2 = new SqlConnection(connectionString + ";Initial Catalog=master;"))
            using (var cmd = connection2.CreateCommand())
            {
                connection2.Open();

                cmd.CommandText = $@"
IF DB_ID (N'{connection.Database}') IS NULL
CREATE DATABASE {connection.Database}
";
                cmd.ExecuteNonQuery();
            }
        }

        public static DatabaseUpgradeResult Migrate(string connectionString)
        {
            var upgrader =
                AddCommon(DeployChanges.To.SqlDatabase(connectionString)).Build();

            return upgrader.PerformUpgrade();
        }

        public static DatabaseUpgradeResult Migrate(IDbConnection connection) => Migrate((SqlConnection)connection);

        public static DatabaseUpgradeResult Migrate(SqlConnection connection)
        {
            var upgrader =
                AddCommon(DeployChanges.To.SqlDatabase(new SqlConnectionManager(connection))).Build();

            return upgrader.PerformUpgrade();
        }

        private static UpgradeEngineBuilder AddCommon(UpgradeEngineBuilder builder) =>
            builder
                .WithScriptsEmbeddedInAssembly(typeof(Migration).Assembly)
                .JournalToSqlTable("dbo", "_SchemaVersions")
                .LogToConsole();
    }
}
