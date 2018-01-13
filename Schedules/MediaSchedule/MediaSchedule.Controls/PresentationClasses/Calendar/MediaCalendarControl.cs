using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Calendar.Configuration;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Calendar;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.Persistent;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Output;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.ToolForms;
using Asa.Calendar.Controls.PresentationClasses.Calendars;
using Asa.Media.Controls.BusinessClasses.Managers;

namespace Asa.Media.Controls.PresentationClasses.Calendar
{
	public abstract class MediaCalendarControl : BaseCalendarControl<MediaCalendar, MediaSchedule, MediaScheduleSettings, MediaScheduleChangeInfo>
	{
		protected abstract bool IsContentChanged { get; }

		protected MediaSchedule Schedule => BusinessObjects.Instance.ScheduleManager.ActiveSchedule;

		protected override Form FormMain => Controller.Instance.FormMain;

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

		protected override void OutpuPowerPointSlides(IEnumerable<CalendarOutputData> outputData)
		{
			if (outputData == null) return;
			FormProgress.SetTitle(outputData.Count() == 2 ? "Creating 2 (two) Calendar slides…\nThis will take about a minute…" : "Creating Calendar slides…\nThis will take a few minutes…");
			FormProgress.ShowOutputProgress();
			Controller.Instance.ShowFloater(() =>
			{
				Enabled = false;
				BusinessObjects.Instance.PowerPointManager.Processor.AppendCalendar(outputData.ToArray());
				Enabled = true;
				FormProgress.CloseProgress();
			});
		}

		protected override void OutputPdfSlides(IEnumerable<CalendarOutputData> outputData)
		{
			if (outputData == null) return;
			var previewGroups = new List<PreviewGroup>();
			FormProgress.SetTitle(outputData.Count() == 2 ? "Creating 2 (two) Calendar slides…\nThis will take about a minute…" : "Creating Calendar slides…\nThis will take a few minutes…");
			FormProgress.ShowOutputProgress();
			Controller.Instance.ShowFloater(() =>
			{
				Enabled = false;
				foreach (var outputItem in outputData)
				{
					var previewGroup = new PreviewGroup
					{
						Name = outputItem.MonthText,
						PresentationSourcePath = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
					};
					BusinessObjects.Instance.PowerPointManager.Processor.PrepareCalendarEmail(previewGroup.PresentationSourcePath, new[] { outputItem });
					previewGroups.Add(previewGroup);
				}
				var pdfFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}-{1}.pdf", Schedule.Name, DateTime.Now.ToString("MM-dd-yy-hmmss")));
				BusinessObjects.Instance.PowerPointManager.Processor.BuildPdf(pdfFileName, previewGroups.Select(pg => pg.PresentationSourcePath));
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

		protected override void PreviewSlides(IEnumerable<CalendarOutputData> outputData)
		{
			if (outputData == null) return;
			var previewGroups = new List<PreviewGroup>();
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Calendar for Preview...");
			FormProgress.ShowProgress(FormMain);
			Enabled = false;
			foreach (var outputItem in outputData)
			{
				var previewGroup = new PreviewGroup
				{
					Name = outputItem.MonthText,
					PresentationSourcePath = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
				};
				BusinessObjects.Instance.PowerPointManager.Processor.PrepareCalendarEmail(previewGroup.PresentationSourcePath, new[] { outputItem });
				previewGroups.Add(previewGroup);
			}
			Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
			Enabled = true;
			FormProgress.CloseProgress();
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formPreview = new FormPreview(
				Controller.Instance.FormMain,
				BusinessObjects.Instance.PowerPointManager.Processor,
				BusinessObjects.Instance.HelpManager,
				Controller.Instance.ShowFloater,
				Controller.Instance.CheckPowerPointRunning))
			{
				formPreview.Text = "Preview this Calendar";
				formPreview.LoadGroups(previewGroups);
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
			}
		}

		protected override void EmailSlides(IEnumerable<CalendarOutputData> outputData)
		{
			if (outputData == null) return;
			var previewGroups = new List<PreviewGroup>();
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Calendar for Email...");
			FormProgress.ShowProgress(FormMain);
			Enabled = false;
			foreach (var outputItem in outputData)
			{
				var previewGroup = new PreviewGroup
				{
					Name = outputItem.MonthText,
					PresentationSourcePath = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
				};
				BusinessObjects.Instance.PowerPointManager.Processor.PrepareCalendarEmail(previewGroup.PresentationSourcePath, new[] { outputItem });
				previewGroups.Add(previewGroup);
			}
			Enabled = true;
			FormProgress.CloseProgress();

			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formEmail = new FormEmail(BusinessObjects.Instance.PowerPointManager.Processor, BusinessObjects.Instance.HelpManager))
			{
				formEmail.Text = "Email this Calendar";
				formEmail.LoadGroups(previewGroups);
				Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
				RegistryHelper.MainFormHandle = formEmail.Handle;
				RegistryHelper.MaximizeMainForm = false;
				formEmail.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
			}
		}
		#endregion
	}
}