namespace nDump.Console
{
    internal class Program
    {
        private const string Usage =
            @"Usage:
    -h | /h | -? | /? display this help 
  xml file options:
    -f | -dp     data plan file (required)
    -o    supply and options file to populate
  run options:  
    -e      Export (requires -sourceconnection and -csv)
    -i      Import (requires -sql and -targetconnection)
    -t      transform (requires -csv and -sql)
    -csv    csv file directory
    -sql    sql file directory
    -sourceconnection   source database connection string
    -targetconnection   target database connection string
    -nofilter           export all data from the source database without filtering
Inline Options Sample:
    nDump.exe -dp dataPlan.xml -sourceconnection ""server=.;Integrated Security=SSPI;Initial Catalog=mydb"" -csv .\csv\  -sql .\sql\ -targetconnection ""server=.;Integrated Security=SSPI;Initial Catalog=emptymydb"" -e -t -i
Options File Sample:
    nDump.exe -dp dataPlan.xml -o myOptions.xml ";

        private const int Fail = -1;

        private static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                PrintUsage();
                return Fail;
            }
            var csvInserterArgParser = new nDumpParser();
            nDumpOptions nDumpOptions;
            try
            {
                nDumpOptions = csvInserterArgParser.Parse(args);
            }
            catch (nDumpConfigurationException ex)
            {
                System.Console.WriteLine(ex.Message);
                PrintUsage();
                return Fail;
            }
            var dataPlan = DataPlan.Load(nDumpOptions.File);

            var consoleLogger = new ConsoleLogger();
            
            return new ConsoleExecutor().ExecuteDataPlan(nDumpOptions, consoleLogger, dataPlan);
        }

        private static void PrintUsage()
        {
            System.Console.Write(Usage);
        }
    }
}