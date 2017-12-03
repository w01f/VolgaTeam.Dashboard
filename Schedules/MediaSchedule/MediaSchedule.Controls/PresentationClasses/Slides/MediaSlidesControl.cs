using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Slides;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.Slides;
using Asa.Common.GUI.ToolForms;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Schedules.Common.Controls.ContentEditors.Events;
using Asa.Schedules.Common.Controls.ContentEditors.Helpers;
using Asa.Schedules.Common.Controls.ContentEditors.Interfaces;
using DevComponents.DotNetBar;
using DevExpress.Skins;

namespace Asa.Media.Controls.PresentationClasses.Slides
{
	[ToolboxItem(false)]
	public partial class MediaSlidesControl : UserControl, IContentControl, IOutputControl
	{
		private SlidesContainerControl _slideContainer;
		public bool IsActive { get; set; }
		public string Identifier => ContentIdentifiers.Slides;
		public RibbonTabItem TabPage => Controller.Instance.TabSlides;

		public MediaSlidesControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			simpleLabelItemSlideSize.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemSlideSize.MaxSize, scaleFactor);
			simpleLabelItemSlideSize.MinSize = RectangleHelper.ScaleSize(simpleLabelItemSlideSize.MinSize, scaleFactor);
		}

		private void LoadSlides()
		{
			if (_slideContainer != null)
			{
				pnMain.Controls.Remove(_slideContainer);
				_slideContainer.Dispose();
			}

			simpleLabelItemSlideSize.Text = string.Format("<size=+4>Slide Size: {0}</size>", SlideSettingsManager.Instance.SlideSettings.SizeFormatted);

			_slideContainer = new SlidesContainerControl();
			_slideContainer.BackColor = BackColor;
			_slideContainer.InitSlides(BusinessObjects.Instance.SlideManager);
			_slideContainer.SlideOutput += (o, e) => OutputPowerPoint(e.SlideMaster);
			_slideContainer.SlidePreview += (o, e) => Preview(e.SlideMaster);
			pnMain.Controls.Add(_slideContainer);
			_slideContainer.BringToFront();
		}

		public void InitMetaData()
		{
			TabPage.Tag = Identifier;
		}

		public void InitControl()
		{
			Controller.Instance.SlidesLogoLabel.Image = BusinessObjects.Instance.SlideManager.RibbonBarLogo ?? Controller.Instance.SlidesLogoLabel.Image;
			Controller.Instance.SlidesLogoBar.RecalcLayout();
			Controller.Instance.SlidesPanel.PerformLayout();

			LoadSlides();
			SlideSettingsManager.Instance.SettingsChanged += (o, e) => LoadSlides();
		}

		public void ShowControl(ContentOpenEventArgs args = null)
		{
			Controller.Instance.MenuOutputPdfButton.Enabled = Controller.Instance.MenuEmailButton.Enabled = true;
			IsActive = true;
			ContentStatusBarManager.Instance.FillStatusBarWithCommonInfo();
		}

		public void GetHelp()
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink("Slides");
		}

		public void OutputPowerPoint()
		{
			var selectedSlideMaster = _slideContainer.SelectedSlide;
			if (selectedSlideMaster == null) return;

			OutputPowerPoint(selectedSlideMaster);
		}

		private void OutputPowerPoint(SlideMaster slideMaster)
		{
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				BusinessObjects.Instance.PowerPointManager.Processor.AppendSlideMaster(slideMaster.GetMasterPath());
				FormProgress.CloseProgress();
			});
		}

		public void OutputPdf()
		{
			var selectedSlideMaster = _slideContainer.SelectedSlide;
			if (selectedSlideMaster == null) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				var tempFileName = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
				BusinessObjects.Instance.PowerPointManager.Processor.PreparePresentation(tempFileName, presentation => BusinessObjects.Instance.PowerPointManager.Processor.AppendSlideMaster(selectedSlideMaster.GetMasterPath(), presentation));
				var previewGroups = new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } };
				var pdfFileName = Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
					string.Format("{0}-{1}.pdf", selectedSlideMaster.Name, DateTime.Now.ToString("MM-dd-yy-hmmss")));
				BusinessObjects.Instance.PowerPointManager.Processor.BuildPdf(pdfFileName, previewGroups.Select(pg => pg.PresentationSourcePath));
				if (File.Exists(pdfFileName))
					try
					{
						Process.Start(pdfFileName);
					}
					catch { }
				FormProgress.CloseProgress();
			});
		}

		public void Preview()
		{
			var selectedSlideMaster = _slideContainer.SelectedSlide;
			if (selectedSlideMaster == null) return;

			Preview(selectedSlideMaster);
		}

		private void Preview(SlideMaster slideMaster)
		{
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			var tempFileName = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			BusinessObjects.Instance.PowerPointManager.Processor.PreparePresentation(tempFileName, presentation => BusinessObjects.Instance.PowerPointManager.Processor.AppendSlideMaster(slideMaster.GetMasterPath(), presentation));
			Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
			FormProgress.CloseProgress();

			using (var formPreview = new FormPreview(
				Controller.Instance.FormMain,
				BusinessObjects.Instance.PowerPointManager.Processor,
				BusinessObjects.Instance.HelpManager,
				Controller.Instance.ShowFloater,
				Controller.Instance.CheckPowerPointRunning))
			{
				formPreview.Text = "Preview Slide";
				formPreview.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
			}
		}

		public void Email()
		{
			var selectedSlideMaster = _slideContainer.SelectedSlide;
			if (selectedSlideMaster == null) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			var tempFileName = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			BusinessObjects.Instance.PowerPointManager.Processor.PreparePresentation(tempFileName, presentation => BusinessObjects.Instance.PowerPointManager.Processor.AppendSlideMaster(selectedSlideMaster.GetMasterPath(), presentation));
			Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
			FormProgress.CloseProgress();

			using (var formEmail = new FormEmail(BusinessObjects.Instance.PowerPointManager.Processor, BusinessObjects.Instance.HelpManager))
			{
				formEmail.Text = "Email this Slide";
				formEmail.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
				Utilities.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
				RegistryHelper.MainFormHandle = formEmail.Handle;
				RegistryHelper.MaximizeMainForm = false;
				formEmail.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
			}
		}

		public void EditSettings()
		{
			throw new NotImplementedException();
		}
	}
}