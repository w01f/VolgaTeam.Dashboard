using System;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Common.GUI.OutputSelector;

namespace Asa.Calendar.Controls.PresentationClasses.Output
{
	public class CaledarMonthOutputItem : ISlideItem
	{
		public CalendarMonth CalendarMonth { get; }
		public bool IsCurrent { get; set; }
		public string DisplayName => CalendarMonth.OutputData.MonthText;
		public int SlidesCount => 1;
		public bool SelectedForOutput { get; set; }

		public ISlideItem[] SlideItems
		{
			get => new ISlideItem[] { };
			set { }
		}

		public CaledarMonthOutputItem(CalendarMonth calendarMonth)
		{
			CalendarMonth = calendarMonth;
			SelectedForOutput = true;
		}
	}
}
