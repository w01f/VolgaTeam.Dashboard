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
	public sealed partial class CustomerControl
	{
		public override bool ReadyForOutput => _outputProcessors.Any(processor => processor.ReadyForOutput);

		private readonly List<OutputProcessor> _outputProcessors = new List<OutputProcessor>();

		public override OutputGroup GetOutputGroup()
		{
			return new OutputGroup
			{
				Name = SlideContainer.StarInfo.Titles.Tab4Title,
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
			protected CustomerControl OutputControl { get; }
			public abstract string OutputName { get; }
			public virtual bool ReadyForOutput => !String.IsNullOrWhiteSpace(OutputName);
			public abstract bool IsCurrent { get; }

			protected OutputProcessor(CustomerControl outputControl)
			{
				OutputControl = outputControl;
			}

			public static IList<OutputProcessor> GetOutputProcessors(CustomerControl outputControl)
			{
				return new OutputProcessor[]
				{
					new TabAOutputProcessor(outputControl),
					new TabBOutputProcessor(outputControl),
					new TabCOutputProcessor(outputControl),
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
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab4SubATitle;
			public override Boolean IsCurrent => OutputControl.tabbedControlGroupData.SelectedTabPage == OutputControl.layoutControlGroupTabA;

			public TabAOutputProcessor(CustomerControl outputControl) : base(outputControl) { }

			protected override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var clipart1 = OutputControl.SlideContainer.EditedContent.CustomerState.TabA.Clipart1 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab4SubAClipart1Image);
				if (clipart1 != null)
					outputDataPackage.ClipartItems.Add("CP04ACLIPART1", clipart1);

				var clipart2 = OutputControl.SlideContainer.EditedContent.CustomerState.TabA.Clipart2 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab4SubAClipart2Image);
				if (clipart2 != null)
					outputDataPackage.ClipartItems.Add("CP04ACLIPART2", clipart2);

				var slideHeader = (OutputControl.SlideContainer.EditedContent.CustomerState.TabA.SlideHeader ?? OutputControl.SlideContainer.StarInfo.CustomerConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault))?.Value;
				var combos = new[]
					{
						(OutputControl.SlideContainer.EditedContent.CustomerState.TabA.Combo1 ?? OutputControl.SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(0))?.Value,
						(OutputControl.SlideContainer.EditedContent.CustomerState.TabA.Combo2 ?? OutputControl.SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(1))?.Value,
						(OutputControl.SlideContainer.EditedContent.CustomerState.TabA.Combo3 ?? OutputControl.SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(2))?.Value,
						(OutputControl.SlideContainer.EditedContent.CustomerState.TabA.Combo4 ?? OutputControl.SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(3))?.Value,
					}
					.Where(item => !String.IsNullOrWhiteSpace(item))
					.ToList();

				switch (combos.Count)
				{
					case 1:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile(clipart1 != null && clipart2 != null ? "CP04A-1.pptx" : (clipart1 != null ? "CP04A-5.pptx" : "CP04A-12.pptx"));
						break;
					case 2:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile(clipart1 != null && clipart2 != null ? "CP04A-2.pptx" : (clipart1 != null ? "CP04A-6.pptx" : "CP04A-11.pptx"));
						break;
					case 3:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile(clipart1 != null && clipart2 != null ? "CP04A-3.pptx" : (clipart1 != null ? "CP04A-7.pptx" : "CP04A-10.pptx"));
						break;
					case 4:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile(clipart1 != null && clipart2 != null ? "CP04A-4.pptx" : (clipart1 != null ? "CP04A-8.pptx" : "CP04A-9.pptx"));
						break;
					default:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile(clipart1 != null && clipart2 != null ? "CP04A-1.pptx" : (clipart1 != null ? "CP04A-5.pptx" : "CP04A-12.pptx"));
						break;
				}

				outputDataPackage.TextItems.Add("CP04AHEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP04ACombo1".ToUpper(), combos.ElementAtOrDefault(0));
				outputDataPackage.TextItems.Add("CP04ACombo2".ToUpper(), combos.ElementAtOrDefault(1));
				outputDataPackage.TextItems.Add("CP04ACombo3".ToUpper(), combos.ElementAtOrDefault(2));
				outputDataPackage.TextItems.Add("CP04ACombo4".ToUpper(), combos.ElementAtOrDefault(3));

				return outputDataPackage;
			}
		}

		internal class TabBOutputProcessor : OutputProcessor
		{
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab4SubBTitle;
			public override Boolean IsCurrent => OutputControl.tabbedControlGroupData.SelectedTabPage == OutputControl.layoutControlGroupTabB;

			public TabBOutputProcessor(CustomerControl outputControl) : base(outputControl) { }

			protected override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var clipart1 = OutputControl.SlideContainer.EditedContent.CustomerState.TabB.Clipart1 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab4SubBClipart1Image);
				if (clipart1 != null)
					outputDataPackage.ClipartItems.Add("CP04BCLIPART1", clipart1);

				var clipart2 = OutputControl.SlideContainer.EditedContent.CustomerState.TabB.Clipart2 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab4SubBClipart2Image);
				if (clipart2 != null)
					outputDataPackage.ClipartItems.Add("CP04BCLIPART2", clipart2);

				var slideHeader = (OutputControl.SlideContainer.EditedContent.CustomerState.TabB.SlideHeader ?? OutputControl.SlideContainer.StarInfo.CustomerConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault))?.Value;
				var subHeaders = new[]
					{
						OutputControl.SlideContainer.EditedContent.CustomerState.TabB.Subheader1 ?? OutputControl.SlideContainer.StarInfo.CustomerConfiguration.PartBSubHeader1DefaultValue,
						OutputControl.SlideContainer.EditedContent.CustomerState.TabB.Subheader2 ?? OutputControl.SlideContainer.StarInfo.CustomerConfiguration.PartBSubHeader2DefaultValue
					}
					.Where(item => !String.IsNullOrWhiteSpace(item))
					.ToList();

				switch (subHeaders.Count)
				{
					case 1:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile(clipart1 != null && clipart2 != null ? "CP04B-2.pptx" : (clipart1 != null ? "CP04B-4.pptx" : "CP04B-6.pptx"));
						break;
					case 2:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile(clipart1 != null && clipart2 != null ? "CP04B-1.pptx" : (clipart1 != null ? "CP04B-3.pptx" : "CP04B-5.pptx"));
						break;
					default:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile(clipart1 != null && clipart2 != null ? "CP04B-2.pptx" : (clipart1 != null ? "CP04B-4.pptx" : "CP04B-6.pptx"));
						break;
				}

				outputDataPackage.TextItems.Add("CP04BHEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP04BSubheader1".ToUpper(), subHeaders.ElementAtOrDefault(0));
				outputDataPackage.TextItems.Add("CP04BSubheader2".ToUpper(), subHeaders.ElementAtOrDefault(1));

				return outputDataPackage;
			}
		}

		internal class TabCOutputProcessor : OutputProcessor
		{
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab4SubCTitle;
			public override Boolean IsCurrent => OutputControl.tabbedControlGroupData.SelectedTabPage == OutputControl.layoutControlGroupTabC;

			public TabCOutputProcessor(CustomerControl outputControl) : base(outputControl) { }

			protected override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var slideHeader = (OutputControl.SlideContainer.EditedContent.CustomerState.TabC.SlideHeader ?? OutputControl.SlideContainer.StarInfo.CustomerConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault))?.Value;
				var subHeaders = new[]
					{
						OutputControl.SlideContainer.EditedContent.CustomerState.TabC.Subheader1 ?? OutputControl.SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader1DefaultValue,
						OutputControl.SlideContainer.EditedContent.CustomerState.TabC.Subheader2 ?? OutputControl.SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader2DefaultValue,
						OutputControl.SlideContainer.EditedContent.CustomerState.TabC.Subheader3 ?? OutputControl.SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader3DefaultValue
					}
					.Where(item => !String.IsNullOrWhiteSpace(item))
					.ToList();

				switch (subHeaders.Count)
				{
					case 1:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile("CP04C-1.pptx");
						break;
					case 2:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile("CP04C-2.pptx");
						break;
					case 3:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile("CP04C-3.pptx");
						break;
					default:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile("CP04C-1.pptx");
						break;
				}

				outputDataPackage.TextItems.Add("CP04CHEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP04CSubheader1".ToUpper(), subHeaders.ElementAtOrDefault(0));
				outputDataPackage.TextItems.Add("CP04CSubheader2".ToUpper(), subHeaders.ElementAtOrDefault(1));
				outputDataPackage.TextItems.Add("CP04CSubheader3".ToUpper(), subHeaders.ElementAtOrDefault(2));

				return outputDataPackage;
			}
		}
	}
}
