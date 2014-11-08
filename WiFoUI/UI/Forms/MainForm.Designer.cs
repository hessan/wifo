namespace WiFoUI.UI.Forms
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.mnuMain = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sep1 = new System.Windows.Forms.ToolStripSeparator();
			this.exportDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.importDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sep2 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.studiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
			this.timelineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sepView = new System.Windows.Forms.ToolStripSeparator();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.autoScrollToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tickIntervalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.itmTicks1 = new System.Windows.Forms.ToolStripMenuItem();
			this.itmTicks2 = new System.Windows.Forms.ToolStripMenuItem();
			this.itmTicks3 = new System.Windows.Forms.ToolStripMenuItem();
			this.itmTicks4 = new System.Windows.Forms.ToolStripMenuItem();
			this.itmTicks5 = new System.Windows.Forms.ToolStripMenuItem();
			this.sep3 = new System.Windows.Forms.ToolStripSeparator();
			this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sep4 = new System.Windows.Forms.ToolStripSeparator();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblSep = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblRecords = new System.Windows.Forms.ToolStripStatusLabel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.txtExpression = new System.Windows.Forms.TextBox();
			this.btnApply = new System.Windows.Forms.Button();
			this.OFD = new System.Windows.Forms.OpenFileDialog();
			this.SFD = new System.Windows.Forms.SaveFileDialog();
			this.timelineChart = new WiFoUI.UI.Components.TimelineChart(this.components);
			this.viewCanvas = new WiFoUI.UI.Components.ViewCanvas(this.components);
			this.mnuMain.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mnuMain
			// 
			this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.studiesToolStripMenuItem,
            this.mnuView,
            this.toolsToolStripMenuItem});
			this.mnuMain.Location = new System.Drawing.Point(0, 0);
			this.mnuMain.Name = "mnuMain";
			this.mnuMain.Size = new System.Drawing.Size(532, 24);
			this.mnuMain.TabIndex = 1;
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.sep1,
            this.exportDataToolStripMenuItem,
            this.importDataToolStripMenuItem,
            this.sep2,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// connectToolStripMenuItem
			// 
			this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
			this.connectToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.connectToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.connectToolStripMenuItem.Text = "&Connect";
			this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
			// 
			// sep1
			// 
			this.sep1.Name = "sep1";
			this.sep1.Size = new System.Drawing.Size(171, 6);
			// 
			// exportDataToolStripMenuItem
			// 
			this.exportDataToolStripMenuItem.Name = "exportDataToolStripMenuItem";
			this.exportDataToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.exportDataToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.exportDataToolStripMenuItem.Text = "&Export Data";
			this.exportDataToolStripMenuItem.Click += new System.EventHandler(this.exportDataToolStripMenuItem_Click);
			// 
			// importDataToolStripMenuItem
			// 
			this.importDataToolStripMenuItem.Name = "importDataToolStripMenuItem";
			this.importDataToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.importDataToolStripMenuItem.Text = "&Import Data";
			this.importDataToolStripMenuItem.Click += new System.EventHandler(this.importDataToolStripMenuItem_Click);
			// 
			// sep2
			// 
			this.sep2.Name = "sep2";
			this.sep2.Size = new System.Drawing.Size(171, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.ShortcutKeyDisplayString = "Alt+F4";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// studiesToolStripMenuItem
			// 
			this.studiesToolStripMenuItem.Name = "studiesToolStripMenuItem";
			this.studiesToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
			this.studiesToolStripMenuItem.Text = "&Studies";
			// 
			// mnuView
			// 
			this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timelineToolStripMenuItem,
            this.sepView});
			this.mnuView.Name = "mnuView";
			this.mnuView.Size = new System.Drawing.Size(44, 20);
			this.mnuView.Text = "&View";
			// 
			// timelineToolStripMenuItem
			// 
			this.timelineToolStripMenuItem.Checked = true;
			this.timelineToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.timelineToolStripMenuItem.Name = "timelineToolStripMenuItem";
			this.timelineToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
			this.timelineToolStripMenuItem.Text = "&Timeline";
			this.timelineToolStripMenuItem.Click += new System.EventHandler(this.view_Click);
			// 
			// sepView
			// 
			this.sepView.Name = "sepView";
			this.sepView.Size = new System.Drawing.Size(117, 6);
			this.sepView.Visible = false;
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoScrollToolStripMenuItem,
            this.tickIntervalToolStripMenuItem,
            this.sep3,
            this.zoomInToolStripMenuItem,
            this.zoomOutToolStripMenuItem,
            this.sep4,
            this.optionsToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			this.toolsToolStripMenuItem.Text = "&Tools";
			// 
			// autoScrollToolStripMenuItem
			// 
			this.autoScrollToolStripMenuItem.Name = "autoScrollToolStripMenuItem";
			this.autoScrollToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.autoScrollToolStripMenuItem.Text = "Auto Scroll";
			this.autoScrollToolStripMenuItem.Click += new System.EventHandler(this.autoScrollToolStripMenuItem_Click);
			// 
			// tickIntervalToolStripMenuItem
			// 
			this.tickIntervalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmTicks1,
            this.itmTicks2,
            this.itmTicks3,
            this.itmTicks4,
            this.itmTicks5});
			this.tickIntervalToolStripMenuItem.Name = "tickIntervalToolStripMenuItem";
			this.tickIntervalToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.tickIntervalToolStripMenuItem.Text = "Tick Interval";
			this.tickIntervalToolStripMenuItem.DropDownOpening += new System.EventHandler(this.tickIntervalToolStripMenuItem_DropDownOpening);
			// 
			// itmTicks1
			// 
			this.itmTicks1.Name = "itmTicks1";
			this.itmTicks1.Size = new System.Drawing.Size(80, 22);
			this.itmTicks1.Text = "1";
			this.itmTicks1.Click += new System.EventHandler(this.itmTicks_Click);
			// 
			// itmTicks2
			// 
			this.itmTicks2.Name = "itmTicks2";
			this.itmTicks2.Size = new System.Drawing.Size(80, 22);
			this.itmTicks2.Text = "2";
			this.itmTicks2.Click += new System.EventHandler(this.itmTicks_Click);
			// 
			// itmTicks3
			// 
			this.itmTicks3.Name = "itmTicks3";
			this.itmTicks3.Size = new System.Drawing.Size(80, 22);
			this.itmTicks3.Text = "3";
			this.itmTicks3.Click += new System.EventHandler(this.itmTicks_Click);
			// 
			// itmTicks4
			// 
			this.itmTicks4.Name = "itmTicks4";
			this.itmTicks4.Size = new System.Drawing.Size(80, 22);
			this.itmTicks4.Text = "4";
			this.itmTicks4.Click += new System.EventHandler(this.itmTicks_Click);
			// 
			// itmTicks5
			// 
			this.itmTicks5.Name = "itmTicks5";
			this.itmTicks5.Size = new System.Drawing.Size(80, 22);
			this.itmTicks5.Text = "5";
			this.itmTicks5.Click += new System.EventHandler(this.itmTicks_Click);
			// 
			// sep3
			// 
			this.sep3.Name = "sep3";
			this.sep3.Size = new System.Drawing.Size(165, 6);
			// 
			// zoomInToolStripMenuItem
			// 
			this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
			this.zoomInToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl++";
			this.zoomInToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Oemplus)));
			this.zoomInToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.zoomInToolStripMenuItem.Text = "Zoom &In";
			this.zoomInToolStripMenuItem.Click += new System.EventHandler(this.zoomInToolStripMenuItem_Click);
			// 
			// zoomOutToolStripMenuItem
			// 
			this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
			this.zoomOutToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+-";
			this.zoomOutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.OemMinus)));
			this.zoomOutToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.zoomOutToolStripMenuItem.Text = "Zoom &Out";
			this.zoomOutToolStripMenuItem.Click += new System.EventHandler(this.zoomOutToolStripMenuItem_Click);
			// 
			// sep4
			// 
			this.sep4.Name = "sep4";
			this.sep4.Size = new System.Drawing.Size(165, 6);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.optionsToolStripMenuItem.Text = "&Options";
			this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.lblSep,
            this.lblRecords});
			this.statusStrip1.Location = new System.Drawing.Point(0, 305);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(532, 22);
			this.statusStrip1.TabIndex = 2;
			// 
			// lblStatus
			// 
			this.lblStatus.ForeColor = System.Drawing.Color.Black;
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(43, 17);
			this.lblStatus.Text = "Offline";
			// 
			// lblSep
			// 
			this.lblSep.Name = "lblSep";
			this.lblSep.Size = new System.Drawing.Size(399, 17);
			this.lblSep.Spring = true;
			// 
			// lblRecords
			// 
			this.lblRecords.Margin = new System.Windows.Forms.Padding(10, 3, 10, 2);
			this.lblRecords.Name = "lblRecords";
			this.lblRecords.Size = new System.Drawing.Size(55, 17);
			this.lblRecords.Text = "0 records";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.txtExpression);
			this.panel1.Controls.Add(this.btnApply);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 24);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(20);
			this.panel1.Size = new System.Drawing.Size(532, 62);
			this.panel1.TabIndex = 3;
			// 
			// txtExpression
			// 
			this.txtExpression.AcceptsReturn = true;
			this.txtExpression.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtExpression.Location = new System.Drawing.Point(20, 20);
			this.txtExpression.Name = "txtExpression";
			this.txtExpression.Size = new System.Drawing.Size(417, 20);
			this.txtExpression.TabIndex = 9;
			this.txtExpression.Text = "RX_BUSY, not FREE_PHY, TX_BUSY, MPDU_TIMEOUT, PLCP_TIMEOUT";
			this.txtExpression.WordWrap = false;
			// 
			// btnApply
			// 
			this.btnApply.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnApply.Location = new System.Drawing.Point(437, 20);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(75, 22);
			this.btnApply.TabIndex = 10;
			this.btnApply.Text = "Apply";
			this.btnApply.UseVisualStyleBackColor = true;
			// 
			// OFD
			// 
			this.OFD.Title = "Import Data";
			// 
			// SFD
			// 
			this.SFD.Filter = "Text Files (*.txt)|*.txt";
			this.SFD.Title = "Export Data";
			// 
			// timelineChart
			// 
			this.timelineChart.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(134)))), ((int)(((byte)(150)))));
			this.timelineChart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.timelineChart.Expression = null;
			this.timelineChart.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(134)))), ((int)(((byte)(150)))));
			this.timelineChart.Location = new System.Drawing.Point(0, 86);
			this.timelineChart.Margin = new System.Windows.Forms.Padding(8);
			this.timelineChart.MarkerEnd = 0;
			this.timelineChart.MarkerStart = 0;
			this.timelineChart.Name = "timelineChart";
			this.timelineChart.PanLock = false;
			this.timelineChart.Size = new System.Drawing.Size(532, 219);
			this.timelineChart.Span = 10000;
			this.timelineChart.TabIndex = 0;
			this.timelineChart.TickInterval = 1000;
			// 
			// viewCanvas
			// 
			this.viewCanvas.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(134)))), ((int)(((byte)(150)))));
			this.viewCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
			this.viewCanvas.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(134)))), ((int)(((byte)(150)))));
			this.viewCanvas.Location = new System.Drawing.Point(0, 86);
			this.viewCanvas.MarkerEnd = 0;
			this.viewCanvas.MarkerStart = 0;
			this.viewCanvas.Name = "viewCanvas";
			this.viewCanvas.PanLock = false;
			this.viewCanvas.Size = new System.Drawing.Size(532, 219);
			this.viewCanvas.Span = 17;
			this.viewCanvas.TabIndex = 4;
			this.viewCanvas.TimelineView = null;
			this.viewCanvas.Visible = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(532, 327);
			this.Controls.Add(this.viewCanvas);
			this.Controls.Add(this.timelineChart);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.mnuMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.mnuMain;
			this.MinimumSize = new System.Drawing.Size(500, 320);
			this.Name = "MainForm";
			this.Text = "WiFo";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.mnuMain.ResumeLayout(false);
			this.mnuMain.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private UI.Components.TimelineChart timelineChart;
		private System.Windows.Forms.MenuStrip mnuMain;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator sep1;
		private System.Windows.Forms.ToolStripMenuItem exportDataToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importDataToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator sep2;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripMenuItem studiesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.ToolStripStatusLabel lblSep;
		private System.Windows.Forms.ToolStripStatusLabel lblRecords;
		private System.Windows.Forms.ToolStripMenuItem mnuView;
		private System.Windows.Forms.ToolStripMenuItem timelineToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator sepView;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox txtExpression;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.OpenFileDialog OFD;
		private System.Windows.Forms.SaveFileDialog SFD;
		private System.Windows.Forms.ToolStripMenuItem zoomInToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem zoomOutToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator sep3;
		private System.Windows.Forms.ToolStripMenuItem autoScrollToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tickIntervalToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator sep4;
		private System.Windows.Forms.ToolStripMenuItem itmTicks1;
		private System.Windows.Forms.ToolStripMenuItem itmTicks2;
		private System.Windows.Forms.ToolStripMenuItem itmTicks3;
		private System.Windows.Forms.ToolStripMenuItem itmTicks4;
		private System.Windows.Forms.ToolStripMenuItem itmTicks5;
		private Components.ViewCanvas viewCanvas;
	}
}