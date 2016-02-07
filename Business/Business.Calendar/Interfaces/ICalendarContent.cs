using System;
using System.Collections.Generic;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Common.Interfaces;
using Asa.Common.Core.Interfaces;

namespace Asa.Business.Calendar.Interfaces
{
	public interface ICalendarContent
	{
		bool AllowCustomNotes { get; }
		List<CalendarMonth> Months { get; }
		List<CalendarDay> Days { get; }
		List<CalendarNote> Notes { get; }
		IBaseScheduleSettings Settings { get; }
		IEnumerable<DateRange> CalculateDateRange(IEnumerable<DateTime> dates);
		DateTime[][] GetDaysByWeek(DateTime start, DateTime end);
		void AddNote(DateRange range, ITextItem noteText, bool userAdded = false);
		void AddNote(DateRange range, string noteText = "");
		void DeleteNote(CalendarNote note);

	}
}
