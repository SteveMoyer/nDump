using System.Collections.Generic;

namespace nDump
{
    public interface ISelectionFilteringStrategy
    {
        string GetFilteredSelectStatement(SqlTableSelect table);
        void SetupFilterTables(List<SqlTableSelect> filtertableSelects);
        void TearDownFilterTables(IList<SqlTableSelect> filtertableSelects);
    }
}