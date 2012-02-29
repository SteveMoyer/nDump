using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using LumenWorks.Framework.IO.Csv;
using nDump.Logging;
using nDump.Model;
using nDump.SqlServer;

namespace nDump.Import
{
    internal class CsvDataImporter
    {
        private readonly ILogger _logger;
        private readonly QueryExecutor _queryExecutor;
        private readonly string _csvDirectory;
        private readonly char _delimiter;

        public CsvDataImporter(ILogger logger, QueryExecutor queryExecutor, string csvDirectory, char delimiter)
        {
            _logger = logger;
            _queryExecutor = queryExecutor;
            _csvDirectory = csvDirectory;
            _delimiter = delimiter;
        }

        public void RemoveDataAndImportFromSqlFiles(List<SqlTableSelect> dataSelects)
        {
            ThrowExceptionIfInvalidDataPlan(dataSelects);
            DeleteDataFromAllDestinationTables(dataSelects);
            InsertDataIntoDestinationTables(dataSelects);
        }

        private void ThrowExceptionIfInvalidDataPlan(List<SqlTableSelect> tables)
        {
            var missingTables = new List<string>();
            foreach (var table in tables)
            {
                var csvFile = Path.Combine(_csvDirectory, table.TableName + ".csv");
                if (!File.Exists(csvFile))
                    missingTables.Add(table.TableName);
            }

            if (missingTables.Count == 0) return;

            var errorMessage =
                string.Format(
                    "The following tables have entries in the dataplan, but the corresponding CSVs are not present in {0}:\n{1}\n" +
                    "Either remove the entries from the dataplan xml, or add the missing CSVs.\n",
                    _csvDirectory, string.Join("\n", missingTables));
            throw new Exception(errorMessage);
        }

        public void DeleteDataFromAllDestinationTables(List<SqlTableSelect> sqlTableSelects)
        {
            List<SqlTableSelect> tableSelects = sqlTableSelects.ToList();
            tableSelects.Reverse();
            _logger.Log("Deleting table data from target in reverse order:");
            foreach (var table in tableSelects)
            {
                _logger.Log("\t" + table.TableName);
                _queryExecutor.ExecuteNonQueryStatement("delete from " + table.TableName);
            }
        }

        public void InsertDataIntoDestinationTables(List<SqlTableSelect> selects)
        {
            _logger.Log("Adding Table data to target:");
            foreach (var table in selects.ToList())
            {
                if (table.DeleteOnly) continue;
                _logger.Log("\t" + table.TableName);
                var csvFile = Path.Combine(_csvDirectory, table.TableName + ".csv");
                var csvReader = new CsvReader(File.OpenText(csvFile), true, _delimiter, '\"', '\"', '#',
                                       ValueTrimmingOptions.UnquotedOnly);
                var dataTable = new DataTable();
                dataTable.Load(csvReader);

                _queryExecutor.ExecuteBulkInsert(table.TableName, dataTable, table.HasIdentity);
                _logger.Log("\t\tInserted " + dataTable.Rows.Count + " rows.");

            }
        }
    }
}