using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Preview;
using Asa.Solutions.StarApp.InteropClasses;
using Asa.Solutions.StarApp.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public sealed partial class VideoControl
	{
		public override bool ReadyForOutput => true;

		private readonly List<OutputProcessor> _outputProcessors = new List<OutputProcessor>();

		public override OutputGroup GetOutputGroup()
		{
			return new OutputGroup(this)
			{
				DisplayName = OutputName,
				IsCurrent = SlideContainer.ActiveSlideContent == this,
				Configurations = _outputProcessors
					.Where(processor => processor.ReadyForOutput)
					.SelectMany(processor => processor.GetOutputConfigurations())
					.ToArray()
			};
		}

		public override void GenerateOutput(IList<OutputConfiguration> configurations)
		{
			foreach (var configuration in configurations)
			{
				var outputProcessor = _outputProcessors.First(processor => processor.OutputType == configuration.OutputType);
				SlideContainer.PowerPointProcessor.AppendStarCommonSlide(outputProcessor.GetOutputData());
			}
		}

		public override IList<PreviewGroup> GeneratePreview(IList<OutputConfiguration> configurations)
		{
			var previewGroups = new List<PreviewGroup>();

			foreach (var configuration in configurations)
			{
				var outputProcessor = _outputProcessors.First(processor => processor.OutputType == configuration.OutputType);
				var tempFileName = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath,
					Path.GetFileName(Path.GetTempFileName()));
				SlideContainer.PowerPointProcessor.PrepareStarCommonSlide(outputProcessor.GetOutputData(), tempFileName);
				previewGroups.Add(new PreviewGroup { Name = outputProcessor.OutputName, PresentationSourcePath = tempFileName });
			}

			return previewGroups;
		}

		internal abstract class OutputProcessor
		{
			protected VideoControl OutputControl { get; }
			public abstract StarAppOutputType OutputType { get; }
			public abstract string OutputName { get; }
			public abstract bool ReadyForOutput { get; }

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

			public abstract IList<OutputConfiguration> GetOutputConfigurations();
			public abstract OutputDataPackage GetOutputData();
		}

		internal class TabAOutputProcessor : OutputProcessor
		{
			public override StarAppOutputType OutputType => StarAppOutputType.VideoTabA;
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab8SubATitle;
			public override Boolean ReadyForOutput => true;

			public TabAOutputProcessor(VideoControl outputControl) : base(outputControl) { }

			public override IList<OutputConfiguration> GetOutputConfigurations()
			{
				return new[] { new OutputConfiguration(StarAppOutputType.VideoTabA, OutputName, 1, OutputControl.SlideContainer.ActiveSlideContent == OutputControl && OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 0) };
			}

			public override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var clipart = OutputControl.SlideContainer.EditedContent.VideoState.TabA.Clipart1 ?? OutputControl.SlideContainer.StarInfo.Tab8SubAClipart1Image;
				if (clipart != null)
				{
					var fileName = Path.GetTempFileName();
					clipart.Save(fileName);
					outputDataPackage.ClipartItems.Add("CP08ACLIPART1", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart.Width, clipart.Height) });
				}

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
			public override StarAppOutputType OutputType => StarAppOutputType.VideoTabB;
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab8SubBTitle;
			public override Boolean ReadyForOutput => true;

			public TabBOutputProcessor(VideoControl outputControl) : base(outputControl) { }

			public override IList<OutputConfiguration> GetOutputConfigurations()
			{
				return new[] { new OutputConfiguration(StarAppOutputType.VideoTabB, OutputName, 1, OutputControl.SlideContainer.ActiveSlideContent == OutputControl && OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 1) };
			}

			public override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var clipart = OutputControl.SlideContainer.EditedContent.VideoState.TabB.Clipart1 ?? OutputControl.SlideContainer.StarInfo.Tab8SubBClipart1Image;
				if (clipart != null)
				{
					var fileName = Path.GetTempFileName();
					clipart.Save(fileName);
					outputDataPackage.ClipartItems.Add("CP08BCLIPART1", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart.Width, clipart.Height) });
				}

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
			public override StarAppOutputType OutputType => StarAppOutputType.VideoTabC;
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab8SubCTitle;
			public override Boolean ReadyForOutput => true;

			public TabCOutputProcessor(VideoControl outputControl) : base(outputControl) { }

			public override IList<OutputConfiguration> GetOutputConfigurations()
			{
				return new[] { new OutputConfiguration(StarAppOutputType.VideoTabC, OutputName, 1, OutputControl.SlideContainer.ActiveSlideContent == OutputControl && OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 2) };
			}

			public override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var clipart = OutputControl.SlideContainer.EditedContent.VideoState.TabC.Clipart1 ?? OutputControl.SlideContainer.StarInfo.Tab8SubCClipart1Image;
				if (clipart != null)
				{
					var fileName = Path.GetTempFileName();
					clipart.Save(fileName);
					outputDataPackage.ClipartItems.Add("CP08CCLIPART1", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart.Width, clipart.Height) });
				}

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
			public override StarAppOutputType OutputType => StarAppOutputType.VideoTabD;
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab8SubDTitle;
			public override Boolean ReadyForOutput => true;

			public TabDOutputProcessor(VideoControl outputControl) : base(outputControl) { }

			public override IList<OutputConfiguration> GetOutputConfigurations()
			{
				return new[] { new OutputConfiguration(StarAppOutputType.VideoTabD, OutputName, 1, OutputControl.SlideContainer.ActiveSlideContent == OutputControl && OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 3) };
			}

			public override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var clipart = OutputControl.SlideContainer.EditedContent.VideoState.TabD.Clipart1 ?? OutputControl.SlideContainer.StarInfo.Tab8SubDClipart1Image;
				if (clipart != null)
				{
					var fileName = Path.GetTempFileName();
					clipart.Save(fileName);
					outputDataPackage.ClipartItems.Add("CP08DCLIPART1", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart.Width, clipart.Height) });
				}

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
