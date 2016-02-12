using System.Drawing;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls
{
	public partial class FormOutputSettings : DevComponents.DotNetBar.Metro.MetroForm
	{
		public FormOutputSettings()
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

				font = new Font(labelControlDescriptionLockToMaster.Font.FontFamily, labelControlDescriptionLockToMaster.Font.Size - 1,
					labelControlDescriptionLockToMaster.Font.Style);
				labelControlDescriptionLockToMaster.Font = font;
				labelControlDescriptionUseDecimalRate.Font = font;
				labelControlDescriptionShowSpotX.Font = font;
				labelControlDescriptionShowSpotsPerWeek.Font = font;

				buttonXOK.Font = new Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
				buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
			}
		}

		private void FormOutputSettings_Load(object sender, System.EventArgs e)
		{
			checkEditShowSpotsPerWeek.ForeColor = checkEditShowSpotsPerWeek.Enabled ? Color.Black : Color.Gray;
		}
	}
}