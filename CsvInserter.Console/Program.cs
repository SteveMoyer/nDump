using System.Collections.Generic;
using System.IO;

namespace CsvInserter.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var csvInserterArgParser = new CSVInserterArgParser();
            CsvInserterArgs csvInserterArgs = csvInserterArgParser.Parse(args);
            var path = args[0];
            var outputPath = args[1];
            IList<string> tablesWithoutIdentities = new List<string>(args[2].Split(new[] {','}));
            var files = Directory.GetFiles(path);

            ICsvToSqlInsertConverter csvToSqlInsertConverter = new CsvToSqlInsertConverter(5000);
            var csvTableFactory = new CsvTableFactory(outputPath, tablesWithoutIdentities);
            ICsvProcessor csvFileProcessor = new CsvFileProcessor(files, csvToSqlInsertConverter, csvTableFactory);

            csvFileProcessor.Process();
        }
    }

    internal class CSVInserterArgParser
    {
        public CsvInserterArgs Parse(string[] args)
        {
            int position = 0;
            bool export = false, import = false, transform = false;
            string file = string.Empty;
            while (position < args.Length)
            {
                switch (args[position].ToLower())
                {
                    case "-e":
                        export = true;
                        position++;
                        break;
                    case "-t":
                        transform = true;
                        position++;
                        break;
                    case "-i":
                        import = true;
                        position++;
                        break;
                    case "-f":
                        file = args[position + 1];
                        position += 2;
                        break;
                }
            }
            string csvDirectory = string.Empty;
            string sqlDiretory = string.Empty;
            return new CsvInserterArgs(export, transform, import, file, csvDirectory, sqlDiretory);
        }
    }

    internal class CsvInserterArgs
    {
        private readonly bool _export;
        private readonly bool _transform;
        private readonly bool _import;
        private readonly string _file;
        private readonly string _csvDirectory;
        private readonly string _sqlDiretory;

        public CsvInserterArgs(bool export, bool transform, bool import, string file, string csvDirectory,
                               string sqlDiretory)
        {
            _export = export;
            _transform = transform;
            _import = import;
            _file = file;
            _csvDirectory = csvDirectory;
            _sqlDiretory = sqlDiretory;
        }

        public string SqlDiretory
        {
            get { return _sqlDiretory; }
        }

        public string CsvDirectory
        {
            get { return _csvDirectory; }
        }

        public string File1
        {
            get { return _file; }
        }

        public bool Import
        {
            get { return _import; }
        }

        public bool Transform
        {
            get { return _transform; }
        }

        public bool Export
        {
            get { return _export; }
        }
    }
}