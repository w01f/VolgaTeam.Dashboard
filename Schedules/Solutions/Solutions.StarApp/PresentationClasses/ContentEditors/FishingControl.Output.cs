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
	public sealed partial class FishingControl
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
			protected FishingControl OutputControl { get; }
			public abstract StarAppOutputType OutputType { get; }
			public abstract string OutputName { get; }
			public abstract bool ReadyForOutput { get; }

			protected OutputProcessor(FishingControl outputControl)
			{
				OutputControl = outputControl;
			}

			public static IList<OutputProcessor> GetOutputProcessors(FishingControl outputControl)
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
			public override StarAppOutputType OutputType => StarAppOutputType.FishingTabA;
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab3SubATitle;
			public override Boolean ReadyForOutput => true;

			public TabAOutputProcessor(FishingControl outputControl) : base(outputControl) { }

			public override IList<OutputConfiguration> GetOutputConfigurations()
			{
				return new[] { new OutputConfiguration(StarAppOutputType.FishingTabA, OutputName, 1, OutputControl.SlideContainer.ActiveSlideContent == OutputControl && OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 0) };
			}

			public override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var clipart = OutputControl.SlideContainer.EditedContent.FishingState.TabA.Clipart1 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab3SubAClipart1Image);
				if (clipart != null)
					outputDataPackage.ClipartItems.Add("CP03ACLIPART1", clipart);

				var slideHeader = (OutputControl.SlideContainer.EditedContent.FishingState.TabA.SlideHeader ?? OutputControl.SlideContainer.StarInfo.FishingConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault))?.Value;
				var subHeaders = new[]
					{
						OutputControl.SlideContainer.EditedContent.FishingState.TabA.Subheader1 ?? OutputControl.SlideContainer.StarInfo.FishingConfiguration.PartASubHeader1DefaultValue,
						OutputControl.SlideContainer.EditedContent.FishingState.TabA.Subheader2 ?? OutputControl.SlideContainer.StarInfo.FishingConfiguration.PartASubHeader2DefaultValue
					}
					.Where(item => !String.IsNullOrWhiteSpace(item))
					.ToList();

				switch (subHeaders.Count)
				{
					case 1:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarFishingFile("CP03A-1.pptx");
						break;
					case 2:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarFishingFile("CP03A-2.pptx");
						break;
					default:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarFishingFile("CP03A-1.pptx");
						break;
				}

				outputDataPackage.TextItems.Add("CP03AHEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP03ASubheader1".ToUpper(), subHeaders.ElementAtOrDefault(0));
				outputDataPackage.TextItems.Add("CP03ASubheader2".ToUpper(), subHeaders.ElementAtOrDefault(1));

				return outputDataPackage;
			}
		}

		internal class TabBOutputProcessor : OutputProcessor
		{
			public override StarAppOutputType OutputType => StarAppOutputType.FishingTabB;
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab3SubBTitle;
			public override Boolean ReadyForOutput => true;

			public TabBOutputProcessor(FishingControl outputControl) : base(outputControl) { }

			public override IList<OutputConfiguration> GetOutputConfigurations()
			{
				return new[] { new OutputConfiguration(StarAppOutputType.FishingTabB, OutputName, 1, OutputControl.SlideContainer.ActiveSlideContent == OutputControl && OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 1) };
			}

			public override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var clipart1 = OutputControl.SlideContainer.EditedContent.FishingState.TabB.Clipart1 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab3SubBClipart1Image);
				if (clipart1 != null)
					outputDataPackage.ClipartItems.Add("CP03BCLIPART1", clipart1);


				var clipart2 = OutputControl.SlideContainer.EditedContent.FishingState.TabB.Clipart2 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab3SubBClipart2Image);
				if (clipart2 != null)
					outputDataPackage.ClipartItems.Add("CP03BCLIPART2", clipart2);

				var slideHeader = (OutputControl.SlideContainer.EditedContent.FishingState.TabB.SlideHeader ?? OutputControl.SlideContainer.StarInfo.FishingConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault))?.Value;
				var combos = new[]
					{
						(OutputControl.SlideContainer.EditedContent.FishingState.TabB.Combo1 ?? OutputControl.SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(0))?.Value,
						(OutputControl.SlideContainer.EditedContent.FishingState.TabB.Combo2 ?? OutputControl.SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(1))?.Value,
						(OutputControl.SlideContainer.EditedContent.FishingState.TabB.Combo3 ?? OutputControl.SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(2))?.Value,
						(OutputControl.SlideContainer.EditedContent.FishingState.TabB.Combo4 ?? OutputControl.SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(3))?.Value,
					}
					.Where(item => !String.IsNullOrWhiteSpace(item))
					.ToList();

				switch (combos.Count)
				{
					case 1:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarFishingFile(clipart1 != null && clipart2 != null ? "CP03B-1.pptx" : (clipart1 != null ? "CP03B-5.pptx" : "CP03B-12.pptx"));
						break;
					case 2:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarFishingFile(clipart1 != null && clipart2 != null ? "CP03B-2.pptx" : (clipart1 != null ? "CP03B-6.pptx" : "CP03B-11.pptx"));
						break;
					case 3:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarFishingFile(clipart1 != null && clipart2 != null ? "CP03B-3.pptx" : (clipart1 != null ? "CP03B-7.pptx" : "CP03B-10.pptx"));
						break;
					case 4:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarFishingFile(clipart1 != null && clipart2 != null ? "CP03B-4.pptx" : (clipart1 != null ? "CP03B-8.pptx" : "CP03B-9.pptx"));
						break;
					default:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarFishingFile(clipart1 != null && clipart2 != null ? "CP03B-1.pptx" : (clipart1 != null ? "CP03B-5.pptx" : "CP03B-12.pptx"));
						break;
				}

				outputDataPackage.TextItems.Add("CP03BHEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP03BCombo1".ToUpper(), combos.ElementAtOrDefault(0));
				outputDataPackage.TextItems.Add("CP03BCombo2".ToUpper(), combos.ElementAtOrDefault(1));
				outputDataPackage.TextItems.Add("CP03BCombo3".ToUpper(), combos.ElementAtOrDefault(2));
				outputDataPackage.TextItems.Add("CP03BCombo4".ToUpper(), combos.ElementAtOrDefault(3));

				return outputDataPackage;
			}
		}

		internal class TabCOutputProcessor : OutputProcessor
		{
			public override StarAppOutputType OutputType => StarAppOutputType.FishingTabC;
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab3SubCTitle;
			public override Boolean ReadyForOutput => true;

			public TabCOutputProcessor(FishingControl outputControl) : base(outputControl) { }

			public override IList<OutputConfiguration> GetOutputConfigurations()
			{
				return new[] { new OutputConfiguration(StarAppOutputType.FishingTabC, OutputName, 1, OutputControl.SlideContainer.ActiveSlideContent == OutputControl && OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 2) };
			}

			public override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var slideHeader = (OutputControl.SlideContainer.EditedContent.FishingState.TabC.SlideHeader ?? OutputControl.SlideContainer.StarInfo.FishingConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault))?.Value;
				var subHeaders = new[]
					{
						OutputControl.SlideContainer.EditedContent.FishingState.TabC.Subheader1 ?? OutputControl.SlideContainer.StarInfo.FishingConfiguration.PartCSubHeader1DefaultValue,
						OutputControl.SlideContainer.EditedContent.FishingState.TabC.Subheader2 ?? OutputControl.SlideContainer.StarInfo.FishingConfiguration.PartCSubHeader2DefaultValue,
						OutputControl.SlideContainer.EditedContent.FishingState.TabC.Subheader3 ?? OutputControl.SlideContainer.StarInfo.FishingConfiguration.PartCSubHeader3DefaultValue
					}
					.Where(item => !String.IsNullOrWhiteSpace(item))
					.ToList();

				switch (subHeaders.Count)
				{
					case 1:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarFishingFile("CP03C-1.pptx");
						break;
					case 2:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarFishingFile("CP03C-2.pptx");
						break;
					case 3:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarFishingFile("CP03C-3.pptx");
						break;
					default:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarFishingFile("CP03C-1.pptx");
						break;
				}

				outputDataPackage.TextItems.Add("CP03CHEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP03CSubheader1".ToUpper(), subHeaders.ElementAtOrDefault(0));
				outputDataPackage.TextItems.Add("CP03CSubheader2".ToUpper(), subHeaders.ElementAtOrDefault(1));
				outputDataPackage.TextItems.Add("CP03CSubheader3".ToUpper(), subHeaders.ElementAtOrDefault(2));

				return outputDataPackage;
			}
		}
	}
}
