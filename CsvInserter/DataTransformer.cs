using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CsvInserter
{
    public class DataTransformer
    {
        private readonly ConsoleLogger _logger;
        private readonly string _sourceDirectory;
        private readonly string _sqlScriptDirectory;

        public DataTransformer(string sqlScriptDirectory, string sourceDirectory, ConsoleLogger logger)
        {
            _logger = logger;
            _sourceDirectory = sourceDirectory;
            _sqlScriptDirectory = sqlScriptDirectory;
        }

        public void ConvertCsvToSql(List<SqlTableSelect> sqlTableSelects)
        {
            _logger.Log("ConvertingCsvFilesToSql");
            var path = _sourceDirectory;
            var outputPath = _sqlScriptDirectory;
            IList<string> tablesWithIdentities =
                sqlTableSelects.Where(@select => @select.HasIdentity).Select(
                    tableSelect => tableSelect.TableName.ToLower()).ToList();
            var files = Directory.GetFiles(path);

            ICsvToSqlInsertConverter csvToSqlInsertConverter = new CsvToSqlInsertConverter(5000, new CsvTokenJoiner());
            var csvTableFactory = new CsvTableFactory(outputPath, tablesWithIdentities);
            ICsvProcessor csvFileProcessor = new CsvFileProcessor(files, csvToSqlInsertConverter, csvTableFactory);

            csvFileProcessor.Process();
        }
    }
}