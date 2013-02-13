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
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using nDump.Model;

namespace nDump.GUI
{
    public partial class ImportTablesForm : Form
    {
        private readonly List<SqlTableSelect> _csvTables;
        private List<string> _selectedItems;

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
                var unconfiguredTableNames = tables.Where(s => !_csvTables.Any(csvTable => s.Equals(csvTable.TableName))).ToList();

                tableListBox.DataSource = unconfiguredTableNames;
                
            }

        }
        public IList<string> SelectedItems
        {
            get { return _selectedItems; }
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            var selected = new List<string>();
                        foreach (var tableName in tableListBox.SelectedItems)
            {
                selected.Add(tableName.ToString());

            }
            _selectedItems = selected;
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
