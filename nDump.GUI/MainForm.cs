using System;
using System.IO;
using System.Windows.Forms;
using nDump.Model;

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

        private void OpenToolStripMenuItemClick(object sender, EventArgs e)
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

        private void SaveToolStripMenuItemClick(object sender, EventArgs e)
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

        private static void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NewToolStripMenuItemClick(object sender, EventArgs e)
        {
            _file = string.Empty;
            LoadDataPlan(new DataPlan());
        }

        private void AddTablesFromDatabaseToolStripMenuItemClick(object sender, EventArgs e)
        {
            var importTablesForm = new ImportTablesForm(_dataPlan.DataSelects);
            if (importTablesForm.ShowDialog() == DialogResult.OK)
            {
                tableTabControl.AddTables(importTablesForm.SelectedItems);
            }
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            LoadDataPlan(new DataPlan());
        }

        private void LoadDataPlan(DataPlan dataPlan)
        {
            _dataPlan = dataPlan;
            tableTabControl.CurrentDataPlan = _dataPlan;
            SetWindowTitle();
        }

        private void SetWindowTitle()
        {
            Text = "nDump - " + _file ==null? "new data plan":_file;
        }

        private void ExportToolStripMenuItemClick(object sender, EventArgs e)
        {
            new ExportForm(_dataPlan).ShowDialog();
        }
    }
}