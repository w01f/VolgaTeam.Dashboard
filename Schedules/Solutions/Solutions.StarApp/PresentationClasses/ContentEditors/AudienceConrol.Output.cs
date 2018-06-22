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
	public sealed partial class AudienceControl
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
			protected AudienceControl OutputControl { get; }
			public abstract StarAppOutputType OutputType { get; }
			public abstract string OutputName { get; }
			public abstract bool ReadyForOutput { get; }

			protected OutputProcessor(AudienceControl outputControl)
			{
				OutputControl = outputControl;
			}

			public static IList<OutputProcessor> GetOutputProcessors(AudienceControl outputControl)
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
			public override StarAppOutputType OutputType => StarAppOutputType.AudienceTabA;
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab9SubATitle;
			public override Boolean ReadyForOutput => true;

			public TabAOutputProcessor(AudienceControl outputControl) : base(outputControl) { }

			public override IList<OutputConfiguration> GetOutputConfigurations()
			{
				return new[] { new OutputConfiguration(StarAppOutputType.AudienceTabA, OutputName, 1, OutputControl.SlideContainer.ActiveSlideContent == OutputControl && OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 0) };
			}

			public override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var clipart1 = OutputControl.SlideContainer.EditedContent.AudienceState.TabA.Clipart1 ?? OutputControl.SlideContainer.StarInfo.Tab9SubAClipart1Image;
				if (clipart1 != null)
				{
					var fileName = Path.GetTempFileName();
					clipart1.Save(fileName);
					outputDataPackage.ClipartItems.Add("CP09ACLIPART1", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart1.Width, clipart1.Height) });
				}

				var clipart2 = OutputControl.SlideContainer.EditedContent.AudienceState.TabA.Clipart2 ?? OutputControl.SlideContainer.StarInfo.Tab9SubAClipart2Image;
				if (clipart2 != null)
				{
					var fileName = Path.GetTempFileName();
					clipart2.Save(fileName);
					outputDataPackage.ClipartItems.Add("CP09ACLIPART2", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart2.Width, clipart2.Height) });
				}

				var slideHeader = OutputControl.SlideContainer.EditedContent.AudienceState.TabA.SlideHeader?.Value ?? OutputControl.SlideContainer.StarInfo.AudienceConfiguration.HeadersPartAItems.FirstOrDefault(h => h.IsDefault)?.Value;
				outputDataPackage.TextItems.Add("CP09AHEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);

				var textItemKeys = new[]
				{
					"CP09ASubheader1",
					"CP09ASubheader2"
				};

				var textItemValues = new List<string>();
				var subHeader1 = OutputControl.SlideContainer.EditedContent.AudienceState.TabA.Subheader1 ?? OutputControl.SlideContainer.StarInfo.AudienceConfiguration.PartASubHeader1DefaultValue;
				if (!String.IsNullOrWhiteSpace(subHeader1))
					textItemValues.Add(subHeader1);

				var subHeader2 = OutputControl.SlideContainer.EditedContent.AudienceState.TabA.Subheader2 ?? OutputControl.SlideContainer.StarInfo.AudienceConfiguration.PartASubHeader2DefaultValue;
				if (!String.IsNullOrWhiteSpace(subHeader2))
					textItemValues.Add(subHeader2);

				switch (textItemValues.Count)
				{
					case 1:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarAudienceFile(clipart1 != null ? "CP09A-2.pptx" : "CP09A-4.pptx");
						break;
					case 2:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarAudienceFile(clipart1 != null ? "CP09A-1.pptx" : "CP09A-3.pptx");
						break;
					default:
						outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarAudienceFile(clipart1 != null ? "CP09A-2.pptx" : "CP09A-4.pptx");
						break;
				}

				for (int i = 0; i < textItemKeys.Length; i++)
					outputDataPackage.TextItems.Add(textItemKeys[i].ToUpper(), textItemValues.ElementAtOrDefault(i) ?? String.Empty);

				return outputDataPackage;
			}
		}

		internal class TabBOutputProcessor : OutputProcessor
		{
			public override StarAppOutputType OutputType => StarAppOutputType.AudienceTabB;
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab9SubBTitle;
			public override Boolean ReadyForOutput => true;

			public TabBOutputProcessor(AudienceControl outputControl) : base(outputControl) { }

			public override IList<OutputConfiguration> GetOutputConfigurations()
			{
				return new[] { new OutputConfiguration(StarAppOutputType.AudienceTabB, OutputName, 1, OutputControl.SlideContainer.ActiveSlideContent == OutputControl && OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 1) };
			}

			public override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;
				outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarAudienceFile("CP09B-1.pptx");

				var clipart1 = OutputControl.SlideContainer.EditedContent.AudienceState.TabB.Clipart1 ?? OutputControl.SlideContainer.StarInfo.Tab9SubBClipart1Image;
				if (clipart1 != null)
				{
					var fileName = Path.GetTempFileName();
					clipart1.Save(fileName);
					outputDataPackage.ClipartItems.Add("CP09BCLIPART1", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart1.Width, clipart1.Height) });
				}

				var clipart2 = OutputControl.SlideContainer.EditedContent.AudienceState.TabB.Clipart2 ?? OutputControl.SlideContainer.StarInfo.Tab9SubBClipart2Image;
				if (clipart2 != null)
				{
					var fileName = Path.GetTempFileName();
					clipart2.Save(fileName);
					outputDataPackage.ClipartItems.Add("CP09BCLIPART2", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart2.Width, clipart2.Height) });
				}

				var clipart3 = OutputControl.SlideContainer.EditedContent.AudienceState.TabB.Clipart3 ?? OutputControl.SlideContainer.StarInfo.Tab9SubBClipart3Image;
				if (clipart3 != null)
				{
					var fileName = Path.GetTempFileName();
					clipart3.Save(fileName);
					outputDataPackage.ClipartItems.Add("CP09BCLIPART3", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart3.Width, clipart3.Height) });
				}

				var slideHeader = (OutputControl.SlideContainer.EditedContent.AudienceState.TabB.SlideHeader ?? OutputControl.SlideContainer.StarInfo.AudienceConfiguration.HeadersPartBItems.FirstOrDefault(h => h.IsDefault))?.Value;
				outputDataPackage.TextItems.Add("CP09BHEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);

				var subHeader1 = OutputControl.SlideContainer.EditedContent.AudienceState.TabB.Subheader1 ?? OutputControl.SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader1DefaultValue;
				var subHeader2 = OutputControl.SlideContainer.EditedContent.AudienceState.TabB.Subheader2 ?? OutputControl.SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader2DefaultValue;
				var subHeader3 = OutputControl.SlideContainer.EditedContent.AudienceState.TabB.Subheader3 ?? OutputControl.SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader3DefaultValue;
				var subHeader4 = OutputControl.SlideContainer.EditedContent.AudienceState.TabB.Subheader4 ?? OutputControl.SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader4DefaultValue;
				var subHeader5 = OutputControl.SlideContainer.EditedContent.AudienceState.TabB.Subheader5 ?? OutputControl.SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader5DefaultValue;
				var subHeader6 = OutputControl.SlideContainer.EditedContent.AudienceState.TabB.Subheader6 ?? OutputControl.SlideContainer.StarInfo.AudienceConfiguration.PartBSubHeader6DefaultValue;

				if (!String.IsNullOrWhiteSpace(subHeader1))
					outputDataPackage.TextItems.Add("CP09BSubHeader1".ToUpper(), subHeader1);
				if (!String.IsNullOrWhiteSpace(subHeader2))
					outputDataPackage.TextItems.Add("CP09BSubHeader2".ToUpper(), subHeader2);
				if (!String.IsNullOrWhiteSpace(subHeader3))
					outputDataPackage.TextItems.Add("CP09BSubHeader3".ToUpper(), subHeader3);
				if (!String.IsNullOrWhiteSpace(subHeader4))
					outputDataPackage.TextItems.Add("CP09BSubHeader4".ToUpper(), subHeader4);
				if (!String.IsNullOrWhiteSpace(subHeader5))
					outputDataPackage.TextItems.Add("CP09BSubHeader5".ToUpper(), subHeader5);
				if (!String.IsNullOrWhiteSpace(subHeader6))
					outputDataPackage.TextItems.Add("CP09BSubHeader6".ToUpper(), subHeader6);

				return outputDataPackage;
			}
		}

		internal class TabCOutputProcessor : OutputProcessor
		{
			public override StarAppOutputType OutputType => StarAppOutputType.AudienceTabC;
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab9SubCTitle;
			public override Boolean ReadyForOutput => true;

			public TabCOutputProcessor(AudienceControl outputControl) : base(outputControl) { }

			public override IList<OutputConfiguration> GetOutputConfigurations()
			{
				return new[] { new OutputConfiguration(StarAppOutputType.AudienceTabC, OutputName, 1, OutputControl.SlideContainer.ActiveSlideContent == OutputControl && OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 2) };
			}

			public override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;

				var clipart1 = OutputControl.SlideContainer.EditedContent.AudienceState.TabC.Clipart1 ?? OutputControl.SlideContainer.StarInfo.Tab9SubCClipart1Image;
				if (clipart1 != null)
				{
					var fileName = Path.GetTempFileName();
					clipart1.Save(fileName);
					outputDataPackage.ClipartItems.Add("CP09CCLIPART1", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart1.Width, clipart1.Height) });
				}

				var clipart2 = OutputControl.SlideContainer.EditedContent.AudienceState.TabC.Clipart2 ?? OutputControl.SlideContainer.StarInfo.Tab9SubCClipart2Image;
				if (clipart2 != null)
				{
					var fileName = Path.GetTempFileName();
					clipart2.Save(fileName);
					outputDataPackage.ClipartItems.Add("CP09CCLIPART2", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart2.Width, clipart2.Height) });
				}

				var clipart3 = OutputControl.SlideContainer.EditedContent.AudienceState.TabC.Clipart3 ?? OutputControl.SlideContainer.StarInfo.Tab9SubCClipart3Image;
				if (clipart3 != null)
				{
					var fileName = Path.GetTempFileName();
					clipart3.Save(fileName);
					outputDataPackage.ClipartItems.Add("CP09CCLIPART3", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart3.Width, clipart3.Height) });
				}

				var clipart4 = OutputControl.SlideContainer.EditedContent.AudienceState.TabC.Clipart4 ?? OutputControl.SlideContainer.StarInfo.Tab9SubCClipart4Image;
				if (clipart4 != null)
				{
					var fileName = Path.GetTempFileName();
					clipart4.Save(fileName);
					outputDataPackage.ClipartItems.Add("CP09CCLIPART4", new OutputImageInfo { FilePath = fileName, Size = new Size(clipart4.Width, clipart4.Height) });
				}

				var slideHeader = (OutputControl.SlideContainer.EditedContent.AudienceState.TabC.SlideHeader ?? OutputControl.SlideContainer.StarInfo.AudienceConfiguration.HeadersPartCItems.FirstOrDefault(h => h.IsDefault))?.Value;
				var combo1 = (OutputControl.SlideContainer.EditedContent.AudienceState.TabC.Combo1 ?? OutputControl.SlideContainer.StarInfo.AudienceConfiguration.PartCCombo1Items.FirstOrDefault(h => h.IsDefault))?.Value;

				outputDataPackage.TextItems.Add("CP09CHEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("HEADER".ToUpper(), slideHeader);
				outputDataPackage.TextItems.Add("CP09CCombo1".ToUpper(), combo1);

				if (clipart1 != null &&
					clipart2 != null &&
					clipart3 != null &&
					clipart4 != null)
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarAudienceFile("CP09C-1.pptx");
				else if (clipart1 != null &&
						 clipart2 != null &&
						 clipart3 != null)
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarAudienceFile("CP09C-2.pptx");
				else if (clipart1 != null &&
						 clipart2 != null)
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarAudienceFile("CP09C-3.pptx");
				else if (clipart2 != null &&
						 clipart4 != null)
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarAudienceFile("CP09C-5.pptx");
				else if (clipart1 != null &&
						 clipart3 != null)
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarAudienceFile("CP09C-6.pptx");
				else if (clipart1 != null &&
						 clipart4 != null)
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarAudienceFile("CP09C-7.pptx");
				else if (clipart1 != null)
					outputDataPackage.TemplateName = MasterWizardManager.Instance.SelectedWizard.GetStarAudienceFile("CP09C-4.pptx");

				return outputDataPackage;
			}
		}
	}
}
