using System.Windows.Forms;
using WiFoUI.UI.Components;

namespace WiFoUI.UI.Forms
{
	public partial class ChartForm : Form
	{
		public ChartForm()
		{
			InitializeComponent();
		}

		public void InitializeChart(object[] xs, double[] ys)
		{
			this.chart.XValues = xs;
			this.chart.YValues = ys;
		}
	}
}
