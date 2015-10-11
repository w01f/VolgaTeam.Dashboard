using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.CommonGUI.Summary;
using NewBizWiz.CommonGUI.Themes;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.OnlineSchedule.Controls.BusinessClasses;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;

namespace NewBizWiz.OnlineSchedule.Controls.PresentationClasses
{
	public class DigitalSummaryLight : SummaryLightControl
	{
		public Schedule LocalSchedule { get; set; }

		public DigitalSummaryLight()
		{
			BusinessObjects.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.BeginInvoke((MethodInvoker)delegate()
			{
				if (sender != this)
					LoadData(e.QuickSave);
			});
		}

		private void TrackOutput()
		{
			BusinessObjects.Instance.ActivityManager.AddActivity(new OutputActivity(Controller.Instance.TabSummaryLight.Text, LocalSchedule.BusinessName, null));
		}

		public override ISummarySchedule Schedule
		{
			get { return LocalSchedule; }
		}

		public override BaseSummarySettings Settings
		{
			get { return LocalSchedule.ProductSummary; }
		}

		public override List<CustomSummaryItem> Items
		{
			get { return Schedule.ProductSummaries.Select(ps => ps.SummaryItem).ToList(); }
		}

		public override HelpManager HelpManager
		{
			get { return BusinessObjects.Instance.HelpManager; }
		}

		public override CheckEdit TableOutputToggle
		{
			get { return Controller.Instance.SummaryLightTableOutputToggle; }
		}

		public override void LoadData(bool quickLoad)
		{
			LocalSchedule = BusinessObjects.Instance.ScheduleManager.GetLocalSchedule();
			checkEditBusinessName.Text = String.Format("{0}", LocalSchedule.BusinessName);
			checkEditDecisionMaker.Text = String.Format("{0}", LocalSchedule.DecisionMaker);
			laPresentationDate.Text = String.Format("{0}", LocalSchedule.PresentationDate.HasValue ? LocalSchedule.PresentationDate.Value.ToString("MM/dd/yyyy") : String.Empty);
			laFlightDates.Text = String.Format("{0}", LocalSchedule.FlightDates);
			FormThemeSelector.Link(Controller.Instance.SummaryLightTheme, BusinessObjects.Instance.ThemeManager.GetThemes(SlideType.Summary1), BusinessObjects.Instance.GetSelectedTheme(SlideType.Summary1), (t =>
			{
				BusinessObjects.Instance.SetSelectedTheme(SlideType.Summary1, t.Name);
				BusinessObjects.Instance.SaveLocalSettings();
				SettingsNotSaved = true;
			}));
			base.LoadData(quickLoad);
		}

		protected override bool SaveSchedule(string scheduleName = "")
		{
			var nameChanged = !string.IsNullOrEmpty(scheduleName);
			if (nameChanged)
				LocalSchedule.Name = scheduleName;
			Controller.Instance.SaveSchedule(LocalSchedule, nameChanged, false, this);
			SettingsNotSaved = false;
			return true;
		}

		public void Save_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			Utilities.Instance.ShowInformation("Schedule Saved");
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
					if (SaveSchedule(form.ScheduleName))
						Utilities.Instance.ShowInformation("Schedule was saved");
				}
				else
				{
					Utilities.Instance.ShowWarning("Schedule Name can't be empty");
				}
			}
		}

		public override void OpenHelp()
		{
			HelpManager.OpenHelpLink("summary1");
		}

		public override ButtonItem PowerPointButton
		{
			get { return Controller.Instance.SummaryLightPowerPoint; }
		}

		public override ButtonItem PreviewButton
		{
			get { return Controller.Instance.SummaryLightPreview; }
		}

		public override ButtonItem EmailButton
		{
			get { return Controller.Instance.SummaryLightEmail; }
		}

		public override ButtonItem PdfButton
		{
			get { return null; }
		}

		public override Theme SelectedTheme
		{
			get { return BusinessObjects.Instance.ThemeManager.GetThemes(SlideType.Summary1).FirstOrDefault(t => t.Name.Equals(BusinessObjects.Instance.GetSelectedTheme(SlideType.Summary1)) || String.IsNullOrEmpty(BusinessObjects.Instance.GetSelectedTheme(SlideType.Summary1))); }
		}

		protected override void Output()
		{
			SaveSchedule();
			if (!CheckPowerPointRunning()) return;
			TrackOutput();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				Controller.Instance.ShowFloater(() =>
				{
					formProgress.Show();
					OnlineSchedulePowerPointHelper.Instance.AppendSummary(this);
					formProgress.Close();
				});
			}
		}

		protected override void OutputPdf()
		{
			throw new NotImplementedException();
		}

		protected override bool CheckPowerPointRunning()
		{
			return Controller.Instance.CheckPowerPointRunning();
		}

		protected override void PreparePreview(string tempFileName)
		{
			OnlineSchedulePowerPointHelper.Instance.PrepareSummaryEmail(tempFileName, this);
		}

		protected override void ShowEmail(string tempFileName)
		{
			using (var formEmail = new FormEmail(OnlineSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager))
			{
				formEmail.Text = "Email this Summary";
				formEmail.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				RegistryHelper.MainFormHandle = formEmail.Handle;
				RegistryHelper.MaximizeMainForm = false;
				formEmail.ShowDialog();
				RegistryHelper.MaximizeMainForm = true;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
			}
		}

		protected override void ShowPreview(string tempFileName)
		{
			Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, OnlineSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager, Controller.Instance.ShowFloater, TrackOutput))
			{
				formPreview.Text = "Preview Summary";
				formPreview.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				DialogResult previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
			}
		}
	}
}
