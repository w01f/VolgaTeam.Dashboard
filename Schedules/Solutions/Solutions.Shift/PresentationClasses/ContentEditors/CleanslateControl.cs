using System;
using System.Collections.Generic;
using System.ComponentModel;
using Asa.Common.Core.Enums;
using Asa.Common.GUI.Preview;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class CleanslateControl : BaseShiftControl
	{
		public override SlideType SlideType => SlideType.ShiftCleanslate;

		public CleanslateControl(BaseShiftContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();
			pictureEditHeader.Image = SlideContainer.ShiftInfo.CleanslateHeaderLogo;
			pictureEditSplash.Image = SlideContainer.ShiftInfo.CleanslateSplashLogo;
		}

		public override void LoadData() { }
		public override void ApplyChanges() { }

		public override Boolean ReadyForOutput => false;

		public override OutputGroup GetOutputGroup()
		{
			return new OutputGroup()
			{
				Name = SlideContainer.ShiftInfo.Titles.Tab0Title,
				IsCurrent = SlideContainer.ActiveSlideContent == this,
				Items = new List<OutputItem>()
			};
		}
	}
}
