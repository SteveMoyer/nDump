using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using FileHelpers;
using nDump.Logging;
using nDump.Model;
using nDump.SqlServer;

namespace nDump.Export
{
    public class SqlDataExporter
    {
        private const string DontCare = "blah";
        private readonly ILogger _logger;
        private readonly string _destinationDirectory;
        private readonly IQueryExecutor _queryExecutor;
        private readonly ISelectionFilteringStrategy _selectionFilteringStrategy;

        public SqlDataExporter(ILogger logger, string destinationDirectory, IQueryExecutor queryExecutor,
                               ISelectionFilteringStrategy selectionFilteringStrategy)
        {
            _logger = logger;
            _queryExecutor = queryExecutor;
            _selectionFilteringStrategy = selectionFilteringStrategy;
            _destinationDirectory = destinationDirectory;
        }

        
        public void ExportToCsv(List<SqlTableSelect> setupScripts, List<SqlTableSelect> selects)
        {
            try
            {
                _selectionFilteringStrategy.TearDownFilterTables(setupScripts);
            }
            catch (TearDownException)
            {
            }
            _selectionFilteringStrategy.SetupFilterTables(setupScripts);
            GenerateCsvs(selects);
            _selectionFilteringStrategy.TearDownFilterTables(setupScripts);
        }

        public void GenerateCsvs(List<SqlTableSelect> selects)
        {
            _logger.Log("Generating Csv:");
            if (!Directory.Exists(_destinationDirectory))
            {
                Directory.CreateDirectory(_destinationDirectory);
                _logger.Log(_destinationDirectory +" did not exist: creating\n");
            }
            
            foreach (var table in selects)
            {
                if(table.DeleteOnly) continue;
                GenerateCsv(table);
            }
        }

        private void GenerateCsv(SqlTableSelect table)
        {
            _logger.Log("     " + table.TableName);
            var select = _selectionFilteringStrategy.GetFilteredSelectStatement(table);
            DataTable results =
                _queryExecutor.ExecuteSelectStatement(select);
            foreach (var column in table.ExcludedColumns)
            {
                results.Columns.Remove(column);
            }

            var csvOptions = new CsvOptions(DontCare, ',', results.Columns.Count) {DateFormat = "g"};

            var filename = _destinationDirectory + table.TableName.ToLower() + ".csv";
            try
            {
                CsvEngine.DataTableToCsv(results, filename, csvOptions);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new nDumpApplicationException("nDump cannot access the CSV file at "+filename+". Is it checked out (TFS) or modifiable? This error may also occur if the file has been opened by another program.",ex);
            }
        }
    }
}