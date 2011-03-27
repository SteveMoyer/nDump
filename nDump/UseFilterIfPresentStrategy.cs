using System;
using System.Collections.Generic;

namespace nDump
{
    public class UseFilterIfPresentStrategy : ISelectionFilteringStrategy
    {
        private readonly QueryExecutor _queryExecutor;
        private readonly ILogger _logger;

        public UseFilterIfPresentStrategy(QueryExecutor queryExecutor, ILogger logger)
        {
            _queryExecutor = queryExecutor;
            _logger = logger;
        }

        public string GetFilteredSelectStatement(SqlTableSelect table)
        {
            return table.Select != string.Empty ? table.Select : "select * from " + table.TableName;
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
                    _queryExecutor.ExecuteNonQueryStatement("drop table " + table.TableName);
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