namespace nDump.GUI
{
    partial class SqlConnectionControl
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.connectionNameLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.testButton = new System.Windows.Forms.Button();
            this.connectionTextBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.connectionNameLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(70, 48);
            this.panel1.TabIndex = 0;
            // 
            // connectionNameLabel
            // 
            this.connectionNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionNameLabel.Location = new System.Drawing.Point(0, 0);
            this.connectionNameLabel.Name = "connectionNameLabel";
            this.connectionNameLabel.Size = new System.Drawing.Size(70, 48);
            this.connectionNameLabel.TabIndex = 2;
            this.connectionNameLabel.Text = "Connection:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.testButton);
            this.panel2.Controls.Add(this.connectionTextBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(70, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(227, 48);
            this.panel2.TabIndex = 1;
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(89, 22);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(135, 23);
            this.testButton.TabIndex = 2;
            this.testButton.Text = "Test Connection";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.TestButtonClick);
            // 
            // connectionTextBox
            // 
            this.connectionTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.connectionTextBox.Location = new System.Drawing.Point(0, 0);
            this.connectionTextBox.Name = "connectionTextBox";
            this.connectionTextBox.Size = new System.Drawing.Size(227, 20);
            this.connectionTextBox.TabIndex = 1;
            this.connectionTextBox.Text = "server=.;Integrated Security=SSPI;Initial Catalog=mydb";
            // 
            // SqlConnectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "SqlConnectionControl";
            this.Size = new System.Drawing.Size(297, 48);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label connectionNameLabel;
        private System.Windows.Forms.TextBox connectionTextBox;
        private System.Windows.Forms.Button testButton;
    }
}
