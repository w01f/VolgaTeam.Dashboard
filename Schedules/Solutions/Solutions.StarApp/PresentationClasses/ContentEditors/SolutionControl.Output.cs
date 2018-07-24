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
	public sealed partial class SolutionControl
	{
		public override bool ReadyForOutput => _outputProcessors.Any(processor => processor.ReadyForOutput);

		private readonly List<OutputProcessor> _outputProcessors = new List<OutputProcessor>();

		public override OutputGroup GetOutputGroup()
		{
			return new OutputGroup
			{
				Name = SlideContainer.StarInfo.Titles.Tab10Title,
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
			protected SolutionControl OutputControl { get; }
			public abstract string OutputName { get; }
			public virtual bool ReadyForOutput => !String.IsNullOrWhiteSpace(OutputName);
			public abstract bool IsCurrent { get; }

			protected OutputProcessor(SolutionControl outputControl)
			{
				OutputControl = outputControl;
			}

			public static IList<OutputProcessor> GetOutputProcessors(SolutionControl outputControl)
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
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab10SubATitle;
			public override Boolean IsCurrent => OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 0;

			public TabAOutputProcessor(SolutionControl outputControl) : base(outputControl) { }

			protected override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var clipart = OutputControl.SlideContainer.EditedContent.SolutionState.TabA.Clipart1 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab10SubAClipart1Image);
				if (clipart != null)
					outputDataPackage.ClipartItems.Add("CP10ACLIPART1", clipart);

				var slideHeader = (OutputControl.SlideContainer.EditedContent.SolutionState.TabA.SlideHeader ?? OutputControl.SlideContainer.StarInfo.SolutionConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault))?.Value;
				var subHeader1 = OutputControl.SlideContainer.EditedContent.SolutionState.TabA.Subheader1 ?? OutputControl.SlideContainer.StarInfo.SolutionConfiguration.PartASubHeader1DefaultValue;

				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarSolutionFile("CP10A-1.pptx");

				outputDataPackage.TextItems.Add("CP10AHEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP10ASubheader1".ToUpper(), subHeader1);

				return outputDataPackage;
			}
		}

		internal class TabBOutputProcessor : OutputProcessor
		{
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab10SubBTitle;
			public override Boolean IsCurrent => OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 1;

			public TabBOutputProcessor(SolutionControl outputControl) : base(outputControl) { }

			protected override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var clipart1 = OutputControl.SlideContainer.EditedContent.SolutionState.TabB.Clipart1 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab10SubBClipart1Image);
				if (clipart1 != null)
					outputDataPackage.ClipartItems.Add("CP10BCLIPART1", clipart1);

				var clipart2 = OutputControl.SlideContainer.EditedContent.SolutionState.TabB.Clipart2 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab10SubBClipart2Image);
				if (clipart2 != null)
					outputDataPackage.ClipartItems.Add("CP10BCLIPART2", clipart2);

				var clipart3 = OutputControl.SlideContainer.EditedContent.SolutionState.TabB.Clipart3 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab10SubBClipart3Image);
				if (clipart3 != null)
					outputDataPackage.ClipartItems.Add("CP10BCLIPART3", clipart3);

				var slideHeader = (OutputControl.SlideContainer.EditedContent.SolutionState.TabB.SlideHeader ?? OutputControl.SlideContainer.StarInfo.SolutionConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault))?.Value;
				var subHeader1 = OutputControl.SlideContainer.EditedContent.SolutionState.TabB.Subheader1 ?? OutputControl.SlideContainer.StarInfo.SolutionConfiguration.PartBSubHeader1DefaultValue;

				outputDataPackage.TextItems.Add("CP10BHEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP10BSubheader1".ToUpper(), subHeader1);

				if (clipart1 != null &&
				   clipart2 != null &&
				   clipart3 != null)
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarSolutionFile("CP10B-3.pptx");
				else if (clipart1 != null &&
						 clipart2 != null)
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarSolutionFile("CP10B-2.pptx");
				else
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarSolutionFile("CP10B-1.pptx");

				return outputDataPackage;
			}
		}

		internal class TabCOutputProcessor : OutputProcessor
		{
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab10SubCTitle;
			public override Boolean IsCurrent => OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 2;

			public TabCOutputProcessor(SolutionControl outputControl) : base(outputControl) { }

			protected override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var clipart1 = OutputControl.SlideContainer.EditedContent.SolutionState.TabC.Clipart1 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab10SubCClipart1Image);
				if (clipart1 != null)
					outputDataPackage.ClipartItems.Add("CP10CCLIPART1", clipart1);

				var clipart2 = OutputControl.SlideContainer.EditedContent.SolutionState.TabC.Clipart2 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab10SubCClipart2Image);
				if (clipart2 != null)
					outputDataPackage.ClipartItems.Add("CP10CCLIPART2", clipart2);

				var slideHeader = (OutputControl.SlideContainer.EditedContent.SolutionState.TabC.SlideHeader ?? OutputControl.SlideContainer.StarInfo.SolutionConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault))?.Value;

				var subHeaders = new[]
					{
						OutputControl.SlideContainer.EditedContent.SolutionState.TabC.Subheader1 ?? OutputControl.SlideContainer.StarInfo.SolutionConfiguration.PartCSubHeader1DefaultValue,
						OutputControl.SlideContainer.EditedContent.SolutionState.TabC.Subheader2 ?? OutputControl.SlideContainer.StarInfo.SolutionConfiguration.PartCSubHeader2DefaultValue
					}.Where(item => !String.IsNullOrWhiteSpace(item))
							.ToList();

				outputDataPackage.TextItems.Add("CP10CHEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP10CSubheader1".ToUpper(), subHeaders.ElementAtOrDefault(0));
				outputDataPackage.TextItems.Add("CP10CSubheader2".ToUpper(), subHeaders.ElementAtOrDefault(1));

				if (clipart1 != null &&
					clipart2 != null)
					outputDataPackage.TemplateName = subHeaders.Count > 1 ?
						MasterWizardManager.Instance.SelectedWizard.GetStarSolutionFile("CP10C-1.pptx") :
						MasterWizardManager.Instance.SelectedWizard.GetStarSolutionFile("CP10C-2.pptx");
				else if (clipart1 != null)
					outputDataPackage.TemplateName = subHeaders.Count > 1 ?
						MasterWizardManager.Instance.SelectedWizard.GetStarSolutionFile("CP10C-3.pptx") :
						MasterWizardManager.Instance.SelectedWizard.GetStarSolutionFile("CP10C-4.pptx");
				else
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarSolutionFile("CP10C-5.pptx");
				return outputDataPackage;
			}
		}

		internal class TabDOutputProcessor : OutputProcessor
		{
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab10SubDTitle;
			public override Boolean IsCurrent => OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 3;

			public TabDOutputProcessor(SolutionControl outputControl) : base(outputControl) { }

			protected override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var clipart = OutputControl.SlideContainer.EditedContent.SolutionState.TabD.Clipart1 ?? ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab10SubDClipart1Image);
				if (clipart != null)
					outputDataPackage.ClipartItems.Add("CP10DCLIPART1", clipart);

				var slideHeader = (OutputControl.SlideContainer.EditedContent.SolutionState.TabD.SlideHeader ?? OutputControl.SlideContainer.StarInfo.SolutionConfiguration.HeadersPartDItems.FirstOrDefault(h => h.IsDefault))?.Value;
				var subHeader1 = OutputControl.SlideContainer.EditedContent.SolutionState.TabD.Subheader1 ?? OutputControl.SlideContainer.StarInfo.SolutionConfiguration.PartDSubHeader1DefaultValue;

				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarSolutionFile("CP10D-1.pptx");

				outputDataPackage.TextItems.Add("CP10DHEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP10DSubheader1".ToUpper(), subHeader1);

				return outputDataPackage;
			}
		}
	}
}
