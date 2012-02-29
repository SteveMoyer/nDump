using System.Data;
using System.Data.SqlClient;

namespace nDump.SqlServer
{
    public class QueryExecutor : IQueryExecutor
    {
        private readonly string _connectionString;

        public QueryExecutor(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void ExecuteNonQueryStatement(string selectStatement)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var nonQueryCommand = new SqlCommand(selectStatement, connection);
                nonQueryCommand.CommandType = CommandType.Text;
                connection.Open();
                nonQueryCommand.ExecuteNonQuery();
            }
        }

        public DataTable ExecuteSelectStatement(string selectStatement)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlDataAdapter adap;
                adap = new SqlDataAdapter(selectStatement, connection);
                var dsSelectResult = new DataTable();
                adap.Fill(dsSelectResult);
                return dsSelectResult;
            }
        }

        public void ExecuteBulkInsert(string tableName, DataTable dataTable, bool keepIdentity)
        {
            var copyOptions = SqlBulkCopyOptions.TableLock;
            copyOptions  |= keepIdentity ? SqlBulkCopyOptions.KeepIdentity : SqlBulkCopyOptions.Default;

            using (var bulkCopy = new SqlBulkCopy(_connectionString, copyOptions))
            {
                bulkCopy.DestinationTableName = tableName;

                foreach (DataColumn column in dataTable.Columns)
                {
                    bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                }
               
                bulkCopy.WriteToServer(dataTable);
            }
        }
    }
}