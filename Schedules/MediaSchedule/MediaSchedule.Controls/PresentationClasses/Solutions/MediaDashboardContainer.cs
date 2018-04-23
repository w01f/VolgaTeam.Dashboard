using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Solutions;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Dashboard.Configuration;
using Asa.Business.Solutions.Dashboard.Entities.NonPersistent;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.Core.OfficeInterops;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.ToolForms;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Solutions.Dashboard.PresentationClasses.ContentEditors;
using DevExpress.XtraPrinting.Native;
using RegistryHelper = Asa.Common.Core.Helpers.RegistryHelper;

namespace Asa.Media.Controls.PresentationClasses.Solutions
{
	class MediaDashboardContainer : BaseDashboardContainer
	{
		public override PowerPointProcessor PowerPointProcessor => BusinessObjects.Instance.PowerPointManager.Processor;

		public MediaDashboardContainer(BaseSolutionInfo solutionInfo) : base(solutionInfo) { }

		public override IDashboardSettingsContainer SettingsContainer => MediaMetaData.Instance.SettingsManager;
		public override Form MainForm => Controller.Instance.FormMain;
		public override Color? AccentColor => BusinessObjects.Instance.FormStyleManager.Style.AccentColor;

		public override void LoadData()
		{
			EditedContent?.Dispose();
			EditedContent = BusinessObjects.Instance.ScheduleManager.ActiveSchedule
				.GetScheduleSolutionContent<MediaDashboardContent>(SolutionInfo)
				.Clone<MediaDashboardContent, DashboardContent>();
			base.LoadData();
		}

		public override void SaveData()
		{
			BusinessObjects.Instance.ScheduleManager.ActiveSchedule
				.ApplyScheduleSolutionContent(SolutionInfo, EditedContent.Clone<MediaDashboardContent, DashboardContent>());
		}

		public override Theme GetSelectedTheme(SlideType slideType)
		{
			return MediaMetaData.Instance.SettingsManager.GetSelectedTheme(slideType);
		}

		public override void OutputPowerPoint()
		{
			var slideInfos = GetOutputSlides();
			if (!slideInfos.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			FormProgress.ShowOutputProgress();
			Controller.Instance.ShowFloater(() =>
			{
				slideInfos.ForEach(s => s.SlideContainer.GenerateOutput(s));
				FormProgress.CloseProgress();
			});
		}

		public override void OutputPdf()
		{
			var slides = GetOutputSlides();
			if (!slides.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			FormProgress.ShowOutputProgress();
			Controller.Instance.ShowFloater(() =>
			{
				var previewGroups = slides.Select(s => s.SlideContainer.GeneratePreview(s)).ToList();
				var pdfFileName = Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
					String.Format("{0}-{1}.pdf", SolutionInfo.ToggleTitle, DateTime.Now.ToString("MM-dd-yy-hmmss")));
				PowerPointProcessor.BuildPdf(pdfFileName, previewGroups.Select(pg => pg.PresentationSourcePath));
				if (File.Exists(pdfFileName))
					try
					{
						Process.Start(pdfFileName);
					}
					catch { }
				FormProgress.CloseProgress();
			});
		}

		public override void Preview()
		{
			var slides = GetOutputSlides();
			if (!slides.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress(Controller.Instance.FormMain);
			var previewGroups = slides.Select(s => s.SlideContainer.GeneratePreview(s)).ToList();
			Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
			FormProgress.CloseProgress();

			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;

			using (var formPreview = new FormPreview(
				Controller.Instance.FormMain,
				PowerPointProcessor,
				BusinessObjects.Instance.HelpManager,
				Controller.Instance.ShowFloater,
				Controller.Instance.CheckPowerPointRunning))
			{
				formPreview.Text = "Preview Solution";
				formPreview.LoadGroups(previewGroups);
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
			}
		}

		public override void Email()
		{
			var slides = GetOutputSlides();
			if (!slides.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Solution...");
			FormProgress.ShowProgress(Controller.Instance.FormMain);
			var previewGroups = slides.Select(s => s.SlideContainer.GeneratePreview(s)).ToList();
			Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
			FormProgress.CloseProgress();

			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formEmail = new FormEmail(PowerPointProcessor, BusinessObjects.Instance.HelpManager))
			{
				formEmail.Text = "Email this Schedule";
				formEmail.LoadGroups(previewGroups);
				Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
				RegistryHelper.MainFormHandle = formEmail.Handle;
				RegistryHelper.MaximizeMainForm = false;
				formEmail.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
			}
		}
	}
}
