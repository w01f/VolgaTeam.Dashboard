using System;
using System.ComponentModel;
using Asa.Common.Core.Enums;
using Asa.Common.GUI.Preview;
using Asa.Solutions.StarApp.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class CleanslateControl : StarAppControl, IStarAppSlide
	{
		public override SlideType SlideType => SlideType.StarAppCleanslate;
		public override string SlideName => SlideContainer.StarInfo.Titles.Tab0Title;

		public CleanslateControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();
			pictureEditHeader.Image = SlideContainer.StarInfo.CleanslateHeaderLogo;
			pictureEditSplash.Image = SlideContainer.StarInfo.CleanslateSplashLogo;
		}

		public override void LoadData(){}

		public override void ApplyChanges(){}

		public override void GenerateOutput() {}

		public override PreviewGroup GeneratePreview()
		{
			throw new NotImplementedException();
		}
	}
}
