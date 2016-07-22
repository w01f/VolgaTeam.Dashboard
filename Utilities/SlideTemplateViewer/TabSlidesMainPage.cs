﻿using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.Slides;
using Asa.Common.GUI.ToolForms;

namespace Asa.SlideTemplateViewer
{
	[ToolboxItem(false)]
	public partial class TabSlidesMainPage : UserControl
	{
		private static TabSlidesMainPage _instance;
		private SlidesContainerControl _slideContainer;

		private TabSlidesMainPage()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			LoadSlides();

			PowerPointManager.Instance.SettingsChanged += (o, e) => LoadSlides();
		}

		public static TabSlidesMainPage Instance
		{
			get
			{
				if (_instance == null)
					_instance = new TabSlidesMainPage();
				return _instance;
			}
		}

		private void LoadSlides()
		{
			if (_slideContainer != null)
			{
				pnMain.Controls.Remove(_slideContainer);
				_slideContainer.Dispose();
			}

			laSlideSize.Text = String.Format("Slide Size: {0}", PowerPointManager.Instance.SlideSettings.SizeFormatted);

			_slideContainer = new SlidesContainerControl();
			_slideContainer.BackColor = BackColor;
			_slideContainer.InitSlides(AppManager.Instance.SlideManager);
			pnMain.Controls.Add(_slideContainer);
			_slideContainer.BringToFront();
		}

		public void buttonItemSlidesPowerPoint_Click(object sender, EventArgs e)
		{
			var selectedSlideMaster = _slideContainer.SelectedSlide;
			if (selectedSlideMaster == null) return;
			if (!AppManager.Instance.CheckPowerPointRunning()) return;
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			AppManager.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				SlideTemplateViewerPowerPointHelper.Instance.AppendSlideMaster(selectedSlideMaster.GetMasterPath());
				FormProgress.CloseProgress();
			});
		}

		public void buttonItemSlidesPreview_Click(object sender, EventArgs e)
		{
			var selectedSlideMaster = _slideContainer.SelectedSlide;
			if (selectedSlideMaster == null) return;
			if (!AppManager.Instance.CheckPowerPointRunning()) return;
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			var tempFileName = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			SlideTemplateViewerPowerPointHelper.Instance.PreparePresentation(tempFileName, presentation => SlideTemplateViewerPowerPointHelper.Instance.AppendSlideMaster(selectedSlideMaster.GetMasterPath(), presentation));
			Utilities.ActivateForm(FormMain.Instance.Handle, false, false);
			FormProgress.CloseProgress();
			if (!File.Exists(tempFileName)) return;
			using (var formPreview = new FormPreview(FormMain.Instance, SlideTemplateViewerPowerPointHelper.Instance, AppManager.Instance.HelpManager, AppManager.Instance.ShowFloater))
			{
				formPreview.Text = "Preview Slides";
				formPreview.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = false;
				RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
				if (previewResult != DialogResult.OK)
					AppManager.Instance.ActivateMainForm();
			}
		}
	}
}