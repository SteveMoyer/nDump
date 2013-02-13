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
using System.Data.SqlClient;
using System.Windows.Forms;

namespace nDump.GUI
{
    public partial class SqlConnectionControl : UserControl
    {
        public SqlConnectionControl()
        {
            InitializeComponent();
        }

        private void TestButtonClick(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqlConnection(connectionTextBox.Text))
                {
                    connection.Open();
                }
            }
            catch (SqlException exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
            MessageBox.Show("Connection Successful");
        }
        public string ConnectionString
        {
            get { return connectionTextBox.Text; }
            set {  connectionTextBox.Text=value; }
        }
        public string ConnectionName
        {
            get { return connectionNameLabel.Text; }
            set { connectionNameLabel.Text = value; }
        }
    }
}
