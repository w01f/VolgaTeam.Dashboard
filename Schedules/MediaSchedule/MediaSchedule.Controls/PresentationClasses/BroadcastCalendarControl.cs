using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using NewBizWiz.Calendar.Controls.PresentationClasses.Calendars;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;
using NewBizWiz.MediaSchedule.Controls.InteropClasses;
using NewBizWiz.MediaSchedule.Controls.ToolForms;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses
{
	public class BroadcastCalendarControl : BaseCalendarControl
	{
		protected Schedule _localSchedule = null;

		public BroadcastCalendarControl()
			: base()
		{
			InitSlideInfo<CalendarSlideInfoControl>();
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadCalendar(e.QuickSave);
			});
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

		public override List<ImageSource> DayImages
		{
			get { return MediaMetaData.Instance.ListManager.Images; }
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
			get
			{
				return _localSchedule.BroadcastCalendar;
			}
		}

		public override Core.Calendar.CalendarSettings CalendarSettings
		{
			get
			{
				return MediaMetaData.Instance.SettingsManager.BroadcastCalendarSettings;
			}
		}

		public override void LoadCalendar(bool quickLoad)
		{
			_localSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			if (!_localSchedule.WeeklySchedule.Programs.Any()) return;
			base.LoadCalendar(quickLoad);
		}

		public override bool SaveCalendarData(string scheduleName = "")
		{
			var result = base.SaveCalendarData(scheduleName);
			Controller.Instance.SaveSchedule(_localSchedule, true, this);
			return result;
		}

		public override void OpenHelp()
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("adcalendar");
		}

		public override void SaveSettings()
		{
			MediaMetaData.Instance.SettingsManager.SaveSettings();
		}

		public override void UpdateOutputFunctions()
		{
			base.UpdateOutputFunctions();
			var enable = _localSchedule.WeeklySchedule.Programs.Any();
			MonthList.Enabled = enable;
			SlideInfoButton.Enabled = enable;
			pnTop.Visible = enable;
			pnMain.Visible = enable;
			if(!enable)
				SlideInfo.Close();
			pictureBoxNoData.Image = Properties.Resources.CalendarDisabled;
			pictureBoxNoData.Visible = !enable;
			pictureBoxNoData.BringToFront();
		}

		protected override void PowerPointInternal(IEnumerable<Core.Calendar.CalendarOutputData> outputData)
		{
			if (outputData == null) return;
			using (var formProgress = new FormProgress())
			{
				Controller.Instance.ShowFloater(() =>
				{
					formProgress.TopMost = true;
					formProgress.laProgress.Text = outputData.Count() == 2 ? "Creating 2 (two) Calendar slides…\nThis will take about a minute…" : "Creating Calendar slides…\nThis will take a few minutes…";
					formProgress.Show();
					Enabled = false;
					MediaSchedulePowerPointHelper.Instance.AppendCalendar(outputData.ToArray());
					Enabled = true;
					formProgress.Close();
				});
			}
		}

		protected override void EmailInternal(IEnumerable<Core.Calendar.CalendarOutputData> outputData)
		{
			if (outputData == null) return;
			var tempFileName = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Calendar for Email...";
				formProgress.TopMost = true;
				formProgress.Show();
				Enabled = false;
				MediaSchedulePowerPointHelper.Instance.PrepareCalendarEmail(tempFileName, outputData.ToArray());
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				Enabled = true;
				formProgress.Close();
			}
			if (!File.Exists(tempFileName)) return;
			using (var formEmail = new FormEmail(FormMain, MediaSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager))
			{
				formEmail.Text = "Email this Calendar";
				formEmail.PresentationFile = tempFileName;
				RegistryHelper.MainFormHandle = formEmail.Handle;
				RegistryHelper.MaximizeMainForm = false;
				formEmail.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
			}
		}

		protected override void PreviewInternal(IEnumerable<Core.Calendar.CalendarOutputData> outputData)
		{
			if (outputData == null) return;
			var tempFileName = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Calendar for Preview...";
				formProgress.TopMost = true;
				formProgress.Show();
				Enabled = false;
				MediaSchedulePowerPointHelper.Instance.PrepareCalendarEmail(tempFileName, outputData.ToArray());
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				Enabled = true;
				formProgress.Close();
			}
			if (!File.Exists(tempFileName)) return;
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, MediaSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager, Controller.Instance.ShowFloater))
			{
				formPreview.Text = "Preview this Calendar";
				formPreview.PresentationFile = tempFileName;
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
