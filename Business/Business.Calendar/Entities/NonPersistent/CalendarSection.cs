using System;
using System.Collections.Generic;
using System.Linq;
using Asa.Business.Calendar.Interfaces;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Common.Interfaces;
using Asa.Common.Core.Interfaces;
using Asa.Common.Core.Objects.Output;
using Newtonsoft.Json;

namespace Asa.Business.Calendar.Entities.NonPersistent
{
	public abstract class CalendarSection
	{
		public List<CalendarMonth> Months { get; private set; }
		public List<CalendarDay> Days { get; private set; }
		public List<CalendarNote> Notes { get; private set; }

		public ICalendarContent Parent { get; set; }

		public abstract bool AllowCustomNotes { get; }

		public IBaseScheduleSettings ParentScheduleSettings => Parent.Settings;

		[JsonConstructor]
		protected CalendarSection() { }

		protected CalendarSection(ICalendarContent parent)
		{
			Parent = parent;

			Months = new List<CalendarMonth>();
			Days = new List<CalendarDay>();
			Notes = new List<CalendarNote>();
		}

		public virtual void AfterConstraction()
		{
			UpdateDaysCollection();
			UpdateMonthCollection();
			UpdateNotesCollection();
		}

		public void Dispose()
		{
			Months.ForEach(m => m.Dispose());
			Months.Clear();

			Days.ForEach(m => m.Dispose());
			Days.Clear();

			Notes.ForEach(m => m.Dispose());
			Notes.Clear();

			Parent = null;
		}

		public void Reset()
		{
			Days.Clear();
			Months.Clear();
			Notes.Clear();
			AfterConstraction();
		}

		public abstract void UpdateDaysCollection();

		public abstract void UpdateMonthCollection();

		public virtual void UpdateNotesCollection()
		{
			if (ParentScheduleSettings.FlightDateStart.HasValue && ParentScheduleSettings.FlightDateEnd.HasValue)
			{
				var notesToDelete = new List<CalendarNote>();
				foreach (var note in Notes)
				{
					if (note.FinishDay < ParentScheduleSettings.FlightDateStart.Value || note.StartDay > ParentScheduleSettings.FlightDateEnd.Value)
						notesToDelete.Add(note);
					else if (note.StartDay < ParentScheduleSettings.FlightDateStart.Value)
						note.StartDay = ParentScheduleSettings.FlightDateStart.Value;
					else if (note.FinishDay > ParentScheduleSettings.FlightDateEnd.Value)
						note.FinishDay = ParentScheduleSettings.FlightDateEnd.Value;
					if (note.Length < 1)
						notesToDelete.Add(note);
				}
				foreach (var note in notesToDelete)
					Notes.Remove(note);
			}
			else
				Notes.Clear();

			UpdateDayAndNoteLinks();
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
			var notesToDelete = new List<CalendarNote>();
			foreach (var note in Notes)
			{
				if (note.StartDay >= newNote.StartDay && note.FinishDay <= newNote.FinishDay)
					notesToDelete.Add(note);
				else if (note.StartDay <= newNote.FinishDay && note.FinishDay > newNote.FinishDay)
					note.StartDay = newNote.FinishDay.AddDays(1);
				else if (note.FinishDay >= newNote.StartDay && note.StartDay < newNote.StartDay)
					note.FinishDay = newNote.StartDay.AddDays(-1);
				if (note.Length < 1)
					notesToDelete.Add(note);
			}
			foreach (var note in notesToDelete)
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
			if (ParentScheduleSettings.FlightDateStart.HasValue && ParentScheduleSettings.FlightDateEnd.HasValue)
			{
				var days = new List<CalendarDay>();

				var startDate = new DateTime(ParentScheduleSettings.FlightDateStart.Value.Year, ParentScheduleSettings.FlightDateStart.Value.Month, 1);
				while (startDate.DayOfWeek != startDay)
					startDate = startDate.AddDays(-1);

				var endDate = new DateTime(
						ParentScheduleSettings.FlightDateEnd.Value.Month < 12 ? ParentScheduleSettings.FlightDateEnd.Value.Year : (ParentScheduleSettings.FlightDateEnd.Value.Year + 1),
						(ParentScheduleSettings.FlightDateEnd.Value.Month < 12 ? ParentScheduleSettings.FlightDateEnd.Value.Month + 1 : 1),
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
					day.BelongsToSchedules = day.Date >= ParentScheduleSettings.FlightDateStart & day.Date <= ParentScheduleSettings.FlightDateEnd;
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
			if (ParentScheduleSettings.FlightDateStart.HasValue && ParentScheduleSettings.FlightDateEnd.HasValue)
			{
				var months = new List<CalendarMonth>();
				var startDate = new DateTime(ParentScheduleSettings.FlightDateStart.Value.Year, ParentScheduleSettings.FlightDateStart.Value.Month, 1);
				while (startDate <= ParentScheduleSettings.FlightDateEnd.Value)
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
	}
}
