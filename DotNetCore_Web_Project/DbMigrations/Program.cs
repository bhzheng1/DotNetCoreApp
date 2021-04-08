// #define MyTest
using DbUp;
using DbUp.Engine;
using DbUp.Helpers;
using DbUp.Support;
using System;
using System.Linq;
using System.Reflection;

namespace DbMigrations
{
    class Program
    {

        public static void Main(string[] args)
        {
            // Uncomment the following line to run against sql local db instance.
            var instanceName = @"localhost";
            var dbName = @"Contoso";

            var connectionString =
                $"Data Source={instanceName},1434;Initial Catalog={dbName};User=sa;Password=Pa$$word;Integrated Security=False;Pooling=False";

            //DropDatabase.For.SqlDatabase(connectionString);

            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgradeEngineBuilder = DeployChanges.To
                .SqlDatabase(connectionString, null) //null or "" for default schema for user
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), script =>
                {
                    if (script.EndsWith("-Transactions.sql"))
                        return !args.Any(a => "--noError".Equals(a, StringComparison.InvariantCultureIgnoreCase));

                    return script.StartsWith("SampleApplication.Scripts.");
                })
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), script => script.StartsWith("SampleApplication.RunAlways."), new SqlScriptOptions { ScriptType = ScriptType.RunAlways, RunGroupOrder = DbUpDefaults.DefaultRunGroupOrder + 1 })
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .JournalToSqlTable("dbo", "_SchemaVersions")
                .LogToConsole();

            if (args.Any(a => "--withTransaction".Equals(a, StringComparison.InvariantCultureIgnoreCase)))
            {
                upgradeEngineBuilder = upgradeEngineBuilder.WithTransaction();
            }
            else if (args.Any(a => "--withTransactionPerScript".Equals(a, StringComparison.InvariantCultureIgnoreCase)))
            {
                upgradeEngineBuilder = upgradeEngineBuilder.WithTransactionPerScript();
            }

            var upGrader = upgradeEngineBuilder.Build();

            Console.WriteLine("Is upgrade required: " + upGrader.IsUpgradeRequired());

            if (args.Any(a => "--generateReport".Equals(a, StringComparison.InvariantCultureIgnoreCase)))
            {
                upGrader.GenerateUpgradeHtmlReport("UpgradeReport.html");
            }
            else
            {
                var result = upGrader.PerformUpgrade();

                if (!result.Successful)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(result.Error);
                    Console.ResetColor();
#if DEBUG
                    Console.ReadLine();
#endif
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success!");
            }

            Console.WriteLine();
            Console.WriteLine(
                "Try the --withTransaction or --withTransactionPerScript to see transaction support in action");
            Console.WriteLine("--noError to exclude the broken script");
            Console.ReadKey();
        }
    }
}
