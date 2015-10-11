using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.CommonGUI.Slides;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Dashboard.InteropClasses;

namespace NewBizWiz.Dashboard.TabSlides
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

			laSlideSize.Text = String.Format(laSlideSize.Text, SettingsManager.Instance.Size);

			LoadSlides();

			AppManager.Instance.SetClickEventHandler(this);
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
			FormMain.Instance.ribbonTabItemSlides.Enabled = Core.Dashboard.SettingsManager.Instance.SlideManager.Slides.Any(s => s.SizeWidth == SettingsManager.Instance.SizeWidth && s.SizeHeght == SettingsManager.Instance.SizeHeght);

			_slideContainer = new SlidesContainerControl();
			_slideContainer.BackColor = BackColor;
			_slideContainer.InitSlides(Core.Dashboard.SettingsManager.Instance.SlideManager);
			pnMain.Controls.Add(_slideContainer);
			_slideContainer.BringToFront();
		}

		public void buttonItemSlidesPowerPoint_Click(object sender, EventArgs e)
		{
			var selectedSlideMaster = _slideContainer.SelectedSlide;
			if (selectedSlideMaster == null) return;
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				form.TopMost = true;
				AppManager.Instance.ShowFloater(() =>
				{
					form.Show();
					DashboardPowerPointHelper.Instance.AppendSlideMaster(selectedSlideMaster.GetMasterPath());
					form.Close();
				});
			}
		}

		public void buttonItemSlidesPreview_Click(object sender, EventArgs e)
		{
			var selectedSlideMaster = _slideContainer.SelectedSlide;
			if (selectedSlideMaster == null) return;
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Preview...";
				formProgress.TopMost = true;
				formProgress.Show();
				var tempFileName = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
				DashboardPowerPointHelper.Instance.PreparePresentation(tempFileName, presentation => DashboardPowerPointHelper.Instance.AppendSlideMaster(selectedSlideMaster.GetMasterPath(), presentation));
				Utilities.Instance.ActivateForm(FormMain.Instance.Handle, false, false);
				formProgress.Close();
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
		}
	}
}