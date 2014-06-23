using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using NewBizWiz.Calendar.Controls.PresentationClasses.Calendars;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;
using NewBizWiz.MediaSchedule.Controls.InteropClasses;
using Schedule = NewBizWiz.Core.MediaSchedule.Schedule;
using SettingsManager = NewBizWiz.Core.Common.SettingsManager;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses
{
	public sealed class BroadcastCalendarControl : BaseCalendarControl
	{
		private Schedule _localSchedule;

		public BroadcastCalendarControl()
		{
			Dock = DockStyle.Fill;
			InitSlideInfo<CalendarSlideInfoControl>();
			var slideInfoControl = (CalendarSlideInfoControl)SlideInfo.ContainedControl;
			slideInfoControl.Reset += OnReset;
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadCalendar(e.QuickSave && !e.UpdateDigital);
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
					SlideInfo.Close(false);
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

		public override ImageListBoxControl MonthList
		{
			get { return Controller.Instance.CalendarMonthsList; }
		}

		public override ButtonItem SlideInfoButton
		{
			get { return Controller.Instance.CalendarSlideInfo; }
		}

		public override ButtonItem PreviewButton
		{
			get { return Controller.Instance.CalendarPreview; }
		}

		public override ButtonItem EmailButton
		{
			get { return Controller.Instance.CalendarEmail; }
		}

		public override ButtonItem PowerPointButton
		{
			get { return Controller.Instance.CalendarPowerPoint; }
		}

		public override ButtonItem ThemeButton
		{
			get { throw new NotImplementedException(); }
		}

		public override ButtonItem CopyButton
		{
			get { return Controller.Instance.CalendarCopy; }
		}

		public override ButtonItem PasteButton
		{
			get { return Controller.Instance.CalendarPaste; }
		}

		public override ButtonItem CloneButton
		{
			get { return Controller.Instance.CalendarClone; }
		}

		public override Core.Calendar.Calendar CalendarData
		{
			get { return _localSchedule.BroadcastCalendar; }
		}

		public override CalendarSettings CalendarSettings
		{
			get { return MediaMetaData.Instance.SettingsManager.BroadcastCalendarSettings; }
		}

		public override void LoadCalendar(bool quickLoad)
		{
			_localSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			if (!_localSchedule.WeeklySchedule.Programs.Any()) return;
			if (!quickLoad)
				_localSchedule.BroadcastCalendar.UpdateNotesCollection();
			base.LoadCalendar(quickLoad);
		}

		public override bool SaveCalendarData(bool byUser, string scheduleName = "")
		{
			var result = base.SaveCalendarData(byUser, scheduleName);
			var nameChanged = !string.IsNullOrEmpty(scheduleName);
			Controller.Instance.SaveSchedule(_localSchedule, nameChanged, true, false, this);
			return result;
		}

		public override void OpenHelp()
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("adcal");
		}

		public override void SaveSettings()
		{
			MediaMetaData.Instance.SettingsManager.SaveSettings();
		}

		private void OnReset(object sender, EventArgs e)
		{
			_localSchedule.BroadcastCalendar.Reset();
			base.LoadCalendar(false);
			MonthList_SelectedIndexChanged(MonthList, EventArgs.Empty);
			SettingsNotSaved = true;
		}

		public override void TrackActivity(UserActivity activity)
		{
			BusinessWrapper.Instance.ActivityManager.AddActivity(activity);
		}

		public override void UpdateOutputFunctions()
		{
			base.UpdateOutputFunctions();
			var enable = _localSchedule.WeeklySchedule.Programs.Any();
			MonthList.Enabled = enable;
			SlideInfoButton.Enabled = enable;
			pnTop.Visible = enable;
			pnMain.Visible = enable;
			if (!enable)
				SlideInfo.Close();
			pictureBoxNoData.Image = Properties.Resources.CalendarDisabled;
			pictureBoxNoData.Visible = !enable;
			pictureBoxNoData.BringToFront();
		}

		private void TrackOutput()
		{
			var options = new Dictionary<string, object>();
			options.Add("Slide", Controller.Instance.TabCalendar.Text);
			options.Add("Advertiser", _localSchedule.BusinessName);
			if (_localSchedule.WeeklySchedule.Programs.Any())
			{
				options.Add("WeeklyTotalSpots", _localSchedule.WeeklySchedule.TotalSpots);
				options.Add("WeeklyAverageRate", _localSchedule.WeeklySchedule.AvgRate);
				options.Add("WeeklyGrossInvestment", _localSchedule.WeeklySchedule.TotalCost);
			}
			if (_localSchedule.MonthlySchedule.Programs.Any())
			{
				options.Add("MonthlyTotalSpots", _localSchedule.MonthlySchedule.TotalSpots);
				options.Add("MonthlyAverageRate", _localSchedule.MonthlySchedule.AvgRate);
				options.Add("MonthlyGrossInvestment", _localSchedule.MonthlySchedule.TotalCost);
			}
			BusinessWrapper.Instance.ActivityManager.AddActivity(new UserActivity("Output", options));
		}

		protected override void PowerPointInternal(IEnumerable<CalendarOutputData> outputData)
		{
			if (outputData == null) return;
			var broadcastCalendarOutputData = outputData.OfType<BroadcastCalendarOutputData>();
			TrackOutput();
			using (var formProgress = new FormProgress())
			{
				Controller.Instance.ShowFloater(() =>
				{
					formProgress.TopMost = true;
					formProgress.laProgress.Text = outputData.Count() == 2 ? "Creating 2 (two) Calendar slides…\nThis will take about a minute…" : "Creating Calendar slides…\nThis will take a few minutes…";
					formProgress.Show();
					Enabled = false;
					MediaSchedulePowerPointHelper.Instance.AppendCalendar(broadcastCalendarOutputData.ToArray());
					Enabled = true;
					formProgress.Close();
				});
			}
		}

		protected override void EmailInternal(IEnumerable<CalendarOutputData> outputData)
		{
			if (outputData == null) return;
			var broadcastCalendarOutputData = outputData.OfType<BroadcastCalendarOutputData>();
			var previewGroups = new List<PreviewGroup>();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Calendar for Email...";
				formProgress.TopMost = true;
				formProgress.Show();
				Enabled = false;
				foreach (var outputItem in broadcastCalendarOutputData)
				{
					var previewGroup = new PreviewGroup
					{
						Name = outputItem.MonthText,
						PresentationSourcePath = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()))
					};
					MediaSchedulePowerPointHelper.Instance.PrepareCalendarEmail(previewGroup.PresentationSourcePath, new[] { outputItem });
					previewGroups.Add(previewGroup);
				}
				Enabled = true;
				formProgress.Close();
			}
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formEmail = new FormEmail(MediaSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager))
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
			options.Add("Slide", Controller.Instance.TabCalendar.Text);
			options.Add("Advertiser", _localSchedule.BusinessName);
			if (_localSchedule.WeeklySchedule.Programs.Any())
			{
				options.Add("WeeklyTotalSpots", _localSchedule.WeeklySchedule.TotalSpots);
				options.Add("WeeklyAverageRate", _localSchedule.WeeklySchedule.AvgRate);
				options.Add("WeeklyGrossInvestment", _localSchedule.WeeklySchedule.TotalCost);
			}
			if (_localSchedule.MonthlySchedule.Programs.Any())
			{
				options.Add("MonthlyTotalSpots", _localSchedule.MonthlySchedule.TotalSpots);
				options.Add("MonthlyAverageRate", _localSchedule.MonthlySchedule.AvgRate);
				options.Add("MonthlyGrossInvestment", _localSchedule.MonthlySchedule.TotalCost);
			}
			BusinessWrapper.Instance.ActivityManager.AddActivity(new UserActivity("Preview", options));
		}

		protected override void PreviewInternal(IEnumerable<CalendarOutputData> outputData)
		{
			if (outputData == null) return;
			var broadcastCalendarOutputData = outputData.OfType<BroadcastCalendarOutputData>();
			var previewGroups = new List<PreviewGroup>();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Calendar for Preview...";
				formProgress.TopMost = true;
				formProgress.Show();
				Enabled = false;
				foreach (var outputItem in broadcastCalendarOutputData)
				{
					var previewGroup = new PreviewGroup
					{
						Name = outputItem.MonthText,
						PresentationSourcePath = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()))
					};
					MediaSchedulePowerPointHelper.Instance.PrepareCalendarEmail(previewGroup.PresentationSourcePath, new[] { outputItem });
					previewGroups.Add(previewGroup);
				}
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				Enabled = true;
				formProgress.Close();
			}
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, MediaSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager, Controller.Instance.ShowFloater, TrackPreview))
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

		public void SlideInfo_CheckedChanged(object sender, EventArgs e)
		{
			if (!AllowToSave) return;
			if (SlideInfoButton.Checked)
			{
				Splash(true);
				SlideInfo.Show();
				Splash(false);
			}
			else
			{
				Splash(true);
				SlideInfo.Close();
				Splash(false);
			}
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

		public void Help_Click(object sender, EventArgs e)
		{
			OpenHelp();
		}
		#endregion
	}
}
