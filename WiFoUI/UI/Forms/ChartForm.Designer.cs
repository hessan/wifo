namespace WiFoUI.UI.Forms
{
	partial class ChartForm
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
			this.chart = new WiFoUI.UI.Components.BarChart(this.components);
			this.SuspendLayout();
			// 
			// chart
			// 
			this.chart.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(134)))), ((int)(((byte)(150)))));
			this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chart.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(134)))), ((int)(((byte)(150)))));
			this.chart.Location = new System.Drawing.Point(0, 0);
			this.chart.MarkerEnd = 0;
			this.chart.MarkerStart = 0;
			this.chart.Name = "chart";
			this.chart.PanLock = false;
			this.chart.Size = new System.Drawing.Size(536, 384);
			this.chart.Span = 3;
			this.chart.TabIndex = 0;
			this.chart.XValues = null;
			this.chart.YTicks = 10;
			this.chart.YValues = null;

			// 
			// ChartForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(536, 384);
			this.Controls.Add(this.chart);
			this.Name = "ChartForm";
			this.Text = "ChartDialog";
			this.ResumeLayout(false);

		}

		#endregion

		private Components.BarChart chart;
	}
}