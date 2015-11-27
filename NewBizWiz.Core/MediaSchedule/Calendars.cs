using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using Asa.Core.Calendar;
using Asa.Core.Common;

namespace Asa.Core.MediaSchedule
{
	public abstract class MediaCalendar : Calendar.Calendar
	{
		protected readonly Schedule _parentSchedule;

		protected MediaCalendar(ISchedule parent)
			: base(parent)
		{
			_parentSchedule = parent as Schedule;
		}

		public override void UpdateDaysCollection()
		{
			if (_parentSchedule.MondayBased)
				UpdateDaysCollection<CalendarDayMondayBased>(_parentSchedule.StartDayOfWeek, _parentSchedule.EndDayOfWeek);
			else
				UpdateDaysCollection<CalendarDaySundayBased>(_parentSchedule.StartDayOfWeek, _parentSchedule.EndDayOfWeek);
		}

		public override IEnumerable<DateRange> CalculateDateRange(IEnumerable<DateTime> dates)
		{
			return CalculateDateRange(dates, _parentSchedule.StartDayOfWeek, _parentSchedule.EndDayOfWeek);
		}

		public override DateTime[][] GetDaysByWeek(DateTime start, DateTime end)
		{
			return GetDaysByWeek(start, end, _parentSchedule.EndDayOfWeek);
		}

		protected abstract void ApplyDefaultMonthSettings(CalendarMonth targetMonth);

		public override void UpdateMonthCollection()
		{
			if (!Schedule.FlightDateStart.HasValue || !Schedule.FlightDateEnd.HasValue)
			{
				Months.Clear();
				return;
			}
			var months = new List<CalendarMonth>();
			var startDate = Schedule.FlightDateStart.Value;
			var monthTemplates = _parentSchedule.MondayBased ? MediaMetaData.Instance.ListManager.MonthTemplatesMondayBased : MediaMetaData.Instance.ListManager.MonthTemplatesSundayBased;
			while (startDate <= Schedule.FlightDateEnd.Value)
			{
				var monthTemplate = monthTemplates.FirstOrDefault(mt => startDate >= mt.StartDate && startDate <= mt.EndDate);
				if (monthTemplate == null) continue;

				startDate = monthTemplate.Month.Value;
				var month = Months.FirstOrDefault(x => x.Date == startDate);
				if (month == null)
				{
					if (_parentSchedule.MondayBased)
						month = new CalendarMonthMediaMondayBased(this);
					else
						month = new CalendarMonthMediaSundayBased(this);
					ApplyDefaultMonthSettings(month);
				}
				month.Date = monthTemplate.Month.Value;
				month.DaysRangeBegin = monthTemplate.StartDate.Value;
				month.DaysRangeEnd = monthTemplate.EndDate.Value;
				month.Days.Clear();
				month.Days.AddRange(Days.Where(x => x.Date >= month.DaysRangeBegin && x.Date <= month.DaysRangeEnd));
				months.Add(month);
				startDate = startDate.AddMonths(1);
			}
			Months.Clear();
			Months.AddRange(months);
		}

		public override void Reset()
		{
			Days.Clear();
			Months.Clear();
			Notes.Clear();
			UpdateDaysCollection();
			UpdateMonthCollection();
			UpdateNotesCollection();
		}
	}

	public class BroadcastCalendar : MediaCalendar
	{
		public BroadcastCalendar(ISchedule parent) : base(parent) { }

		public BroadcastDataTypeEnum DataSourceType { get; set; }

		public override bool AllowCustomNotes
		{
			get { return false; }
		}

		public bool AllowSelectDataSource
		{
			get
			{
				var mediaSchedule = (RegularSchedule)_parentSchedule;
				var hasSchedule = mediaSchedule.SelectedSpotType == SpotType.Week && mediaSchedule.ProgramSchedule.TotalSpots > 0;
				var hasSnapshots = mediaSchedule.Snapshots.Any(s => s.Programs.Count > 0);
				return hasSchedule && hasSnapshots;
			}
		}

		public override string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(base.Serialize());
			if (DataSourceType != BroadcastDataTypeEnum.None)
				result.AppendLine(@"<DataSourceType>" + DataSourceType + @"</DataSourceType>");
			return result.ToString();
		}

		public override void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "DataSourceType":
						BroadcastDataTypeEnum temp;
						if (Enum.TryParse(childNode.InnerText, true, out temp))
							DataSourceType = temp;
						break;
				}
			}
			if (_parentSchedule.MondayBased)
				DeserializeInternal<CalendarMonthMediaMondayBased, CalendarDayMondayBased, MediaDataNote>(node, _parentSchedule.StartDayOfWeek, _parentSchedule.EndDayOfWeek);
			else
				DeserializeInternal<CalendarMonthMediaSundayBased, CalendarDaySundayBased, MediaDataNote>(node, _parentSchedule.StartDayOfWeek, _parentSchedule.EndDayOfWeek);
		}

		public void UpdateDataSource()
		{
			var mediaSchedule = (RegularSchedule)_parentSchedule;
			var hasSchedule = mediaSchedule.SelectedSpotType == SpotType.Week && mediaSchedule.ProgramSchedule.TotalSpots > 0;
			var hasSnapshots = mediaSchedule.Snapshots.Any(s => s.Programs.Count > 0);
			if (AllowSelectDataSource) return;
			if (hasSchedule) DataSourceType = BroadcastDataTypeEnum.Schedule;
			if (hasSnapshots) DataSourceType = BroadcastDataTypeEnum.Snapshots;
		}

		public override void UpdateNotesCollection()
		{
			var noteSeparator = String.Empty;

			var notes = new List<MediaDataNote>();
			switch (DataSourceType)
			{
				case BroadcastDataTypeEnum.Schedule:
					noteSeparator = "   ";
					notes.AddRange(GetNotesFromSchedule());
					break;
				case BroadcastDataTypeEnum.Snapshots:
					noteSeparator = "  |  ";
					notes.AddRange(GetNotesFromSnapshots());
					break;
			}

			bool needToSplit;
			notes.ForEach(n => n.Splitted = false);
			var splittedNotes = new List<MediaDataNote>(notes);
			do
			{
				needToSplit = false;
				foreach (var calendarNote in notes.OrderByDescending(n => n.Length))
				{
					if (calendarNote.Splitted) continue;
					var intersectedNote = splittedNotes.Where(sn => sn != calendarNote).OrderBy(n => n.Length).FirstOrDefault(mn =>
						(mn.StartDay >= calendarNote.StartDay && mn.StartDay <= calendarNote.FinishDay) ||
						(mn.FinishDay >= calendarNote.StartDay && mn.FinishDay <= calendarNote.FinishDay));
					if (intersectedNote == null) continue;
					needToSplit = true;
					if ((intersectedNote.StartDay >= calendarNote.StartDay && intersectedNote.StartDay <= calendarNote.FinishDay) &&
						(intersectedNote.FinishDay >= calendarNote.StartDay && intersectedNote.FinishDay <= calendarNote.FinishDay))
					{
						calendarNote.MediaData = new[] { calendarNote.MediaData, intersectedNote.MediaData }.Join(noteSeparator);
						splittedNotes.Remove(intersectedNote);
						intersectedNote.Splitted = true;
					}
					else if (intersectedNote.StartDay >= calendarNote.StartDay && intersectedNote.StartDay <= calendarNote.FinishDay)
					{
						splittedNotes.Add(new MediaDataNote(this)
						{
							StartDay = calendarNote.StartDay,
							FinishDay = intersectedNote.FinishDay,
							MediaData = new[] { calendarNote.MediaData, intersectedNote.MediaData }.Join(noteSeparator)
						});
						splittedNotes.Remove(calendarNote);
						splittedNotes.Remove(intersectedNote);
						intersectedNote.Splitted = true;
					}
					else if (intersectedNote.FinishDay >= calendarNote.StartDay && intersectedNote.FinishDay <= calendarNote.FinishDay)
					{
						splittedNotes.Add(new MediaDataNote(this)
						{
							StartDay = intersectedNote.StartDay,
							FinishDay = calendarNote.FinishDay,
							MediaData = new[] { calendarNote.MediaData, intersectedNote.MediaData }.Join(noteSeparator)
						});
						splittedNotes.Remove(calendarNote);
						splittedNotes.Remove(intersectedNote);
						intersectedNote.Splitted = true;
					}
				}
				notes.Clear();
				notes.AddRange(splittedNotes);
				notes.ForEach(n => n.Splitted = false);
			}
			while (needToSplit);

			foreach (var calendarNote in Notes.OfType<MediaDataNote>().Where(n => n.EditedByUser))
			{
				notes.Where(n => n.StartDay.Date == calendarNote.StartDay.Date && n.FinishDay.Date == calendarNote.FinishDay.Date).ToList().ForEach(n =>
				{
					n.Note = calendarNote.Note;
					n.BackgroundColor = calendarNote.BackgroundColor;
				});
			}

			Notes.Clear();
			Notes.AddRange(notes);

			UpdateDayAndNoteLinks();
		}

		private IEnumerable<MediaDataNote> GetNotesFromSchedule()
		{
			const string noteSeparator = "   ";
			var notes = new List<MediaDataNote>();
			var programs = ((RegularSchedule)_parentSchedule).ProgramSchedule.Sections.SelectMany(s=>s.Programs).ToList();
			if (Schedule.FlightDateStart.HasValue && Schedule.FlightDateEnd.HasValue)
			{
				notes.AddRange(programs.SelectMany(p => p.Spots)
					.Where(s => s.Count > 0 && s.StartDate.HasValue && s.EndDate.HasValue)
					.GroupBy(g => new { g.StartDate, g.EndDate })
					.Select(g => new MediaDataNote(this)
					{
						StartDay = g.Key.StartDate.Value,
						FinishDay = g.Key.EndDate.Value,
						MediaData = g.Select(sp => sp.FormattedString).Join(noteSeparator)
					}));
			}
			return notes;
		}

		private IEnumerable<MediaDataNote> GetNotesFromSnapshots()
		{
			var notes = new List<MediaDataNote>();

			if (Schedule.FlightDateStart.HasValue && Schedule.FlightDateEnd.HasValue)
			{
				var startDate = Schedule.FlightDateStart.Value;
				var endDate = Schedule.FlightDateEnd.Value;
				while (startDate < endDate)
				{
					var noteEndDate = startDate.AddDays(6);

					notes.AddRange(((RegularSchedule)_parentSchedule).Snapshots
						.Where(s => !s.ActiveWeeks.Any() || s.ActiveWeeks.Any(w => w.StartDate == startDate && w.FinishDate == noteEndDate))
						.Select(s =>
						{
							var textGroup = new TextGroup();
							textGroup.Items.Add(new TextItem(String.Format("{0} - ", s.Name), true));
							var programsText = String.Join(",   ", s.Programs.Select(program =>
								String.Format("{0}{1}  {2}x  {3}",
									s.ShowStation ? String.Format("{0}  ", program.Station) : String.Empty,
									(s.ShowProgram && !String.IsNullOrEmpty(program.Name)) || (s.ShowTime && !String.IsNullOrEmpty(program.Time)) ?
										String.Format("({0}{1})",
											s.ShowProgram && !String.IsNullOrEmpty(program.Name) ? String.Format("{0} ", program.Name) : String.Empty,
											s.ShowTime && !String.IsNullOrEmpty(program.Time) ? String.Format("{0}", program.Time) : String.Empty) :
										String.Empty,
									program.TotalSpots,
									program.StartDayLetter == program.EndDayLetter ?
										program.StartDayLetter :
										String.Format("{0}-{1}", program.StartDayLetter, program.EndDayLetter)
									)
								));
							textGroup.Items.Add(new TextItem(programsText, false));
							return new MediaDataNote(this)
							{
								StartDay = startDate,
								FinishDay = noteEndDate,
								MediaData = textGroup
							};
						})
						);
					startDate = startDate.AddDays(7);
				}
			}
			return notes;
		}

		protected override void ApplyDefaultMonthSettings(CalendarMonth targetMonth)
		{
			targetMonth.OutputData.ShowLogo = MediaMetaData.Instance.ListManager.DefaultBroadcastCalendarSettings.ShowLogo;
			targetMonth.OutputData.ShowBigDate = MediaMetaData.Instance.ListManager.DefaultBroadcastCalendarSettings.ShowBigDate;
		}
	}

	public class CustomCalendar : MediaCalendar
	{
		public CustomCalendar(ISchedule parent) : base(parent) { }

		public override bool AllowCustomNotes
		{
			get { return true; }
		}

		public override void Deserialize(XmlNode node)
		{
			if (_parentSchedule.MondayBased)
				DeserializeInternal<CalendarMonthMediaMondayBased, CalendarDayMondayBased, CommonCalendarNote>(node, _parentSchedule.StartDayOfWeek, _parentSchedule.EndDayOfWeek);
			else
				DeserializeInternal<CalendarMonthMediaSundayBased, CalendarDaySundayBased, CommonCalendarNote>(node, _parentSchedule.StartDayOfWeek, _parentSchedule.EndDayOfWeek);
		}

		protected override void ApplyDefaultMonthSettings(CalendarMonth targetMonth)
		{
			targetMonth.OutputData.ShowLogo = MediaMetaData.Instance.ListManager.DefaultCustomCalendarSettings.ShowLogo;
			targetMonth.OutputData.ShowBigDate = MediaMetaData.Instance.ListManager.DefaultCustomCalendarSettings.ShowBigDate;
		}
	}

	public abstract class CalendarMonthMedia : CalendarMonth
	{
		protected CalendarMonthMedia(Calendar.Calendar parent)
			: base(parent)
		{
			OutputData = new MediaCalendarOutputData(this);
		}

		public override DateTime Date
		{
			get { return _date; }
			set { _date = value; }
		}
	}

	public class CalendarMonthMediaMondayBased : CalendarMonthMedia
	{
		public CalendarMonthMediaMondayBased(Calendar.Calendar parent) : base(parent) { }
	}

	public class CalendarMonthMediaSundayBased : CalendarMonthMedia
	{
		public CalendarMonthMediaSundayBased(Calendar.Calendar parent) : base(parent) { }
	}

	public class MediaCalendarOutputData : CalendarOutputData
	{
		public MediaCalendarOutputData(CalendarMonth parent)
			: base(parent)
		{
			ApplyForAllCustomComment = false;
			ShowLogo = false;
		}
	}

	public class MediaMonthTemplate
	{
		public DateTime? Month { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Month":
						{
							DateTime tempDateTime;
							if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
								Month = tempDateTime;
						}
						break;
					case "StartDate":
						{
							DateTime tempDateTime;
							if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
								StartDate = tempDateTime;
						}
						break;
					case "EndDate":
						{
							DateTime tempDateTime;
							if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
								EndDate = tempDateTime;
						}
						break;
				}
			}
		}
	}

	public class MediaDataNote : CalendarNote
	{
		public TextGroup MediaData { get; set; }
		public bool EditedByUser { get; private set; }
		public bool Splitted { get; set; }

		public override ITextItem Note
		{
			get { return _note ?? MediaData; }
			set
			{
				if (!MediaData.IsEqual(value))
					_note = value;
				EditedByUser = EditedByUser || _note != null;
			}
		}

		public override Color BackgroundColor
		{
			get { return _backgroundColor; }
			set
			{
				_backgroundColor = value;
				EditedByUser = EditedByUser || _backgroundColor != DefaultBackgroundColor;
			}
		}

		public MediaDataNote(BroadcastCalendar parent) : base(parent) { }

		public override string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(base.Serialize());
			result.AppendLine(@"<EditedByUser>" + EditedByUser + @"</EditedByUser>");
			return result.ToString();
		}

		public override void Deserialize(XmlNode node)
		{
			base.Deserialize(node);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "EditedByUser":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								EditedByUser = temp;
						}
						break;
				}
			}
		}

		public void Reset()
		{
			_note = null;
			_backgroundColor = DefaultBackgroundColor;
			EditedByUser = false;
		}
	}
}
