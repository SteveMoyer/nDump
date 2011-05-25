using System.Data;

namespace nDump.SqlServer
{
    public interface IQueryExecutor
    {
        void ExecuteNonQueryStatement(string selectStatement);
        DataTable ExecuteSelectStatement(string selectStatement);
    }
}