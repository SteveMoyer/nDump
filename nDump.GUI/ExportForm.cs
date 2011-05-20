using System;
using System.IO;
using System.Windows.Forms;

namespace nDump.GUI
{
    public partial class ExportForm : Form
    {
        private readonly DataPlan _dataPlan;

        public ExportForm(DataPlan dataPlan)
        {
            _dataPlan = dataPlan;
            InitializeComponent();
        }

        private void RunButtonClick(object sender, System.EventArgs e)
        {
            LogTextBox.Text = string.Empty;
            var planResult = new ConsoleExecutor().ExecuteDataPlan(CreateOptionsFromForm(),new TextAreaLogger(LogTextBox),_dataPlan);
            MessageBox.Show(planResult == 0 ? "Completed Action Successfully." : "Action failed, read log for details.");
        }

        private nDumpOptions CreateOptionsFromForm()
        {
            return new nDumpOptions(ExportCheckBox.Checked,TransformCheckBox.Checked,ImportCheckBox.Checked,null,csvDirTextBox.Text,SqlScriptDirTextBox.Text,exportsqlConnection.ConnectionString,ImportSqlConnection.ConnectionString,ApplyFilterCheckBox.Checked);
        }

        private void LoadOptionsButtonClick(object sender, EventArgs e)
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
                    
                    LoadOptions(nDumpOptions.Load(dataPlanOpenFileDialog.FileName));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }

        private void LoadOptions(nDumpOptions options)
        {
            SqlScriptDirTextBox.Text = options.SqlDirectory;
            csvDirTextBox.Text = options.CsvDirectory;
            ApplyFilterCheckBox.Checked = options.ApplyFilters;
            ExportCheckBox.Checked = options.Export;
            ImportCheckBox.Checked = options.Import;
            exportsqlConnection.ConnectionString = options.SourceConnectionString;
            ImportSqlConnection.ConnectionString = options.TargetConnectionString;
            TransformCheckBox.Checked = options.Transform;

        }

        private void SaveOptionsButtonClick(object sender, EventArgs e)
        {
            string file = null;
            var dataPlanSaveFileDialog = new SaveFileDialog
                                             {
                                                 Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*",
                                                 FilterIndex = 1,
                                                 RestoreDirectory = true
                                             };


            if (dataPlanSaveFileDialog.ShowDialog() == DialogResult.Cancel ||
                string.IsNullOrWhiteSpace(dataPlanSaveFileDialog.FileName))
            {
                return;
            }
            file = dataPlanSaveFileDialog.FileName;
            CreateOptionsFromForm().Save(file);
        }


    }

    internal class TextAreaLogger : ILogger
    {
        private readonly TextBox _logTextBox;

        public TextAreaLogger(TextBox logTextBox)
        {
            _logTextBox = logTextBox;
        }

        public void Log(string message)
        {
            _logTextBox.AppendText(message + Environment.NewLine);
        }
    }
}