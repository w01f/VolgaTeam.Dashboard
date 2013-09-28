using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.CommonGUI.Slides;
using NewBizWiz.Core.Common;
using NewBizWiz.Dashboard.InteropClasses;
using NewBizWiz.Dashboard.ToolForms;

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
			laUserName.Text = Environment.UserName;
			pbVersion.Image = MasterWizardManager.Instance.Version;

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
				AppManager.Instance.ShowFloater(null, (() =>
				{
					form.Show();
					DashboardPowerPointHelper.Instance.AppendSlideMaster(selectedSlideMaster.MasterPath);
					form.Close();
				}));
			}
		}
	}
}