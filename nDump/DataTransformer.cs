using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace nDump
{
    public class DataTransformer
    {
        private readonly ConsoleLogger _logger;
        private readonly string _sourceDirectory;
        private readonly string _sqlScriptDirectory;
        private readonly ICsvToSqlInsertConverter _csvToSqlInsertConverter;

        public DataTransformer(string sqlScriptDirectory, string sourceDirectory, ConsoleLogger logger,
                               ICsvToSqlInsertConverter csvToSqlInsertConverter)
        {
            _sqlScriptDirectory = sqlScriptDirectory;
            _sourceDirectory = sourceDirectory;
            _logger = logger;
            _csvToSqlInsertConverter = csvToSqlInsertConverter;
        }

        public DataTransformer(string sqlScriptDirectory, string sourceDirectory, ConsoleLogger logger)
            : this(sqlScriptDirectory, sourceDirectory, logger, new CsvToSqlInsertConverter(5000))
        {
        }

        public void ConvertCsvToSql(List<SqlTableSelect> sqlTableSelects)
        {
            _logger.Log("ConvertingCsvFilesToSql");
            IList<string> tablesWithIdentities =
                sqlTableSelects.Where(@select => @select.HasIdentity).Select(
                    tableSelect => tableSelect.TableName.ToLower()).ToList();
            var files = Directory.GetFiles(_sourceDirectory);

            var csvTableFactory = new CsvTableFactory(_sqlScriptDirectory, tablesWithIdentities);
            ICsvProcessor csvFileProcessor = new CsvFileProcessor(files, _csvToSqlInsertConverter, csvTableFactory);

            csvFileProcessor.Process();
        }
    }
}