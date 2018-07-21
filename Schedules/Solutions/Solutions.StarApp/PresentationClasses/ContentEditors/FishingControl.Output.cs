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
	public sealed partial class FishingControl
	{
		public override bool ReadyForOutput => _outputProcessors.Any(processor => processor.ReadyForOutput);

		private readonly List<OutputProcessor> _outputProcessors = new List<OutputProcessor>();

		public override OutputGroup GetOutputGroup()
		{
			return new OutputGroup
			{
				Name = SlideContainer.StarInfo.Titles.Tab3Title,
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
			protected FishingControl OutputControl { get; }
			public abstract string OutputName { get; }
			public virtual bool ReadyForOutput => !String.IsNullOrWhiteSpace(OutputName);
			public abstract bool IsCurrent { get; }

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
					new TabUOutputProcessor(outputControl),
					new TabVOutputProcessor(outputControl),
					new TabWOutputProcessor(outputControl),
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
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab3SubATitle;
			public override Boolean IsCurrent => OutputControl.tabbedControlGroupData.SelectedTabPage == OutputControl.layoutControlGroupTabA;

			public TabAOutputProcessor(FishingControl outputControl) : base(outputControl) { }

			protected override OutputDataPackage GetOutputData()
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
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab3SubBTitle;
			public override Boolean IsCurrent => OutputControl.tabbedControlGroupData.SelectedTabPage == OutputControl.layoutControlGroupTabB;

			public TabBOutputProcessor(FishingControl outputControl) : base(outputControl) { }

			protected override OutputDataPackage GetOutputData()
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
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab3SubCTitle;
			public override Boolean IsCurrent => OutputControl.tabbedControlGroupData.SelectedTabPage == OutputControl.layoutControlGroupTabC;

			public TabCOutputProcessor(FishingControl outputControl) : base(outputControl) { }

			protected override OutputDataPackage GetOutputData()
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

		internal class TabUOutputProcessor : OutputProcessor
		{
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab3SubUTitle;
			public override Boolean IsCurrent => OutputControl.tabbedControlGroupData.SelectedTabPage == OutputControl.layoutControlGroupTabU;

			public TabUOutputProcessor(FishingControl outputControl) : base(outputControl) { }

			protected override OutputDataPackage GetOutputData()
			{
				return new OutputDataPackage();
			}

			public override OutputItem GetOutputItem()
			{
				var slideObject = OutputControl.SlideContainer.EditedContent.FishingState.TabU.Slide;

				if (slideObject.SourceSlideMasters.ContainsKey(SlideSettingsManager.Instance.SlideSettings.Format))
				{
					var slideMasterName = slideObject.SourceSlideMasters[SlideSettingsManager.Instance.SlideSettings.Format];
					var targetSlideMaster = OutputControl.SlideContainer.StarInfo.FishingConfiguration.PartUSlides.Slides
						.FirstOrDefault(slideMaster =>
							String.Equals(slideMaster.Name, slideMasterName, StringComparison.OrdinalIgnoreCase));

					if (targetSlideMaster != null)
					{
						return new OutputItem
						{
							Name = OutputName,
							PresentationSourcePath = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath,
								Path.GetFileName(Path.GetTempFileName())),
							SlidesCount = 1,
							IsCurrent = IsCurrent,
							SlideGeneratingAction = (processor, destinationPresentation) =>
							{
								processor.AppendSlideMaster(targetSlideMaster.GetMasterPath(), destinationPresentation);
							},
							PreviewGeneratingAction = (processor, presentationSourcePath) =>
							{
								processor.PreparePresentation(presentationSourcePath,
									presentation => processor.AppendSlideMaster(targetSlideMaster.GetMasterPath(), presentation));
							}
						};
					}
				}
				return null;
			}
		}

		internal class TabVOutputProcessor : OutputProcessor
		{
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab3SubVTitle;
			public override Boolean IsCurrent => OutputControl.tabbedControlGroupData.SelectedTabPage == OutputControl.layoutControlGroupTabV;

			public TabVOutputProcessor(FishingControl outputControl) : base(outputControl) { }

			protected override OutputDataPackage GetOutputData()
			{
				return new OutputDataPackage();
			}

			public override OutputItem GetOutputItem()
			{
				var slideObject = OutputControl.SlideContainer.EditedContent.FishingState.TabV.Slide;

				if (slideObject.SourceSlideMasters.ContainsKey(SlideSettingsManager.Instance.SlideSettings.Format))
				{
					var slideMasterName = slideObject.SourceSlideMasters[SlideSettingsManager.Instance.SlideSettings.Format];
					var targetSlideMaster = OutputControl.SlideContainer.StarInfo.FishingConfiguration.PartVSlides.Slides
						.FirstOrDefault(slideMaster =>
							String.Equals(slideMaster.Name, slideMasterName, StringComparison.OrdinalIgnoreCase));

					if (targetSlideMaster != null)
					{
						return new OutputItem
						{
							Name = OutputName,
							PresentationSourcePath = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath,
								Path.GetFileName(Path.GetTempFileName())),
							SlidesCount = 1,
							IsCurrent = IsCurrent,
							SlideGeneratingAction = (processor, destinationPresentation) =>
							{
								processor.AppendSlideMaster(targetSlideMaster.GetMasterPath(), destinationPresentation);
							},
							PreviewGeneratingAction = (processor, presentationSourcePath) =>
							{
								processor.PreparePresentation(presentationSourcePath,
									presentation => processor.AppendSlideMaster(targetSlideMaster.GetMasterPath(), presentation));
							}
						};
					}
				}
				return null;
			}
		}

		internal class TabWOutputProcessor : OutputProcessor
		{
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab3SubWTitle;
			public override Boolean IsCurrent => OutputControl.tabbedControlGroupData.SelectedTabPage == OutputControl.layoutControlGroupTabW;

			public TabWOutputProcessor(FishingControl outputControl) : base(outputControl) { }

			protected override OutputDataPackage GetOutputData()
			{
				return new OutputDataPackage();
			}

			public override OutputItem GetOutputItem()
			{
				var slideObject = OutputControl.SlideContainer.EditedContent.FishingState.TabW.Slide;

				if (slideObject.SourceSlideMasters.ContainsKey(SlideSettingsManager.Instance.SlideSettings.Format))
				{
					var slideMasterName = slideObject.SourceSlideMasters[SlideSettingsManager.Instance.SlideSettings.Format];
					var targetSlideMaster = OutputControl.SlideContainer.StarInfo.FishingConfiguration.PartWSlides.Slides
						.FirstOrDefault(slideMaster =>
							String.Equals(slideMaster.Name, slideMasterName, StringComparison.OrdinalIgnoreCase));

					if (targetSlideMaster != null)
					{
						return new OutputItem
						{
							Name = OutputName,
							PresentationSourcePath = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath,
								Path.GetFileName(Path.GetTempFileName())),
							SlidesCount = 1,
							IsCurrent = IsCurrent,
							SlideGeneratingAction = (processor, destinationPresentation) =>
							{
								processor.AppendSlideMaster(targetSlideMaster.GetMasterPath(), destinationPresentation);
							},
							PreviewGeneratingAction = (processor, presentationSourcePath) =>
							{
								processor.PreparePresentation(presentationSourcePath,
									presentation => processor.AppendSlideMaster(targetSlideMaster.GetMasterPath(), presentation));
							}
						};
					}
				}
				return null;
			}
		}
	}
}
