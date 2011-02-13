using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CsvInserter
{
    public class DataImporter
    {
        private readonly ConsoleLogger _logger;
        private string _serverName;
        private string sqlScriptDirectory;
        private readonly QueryExecutor _queryExecutor;

        public DataImporter(ConsoleLogger logger, string serverName, QueryExecutor queryExecutor,
                            string sqlScriptDirectory)
        {
            _logger = logger;
            this.sqlScriptDirectory = sqlScriptDirectory;
            _queryExecutor = queryExecutor;
            _serverName = serverName;
        }

        public void DeleteDataFromAllDestinationTables(List<SqlTableSelect> sqlTableSelects, string databaseName)
        {
            List<SqlTableSelect> tableSelects = sqlTableSelects.ToList();
            tableSelects.Reverse();
            _logger.Log("DeletingTable data from target in reverse order:");
            foreach (var table in tableSelects)
            {
                _logger.Log("     " + table.TableName);
                _queryExecutor.ExecuteNonQueryStatement("delete from " + table.TableName,
                                                        "server=" + _serverName +
                                                        ";Integrated Security=SSPI;Initial Catalog=" +
                                                        databaseName);
            }
        }

        public void InsertDataIntoDesinationTables(List<SqlTableSelect> selects, string databaseName)
        {
            List<SqlTableSelect> tableSelects = selects.ToList();

            _logger.Log("Adding Table data to target:");
            foreach (var table in tableSelects)
            {
                _logger.Log("     " + table.TableName);
                String script = File.OpenText(sqlScriptDirectory + table.TableName + ".sql").ReadToEnd();
                _queryExecutor.ExecuteNonQueryStatement(script,
                                                        "server=" + _serverName +
                                                        ";Integrated Security=SSPI;Initial Catalog=" +
                                                        databaseName);
            }
        }
    }
}