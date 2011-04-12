using System.Data;

namespace nDump
{
    public interface IQueryExecutor
    {
        void ExecuteNonQueryStatement(string selectStatement);
        DataTable ExecuteSelectStatement(string selectStatement);
    }
}