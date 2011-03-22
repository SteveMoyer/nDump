using System.Collections.Generic;
using System.IO;
using nDump;

namespace nDump
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var path = args[0];
            var outputPath = args[1];
            IList<string> tablesWithoutIdentities = new List<string>(args[2].Split(new[] {','}));
            var files = Directory.GetFiles(path);

            int numberOfRowsPerInsert = 999;
            ICsvToSqlInsertConverter csvToSqlInsertConverter = new CsvToSqlInsertConverter(numberOfRowsPerInsert);
            var csvTableFactory = new CsvTableFactory(outputPath,tablesWithoutIdentities);
            ICsvProcessor csvFileProcessor = new CsvFileProcessor(files,csvToSqlInsertConverter, csvTableFactory);
            
            csvFileProcessor.Process();
           
        }
    }
}