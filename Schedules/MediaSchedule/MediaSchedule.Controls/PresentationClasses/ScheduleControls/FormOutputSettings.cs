using System;
using Asa.CommonGUI.Common;

namespace Asa.MediaSchedule.Controls.PresentationClasses.ScheduleControls
{
	public partial class FormOutputSettings : DevComponents.DotNetBar.Metro.MetroForm
	{
		public FormOutputSettings()
		{
			InitializeComponent();
			spinEditOutputLimitPeriods.Enter += TextEditorsHelper.Editor_Enter;
			spinEditOutputLimitPeriods.MouseDown += TextEditorsHelper.Editor_MouseDown;
			spinEditOutputLimitPeriods.MouseUp += TextEditorsHelper.Editor_MouseUp;
		}

		private void checkEditOutputLimitQuarters_CheckedChanged(object sender, EventArgs e)
		{
			if (checkEditOutputLimitQuarters.Checked)
				checkEditOutputLimitPeriods.Checked = false;
		}

		private void checkEditOutputLimitPeriods_CheckedChanged(object sender, EventArgs e)
		{
			spinEditOutputLimitPeriods.Enabled = checkEditOutputLimitPeriods.Checked;
			if (!checkEditOutputLimitPeriods.Checked)
				spinEditOutputLimitPeriods.EditValue = null;
			else
				checkEditOutputLimitQuarters.Checked = false;
		}
	}
}