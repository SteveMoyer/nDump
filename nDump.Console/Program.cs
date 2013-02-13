/* Copyright 2010-2013 Steve Moyer
 * This file is part of nDump.
 * 
 * nDump is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * nDump is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with nDump.  If not, see <http://www.gnu.org/licenses/>.
*/
﻿using nDump.Configuration;
using nDump.Logging;
using nDump.Model;
using nDump.Workflow;

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
    -i      Import (requires -sql and -targetconnection). Cannot use import and bulk import or insert at same time.
    -in     Insert (requires -sql and -targetconnection). Cannot use bulk import and insert or import at same time.
    -bi     Bulk Import (requires -sql and -targetconnection). Cannot use bulk import and insert or import at same time.
    -bd     Bulk Delete (requires -sql and -targetconnection)
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
            var nDumpArgParser = new nDumpParser();
            nDumpOptions nDumpOptions;
            try
            {
                nDumpOptions = nDumpArgParser.Parse(args);
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
