using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace nDump.GUI
{
    public partial class ImportTablesForm : Form
    {
        private readonly List<SqlTableSelect> _csvTables;

        public ImportTablesForm()
        {
            InitializeComponent();
        }

        public ImportTablesForm(List<SqlTableSelect> csvTables):this()
        {
            _csvTables = csvTables;
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            using (var sqlConnection = new SqlConnection(connectionTextBox.Text))
            {
                
                var sqlCommand = new SqlCommand("select o.name from sys.objects o WHERE o.[type]='U'", sqlConnection);
                sqlConnection.Open();
                var sqlDataReader = sqlCommand.ExecuteReader();
                var tables = new List<string>();
                while (sqlDataReader.Read())
                {
                    tables.Add(sqlDataReader["name"].ToString());
                }
                var unconfiguredTableNames = tables.Where(s => !_csvTables.Any(csvTable => csvTable.TableName.Equals(s))).ToList();

                tableListBox.DataSource = unconfiguredTableNames;
                
            }

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            foreach (var tableName in tableListBox.SelectedItems)
            {
                _csvTables.Add(new SqlTableSelect(tableName.ToString()));

            }
            Close();
        }
    }
}
