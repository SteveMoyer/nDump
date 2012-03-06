using System.Data;

namespace nDump.SqlServer
{
    public interface IQueryExecutor
    {
        void ExecuteNonQueryStatement(string selectStatement);
        DataTable ExecuteSelectStatement(string selectStatement);
        void ExecuteBulkInsert(string tableName, DataTable dataTable, bool keepIdentity);
    }
}