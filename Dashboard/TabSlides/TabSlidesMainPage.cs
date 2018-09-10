using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Dashboard.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Slides;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.Slides;
using Asa.Common.GUI.ToolForms;

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

			FormMain.Instance.ribbonTabItemSlides.Enabled = SettingsManager.Instance.SlideManager.Slides.Any(s => s.Format == SlideSettingsManager.Instance.SlideSettings.Format);
			laSlideSize.Text = String.Format("Slide Size: {0}", SlideSettingsManager.Instance.SlideSettings.SizeFormatted);

			_slideContainer = new SlidesContainerControl();
			_slideContainer.BackColor = BackColor;
			_slideContainer.InitSlides(SettingsManager.Instance.SlideManager, new Size());
			_slideContainer.SlideOutput += (o, e) => GenerateOutput(e.SlideMaster);
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
				AppManager.Instance.PowerPointManager.Processor.AppendSlideMaster(slideMaster.GetMasterPath());
				FormProgress.CloseProgress();
			});
		}

		public void buttonItemSlidesPowerPoint_Click(object sender, EventArgs e)
		{
			var slideMaster = _slideContainer.SelectedSlide;
			if (slideMaster == null) return;
			GenerateOutput(slideMaster);
		}
	}
}