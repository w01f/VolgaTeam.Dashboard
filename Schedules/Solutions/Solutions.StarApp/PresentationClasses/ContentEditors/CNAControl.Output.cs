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
	public sealed partial class CNAControl
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
			protected CNAControl OutputControl { get; }
			public abstract StarAppOutputType OutputType { get; }
			public abstract string OutputName { get; }
			public abstract bool ReadyForOutput { get; }

			protected OutputProcessor(CNAControl outputControl)
			{
				OutputControl = outputControl;
			}

			public static IList<OutputProcessor> GetOutputProcessors(CNAControl outputControl)
			{
				return new OutputProcessor[]
				{
					new TabAOutputProcessor(outputControl),
					new TabBOutputProcessor(outputControl),
				};
			}

			public abstract IList<OutputConfiguration> GetOutputConfigurations();
			public abstract OutputDataPackage GetOutputData();
		}

		internal class TabAOutputProcessor : OutputProcessor
		{
			public override StarAppOutputType OutputType => StarAppOutputType.CNATabA;
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab2SubATitle;
			public override Boolean ReadyForOutput => true;

			public TabAOutputProcessor(CNAControl outputControl) : base(outputControl) { }

			public override IList<OutputConfiguration> GetOutputConfigurations()
			{
				return new[] { new OutputConfiguration(StarAppOutputType.CNATabA, OutputName, 1, OutputControl.SlideContainer.ActiveSlideContent == OutputControl && OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 0) };
			}

			public override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var clipart = OutputControl.SlideContainer.EditedContent.CNAState.TabA.Clipart1 ?? OutputControl.SlideContainer.StarInfo.Tab2SubAClipart1Image;
				if (clipart != null)
				{
					var fileName = Path.GetTempFileName();
					clipart.Save(fileName);
					outputDataPackage.ClipartItems.Add("CP02ACLIPART1", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart.Width, clipart.Height) });
				}

				var slideHeader = (OutputControl.SlideContainer.EditedContent.CNAState.TabA.SlideHeader ?? OutputControl.SlideContainer.StarInfo.CNAConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault))?.Value;
				outputDataPackage.TextItems.Add("CP02AHEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);

				var textItemKeys = new[]
				{
					"CP02ASubheader1",
					"CP02ASubheader2"
				};

				var textItemValues = new List<string>();
				var subHeader1 = OutputControl.SlideContainer.EditedContent.CNAState.TabA.Subheader1 ?? OutputControl.SlideContainer.StarInfo.CNAConfiguration.PartASubHeader1DefaultValue;
				if (!String.IsNullOrWhiteSpace(subHeader1))
					textItemValues.Add(subHeader1);

				var subHeader2 = OutputControl.SlideContainer.EditedContent.CNAState.TabA.Subheader2 ?? OutputControl.SlideContainer.StarInfo.CNAConfiguration.PartASubHeader2DefaultValue;
				if (!String.IsNullOrWhiteSpace(subHeader2))
					textItemValues.Add(subHeader2);

				switch (textItemValues.Count)
				{
					case 1:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCNAFile("CP02A-1.pptx");
						break;
					case 2:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCNAFile("CP02A-2.pptx");
						break;
					default:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCNAFile("CP02A-1.pptx");
						break;
				}

				for (int i = 0; i < textItemKeys.Length; i++)
					outputDataPackage.TextItems.Add(textItemKeys[i].ToUpper(), textItemValues.ElementAtOrDefault(i)??String.Empty);

				return outputDataPackage;
			}
		}

		internal class TabBOutputProcessor : OutputProcessor
		{
			public override StarAppOutputType OutputType => StarAppOutputType.CNATabB;
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab2SubBTitle;
			public override Boolean ReadyForOutput => true;

			public TabBOutputProcessor(CNAControl outputControl) : base(outputControl) { }

			public override IList<OutputConfiguration> GetOutputConfigurations()
			{
				return new[] { new OutputConfiguration(StarAppOutputType.CNATabB, OutputName, 1, OutputControl.SlideContainer.ActiveSlideContent == OutputControl && OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 1) };
			}

			public override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var clipart1 = OutputControl.SlideContainer.EditedContent.CNAState.TabB.Clipart1 ?? OutputControl.SlideContainer.StarInfo.Tab2SubBClipart1Image;
				if (clipart1 != null)
				{
					var fileName = Path.GetTempFileName();
					clipart1.Save(fileName);
					outputDataPackage.ClipartItems.Add("CP02BCLIPART1", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart1.Width, clipart1.Height) });
				}

				var clipart2 = OutputControl.SlideContainer.EditedContent.CNAState.TabB.Clipart2 ?? OutputControl.SlideContainer.StarInfo.Tab2SubBClipart2Image;
				if (clipart2 != null)
				{
					var fileName = Path.GetTempFileName();
					clipart2.Save(fileName);
					outputDataPackage.ClipartItems.Add("CP02BCLIPART2", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart2.Width, clipart2.Height) });
				}

				var slideHeader = (OutputControl.SlideContainer.EditedContent.CNAState.TabB.SlideHeader ?? OutputControl.SlideContainer.StarInfo.CNAConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault))?.Value;
				outputDataPackage.TextItems.Add("CP02BHEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);

				var comboItemKeys = new[]
				{
					"CP02BCombo1",
					"CP02BCombo2",
					"CP02BCombo3",
					"CP02BCombo4",
					"CP02BCombo5"
				};

				var comboItemValues = new List<string>();
				var combo1 = (OutputControl.SlideContainer.EditedContent.CNAState.TabB.Combo1 ?? OutputControl.SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(0))?.Value;
				if (!String.IsNullOrWhiteSpace(combo1))
					comboItemValues.Add(combo1);

				var combo2 = (OutputControl.SlideContainer.EditedContent.CNAState.TabB.Combo2 ?? OutputControl.SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(1))?.Value;
				if (!String.IsNullOrWhiteSpace(combo2))
					comboItemValues.Add(combo2);

				var combo3 = (OutputControl.SlideContainer.EditedContent.CNAState.TabB.Combo3 ?? OutputControl.SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(2))?.Value;
				if (!String.IsNullOrWhiteSpace(combo3))
					comboItemValues.Add(combo3);

				var combo4 = (OutputControl.SlideContainer.EditedContent.CNAState.TabB.Combo4 ?? OutputControl.SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(3))?.Value;
				if (!String.IsNullOrWhiteSpace(combo4))
					comboItemValues.Add(combo4);

				var combo5 = (OutputControl.SlideContainer.EditedContent.CNAState.TabB.Combo5 ?? OutputControl.SlideContainer.StarInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).ElementAtOrDefault(4))?.Value;
				if (!String.IsNullOrWhiteSpace(combo5))
					comboItemValues.Add(combo5);

				switch (comboItemValues.Count)
				{
					case 1:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCNAFile(clipart2 != null ? "CP02B-1.pptx" : "CP02B-6.pptx");
						break;
					case 2:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCNAFile(clipart2 != null ? "CP02B-2.pptx" : "CP02B-7.pptx");
						break;
					case 3:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCNAFile(clipart2 != null ? "CP02B-3.pptx" : "CP02B-8.pptx");
						break;
					case 4:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCNAFile(clipart2 != null ? "CP02B-4.pptx" : "CP02B-9.pptx");
						break;
					case 5:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCNAFile(clipart2 != null ? "CP02B-5.pptx" : "CP02B-10.pptx");
						break;
					default:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarCNAFile("CP02A-1.pptx");
						break;
				}

				for (int i = 0; i < comboItemKeys.Length; i++)
					outputDataPackage.TextItems.Add(comboItemKeys[i].ToUpper(), comboItemValues.ElementAtOrDefault(i) ?? String.Empty);

				return outputDataPackage;
			}
		}
	}
}
