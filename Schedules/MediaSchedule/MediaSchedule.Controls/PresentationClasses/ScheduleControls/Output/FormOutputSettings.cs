using System;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Media.Controls.BusinessClasses.Managers;
using DevExpress.Skins;
using DevExpress.XtraLayout.Utils;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Output
{
	public partial class FormOutputSettings : DevComponents.DotNetBar.Metro.MetroForm
	{
		public FormOutputSettings()
		{
			InitializeComponent();
			spinEditOutputLimitPeriods.EnableSelectAll();

			layoutControlGroupContractSettings.Visibility =
				BusinessObjects.Instance.OutputManager.ContractTemplateFolder.ExistsLocal()
					? LayoutVisibility.Always
					: LayoutVisibility.Never;

			layoutControlItemEmptySports.MaxSize = RectangleHelper.ScaleSize(layoutControlItemEmptySports.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemEmptySports.MinSize = RectangleHelper.ScaleSize(layoutControlItemEmptySports.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemUseGenericDates.MaxSize = RectangleHelper.ScaleSize(layoutControlItemUseGenericDates.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemUseGenericDates.MinSize = RectangleHelper.ScaleSize(layoutControlItemUseGenericDates.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOutputLimitPeriods.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOutputLimitPeriods.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOutputLimitPeriods.MinSize = RectangleHelper.ScaleSize(layoutControlItemOutputLimitPeriods.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOutputLimitQuarters.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOutputLimitQuarters.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOutputLimitQuarters.MinSize = RectangleHelper.ScaleSize(layoutControlItemOutputLimitQuarters.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemUseDecimalRate.MaxSize = RectangleHelper.ScaleSize(layoutControlItemUseDecimalRate.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemUseDecimalRate.MinSize = RectangleHelper.ScaleSize(layoutControlItemUseDecimalRate.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOutputNoBrackets.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOutputNoBrackets.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOutputNoBrackets.MinSize = RectangleHelper.ScaleSize(layoutControlItemOutputNoBrackets.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCloneLineToTheEnd.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCloneLineToTheEnd.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCloneLineToTheEnd.MinSize = RectangleHelper.ScaleSize(layoutControlItemCloneLineToTheEnd.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemLockToMaster.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLockToMaster.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemLockToMaster.MinSize = RectangleHelper.ScaleSize(layoutControlItemLockToMaster.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));

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

		private void checkEditShowRatesExpiration_CheckedChanged(object sender, System.EventArgs e)
		{
			dateEditRatesExpirationDate.Enabled = checkEditShowRatesExpiration.Checked;
			if (!checkEditShowRatesExpiration.Checked)
				dateEditRatesExpirationDate.EditValue = null;
		}
	}
}