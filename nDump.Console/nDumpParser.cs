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

namespace nDump.Console
{
// ReSharper disable InconsistentNaming
    internal class nDumpParser
// ReSharper restore InconsistentNaming
    {
        public nDumpOptions Parse(string[] args)
        {
            var position = 0;
            bool export = false, import = false, transform = false, bulkImport = false, bulkDelete = false, insert = false;
            var file = string.Empty;
            var csvDirectory = string.Empty;
            var sqlDirectory = string.Empty;
            var sourceConnection = string.Empty;
            var targetConnection = string.Empty;
            var applyFilters = true;
            var options = string.Empty;
            while (position < args.Length)
            {
                switch (args[position].ToLower())
                {
                    case "-?":
                    case "/?":
                    case "-h":
                    case "/h":
                        throw new nDumpConfigurationException();
                    case "-e":
                        export = true;
                        position++;
                        break;
                    case "-t":
                        transform = true;
                        bulkImport = false;
                        position++;
                        break;
                    case "-bd":
                        bulkDelete = true;
                        position++;
                        break;
                    case "-in":
                        insert = true;
                        if (import || bulkImport)
                        {
                            throw new nDumpConfigurationException("Cannot use insert and bulk import or import at same time");
                        }
                        position++;
                        break;
                    case "-bi":
                        bulkImport = true;
                        if (import || insert)
                        {
                            throw new nDumpConfigurationException("Cannot use bulk import and insert or import at same time");
                        }
                        position++;
                        break;
                    case "-i":
                        import = true;
                        if (bulkImport || insert)
                        {
                            throw new nDumpConfigurationException("Cannot use import and bulk import or insert at same time");
                        }
                        position++;
                        break;
                    case "-f":
                    case "-dp":
                        file = args[position + 1];
                        position += 2;
                        break;
                    case "-o":
                        options = args[position + 1];
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
                        sourceConnection = args[position + 1];
                        position += 2;
                        break;
                    case "-targetconnection":
                        targetConnection = args[position + 1];
                        position += 2;
                        break;
                    case "-nofilter":
                        applyFilters = false;
                        position += 1;
                        break;
                    default:
                        throw new nDumpConfigurationException("Invalid argument supplied, see usage below.\n");
                }
            }
            if (!string.IsNullOrEmpty(options))
            {
                var ndumpOptions = nDumpOptions.Load(options);
                ndumpOptions.File = file;
                return ndumpOptions;
            }
            return new nDumpOptions(export, transform, import, file, csvDirectory, sqlDirectory, sourceConnection,
                                    targetConnection, applyFilters, bulkImport, bulkDelete, insert);
        }
    }
}
