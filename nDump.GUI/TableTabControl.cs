using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace nDump.GUI
{
    public partial class TableTabControl : UserControl
    {
        public TableTabControl()
        {
            InitializeComponent();
        }
        public DataPlan CurrentDataPlan
        {
            set { SelectTableGrid.SelectList = value.DataSelects;
                SetupTableGrid.SelectList = value.SetupScripts;
            }

        }
    }
}
