namespace WiFoUI.UI.Dialogs
{
	partial class OptionsDialog
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.grpServer = new System.Windows.Forms.GroupBox();
			this.txtPort = new System.Windows.Forms.TextBox();
			this.txtAddress = new System.Windows.Forms.TextBox();
			this.lblAddress = new System.Windows.Forms.Label();
			this.lblPort = new System.Windows.Forms.Label();
			this.tabExtensions = new System.Windows.Forms.TabPage();
			this.propertyGrid = new System.Windows.Forms.PropertyGrid();
			this.lstExtensions = new System.Windows.Forms.ListBox();
			this.grpPython = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.FBD = new System.Windows.Forms.FolderBrowserDialog();
			this.btnLibPath = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.grpServer.SuspendLayout();
			this.tabExtensions.SuspendLayout();
			this.grpPython.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabGeneral);
			this.tabControl1.Controls.Add(this.tabExtensions);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(4, 4);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(492, 315);
			this.tabControl1.TabIndex = 3;
			// 
			// tabGeneral
			// 
			this.tabGeneral.BackColor = System.Drawing.Color.White;
			this.tabGeneral.Controls.Add(this.grpPython);
			this.tabGeneral.Controls.Add(this.grpServer);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.tabGeneral.Size = new System.Drawing.Size(484, 289);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "WiFo";
			// 
			// grpServer
			// 
			this.grpServer.Controls.Add(this.txtPort);
			this.grpServer.Controls.Add(this.txtAddress);
			this.grpServer.Controls.Add(this.lblAddress);
			this.grpServer.Controls.Add(this.lblPort);
			this.grpServer.Location = new System.Drawing.Point(6, 6);
			this.grpServer.Name = "grpServer";
			this.grpServer.Padding = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.grpServer.Size = new System.Drawing.Size(472, 80);
			this.grpServer.TabIndex = 2;
			this.grpServer.TabStop = false;
			this.grpServer.Text = "Server Information";
			// 
			// txtPort
			// 
			this.txtPort.Location = new System.Drawing.Point(60, 46);
			this.txtPort.Name = "txtPort";
			this.txtPort.Size = new System.Drawing.Size(52, 20);
			this.txtPort.TabIndex = 3;
			this.txtPort.Text = "1363";
			this.txtPort.TextChanged += new System.EventHandler(this.txtPort_TextChanged);
			// 
			// txtAddress
			// 
			this.txtAddress.Location = new System.Drawing.Point(60, 20);
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.Size = new System.Drawing.Size(179, 20);
			this.txtAddress.TabIndex = 2;
			this.txtAddress.Text = "10.220.10.69";
			this.txtAddress.TextChanged += new System.EventHandler(this.txtAddress_TextChanged);
			// 
			// lblAddress
			// 
			this.lblAddress.AutoSize = true;
			this.lblAddress.Location = new System.Drawing.Point(6, 23);
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = new System.Drawing.Size(48, 13);
			this.lblAddress.TabIndex = 0;
			this.lblAddress.Text = "Address:";
			// 
			// lblPort
			// 
			this.lblPort.AutoSize = true;
			this.lblPort.Location = new System.Drawing.Point(6, 49);
			this.lblPort.Name = "lblPort";
			this.lblPort.Size = new System.Drawing.Size(29, 13);
			this.lblPort.TabIndex = 1;
			this.lblPort.Text = "Port:";
			// 
			// tabExtensions
			// 
			this.tabExtensions.BackColor = System.Drawing.Color.White;
			this.tabExtensions.Controls.Add(this.propertyGrid);
			this.tabExtensions.Controls.Add(this.lstExtensions);
			this.tabExtensions.Location = new System.Drawing.Point(4, 22);
			this.tabExtensions.Name = "tabExtensions";
			this.tabExtensions.Padding = new System.Windows.Forms.Padding(3);
			this.tabExtensions.Size = new System.Drawing.Size(484, 289);
			this.tabExtensions.TabIndex = 1;
			this.tabExtensions.Text = "Extensions";
			// 
			// propertyGrid
			// 
			this.propertyGrid.BackColor = System.Drawing.Color.White;
			this.propertyGrid.Location = new System.Drawing.Point(215, 6);
			this.propertyGrid.Name = "propertyGrid";
			this.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
			this.propertyGrid.Size = new System.Drawing.Size(263, 277);
			this.propertyGrid.TabIndex = 1;
			// 
			// lstExtensions
			// 
			this.lstExtensions.FormattingEnabled = true;
			this.lstExtensions.Location = new System.Drawing.Point(8, 6);
			this.lstExtensions.Name = "lstExtensions";
			this.lstExtensions.Size = new System.Drawing.Size(201, 277);
			this.lstExtensions.TabIndex = 0;
			this.lstExtensions.SelectedIndexChanged += new System.EventHandler(this.lstExtensions_SelectedIndexChanged);
			// 
			// grpPython
			// 
			this.grpPython.Controls.Add(this.btnLibPath);
			this.grpPython.Controls.Add(this.label1);
			this.grpPython.Location = new System.Drawing.Point(6, 92);
			this.grpPython.Name = "grpPython";
			this.grpPython.Padding = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.grpPython.Size = new System.Drawing.Size(472, 51);
			this.grpPython.TabIndex = 4;
			this.grpPython.TabStop = false;
			this.grpPython.Text = "Python";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Library Path:";
			// 
			// FBD
			// 
			this.FBD.ShowNewFolderButton = false;
			// 
			// btnLibPath
			// 
			this.btnLibPath.Location = new System.Drawing.Point(78, 18);
			this.btnLibPath.Name = "btnLibPath";
			this.btnLibPath.Size = new System.Drawing.Size(388, 23);
			this.btnLibPath.TabIndex = 1;
			this.btnLibPath.UseVisualStyleBackColor = true;
			this.btnLibPath.Click += new System.EventHandler(this.btnLibPath_Click);
			// 
			// OptionsDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(500, 323);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionsDialog";
			this.Padding = new System.Windows.Forms.Padding(4);
			this.Text = "WiFo Options";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsDialog_FormClosing);
			this.Load += new System.EventHandler(this.OptionsDialog_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.grpServer.ResumeLayout(false);
			this.grpServer.PerformLayout();
			this.tabExtensions.ResumeLayout(false);
			this.grpPython.ResumeLayout(false);
			this.grpPython.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.GroupBox grpServer;
		private System.Windows.Forms.TextBox txtPort;
		private System.Windows.Forms.TextBox txtAddress;
		private System.Windows.Forms.Label lblAddress;
		private System.Windows.Forms.Label lblPort;
		private System.Windows.Forms.TabPage tabExtensions;
		private System.Windows.Forms.PropertyGrid propertyGrid;
		private System.Windows.Forms.ListBox lstExtensions;
		private System.Windows.Forms.GroupBox grpPython;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnLibPath;
		private System.Windows.Forms.FolderBrowserDialog FBD;
	}
}