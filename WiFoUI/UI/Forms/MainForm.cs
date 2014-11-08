using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using WiFo.Data;
using WiFo.Extensibility;
using WiFo.Expressions;
using WiFo.UI;
using WiFoUI.Logic;
using WiFoUI.UI.Dialogs;
using WiFoUI.UI.Components;

namespace WiFoUI.UI.Forms
{
	public partial class MainForm : Form, IWiFoContext
	{
		class ExecutionResult
		{
			public dynamic obj;
		}

		public dynamic Execute(UserInput input)
		{
			ExecutionResult result = new ExecutionResult();
			result.obj = null;

			this.Invoke(new MethodInvoker(delegate
			{
				switch (input.InputType)
				{
					case UserInputType.Boolean:
						result.obj = MessageBox.Show(
							input.Message,
							input.Title,
							MessageBoxButtons.OKCancel) == DialogResult.OK;
						break;
					case UserInputType.Integer:
						result.obj = NumberDialog.Show(
							input.Title,
							input.DefaultIntValue,
							input.Minimum,
							input.Maximum,
							input.UnitText == null ? String.Empty : input.UnitText);
						break;
					case UserInputType.String:
						result.obj = InputDialog.Show(
							input.Title,
							input.Message,
							input.DefaultValue);
						break;
					case UserInputType.FileName:
						OFD.Title = input.Title;
						OFD.Filter = input.Filter;
						OFD.FileName = "";

						if (OFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
							result.obj = OFD.FileName;
						else result.obj = null;
						break;
				}
			}), result);

			return result.obj;
		}

		public void Execute(UserOutput output)
		{
			this.BeginInvoke(new MethodInvoker(delegate
			{
				switch (output.OutputType)
				{
					case UserOutputType.BarPlot:
						ChartForm form = new ChartForm();
						form.Text = output.Title;
						form.InitializeChart(output.XValues, output.YValues);
						form.Show();
						break;
					case UserOutputType.Message:
						MessageBox.Show(
							output.Message,
							output.Title,
							MessageBoxButtons.OK,
							output.IsWarning ? MessageBoxIcon.Warning : MessageBoxIcon.None);
						break;
					case UserOutputType.Results:
						PropertiesForm props = new PropertiesForm(output.Results);
						props.Text = output.Title;
						props.Show();
						break;
				}
			}));
		}

		public MainForm()
		{
			Microsoft.VisualBasic.Devices.ComputerInfo info = new Microsoft.VisualBasic.Devices.ComputerInfo();
			int capacity = (int)Math.Min(int.MaxValue, info.AvailablePhysicalMemory / (ulong)16);
			RecordList records = new RecordList(capacity);

			InitializeComponent();

			timelineChart.Initialize(records);
			timelineChart.Span = 100000;
			timelineChart.TickInterval = 10000;
			viewCanvas.Initialize(records);
			timelineChart.Expression = new Expression(txtExpression.Text);
			client.FetchComplete += new FetchEventHandler(client_FetchComplete);
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			client.Connected += new EventHandler(client_Connected);
			client.Disconnected += new EventHandler(client_Disconnected);
			SettingsManager.Default.Load();
			ExtensionManager.Default.LoadExtensions();
			PopulateStudies();
			PopulateTimelineViews();
			SettingsManager.Default.Load();
			autoScrollToolStripMenuItem.Checked = timelineChart.PanLock;
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (!client.IsStopped)
				client.Stop();

			Application.Exit();
		}

		private void client_Connected(object sender, EventArgs e)
		{
			connectToolStripMenuItem.Text = "Disconnect";
			lblStatus.Text = "Online";
			lblStatus.ForeColor = Color.DarkGreen;
			connectToolStripMenuItem.Enabled = true;
		}

		private void client_Disconnected(object sender, EventArgs e)
		{
			connectToolStripMenuItem.Text = "Connect";
			lblStatus.Text = "Offline";
			lblStatus.ForeColor = Color.Black;
			connectToolStripMenuItem.Enabled = true;
		}

		private void client_FetchComplete(object sender, FetchEventArgs e)
		{
			RecordList records = timelineChart.Timeline.Records;
			records.AppendAll(e.Records);

			try
			{
				lblRecords.Text = records.Count + " records";
				timelineChart.Invalidate();
			}
			catch { }
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void study_Click(object sender, EventArgs e)
		{
			IStudy ext = (IStudy)((ToolStripMenuItem)sender).Tag;

			new Thread(new ThreadStart(delegate
			{
				timelineChart.Timeline.PerformStudy(ext, (uint)timelineChart.MarkerStart, (uint)timelineChart.MarkerEnd, this);
			})).Start();
		}

		private void view_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = (ToolStripMenuItem)sender;

			foreach (ToolStripItem temp in mnuView.DropDownItems)
			{
				if (temp is ToolStripMenuItem)
					((ToolStripMenuItem)temp).Checked = false;
			}

			if (viewCanvas.TimelineView != null)
				viewCanvas.TimelineView.OnUnSelected(timelineChart.Timeline, this);

			AbstractChart oldChart = timelineChart.Visible ? (AbstractChart)timelineChart : viewCanvas;
			AbstractChart newChart;

			if (item.Tag == null)
			{
				if (viewCanvas.TimelineView != null)
					viewCanvas.TimelineView = null;

				newChart = timelineChart;
			}
			else
			{
				ITimelineView view = (ITimelineView)item.Tag;
				viewCanvas.TimelineView = view;
				view.OnSelected(timelineChart.Timeline, this);
				newChart = viewCanvas;
			}

			oldChart.CopyVisualStateTo(newChart);
			newChart.Visible = true;
			oldChart.Visible = false;
			item.Checked = true;
		}

		private void connectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (client.IsStopped)
			{
				connectToolStripMenuItem.Text = lblStatus.Text = "Connecting...";
				client.Start();
			}
			else
			{
				connectToolStripMenuItem.Text = lblStatus.Text = "Disconnecting...";
				client.Stop();
			}

			lblStatus.ForeColor = Color.DarkGray;
			connectToolStripMenuItem.Enabled = false;
		}

		private void exportDataToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (SFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				StreamWriter writer = new StreamWriter(SFD.FileName);
				RecordList records = timelineChart.Timeline.Records;

				for (int i = 0; i < records.Count; i++)
				{
					Record rec = records[i];
					writer.WriteLine(rec.Time + "\t" + rec.State);
				}

				writer.Close();
			}
		}

		private void importDataToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OFD.Title = "Import Data";
			OFD.Filter = "Text Files (*.txt)|*.txt";

			if (OFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				System.IO.StreamReader reader = new System.IO.StreamReader(OFD.FileName);
				RecordList records = timelineChart.Timeline.Records;
				string l = reader.ReadLine();

				timelineChart.Clear();

				Record[] buffer = new Record[64];
				int head = 0;

				while (l != null)
				{
					string[] s = l.Split('\t');
					uint time, state;

					if (!uint.TryParse(s[0], out time))
						break;

					if (!uint.TryParse(s[1], out state))
						break;

					buffer[head++] = new Record(time, state);

					if (head == buffer.Length)
					{
						records.AppendAll(buffer);
						head = 0;
					}

					l = reader.ReadLine();
				}

				reader.Close();
				lblRecords.Text = records.Count + " records";
				timelineChart.Invalidate(timelineChart.ChartBounds);
			}
		}

		private void autoScrollToolStripMenuItem_Click(object sender, EventArgs e)
		{
			timelineChart.PanLock = !timelineChart.PanLock;
			autoScrollToolStripMenuItem.Checked = timelineChart.PanLock;
			timelineChart.Invalidate();
		}

		private void itmTicks_Click(object sender, EventArgs e)
		{
			timelineChart.TickInterval = (int)((ToolStripMenuItem)sender).Tag;
		}

		private void tickIntervalToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
		{
			int minimum = timelineChart.Span / 10;
			int maximum = timelineChart.Span / 2;

			int[] values = { minimum, minimum + (maximum - minimum) / 4, (minimum + maximum) / 2, minimum + 3 * (maximum - minimum) / 4, maximum };
			ToolStripMenuItem[] items = { itmTicks1, itmTicks2, itmTicks3, itmTicks4, itmTicks5 };

			for (int i = 0; i < 5; i++)
			{
				items[i].Text = values[i] + "μs";
				items[i].Tag = values[i];
			}
		}

		private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (timelineChart.Span > 100)
			{
				timelineChart.Span /= 10;
				timelineChart.TickInterval /= 10;
			}
		}

		private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (timelineChart.Span < int.MaxValue / 100)
			{
				timelineChart.Span *= 10;
				timelineChart.TickInterval *= 10;
			}
		}

		private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new OptionsDialog().ShowDialog(this);
		}

		private void PopulateStudies()
		{
			int count = 0;

			foreach (IStudy ext in ExtensionManager.Default.Studies)
			{
				ToolStripMenuItem item = new ToolStripMenuItem(ext.DisplayName);
				item.Tag = ext;
				item.Click += new EventHandler(study_Click);
				studiesToolStripMenuItem.DropDownItems.Add(item);
				count++;
			}

			if (count > 0)
				studiesToolStripMenuItem.Enabled = true;
		}

		private void PopulateTimelineViews()
		{
			int count = 0;

			foreach (ITimelineView ext in ExtensionManager.Default.TimelineViews)
			{
				ToolStripMenuItem item = new ToolStripMenuItem(ext.DisplayName);
				item.Tag = ext;
				item.Click += new EventHandler(view_Click);
				mnuView.DropDownItems.Add(item);
				count++;
			}

			if (count > 0)
				sepView.Visible = true;
		}

		private Client client = new Client("10.220.10.69", 1363);
	}
}
