using System.Collections.Generic;
using System.IO;
using LumenWorks.Framework.IO.Csv;

namespace nDump
{
    public class CsvTableFactory : ICsvTableFactory
    {
        private readonly string _outputPath;
        private readonly IList<string> _tablesWithIdentities;

        public CsvTableFactory(string outputPath, IList<string> tablesWithIdentities)
        {
            _outputPath = outputPath;
            _tablesWithIdentities = tablesWithIdentities;
        }

        public ICsvTable CreateCsvTable(string file)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
            var filePerStatementSqlFileWriter = new FilePerStatementSqlFileWriter(_outputPath, fileNameWithoutExtension);
            var reader = new CsvReader(File.OpenText(file), true, ',', '\"', '\"', '#',
                                       ValueTrimmingOptions.UnquotedOnly);
            return new CsvTable(reader, filePerStatementSqlFileWriter, fileNameWithoutExtension,
                                _tablesWithIdentities.Contains(fileNameWithoutExtension.ToLower()));
        }
    }
}