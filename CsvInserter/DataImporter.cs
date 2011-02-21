using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CsvInserter
{
    public class DataImporter
    {
        private readonly ConsoleLogger _logger;
        private readonly string _sqlScriptDirectory;
        private readonly QueryExecutor _queryExecutor;

        public DataImporter(ConsoleLogger logger, QueryExecutor queryExecutor, string sqlScriptDirectory)
        {
            _logger = logger;
            _sqlScriptDirectory = sqlScriptDirectory;
            _queryExecutor = queryExecutor;
        }

        public void DeleteDataFromAllDestinationTables(List<SqlTableSelect> sqlTableSelects)
        {
            List<SqlTableSelect> tableSelects = sqlTableSelects.ToList();
            tableSelects.Reverse();
            _logger.Log("DeletingTable data from target in reverse order:");
            foreach (var table in tableSelects)
            {
                _logger.Log("     " + table.TableName);
                _queryExecutor.ExecuteNonQueryStatement("delete from " + table.TableName);
            }
        }

        public void InsertDataIntoDesinationTables(List<SqlTableSelect> selects)
        {
            List<SqlTableSelect> tableSelects = selects.ToList();

            _logger.Log("Adding Table data to target:");
            foreach (var table in tableSelects)
            {
                _logger.Log("     " + table.TableName);
                String script = File.OpenText(_sqlScriptDirectory + table.TableName + ".sql").ReadToEnd();
                if (!string.IsNullOrWhiteSpace(script))
                    _queryExecutor.ExecuteNonQueryStatement(script);
            }
        }
        public void RemoveDataAndImportFromSqlFiles(List<SqlTableSelect> selects)
        {
            DeleteDataFromAllDestinationTables(selects);
            InsertDataIntoDesinationTables(selects);
       
        }
    }
}