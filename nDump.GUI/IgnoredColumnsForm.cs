using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace nDump.GUI
{
    public partial class IgnoredColumnsForm : Form
    {
       
       
        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        public List<string> IgnoredColumns
        {
            get { return columnsListTextBox.Text.Split('\n').Where(s=> !String.IsNullOrWhiteSpace(s)).ToList(); }
            set { columnsListTextBox.Text = string.Join("\n", value); }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}