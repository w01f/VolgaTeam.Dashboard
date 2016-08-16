using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Dashboard.Configuration;
using Asa.Business.Solutions.Dashboard.Entities.NonPersistent;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.ToolForms;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.InteropClasses;
using Asa.Solutions.Dashboard.PresentationClasses.ContentEditors;
using DevExpress.XtraPrinting.Native;
using RegistryHelper = Asa.Common.Core.Helpers.RegistryHelper;

namespace Asa.Media.Controls.PresentationClasses.Solutions
{
	class MediaDashboardContainer : BaseDashboardContainer
	{
		public MediaDashboardContainer(BaseSolutionInfo solutionInfo) : base(solutionInfo) { }

		public override IDashboardSettingsContainer SettingsContainer => MediaMetaData.Instance.SettingsManager;

		public override void LoadData()
		{
			EditedContent?.Dispose();
			EditedContent = BusinessObjects.Instance.ScheduleManager.ActiveSchedule
				.GetScheduleSolutionContent<DashboardContent>(SolutionInfo.Type)
				.Clone<DashboardContent, DashboardContent>();
			base.LoadData();
		}

		public override void SaveData()
		{
			BusinessObjects.Instance.ScheduleManager.ActiveSchedule
				.ApplyScheduleSolutionContent(SolutionInfo.Type, EditedContent.Clone<DashboardContent, DashboardContent>());
		}

		public override Theme GetSelectedTheme(SlideType slideType)
		{
			return MediaMetaData.Instance.SettingsManager.GetSelectedTheme(slideType);
		}

		public override void OutputPowerPoint()
		{
			var slides = GetOutputSlides();
			if (!slides.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				slides.ForEach(s => s.GenerateOutput());
				FormProgress.CloseProgress();
			});
		}

		public override void OutputPdf()
		{
			var slides = GetOutputSlides();
			if (!slides.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				var previewGroups = slides.Select(s => s.GeneratePreview()).ToList();
				var pdfFileName = Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
					String.Format("{0}-{1}.pdf", SolutionInfo.Title, DateTime.Now.ToString("MM-dd-yy-hmmss")));
				RegularMediaSchedulePowerPointHelper.Instance.BuildPdf(pdfFileName, previewGroups.Select(pg => pg.PresentationSourcePath));
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
			FormProgress.ShowProgress();
			var previewGroups = slides.Select(s => s.GeneratePreview()).ToList();
			Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
			FormProgress.CloseProgress();

			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;

			using (var formPreview = new FormPreview(
				Controller.Instance.FormMain,
				RegularMediaSchedulePowerPointHelper.Instance,
				BusinessObjects.Instance.HelpManager,
				Controller.Instance.ShowFloater))
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
			FormProgress.ShowProgress();
			var previewGroups = slides.Select(s => s.GeneratePreview()).ToList();
			Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
			FormProgress.CloseProgress();

			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formEmail = new FormEmail(RegularMediaSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager))
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
