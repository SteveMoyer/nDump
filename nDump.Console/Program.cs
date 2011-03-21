using System;

namespace nDump.Console
{
    internal class Program
    {
        private const string Usage =
            @"Usage:
    -e      Export (requires -sourceconnection and -csv)
    -i      Import (requires -sql and -targetconnection)
    -t      transform (requires -csv and -sql)
    -f      data plan file (required)
    -csv    csv file directory
    -sql    sql file directory
    -sourceconnection   source database connection string
    -targetconnection   target database connection string
    -nofilter           export all data from the source database without filtering
Sample:
    nDump.exe -f dataPlan.xml -sourceconnection ""server=.;Integrated Security=SSPI;Initial Catalog=mydb"" -csv .\csv\  -sql .\sql\ -targetconnection ""server=.;Integrated Security=SSPI;Initial Catalog=emptymydb"" -e -t -i
";

        private static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                System.Console.Write(Usage);
                return -1;
            }
            var csvInserterArgParser = new nDumpParser();
            nDumpArgs nDumpArgs = csvInserterArgParser.Parse(args);
            DataPlan dataPlan = DataPlan.Load(nDumpArgs.File1);

            var consoleLogger = new ConsoleLogger();
            if (nDumpArgs.Export)
            {
                var queryExecutor = new QueryExecutor(nDumpArgs.SourceConnectionString);
                ISelectionFilteringStrategy filteringStrategy = nDumpArgs.ApplyFilters ?(ISelectionFilteringStrategy)
                    new UseFilterIfPresentStrategy(queryExecutor,consoleLogger)
                    :new IgnoreFilterStrategy();
                var exporter = new SqlDataExporter(consoleLogger, nDumpArgs.CsvDirectory,
                                                   queryExecutor,filteringStrategy);
                try
                {
                    exporter.ExportToCsv(dataPlan.SetupScripts, dataPlan.DataSelects);
 
                }
                catch (nDumpApplicationException ex)
                {

                    consoleLogger.Log("Export To Csv Failed.\n"+ ex.StackTrace);
                    return -1;
                    
                }
            }
            if (nDumpArgs.Transform)
            {
                var transformer = new DataTransformer(nDumpArgs.SqlDiretory, nDumpArgs.CsvDirectory,
                                                      consoleLogger);
                try
                {

                    transformer.ConvertCsvToSql(dataPlan.DataSelects);
                    
                }
                catch (nDumpApplicationException ex)
                {

                    consoleLogger.Log("Export To Csv Failed.\n" + ex.StackTrace);
                    return -1;

                }
            }
            if (nDumpArgs.Import)
            {
                var importer = new SqlDataImporter(consoleLogger,
                                                   new QueryExecutor(nDumpArgs.TargetConnectionString),
                                                   nDumpArgs.SqlDiretory);
                try
                {
                    importer.RemoveDataAndImportFromSqlFiles(dataPlan.DataSelects);
                }
                catch (nDumpApplicationException ex)
                {

                    consoleLogger.Log("Import Of Sql Failed.\n" + ex.StackTrace);
                    return -1;

                }
                
            }
            return 0;
        }
    }
}