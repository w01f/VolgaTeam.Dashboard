using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using NewBizWiz.Core.Common;

namespace NewBizWiz.CommonGUI.Slides
{
	[ToolboxItem(false)]
	public partial class SlidesContainerControl : UserControl
	{
		private SlideManager _slideManager;

		public SlideMaster SelectedSlide
		{
			get
			{
				var selectedGroup = xtraTabControlSlides.SelectedTabPage as SlideGroupControl;
				if (selectedGroup == null) return null;
				return selectedGroup.SelectedSlide;
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
			foreach (var group in _slideManager.Slides.Where(s => s.SizeWidth == SettingsManager.Instance.SizeWidth && s.SizeHeght == SettingsManager.Instance.SizeHeght).Select(s => s.Group).Distinct())
			{
				var groupPage = new SlideGroupControl();
				groupPage.LoadSlides(group, _slideManager.Slides.Where(s => s.Group.Equals(group) && s.SizeWidth == SettingsManager.Instance.SizeWidth && s.SizeHeght == SettingsManager.Instance.SizeHeght));
				xtraTabControlSlides.TabPages.Add(groupPage);
			}
		}

		public void SetSelectedSlide(string selectedGroup, string selectedSlide)
		{
			if (!String.IsNullOrEmpty(selectedGroup) && !String.IsNullOrEmpty(selectedSlide))
			{
				var selectedGroupPage = xtraTabControlSlides.TabPages.OfType<SlideGroupControl>().FirstOrDefault(g => g.GroupName.Equals(selectedGroup));
				if (selectedGroupPage != null)
				{
					xtraTabControlSlides.SelectedTabPage = selectedGroupPage;
					selectedGroupPage.SelectSlide(selectedSlide);
				}
			}
		}
	}
}
