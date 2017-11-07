using Asa.Common.Core.Helpers;
using DevExpress.Skins;

namespace Asa.Common.GUI.ToolForms
{
	public partial class FormContractSettings : DevComponents.DotNetBar.Metro.MetroForm
	{
		public FormContractSettings()
		{
			InitializeComponent();

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