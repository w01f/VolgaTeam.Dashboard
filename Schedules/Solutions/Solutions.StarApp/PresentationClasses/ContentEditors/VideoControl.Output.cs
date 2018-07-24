using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Preview;
using Asa.Solutions.Common.InteropClasses;
using Asa.Solutions.Common.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public sealed partial class VideoControl
	{
		public override bool ReadyForOutput => _outputProcessors.Any(processor => processor.ReadyForOutput);

		private readonly List<OutputProcessor> _outputProcessors = new List<OutputProcessor>();

		public override OutputGroup GetOutputGroup()
		{
			return new OutputGroup
			{
				Name = SlideContainer.StarInfo.Titles.Tab8Title,
				IsCurrent = SlideContainer.ActiveSlideContent == this,
				Items = _outputProcessors
					.Where(processor => processor.ReadyForOutput)
					.Select(processor => processor.GetOutputItem())
					.Where(outputItem => outputItem != null)
					.ToArray()
			};
		}

		internal abstract class OutputProcessor
		{
			protected VideoControl OutputControl { get; }
			public abstract string OutputName { get; }
			public virtual bool ReadyForOutput => !String.IsNullOrWhiteSpace(OutputName);
			public abstract bool IsCurrent { get; }

			protected OutputProcessor(VideoControl outputControl)
			{
				OutputControl = outputControl;
			}

			public static IList<OutputProcessor> GetOutputProcessors(VideoControl outputControl)
			{
				return new OutputProcessor[]
				{
					new TabAOutputProcessor(outputControl),
					new TabBOutputProcessor(outputControl),
					new TabCOutputProcessor(outputControl),
					new TabDOutputProcessor(outputControl),
				};
			}

			protected abstract OutputDataPackage GetOutputData();

			public virtual OutputItem GetOutputItem()
			{
				var outputData = GetOutputData();
				return new OutputItem
				{
					Name = OutputName,
					PresentationSourcePath = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath,
						Path.GetFileName(Path.GetTempFileName())),
					SlidesCount = 1,
					IsCurrent = IsCurrent,
					SlideGeneratingAction = (processor, destinationPresentation) =>
					{
						processor.AppendStarCommonSlide(outputData, destinationPresentation);
					},
					PreviewGeneratingAction = (processor, presentationSourcePath) =>
					{
						processor.PrepareStarCommonSlide(presentationSourcePath, outputData);
					}
				};
			}
		}

		internal class TabAOutputProcessor : OutputProcessor
		{
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab8SubATitle;
			public override Boolean IsCurrent => OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 0;

			public TabAOutputProcessor(VideoControl outputControl) : base(outputControl) { }

			protected override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var clipart = OutputControl.SlideContainer.EditedContent.VideoState.TabA.Clipart1 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab8SubAClipart1Image);
				if (clipart != null)
					outputDataPackage.ClipartItems.Add("CP08ACLIPART1", clipart);

				var slideHeader = (OutputControl.SlideContainer.EditedContent.VideoState.TabA.SlideHeader ?? OutputControl.SlideContainer.StarInfo.VideoConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault))?.Value;
				var subHeader1 = OutputControl.SlideContainer.EditedContent.VideoState.TabA.Subheader1 ?? OutputControl.SlideContainer.StarInfo.VideoConfiguration.PartASubHeader1DefaultValue;

				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarVideoFile("CP08A-1.pptx");

				outputDataPackage.TextItems.Add("CP08AHEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP08ASubheader1".ToUpper(), subHeader1);

				return outputDataPackage;
			}
		}

		internal class TabBOutputProcessor : OutputProcessor
		{
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab8SubBTitle;
			public override Boolean IsCurrent => OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 1;

			public TabBOutputProcessor(VideoControl outputControl) : base(outputControl) { }

			protected override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var clipart = OutputControl.SlideContainer.EditedContent.VideoState.TabB.Clipart1 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab8SubBClipart1Image);
				if (clipart != null)
					outputDataPackage.ClipartItems.Add("CP08BCLIPART1", clipart);

				var slideHeader = (OutputControl.SlideContainer.EditedContent.VideoState.TabB.SlideHeader ?? OutputControl.SlideContainer.StarInfo.VideoConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault))?.Value;
				var subHeader1 = OutputControl.SlideContainer.EditedContent.VideoState.TabB.Subheader1 ?? OutputControl.SlideContainer.StarInfo.VideoConfiguration.PartBSubHeader1DefaultValue;

				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarVideoFile("CP08B-1.pptx");

				outputDataPackage.TextItems.Add("CP08BHEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP08BSubheader1".ToUpper(), subHeader1);

				return outputDataPackage;
			}
		}

		internal class TabCOutputProcessor : OutputProcessor
		{
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab8SubCTitle;
			public override Boolean IsCurrent => OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 2;

			public TabCOutputProcessor(VideoControl outputControl) : base(outputControl) { }

			protected override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var clipart = OutputControl.SlideContainer.EditedContent.VideoState.TabC.Clipart1 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab8SubCClipart1Image);
				if (clipart != null)
					outputDataPackage.ClipartItems.Add("CP08CCLIPART1", clipart);

				var slideHeader = (OutputControl.SlideContainer.EditedContent.VideoState.TabC.SlideHeader ?? OutputControl.SlideContainer.StarInfo.VideoConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault))?.Value;
				var subHeader1 = OutputControl.SlideContainer.EditedContent.VideoState.TabC.Subheader1 ?? OutputControl.SlideContainer.StarInfo.VideoConfiguration.PartCSubHeader1DefaultValue;

				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarVideoFile(!String.IsNullOrEmpty(subHeader1) ? "CP08C-1.pptx" : "CP08C-2.pptx");

				outputDataPackage.TextItems.Add("CP08CHEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP08CSubheader1".ToUpper(), subHeader1);

				return outputDataPackage;
			}
		}

		internal class TabDOutputProcessor : OutputProcessor
		{
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab8SubDTitle;
			public override Boolean IsCurrent => OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 3;

			public TabDOutputProcessor(VideoControl outputControl) : base(outputControl) { }

			protected override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var clipart = OutputControl.SlideContainer.EditedContent.VideoState.TabD.Clipart1 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab8SubDClipart1Image);
				if (clipart != null)
					outputDataPackage.ClipartItems.Add("CP08DCLIPART1", clipart);

				var slideHeader = (OutputControl.SlideContainer.EditedContent.VideoState.TabD.SlideHeader ?? OutputControl.SlideContainer.StarInfo.VideoConfiguration.HeadersPartDItems.FirstOrDefault(h => h.IsDefault))?.Value;
				var subHeader1 = OutputControl.SlideContainer.EditedContent.VideoState.TabD.Subheader1 ?? OutputControl.SlideContainer.StarInfo.VideoConfiguration.PartDSubHeader1DefaultValue;

				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarVideoFile(!String.IsNullOrEmpty(subHeader1) ? "CP08D-1.pptx" : "CP08D-2.pptx");

				outputDataPackage.TextItems.Add("CP08DHEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP08DSubheader1".ToUpper(), subHeader1);

				return outputDataPackage;
			}
		}
	}
}
