using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Slides;
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

			SlideSettingsManager.Instance.SettingsChanged += (o, e) => LoadSlides();
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

			_slideContainer = new SlidesContainerControl();
			_slideContainer.BackColor = BackColor;
			_slideContainer.InitSlides(AppManager.Instance.SlideManager);
			_slideContainer.SlideOutput += (o, e) => GenerateOutput(e.SlideMaster);
			_slideContainer.SlidePreview += (o, e) => GeneratePreview(e.SlideMaster);
			pnMain.Controls.Add(_slideContainer);
			_slideContainer.BringToFront();
		}

		private void GenerateOutput(SlideMaster slideMaster)
		{
			if (!AppManager.Instance.CheckPowerPointRunning()) return;
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			FormProgress.ShowOutputProgress();
			AppManager.Instance.ShowFloater(() =>
			{
				AppManager.Instance.PowerPointManager.Processor.AppendSlideMaster(slideMaster.GetMasterPath());
				FormProgress.CloseProgress();
			});
		}

		private void GeneratePreview(SlideMaster slideMaster)
		{
			if (!AppManager.Instance.CheckPowerPointRunning()) return;
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Preview...");
			FormProgress.ShowProgress(FormMain.Instance);
			var tempFileName = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			AppManager.Instance.PowerPointManager.Processor.PreparePresentation(tempFileName, presentation => AppManager.Instance.PowerPointManager.Processor.AppendSlideMaster(slideMaster.GetMasterPath(), presentation));
			Utilities.ActivateForm(FormMain.Instance.Handle, false, false);
			FormProgress.CloseProgress();
			if (!File.Exists(tempFileName)) return;
			using (var formPreview = new FormPreview(FormMain.Instance, AppManager.Instance.PowerPointManager.Processor, AppManager.Instance.ShowFloater,AppManager.Instance.CheckPowerPointRunning))
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