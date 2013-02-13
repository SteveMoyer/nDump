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
using System.Linq;
using nDump.Logging;
using nDump.Model;

namespace nDump.Transformation
{
    public class DataTransformer
    {
        private readonly ILogger _logger;
        private readonly char _delimiter;
        private readonly string _sourceDirectory;
        private readonly string _sqlScriptDirectory;
        private readonly ICsvToSqlInsertConverter _csvToSqlInsertConverter;

        public DataTransformer(string sqlScriptDirectory, string sourceDirectory, ILogger logger, char delimiter,
                               ICsvToSqlInsertConverter csvToSqlInsertConverter)
        {
            _sqlScriptDirectory = sqlScriptDirectory;
            _sourceDirectory = sourceDirectory;
            _logger = logger;
            _delimiter = delimiter;
            _csvToSqlInsertConverter = csvToSqlInsertConverter;
        }

        public DataTransformer(string sqlScriptDirectory, string sourceDirectory, ILogger logger, char delimiter)
            : this(sqlScriptDirectory, sourceDirectory, logger, delimiter, new CsvToSqlInsertConverter(999))
        {
        }

        public void ConvertCsvToSql(List<SqlTableSelect> sqlTableSelects)
        {
            _logger.Log("ConvertingCsvFilesToSql");
            IList<string> tablesWithIdentities= 
                sqlTableSelects.Where(@select => @select.HasIdentity).Select(
                    tableSelect => tableSelect.TableName.ToLower()).ToList();
            var files = Directory.GetFiles(_sourceDirectory);

            var csvTableFactory = new CsvTableFactory(_sqlScriptDirectory, tablesWithIdentities,_delimiter, _logger);
            ICsvProcessor csvFileProcessor = new CsvFileProcessor(files, _csvToSqlInsertConverter, csvTableFactory);

            csvFileProcessor.Process();
        }
    }
}
