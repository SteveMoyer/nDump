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
            nDumpOptions nDumpOptions = csvInserterArgParser.Parse(args);
            DataPlan dataPlan = DataPlan.Load(nDumpOptions.File1);

            var consoleLogger = new ConsoleLogger();
            
            return new ConsoleExecutor().ExecuteDataPlan(nDumpOptions, consoleLogger, dataPlan);
        }

      
    }
}