using System;
using System.IO;
using System.Windows.Forms;

namespace nDump.GUI
{
    public partial class MainForm : Form
    {
        private DataPlan _dataPlan;
        private string _file;

        public MainForm()
        {
            InitializeComponent();
                   }
//
//        private void AddColumnsToDataGrid(DataGridView dataGridView)
//        {
//            var tableName = new DataGridViewTextBoxColumn();
//            var select = new DataGridViewTextBoxColumn();
//            var hasIdentity = new DataGridViewCheckBoxColumn();
//            var deleteOnly = new DataGridViewCheckBoxColumn();
//            var ignoredColumns = new DataGridViewButtonColumn();
//            tableName.DataPropertyName = "TableName";
//            tableName.HeaderText = "Table Name";
//            tableName.Name = "TableName";
//            select.DataPropertyName = "Select";
//            select.HeaderText = "Filtering Select";
//            select.Name = "Select";
//            hasIdentity.DataPropertyName = "HasIdentity";
//            hasIdentity.HeaderText = "Has Identity";
//            hasIdentity.Name = "HasIdentity";
//            hasIdentity.Resizable = DataGridViewTriState.True;
//            hasIdentity.SortMode = DataGridViewColumnSortMode.Automatic;
//            deleteOnly.DataPropertyName = "DeleteOnly";
//            deleteOnly.HeaderText = "Delete Only";
//            deleteOnly.Name = "DeleteOnly";
//            deleteOnly.Resizable = DataGridViewTriState.True;
//            deleteOnly.SortMode = DataGridViewColumnSortMode.Automatic;
//            ignoredColumns.HeaderText = "Ignored Columns";
//            ignoredColumns.Name = "IgnoredColumns";
//            ignoredColumns.Text = "Ignored Columns";
//            dataGridView.Columns.AddRange(new DataGridViewColumn[]
//                                              {
//                                                  tableName,
//                                                  select,
//                                                  hasIdentity,
//                                                  deleteOnly,
//                                                  ignoredColumns
//                                              });
//        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dataPlanOpenFileDialog = new OpenFileDialog
                                             {
                                                 Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*",
                                                 FilterIndex = 1,
                                                 Multiselect = false,
                                                 RestoreDirectory = true
                                             };

            if (dataPlanOpenFileDialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                if (File.Exists(dataPlanOpenFileDialog.FileName))
                {
                    _file = dataPlanOpenFileDialog.FileName;
                    Text = _file;
                    LoadDataPlan(DataPlan.Load(dataPlanOpenFileDialog.FileName));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dataPlanSaveFileDialog = new SaveFileDialog
                                             {
                                                 Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*",
                                                 FilterIndex = 1,
                                                 RestoreDirectory = true
                                             };

            if (String.IsNullOrEmpty(_file))
            {
                if (dataPlanSaveFileDialog.ShowDialog() == DialogResult.Cancel ||
                    string.IsNullOrWhiteSpace(dataPlanSaveFileDialog.FileName))
                {
                    return;
                }
                _file = dataPlanSaveFileDialog.FileName;
            }
            _dataPlan.Save(_file);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _file = string.Empty;
            LoadDataPlan(_dataPlan);
        }

        private void addTablesFromDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ImportTablesForm(_dataPlan.DataSelects).ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadDataPlan(new DataPlan());
        }

        private void LoadDataPlan(DataPlan dataPlan)
        {
            _dataPlan = dataPlan;
            tableTabControl.CurrentDataPlan = _dataPlan;
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ExportForm(_dataPlan).ShowDialog();
        }

      
    }
}