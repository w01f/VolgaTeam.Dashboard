using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Slides;
using DevExpress.Utils;

namespace Asa.Common.GUI.Slides
{
	public partial class SlidesContainerControl : UserControl
	{
		private SlideManager _slideManager;

		public event EventHandler<SlideMasterEventArgs> SlideOutput;
		public event EventHandler<SlideMasterEventArgs> SlidePreview;
		public event EventHandler<EventArgs> SelectionChanged;

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

		public void InitSlides(SlideManager slideManager, Size thumbnailSize)
		{
			_slideManager = slideManager;
			xtraTabControlSlides.TabPages.OfType<SlideGroupPage>().ToList().ForEach(g => g.Release());
			xtraTabControlSlides.TabPages.Clear();

			var groups = _slideManager.Slides
				.Where(s => s.Format == SlideSettingsManager.Instance.SlideSettings.Format)
				.Select(s => s.Group).Distinct()
				.ToList();
			foreach (var group in groups)
			{
				var groupPage = new SlideGroupPage(
					group,
					_slideManager.Slides.Where(s => s.Group.Equals(group) && s.Format == SlideSettingsManager.Instance.SlideSettings.Format).ToList(),
					thumbnailSize);
				if (SlideOutput != null)
					groupPage.SlideOutput += OnSlideOutput;
				if (SlidePreview != null)
					groupPage.SlidePreview += OnSlidePreview;
				groupPage.SelectionChanged += OnSelectionChanged;
				xtraTabControlSlides.TabPages.Add(groupPage);
			}
			xtraTabControlSlides.ShowTabHeader = groups.Count > 1 ? DefaultBoolean.True : DefaultBoolean.False;
		}

		private void OnSelectionChanged(Object sender, EventArgs e)
		{
			SelectionChanged?.Invoke(this, EventArgs.Empty);
		}

		public void SelectSlide(SlideMaster slideMaster)
		{
			foreach (var slideGroupPage in xtraTabControlSlides.TabPages.OfType<SlideGroupPage>().ToList())
			{
				if (String.Equals(slideGroupPage.SlideGroupName, slideMaster.Group, StringComparison.OrdinalIgnoreCase))
				{
					slideGroupPage.SelectSlide(slideMaster);
					xtraTabControlSlides.SelectedTabPage = slideGroupPage;
				}
				else
					slideGroupPage.ResetSelection();
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
