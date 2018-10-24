using System;
using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ClearMeasure.OnionDevOpsArchitecture.IntegrationTests
{
    public class SqlExecuter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public void ExecuteSql(string commandText, Action<DbDataReader> readerAction)
        {
            var factory = new StubbedDataContextFactory();
            using (var dbConnection = factory.GetContext().Database.GetDbConnection())
            {
                dbConnection.ConnectionString = factory.Config.GetConnectionString();
                dbConnection.Open();
                using (var command = dbConnection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText =
                        commandText;
                    DbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        readerAction(reader);
                    }
                }
                dbConnection.Close();
            }
        }
    }
}