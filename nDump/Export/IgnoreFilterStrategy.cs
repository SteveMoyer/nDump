using System.Collections.Generic;
using nDump.Model;

namespace nDump.Export
{
    public class IgnoreFilterStrategy : ISelectionFilteringStrategy
    {
        public string GetFilteredSelectStatement(SqlTableSelect table)
        {
            return "select * from " + table.TableName;
        }

        public void SetupFilterTables(List<SqlTableSelect> filtertableSelects)
        {
            return;
        }

        public void TearDownFilterTables(IList<SqlTableSelect> filtertableSelects)
        {
            return;
        }
    }
}