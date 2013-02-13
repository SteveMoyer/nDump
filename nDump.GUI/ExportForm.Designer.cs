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
﻿namespace nDump.GUI
{
    partial class ExportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SqlScriptDirTextBox = new System.Windows.Forms.TextBox();
            this.csvDirTextBox = new System.Windows.Forms.TextBox();
            this.ExportCheckBox = new System.Windows.Forms.CheckBox();
            this.ImportCheckBox = new System.Windows.Forms.CheckBox();
            this.TransformCheckBox = new System.Windows.Forms.CheckBox();
            this.ApplyFilterCheckBox = new System.Windows.Forms.CheckBox();
            this.ImportSqlConnection = new nDump.GUI.SqlConnectionControl();
            this.exportsqlConnection = new nDump.GUI.SqlConnectionControl();
            this.LogTextBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.SqlScriptDirTextBox);
            this.panel1.Controls.Add(this.csvDirTextBox);
            this.panel1.Controls.Add(this.ExportCheckBox);
            this.panel1.Controls.Add(this.ImportCheckBox);
            this.panel1.Controls.Add(this.TransformCheckBox);
            this.panel1.Controls.Add(this.ApplyFilterCheckBox);
            this.panel1.Controls.Add(this.ImportSqlConnection);
            this.panel1.Controls.Add(this.exportsqlConnection);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(748, 183);
            this.panel1.TabIndex = 1;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 154);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(127, 23);
            this.button3.TabIndex = 14;
            this.button3.Text = "Load Options";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.LoadOptionsButtonClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(156, 154);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(127, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Save Options";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.SaveOptionsButtonClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(296, 154);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Run Selected Options!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.RunButtonClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(215, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Sql Script Dir";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(215, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "CSV Dir";
            // 
            // SqlScriptDirTextBox
            // 
            this.SqlScriptDirTextBox.Location = new System.Drawing.Point(296, 128);
            this.SqlScriptDirTextBox.Name = "SqlScriptDirTextBox";
            this.SqlScriptDirTextBox.Size = new System.Drawing.Size(381, 20);
            this.SqlScriptDirTextBox.TabIndex = 9;
            this.SqlScriptDirTextBox.Text = ".\\sql\\";
            // 
            // csvDirTextBox
            // 
            this.csvDirTextBox.Location = new System.Drawing.Point(296, 102);
            this.csvDirTextBox.Name = "csvDirTextBox";
            this.csvDirTextBox.Size = new System.Drawing.Size(381, 20);
            this.csvDirTextBox.TabIndex = 8;
            this.csvDirTextBox.Text = ".\\csv\\";
            // 
            // ExportCheckBox
            // 
            this.ExportCheckBox.AutoSize = true;
            this.ExportCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ExportCheckBox.Location = new System.Drawing.Point(33, 131);
            this.ExportCheckBox.Name = "ExportCheckBox";
            this.ExportCheckBox.Size = new System.Drawing.Size(56, 17);
            this.ExportCheckBox.TabIndex = 7;
            this.ExportCheckBox.Text = "Export";
            this.ExportCheckBox.UseVisualStyleBackColor = true;
            // 
            // ImportCheckBox
            // 
            this.ImportCheckBox.AutoSize = true;
            this.ImportCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ImportCheckBox.Location = new System.Drawing.Point(131, 130);
            this.ImportCheckBox.Name = "ImportCheckBox";
            this.ImportCheckBox.Size = new System.Drawing.Size(55, 17);
            this.ImportCheckBox.TabIndex = 6;
            this.ImportCheckBox.Text = "Import";
            this.ImportCheckBox.UseVisualStyleBackColor = true;
            // 
            // TransformCheckBox
            // 
            this.TransformCheckBox.AutoSize = true;
            this.TransformCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TransformCheckBox.Location = new System.Drawing.Point(113, 102);
            this.TransformCheckBox.Name = "TransformCheckBox";
            this.TransformCheckBox.Size = new System.Drawing.Size(73, 17);
            this.TransformCheckBox.TabIndex = 5;
            this.TransformCheckBox.Text = "Transform";
            this.TransformCheckBox.UseVisualStyleBackColor = true;
            // 
            // ApplyFilterCheckBox
            // 
            this.ApplyFilterCheckBox.AutoSize = true;
            this.ApplyFilterCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ApplyFilterCheckBox.Checked = true;
            this.ApplyFilterCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ApplyFilterCheckBox.Location = new System.Drawing.Point(3, 102);
            this.ApplyFilterCheckBox.Name = "ApplyFilterCheckBox";
            this.ApplyFilterCheckBox.Size = new System.Drawing.Size(82, 17);
            this.ApplyFilterCheckBox.TabIndex = 2;
            this.ApplyFilterCheckBox.Text = "Apply Filters";
            this.ApplyFilterCheckBox.UseVisualStyleBackColor = true;
            // 
            // ImportSqlConnection
            // 
            this.ImportSqlConnection.ConnectionName = "Import Connection";
            this.ImportSqlConnection.ConnectionString = "server=.;Integrated Security=SSPI;Initial Catalog=mydb";
            this.ImportSqlConnection.Dock = System.Windows.Forms.DockStyle.Top;
            this.ImportSqlConnection.Location = new System.Drawing.Point(0, 48);
            this.ImportSqlConnection.Name = "ImportSqlConnection";
            this.ImportSqlConnection.Size = new System.Drawing.Size(748, 48);
            this.ImportSqlConnection.TabIndex = 1;
            // 
            // exportsqlConnection
            // 
            this.exportsqlConnection.ConnectionName = "Export Connection";
            this.exportsqlConnection.ConnectionString = "server=.;Integrated Security=SSPI;Initial Catalog=mydb";
            this.exportsqlConnection.Dock = System.Windows.Forms.DockStyle.Top;
            this.exportsqlConnection.Location = new System.Drawing.Point(0, 0);
            this.exportsqlConnection.Name = "exportsqlConnection";
            this.exportsqlConnection.Size = new System.Drawing.Size(748, 48);
            this.exportsqlConnection.TabIndex = 0;
            // 
            // LogTextBox
            // 
            this.LogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogTextBox.Location = new System.Drawing.Point(0, 183);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.Size = new System.Drawing.Size(748, 417);
            this.LogTextBox.TabIndex = 2;
            // 
            // ExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 600);
            this.Controls.Add(this.LogTextBox);
            this.Controls.Add(this.panel1);
            this.Name = "ExportForm";
            this.Text = "Run nDump Against Databases";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SqlConnectionControl exportsqlConnection;
        private System.Windows.Forms.Panel panel1;
        private SqlConnectionControl ImportSqlConnection;
        private System.Windows.Forms.CheckBox ExportCheckBox;
        private System.Windows.Forms.CheckBox ImportCheckBox;
        private System.Windows.Forms.CheckBox TransformCheckBox;
        private System.Windows.Forms.CheckBox ApplyFilterCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SqlScriptDirTextBox;
        private System.Windows.Forms.TextBox csvDirTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox LogTextBox;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
    }
}
