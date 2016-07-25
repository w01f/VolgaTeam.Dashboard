using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Slides;
using DevExpress.XtraPrinting.Native;

namespace Asa.Common.GUI.Slides
{
	[ToolboxItem(false)]
	public partial class SlidesContainerControl : UserControl
	{
		private SlideManager _slideManager;

		public event EventHandler<SlideMasterEventArgs> SlideOutput;
		public event EventHandler<SlideMasterEventArgs> SlidePreview;

		public SlideMaster SelectedSlide
		{
			get
			{
				var selectedGroup = xtraTabControlSlides.SelectedTabPage as SlideGroupPage;
				return selectedGroup?.SelectedSlide;
			}
		}

		public SlidesContainerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		public void InitSlides(SlideManager slideManager)
		{
			_slideManager = slideManager;
			xtraTabControlSlides.TabPages.OfType<SlideGroupPage>().ForEach(g => g.Release());
			xtraTabControlSlides.TabPages.Clear();
			foreach (var group in _slideManager.Slides.Where(s => s.Format == PowerPointManager.Instance.SlideSettings.Format).Select(s => s.Group).Distinct())
			{
				var groupPage = new SlideGroupPage(
					group,
					_slideManager.Slides.Where(s => s.Group.Equals(group) && s.Format == PowerPointManager.Instance.SlideSettings.Format));
				groupPage.SlideOutput += OnSlideOutput;
				groupPage.SlidePreview += OnSlidePreview;
				xtraTabControlSlides.TabPages.Add(groupPage);
			}
		}

		private void OnSlideOutput(object sender, SlideMasterEventArgs e)
		{
			SlideOutput?.Invoke(sender, e);
		}

		private void OnSlidePreview(object sender, SlideMasterEventArgs e)
		{
			SlidePreview?.Invoke(sender, e);
		}
	}
}
