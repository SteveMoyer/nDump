using System.Collections.Generic;
using System.IO;
using System.Linq;
using nDump.Logging;
using nDump.Model;

namespace nDump.Transformation
{
    public class DataTransformer
    {
        private readonly ILogger _logger;
        private readonly char _delimiter;
        private readonly string _sourceDirectory;
        private readonly string _sqlScriptDirectory;
        private readonly ICsvToSqlInsertConverter _csvToSqlInsertConverter;

        public DataTransformer(string sqlScriptDirectory, string sourceDirectory, ILogger logger, char delimiter,
                               ICsvToSqlInsertConverter csvToSqlInsertConverter)
        {
            _sqlScriptDirectory = sqlScriptDirectory;
            _sourceDirectory = sourceDirectory;
            _logger = logger;
            _delimiter = delimiter;
            _csvToSqlInsertConverter = csvToSqlInsertConverter;
        }

        public DataTransformer(string sqlScriptDirectory, string sourceDirectory, ILogger logger, char delimiter)
            : this(sqlScriptDirectory, sourceDirectory, logger, delimiter, new CsvToSqlInsertConverter(999))
        {
        }

        public void ConvertCsvToSql(List<SqlTableSelect> sqlTableSelects)
        {
            _logger.Log("ConvertingCsvFilesToSql");
            IList<string> tablesWithIdentities= 
                sqlTableSelects.Where(@select => @select.HasIdentity).Select(
                    tableSelect => tableSelect.TableName.ToLower()).ToList();
            var files = Directory.GetFiles(_sourceDirectory);

            var csvTableFactory = new CsvTableFactory(_sqlScriptDirectory, tablesWithIdentities,_delimiter, _logger);
            ICsvProcessor csvFileProcessor = new CsvFileProcessor(files, _csvToSqlInsertConverter, csvTableFactory);

            csvFileProcessor.Process();
        }
    }
}