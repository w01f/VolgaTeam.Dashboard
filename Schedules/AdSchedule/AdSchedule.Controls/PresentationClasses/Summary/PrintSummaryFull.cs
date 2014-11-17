﻿using System;
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
	public class PrintSummaryFull : SummaryFullControl
	{
		public Schedule LocalSchedule { get; set; }

		public AdFullSummarySettings AdFullSummary
		{
			get { return (AdFullSummarySettings)Schedule.CustomSummary; }
		}

		public PrintSummaryFull()
		{
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate()
			{
				if (sender != this)
					LoadData(e.QuickSave);
			});
		}

		private void TrackOutput()
		{
			BusinessWrapper.Instance.ActivityManager.AddActivity(new OutputActivity(Controller.Instance.TabSummaryFull.Text, LocalSchedule.BusinessName, null));
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
			FormThemeSelector.Link(Controller.Instance.SummaryFullTheme, BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType.Summary2), BusinessWrapper.Instance.GetSelectedTheme(SlideType.Summary2), (t =>
			{
				BusinessWrapper.Instance.SetSelectedTheme(SlideType.Summary2, t.Name);
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
			get { return BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType.Summary2).FirstOrDefault(t => t.Name.Equals(BusinessWrapper.Instance.GetSelectedTheme(SlideType.Summary2)) || String.IsNullOrEmpty(BusinessWrapper.Instance.GetSelectedTheme(SlideType.Summary2))); }
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
					AdSchedulePowerPointHelper.Instance.AppendSummary(this);
					formProgress.Close();
				});
			}
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