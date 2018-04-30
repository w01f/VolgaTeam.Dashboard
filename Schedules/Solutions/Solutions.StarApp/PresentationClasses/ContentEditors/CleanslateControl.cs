using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Asa.Common.Core.Enums;
using Asa.Common.GUI.Preview;
using Asa.Solutions.StarApp.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class CleanslateControl : StarAppControl
	{
		public override SlideType SlideType => SlideType.StarAppCleanslate;
		public override string OutputName => SlideContainer.StarInfo.Titles.Tab0Title;

		public CleanslateControl(BaseStarAppContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();
			pictureEditHeader.Image = SlideContainer.StarInfo.CleanslateHeaderLogo;
			pictureEditSplash.Image = SlideContainer.StarInfo.CleanslateSplashLogo;
		}

		public override void LoadData(){}
		public override void ApplyChanges(){}

		public override Boolean ReadyForOutput => false;

		public override OutputGroup GetOutputGroup()
		{
			var outputConfigurations = new List<OutputConfiguration>();

			return new OutputGroup(this)
			{
				DisplayName = OutputName,
				IsCurrent = SlideContainer.ActiveSlideContent == this,
				Configurations = outputConfigurations.ToArray()
			};
		}

		public override void GenerateOutput(IList<OutputConfiguration> configurations) { }

		public override IList<PreviewGroup> GeneratePreview(IList<OutputConfiguration> configurations)
		{
			var tempFileName = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath,
				Path.GetFileName(Path.GetTempFileName()));
			//SolutionDashboardPowerPointHelper.Instance.PrepareCover(this, tempFileName);
			return new[] { new PreviewGroup { Name = OutputName, PresentationSourcePath = tempFileName } };
		}
	}
}
