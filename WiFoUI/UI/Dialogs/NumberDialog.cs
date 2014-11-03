using System;
using System.Windows.Forms;

namespace WiFoUI.UI.Dialogs
{
	public partial class NumberDialog : Form
	{
		public NumberDialog()
		{
			InitializeComponent();
		}

		public static int? Show(string title, int initialValue, int min, int max, string unit)
		{
			NumberDialog dialog = new NumberDialog();
			dialog.Text = title;
			dialog.minValue = min;
			dialog.maxValue = max;
			dialog.lblUnit.Text = unit;
			dialog.txtValue.Text = Math.Min(Math.Max(initialValue, min), max).ToString();
			dialog.txtValue.SelectAll();

			if (dialog.ShowDialog() == DialogResult.OK)
				return dialog.ReturnValue;

			return null;
		}

		private int ReturnValue
		{
			get
			{
				return int.Parse(txtValue.Text);
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			int val;

			if (int.TryParse(txtValue.Text, out val))
			{
				lblError.Visible = false;

				if (val < minValue)
					txtValue.Text = minValue.ToString();
				else if (val > maxValue)
					txtValue.Text = maxValue.ToString();
				else DialogResult = System.Windows.Forms.DialogResult.OK;
			}
			else lblError.Visible = true;
		}

		private void txtValue_TextChanged(object sender, EventArgs e)
		{
			btnOK.Enabled = txtValue.TextLength > 0;
		}

		private int minValue, maxValue;
	}
}
