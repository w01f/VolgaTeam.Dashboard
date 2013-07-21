using System.Windows.Forms;

namespace CalendarBuilder.ToolForms
{
	public partial class FormSlideOutput : Form
	{
		public FormSlideOutput()
		{
			InitializeComponent();
		}

		private void FormSlideOutput_Shown(object sender, System.EventArgs e)
		{
			AppManager.ActivatePowerPoint();
			AppManager.ActivateMiniBar();
			AppManager.ActivateForm(Handle, false, true);
		}
	}
}
