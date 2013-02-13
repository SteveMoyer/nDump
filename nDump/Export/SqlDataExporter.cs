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
using nDump.Logging;
using nDump.Model;

namespace nDump.Export
{
    public class SqlDataExporter
    {
        
        private readonly ILogger _logger;
        
        private readonly ISelectionFilteringStrategy _selectionFilteringStrategy;
        private readonly CsvGenerator _csvGenerator;

        public SqlDataExporter(ILogger logger, 
                               ISelectionFilteringStrategy selectionFilteringStrategy, CsvGenerator csvGenerator)
        {
            _logger = logger;
            _csvGenerator = csvGenerator;
            _selectionFilteringStrategy = selectionFilteringStrategy;
            
        }

        
        public void ExportToCsv(List<SqlTableSelect> setupScripts, List<SqlTableSelect> selects)
        {
            try
            {
                _selectionFilteringStrategy.TearDownFilterTables(setupScripts);
            }
            catch (TearDownException)
            {
            }
            _selectionFilteringStrategy.SetupFilterTables(setupScripts);
            
            _csvGenerator.Generate(selects);
            _selectionFilteringStrategy.TearDownFilterTables(setupScripts);
        }


    }
}
