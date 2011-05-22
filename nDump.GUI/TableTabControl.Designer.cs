namespace nDump.GUI
{
    partial class TableTabControl
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.filterTabPage = new System.Windows.Forms.TabPage();
            this.selectTabPage = new System.Windows.Forms.TabPage();
            this.SetupTableGrid = new nDump.GUI.TableGrid();
            this.SelectTableGrid = new nDump.GUI.TableGrid();
            this.tabControl1.SuspendLayout();
            this.filterTabPage.SuspendLayout();
            this.selectTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.selectTabPage);
            this.tabControl1.Controls.Add(this.filterTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(391, 369);
            this.tabControl1.TabIndex = 0;
            // 
            // filterTabPage
            // 
            this.filterTabPage.Controls.Add(this.SetupTableGrid);
            this.filterTabPage.Location = new System.Drawing.Point(4, 22);
            this.filterTabPage.Name = "filterTabPage";
            this.filterTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.filterTabPage.Size = new System.Drawing.Size(383, 343);
            this.filterTabPage.TabIndex = 0;
            this.filterTabPage.Text = "Setup Tables";
            this.filterTabPage.UseVisualStyleBackColor = true;
            // 
            // selectTabPage
            // 
            this.selectTabPage.Controls.Add(this.SelectTableGrid);
            this.selectTabPage.Location = new System.Drawing.Point(4, 22);
            this.selectTabPage.Name = "selectTabPage";
            this.selectTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.selectTabPage.Size = new System.Drawing.Size(383, 343);
            this.selectTabPage.TabIndex = 1;
            this.selectTabPage.Text = "Selects";
            this.selectTabPage.UseVisualStyleBackColor = true;
            // 
            // SetupTableGrid
            // 
            this.SetupTableGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SetupTableGrid.Location = new System.Drawing.Point(3, 3);
            this.SetupTableGrid.Name = "SetupTableGrid";
            this.SetupTableGrid.SelectList = null;
            this.SetupTableGrid.Size = new System.Drawing.Size(377, 337);
            this.SetupTableGrid.TabIndex = 0;
            // 
            // SelectTableGrid
            // 
            this.SelectTableGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectTableGrid.Location = new System.Drawing.Point(3, 3);
            this.SelectTableGrid.Name = "SelectTableGrid";
            this.SelectTableGrid.SelectList = null;
            this.SelectTableGrid.Size = new System.Drawing.Size(377, 337);
            this.SelectTableGrid.TabIndex = 0;
            // 
            // TableTabControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "TableTabControl";
            this.Size = new System.Drawing.Size(391, 369);
            this.tabControl1.ResumeLayout(false);
            this.filterTabPage.ResumeLayout(false);
            this.selectTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage filterTabPage;
        private System.Windows.Forms.TabPage selectTabPage;
        private TableGrid SelectTableGrid;
        private TableGrid SetupTableGrid;
    }
}
