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
using System.Linq;
using nDump.Logging;
using nDump.Model;
using nDump.Transformation.Escaping;

namespace nDump.Transformation {
    public class CsvToSqlBulkInsertConverter : ICsvToSqlInsertConverter {
        private readonly ILogger logger;
        private readonly IEscapingStrategy _headerEscapingStrategy;

        private const string INSERT_HEADER_FORMAT = "BULK INSERT {0} FROM '{1}' WITH " +
                                                  "(FIELDTERMINATOR = ',', ROWTERMINATOR = '\\n', FIRSTROW=2, ORDER ( {2} ))";

        public CsvToSqlBulkInsertConverter(IEscapingStrategy headerEscapingStrategy) {
            _headerEscapingStrategy = headerEscapingStrategy;
        }

        public CsvToSqlBulkInsertConverter(ILogger logger)
            : this(new ColumnHeaderKeywordEscapingStrategy()){
            this.logger = logger;
        }

        public void Convert(ICsvTable csvTable, string file){
            GenerateInserts(csvTable, file);
        }

        private void GenerateInserts(ICsvTable csvTable, string file){
            string columnNames = null;

            using (var fp = File.OpenRead(file)){
                var data = new StreamReader(fp).ReadLine();
                columnNames = (data ?? string.Empty)
                    .Split(',')
                    .Select(x => string.Format("[{0}]", x))
                    .Aggregate((x, y) => string.Format("{0},{1}", x, y));
            }

            csvTable.Write(string.Format(INSERT_HEADER_FORMAT, _headerEscapingStrategy.Escape(csvTable.Name), file, columnNames));
        }
    }
}
