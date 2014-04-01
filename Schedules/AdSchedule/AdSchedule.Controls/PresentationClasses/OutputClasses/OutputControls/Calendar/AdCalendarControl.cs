﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.InteropClasses;
using NewBizWiz.AdSchedule.Controls.ToolForms;
using NewBizWiz.Calendar.Controls.PresentationClasses.Calendars;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;
using Schedule = NewBizWiz.Core.AdSchedule.Schedule;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls.Calendar
{
	public sealed class AdCalendarControl : BaseCalendarControl
	{
		private Schedule _localSchedule = null;

		public AdCalendarControl()
			: base()
		{
			Dock = DockStyle.Fill;
			InitSlideInfo<SlideInfoControl>();
			laCalendarName.Visible = false;
			hyperLinkEditReset.Visible = true;
			hyperLinkEditReset.OpenLink += hyperLinkEditReset_OpenLink;
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadCalendar(e.QuickSave);
			});
		}

		public override List<ImageSource> DayImages
		{
			get { return Core.AdSchedule.ListManager.Instance.Images; }
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
			get { return Controller.Instance.CalendarMonthList; }
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
			get { return _localSchedule.Calendar; }
		}

		public override CalendarSettings CalendarSettings
		{
			get { return _localSchedule.ViewSettings.CalendarSettings; }
		}

		public override void LoadCalendar(bool quickLoad)
		{
			_localSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			base.LoadCalendar(quickLoad);
		}

		public override bool SaveCalendarData(string scheduleName = "")
		{
			var result = base.SaveCalendarData(scheduleName);
			var nameChanged = !string.IsNullOrEmpty(scheduleName);
			Controller.Instance.SaveSchedule(_localSchedule, nameChanged, true, this);
			return result;
		}

		public override void OpenHelp()
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("calendars");
		}

		public override void SaveSettings()
		{
			SettingsNotSaved = true;
		}

		public bool AllowToLeaveControl
		{
			get
			{
				bool result;
				if (SettingsNotSaved || (SelectedView != null && SelectedView.SettingsNotSaved) || SlideInfo.SettingsNotSaved)
				{
					SaveCalendarData();
					SlideInfo.Close(false);
					result = true;
				}
				else
					result = true;
				return result;
			}
		}

		private void TrackOutput()
		{
			BusinessWrapper.Instance.ActivityManager.AddActivity(new OutputActivity(Controller.Instance.TabCalendar.Text, _localSchedule.BusinessName, (decimal)_localSchedule.PrintProducts.Sum(p => p.TotalFinalRate)));
		}

		protected override void PowerPointInternal(IEnumerable<CalendarOutputData> outputData)
		{
			if (outputData == null) return;
			var commonOutputData = outputData.OfType<AdCalendarOutputData>();
			TrackOutput();
			using (var formProgress = new FormProgress())
			{
				Controller.Instance.ShowFloater(() =>
				{
					formProgress.TopMost = true;
					formProgress.laProgress.Text = commonOutputData.Count() == 2 ? "Creating 2 (two) Calendar slides…\nThis will take about a minute…" : "Creating Calendar slides…\nThis will take a few minutes…";
					formProgress.Show();
					Enabled = false;
					AdSchedulePowerPointHelper.Instance.AppendCalendar(commonOutputData.ToArray());
					Enabled = true;
					formProgress.Close();
				});
			}
		}

		protected override void EmailInternal(IEnumerable<CalendarOutputData> outputData)
		{
			if (outputData == null) return;
			var commonOutputData = outputData.OfType<AdCalendarOutputData>();
			var previewGroups = new List<PreviewGroup>();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Calendar for Email...";
				formProgress.TopMost = true;
				formProgress.Show();
				Enabled = false;
				foreach (var outputItem in commonOutputData)
				{
					var previewGroup = new PreviewGroup
					{
						Name = outputItem.MonthText,
						PresentationSourcePath = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()))
					};
					AdSchedulePowerPointHelper.Instance.PrepareCalendarEmail(previewGroup.PresentationSourcePath, new[] { outputItem });
					previewGroups.Add(previewGroup);
				}
				Enabled = true;
				formProgress.Close();
			}
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formEmail = new FormEmail(AdSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager))
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

		protected override void PreviewInternal(IEnumerable<CalendarOutputData> outputData)
		{
			if (outputData == null) return;
			var commonOutputData = outputData.OfType<AdCalendarOutputData>();
			var previewGroups = new List<PreviewGroup>();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Calendar for Preview...";
				formProgress.TopMost = true;
				formProgress.Show();
				Enabled = false;
				foreach (var outputItem in commonOutputData)
				{
					var previewGroup = new PreviewGroup
					{
						Name = outputItem.MonthText,
						PresentationSourcePath = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()))
					};
					AdSchedulePowerPointHelper.Instance.PrepareCalendarEmail(previewGroup.PresentationSourcePath, new[] { outputItem });
					previewGroups.Add(previewGroup);
				}
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				Enabled = true;
				formProgress.Close();
			}
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, AdSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager, Controller.Instance.ShowFloater, TrackOutput))
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

		private void hyperLinkEditReset_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("Are you sure want to reset data?") == DialogResult.Yes)
			{
				_localSchedule.Calendar.ResetToDefault();
				MonthView.RefreshData();
				SlideInfo.LoadData(allowToSave: false);
			}
			e.Handled = true;
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
			if (SaveCalendarData())
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
					if (SaveCalendarData(form.ScheduleName))
						Utilities.Instance.ShowInformation("Schedule was saved");
				}
				else
					Utilities.Instance.ShowWarning("Schedule Name can't be empty");
			}
		}

		public void Export_Click(object sender, EventArgs e)
		{
			using (var form = new FormExportSchedule())
			{
				form.Text = _localSchedule.Name;
				form.ScheduleName = String.Format("Ninja-{0}", DateTime.Now.ToString("MMddyy-hhmmtt"));
				if (form.ShowDialog() == DialogResult.OK)
				{
					Core.Calendar.ScheduleManager.ImportSchedule(_localSchedule.ScheduleFile.FullName, form.ScheduleName);
				}
			}
		}

		public void Preview_Click(object sender, EventArgs e)
		{
			SaveCalendarData();
			Preview();
		}

		public void PowerPoint_Click(object sender, EventArgs e)
		{
			SaveCalendarData();
			Print();
		}

		public void Email_Click(object sender, EventArgs e)
		{
			SaveCalendarData();
			Email();
		}

		public void Help_Click(object sender, EventArgs e)
		{
			OpenHelp();
		}
		#endregion
	}
}
