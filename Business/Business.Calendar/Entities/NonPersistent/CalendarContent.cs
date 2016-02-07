using System;
using System.Collections.Generic;
using System.Linq;
using Asa.Business.Calendar.Interfaces;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Common.Interfaces;
using Asa.Common.Core.Interfaces;
using Asa.Common.Core.Objects.Output;

namespace Asa.Business.Calendar.Entities.NonPersistent
{
	public abstract class CalendarContent<TSchedule, TScheduleSettings> : BaseSchedulePartitionContent<TSchedule, TScheduleSettings>, ICalendarContent
		where TSchedule : ISchedule<TScheduleSettings>
		where TScheduleSettings : IBaseScheduleSettings
	{
		public List<CalendarMonth> Months { get; private set; }
		public List<CalendarDay> Days { get; private set; }
		public List<CalendarNote> Notes { get; private set; }
		public IBaseScheduleSettings Settings
		{
			get { return ScheduleSettings; }
		}
		public abstract bool AllowCustomNotes { get; }

		protected CalendarContent()
		{
			Months = new List<CalendarMonth>();
			Days = new List<CalendarDay>();
			Notes = new List<CalendarNote>();
		}

		public override void Dispose()
		{
			Months.ForEach(m => m.Dispose());
			Months.Clear();

			Days.ForEach(m => m.Dispose());
			Days.Clear();

			Notes.ForEach(m => m.Dispose());
			Notes.Clear();

			base.Dispose();
		}

		public abstract void UpdateDaysCollection();

		public abstract void UpdateMonthCollection();

		public abstract IEnumerable<DateRange> CalculateDateRange(IEnumerable<DateTime> dates);

		public abstract DateTime[][] GetDaysByWeek(DateTime start, DateTime end);

		public virtual void Reset() { }

		public virtual void UpdateNotesCollection()
		{
			if (ScheduleSettings.FlightDateStart.HasValue && ScheduleSettings.FlightDateEnd.HasValue)
			{
				var _notesToDelete = new List<CalendarNote>();
				foreach (var note in Notes)
				{
					if (note.FinishDay < ScheduleSettings.FlightDateStart.Value || note.StartDay > ScheduleSettings.FlightDateEnd.Value)
						_notesToDelete.Add(note);
					else if (note.StartDay < ScheduleSettings.FlightDateStart.Value)
						note.StartDay = ScheduleSettings.FlightDateStart.Value;
					else if (note.FinishDay > ScheduleSettings.FlightDateEnd.Value)
						note.FinishDay = ScheduleSettings.FlightDateEnd.Value;
					if (note.Length < 1)
						_notesToDelete.Add(note);
				}
				foreach (var note in _notesToDelete)
					Notes.Remove(note);
			}
			else
				Notes.Clear();
			UpdateDayAndNoteLinks();
		}

		protected override void AfterConstruction()
		{
			base.AfterConstruction();
			UpdateDaysCollection();
			UpdateMonthCollection();
			UpdateNotesCollection();
		}

		protected void UpdateDayAndNoteLinks()
		{
			foreach (var day in Days)
				day.HasNotes = Notes.Any(x => day.Date >= x.StartDay && day.Date <= x.FinishDay);
		}

		public void AddNote(DateRange range, string noteText = "")
		{
			var note = new TextItem(noteText, false);
			AddNote(range, note, true);
		}

		public void AddNote(DateRange range, ITextItem noteText, bool userAdded = false)
		{
			var newNote = new CommonCalendarNote(this) { UserAdded = userAdded };
			newNote.StartDay = range.StartDate.Value;
			newNote.FinishDay = range.FinishDate.Value;
			newNote.Note = noteText;
			var _notesToDelete = new List<CalendarNote>();
			foreach (var note in Notes)
			{
				if (note.StartDay >= newNote.StartDay && note.FinishDay <= newNote.FinishDay)
					_notesToDelete.Add(note);
				else if (note.StartDay <= newNote.FinishDay && note.FinishDay > newNote.FinishDay)
					note.StartDay = newNote.FinishDay.AddDays(1);
				else if (note.FinishDay >= newNote.StartDay && note.StartDay < newNote.StartDay)
					note.FinishDay = newNote.StartDay.AddDays(-1);
				if (note.Length < 1)
					_notesToDelete.Add(note);
			}
			foreach (var note in _notesToDelete)
				Notes.Remove(note);
			Notes.Add(newNote);
			UpdateDayAndNoteLinks();
		}

		public void DeleteNote(CalendarNote note)
		{
			Notes.Remove(note);
			UpdateDayAndNoteLinks();
		}

		protected void UpdateDaysCollection<T>(DayOfWeek startDay, DayOfWeek endDay) where T : CalendarDay
		{
			if (ScheduleSettings.FlightDateStart.HasValue && ScheduleSettings.FlightDateEnd.HasValue)
			{
				var days = new List<CalendarDay>();

				var startDate = new DateTime(ScheduleSettings.FlightDateStart.Value.Year, ScheduleSettings.FlightDateStart.Value.Month, 1);
				while (startDate.DayOfWeek != startDay)
					startDate = startDate.AddDays(-1);

				var endDate = new DateTime(
						ScheduleSettings.FlightDateEnd.Value.Month < 12 ? ScheduleSettings.FlightDateEnd.Value.Year : (ScheduleSettings.FlightDateEnd.Value.Year + 1),
						(ScheduleSettings.FlightDateEnd.Value.Month < 12 ? ScheduleSettings.FlightDateEnd.Value.Month + 1 : 1),
						1)
					.AddDays(-1);
				while (endDate.DayOfWeek != endDay)
					endDate = endDate.AddDays(1);

				while (startDate <= endDate)
				{
					var day = Days.FirstOrDefault(x => x.Date.Equals(startDate));
					if (day == null)
					{
						day = (T)Activator.CreateInstance(typeof(T), this);
						day.Date = startDate;
					}
					day.BelongsToSchedules = day.Date >= ScheduleSettings.FlightDateStart & day.Date <= ScheduleSettings.FlightDateEnd;
					days.Add(day);
					startDate = startDate.AddDays(1);
				}
				Days.Clear();
				Days.AddRange(days);
			}
			else
				Days.Clear();
		}

		protected void UpdateMonthCollection<T>() where T : CalendarMonth
		{
			if (ScheduleSettings.FlightDateStart.HasValue && ScheduleSettings.FlightDateEnd.HasValue)
			{
				var months = new List<CalendarMonth>();
				var startDate = new DateTime(ScheduleSettings.FlightDateStart.Value.Year, ScheduleSettings.FlightDateStart.Value.Month, 1);
				while (startDate <= ScheduleSettings.FlightDateEnd.Value)
				{
					var month = Months.FirstOrDefault(x => x.Date.Equals(startDate));
					if (month == null)
					{
						month = (T)Activator.CreateInstance(typeof(T), this);
						month.Date = startDate;
					}
					month.Days.Clear();
					month.Days.AddRange(Days.Where(x => x.Date >= month.DaysRangeBegin && x.Date <= month.DaysRangeEnd));
					months.Add(month);
					startDate = startDate.AddMonths(1);
				}
				Months.Clear();
				Months.AddRange(months);
			}
			else
				Months.Clear();
		}

		protected IEnumerable<DateRange> CalculateDateRange(IEnumerable<DateTime> dates, DayOfWeek startDay, DayOfWeek endDay)
		{
			var result = new List<DateRange>();
			var selectedDates = new List<DateTime>();
			selectedDates.AddRange(dates);
			selectedDates.Sort();

			var firstSelectedDate = selectedDates.FirstOrDefault();
			var lastSelectedDate = selectedDates.LastOrDefault();
			var firstWeekday = firstSelectedDate;
			while (firstWeekday.DayOfWeek != startDay)
				firstWeekday = firstWeekday.AddDays(-1);
			var lastWeekday = firstSelectedDate;
			while (lastWeekday.DayOfWeek != endDay)
				lastWeekday = lastWeekday.AddDays(1);

			while (firstWeekday < lastSelectedDate)
			{
				var range = new DateRange();
				if (firstWeekday >= firstSelectedDate && lastWeekday <= lastSelectedDate)
				{
					range.StartDate = firstWeekday;
					range.FinishDate = lastWeekday;
				}
				else if (firstWeekday <= firstSelectedDate && lastWeekday >= lastSelectedDate)
				{
					range.StartDate = firstSelectedDate;
					range.FinishDate = lastSelectedDate;
				}
				else if (firstWeekday <= firstSelectedDate && lastWeekday >= firstSelectedDate)
				{
					range.StartDate = firstSelectedDate;
					range.FinishDate = lastWeekday;
				}
				else if (firstWeekday <= lastSelectedDate && lastWeekday >= lastSelectedDate)
				{
					range.StartDate = firstWeekday;
					range.FinishDate = lastSelectedDate;
				}
				result.Add(range);
				firstWeekday = firstWeekday.AddDays(7);
				lastWeekday = lastWeekday.AddDays(7);
			}
			return result;
		}

		protected DateTime[][] GetDaysByWeek(DateTime start, DateTime end, DayOfWeek borderDay)
		{
			var weeks = new List<DateTime[]>();
			var week = new List<DateTime>();
			while (!(start > end))
			{
				week.Add(start);
				if (start.DayOfWeek == borderDay)
				{
					weeks.Add(week.ToArray());
					week.Clear();
				}
				start = start.AddDays(1);
			}
			if (week.Count > 0)
				weeks.Add(week.ToArray());
			return weeks.ToArray();
		}
	}
}
