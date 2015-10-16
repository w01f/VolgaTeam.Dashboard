using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using NewBizWiz.Calendar.Controls.PresentationClasses.Calendars;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;
using NewBizWiz.MediaSchedule.Controls.InteropClasses;
using ScheduleManager = NewBizWiz.Core.MediaSchedule.ScheduleManager;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.Calendar
{
	public abstract class MediaCalendarControl : BaseCalendarControl
	{
		protected RegularSchedule _localSchedule;

		protected MediaCalendarControl()
		{
			Dock = DockStyle.Fill;
			hyperLinkEditReset.Visible = true;
			hyperLinkEditReset.OpenLink += OnReset;
			BusinessObjects.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.BeginInvoke((MethodInvoker)delegate
			{
				if (sender != this)
				{
					LoadCalendar(e.QuickSave && !e.UpdateDigital && !e.CalendarTypeChanged);
					CalendarUpdated = e.QuickSave && !e.UpdateDigital && !e.CalendarTypeChanged;
				}
			});
		}

		public bool AllowToLeaveControl
		{
			get
			{
				bool result;
				if (SettingsNotSaved || (SelectedView != null && SelectedView.SettingsNotSaved) || SlideInfo.SettingsNotSaved)
				{
					SaveCalendarData(false);
					result = true;
				}
				else
					result = true;
				return result;
			}
		}

		public override ISchedule Schedule
		{
			get { return _localSchedule; }
		}

		public override Form FormMain
		{
			get { return Controller.Instance.FormMain; }
		}

		public override RibbonControl Ribbon
		{
			get { return Controller.Instance.Ribbon; }
		}

		public override ButtonItem ThemeButton
		{
			get { throw new NotImplementedException(); }
		}

		public override CalendarSettings CalendarSettings
		{
			get { return MediaMetaData.Instance.SettingsManager.BroadcastCalendarSettings; }
		}

		protected abstract RibbonTabItem CalendarTab { get; }

		public override void LoadCalendar(bool quickLoad)
		{
			_localSchedule = BusinessObjects.Instance.ScheduleManager.GetLocalSchedule();
			base.LoadCalendar(quickLoad);
		}

		public override bool SaveCalendarData(bool byUser, string scheduleName = "")
		{
			var result = base.SaveCalendarData(byUser, scheduleName);
			var nameChanged = !string.IsNullOrEmpty(scheduleName);
			Controller.Instance.SaveSchedule(_localSchedule, nameChanged, true, false, false, this);
			return result;
		}

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

		public override void TrackActivity(UserActivity activity)
		{
			BusinessObjects.Instance.ActivityManager.AddActivity(activity);
		}

		private void OnReset(object sender, EventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("Are you SURE you want to RESET your calendar to the default Information?") != DialogResult.Yes) return;
			CalendarData.Reset();
			base.LoadCalendar(false);
			MonthList_SelectedIndexChanged(MonthList, EventArgs.Empty);
			SettingsNotSaved = true;
		}

		protected void TrackOutput()
		{
			var options = new Dictionary<string, object>();
			options.Add("Slide", CalendarTab.Text);
			options.Add("Advertiser", _localSchedule.BusinessName);
			if (_localSchedule.Section.Programs.Any())
			{
				options.Add("TotalSpots", _localSchedule.Section.TotalSpots);
				options.Add("AverageRate", _localSchedule.Section.AvgRate);
				options.Add("GrossInvestment", _localSchedule.Section.TotalCost);
			}
			BusinessObjects.Instance.ActivityManager.AddActivity(new UserActivity("Output", options));
		}

		protected override void PowerPointInternal(IEnumerable<CalendarOutputData> outputData)
		{
			if (outputData == null) return;
			TrackOutput();
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.SetTitle(outputData.Count() == 2 ? "Creating 2 (two) Calendar slides…\nThis will take about a minute…" : "Creating Calendar slides…\nThis will take a few minutes…");
				FormProgress.ShowProgress();
				Enabled = false;
				RegularMediaSchedulePowerPointHelper.Instance.AppendCalendar(outputData.ToArray());
				Enabled = true;
				FormProgress.CloseProgress();
			});
		}

		protected override void EmailInternal(IEnumerable<CalendarOutputData> outputData)
		{
			if (outputData == null) return;
			var previewGroups = new List<PreviewGroup>();
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Calendar for Email...");
			FormProgress.ShowProgress();
			Enabled = false;
			foreach (var outputItem in outputData)
			{
				var previewGroup = new PreviewGroup
				{
					Name = outputItem.MonthText,
					PresentationSourcePath = Path.Combine(Core.Common.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
				};
				RegularMediaSchedulePowerPointHelper.Instance.PrepareCalendarEmail(previewGroup.PresentationSourcePath, new[] { outputItem });
				previewGroups.Add(previewGroup);
			}
			Enabled = true;
			FormProgress.CloseProgress();

			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formEmail = new FormEmail(RegularMediaSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager))
			{
				formEmail.Text = "Email this Calendar";
				formEmail.LoadGroups(previewGroups);
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				RegistryHelper.MainFormHandle = formEmail.Handle;
				RegistryHelper.MaximizeMainForm = false;
				formEmail.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
			}
		}

		private void TrackPreview()
		{
			var options = new Dictionary<string, object>();
			options.Add("Slide", CalendarTab.Text);
			options.Add("Advertiser", _localSchedule.BusinessName);
			if (_localSchedule.Section.Programs.Any())
			{
				options.Add("TotalSpots", _localSchedule.Section.TotalSpots);
				options.Add("AverageRate", _localSchedule.Section.AvgRate);
				options.Add("GrossInvestment", _localSchedule.Section.TotalCost);
			}
			BusinessObjects.Instance.ActivityManager.AddActivity(new UserActivity("Preview", options));
		}

		protected override void PreviewInternal(IEnumerable<CalendarOutputData> outputData)
		{
			if (outputData == null) return;
			var previewGroups = new List<PreviewGroup>();
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Calendar for Preview...");
			FormProgress.ShowProgress();
			Enabled = false;
			foreach (var outputItem in outputData)
			{
				var previewGroup = new PreviewGroup
				{
					Name = outputItem.MonthText,
					PresentationSourcePath = Path.Combine(Core.Common.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
				};
				RegularMediaSchedulePowerPointHelper.Instance.PrepareCalendarEmail(previewGroup.PresentationSourcePath, new[] { outputItem });
				previewGroups.Add(previewGroup);
			}
			Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
			Enabled = true;
			FormProgress.CloseProgress();
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, RegularMediaSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager, Controller.Instance.ShowFloater, TrackPreview))
			{
				formPreview.Text = "Preview this Calendar";
				formPreview.LoadGroups(previewGroups);
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
			}
		}

		protected override void PdfInternal(IEnumerable<CalendarOutputData> outputData)
		{
			if (outputData == null) return;
			TrackOutput();
			var previewGroups = new List<PreviewGroup>();
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.SetTitle(outputData.Count() == 2 ? "Creating 2 (two) Calendar slides…\nThis will take about a minute…" : "Creating Calendar slides…\nThis will take a few minutes…");
				FormProgress.ShowProgress();
				Enabled = false;
				foreach (var outputItem in outputData)
				{
					var previewGroup = new PreviewGroup
					{
						Name = outputItem.MonthText,
						PresentationSourcePath = Path.Combine(Core.Common.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
					};
					RegularMediaSchedulePowerPointHelper.Instance.PrepareCalendarEmail(previewGroup.PresentationSourcePath, new[] { outputItem });
					previewGroups.Add(previewGroup);
				}
				var pdfFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}-{1}.pdf", _localSchedule.Name, DateTime.Now.ToString("MM-dd-yy-hmmss")));
				RegularMediaSchedulePowerPointHelper.Instance.BuildPdf(pdfFileName, previewGroups.Select(pg => pg.PresentationSourcePath));
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

		#region Copy-Paste Methods and Event Handlers
		public void CalendarCopy_Click(object sender, EventArgs e)
		{
			SelectedView.CopyDay();
		}

		public void CalendarPaste_Click(object sender, EventArgs e)
		{
			SelectedView.PasteDay();
		}

		public void CalendarClone_Click(object sender, EventArgs e)
		{
			SelectedView.CloneDay();
		}
		#endregion

		#region Ribbon Operations Events
		public void MonthList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (MonthList.SelectedIndex < 0 || !AllowToSave) return;
			SlideInfo.LoadData(CalendarData.Months[MonthList.SelectedIndex]);
			Splash(true);
			SelectedView.ChangeMonth(CalendarData.Months[MonthList.SelectedIndex].Date);
			Splash(false);
			CalendarSettings.SelectedMonth = CalendarData.Months[MonthList.SelectedIndex].Date;
		}

		public void Save_Click(object sender, EventArgs e)
		{
			if (SaveCalendarData(true))
				Utilities.Instance.ShowInformation("Calendar Saved");
		}

		public void SaveAs_Click(object sender, EventArgs e)
		{
			using (var form = new FormNewSchedule(ScheduleManager.GetShortScheduleList().Select(s => s.ShortFileName)))
			{
				form.Text = "Save Schedule";
				form.laLogo.Text = "Please set a new name for your Schedule:";
				if (form.ShowDialog() != DialogResult.OK) return;
				if (!string.IsNullOrEmpty(form.ScheduleName))
				{
					if (SaveCalendarData(true, form.ScheduleName))
						Utilities.Instance.ShowInformation("Schedule was saved");
				}
				else
					Utilities.Instance.ShowWarning("Schedule Name can't be empty");
			}
		}

		public void Preview_Click(object sender, EventArgs e)
		{
			SaveCalendarData(false);
			Preview();
		}

		public void PowerPoint_Click(object sender, EventArgs e)
		{
			SaveCalendarData(false);
			Print();
		}

		public void Email_Click(object sender, EventArgs e)
		{
			SaveCalendarData(false);
			Email();
		}

		public void Pdf_Click(object sender, EventArgs e)
		{
			SaveCalendarData(false);
			PrintPdf();
		}

		public abstract void Help_Click(object sender, EventArgs e);
		#endregion
	}
}