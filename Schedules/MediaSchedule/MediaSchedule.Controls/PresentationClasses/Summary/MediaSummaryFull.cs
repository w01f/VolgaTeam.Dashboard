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
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;
using NewBizWiz.MediaSchedule.Controls.InteropClasses;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.Summary
{
	public class MediaSummaryFull : SummaryFullControl
	{
		public RegularSchedule LocalSchedule { get; set; }

		public MediaFullSummarySettings MediaFullSummary
		{
			get { return (MediaFullSummarySettings)Schedule.CustomSummary; }
		}

		public MediaSummaryFull()
		{
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate()
			{
				if (sender != this)
					LoadData(e.QuickSave && !e.UpdateDigital);
			});
		}

		private void TrackOutput()
		{
			var options = new Dictionary<string, object>();
			options.Add("Slide", Controller.Instance.TabSummaryFull.Text);
			options.Add("Advertiser", LocalSchedule.BusinessName);
			if (LocalSchedule.Section.Programs.Any())
			{
				options.Add("TotalSpots", LocalSchedule.Section.TotalSpots);
				options.Add("AverageRate", LocalSchedule.Section.AvgRate);
				options.Add("GrossInvestment", LocalSchedule.Section.TotalCost);
			}
			BusinessWrapper.Instance.ActivityManager.AddActivity(new UserActivity("Output", options));
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
			get { return Schedule.CustomSummary.Items; }
		}

		public override HelpManager HelpManager
		{
			get { return BusinessWrapper.Instance.HelpManager; }
		}

		public override CheckEdit TableOutputToggle
		{
			get { return Controller.Instance.SummaryFullTableOutputToggle; }
		}

		public override void LoadData(bool quickLoad)
		{
			LocalSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			checkEditBusinessName.Text = String.Format("{0}", LocalSchedule.BusinessName);
			checkEditDecisionMaker.Text = String.Format("{0}", LocalSchedule.DecisionMaker);
			laPresentationDate.Text = String.Format("{0}", LocalSchedule.PresentationDate.HasValue ? LocalSchedule.PresentationDate.Value.ToString("MM/dd/yyyy") : String.Empty);
			laFlightDates.Text = String.Format("{0}", LocalSchedule.FlightDates);
			FormThemeSelector.Link(Controller.Instance.SummaryFullTheme,
				BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType.Summary2),
				MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType.Summary2),
				(t =>
			{
				MediaMetaData.Instance.SettingsManager.SetSelectedTheme(SlideType.Summary2, t.Name);
				MediaMetaData.Instance.SettingsManager.SaveSettings();
				SettingsNotSaved = true;
			}));
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

		protected override void InitItem(SummaryCustomItemControl item)
		{
			base.InitItem(item);
			item.DataChanged += (o, e) => { MediaFullSummary.IsDefaultSate = false; };
		}

		protected override void OnAddItem(object sender, EventArgs e)
		{
			base.OnAddItem(sender, e);
			MediaFullSummary.IsDefaultSate = false;
		}

		protected override void ItemOnItemDeleted(object sender, SummaryItemEventArgs e)
		{
			base.ItemOnItemDeleted(sender, e);
			MediaFullSummary.IsDefaultSate = false;
		}

		protected override void ItemOnItemPositionChanged(object sender, SummaryItemEventArgs e)
		{
			base.ItemOnItemPositionChanged(sender, e);
			MediaFullSummary.IsDefaultSate = false;
		}

		public void Save_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			Utilities.Instance.ShowInformation("Schedule Saved");
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
			HelpManager.OpenHelpLink("summary2");
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

		public override Theme SelectedTheme
		{
			get { return BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType.Summary2).FirstOrDefault(t => t.Name.Equals(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType.Summary2)) || String.IsNullOrEmpty(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType.Summary2))); }
		}

		protected override void Output()
		{
			SaveSchedule();
			TrackOutput();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				Controller.Instance.ShowFloater(() =>
				{
					formProgress.Show();
					RegularMediaSchedulePowerPointHelper.Instance.AppendSummary(this);
					formProgress.Close();
				});
			}
		}

		protected override void PreparePreview(string tempFileName)
		{
			RegularMediaSchedulePowerPointHelper.Instance.PrepareSummaryEmail(tempFileName, this);
		}

		protected override void ShowEmail(string tempFileName)
		{
			using (var formEmail = new FormEmail(RegularMediaSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager))
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
			options.Add("Slide", Controller.Instance.TabSummaryFull.Text);
			options.Add("Advertiser", LocalSchedule.BusinessName);
			if (LocalSchedule.Section.Programs.Any())
			{
				options.Add("TotalSpots", LocalSchedule.Section.TotalSpots);
				options.Add("AverageRate", LocalSchedule.Section.AvgRate);
				options.Add("GrossInvestment", LocalSchedule.Section.TotalCost);
			}
			BusinessWrapper.Instance.ActivityManager.AddActivity(new UserActivity("Preview", options));
		}

		protected override void ShowPreview(string tempFileName)
		{
			Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, RegularMediaSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager, Controller.Instance.ShowFloater, TrackPreview))
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
