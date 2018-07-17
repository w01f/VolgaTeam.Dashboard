using System;
using System.Collections.Generic;
using System.ComponentModel;
using Asa.Common.Core.Enums;
using Asa.Common.GUI.Preview;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class CleanslateControl : StarAppControl
	{
		public override SlideType SlideType => SlideType.StarAppCleanslate;

		public CleanslateControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();
			pictureEditHeader.Image = SlideContainer.StarInfo.CleanslateHeaderLogo;
			pictureEditSplash.Image = SlideContainer.StarInfo.CleanslateSplashLogo;
		}

		public override void LoadData() { }
		public override void ApplyChanges() { }

		public override Boolean ReadyForOutput => false;

		public override OutputGroup GetOutputGroup()
		{
			return new OutputGroup()
			{
				Name = SlideContainer.StarInfo.Titles.Tab0Title,
				IsCurrent = SlideContainer.ActiveSlideContent == this,
				Items = new List<OutputItem>()
			};
		}
	}
}
