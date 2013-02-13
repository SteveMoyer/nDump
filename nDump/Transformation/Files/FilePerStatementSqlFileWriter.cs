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
﻿using System.IO;
using System.Text;
using nDump.Logging;

namespace nDump.Transformation.Files
{
    public class FilePerStatementSqlFileWriter:ISqlFileWriter
    {
        private readonly string _directory;
        private readonly string _fileNameWithoutExtension;
        private readonly ILogger _logger;
        private int _counter = 1;

        public FilePerStatementSqlFileWriter(string directory, string fileNameWithoutExtension, ILogger logger)
        {
            _directory = directory;
            _fileNameWithoutExtension = fileNameWithoutExtension;
            _logger = logger;
        }

        public void Write(string sql)
        {
            if (!Directory.Exists(_directory))
            {
                Directory.CreateDirectory(_directory);
                _logger.Log(_directory + " did not exist: creating\n");
            }
            

            var sqlFilePath = _directory + @"\" + _fileNameWithoutExtension + "_" + string.Format("{0:000}", _counter) + ".sql";
            const bool shouldAppend = false;
            var sw = new StreamWriter(sqlFilePath, shouldAppend, Encoding.Unicode);
            _counter++;
            sw.Write(sql);
            sw.Dispose();
        }
    }
}
