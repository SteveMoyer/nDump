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
        private readonly string _bulkInsertDestinationDirectory;
        private const char Delimiter = '\t';
        private const string Off = "off";
        private const string On = "on";
        
        public CsvGenerator(ILogger logger, ISelectionFilteringStrategy selectionFilteringStrategy,
                            IQueryExecutor queryExecutor, string destinationDirectory)
        {
            _logger = logger;
            _destinationDirectory = destinationDirectory;
            _bulkInsertDestinationDirectory = string.Format("{0}/bulkInsert", this._destinationDirectory);
            _queryExecutor = queryExecutor;
            _selectionFilteringStrategy = selectionFilteringStrategy;
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
            _logger.Log("     " + table.TableName);
            var select = _selectionFilteringStrategy.GetFilteredSelectStatement(table);
            DataTable results =
                _queryExecutor.ExecuteSelectStatement(select);
            foreach (var column in table.ExcludedColumns)
            {
                results.Columns.Remove(column);
            }

            var csvOptions = new CsvOptions(DontCare, Delimiter, results.Columns.Count) {DateFormat = "g"};

            var filename = _destinationDirectory + table.TableName.ToLower() + ".csv";
            try
            {
                CsvEngine.DataTableToCsv(results, filename, csvOptions);
                GenerateBulkInsert(table, results, filename);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new nDumpApplicationException(
                    "nDump cannot access the CSV file at " + filename +
                    ". Is it checked out (TFS) or modifiable? This error may also occur if the file has been opened by another program.",
                    ex);
            }
        }

        private void GenerateBulkInsert(SqlTableSelect selectedTable, DataTable results, string filename)
        {
            var castSelectQuery = new List<string>();
            var selectQuery = new List<string>();
            var selectQueryForView = new List<string>();

            var tableName = selectedTable.TableName;

            foreach (DataColumn dCol in results.Columns)
            {
                castSelectQuery.Add(dCol.DataType.Equals(typeof(Boolean))
                                        ? string.Format("CAST ([{0}] AS bit) AS [{0}]", dCol.ColumnName)
                                        : string.Format("[{0}]", dCol.ColumnName));
                selectQueryForView.Add(dCol.DataType.Equals(typeof(Boolean))
                                        ? string.Format("CAST ([{0}] AS varchar) AS [{0}]", dCol.ColumnName)
                                        : string.Format("[{0}]", dCol.ColumnName));
                selectQuery.Add(string.Format("[{0}]", dCol.ColumnName));
            }

            var castColumnQuery = string.Join(", ", castSelectQuery);
            var columnQuery = string.Join(", ", selectQuery);
            var columnQueryForView = string.Join(", ", selectQueryForView);
            string cleanTableName = tableName.Replace(".", "_").Replace("[", "").Replace("]", "");

            var sb = new StringBuilder();

            CreateTempTable(sb, tableName, cleanTableName, columnQueryForView);

            SetIdentityInsert(selectedTable, sb, On);

            InsertToTable(tableName, sb, cleanTableName, columnQuery, castColumnQuery);

            SetIdentityInsert(selectedTable, sb, Off);

            DeleteTempTable(sb, cleanTableName);

            var fileWriter = new FilePerStatementSqlFileWriter(_bulkInsertDestinationDirectory, tableName, _logger);
            fileWriter.Write(sb.ToString());
        }
        private static void CreateTempTable(StringBuilder sb, string tableName, string cleanTableName, string columnQueryForView)
        {

            //            sb.AppendFormat(
            //                @"IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[vw_{0}]') AND type in (N'V')) 
            //                            BEGIN 
            //                                DROP VIEW dbo.vw_{0} 
            //                            END
            //                       GO"
            //                , cleanTableName).AppendLine();

            //            sb.AppendFormat(
            //                @"CREATE VIEW dbo.vw_{0}
            //                  AS 
            //                    SELECT {1}
            //	                FROM {2}
            //                  GO",
            //                cleanTableName, columnQueryForView, tableName).AppendLine();

            sb.AppendFormat(
                @"IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{0}_csv]') AND type in (N'U'))
                  BEGIN
	                DROP TABLE {0}_csv
                  END
                GO",
                   cleanTableName).AppendLine();

            sb.AppendFormat(
                @"SELECT {0} INTO dbo.{1}_csv FROM {2} Where 1=2
                  GO",
                     columnQueryForView,
                     cleanTableName,
                     tableName
                     ).AppendLine();
        }
        private static void InsertToTable(string tableName, StringBuilder sb, string cleanTableName, string columnQuery, string castColumnQuery)
        {
            sb.AppendFormat(
                @"BULK INSERT dbo.{0}_csv
	              FROM '$CSVPATH${1}.csv' 
	              WITH
	              (
		            FIRSTROW = 2, 
		            FIELDTERMINATOR = '{2}',
		            ROWTERMINATOR = '\n',
		            DATAFILETYPE ='widechar',
                    KEEPIDENTITY 
	              )
                  GO", cleanTableName, tableName.ToLower(), Delimiter).AppendLine();

            sb.AppendFormat(
                @"INSERT into {0} ({1})
                  SELECT {2} FROM dbo.{3}_csv
                  GO", tableName, columnQuery, castColumnQuery, cleanTableName).AppendLine();
        }

        private static void DeleteTempTable(StringBuilder sb, string cleanTableName)
        {
            //            sb.AppendFormat(
            //                @"DROP VIEW dbo.vw_{0} 
            //                  GO", cleanTableName).AppendLine();

            sb.AppendFormat(
                @"DROP TABLE dbo.{0}_csv 
                  GO", cleanTableName).AppendLine();
        }

        private string GenerateCsv(string tableName, DataTable results)
        {
            var csvOptions = new CsvOptions(DontCare, Delimiter, results.Columns.Count)
            {
                DateFormat = "g",
                Encoding = Encoding.Unicode
            };
            var filename = _destinationDirectory + tableName.ToLower() + ".csv";
            CsvEngine.DataTableToCsv(results, filename, csvOptions);
            return filename;
        }
        private const string SetIdentityInsertFormatString = "set identity_insert {0} {1}";

        private static void SetIdentityInsert(SqlTableSelect selectedTable, StringBuilder stringBuilder, String switcher)
        {
            if (selectedTable.HasIdentity)
            {
                stringBuilder.AppendFormat(SetIdentityInsertFormatString, selectedTable.TableName, switcher).AppendLine().AppendLine("GO ");
            }
        }
    }
}