using System;
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