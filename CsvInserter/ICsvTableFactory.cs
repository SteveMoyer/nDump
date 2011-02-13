using System;
using System.Collections.Generic;
using System.IO;
using LumenWorks.Framework.IO.Csv;

namespace CsvInserter
{
    public interface ICsvTableFactory
    {
        ICsvTable CreateCsvTable(string file);
    }

    public class CsvTableFactory : ICsvTableFactory
    {
        private readonly string _outputPath;
        private readonly IList<String> _tablesWithIdentities;

        public CsvTableFactory(string outputPath, IList<string> tablesWithIdentities)
        {
            _outputPath = outputPath;
            _tablesWithIdentities = tablesWithIdentities;
        }

        public ICsvTable CreateCsvTable(string file)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
            var writer = new StreamWriter(_outputPath + @"\" + fileNameWithoutExtension + ".sql", false);
            var reader = new CsvReader(File.OpenText(file), true, ',', '\"', '\"', '#',
                                       ValueTrimmingOptions.UnquotedOnly);
            return new CsvTable(reader, writer, fileNameWithoutExtension,
                                _tablesWithIdentities.Contains(fileNameWithoutExtension.ToLower()));
        }
    }
}