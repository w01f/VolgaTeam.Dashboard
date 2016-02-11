using System;
using System.Drawing;
using Asa.Common.GUI.Common;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls
{
	public partial class FormOutputSettings : DevComponents.DotNetBar.Metro.MetroForm
	{
		public FormOutputSettings()
		{
			InitializeComponent();
			spinEditOutputLimitPeriods.Enter += TextEditorsHelper.Editor_Enter;
			spinEditOutputLimitPeriods.MouseDown += TextEditorsHelper.Editor_MouseDown;
			spinEditOutputLimitPeriods.MouseUp += TextEditorsHelper.Editor_MouseUp;
			if ((CreateGraphics()).DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;

				font = new Font(labelControlDescriptionApplyForAll.Font.FontFamily, labelControlDescriptionApplyForAll.Font.Size - 1,
					labelControlDescriptionApplyForAll.Font.Style);
				labelControlDescriptionApplyForAll.Font = font;
				labelControlDescriptionEmptySports.Font = font;
				labelControlDescriptionLockToMaster.Font = font;
				labelControlDescriptionOutputLimitPeriods.Font = font;
				labelControlDescriptionOutputLimitQuarters.Font = font;
				labelControlDescriptionOutputNoBrackets.Font = font;
				labelControlDescriptionUseDecimalRate.Font = font;
				labelControlDescriptionUseGenericDates.Font = font;

				buttonXOK.Font = new Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
				buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
			}
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