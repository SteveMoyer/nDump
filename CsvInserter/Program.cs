using System.Collections.Generic;
using System.IO;

namespace CsvInserter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var path = args[0];
            var outputPath = args[1];
            IList<string> tablesWithoutIdentities = new List<string>(args[2].Split(new[] {','}));
            var files = Directory.GetFiles(path);
     
            ICvsToSqlInsertConverter cvsToSqlInsertConverter = new CvsToSqlInsertConverter();
            var csvTableFactory = new CsvTableFactory(outputPath,tablesWithoutIdentities);
            ICsvProcessor csvFileProcessor = new CsvFileProcessor(files,cvsToSqlInsertConverter, csvTableFactory);
            
            csvFileProcessor.Process();
           
        }
    }
}