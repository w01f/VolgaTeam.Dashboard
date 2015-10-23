using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Asa.AdSchedule.Controls.BusinessClasses;
using Asa.AdSchedule.Controls.InteropClasses;
using Asa.CommonGUI.Preview;
using Asa.CommonGUI.Themes;
using Asa.CommonGUI.ToolForms;
using Asa.Core.Common;
using Asa.Core.OnlineSchedule;
using Asa.OnlineSchedule.Controls.InteropClasses;
using Asa.OnlineSchedule.Controls.PresentationClasses;
using Schedule = Asa.Core.AdSchedule.Schedule;

namespace Asa.AdSchedule.Controls.PresentationClasses
{
	public class AdWebPackageControl : WebPackageControl
	{
		public AdWebPackageControl(Form form)
			: base(form)
		{
			BusinessObjects.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.BeginInvoke((MethodInvoker)delegate()
			{
				if (sender != this)
					LoadSchedule(e.QuickSave);
			});
			BusinessObjects.Instance.ThemeManager.ThemesChanged += (o, e) =>
			{
				InitThemeSelector();
				Controller.Instance.DigitalPackageThemeBar.RecalcLayout();
				Controller.Instance.DigitalPackagePanel.PerformLayout();
			};
		}

		public Schedule LocalSchedule { get; set; }

		public override Theme SelectedTheme
		{
			get { return BusinessObjects.Instance.ThemeManager.GetThemes(SlideType.PrintWebPackage).FirstOrDefault(t => t.Name.Equals(Core.AdSchedule.SettingsManager.Instance.GetSelectedTheme(SlideType.PrintWebPackage)) || String.IsNullOrEmpty(Core.AdSchedule.SettingsManager.Instance.GetSelectedTheme(SlideType.PrintWebPackage))); }
		}

		public override HelpManager HelpManager
		{
			get { return BusinessObjects.Instance.HelpManager; }
		}

		public override ISchedule Schedule { get { return LocalSchedule; } }

		public override DigitalPackageSettings Settings
		{
			get { return LocalSchedule.ViewSettings.DigitalPackageSettings; }
		}
		public override IEnumerable<ProductPackageRecord> PackageRecords
		{
			get { return LocalSchedule.DigitalProducts.OrderBy(p => p.Index).Select(p => p.PackageRecord).ToList(); }
		}

		public override ButtonItem Preview
		{
			get { return Controller.Instance.DigitalPackagePreview; }
		}

		public override ButtonItem PowerPoint
		{
			get { return Controller.Instance.DigitalPackagePowerPoint; }
		}

		public override ButtonItem Pdf
		{
			get { return Controller.Instance.DigitalPackagePdf; }
		}

		public override ButtonItem Email
		{
			get { return Controller.Instance.DigitalPackageEmail; }
		}

		public override ButtonItem Theme
		{
			get { return Controller.Instance.DigitalPackageTheme; }
		}

		public override void LoadSchedule(bool quickLoad)
		{
			LocalSchedule = BusinessObjects.Instance.ScheduleManager.GetLocalSchedule();
			InitThemeSelector();
			base.LoadSchedule(quickLoad);
		}

		protected override bool SaveSchedule(string scheduleName = "")
		{
			var nameChanged = !string.IsNullOrEmpty(scheduleName);
			if (nameChanged)
				LocalSchedule.Name = scheduleName;
			Controller.Instance.SaveSchedule(LocalSchedule, nameChanged, false, this);
			return base.SaveSchedule(scheduleName);
		}

		protected override IEnumerable<string> GetExistedScheduleNames()
		{
			return Core.AdSchedule.ScheduleManager.GetShortScheduleList().Select(s => s.ShortFileName);
		}

		private void InitThemeSelector()
		{
			FormThemeSelector.Link(Controller.Instance.DigitalPackageTheme, BusinessObjects.Instance.ThemeManager.GetThemes(SlideType.PrintWebPackage), Core.AdSchedule.SettingsManager.Instance.GetSelectedTheme(SlideType.PrintWebPackage), (t =>
			{
				Core.AdSchedule.SettingsManager.Instance.SetSelectedTheme(SlideType.PrintWebPackage, t.Name);
				Core.AdSchedule.SettingsManager.Instance.SaveSettings();
				SettingsNotSaved = true;
			}));
		}

		public override void Help_Click(object sender, EventArgs e)
		{
			HelpManager.OpenHelpLink("digitalpkg");
		}

		private void TrackOutput()
		{
			BusinessObjects.Instance.ActivityManager.AddActivity(new OutputActivity(Controller.Instance.TabDigitalPackage.Text, Schedule.BusinessName, PackageRecords.Sum(pr => pr.InvestmentCalculated)));
		}

		public override void OutputSlides()
		{
			TrackOutput();
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				OnlineSchedulePowerPointHelper.Instance.AppendWebPackage(this);
				FormProgress.CloseProgress();
			});
		}

		public override void ShowPreview(string tempFileName)
		{
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, AdSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager, Controller.Instance.ShowFloater, TrackOutput))
			{
				formPreview.Text = "Preview Digital Package";
				formPreview.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = _formContainer.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = _formContainer.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.Instance.ActivateForm(_formContainer.Handle, true, false);
			}
		}

		public override void PdfSlides()
		{
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				var pdfFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}-{1}.pdf", LocalSchedule.Name, DateTime.Now.ToString("MM-dd-yy-hmmss")));
				OnlineSchedulePowerPointHelper.Instance.PrepareWebPackagePdf(this, pdfFileName);
				if (File.Exists(pdfFileName))
					try
					{
						Process.Start(pdfFileName);
					}
					catch { }
				FormProgress.CloseProgress();
			});
		}
	}
}