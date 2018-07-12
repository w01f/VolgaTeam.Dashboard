using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Asa.Common.Core.Enums;
using Asa.Common.GUI.Preview;
using Asa.Solutions.Shift.PresentationClasses.Output;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class CleanslateControl : BaseShiftControl
	{
		public override SlideType SlideType => SlideType.ShiftCleanslate;
		public override string OutputName => SlideContainer.ShiftInfo.Titles.Tab0Title;

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
