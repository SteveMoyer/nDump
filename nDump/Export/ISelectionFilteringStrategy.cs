using System.Collections.Generic;
using nDump.Model;

namespace nDump.Export
{
    public interface ISelectionFilteringStrategy
    {
        string GetFilteredSelectStatement(SqlTableSelect table);
        void SetupFilterTables(List<SqlTableSelect> filtertableSelects);
        void TearDownFilterTables(IList<SqlTableSelect> filtertableSelects);
    }
}