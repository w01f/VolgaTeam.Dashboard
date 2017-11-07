using System;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;

namespace Asa.Media.Controls.PresentationClasses.SettingsControls
{
	public partial class FormFlightDatesChangeWarning : MetroForm
	{
		public bool KeepSpots => buttonXKeepSpots.Checked;

		public FormFlightDatesChangeWarning(SpotType spotType)
		{
			InitializeComponent();
			simpleLabelItemTitle.Text = String.Format(simpleLabelItemTitle.Text, spotType);

			layoutControlItemKeepSpots.MaxSize = RectangleHelper.ScaleSize(layoutControlItemKeepSpots.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemKeepSpots.MinSize = RectangleHelper.ScaleSize(layoutControlItemKeepSpots.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemDoNotKeepSpots.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDoNotKeepSpots.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemDoNotKeepSpots.MinSize = RectangleHelper.ScaleSize(layoutControlItemDoNotKeepSpots.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
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
