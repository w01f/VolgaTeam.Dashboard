using System.Windows.Forms;
using NewBizWiz.Calendar.Controls.InteropClasses;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Calendar.Controls.ToolForms
{
	public partial class FormSlideOutput : Form
	{
		public FormSlideOutput()
		{
			InitializeComponent();
		}

		private void FormSlideOutput_Shown(object sender, System.EventArgs e)
		{
			Utilities.Instance.ActivatePowerPoint(CalendarPowerPointHelper.Instance.PowerPointObject);
			Utilities.Instance.ActivateMiniBar();
			Utilities.Instance.ActivateForm(Handle, false, true);
		}
	}
}
