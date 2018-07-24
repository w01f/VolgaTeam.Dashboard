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
	partial class CoverControl
	{
		public override bool ReadyForOutput => _outputProcessors.Any(processor => processor.ReadyForOutput);

		private readonly List<OutputProcessor> _outputProcessors = new List<OutputProcessor>();

		public override OutputGroup GetOutputGroup()
		{
			return new OutputGroup
			{
				Name = SlideContainer.StarInfo.Titles.Tab1Title,
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
			protected CoverControl OutputControl { get; }
			public abstract string OutputName { get; }
			public virtual bool ReadyForOutput => !String.IsNullOrWhiteSpace(OutputName);
			public abstract bool IsCurrent { get; }

			protected OutputProcessor(CoverControl outputControl)
			{
				OutputControl = outputControl;
			}

			public static IList<OutputProcessor> GetOutputProcessors(CoverControl outputControl)
			{
				return new OutputProcessor[]
				{
					new TabAOutputProcessor(outputControl),
				};
			}

			protected abstract OutputDataPackage GetOutputData();

			public virtual OutputItem GetOutputItem()
			{
				var outputData = GetOutputData();
				return new OutputItem
				{
					Name = OutputName,
					InsertOnTop = outputData.AddAsFirtsPage,
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
			public override String OutputName => OutputControl.SlideContainer.StarInfo.Titles.Tab1SubATitle;
			public override Boolean IsCurrent => OutputControl.tabbedControlGroupData.SelectedTabPageIndex == 0;

			public TabAOutputProcessor(CoverControl outputControl) : base(outputControl) { }

			protected override OutputDataPackage GetOutputData()
			{
				var outputDataPackage = new OutputDataPackage();

				outputDataPackage.Theme = OutputControl.SelectedTheme;
				outputDataPackage.AddAsFirtsPage = OutputControl.SlideContainer.EditedContent.CoverState.TabA.AddAsPageOne;

				var clipart = OutputControl.SlideContainer.EditedContent.CoverState.TabA.Clipart1 ??
							  ImageClipartObject.FromImage(OutputControl.SlideContainer.StarInfo.Tab1SubAClipart1Image);
				if (clipart != null)
					outputDataPackage.ClipartItems.Add("CP01ACLIPART1", clipart);

				var slideHeader = (OutputControl.SlideContainer.EditedContent.CoverState.TabA.SlideHeader ??
								   OutputControl.SlideContainer.StarInfo.CoverConfiguration.HeaderPartAItems.FirstOrDefault(h =>
									   h.IsDefault))?.Value;
				var subHeader1 = OutputControl.SlideContainer.EditedContent.CoverState.TabA.Subheader1 ??
								 OutputControl.SlideContainer.StarInfo.CoverConfiguration.PartASubHeader1DefaultValue;
				var calendar1 = OutputControl.SlideContainer.EditedContent.CoverState.TabA.Calendar1 != DateTime.MinValue
					? OutputControl.SlideContainer.EditedContent.CoverState.TabA.Calendar1 ?? OutputControl._defaultDate
					: (DateTime?)null;
				var combo1 = OutputControl.SlideContainer.EditedContent.CoverState.TabA.Combo1 ??
							 OutputControl._usersByStation.FirstOrDefault();

				if (!String.IsNullOrWhiteSpace(slideHeader) &&
					!String.IsNullOrWhiteSpace(subHeader1) &&
					calendar1.HasValue &&
					combo1 != null)
				{
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetStarCoverFile(clipart != null ? "CP01A-1.pptx" : "CP01A-8.pptx");

					outputDataPackage.TextItems.Add("CP01ATitleBox".ToUpper(), slideHeader);
					outputDataPackage.TextItems.Add("CP01ASubheader1".ToUpper(), subHeader1);
					outputDataPackage.TextItems.Add("CP01ACalendar1".ToUpper(), calendar1.Value.ToString("MMMM d, yyyy"));
					outputDataPackage.TextItems.Add("CP01ACombo1".ToUpper(), combo1.ToString());
					outputDataPackage.TextItems.Add("CP01ACombo1b".ToUpper(), String.Join("     ", combo1.Email, combo1.Phone));
				}
				else if (!String.IsNullOrWhiteSpace(slideHeader) &&
						 calendar1.HasValue &&
						 combo1 != null)
				{
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetStarCoverFile(clipart != null ? "CP01A-2.pptx" : "CP01A-9.pptx");

					outputDataPackage.TextItems.Add("CP01ATitleBox".ToUpper(), slideHeader);
					outputDataPackage.TextItems.Add("CP01ACalendar1".ToUpper(), calendar1.Value.ToString("MMMM d, yyyy"));
					outputDataPackage.TextItems.Add("CP01ACombo1".ToUpper(), combo1.ToString());
					outputDataPackage.TextItems.Add("CP01ACombo1b".ToUpper(), String.Join("     ", combo1.Email, combo1.Phone));
				}
				else if (!String.IsNullOrWhiteSpace(slideHeader) &&
						 !String.IsNullOrWhiteSpace(subHeader1) &&
						 calendar1.HasValue)
				{
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetStarCoverFile(clipart != null ? "CP01A-3.pptx" : "CP01A-10.pptx");

					outputDataPackage.TextItems.Add("CP01ATitleBox".ToUpper(), slideHeader);
					outputDataPackage.TextItems.Add("CP01ASubheader1".ToUpper(), subHeader1);
					outputDataPackage.TextItems.Add("CP01ACalendar1".ToUpper(), calendar1.Value.ToString("MMMM d, yyyy"));
				}
				else if (!String.IsNullOrWhiteSpace(slideHeader) &&
						 !String.IsNullOrWhiteSpace(subHeader1) &&
						 combo1 != null)
				{
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetStarCoverFile(clipart != null ? "CP01A-7.pptx" : "CP018A-14.pptx");

					outputDataPackage.TextItems.Add("CP01ATitleBox".ToUpper(), slideHeader);
					outputDataPackage.TextItems.Add("CP01ASubheader1".ToUpper(), subHeader1);
					outputDataPackage.TextItems.Add("CP01ACombo1".ToUpper(), combo1.ToString());
					outputDataPackage.TextItems.Add("CP01ACombo1b".ToUpper(), String.Join("     ", combo1.Email, combo1.Phone));
				}
				else if (!String.IsNullOrWhiteSpace(slideHeader) &&
						 !String.IsNullOrWhiteSpace(subHeader1))
				{
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetStarCoverFile(clipart != null ? "CP01A-4.pptx" : "CP01A-11.pptx");

					outputDataPackage.TextItems.Add("CP01ATitleBox".ToUpper(), slideHeader);
					outputDataPackage.TextItems.Add("CP01ASubheader1".ToUpper(), subHeader1);
				}
				else if (!String.IsNullOrWhiteSpace(slideHeader) &&
						 calendar1.HasValue)
				{
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetStarCoverFile(clipart != null ? "CP01A-5.pptx" : "CP01A-12.pptx");

					outputDataPackage.TextItems.Add("CP01ATitleBox".ToUpper(), slideHeader);
					outputDataPackage.TextItems.Add("CP01ACalendar1".ToUpper(), calendar1.Value.ToString("MMMM d, yyyy"));
				}
				else if (!String.IsNullOrWhiteSpace(slideHeader))
				{
					outputDataPackage.TemplateName =
						MasterWizardManager.Instance.SelectedWizard.GetStarCoverFile(clipart != null ? "CP01A-6.pptx" : "CP01A-13.pptx");

					outputDataPackage.TextItems.Add("CP01ATitleBox".ToUpper(), slideHeader);
				}

				return outputDataPackage;
			}
		}
	}
}
