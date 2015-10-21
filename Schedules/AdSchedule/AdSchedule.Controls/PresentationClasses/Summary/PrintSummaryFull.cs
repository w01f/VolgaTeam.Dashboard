using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
	public class PrintSummaryFull : SummaryFullControl
	{
		public Schedule LocalSchedule { get; set; }

		public AdFullSummarySettings AdFullSummary
		{
			get { return (AdFullSummarySettings)Schedule.CustomSummary; }
		}

		public PrintSummaryFull()
		{
			BusinessObjects.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.BeginInvoke((MethodInvoker)delegate()
			{
				if (sender != this)
					LoadData(e.QuickSave);
			});
			BusinessObjects.Instance.ThemeManager.ThemesChanged += (o, e) =>
			{
				InitThemeSelector();
				Controller.Instance.SummaryFullThemeBar.RecalcLayout();
				Controller.Instance.SummaryFullPanel.PerformLayout();
			};
		}

		private void TrackOutput()
		{
			BusinessObjects.Instance.ActivityManager.AddActivity(new OutputActivity(Controller.Instance.TabSummaryFull.Text, LocalSchedule.BusinessName, null));
		}

		public override ISummarySchedule Schedule
		{
			get { return LocalSchedule; }
		}

		public override BaseSummarySettings Settings
		{
			get { return LocalSchedule.CustomSummary; }
		}

		public override List<CustomSummaryItem> Items
		{
			get { return LocalSchedule.CustomSummary.Items; }
		}

		public override HelpManager HelpManager
		{
			get { return BusinessObjects.Instance.HelpManager; }
		}

		public override CheckEdit TableOutputToggle
		{
			get { return Controller.Instance.SummaryFullTableOutputToggle; }
		}

		public override void LoadData(bool quickLoad)
		{
			LocalSchedule = BusinessObjects.Instance.ScheduleManager.GetLocalSchedule();
			checkEditBusinessName.Text = String.Format("{0}", LocalSchedule.BusinessName);
			checkEditDecisionMaker.Text = String.Format("{0}", LocalSchedule.DecisionMaker);
			laPresentationDate.Text = String.Format("{0}", LocalSchedule.PresentationDate.HasValue ? LocalSchedule.PresentationDate.Value.ToString("MM/dd/yyyy") : String.Empty);
			laFlightDates.Text = String.Format("{0}", LocalSchedule.FlightDates);
			InitThemeSelector();
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

		private void InitThemeSelector()
		{
			FormThemeSelector.Link(Controller.Instance.SummaryFullTheme, BusinessObjects.Instance.ThemeManager.GetThemes(SlideType.Summary2), Core.AdSchedule.SettingsManager.Instance.GetSelectedTheme(SlideType.Summary2), (t =>
			{
				Core.AdSchedule.SettingsManager.Instance.SetSelectedTheme(SlideType.Summary2, t.Name);
				Core.AdSchedule.SettingsManager.Instance.SaveSettings();
				SettingsNotSaved = true;
			}));
		}

		protected override void InitItem(SummaryCustomItemControl item)
		{
			base.InitItem(item);
			item.DataChanged += (o, e) => { AdFullSummary.IsDefaultSate = false; };
		}

		protected override void OnAddItem(object sender, EventArgs e)
		{
			base.OnAddItem(sender, e);
			AdFullSummary.IsDefaultSate = false;
		}

		protected override void ItemOnItemDeleted(object sender, SummaryItemEventArgs e)
		{
			base.ItemOnItemDeleted(sender, e);
			AdFullSummary.IsDefaultSate = false;
		}

		protected override void ItemOnItemPositionChanged(object sender, SummaryItemEventArgs e)
		{
			base.ItemOnItemPositionChanged(sender, e);
			AdFullSummary.IsDefaultSate = false;
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
			get { return Controller.Instance.SummaryFullPowerPoint; }
		}

		public override ButtonItem PreviewButton
		{
			get { return Controller.Instance.SummaryFullPreview; }
		}

		public override ButtonItem EmailButton
		{
			get { return Controller.Instance.SummaryFullEmail; }
		}

		public override ButtonItem PdfButton
		{
			get { return Controller.Instance.SummaryFullPdf; }
		}

		public override Theme SelectedTheme
		{
			get { return BusinessObjects.Instance.ThemeManager.GetThemes(SlideType.Summary2).FirstOrDefault(t => t.Name.Equals(Core.AdSchedule.SettingsManager.Instance.GetSelectedTheme(SlideType.Summary2)) || String.IsNullOrEmpty(Core.AdSchedule.SettingsManager.Instance.GetSelectedTheme(SlideType.Summary2))); }
		}

		public override void OpenHelp()
		{
			HelpManager.OpenHelpLink("summary2");
		}

		protected override void Output()
		{
			SaveSchedule();
			if (!CheckPowerPointRunning()) return;
			TrackOutput();
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				AdSchedulePowerPointHelper.Instance.AppendSummary(this);
				FormProgress.CloseProgress();
			});
		}

		protected override void OutputPdf()
		{
			SaveSchedule();
			if (!CheckPowerPointRunning()) return;
			TrackOutput();
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				var pdfFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}-{1}.pdf", LocalSchedule.Name, DateTime.Now.ToString("MM-dd-yy-hmmss")));
				AdSchedulePowerPointHelper.Instance.PrepareSummaryPdf(pdfFileName, this);
				if (File.Exists(pdfFileName))
					try
					{
						Process.Start(pdfFileName);
					}
					catch { }
				FormProgress.CloseProgress();
			});
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
			using (var formEmail = new FormEmail(AdSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager))
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
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, AdSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager, Controller.Instance.ShowFloater, TrackOutput))
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
