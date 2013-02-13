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
﻿using System;
using System.Collections.Generic;
using nDump.Logging;
using nDump.Model;
using nDump.SqlServer;

namespace nDump.Export
{
    public class UseFilterIfPresentStrategy : ISelectionFilteringStrategy
    {
        private const string DropTableSqlFormat = "drop table {0}";
        private readonly IQueryExecutor _queryExecutor;
        private readonly ILogger _logger;

        public UseFilterIfPresentStrategy(IQueryExecutor queryExecutor, ILogger logger)
        {   
            _queryExecutor = queryExecutor;
            _logger = logger;
        }

        public string GetFilteredSelectStatement(SqlTableSelect table)
        {
            return !String.IsNullOrWhiteSpace(table.Select ) ? table.Select : "select * from " + table.TableName;
        }

        public void SetupFilterTables(List<SqlTableSelect> filtertableSelects)
        {
            _logger.Log("Setting Up:");
            foreach (var table in filtertableSelects)
            {
                _logger.Log("     " + table.TableName);
                _queryExecutor.ExecuteNonQueryStatement(table.Select);
            }
        }

        public void TearDownFilterTables(IList<SqlTableSelect> filtertableSelects)
        {
            bool fail = false;
            string failedSteps = string.Empty;
            _logger.Log("Tearing Down:");
            foreach (var table in filtertableSelects)
            {
                _logger.Log("     " + table.TableName);
                try
                {
                    _queryExecutor.ExecuteNonQueryStatement(String.Format(DropTableSqlFormat, table.TableName));
                }
                catch (Exception)
                {
                    _logger.Log("         Failed " + table.TableName);
                    fail = true;
                    failedSteps += " " + table.TableName;
                }
            }
            if (fail)
                throw new TearDownException("one or more teardown steps failed:" + failedSteps);
        }
    }
}
