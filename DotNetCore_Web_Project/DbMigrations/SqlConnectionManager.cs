using System.Collections.Generic;
using System.Data.SqlClient;
using DbUp.Engine.Transactions;
using DbUp.Support;

namespace DbMigrations
{
    public class SqlConnectionManager : DatabaseConnectionManager
    {
        public SqlConnectionManager(SqlConnection connection) : base(new DelegateConnectionFactory((log, dbManager) =>
        {
            if (dbManager.IsScriptOutputLogged)
                connection.InfoMessage += (sender, e) => log.WriteInformation("{0}\r\n", (object)e.Message);
            return connection;
        }))
        {
        }

        public override IEnumerable<string> SplitScriptIntoCommands(string scriptContents) => new SqlCommandSplitter().SplitScriptIntoCommands(scriptContents);
    }
}
