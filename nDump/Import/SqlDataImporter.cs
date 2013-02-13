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
using nDump.SqlServer;
using nDump.Transformation.Files;

namespace nDump.Import
{
    public class SqlDataImporter
    {
        private readonly ILogger _logger;
        private readonly ISqlScriptFileStrategy _sqlScriptFileStrategy;
        private readonly IQueryExecutor _queryExecutor;

        public SqlDataImporter(ILogger logger, IQueryExecutor queryExecutor,
                               ISqlScriptFileStrategy sqlScriptFileStrategy)
        {
            _logger = logger;
            _sqlScriptFileStrategy = sqlScriptFileStrategy;
            _queryExecutor = queryExecutor;
        }

        public void DeleteDataFromAllDestinationTables(List<SqlTableSelect> sqlTableSelects)
        {
            List<SqlTableSelect> tableSelects = sqlTableSelects.ToList();
            tableSelects.Reverse();
            _logger.Log("Deleting table data from target in reverse order:");
            foreach (var table in tableSelects)
            {
                _logger.Log("\t" + table.TableName);
                _queryExecutor.ExecuteNonQueryStatement("delete from " + table.TableName);
            }
        }

        public void InsertDataIntoDesinationTables(List<SqlTableSelect> selects)
        {
            _logger.Log("Adding Table data to target:");
            foreach (var table in selects.ToList())
            {
                if (table.DeleteOnly) continue;
                _logger.Log("\t" + table.TableName);
                RunAllScriptFilesFor(table);
            }
        }

        private void RunAllScriptFilesFor(SqlTableSelect table)
        {
            IEnumerator<SqlScript> scriptEnumerator = _sqlScriptFileStrategy.GetEnumeratorFor(table.TableName);
            while (scriptEnumerator.MoveNext())
            {
                var script = scriptEnumerator.Current;
                ExecuteScriptIfNotEmpty(script);
            }
        }

        private void ExecuteScriptIfNotEmpty(SqlScript script)
        {
            _logger.Log(script.Name + "\n");
            if (!string.IsNullOrWhiteSpace(script.Script))
                _queryExecutor.ExecuteNonQueryStatement(script.Script);
        }

        public void RemoveDataAndImportFromSqlFiles(List<SqlTableSelect> selects)
        {
            DeleteDataFromAllDestinationTables(selects);
            InsertDataIntoDesinationTables(selects);
        }
    }
}
