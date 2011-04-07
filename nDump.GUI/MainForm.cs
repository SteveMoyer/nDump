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
                    _dataPlan = DataPlan.Load(dataPlanOpenFileDialog.FileName);
                    this.Text = _file;
                    dataPlanPropertyGrid.SelectedObject = _dataPlan;
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
                dataPlanSaveFileDialog.ShowDialog();
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
            _dataPlan=new DataPlan();
        }
    }
}