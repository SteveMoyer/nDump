using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using nDump.Logging;
using nDump.Model;
using nDump.SqlServer;
using nDump.Transformation.Files;

namespace nDump.Import
{
    public class SqlDataImporter
    {
        private readonly ILogger _logger;
        private readonly ISqlScriptFileStrategy _sqlScriptFileStrategy;
        private readonly IQueryExecutor _queryExecutor;

        public SqlDataImporter(ILogger logger, IQueryExecutor queryExecutor,
                               ISqlScriptFileStrategy sqlScriptFileStrategy)
        {
            _logger = logger;
            _sqlScriptFileStrategy = sqlScriptFileStrategy;
            _queryExecutor = queryExecutor;
        }

        public void DeleteDataFromAllDestinationTables(List<SqlTableSelect> sqlTableSelects)
        {
            List<SqlTableSelect> tableSelects = sqlTableSelects.ToList();
            tableSelects.Reverse();
            _logger.Log("Deleting table data from target in reverse order:");
            foreach (var table in tableSelects)
            {
                _logger.Log("     " + table.TableName);
                _queryExecutor.ExecuteNonQueryStatement("delete from " + table.TableName);
            }
        }

        public void InsertDataIntoDesinationTables(List<SqlTableSelect> selects)
        {
            _logger.Log("Adding Table data to target:");
            foreach (var table in selects.ToList())
            {
                if (table.DeleteOnly) continue;
                _logger.Log("     " + table.TableName);
                RunAllScriptFilesFor(table);
            }
        }

        private void RunAllScriptFilesFor(SqlTableSelect table)
        {
            IEnumerator<SqlScript> scriptEnumerator = _sqlScriptFileStrategy.GetEnumeratorFor(table.TableName);
            while (scriptEnumerator.MoveNext())
            {
                var script = scriptEnumerator.Current;
                ExecuteScriptIfNotEmpty(script);
            }
        }

        private void ExecuteScriptIfNotEmpty(SqlScript script)
        {
            _logger.Log(script.Name + "\n");
            if (!string.IsNullOrWhiteSpace(script.Script))
                _queryExecutor.ExecuteNonQueryStatement(script.Script);
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