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
