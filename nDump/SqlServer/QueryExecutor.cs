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
﻿using System.Data;
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
