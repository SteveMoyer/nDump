using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace nDump
{
    public class SqlDataImporter
    {
        private const string SqlFileNameFormat = "{0}{1}_{2:000}.sql";
        private readonly ILogger _logger;
        private readonly string _sqlScriptDirectory;
        private readonly QueryExecutor _queryExecutor;

        public SqlDataImporter(ILogger logger, QueryExecutor queryExecutor, string sqlScriptDirectory)
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
                if (table.DeleteOnly) continue;
                _logger.Log("     " + table.TableName);
                RunAllScriptFilesFor(table);
            }
        }

        private void RunAllScriptFilesFor(SqlTableSelect table)
        {
            int i = 1;
            string path = String.Format(SqlFileNameFormat, _sqlScriptDirectory, table.TableName, i);
            while (File.Exists(path))
            {
                String script = File.OpenText(path).ReadToEnd();
                if (!string.IsNullOrWhiteSpace(script))
                    _queryExecutor.ExecuteNonQueryStatement(script);
                i++;
                path = String.Format(SqlFileNameFormat, _sqlScriptDirectory, table.TableName, i);
            }
        }

        public void RemoveDataAndImportFromSqlFiles(List<SqlTableSelect> selects)
        {
            try
            {
                DeleteDataFromAllDestinationTables(selects);
                InsertDataIntoDesinationTables(selects);
            }
            catch (SqlException ex)
            {
                throw new nDumpApplicationException(ex.Message, ex);
            }
        }
    }
}