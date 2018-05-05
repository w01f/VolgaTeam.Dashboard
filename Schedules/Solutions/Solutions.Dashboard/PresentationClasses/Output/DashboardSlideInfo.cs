using Asa.Common.GUI.OutputSelector;

namespace Asa.Solutions.Dashboard.PresentationClasses.Output
{
	public class DashboardSlideInfo : ISlideItem
	{
		public IDashboardSlide SlideContainer { get; set; }
		public bool IsCurrent { get; set; }
		public string SlideName { get; set; }


		public string DisplayName => SlideName;
		public int SlidesCount => 1;
		public bool SelectedForOutput { get; set; } = true;
		public ISlideItem[] SlideItems
		{
			get => new ISlideItem[] { };
			set { }
		}
	}
}
