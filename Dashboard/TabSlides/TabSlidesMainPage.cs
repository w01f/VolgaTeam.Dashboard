using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Dashboard.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Slides;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.Slides;
using Asa.Common.GUI.ToolForms;
using Asa.Dashboard.InteropClasses;

namespace Asa.Dashboard.TabSlides
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

			AppManager.Instance.SetClickEventHandler(this);

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

			FormMain.Instance.ribbonTabItemSlides.Enabled = SettingsManager.Instance.SlideManager.Slides.Any(s => s.Format == PowerPointManager.Instance.SlideSettings.Format);
			laSlideSize.Text = String.Format("Slide Size: {0}", PowerPointManager.Instance.SlideSettings.SizeFormatted);

			_slideContainer = new SlidesContainerControl();
			_slideContainer.BackColor = BackColor;
			_slideContainer.InitSlides(SettingsManager.Instance.SlideManager);
			_slideContainer.SlideOutput += (o, e) => GenerateOutput(e.SlideMaster);
			_slideContainer.SlidePreview += (o, e) => GeneratePreview(e.SlideMaster);
			pnMain.Controls.Add(_slideContainer);
			_slideContainer.BringToFront();
		}

		private void GenerateOutput(SlideMaster slideMaster)
		{
			if (!AppManager.Instance.CheckPowerPointRunning()) return;
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			AppManager.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				DashboardPowerPointHelper.Instance.AppendSlideMaster(slideMaster.GetMasterPath());
				FormProgress.CloseProgress();
			});
		}

		private void GeneratePreview(SlideMaster slideMaster)
		{
			if (!AppManager.Instance.CheckPowerPointRunning()) return;
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress();
			var tempFileName = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			DashboardPowerPointHelper.Instance.PreparePresentation(tempFileName, presentation => DashboardPowerPointHelper.Instance.AppendSlideMaster(slideMaster.GetMasterPath(), presentation));
			Utilities.ActivateForm(FormMain.Instance.Handle, false, false);
			FormProgress.CloseProgress();
			if (!File.Exists(tempFileName)) return;
			using (var formPreview = new FormPreview(FormMain.Instance, DashboardPowerPointHelper.Instance, AppManager.Instance.HelpManager, AppManager.Instance.ShowFloater))
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

		public void buttonItemSlidesPowerPoint_Click(object sender, EventArgs e)
		{
			var slideMaster = _slideContainer.SelectedSlide;
			if (slideMaster == null) return;
			GenerateOutput(slideMaster);
		}

		public void buttonItemSlidesPreview_Click(object sender, EventArgs e)
		{
			var slideMaster = _slideContainer.SelectedSlide;
			if (slideMaster == null) return;
			GeneratePreview(slideMaster);
		}
	}
}