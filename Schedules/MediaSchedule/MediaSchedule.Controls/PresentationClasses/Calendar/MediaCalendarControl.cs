using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Calendar.Configuration;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Calendar;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.Persistent;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Output;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.ToolForms;
using Asa.Calendar.Controls.PresentationClasses.Calendars;
using Asa.Calendar.Controls.PresentationClasses.Output;
using Asa.Common.Core.OfficeInterops;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.InteropClasses;

namespace Asa.Media.Controls.PresentationClasses.Calendar
{
	public abstract class MediaCalendarControl : BaseCalendarControl<MediaCalendar, MediaSchedule, MediaScheduleSettings, MediaScheduleChangeInfo>
	{
		protected abstract bool IsContentChanged { get; }

		protected MediaSchedule Schedule => BusinessObjects.Instance.ScheduleManager.ActiveSchedule;
		protected override Form FormMain => Controller.Instance.FormMain;
		protected override PowerPointProcessor PowerPointProcessor => BusinessObjects.Instance.PowerPointManager.Processor;
		protected override Color? AccentColor => BusinessObjects.Instance.FormStyleManager.Style.AccentColor;
		public override CalendarSettings CalendarSettings => MediaMetaData.Instance.SettingsManager.BroadcastCalendarSettings;

		#region BaseContentEditControl Override
		public override void InitControl()
		{
			base.InitControl();

			retractableBarControl.simpleButtonExpand.Image = BusinessObjects.Instance.ImageResourcesManager.RetractableBarExpandImage ??
															 retractableBarControl.simpleButtonExpand.Image;
			retractableBarControl.simpleButtonCollapse.Image = BusinessObjects.Instance.ImageResourcesManager.RetractableBarCollpaseImage ??
															   retractableBarControl.simpleButtonCollapse.Image;

			ResetButton.Click += OnCalendarResetClick;
			InitSlideInfo<CalendarSlideInfoControl>();
		}

		public override void InitBusinessObjects()
		{
			BusinessObjects.Instance.AdditionalInitializator.RequestContentInitailization(Identifier);
		}

		protected override void UpdateEditedContet()
		{
			if (IsContentChanged)
			{
				if (EditedContent != null)
				{
					ReleaseControls();
					EditedContent.Dispose();
				}
				EditedContent = GetEditedCalendar();
				base.UpdateEditedContet();
			}
		}

		public abstract MediaCalendar GetEditedCalendar();

		protected void Reset()
		{
			Splash(true);
			FormProgress.ShowProgress("Loading Data...", () =>
			{
				ReleaseControls();
				ActiveCalendarSection.Reset();
				LoadCalendar();
			});
			Splash(false);
			SettingsNotSaved = true;
		}
		#endregion

		#region ICalendarControl Members
		public override ColorSchema GetColorSchema(string colorName)
		{
			return BusinessObjects.Instance.OutputManager.CalendarColors.Items
				.Where(color => color.Name.ToLower() == colorName.ToLower())
				.Select(color => color.Schema)
				.FirstOrDefault();
		}

		public override void SaveSettings()
		{
			MediaMetaData.Instance.SettingsManager.SaveSettings();
		}

		public override void OpenHelp(string key)
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink(key);
		}
		#endregion

		#region Event Handlers
		protected void OnCalendarResetClick(object sender, EventArgs e)
		{
			if (PopupMessageHelper.Instance.ShowWarningQuestion("Are you SURE you want to RESET your calendar to the default Information?") != DialogResult.Yes) return;
			Reset();
		}
		#endregion

		#region Output Staff
		protected override void UpdateMenuOutputButtons()
		{
			UpdateDataManagementAndOutputFunctions();
		}

		protected override IList<OutputGroup> GeneratePreviewData(IList<CaledarMonthOutputItem> monthItems)
		{
			var previewGroups = new List<OutputGroup>();
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Calendar for Preview...");
			FormProgress.ShowProgress(FormMain);
			Enabled = false;
			foreach (var monthOutputItem in monthItems)
			{
				var tempPresentationPath = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath,
					Path.GetFileName(Path.GetTempFileName()));
				var previewGroup = new OutputGroup
				{
					Name = monthOutputItem.DisplayName,
					IsCurrent = monthOutputItem.IsCurrent,
					Items = new List<OutputItem>(new[]
					{
						new OutputItem
						{
							Name = monthOutputItem.DisplayName,
							PresentationSourcePath = tempPresentationPath,
							SlidesCount = 1,
							IsCurrent = true,
							SlideGeneratingAction = (processor,destinationPresentation) =>
							{
								processor.AppendCalendar(monthOutputItem.CalendarMonth.OutputData,destinationPresentation);
							},
							PreviewGeneratingAction = (processor, filePath) =>
							{
								processor.PrepareCalendarPreview(filePath,monthOutputItem.CalendarMonth.OutputData);
							}
						}
					})
				};
				previewGroups.Add(previewGroup);
			}
			FormProgress.CloseProgress();
			Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);

			return previewGroups;
		}

		protected override void OutpuPowerPointSlides(IList<OutputItem> outputItems)
		{
			FormProgress.SetTitle(outputItems.Count == 2 ? "Creating 2 (two) Calendar slides…\nThis will take about a minute…" : "Creating Calendar slides…\nThis will take a few minutes…");
			FormProgress.ShowOutputProgress();
			Controller.Instance.ShowFloater(() =>
			{
				Enabled = false;

				foreach (var outputItem in outputItems)
					outputItem.SlideGeneratingAction?.Invoke(PowerPointProcessor, null);

				Enabled = true;
				FormProgress.CloseProgress();
			});
		}

		protected override void OutputPdfSlides(IList<OutputItem> outputItems)
		{
			FormProgress.SetTitle(outputItems.Count == 2 ? "Creating 2 (two) Calendar slides…\nThis will take about a minute…" : "Creating Calendar slides…\nThis will take a few minutes…");
			FormProgress.ShowOutputProgress();
			Controller.Instance.ShowFloater(() =>
			{
				Enabled = false;

				var pdfFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}-{1:MM-dd-yy-hmmss}.pdf", Schedule.Name, DateTime.Now));
				PowerPointProcessor.BuildPdf(pdfFileName, presentation =>
				{
					foreach (var outputItem in outputItems)
						outputItem.SlideGeneratingAction?.Invoke(PowerPointProcessor, presentation);
				});
				if (File.Exists(pdfFileName))
					try
					{
						Process.Start(pdfFileName);
					}
					catch { }

				Enabled = true;
				FormProgress.CloseProgress();
			});
		}

		protected override void EmailSlides(IList<OutputItem> outputItems)
		{
			using (var form = new FormEmailFileName())
			{
				if (form.ShowDialog() == DialogResult.OK)
				{
					FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Email...");
					FormProgress.ShowProgress();
					Controller.Instance.ShowFloater(() =>
					{
						var emailFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}-{1:MM-dd-yy-hmmss}.pdf", Schedule.Name, DateTime.Now));
						var defaultItem = outputItems.First();
						BusinessObjects.Instance.PowerPointManager.Processor.PreparePresentation(emailFileName, presentation =>
						{
							foreach (var outputItem in outputItems)
								outputItem.SlideGeneratingAction?.Invoke(BusinessObjects.Instance.PowerPointManager.Processor, presentation);
						});

						var emailFile = Path.Combine(
							Path.GetFullPath(defaultItem.PresentationSourcePath)
								.Replace(Path.GetFileName(defaultItem.PresentationSourcePath), string.Empty),
							form.FileName + ".pptx");
						File.Copy(emailFileName, emailFile, true);

						FormProgress.CloseProgress();

						try
						{
							if (OutlookHelper.Instance.Open())
							{
								OutlookHelper.Instance.CreateMessage("Advertising Schedule", emailFile);
								OutlookHelper.Instance.Close();
							}
							else
								PopupMessageHelper.Instance.ShowWarning("Cannot open Outlook");
							File.Delete(emailFile);
						}
						catch { }
					});
				}
			}
		}
		#endregion
	}
}