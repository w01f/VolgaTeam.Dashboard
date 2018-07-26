using System;
using System.ComponentModel;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.Cleanslate;
using Asa.Common.Core.Enums;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class CleanslateControl : BaseShiftControl
	{
		public override SlideType SlideType => SlideType.ShiftCleanslate;

		public CleanslateControl(BaseShiftContainer slideContainer, ShiftTopTabInfo tabInfo) : base(slideContainer, tabInfo)
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
