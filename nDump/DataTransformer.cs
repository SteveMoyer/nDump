using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace nDump
{
    public class DataTransformer
    {
        private readonly ILogger _logger;
        private readonly string _sourceDirectory;
        private readonly string _sqlScriptDirectory;
        private readonly ICsvToSqlInsertConverter _csvToSqlInsertConverter;

        public DataTransformer(string sqlScriptDirectory, string sourceDirectory, ILogger logger,
                               ICsvToSqlInsertConverter csvToSqlInsertConverter)
        {
            _sqlScriptDirectory = sqlScriptDirectory;
            _sourceDirectory = sourceDirectory;
            _logger = logger;
            _csvToSqlInsertConverter = csvToSqlInsertConverter;
        }

        public DataTransformer(string sqlScriptDirectory, string sourceDirectory, ILogger logger)
            : this(sqlScriptDirectory, sourceDirectory, logger, new CsvToSqlInsertConverter(999))
        {
        }

        public void ConvertCsvToSql(List<SqlTableSelect> sqlTableSelects)
        {
            _logger.Log("ConvertingCsvFilesToSql");
            IList<string> tablesWithIdentities =
                sqlTableSelects.Where(@select => @select.HasIdentity).Select(
                    tableSelect => tableSelect.TableName.ToLower()).ToList();
            var files = Directory.GetFiles(_sourceDirectory);

            var csvTableFactory = new CsvTableFactory(_sqlScriptDirectory, tablesWithIdentities,_logger);
            ICsvProcessor csvFileProcessor = new CsvFileProcessor(files, _csvToSqlInsertConverter, csvTableFactory);

            csvFileProcessor.Process();
        }
    }
}