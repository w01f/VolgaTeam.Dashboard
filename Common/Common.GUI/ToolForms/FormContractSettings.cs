using System.Drawing;

namespace Asa.Common.GUI.ToolForms
{
	public partial class FormContractSettings : DevComponents.DotNetBar.Metro.MetroForm
	{
		public FormContractSettings()
		{
			InitializeComponent();
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

				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 2, laTitle.Font.Style);
				buttonXOK.Font = new Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
				buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
			}
		}

		private void checkEditShowRatesExpiration_CheckedChanged(object sender, System.EventArgs e)
		{
			dateEditRatesExpirationDate.Enabled = checkEditShowRatesExpiration.Checked;
			if (!checkEditShowRatesExpiration.Checked)
				dateEditRatesExpirationDate.EditValue = null;
		}
	}
}