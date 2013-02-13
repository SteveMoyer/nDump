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
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace nDump.GUI
{
    public partial class IgnoredColumnsForm : Form
    {
        private Panel panel1;
        private new Button CancelButton;
        private Button OkButton;
        private Label label1;
        private Panel panel2;
        private TextBox columnsListTextBox;

        private void InitializeComponent()
        {
            panel1 = new Panel();
            CancelButton = new Button();
            OkButton = new Button();
            label1 = new Label();
            panel2 = new Panel();
            columnsListTextBox = new TextBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(CancelButton);
            panel1.Controls.Add(OkButton);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 218);
            panel1.Name = "panel1";
            panel1.Size = new Size(292, 55);
            panel1.TabIndex = 0;
            // 
            // CancelButton
            // 
            CancelButton.Location = new Point(214, 29);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(75, 23);
            CancelButton.TabIndex = 2;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButtonClick;
            // 
            // OkButton
            // 
            OkButton.Location = new Point(133, 29);
            OkButton.Name = "OkButton";
            OkButton.Size = new Size(75, 23);
            OkButton.TabIndex = 1;
            OkButton.Text = "Ok";
            OkButton.UseVisualStyleBackColor = true;
            OkButton.Click += OkButtonClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(4, 4);
            label1.Name = "label1";
            label1.Size = new Size(181, 13);
            label1.TabIndex = 0;
            label1.Text = "Enter ignored Columns(One Per Line)";
            // 
            // panel2
            // 
            panel2.Controls.Add(columnsListTextBox);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(292, 218);
            panel2.TabIndex = 1;
            // 
            // columnsListTextBox
            // 
            columnsListTextBox.Dock = DockStyle.Fill;
            columnsListTextBox.Location = new Point(0, 0);
            columnsListTextBox.Multiline = true;
            columnsListTextBox.Name = "columnsListTextBox";
            columnsListTextBox.Size = new Size(292, 218);
            columnsListTextBox.TabIndex = 0;
            // 
            // IgnoredColumnsForm
            // 
            ClientSize = new Size(292, 273);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "IgnoredColumnsForm";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        
    }
}
