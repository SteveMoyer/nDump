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

    internal class CsvTableFactory : ICsvTableFactory
    {
        private readonly string _outputPath;
        private readonly IList<String> _tablesWithoutIdentities;

        public CsvTableFactory(string outputPath, IList<string> tablesWithoutIdentities)
        {
            _outputPath = outputPath;
            _tablesWithoutIdentities = tablesWithoutIdentities;
        }

        public ICsvTable CreateCsvTable(string file)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
            var writer = new StreamWriter(File.OpenWrite(_outputPath + @"\" + fileNameWithoutExtension + ".sql"));
            var reader = new CsvReader(File.OpenText(file), true, ',', '\'', '\'', '#',
                                       ValueTrimmingOptions.UnquotedOnly);
            return new CsvTable(reader, writer, fileNameWithoutExtension,
                                _tablesWithoutIdentities.Contains(fileNameWithoutExtension));
        }
    }
}