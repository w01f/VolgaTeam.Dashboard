using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Asa.AdSchedule.Controls.BusinessClasses;
using Asa.AdSchedule.Controls.InteropClasses;
using Asa.Calendar.Controls.PresentationClasses.Calendars;
using Asa.CommonGUI.Preview;
using Asa.CommonGUI.ToolForms;
using Asa.Core.AdSchedule;
using Asa.Core.Calendar;
using Asa.Core.Common;
using Schedule = Asa.Core.AdSchedule.Schedule;
using ScheduleManager = Asa.Core.AdSchedule.ScheduleManager;

namespace Asa.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls.Calendar
{
	public abstract class AdCalendarControl : BaseCalendarControl
	{
		protected Schedule _localSchedule = null;

		protected AdCalendarControl()
		{
			Dock = DockStyle.Fill;
			hyperLinkEditReset.Visible = true;
			hyperLinkEditReset.OpenLink += hyperLinkEditReset_OpenLink;
			BusinessObjects.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.BeginInvoke((MethodInvoker)delegate
			{
				if (sender != this)
				{
					LoadCalendar(e.QuickSave);
					CalendarUpdated = e.QuickSave;
				}
			});
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

		public override CalendarSettings CalendarSettings
		{
			get { return _localSchedule.ViewSettings.CalendarSettings; }
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
			Controller.Instance.SaveSchedule(_localSchedule, nameChanged, true, this);
			return result;
		}

		public override ColorSchema GetColorSchema(string colorName)
		{
			return BusinessObjects.Instance.OutputManager.CalendarColors.Items
				.Where(color => color.Name.ToLower() == colorName.ToLower())
				.Select(color => color.Schema)
				.FirstOrDefault();
		}

		public override void OpenHelp(string key)
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink(key);
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
					SaveCalendarData(false);
					result = true;
				}
				else
					result = true;
				return result;
			}
		}

		protected override void PowerPointInternal(IEnumerable<CalendarOutputData> outputData)
		{
			if (outputData == null) return;
			var commonOutputData = outputData.OfType<AdCalendarOutputData>();
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.SetTitle(commonOutputData.Count() == 2 ? "Creating 2 (two) Calendar slides…\nThis will take about a minute…" : "Creating Calendar slides…\nThis will take a few minutes…");
				FormProgress.ShowProgress();
				Enabled = false;
				AdSchedulePowerPointHelper.Instance.AppendCalendar(commonOutputData.ToArray());
				Enabled = true;
				FormProgress.CloseProgress();
			});
		}

		protected override void EmailInternal(IEnumerable<CalendarOutputData> outputData)
		{
			if (outputData == null) return;
			var commonOutputData = outputData.OfType<AdCalendarOutputData>();
			var previewGroups = new List<PreviewGroup>();
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Calendar for Email...");
			FormProgress.ShowProgress();
			Enabled = false;
			foreach (var outputItem in commonOutputData)
			{
				var previewGroup = new PreviewGroup
				{
					Name = outputItem.MonthText,
					PresentationSourcePath = Path.Combine(Core.Common.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
				};
				AdSchedulePowerPointHelper.Instance.PrepareCalendarEmail(previewGroup.PresentationSourcePath, new[] { outputItem });
				previewGroups.Add(previewGroup);
			}
			Enabled = true;
			FormProgress.CloseProgress();
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formEmail = new FormEmail(AdSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager))
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
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Calendar for Preview...");
			FormProgress.ShowProgress();
			Enabled = false;
			foreach (var outputItem in commonOutputData)
			{
				var previewGroup = new PreviewGroup
				{
					Name = outputItem.MonthText,
					PresentationSourcePath = Path.Combine(Core.Common.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
				};
				AdSchedulePowerPointHelper.Instance.PrepareCalendarEmail(previewGroup.PresentationSourcePath, new[] { outputItem });
				previewGroups.Add(previewGroup);
			}
			Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
			Enabled = true;
			FormProgress.CloseProgress();
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, AdSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager, Controller.Instance.ShowFloater))
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
			var commonOutputData = outputData.OfType<AdCalendarOutputData>();
			var previewGroups = new List<PreviewGroup>();
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.SetTitle(outputData.Count() == 2 ? "Creating 2 (two) Calendar slides…\nThis will take about a minute…" : "Creating Calendar slides…\nThis will take a few minutes…");
				FormProgress.ShowProgress();
				Enabled = false;
				foreach (var outputItem in commonOutputData)
				{
					var previewGroup = new PreviewGroup
					{
						Name = outputItem.MonthText,
						PresentationSourcePath = Path.Combine(Core.Common.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
					};
					AdSchedulePowerPointHelper.Instance.PrepareCalendarEmail(previewGroup.PresentationSourcePath, new[] { outputItem });
					previewGroups.Add(previewGroup);
				}
				var pdfFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}-{1}.pdf", _localSchedule.Name, DateTime.Now.ToString("MM-dd-yy-hmmss")));
				AdSchedulePowerPointHelper.Instance.BuildPdf(pdfFileName, previewGroups.Select(pg => pg.PresentationSourcePath));
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

		private void hyperLinkEditReset_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("Are you sure want to reset data?") == DialogResult.Yes)
			{
				((AdCalendar)CalendarData).ResetToDefault();
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
