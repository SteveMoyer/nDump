using nDump;

namespace nDump.Console
{
    internal class Program
    {
        private const string USAGE = @"Usage:
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
                System.Console.Write(USAGE);
                return;
            }
            var csvInserterArgParser = new nDumpParser();
            nDumpArgs nDumpArgs = csvInserterArgParser.Parse(args);
            DataPlan dataPlan = DataPlan.Load(nDumpArgs.File1);
             
            if (nDumpArgs.Export)
            {
                var exporter = new DataExporter(new ConsoleLogger(), nDumpArgs.CsvDirectory,
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
                var importer = new DataImporter(new ConsoleLogger(),
                                                new QueryExecutor(nDumpArgs.TargetConnectionString),
                                                nDumpArgs.SqlDiretory);
                importer.RemoveDataAndImportFromSqlFiles(dataPlan.DataSelects);
            }   
        }
    }

    internal class nDumpParser
    {
        public nDumpArgs Parse(string[] args)
        {
            int position = 0;
            bool export = false, import = false, transform = false;
            string file = string.Empty;
            string csvDirectory = string.Empty;
            string sqlDirectory = string.Empty;
            string sourceConnection = string.Empty;
            string targetConnection = string.Empty;
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
                    case "-csv":
                        csvDirectory = args[position + 1];
                        position += 2;
                        break;
                    case "-sql":
                        sqlDirectory = args[position + 1];
                        position += 2;
                        break;
                    case "-sourceconnection":
                        sourceConnection= args[position + 1];
                        position += 2;
                        break;
                    case "-targetconnection":
                        targetConnection= args[position + 1];
                        position += 2;
                        break;
                }
            }
            return new nDumpArgs(export, transform, import, file, csvDirectory, sqlDirectory,sourceConnection,targetConnection);
        }
    }

    internal class nDumpArgs
    {
        private readonly bool _export;
        private readonly bool _transform;
        private readonly bool _import;
        private readonly string _file;
        private readonly string _csvDirectory;
        private readonly string _sqlDiretory;
        private readonly string _sourceConnectionString;
        private string _targetConnectionString;

        public nDumpArgs(bool export, bool transform, bool import, string file, string csvDirectory,
                               string sqlDiretory, string sourceConnectionString, string targetConnectionString)
        {
            _export = export;
            _targetConnectionString = targetConnectionString;
            _sourceConnectionString = sourceConnectionString;
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

        public string SourceConnectionString
        {
            get { return _sourceConnectionString; }
        }

        public string TargetConnectionString
        {
            get { return _targetConnectionString; }
        }
    }
}