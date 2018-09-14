using System;
using System.ComponentModel;
using System.Windows.Forms;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using DevExpress.Skins;

namespace Asa.Solutions.Dashboard.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public partial class CleanslateControl : DashboardSlideControl
	{
		public override SlideType SlideType => SlideType.Cleanslate;

		public CleanslateControl(BaseDashboardContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();
			Text = SlideContainer.DashboardInfo.CleanslateTitle;
			pictureEditHeader.Image = SlideContainer.DashboardInfo.GraphicResources?.CleanslateHeaderLogo;
			pictureEditSplash.Image = SlideContainer.DashboardInfo.GraphicResources?.CleanslateSplashLogo;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControl.MaximumSize = RectangleHelper.ScaleSize(layoutControl.MaximumSize, scaleFactor);
			layoutControl.MinimumSize = RectangleHelper.ScaleSize(layoutControl.MinimumSize, scaleFactor);
		}

		public override void LoadData() { }

		public override void ApplyChanges() { }

		public override Boolean ReadyForOutput => true;
	}
}