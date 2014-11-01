using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WiFoUI.UI.Dialogs
{
	public partial class InputDialog : Form
	{
		public InputDialog()
		{
			InitializeComponent();
		}

		public static string Show(string title, string message, string def)
		{
			InputDialog dialog = new InputDialog();
			dialog.Text = title;
			dialog.lblMessage.Text = message;
			dialog.txtValue.Text = def == null ? "" : def;
			dialog.txtValue.SelectAll();

			if (dialog.ShowDialog() == DialogResult.OK)
				return dialog.txtValue.Text;

			return null;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		private void txtValue_TextChanged(object sender, EventArgs e)
		{
			btnOK.Enabled = txtValue.TextLength > 0;
		}
	}
}
