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
Sample:
    nDump.exe -f dataPlan.xml -sourceconnection ""server=.;Integrated Security=SSPI;Initial Catalog=mydb"" -csv .\csv\  -sql .\sql\ -targetconnection ""server=.;Integrated Security=SSPI;Initial Catalog=emptymydb"" -e -t -i
";

        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                System.Console.Write(Usage);
                return;
            }
            var csvInserterArgParser = new nDumpParser();
            nDumpArgs nDumpArgs = csvInserterArgParser.Parse(args);
            DataPlan dataPlan = DataPlan.Load(nDumpArgs.File1);

            if (nDumpArgs.Export)
            {
                var exporter = new SqlDataExporter(new ConsoleLogger(), nDumpArgs.CsvDirectory,
                                                   new QueryExecutor(nDumpArgs.SourceConnectionString));
                exporter.ExportToCsv(dataPlan.SetupScripts, dataPlan.DataSelects);
            }
            if (nDumpArgs.Transform)
            {
                var transformer = new DataTransformer(nDumpArgs.SqlDiretory, nDumpArgs.CsvDirectory,
                                                      new ConsoleLogger());
                transformer.ConvertCsvToSql(dataPlan.DataSelects);
            }
            if (nDumpArgs.Import)
            {
                var importer = new SqlDataImporter(new ConsoleLogger(),
                                                   new QueryExecutor(nDumpArgs.TargetConnectionString),
                                                   nDumpArgs.SqlDiretory);
                importer.RemoveDataAndImportFromSqlFiles(dataPlan.DataSelects);
            }
        }
    }
}