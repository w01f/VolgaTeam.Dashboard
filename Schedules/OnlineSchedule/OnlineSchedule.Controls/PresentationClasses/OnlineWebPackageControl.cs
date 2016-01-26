using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Asa.CommonGUI.Preview;
using Asa.CommonGUI.Themes;
using Asa.CommonGUI.ToolForms;
using Asa.Core.Common;
using Asa.Core.OnlineSchedule;
using Asa.OnlineSchedule.Controls.BusinessClasses;
using Asa.OnlineSchedule.Controls.InteropClasses;

namespace Asa.OnlineSchedule.Controls.PresentationClasses
{
	public class OnlineWebPackageControl : WebPackageControl
	{
		public Schedule LocalSchedule { get; set; }

		public override Theme SelectedTheme
		{
			get { return BusinessObjects.Instance.ThemeManager.GetThemes(SlideType.OnlineWebPackage).FirstOrDefault(t => t.Name.Equals(BusinessObjects.Instance.GetSelectedTheme(SlideType.OnlineWebPackage)) || String.IsNullOrEmpty(BusinessObjects.Instance.GetSelectedTheme(SlideType.OnlineWebPackage))); }
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
			get { return LocalSchedule.DigitalProducts.Select(p => p.PackageRecord).ToList(); }
		}

		public override ButtonItem Preview
		{
			get { return Controller.Instance.DigitalPackagePreview; }
		}

		public override ButtonItem PowerPoint
		{
			get { return Controller.Instance.DigitalPackagePowerPoint; }
		}

		public override ButtonItem Email
		{
			get { return Controller.Instance.DigitalPackageEmail; }
		}

		public override ButtonItem Theme
		{
			get { return Controller.Instance.DigitalPackageTheme; }
		}

		public OnlineWebPackageControl(Form form)
			: base(form)
		{
			BusinessObjects.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.BeginInvoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadSchedule(e.QuickSave);
			});
		}

		public override void LoadSchedule(bool quickLoad)
		{
			LocalSchedule = BusinessObjects.Instance.ScheduleManager.GetLocalSchedule();
			FormThemeSelector.Link(Controller.Instance.DigitalPackageTheme, BusinessObjects.Instance.ThemeManager.GetThemes(SlideType.OnlineWebPackage), BusinessObjects.Instance.GetSelectedTheme(SlideType.OnlineWebPackage), (t =>
			{
				BusinessObjects.Instance.SetSelectedTheme(SlideType.OnlineWebPackage, t.Name);
				BusinessObjects.Instance.SaveLocalSettings();
				SettingsNotSaved = true;
			}));
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
			return ScheduleManager.GetShortScheduleList().Select(s => s.ShortFileName);
		}

		public override void OutputSlides()
		{
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
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, OnlineSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager, Controller.Instance.ShowFloater))
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

		public override void Help_Click(object sender, EventArgs e)
		{
			HelpManager.OpenHelpLink("digitalpk");
		}
	}
}
