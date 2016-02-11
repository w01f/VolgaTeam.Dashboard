using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Enums;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Business.Online.Interfaces;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.Themes;
using Asa.Common.GUI.ToolForms;
using Asa.Online.Controls.PresentationClasses.Packages;
using DevComponents.DotNetBar;
using Asa.Media.Controls.BusinessClasses;
using Asa.Media.Controls.InteropClasses;
using Asa.Online.Controls.InteropClasses;

namespace Asa.Media.Controls.PresentationClasses.Digital
{
	public class MediaWebPackageControl : WebPackageControl<DigitalProductsContent, IDigitalSchedule<IDigitalScheduleSettings>, IDigitalScheduleSettings, MediaScheduleChangeInfo>
	{
		protected override IDigitalSchedule<IDigitalScheduleSettings> Schedule
		{
			get { return BusinessObjects.Instance.ScheduleManager.ActiveSchedule; }
		}

		public override string Identifier
		{
			get { return ContentIdentifiers.DigitalPackages; }
		}

		public override RibbonTabItem TabPage
		{
			get { return Controller.Instance.TabDigitalPackage; }
		}

		public override Form MainForm
		{
			get { return Controller.Instance.FormMain; }
		}

		#region BasePartitionEditControl Override
		public override void InitControl()
		{
			base.InitControl();
			BusinessObjects.Instance.ThemeManager.ThemesChanged += (o, e) => OnOuterThemeChanged();
		}

		protected override void UpdateEditedContet()
		{
			var quickLoad = EditedContent != null && !(ContentUpdateInfo.ChangeInfo.WholeScheduleChanged ||
				ContentUpdateInfo.ChangeInfo.DigitalContentChanged);

			if (quickLoad) return;

			if (EditedContent != null)
				EditedContent.Dispose();
			EditedContent = Schedule.DigitalProductsContent.Clone<DigitalProductsContent, DigitalProductsContent>();

			base.UpdateEditedContet();
		}

		protected override void ApplyChanges()
		{
			base.ApplyChanges();
			ChangeInfo.DigitalContentChanged = ChangeInfo.DigitalContentChanged || SettingsNotSaved;
		}

		protected override void SaveData()
		{
			Schedule.DigitalProductsContent = EditedContent.Clone<DigitalProductsContent, DigitalProductsContent>();
			base.SaveData();
		}

		public override void GetHelp()
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink("digitalpk");
		}
		protected override void LoadThemes()
		{
			base.LoadThemes();
			FormThemeSelector.Link(Controller.Instance.DigitalPackageTheme, BusinessObjects.Instance.ThemeManager.GetThemes(MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? SlideType.TVWebPackage : SlideType.RadioWebPackage), MediaMetaData.Instance.SettingsManager.GetSelectedTheme(MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? SlideType.TVWebPackage : SlideType.RadioWebPackage), (t =>
			{
				MediaMetaData.Instance.SettingsManager.SetSelectedTheme(MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? SlideType.TVWebPackage : SlideType.RadioWebPackage, t.Name);
				MediaMetaData.Instance.SettingsManager.SaveSettings();
				IsThemeChanged = true;
			}));
			Controller.Instance.DigitalPackageThemeBar.RecalcLayout();
			Controller.Instance.DigitalPackagePanel.PerformLayout();
		}
		#endregion

		#region Output Stuff
		public override Theme SelectedTheme
		{
			get { return BusinessObjects.Instance.ThemeManager.GetThemes(MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? SlideType.TVWebPackage : SlideType.RadioWebPackage).FirstOrDefault(t => t.Name.Equals(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? SlideType.TVWebPackage : SlideType.RadioWebPackage)) || String.IsNullOrEmpty(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? SlideType.TVWebPackage : SlideType.RadioWebPackage))); }
		}

		protected override void GetDisabledOutputInfo()
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink("dgpkg");
		}

		protected override void UpdateOutputState()
		{
			var enabled = IsOutputEnabled;
			Controller.Instance.DigitalPackagePowerPoint.Enabled =
				Controller.Instance.DigitalPackagePdf.Enabled =
					Controller.Instance.DigitalPackagePreview.Enabled =
						Controller.Instance.DigitalPackageEmail.Enabled = enabled;
		}

		protected override void OutputPowerPointSlides()
		{
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				OnlineSchedulePowerPointHelper.Instance.AppendWebPackage(this);
				FormProgress.CloseProgress();
			});
		}

		protected override void OutputPdfSlides()
		{
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				var pdfFileName = Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
					String.Format("{0}-{1}.pdf",
						Schedule.Name, DateTime.Now.ToString("MM-dd-yy-hmmss")));
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

		protected override void PreviewSlides(string tempFileName)
		{
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, RegularMediaSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager, Controller.Instance.ShowFloater))
			{
				formPreview.Text = "Preview Digital Package";
				formPreview.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = MainForm.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = MainForm.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.ActivateForm(MainForm.Handle, MainForm.WindowState == FormWindowState.Maximized, false);
			}
		}

		protected override void EmailSlides(string tempFileName)
		{
			using (var formEmail = new FormEmail(OnlineSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager))
			{
				formEmail.Text = "Email this Online Schedule";
				formEmail.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
				Utilities.ActivateForm(MainForm.Handle, MainForm.WindowState == FormWindowState.Maximized, false);
				RegistryHelper.MainFormHandle = formEmail.Handle;
				RegistryHelper.MaximizeMainForm = false;
				formEmail.ShowDialog();
				RegistryHelper.MaximizeMainForm = MainForm.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = MainForm.Handle;
			}
		}
		#endregion
	}
}