using System.Drawing;

namespace Asa.MediaSchedule.Controls.PresentationClasses.SnapshotControls
{
	public partial class FormOutputSettings : DevComponents.DotNetBar.Metro.MetroForm
	{
		public FormOutputSettings()
		{
			InitializeComponent();
		}

		private void FormOutputSettings_Load(object sender, System.EventArgs e)
		{
			checkEditShowSpotsPerWeek.ForeColor = checkEditShowSpotsPerWeek.Enabled ? Color.Black : Color.Gray;
			checkEditApplyForAll.ForeColor = checkEditApplyForAll.Enabled ? Color.Black : Color.Gray;
		}
	}
}