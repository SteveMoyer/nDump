using System.Collections.Generic;
using System.IO;

namespace CsvInserter.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
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
}