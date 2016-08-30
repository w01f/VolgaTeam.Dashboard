using System;
using System.Drawing;
using Asa.Business.Media.Enums;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;

namespace Asa.Media.Controls.PresentationClasses.SettingsControls
{
	public partial class FormFlightDatesChangeWarning : MetroForm
	{
		public bool KeepSpots => buttonXKeepSpots.Checked;

		public FormFlightDatesChangeWarning(SpotType spotType)
		{
			InitializeComponent();
			labelControlTitle.Text = String.Format(labelControlTitle.Text, spotType);
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

				buttonXKeepSpots.Font = new Font(buttonXKeepSpots.Font.FontFamily, buttonXKeepSpots.Font.Size - 2, buttonXKeepSpots.Font.Style);
				buttonXDoNotKeepSpots.Font = new Font(buttonXDoNotKeepSpots.Font.FontFamily, buttonXDoNotKeepSpots.Font.Size - 2, buttonXDoNotKeepSpots.Font.Style);
				buttonXSave.Font = new Font(buttonXSave.Font.FontFamily, buttonXSave.Font.Size - 2, buttonXSave.Font.Style);
				buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
			}
		}

		private void OnKeepSpotsClick(object sender, EventArgs e)
		{
			var button = (ButtonX)sender;
			if(button.Checked) return;
			buttonXKeepSpots.Checked = false;
			buttonXDoNotKeepSpots.Checked = false;
			button.Checked = true;
		}
	}
}
