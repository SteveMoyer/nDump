using System.Data;
using System.Data.SqlClient;

namespace CsvInserter
{
    public class QueryExecutor
    {
        public void ExecuteNonQueryStatement(string selectStatement, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var nonQueryCommand = new SqlCommand(selectStatement, connection);
                nonQueryCommand.CommandType = CommandType.Text;
                connection.Open();
                nonQueryCommand.ExecuteNonQuery();
            }
        }

        public DataTable ExecuteSelectStatement(string selectStatement, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adap;
                adap = new SqlDataAdapter(selectStatement, connection);
                var dsSelectResult = new DataTable();
                adap.Fill(dsSelectResult);
                return dsSelectResult;
            }
        }
    }
}