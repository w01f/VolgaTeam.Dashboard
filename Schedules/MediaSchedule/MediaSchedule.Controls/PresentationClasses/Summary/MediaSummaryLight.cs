﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using Asa.CommonGUI.Preview;
using Asa.CommonGUI.Summary;
using Asa.CommonGUI.Themes;
using Asa.CommonGUI.ToolForms;
using Asa.Core.Common;
using Asa.Core.MediaSchedule;
using Asa.MediaSchedule.Controls.BusinessClasses;
using Asa.MediaSchedule.Controls.InteropClasses;

namespace Asa.MediaSchedule.Controls.PresentationClasses.Summary
{
	public class MediaSummaryLight : SummaryLightControl
	{
		public RegularSchedule LocalSchedule { get; set; }

		public MediaSummaryLight()
		{
			BusinessObjects.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.BeginInvoke((MethodInvoker)delegate()
			{
				if (sender != this)
					LoadData(e.QuickSave && !e.UpdateDigital);
			});
			BusinessObjects.Instance.ThemeManager.ThemesChanged += (o, e) =>
			{
				InitThemeSelector();
				Controller.Instance.SummaryLightThemeBar.RecalcLayout();
				Controller.Instance.SummaryLightPanel.PerformLayout();
			};
		}

		private void TrackOutput()
		{
			var options = new Dictionary<string, object>();
			options.Add("Slide", Controller.Instance.TabSummaryLight.Text);
			options.Add("Advertiser", LocalSchedule.BusinessName);
			if (LocalSchedule.Section.Programs.Any())
			{
				options.Add("TotalSpots", LocalSchedule.Section.TotalSpots);
				options.Add("AverageRate", LocalSchedule.Section.AvgRate);
				options.Add("GrossInvestment", LocalSchedule.Section.TotalCost);
			}
			BusinessObjects.Instance.ActivityManager.AddActivity(new UserActivity("Output", options));
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
			InitThemeSelector();
			base.LoadData(quickLoad);
		}

		protected override bool SaveSchedule(string scheduleName = "")
		{
			var nameChanged = !string.IsNullOrEmpty(scheduleName);
			if (nameChanged)
				LocalSchedule.Name = scheduleName;
			Controller.Instance.SaveSchedule(LocalSchedule, nameChanged, false, false, false, this);
			SettingsNotSaved = false;
			return true;
		}

		private void InitThemeSelector()
		{
			FormThemeSelector.Link(Controller.Instance.SummaryLightTheme,
				BusinessObjects.Instance.ThemeManager.GetThemes(SlideType.Summary1),
				MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType.Summary1),
				(t =>
				{
					MediaMetaData.Instance.SettingsManager.SetSelectedTheme(SlideType.Summary1, t.Name);
					MediaMetaData.Instance.SettingsManager.SaveSettings();
					SettingsNotSaved = true;
				}));
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
			get { return Controller.Instance.SummaryLightPdf; }
		}

		public override StorageDirectory ContractTemplateFolder
		{
			get { return BusinessObjects.Instance.OutputManager.ContractTemplateFolder; }
		}

		public override Theme SelectedTheme
		{
			get { return BusinessObjects.Instance.ThemeManager.GetThemes(SlideType.Summary1).FirstOrDefault(t => t.Name.Equals(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType.Summary1)) || String.IsNullOrEmpty(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType.Summary1))); }
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
				RegularMediaSchedulePowerPointHelper.Instance.AppendSummary(this);
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
				RegularMediaSchedulePowerPointHelper.Instance.PrepareSummaryPdf(pdfFileName, this);
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
			RegularMediaSchedulePowerPointHelper.Instance.PrepareSummaryEmail(tempFileName, this);
		}

		protected override void ShowEmail(string tempFileName)
		{
			using (var formEmail = new FormEmail(RegularMediaSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager))
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

		private void TrackPreview()
		{
			var options = new Dictionary<string, object>();
			options.Add("Slide", Controller.Instance.TabSummaryLight.Text);
			options.Add("Advertiser", LocalSchedule.BusinessName);
			if (LocalSchedule.Section.Programs.Any())
			{
				options.Add("TotalSpots", LocalSchedule.Section.TotalSpots);
				options.Add("AverageRate", LocalSchedule.Section.AvgRate);
				options.Add("GrossInvestment", LocalSchedule.Section.TotalCost);
			}
			BusinessObjects.Instance.ActivityManager.AddActivity(new UserActivity("Preview", options));
		}

		protected override void ShowPreview(string tempFileName)
		{
			Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, RegularMediaSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager, Controller.Instance.ShowFloater, TrackPreview))
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
