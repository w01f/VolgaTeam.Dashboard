using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
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
			_slideContainer.InitSlides(AppManager.Instance.SlideManager, new Size());
			_slideContainer.SlideOutput += (o, e) => GenerateOutput(e.SlideMaster);
			pnMain.Controls.Add(_slideContainer);
			_slideContainer.BringToFront();
		}

		private IList<OutputItem> GetOutputItems(SlideMaster slideMaster = null)
		{
			var selectedSlideMaster = slideMaster ?? _slideContainer.SelectedSlide;
			var defaultOutputGroup = new OutputGroup
			{
				Name = "Preview",
				IsCurrent = true,
				Items = new List<OutputItem>(new[]
				{
					new OutputItem
					{
						Name = "Preview",
						IsCurrent = true,
						SlidesCount = 1,
						PresentationSourcePath = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath,
							Path.GetFileName(Path.GetTempFileName())),
						SlideGeneratingAction = (processor, destinationPresentation) =>
						{
							var templatePath = selectedSlideMaster.GetMasterPath();
							processor.AppendSlideMaster(templatePath, destinationPresentation);
						},
						PreviewGeneratingAction = (processor, presentationSourcePath) =>
						{
							var templatePath = selectedSlideMaster.GetMasterPath();
							processor.PreparePresentation(presentationSourcePath,
								presentation => processor.AppendSlideMaster(templatePath, presentation));
						}
					}
				})
			};

			var selectedOutputItems = new List<OutputItem>();
			using (var form = new FormPreview(
				FormMain.Instance,
				AppManager.Instance.PowerPointManager.Processor))
			{
				form.LoadGroups(new[] { defaultOutputGroup });
				if (form.ShowDialog() == DialogResult.OK)
					selectedOutputItems.AddRange(form.GetSelectedItems());
			}

			return selectedOutputItems;
		}

		private void GenerateOutput(SlideMaster slideMaster)
		{
			if (!AppManager.Instance.CheckPowerPointRunning()) return;

			var outputItems = GetOutputItems(slideMaster);
			if (!outputItems.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			FormProgress.ShowOutputProgress();
			AppManager.Instance.ShowFloater(() =>
			{
				foreach (var outputItem in outputItems)
					outputItem.SlideGeneratingAction?.Invoke(AppManager.Instance.PowerPointManager.Processor, null);
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