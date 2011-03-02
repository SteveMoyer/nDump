using System;
using System.Collections.Generic;
using System.Data;
using FileHelpers;

namespace nDump
{
    public class DataExporter
    {
        private readonly ConsoleLogger _logger;
        private readonly string _destinationDirectory;
        private readonly QueryExecutor _queryExecutor;

        public DataExporter(ConsoleLogger logger, string destinationDirectory, QueryExecutor queryExecutor)
        {
            _logger = logger;
            _queryExecutor = queryExecutor;
            _destinationDirectory = destinationDirectory;
        }

        public void TearDownFilterTables(IList<SqlTableSelect> filtertableSelects)
        {
            bool fail = false;
            string failedSteps = string.Empty;
            _logger.Log("Tearing Down:");
            foreach (var table in filtertableSelects)
            {
                _logger.Log("     " + table.TableName);
                try
                {
                    _queryExecutor.ExecuteNonQueryStatement("drop table " + table.TableName);
                }
                catch (Exception)
                {
                    _logger.Log("         Failed " + table.TableName);
                    fail = true;
                    failedSteps += " " + table.TableName;
                }
            }
            if (fail)
                throw new TearDownException("one or more teardown steps failed:" + failedSteps);
        }

        public void SetupFilterTables(List<SqlTableSelect> filtertableSelects)
        {
            _logger.Log("Setting Up:");
            foreach (var table in filtertableSelects)
            {
                _logger.Log("     " + table.TableName);
                _queryExecutor.ExecuteNonQueryStatement(table.Select);
            }
        }

        public void ExportToCsv(List<SqlTableSelect> setupScripts, List<SqlTableSelect> selects)
        {
            try
            {
                TearDownFilterTables(setupScripts);
            }
            catch (TearDownException)
            {
            }
            SetupFilterTables(setupScripts);
            GenerateCsvs(selects);
            TearDownFilterTables(setupScripts);
        }

        public void GenerateCsvs(List<SqlTableSelect> selects)
        {
            _logger.Log("Generating Csv:");

            foreach (var table in selects)
            {
                _logger.Log("     " + table.TableName);
                var select = table.Select != string.Empty ? table.Select : "select * from " + table.TableName;
                DataTable results =
                    _queryExecutor.ExecuteSelectStatement(select);
                foreach (var column in table.ExcludedColumns)
                {
                    results.Columns.Remove(column);
                }

                var csvOptions = new CsvOptions("blah", ',', results.Columns.Count) {DateFormat = "g"};
                CsvEngine.DataTableToCsv(results, _destinationDirectory + table.TableName.ToLower() + ".csv", csvOptions);
            }
        }
    }
}