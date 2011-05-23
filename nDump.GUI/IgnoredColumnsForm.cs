using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace nDump.GUI
{
    public partial class IgnoredColumnsForm 
    {
        public IgnoredColumnsForm()
        {
            InitializeComponent();
        }

        private void OkButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        public List<string> IgnoredColumns
        {
            get { return columnsListTextBox.Text.Split('\n').Where(s => !String.IsNullOrWhiteSpace(s)).Select(s=> s.Trim()).ToList(); }
            set { columnsListTextBox.Text = string.Join("\n", value); }
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}