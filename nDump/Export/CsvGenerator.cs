using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using FileHelpers;
using nDump.Logging;
using nDump.Model;
using nDump.SqlServer;
using nDump.Transformation.Files;

namespace nDump.Export
{
    public class CsvGenerator
    {
        private readonly ILogger _logger;
        private readonly ISelectionFilteringStrategy _selectionFilteringStrategy;
        private readonly IQueryExecutor _queryExecutor;
        private readonly string _destinationDirectory;
        private readonly char _delimiter;

        public CsvGenerator(ILogger logger, ISelectionFilteringStrategy selectionFilteringStrategy, IQueryExecutor queryExecutor, string destinationDirectory, char delimiter)
        {
            _logger = logger;
            _destinationDirectory = destinationDirectory;            
            _queryExecutor = queryExecutor;
            _selectionFilteringStrategy = selectionFilteringStrategy;
            _delimiter = delimiter;
        }

        public void Generate(List<SqlTableSelect> selects)
        {
            CreateDestinationDirectoryIfNecessary();
            _logger.Log("Generating Csv:");

            foreach (var table in selects)
            {
                if (table.DeleteOnly) continue;
                GenerateCsv(table);
            }
        }

        private void CreateDestinationDirectoryIfNecessary()
        {
            if (!Directory.Exists(_destinationDirectory))
            {
                Directory.CreateDirectory(_destinationDirectory);
                _logger.Log(_destinationDirectory + " did not exist: creating\n");
            }
        }

        private const string DontCare = "Do Not Care";

        private void GenerateCsv(SqlTableSelect table)
        {
            _logger.Log("\t" + table.TableName);
            var select = _selectionFilteringStrategy.GetFilteredSelectStatement(table);
            var results =
                _queryExecutor.ExecuteSelectStatement(select);
            foreach (var column in table.ExcludedColumns)
            {
                results.Columns.Remove(column);
            }

            var csvOptions = new CsvOptions(DontCare, _delimiter, results.Columns.Count) {DateFormat = "g", Encoding = Encoding.UTF8};

            var filename = _destinationDirectory + table.TableName.ToLower() + ".csv";
            try
            {
                CsvEngine.DataTableToCsv(results, filename, csvOptions);                
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new nDumpApplicationException(
                    "nDump cannot access the CSV file at " + filename +
                    ". Is it checked out (TFS) or modifiable? This error may also occur if the file has been opened by another program.",
                    ex);
            }
        }
    }
}