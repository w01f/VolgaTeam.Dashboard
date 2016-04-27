using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Slides;

namespace Asa.Common.GUI.Slides
{
	[ToolboxItem(false)]
	public partial class SlidesContainerControl : UserControl
	{
		private SlideManager _slideManager;

		public event EventHandler<SlideMasterEventArgs> SlideChanged;
		public event EventHandler<SlideMasterEventArgs> SlideSelected;

		public SlideMaster SelectedSlide
		{
			get
			{
				var selectedGroup = xtraTabControlSlides.SelectedTabPage as SlideGroupControl;
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
			xtraTabControlSlides.TabPages.Clear();
			foreach (var group in _slideManager.Slides.Where(s => s.Format == PowerPointManager.Instance.SlideSettings.Format).Select(s => s.Group).Distinct())
			{
				var groupPage = new SlideGroupControl();
				groupPage.LoadSlides(group, _slideManager.Slides.Where(s => s.Group.Equals(group) && s.Format == PowerPointManager.Instance.SlideSettings.Format));
				groupPage.SlideChanged += (o, e) =>
				{
					var selectedGroup = o as SlideGroupControl;
					if (selectedGroup != xtraTabControlSlides.SelectedTabPage) return;
					SlideChanged?.Invoke(o, e);
				};
				groupPage.SlideSelected += (o, e) =>
				{
					var selectedGroup = o as SlideGroupControl;
					if (selectedGroup != xtraTabControlSlides.SelectedTabPage) return;
					SlideSelected?.Invoke(o, e);
				};
				xtraTabControlSlides.TabPages.Add(groupPage);
			}
			xtraTabControlSlides.SelectedPageChanged += (o, e) =>
			{
				SlideChanged?.Invoke(e.Page, new SlideMasterEventArgs { SelectedSlide = (e.Page as SlideGroupControl).SelectedSlide });
			};
		}

		public void SetSelectedSlide(string selectedGroup, string selectedSlide)
		{
			if (!String.IsNullOrEmpty(selectedGroup) && !String.IsNullOrEmpty(selectedSlide))
			{
				var selectedGroupPage = xtraTabControlSlides.TabPages.OfType<SlideGroupControl>().FirstOrDefault(g => g.GroupName.Equals(selectedGroup));
				if (selectedGroupPage == null) return;
				xtraTabControlSlides.SelectedTabPage = selectedGroupPage;
				selectedGroupPage.SelectSlide(selectedSlide);
			}
		}
	}
}
