using System;
using System.ComponentModel;
using System.Windows.Forms;
using Asa.Common.Core.Enums;

namespace Asa.Solutions.Dashboard.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public partial class CleanslateControl : DashboardSlideControl
	{
		public override SlideType SlideType => SlideType.Cleanslate;

		public CleanslateControl(BaseDashboardContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();
			Text = SlideContainer.DashboardInfo.Title;
			pictureEditHeader.Image = SlideContainer.DashboardInfo.CleanslateHeaderLogo;
			pictureEditSplash.Image = SlideContainer.DashboardInfo.CleanslateSplashLogo;
		}

		public override void LoadData() { }

		public override void ApplyChanges() { }

		public override Boolean ReadyForOutput => true;
	}
}