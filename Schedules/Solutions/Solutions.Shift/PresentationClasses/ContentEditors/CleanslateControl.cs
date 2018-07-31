using System;
using System.ComponentModel;
using System.Windows.Forms;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.Cleanslate;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class CleanslateControl : BaseShiftControl
	{
		public CleanslateControl(BaseShiftContainer slideContainer, ShiftTopTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();
		}

		public override void InitControls()
		{
			var cleanslateTabInfo = TabInfo as CleanslateTabInfo;
			pictureEditHeader.Image = cleanslateTabInfo?.HeaderLogo;
			pictureEditSplash.Image = cleanslateTabInfo?.SplashLogo;

			if (cleanslateTabInfo?.BackgroundLogo != null)
			{
				BackgroundImage = cleanslateTabInfo.BackgroundLogo;
				BackgroundImageLayout = ImageLayout.Stretch;
			}
		}

		public override void LoadData() { }
		public override void ApplyChanges() { }

		public override Boolean ReadyForOutput => false;
	}
}
