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
    partial class TableGrid
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.selectDataGridView = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.IgnoredColumnsButton = new System.Windows.Forms.Button();
            this.moveDownButton = new System.Windows.Forms.Button();
            this.moveUpButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.selectDataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectDataGridView
            // 
            this.selectDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.selectDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectDataGridView.Location = new System.Drawing.Point(0, 0);
            this.selectDataGridView.MultiSelect = false;
            this.selectDataGridView.Name = "selectDataGridView";
            this.selectDataGridView.Size = new System.Drawing.Size(529, 131);
            this.selectDataGridView.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.IgnoredColumnsButton);
            this.panel1.Controls.Add(this.moveDownButton);
            this.panel1.Controls.Add(this.moveUpButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 131);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(529, 37);
            this.panel1.TabIndex = 2;
            // 
            // IgnoredColumnsButton
            // 
            this.IgnoredColumnsButton.Location = new System.Drawing.Point(182, 11);
            this.IgnoredColumnsButton.Name = "IgnoredColumnsButton";
            this.IgnoredColumnsButton.Size = new System.Drawing.Size(121, 23);
            this.IgnoredColumnsButton.TabIndex = 4;
            this.IgnoredColumnsButton.Text = "Ignored Columns";
            this.IgnoredColumnsButton.UseVisualStyleBackColor = true;
            this.IgnoredColumnsButton.Click += new System.EventHandler(this.IgnoredColumnsButtonClick);
            // 
            // moveDownButton
            // 
            this.moveDownButton.Location = new System.Drawing.Point(91, 11);
            this.moveDownButton.Name = "moveDownButton";
            this.moveDownButton.Size = new System.Drawing.Size(75, 23);
            this.moveDownButton.TabIndex = 3;
            this.moveDownButton.Text = "Move Down";
            this.moveDownButton.UseVisualStyleBackColor = true;
            this.moveDownButton.Click += new System.EventHandler(this.MoveDownButtonClick);
            // 
            // moveUpButton
            // 
            this.moveUpButton.Location = new System.Drawing.Point(10, 11);
            this.moveUpButton.Name = "moveUpButton";
            this.moveUpButton.Size = new System.Drawing.Size(75, 23);
            this.moveUpButton.TabIndex = 2;
            this.moveUpButton.Text = "Move Up";
            this.moveUpButton.UseVisualStyleBackColor = true;
            this.moveUpButton.Click += new System.EventHandler(this.MoveUpButtonClick);
            // 
            // TableGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.selectDataGridView);
            this.Controls.Add(this.panel1);
            this.Name = "TableGrid";
            this.Size = new System.Drawing.Size(529, 168);
            ((System.ComponentModel.ISupportInitialize)(this.selectDataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView selectDataGridView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button moveDownButton;
        private System.Windows.Forms.Button moveUpButton;
        private System.Windows.Forms.Button IgnoredColumnsButton;
    }
}
