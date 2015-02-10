using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.InteropClasses;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.CommonGUI.Summary;
using NewBizWiz.CommonGUI.Themes;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.Summary
{
	public class PrintSummaryLight : SummaryLightControl
	{
		public Schedule LocalSchedule { get; set; }

		public PrintSummaryLight()
		{
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.BeginInvoke((MethodInvoker)delegate()
			{
				if (sender != this)
					LoadData(e.QuickSave);
			});
		}

		private void TrackOutput()
		{
			BusinessWrapper.Instance.ActivityManager.AddActivity(new OutputActivity(Controller.Instance.TabSummaryLight.Text, LocalSchedule.BusinessName, null));
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
			get { return LocalSchedule.ProductSummaries.Select(ps => ps.SummaryItem).ToList(); }
		}

		public override HelpManager HelpManager
		{
			get { return BusinessWrapper.Instance.HelpManager; }
		}

		public override CheckEdit TableOutputToggle
		{
			get { return Controller.Instance.SummaryLightTableOutputToggle; }
		}

		public override void LoadData(bool quickLoad)
		{
			LocalSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			checkEditBusinessName.Text = String.Format("{0}", LocalSchedule.BusinessName);
			checkEditDecisionMaker.Text = String.Format("{0}", LocalSchedule.DecisionMaker);
			laPresentationDate.Text = String.Format("{0}", LocalSchedule.PresentationDate.HasValue ? LocalSchedule.PresentationDate.Value.ToString("MM/dd/yyyy") : String.Empty);
			laFlightDates.Text = String.Format("{0}", LocalSchedule.FlightDates);
			FormThemeSelector.Link(Controller.Instance.SummaryLightTheme, BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType.Summary1), BusinessWrapper.Instance.GetSelectedTheme(SlideType.Summary1), (t =>
			{
				BusinessWrapper.Instance.SetSelectedTheme(SlideType.Summary1, t.Name);
				BusinessWrapper.Instance.SaveLocalSettings();
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
			get { return BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType.Summary1).FirstOrDefault(t => t.Name.Equals(BusinessWrapper.Instance.GetSelectedTheme(SlideType.Summary1)) || String.IsNullOrEmpty(BusinessWrapper.Instance.GetSelectedTheme(SlideType.Summary1))); }
		}

		public override void OpenHelp()
		{
			HelpManager.OpenHelpLink("summary1");
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
					AdSchedulePowerPointHelper.Instance.AppendSummary(this);
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
			AdSchedulePowerPointHelper.Instance.PrepareSummaryEmail(tempFileName, this);
		}

		protected override void ShowEmail(string tempFileName)
		{
			using (var formEmail = new FormEmail(AdSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager))
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
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, AdSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager, Controller.Instance.ShowFloater, TrackOutput))
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
