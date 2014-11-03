using System;
using System.Windows.Forms;
using WiFo.Extensibility;
using WiFo.Extensibility.Settings;
using WiFoUI.Logic;

namespace WiFoUI.UI.Dialogs
{
	public partial class OptionsDialog : Form
	{
		private class ExtensionWrapper
		{
			public ExtensionWrapper(IExtension ext)
			{
				this.ext = ext;
			}

			public override string ToString()
			{
				return ext.DisplayName;
			}

			internal IExtension ext;
		}

		public OptionsDialog()
		{
			InitializeComponent();
		}

		private void OptionsDialog_Load(object sender, EventArgs e)
		{
			foreach (IExtension ext in ExtensionManager.All)
				if (ext is ISettingsContributor)
					lstExtensions.Items.Add(new ExtensionWrapper(ext));

			if (lstExtensions.Items.Count > 0)
				lstExtensions.SelectedIndex = 0;

			txtAddress.Text = SettingsManager.Default.ServerAddress;
			txtPort.Text = SettingsManager.Default.ServerPort.ToString();

			string path = SettingsManager.Default.PythonLibraryPath;
			btnLibPath.Text = path == null ? "(Not specified)" : path;
		}

		private void lstExtensions_SelectedIndexChanged(object sender, EventArgs e)
		{
			propertyGrid.SelectedObject = ((ExtensionWrapper)lstExtensions.SelectedItem).ext;
		}

		private void OptionsDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			SettingsManager.Default.Save();
		}

		private void txtAddress_TextChanged(object sender, EventArgs e)
		{
			if (txtAddress.Text.Length > 0)
				SettingsManager.Default.ServerAddress = txtAddress.Text;
		}

		private void txtPort_TextChanged(object sender, EventArgs e)
		{
			int port;

			if (int.TryParse(txtPort.Text, out port))
			{
				if (port > 0)
					SettingsManager.Default.ServerPort = port;
				else txtPort.Text = "1";
			}
		}

		private void btnLibPath_Click(object sender, EventArgs e)
		{
			if (SettingsManager.Default.PythonLibraryPath != null)
				FBD.SelectedPath = SettingsManager.Default.PythonLibraryPath;

			if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				SettingsManager.Default.PythonLibraryPath = FBD.SelectedPath;
				btnLibPath.Text = FBD.SelectedPath;

				MessageBox.Show("This will take effect the next time you run the application.", "WiFo", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
	}
}
