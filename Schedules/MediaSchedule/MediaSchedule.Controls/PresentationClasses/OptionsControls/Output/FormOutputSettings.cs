using Asa.Common.Core.Helpers;
using Asa.Media.Controls.BusinessClasses.Managers;
using DevExpress.Skins;
using DevExpress.XtraLayout.Utils;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.Output
{
	public partial class FormOutputSettings : DevComponents.DotNetBar.Metro.MetroForm
	{
		public FormOutputSettings()
		{
			InitializeComponent();

			layoutControlGroupContractSettings.Visibility =
				BusinessObjects.Instance.OutputManager.ContractTemplateFolder.ExistsLocal()
					? LayoutVisibility.Always
					: LayoutVisibility.Never;

			layoutControlItemUseDecimalRate.MaxSize = RectangleHelper.ScaleSize(layoutControlItemUseDecimalRate.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemUseDecimalRate.MinSize = RectangleHelper.ScaleSize(layoutControlItemUseDecimalRate.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemShowSpotX.MaxSize = RectangleHelper.ScaleSize(layoutControlItemShowSpotX.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemShowSpotX.MinSize = RectangleHelper.ScaleSize(layoutControlItemShowSpotX.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCloneLineToTheEnd.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCloneLineToTheEnd.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCloneLineToTheEnd.MinSize = RectangleHelper.ScaleSize(layoutControlItemCloneLineToTheEnd.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemLockToMaster.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLockToMaster.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemLockToMaster.MinSize = RectangleHelper.ScaleSize(layoutControlItemLockToMaster.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void checkEditShowRatesExpiration_CheckedChanged(object sender, System.EventArgs e)
		{
			dateEditRatesExpirationDate.Enabled = checkEditShowRatesExpiration.Checked;
			if (!checkEditShowRatesExpiration.Checked)
				dateEditRatesExpirationDate.EditValue = null;
		}
	}
}