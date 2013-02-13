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
﻿using System.Collections.Generic;
using System.IO;
using LumenWorks.Framework.IO.Csv;
using nDump.Logging;
using nDump.Model;
using nDump.Transformation.Files;

namespace nDump.Transformation
{
    public class CsvTableFactory : ICsvTableFactory
    {
        private readonly string _outputPath;
        private readonly IList<string> _tablesWithIdentities;
        private readonly char _delimiter;
        private readonly ILogger _logger;

        public CsvTableFactory(string outputPath, IList<string> tablesWithIdentities, char delimiter, ILogger logger)
        {
            _outputPath = outputPath;
            _tablesWithIdentities = tablesWithIdentities;
            _delimiter = delimiter;
            _logger = logger;
        }

        public ICsvTable CreateCsvTable(string file)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
            var filePerStatementSqlFileWriter = new FilePerStatementSqlFileWriter(_outputPath, fileNameWithoutExtension, _logger);
            var reader = new CsvReader(File.OpenText(file), true, _delimiter, '\"', '\"', '#',
                                       ValueTrimmingOptions.UnquotedOnly);
            return new CsvTable(reader, filePerStatementSqlFileWriter, fileNameWithoutExtension,
                                _tablesWithIdentities.Contains(fileNameWithoutExtension.ToLower()));
        }
    }
}
