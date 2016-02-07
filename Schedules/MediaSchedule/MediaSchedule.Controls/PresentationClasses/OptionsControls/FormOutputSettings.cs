using System.Drawing;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls
{
	public partial class FormOutputSettings : DevComponents.DotNetBar.Metro.MetroForm
	{
		public FormOutputSettings()
		{
			InitializeComponent();
		}

		private void FormOutputSettings_Load(object sender, System.EventArgs e)
		{
			checkEditApplyForAll.ForeColor = checkEditApplyForAll.Enabled ? Color.Black : Color.Gray;
		}
	}
}