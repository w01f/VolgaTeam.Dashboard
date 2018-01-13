using System;
using System.Collections.Generic;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Common.Interfaces;

namespace Asa.Business.Calendar.Interfaces
{
	public interface ICalendarContent
	{
		List<CalendarSection> Sections { get; }
		IBaseScheduleSettings Settings { get; }
		IEnumerable<DateRange> CalculateDateRange(IEnumerable<DateTime> dates);
		DateTime[][] GetDaysByWeek(DateTime start, DateTime end);
	}
}
