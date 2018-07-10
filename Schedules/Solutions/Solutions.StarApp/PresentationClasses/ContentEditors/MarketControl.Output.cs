using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Preview;
using Asa.Solutions.StarApp.InteropClasses;
using Asa.Solutions.StarApp.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public sealed partial class MarketControl
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
			protected MarketControl OutputControl { get; }
			public abstract StarAppOutputType OutputType { get; }
			public abstract string OutputName { get; }
			public abstract bool ReadyForOutput { get; }

			protected OutputProcessor(MarketControl outputControl)
			{
				OutputControl = outputControl;
			}

			public static IList<OutputProcessor> GetOutputProcessors(MarketControl outputControl)
			{
				return new OutputProcessor[]
				{
					new TabAOutputProcessor(outputControl),
					new TabBOutputProcessor(outputControl),
					new TabCOutputProcessor(outputControl),
				};
			}

			public abstract IList<OutputConfiguration> GetOutputConfigurations();
			public abstract OutputDataPackage GetOutputData();
		}

		internal class TabAOutputProcessor : OutputProcessor
		{
			public override StarAppOutputType OutputType => StarAppOutputType.MarketTabA;
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab7SubATitle;
			public override Boolean ReadyForOutput => true;

			public TabAOutputProcessor(MarketControl outputControl) : base(outputControl) { }

			public override IList<OutputConfiguration> GetOutputConfigurations()
			{
				return new[] { new OutputConfiguration(StarAppOutputType.MarketTabA, OutputName, 1, OutputControl.SlideContainer.ActiveSlideContent == OutputControl && OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 0) };
			}

			public override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var clipart = OutputControl.SlideContainer.EditedContent.MarketState.TabA.Clipart1 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab7SubAClipart1Image);
				if (clipart != null)
					outputDataPackage.ClipartItems.Add("CP07ACLIPART1", clipart);

				var slideHeader = (OutputControl.SlideContainer.EditedContent.MarketState.TabA.SlideHeader ?? OutputControl.SlideContainer.StarInfo.MarketConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault))?.Value;
				var subHeader1 = OutputControl.SlideContainer.EditedContent.MarketState.TabA.Subheader1 ?? OutputControl.SlideContainer.StarInfo.MarketConfiguration.PartASubHeader1DefaultValue;

				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarMarketFile(clipart != null ? "CP07A-1.pptx" : "CP07A-2.pptx");

				outputDataPackage.TextItems.Add("CP07AHEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP07ASubheader1".ToUpper(), subHeader1);

				return outputDataPackage;
			}
		}

		internal class TabBOutputProcessor : OutputProcessor
		{
			public override StarAppOutputType OutputType => StarAppOutputType.MarketTabB;
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab7SubBTitle;
			public override Boolean ReadyForOutput => true;

			public TabBOutputProcessor(MarketControl outputControl) : base(outputControl) { }

			public override IList<OutputConfiguration> GetOutputConfigurations()
			{
				return new[] { new OutputConfiguration(StarAppOutputType.MarketTabB, OutputName, 1, OutputControl.SlideContainer.ActiveSlideContent == OutputControl && OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 1) };
			}

			public override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var clipart1 = OutputControl.SlideContainer.EditedContent.MarketState.TabB.Clipart1 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab7SubBClipart1Image);
				if (clipart1 != null)
					outputDataPackage.ClipartItems.Add("CP07BCLIPART1", clipart1);

				var clipart2 = OutputControl.SlideContainer.EditedContent.MarketState.TabB.Clipart2 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab7SubBClipart2Image);
				if (clipart2 != null)
					outputDataPackage.ClipartItems.Add("CP07BCLIPART2", clipart2);

				var clipart3 = OutputControl.SlideContainer.EditedContent.MarketState.TabB.Clipart3 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab7SubBClipart3Image);
				if (clipart3 != null)
					outputDataPackage.ClipartItems.Add("CP07BCLIPART3", clipart3);

				var clipart4 = OutputControl.SlideContainer.EditedContent.MarketState.TabB.Clipart4 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab7SubBClipart4Image);
				if (clipart4 != null)
					outputDataPackage.ClipartItems.Add("CP07BCLIPART4", clipart4);

				var clipart5 = OutputControl.SlideContainer.EditedContent.MarketState.TabB.Clipart5 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab7SubBClipart5Image);
				if (clipart5 != null)
					outputDataPackage.ClipartItems.Add("CP07BCLIPART5", clipart5);

				var slideHeader = (OutputControl.SlideContainer.EditedContent.MarketState.TabB.SlideHeader ?? OutputControl.SlideContainer.StarInfo.MarketConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault))?.Value;
				var subHeader1 = OutputControl.SlideContainer.EditedContent.MarketState.TabB.Subheader1 ?? OutputControl.SlideContainer.StarInfo.MarketConfiguration.PartBSubHeader1DefaultValue;
				var subHeader2 = OutputControl.SlideContainer.EditedContent.MarketState.TabB.Subheader2 ?? OutputControl.SlideContainer.StarInfo.MarketConfiguration.PartBSubHeader2DefaultValue;

				outputDataPackage.TextItems.Add("CP07BHEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP07BSubHeader1".ToUpper(), subHeader1);
				outputDataPackage.TextItems.Add("CP07BSubHeader2".ToUpper(), subHeader2);

				if (clipart1 != null &&
					clipart2 != null &&
					clipart3 != null &&
					clipart4 != null &&
					clipart5 != null)
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarMarketFile(!String.IsNullOrWhiteSpace(subHeader1) && !String.IsNullOrWhiteSpace(subHeader2) ? "CP07B-1.pptx" : (!String.IsNullOrWhiteSpace(subHeader1) ? "CP07B-6.pptx" : (!String.IsNullOrWhiteSpace(subHeader2) ? "CP07B-11.pptx" : "CP07B-16.pptx")));
				else if (clipart1 != null &&
						 clipart2 != null &&
						 clipart3 != null &&
						 clipart4 != null)
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarMarketFile(!String.IsNullOrWhiteSpace(subHeader1) && !String.IsNullOrWhiteSpace(subHeader2) ? "CP07B-2.pptx" : (!String.IsNullOrWhiteSpace(subHeader1) ? "CP07B-7.pptx" : (!String.IsNullOrWhiteSpace(subHeader2) ? "CP07B-12.pptx" : "CP07B-17.pptx")));
				else if (clipart1 != null &&
						 clipart2 != null &&
						 clipart3 != null)
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarMarketFile(!String.IsNullOrWhiteSpace(subHeader1) && !String.IsNullOrWhiteSpace(subHeader2) ? "CP07B-3.pptx" : (!String.IsNullOrWhiteSpace(subHeader1) ? "CP07B-8.pptx" : (!String.IsNullOrWhiteSpace(subHeader2) ? "CP07B-13.pptx" : "CP07B-18.pptx")));
				else if (clipart1 != null &&
						 clipart2 != null)
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarMarketFile(!String.IsNullOrWhiteSpace(subHeader1) && !String.IsNullOrWhiteSpace(subHeader2) ? "CP07B-4.pptx" : (!String.IsNullOrWhiteSpace(subHeader1) ? "CP07B-9.pptx" : (!String.IsNullOrWhiteSpace(subHeader2) ? "CP07B-14.pptx" : "CP07B-19.pptx")));
				else if (clipart1 != null)
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarMarketFile(!String.IsNullOrWhiteSpace(subHeader1) && !String.IsNullOrWhiteSpace(subHeader2) ? "CP07B-5.pptx" : (!String.IsNullOrWhiteSpace(subHeader1) ? "CP07B-10.pptx" : (!String.IsNullOrWhiteSpace(subHeader2) ? "CP07B-15.pptx" : "CP07B-20.pptx")));

				return outputDataPackage;
			}
		}

		internal class TabCOutputProcessor : OutputProcessor
		{
			public override StarAppOutputType OutputType => StarAppOutputType.MarketTabC;
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab7SubCTitle;
			public override Boolean ReadyForOutput => true;

			public TabCOutputProcessor(MarketControl outputControl) : base(outputControl) { }

			public override IList<OutputConfiguration> GetOutputConfigurations()
			{
				return new[] { new OutputConfiguration(StarAppOutputType.MarketTabC, OutputName, 1, OutputControl.SlideContainer.ActiveSlideContent == OutputControl && OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 2) };
			}

			public override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var clipart1 = OutputControl.SlideContainer.EditedContent.MarketState.TabC.Clipart1 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab7SubCClipart1Image);
				if (clipart1 != null)
					outputDataPackage.ClipartItems.Add("CP07CCLIPART1", clipart1);

				var clipart2 = OutputControl.SlideContainer.EditedContent.MarketState.TabC.Clipart2 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab7SubCClipart2Image);
				if (clipart2 != null)
					outputDataPackage.ClipartItems.Add("CP07CCLIPART2", clipart2);

				var clipart3 = OutputControl.SlideContainer.EditedContent.MarketState.TabC.Clipart3 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab7SubCClipart3Image);
				if (clipart3 != null)
					outputDataPackage.ClipartItems.Add("CP07CCLIPART3", clipart3);

				var clipart4 = OutputControl.SlideContainer.EditedContent.MarketState.TabC.Clipart4 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab7SubCClipart4Image);
				if (clipart4 != null)
					outputDataPackage.ClipartItems.Add("CP07CCLIPART4", clipart4);

				var slideHeader = (OutputControl.SlideContainer.EditedContent.MarketState.TabC.SlideHeader ?? OutputControl.SlideContainer.StarInfo.MarketConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault))?.Value;
				var combo1 = (OutputControl.SlideContainer.EditedContent.MarketState.TabC.Combo1 ?? OutputControl.SlideContainer.StarInfo.MarketConfiguration.PartCCombo1Items.FirstOrDefault(h => h.IsDefault))?.Value;

				outputDataPackage.TextItems.Add("CP07CHEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP07CCombo1".ToUpper(), combo1);

				if (clipart1 != null &&
					clipart2 != null &&
					clipart3 != null &&
					clipart4 != null)
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarMarketFile("CP07C-1.pptx");
				else if (clipart1 != null &&
						 clipart2 != null &&
						 clipart3 != null)
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarMarketFile("CP07C-2.pptx");
				else if (clipart1 != null &&
						 clipart2 != null)
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarMarketFile("CP07C-3.pptx");
				else if (clipart2 != null &&
						 clipart4 != null)
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarMarketFile("CP07C-5.pptx");
				else if (clipart1 != null &&
						 clipart3 != null)
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarMarketFile("CP07C-6.pptx");
				else if (clipart1 != null &&
						 clipart4 != null)
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarMarketFile("CP07C-7.pptx");
				else if (clipart1 != null)
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarMarketFile("CP07C-4.pptx");

				return outputDataPackage;
			}
		}
	}
}
