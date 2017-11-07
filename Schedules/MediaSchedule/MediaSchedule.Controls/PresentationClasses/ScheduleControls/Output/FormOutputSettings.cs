using System;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using DevExpress.Skins;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Output
{
	public partial class FormOutputSettings : DevComponents.DotNetBar.Metro.MetroForm
	{
		public FormOutputSettings()
		{
			InitializeComponent();
			spinEditOutputLimitPeriods.EnableSelectAll();

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void checkEditOutputLimitQuarters_CheckedChanged(object sender, EventArgs e)
		{
			if (checkEditOutputLimitQuarters.Checked)
				checkEditOutputLimitPeriods.Checked = false;
		}

		private void checkEditOutputLimitPeriods_CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemOutputLimitPeriodsValue.Enabled = checkEditOutputLimitPeriods.Checked;
			if (!checkEditOutputLimitPeriods.Checked)
				spinEditOutputLimitPeriods.EditValue = null;
			else
				checkEditOutputLimitQuarters.Checked = false;
		}
	}
}