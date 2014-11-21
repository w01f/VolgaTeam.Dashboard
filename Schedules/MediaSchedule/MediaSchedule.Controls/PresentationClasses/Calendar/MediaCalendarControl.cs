using System;
using System.Collections.Generic;
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
using SettingsManager = NewBizWiz.Core.Common.SettingsManager;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.Calendar
{
	public abstract class MediaCalendarControl : BaseCalendarControl
	{
		protected RegularSchedule _localSchedule;

		protected MediaCalendarControl()
		{
			Dock = DockStyle.Fill;
			InitSlideInfo<CalendarSlideInfoControl>();
			var slideInfoControl = (CalendarSlideInfoControl)SlideInfo.ContainedControl;
			slideInfoControl.Reset += OnReset;
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadCalendar(e.QuickSave && !e.UpdateDigital && !e.CalendarTypeChanged);
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
			_localSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			base.LoadCalendar(quickLoad);
		}

		public override bool SaveCalendarData(bool byUser, string scheduleName = "")
		{
			var result = base.SaveCalendarData(byUser, scheduleName);
			var nameChanged = !string.IsNullOrEmpty(scheduleName);
			Controller.Instance.SaveSchedule(_localSchedule, nameChanged, true, false, false, this);
			return result;
		}

		public override void SaveSettings()
		{
			MediaMetaData.Instance.SettingsManager.SaveSettings();
		}

		public override void OpenHelp(string key)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink(key);
		}

		public override void TrackActivity(UserActivity activity)
		{
			BusinessWrapper.Instance.ActivityManager.AddActivity(activity);
		}

		private void OnReset(object sender, EventArgs e)
		{
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
			BusinessWrapper.Instance.ActivityManager.AddActivity(new UserActivity("Output", options));
		}

		protected override void PowerPointInternal(IEnumerable<CalendarOutputData> outputData)
		{
			if (outputData == null) return;
			TrackOutput();
			using (var formProgress = new FormProgress())
			{
				Controller.Instance.ShowFloater(() =>
				{
					formProgress.TopMost = true;
					formProgress.laProgress.Text = outputData.Count() == 2 ? "Creating 2 (two) Calendar slides…\nThis will take about a minute…" : "Creating Calendar slides…\nThis will take a few minutes…";
					formProgress.Show();
					Enabled = false;
					RegularMediaSchedulePowerPointHelper.Instance.AppendCalendar(outputData.ToArray());
					Enabled = true;
					formProgress.Close();
				});
			}
		}

		protected override void EmailInternal(IEnumerable<CalendarOutputData> outputData)
		{
			if (outputData == null) return;
			var previewGroups = new List<PreviewGroup>();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Calendar for Email...";
				formProgress.TopMost = true;
				formProgress.Show();
				Enabled = false;
				foreach (var outputItem in outputData)
				{
					var previewGroup = new PreviewGroup
					{
						Name = outputItem.MonthText,
						PresentationSourcePath = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()))
					};
					RegularMediaSchedulePowerPointHelper.Instance.PrepareCalendarEmail(previewGroup.PresentationSourcePath, new[] { outputItem });
					previewGroups.Add(previewGroup);
				}
				Enabled = true;
				formProgress.Close();
			}
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formEmail = new FormEmail(RegularMediaSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager))
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
			BusinessWrapper.Instance.ActivityManager.AddActivity(new UserActivity("Preview", options));
		}

		protected override void PreviewInternal(IEnumerable<CalendarOutputData> outputData)
		{
			if (outputData == null) return;
			var previewGroups = new List<PreviewGroup>();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Calendar for Preview...";
				formProgress.TopMost = true;
				formProgress.Show();
				Enabled = false;
				foreach (var outputItem in outputData)
				{
					var previewGroup = new PreviewGroup
					{
						Name = outputItem.MonthText,
						PresentationSourcePath = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()))
					};
					RegularMediaSchedulePowerPointHelper.Instance.PrepareCalendarEmail(previewGroup.PresentationSourcePath, new[] { outputItem });
					previewGroups.Add(previewGroup);
				}
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				Enabled = true;
				formProgress.Close();
			}
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, RegularMediaSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager, Controller.Instance.ShowFloater, TrackPreview))
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
			using (var form = new FormNewSchedule())
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

		public abstract void Help_Click(object sender, EventArgs e);
		#endregion
	}
}