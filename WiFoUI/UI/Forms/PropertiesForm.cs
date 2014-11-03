using System.Windows.Forms;

namespace WiFoUI.UI.Forms
{
	public partial class PropertiesForm : Form
	{
		public PropertiesForm(object obj)
		{
			InitializeComponent();
			propertyGrid.SelectedObject = obj;
		}
	}
}
