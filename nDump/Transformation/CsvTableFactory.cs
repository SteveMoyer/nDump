using System.Collections.Generic;
using System.IO;
using LumenWorks.Framework.IO.Csv;
using nDump.Logging;
using nDump.Model;

namespace nDump.Transformation
{
    public class CsvTableFactory : ICsvTableFactory
    {
        private readonly string _outputPath;
        private readonly IList<string> _tablesWithIdentities;
        private readonly ILogger _logger;

        public CsvTableFactory(string outputPath, IList<string> tablesWithIdentities, ILogger logger)
        {
            _outputPath = outputPath;
            _tablesWithIdentities = tablesWithIdentities;
            _logger = logger;
        }

        public ICsvTable CreateCsvTable(string file)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
            var filePerStatementSqlFileWriter = new FilePerStatementSqlFileWriter(_outputPath, fileNameWithoutExtension,_logger);
            var reader = new CsvReader(File.OpenText(file), true, ',', '\"', '\"', '#',
                                       ValueTrimmingOptions.UnquotedOnly);
            return new CsvTable(reader, filePerStatementSqlFileWriter, fileNameWithoutExtension,
                                _tablesWithIdentities.Contains(fileNameWithoutExtension.ToLower()));
        }
    }
}