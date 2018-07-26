using System;
using System.ComponentModel;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Configuration.Cleanslate;
using Asa.Common.Core.Enums;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class CleanslateControl : StarAppControl
	{
		public override SlideType SlideType => SlideType.StarAppCleanslate;

		public CleanslateControl(BaseStarAppContainer slideContainer, StarTopTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			var cleanslateTabInfo = tabInfo as CleanslateTabInfo;

			pictureEditHeader.Image = cleanslateTabInfo?.HeaderLogo;
			pictureEditSplash.Image = cleanslateTabInfo?.SplashLogo;
		}

		public override void LoadData() { }
		public override void ApplyChanges() { }

		public override Boolean ReadyForOutput => false;
	}
}
