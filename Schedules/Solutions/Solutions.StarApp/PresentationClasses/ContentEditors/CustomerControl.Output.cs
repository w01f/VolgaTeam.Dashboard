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
	public sealed partial class CustomerControl
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
			protected CustomerControl OutputControl { get; }
			public abstract StarAppOutputType OutputType { get; }
			public abstract string OutputName { get; }
			public abstract bool ReadyForOutput { get; }

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

			public abstract IList<OutputConfiguration> GetOutputConfigurations();
			public abstract OutputDataPackage GetOutputData();
		}

		internal class TabAOutputProcessor : OutputProcessor
		{
			public override StarAppOutputType OutputType => StarAppOutputType.CustomerTabA;
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab4SubATitle;
			public override Boolean ReadyForOutput => true;

			public TabAOutputProcessor(CustomerControl outputControl) : base(outputControl) { }

			public override IList<OutputConfiguration> GetOutputConfigurations()
			{
				return new[] { new OutputConfiguration(StarAppOutputType.CustomerTabA, OutputName, 1, OutputControl.SlideContainer.ActiveSlideContent == OutputControl && OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 0) };
			}

			public override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var clipart1 = OutputControl.SlideContainer.EditedContent.CustomerState.TabA.Clipart1 ?? OutputControl.SlideContainer.StarInfo.Tab4SubAClipart1Image;
				if (clipart1 != null)
				{
					var fileName = Path.GetTempFileName();
					clipart1.Save(fileName);
					outputDataPackage.ClipartItems.Add("CP04ACLIPART1", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart1.Width, clipart1.Height) });
				}

				var clipart2 = OutputControl.SlideContainer.EditedContent.CustomerState.TabA.Clipart2 ?? OutputControl.SlideContainer.StarInfo.Tab4SubAClipart2Image;
				if (clipart2 != null)
				{
					var fileName = Path.GetTempFileName();
					clipart2.Save(fileName);
					outputDataPackage.ClipartItems.Add("CP04ACLIPART2", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart2.Width, clipart2.Height) });
				}

				var slideHeader = OutputControl.SlideContainer.EditedContent.CustomerState.TabA.SlideHeader?.Value ?? OutputControl.SlideContainer.StarInfo.CustomerConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault)?.Value;
				var combo1 = (OutputControl.SlideContainer.EditedContent.CustomerState.TabA.Combo1 ?? OutputControl.SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(0))?.Value;
				var combo2 = (OutputControl.SlideContainer.EditedContent.CustomerState.TabA.Combo2 ?? OutputControl.SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(1))?.Value;
				var combo3 = (OutputControl.SlideContainer.EditedContent.CustomerState.TabA.Combo3 ?? OutputControl.SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(2))?.Value;
				var combo4 = (OutputControl.SlideContainer.EditedContent.CustomerState.TabA.Combo4 ?? OutputControl.SlideContainer.StarInfo.TargetCustomersLists.CombinedList.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(3))?.Value;

				if (!String.IsNullOrWhiteSpace(combo1) &&
					!String.IsNullOrWhiteSpace(combo2) &&
					!String.IsNullOrWhiteSpace(combo3) &&
					!String.IsNullOrWhiteSpace(combo4))
				{
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile(clipart1 != null && clipart2 != null ? "CP04A-4.pptx" : (clipart1 != null ? "CP04A-8.pptx" : "CP04A-9.pptx"));

					outputDataPackage.TextItems.Add("CP04AHEADER".ToUpper(), slideHeader);
					outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
					outputDataPackage.TextItems.Add("CP04ACombo1".ToUpper(), combo1);
					outputDataPackage.TextItems.Add("CP04ACombo2".ToUpper(), combo2);
					outputDataPackage.TextItems.Add("CP04ACombo3".ToUpper(), combo3);
					outputDataPackage.TextItems.Add("CP04ACombo4".ToUpper(), combo4);
				}
				else if (!String.IsNullOrWhiteSpace(combo1) &&
						 !String.IsNullOrWhiteSpace(combo2) &&
						 !String.IsNullOrWhiteSpace(combo3))
				{
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile(clipart1 != null && clipart2 != null ? "CP04A-3.pptx" : (clipart1 != null ? "CP04A-7.pptx" : "CP04A-10.pptx"));

					outputDataPackage.TextItems.Add("CP04AHEADER".ToUpper(), slideHeader);
					outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
					outputDataPackage.TextItems.Add("CP04ACombo1".ToUpper(), combo1);
					outputDataPackage.TextItems.Add("CP04ACombo2".ToUpper(), combo2);
					outputDataPackage.TextItems.Add("CP04ACombo3".ToUpper(), combo3);
				}
				else if (!String.IsNullOrWhiteSpace(combo1) &&
						 !String.IsNullOrWhiteSpace(combo2))
				{
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile(clipart1 != null && clipart2 != null ? "CP04A-2.pptx" : (clipart1 != null ? "CP04A-6.pptx" : "CP04A-11.pptx"));

					outputDataPackage.TextItems.Add("CP04AHEADER".ToUpper(), slideHeader);
					outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
					outputDataPackage.TextItems.Add("CP04ACombo1".ToUpper(), combo1);
					outputDataPackage.TextItems.Add("CP04ACombo2".ToUpper(), combo2);
				}
				else if (!String.IsNullOrWhiteSpace(combo1))
				{
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile(clipart1 != null && clipart2 != null ? "CP04A-1.pptx" : (clipart1 != null ? "CP04A-5.pptx" : "CP04A-12.pptx"));

					outputDataPackage.TextItems.Add("CP04AHEADER".ToUpper(), slideHeader);
					outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
					outputDataPackage.TextItems.Add("CP04ACombo1".ToUpper(), combo1);
				}

				return outputDataPackage;
			}
		}

		internal class TabBOutputProcessor : OutputProcessor
		{
			public override StarAppOutputType OutputType => StarAppOutputType.CustomerTabB;
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab4SubBTitle;
			public override Boolean ReadyForOutput => true;

			public TabBOutputProcessor(CustomerControl outputControl) : base(outputControl) { }

			public override IList<OutputConfiguration> GetOutputConfigurations()
			{
				return new[] { new OutputConfiguration(StarAppOutputType.CustomerTabB, OutputName, 1, OutputControl.SlideContainer.ActiveSlideContent == OutputControl && OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 1) };
			}

			public override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var clipart1 = OutputControl.SlideContainer.EditedContent.CustomerState.TabB.Clipart1 ?? OutputControl.SlideContainer.StarInfo.Tab4SubBClipart1Image;
				if (clipart1 != null)
				{
					var fileName = Path.GetTempFileName();
					clipart1.Save(fileName);
					outputDataPackage.ClipartItems.Add("CP04BCLIPART1", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart1.Width, clipart1.Height) });
				}

				var clipart2 = OutputControl.SlideContainer.EditedContent.CustomerState.TabB.Clipart2 ?? OutputControl.SlideContainer.StarInfo.Tab4SubBClipart2Image;
				if (clipart2 != null)
				{
					var fileName = Path.GetTempFileName();
					clipart2.Save(fileName);
					outputDataPackage.ClipartItems.Add("CP04BCLIPART2", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart2.Width, clipart2.Height) });
				}

				var slideHeader = OutputControl.SlideContainer.EditedContent.CustomerState.TabB.SlideHeader?.Value ?? OutputControl.SlideContainer.StarInfo.CustomerConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault)?.Value;
				var subHeader1 = OutputControl.SlideContainer.EditedContent.CustomerState.TabB.Subheader1 ?? OutputControl.SlideContainer.StarInfo.CustomerConfiguration.PartBSubHeader1DefaultValue;
				var subHeader2 = OutputControl.SlideContainer.EditedContent.CustomerState.TabB.Subheader2 ?? OutputControl.SlideContainer.StarInfo.CustomerConfiguration.PartBSubHeader2DefaultValue;

				if (!String.IsNullOrWhiteSpace(subHeader1) &&
					!String.IsNullOrWhiteSpace(subHeader2))
				{
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile(clipart1 != null && clipart2 != null ? "CP04B-1.pptx" : (clipart1 != null ? "CP04B-3.pptx" : "CP04B-5.pptx"));

					outputDataPackage.TextItems.Add("CP04BHEADER".ToUpper(), slideHeader);
					outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
					outputDataPackage.TextItems.Add("CP04BSubheader1".ToUpper(), subHeader1);
					outputDataPackage.TextItems.Add("CP04BSubheader2".ToUpper(), subHeader2);
				}
				else if (!String.IsNullOrWhiteSpace(subHeader1))
				{
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile(clipart1 != null && clipart2 != null ? "CP04B-2.pptx" : (clipart1 != null ? "CP04B-4.pptx" : "CP04B-6.pptx"));

					outputDataPackage.TextItems.Add("CP04BHEADER".ToUpper(), slideHeader);
					outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
					outputDataPackage.TextItems.Add("CP04BSubheader1".ToUpper(), subHeader1);
				}

				return outputDataPackage;
			}
		}

		internal class TabCOutputProcessor : OutputProcessor
		{
			public override StarAppOutputType OutputType => StarAppOutputType.CustomerTabC;
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab4SubCTitle;
			public override Boolean ReadyForOutput => true;

			public TabCOutputProcessor(CustomerControl outputControl) : base(outputControl) { }

			public override IList<OutputConfiguration> GetOutputConfigurations()
			{
				return new[] { new OutputConfiguration(StarAppOutputType.CustomerTabC, OutputName, 1, OutputControl.SlideContainer.ActiveSlideContent == OutputControl && OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 2) };
			}

			public override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var slideHeader = OutputControl.SlideContainer.EditedContent.CustomerState.TabC.SlideHeader?.Value ?? OutputControl.SlideContainer.StarInfo.CustomerConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault)?.Value;
				var subHeader1 = OutputControl.SlideContainer.EditedContent.CustomerState.TabC.Subheader1 ?? OutputControl.SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader1DefaultValue;
				var subHeader2 = OutputControl.SlideContainer.EditedContent.CustomerState.TabC.Subheader2 ?? OutputControl.SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader2DefaultValue;
				var subHeader3 = OutputControl.SlideContainer.EditedContent.CustomerState.TabC.Subheader3 ?? OutputControl.SlideContainer.StarInfo.CustomerConfiguration.PartCSubHeader3DefaultValue;

				if (!String.IsNullOrWhiteSpace(subHeader1) &&
					!String.IsNullOrWhiteSpace(subHeader2) &&
					!String.IsNullOrWhiteSpace(subHeader3))
				{
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile("CP04C-3.pptx");

					outputDataPackage.TextItems.Add("CP04CHEADER".ToUpper(), slideHeader);
					outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
					outputDataPackage.TextItems.Add("CP04CSubheader1".ToUpper(), subHeader1);
					outputDataPackage.TextItems.Add("CP04CSubheader2".ToUpper(), subHeader2);
					outputDataPackage.TextItems.Add("CP04CSubheader3".ToUpper(), subHeader3);
				}
				else if (!String.IsNullOrWhiteSpace(subHeader1) &&
						 !String.IsNullOrWhiteSpace(subHeader2))
				{
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile("CP04C-2.pptx");

					outputDataPackage.TextItems.Add("CP04CHEADER".ToUpper(), slideHeader);
					outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
					outputDataPackage.TextItems.Add("CP04CSubheader1".ToUpper(), subHeader1);
					outputDataPackage.TextItems.Add("CP04CSubheader2".ToUpper(), subHeader2);
				}
				else if (!String.IsNullOrWhiteSpace(subHeader1))
				{
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCustomerFile("CP04C-1.pptx");

					outputDataPackage.TextItems.Add("CP04CHEADER".ToUpper(), slideHeader);
					outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
					outputDataPackage.TextItems.Add("CP04CSubheader1".ToUpper(), subHeader1);
				}

				return outputDataPackage;
			}
		}
	}
}
