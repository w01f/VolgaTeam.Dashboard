using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Microsoft.VisualBasic;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using ListManager = NewBizWiz.Core.AdSchedule.ListManager;

namespace NewBizWiz.Core.Calendar
{
	public enum ColorOption
	{
		BlackWhite = 0,
		SpotColor,
		FullColor
	}

	public class ScheduleManager
	{
		private Schedule _currentSchedule;

		public bool CalendarLoaded { get; set; }
		public event EventHandler<ScheduleSaveEventArgs> SettingsSaved;

		public string CurrentAdvertiser
		{
			get { return _currentSchedule != null ? _currentSchedule.BusinessName : null; }
		}

		public static ShortSchedule[] GetShortScheduleExtendedList()
		{
			var schedules = new List<ShortSchedule>();
			var saveFolder = new DirectoryInfo(SettingsManager.Instance.SaveFolder);
			if (saveFolder.Exists)
				schedules.AddRange(GetShortScheduleList(saveFolder));
			return schedules.ToArray();
		}

		public static ShortSchedule[] GetShortScheduleList(DirectoryInfo rootFolder)
		{
			var scheduleList = rootFolder.GetFiles("*.xml").Select(file => new ShortSchedule(file)).ToList();
			scheduleList.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x.ShortFileName, y.ShortFileName));
			return scheduleList.ToArray();
		}

		public void CreateSchedule(string scheduleName)
		{
			string calendarFilePath = GetScheduleFileName(scheduleName);
			OpenSchedule(calendarFilePath);
		}

		public void OpenSchedule(string scheduleFilePath)
		{
			_currentSchedule = new Schedule(scheduleFilePath);
			CalendarLoaded = true;
		}

		public static string GetScheduleFileName(string calendarName)
		{
			return Path.Combine(SettingsManager.Instance.SaveFolder, calendarName + ".xml");
		}

		public Schedule GetLocalSchedule()
		{
			return new Schedule(_currentSchedule.CalendarFile.FullName);
		}

		public ShortSchedule GetShortSchedule()
		{
			return new ShortSchedule(_currentSchedule.CalendarFile);
		}

		public void SaveSchedule(Schedule localCalendar, bool quickSave, Control sender)
		{
			localCalendar.Save();
			_currentSchedule = localCalendar;
			if (SettingsSaved != null)
				SettingsSaved(sender, new ScheduleSaveEventArgs(quickSave));
		}

		public void RemoveInstance()
		{
			SettingsSaved = null;
		}
	}

	public class ShortSchedule
	{
		private readonly FileInfo _calendarFile;

		public ShortSchedule(FileInfo file)
		{
			BusinessName = string.Empty;
			Status = ListManager.Instance.Statuses.FirstOrDefault();
			_calendarFile = file;
			Load();
		}

		public string BusinessName { get; set; }
		public string Status { get; set; }

		public string ShortFileName
		{
			get { return _calendarFile.Name.Replace(_calendarFile.Extension, ""); }
		}

		public string FullFileName
		{
			get { return _calendarFile.FullName; }
		}

		public DateTime LastModifiedDate
		{
			get { return _calendarFile.LastWriteTime; }
		}

		private void Load()
		{
			if (_calendarFile.Exists)
			{
				var document = new XmlDocument();
				document.Load(_calendarFile.FullName);

				var node = document.SelectSingleNode(@"/Schedule/BusinessName");
				if (node != null)
					BusinessName = node.InnerText;

				node = document.SelectSingleNode(@"/Schedule/Status");
				if (node != null)
					Status = node.InnerText;
			}
		}

		public void Save()
		{
			if (_calendarFile.Exists)
			{
				try
				{
					var document = new XmlDocument();
					document.Load(_calendarFile.FullName);

					var node = document.SelectSingleNode(@"/Schedule/Status");
					if (node != null)
						node.InnerText = Status;
					else
					{
						node = document.SelectSingleNode(@"/Schedule");
						if (node != null)
							node.InnerXml += (@"<Status>" + (Status != null ? Status.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + @"</Status>");
					}
					document.Save(_calendarFile.FullName);
				}
				catch { }
			}
		}
	}

	public class Schedule : ISchedule
	{
		public Schedule(string fileName)
		{
			BusinessName = string.Empty;
			DecisionMaker = string.Empty;
			ClientType = string.Empty;
			AccountNumber = string.Empty;
			Status = ListManager.Instance.Statuses.FirstOrDefault();

			_calendarFile = new FileInfo(fileName);
			if (!File.Exists(fileName))
			{
				var xml = new StringBuilder();
				xml.AppendLine(@"<Schedule>");
				xml.AppendLine(@"<Status>" + (Status != null ? Status.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + @"</Status>");
				xml.AppendLine(@"</Schedule>");
				using (var sw = new StreamWriter(_calendarFile.FullName, false))
				{
					sw.Write(xml);
					sw.Flush();
				}
				_calendarFile = new FileInfo(fileName);
			}
			else
				LoadSettings();
			LoadCalendars();
		}

		private FileInfo _calendarFile { get; set; }
		public bool IsNew { get; set; }
		public string BusinessName { get; set; }
		public string DecisionMaker { get; set; }
		public string ClientType { get; set; }
		public string AccountNumber { get; set; }
		public string Status { get; set; }
		public DateTime? PresentationDate { get; set; }
		public DateTime? FlightDateStart { get; set; }
		public DateTime? FlightDateEnd { get; set; }
		public IScheduleViewSettings SharedViewSettings { get; private set; }

		public Calendar GraphicCalendar { get; set; }

		public string Name
		{
			get { return _calendarFile.Name.Replace(_calendarFile.Extension, ""); }
			set { _calendarFile = new FileInfo(Path.Combine(_calendarFile.Directory.FullName, value + ".xml")); }
		}

		public FileInfo CalendarFile
		{
			get { return _calendarFile; }
		}

		public string FlightDates
		{
			get
			{
				if (FlightDateStart.HasValue && FlightDateEnd.HasValue)
					return FlightDateStart.Value.ToString("MM/dd/yy") + " - " + FlightDateEnd.Value.ToString("MM/dd/yy");
				return string.Empty;
			}
		}

		public int TotalWeeks
		{
			get
			{
				var datesRange = FlightDateEnd - FlightDateStart;
				return datesRange.HasValue ? datesRange.Value.Days / 7 + 1 : 0;
			}
		}

		public string ThemeName { get; set; }

		private void LoadSettings()
		{
			if (!_calendarFile.Exists) return;
			var document = new XmlDocument();
			document.Load(_calendarFile.FullName);

			var node = document.SelectSingleNode(@"/Schedule/BusinessName");
			if (node != null)
				BusinessName = node.InnerText;

			node = document.SelectSingleNode(@"/Schedule/DecisionMaker");
			if (node != null)
				DecisionMaker = node.InnerText;

			node = document.SelectSingleNode(@"/Schedule/ClientType");
			if (node != null)
				ClientType = node.InnerText;

			node = document.SelectSingleNode(@"/Schedule/AccountNumber");
			if (node != null)
				AccountNumber = node.InnerText;

			node = document.SelectSingleNode(@"/Schedule/Status");
			if (node != null)
				Status = node.InnerText;

			node = document.SelectSingleNode(@"/Schedule/PresentationDate");
			DateTime tempDateTime;
			if (node != null)
				if (DateTime.TryParse(node.InnerText, out tempDateTime))
					PresentationDate = tempDateTime;

			node = document.SelectSingleNode(@"/Schedule/FlightDateStart");
			if (node != null)
				if (DateTime.TryParse(node.InnerText, out tempDateTime))
					FlightDateStart = tempDateTime;

			node = document.SelectSingleNode(@"/Schedule/FlightDateEnd");
			if (node != null)
				if (DateTime.TryParse(node.InnerText, out tempDateTime))
					FlightDateEnd = tempDateTime;
		}

		private void LoadCalendars()
		{
			GraphicCalendar = new CalendarSundayBased(this);
			if (!_calendarFile.Exists) return;
			var document = new XmlDocument();
			document.Load(_calendarFile.FullName);

			var node = document.SelectSingleNode(@"/Schedule/GraphicCalendar");
			if (node != null)
			{
				GraphicCalendar.Deserialize(node);
			}
			else
			{
				GraphicCalendar.UpdateDaysCollection();
				GraphicCalendar.UpdateMonthCollection();
				GraphicCalendar.UpdateNotesCollection();
			}
		}

		public void Save()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<Schedule>");
			xml.AppendLine(@"<BusinessName>" + BusinessName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</BusinessName>");
			Common.ListManager.Instance.Advertisers.Add(BusinessName);
			Common.ListManager.Instance.Advertisers.Save();

			xml.AppendLine(@"<DecisionMaker>" + DecisionMaker.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DecisionMaker>");
			Common.ListManager.Instance.DecisionMakers.Add(DecisionMaker);
			Common.ListManager.Instance.DecisionMakers.Save();

			xml.AppendLine(@"<Status>" + (Status != null ? Status.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + @"</Status>");
			xml.AppendLine(@"<ClientType>" + ClientType.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</ClientType>");
			xml.AppendLine(@"<AccountNumber>" + AccountNumber.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</AccountNumber>");
			if (PresentationDate.HasValue)
				xml.AppendLine(@"<PresentationDate>" + PresentationDate.Value + @"</PresentationDate>");
			if (FlightDateStart.HasValue)
				xml.AppendLine(@"<FlightDateStart>" + FlightDateStart.Value + @"</FlightDateStart>");
			if (FlightDateEnd.HasValue)
				xml.AppendLine(@"<FlightDateEnd>" + FlightDateEnd.Value + @"</FlightDateEnd>");

			xml.AppendLine(@"<GraphicCalendar>" + GraphicCalendar.Serialize() + @"</GraphicCalendar>");

			xml.AppendLine(@"</Schedule>");

			using (var sw = new StreamWriter(_calendarFile.FullName, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}
	}

	public abstract class Calendar
	{
		protected Calendar(ISchedule parent)
		{
			Schedule = parent;
			Months = new List<CalendarMonth>();
			Days = new List<CalendarDay>();
			Notes = new List<CalendarNote>();
		}

		public ISchedule Schedule { get; private set; }
		public List<CalendarMonth> Months { get; private set; }
		public List<CalendarDay> Days { get; private set; }
		public List<CalendarNote> Notes { get; private set; }
		public abstract bool AllowCustomNotes { get; }

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<Days>");
			foreach (var day in Days)
				result.AppendLine(@"<Day>" + day.Serialize() + @"</Day>");
			result.AppendLine(@"</Days>");

			result.AppendLine(@"<Months>");
			foreach (var month in Months)
				result.AppendLine(@"<Month>" + month.Serialize() + @"</Month>");
			result.AppendLine(@"</Months>");

			result.AppendLine(@"<CalendarNotes>");
			foreach (var note in Notes)
				result.AppendLine(@"<CalendarNote>" + note.Serialize() + @"</CalendarNote>");
			result.AppendLine(@"</CalendarNotes>");
			return result.ToString();
		}

		public abstract void Deserialize(XmlNode node);

		public abstract void UpdateDaysCollection();

		public abstract void UpdateMonthCollection();

		public abstract IEnumerable<DateRange> CalculateDateRange(IEnumerable<DateTime> dates);

		public abstract DateTime[][] GetDaysByWeek(DateTime start, DateTime end);

		public virtual void Reset() { }

		public virtual void UpdateNotesCollection()
		{
			if (Schedule.FlightDateStart.HasValue && Schedule.FlightDateEnd.HasValue)
			{
				var _notesToDelete = new List<CalendarNote>();
				foreach (var note in Notes)
				{
					if (note.FinishDay < Schedule.FlightDateStart.Value || note.StartDay > Schedule.FlightDateEnd.Value)
						_notesToDelete.Add(note);
					else if (note.StartDay < Schedule.FlightDateStart.Value)
						note.StartDay = Schedule.FlightDateStart.Value;
					else if (note.FinishDay > Schedule.FlightDateEnd.Value)
						note.FinishDay = Schedule.FlightDateEnd.Value;
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
			newNote.StartDay = range.StartDate;
			newNote.FinishDay = range.FinishDate;
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

		protected void Deserialize<TMonth, TDay, TNote>(XmlNode node, DayOfWeek startDay, DayOfWeek endDay)
			where TMonth : CalendarMonth
			where TDay : CalendarDay
			where TNote : CalendarNote
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Days":
						Days.Clear();
						foreach (XmlNode dayNode in childNode.ChildNodes)
						{
							var day = (TDay)Activator.CreateInstance(typeof(TDay), this);
							day.Deserialize(dayNode);
							Days.Add(day);
						}
						break;
					case "Months":
						Months.Clear();
						foreach (XmlNode monthNode in childNode.ChildNodes)
						{
							var month = (TMonth)Activator.CreateInstance(typeof(TMonth), this);
							month.Deserialize(monthNode);
							Months.Add(month);
						}
						break;
					case "CalendarNotes":
						Notes.Clear();
						foreach (XmlNode noteNode in childNode.ChildNodes)
						{
							var note = (TNote)Activator.CreateInstance(typeof(TNote), this);
							note.Deserialize(noteNode);
							if (note.StartDay != DateTime.MinValue && note.FinishDay != DateTime.MinValue)
								Notes.Add(note);
						}
						break;
				}
			}

			UpdateDaysCollection();
			UpdateMonthCollection();
			UpdateNotesCollection();
		}

		protected void UpdateDaysCollection<T>(DayOfWeek startDay, DayOfWeek endDay) where T : CalendarDay
		{
			if (Schedule.FlightDateStart.HasValue && Schedule.FlightDateEnd.HasValue)
			{
				var days = new List<CalendarDay>();

				var startDate = new DateTime(Schedule.FlightDateStart.Value.Year, Schedule.FlightDateStart.Value.Month, 1);
				while (startDate.DayOfWeek != startDay)
					startDate = startDate.AddDays(-1);

				var endDate = new DateTime(Schedule.FlightDateEnd.Value.Month < 12 ? Schedule.FlightDateEnd.Value.Year : (Schedule.FlightDateEnd.Value.Year + 1), (Schedule.FlightDateEnd.Value.Month < 12 ? Schedule.FlightDateEnd.Value.Month + 1 : 1), 1).AddDays(-1);
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
					day.BelongsToSchedules = day.Date >= Schedule.FlightDateStart & day.Date <= Schedule.FlightDateEnd;
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
			if (Schedule.FlightDateStart.HasValue && Schedule.FlightDateEnd.HasValue)
			{
				var months = new List<CalendarMonth>();
				var startDate = new DateTime(Schedule.FlightDateStart.Value.Year, Schedule.FlightDateStart.Value.Month, 1);
				while (startDate <= Schedule.FlightDateEnd.Value)
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

	public class CalendarSundayBased : Calendar
	{
		public CalendarSundayBased(ISchedule parent)
			: base(parent) { }

		public override bool AllowCustomNotes
		{
			get { return true; }
		}

		public override void Deserialize(XmlNode node)
		{
			Deserialize<CalendarMonthSundayBased, CalendarDaySundayBased, CommonCalendarNote>(node, DayOfWeek.Sunday, DayOfWeek.Saturday);
		}

		public override void UpdateDaysCollection()
		{
			UpdateDaysCollection<CalendarDaySundayBased>(DayOfWeek.Sunday, DayOfWeek.Saturday);
		}

		public override void UpdateMonthCollection()
		{
			UpdateMonthCollection<CalendarMonthSundayBased>();
		}

		public override DateTime[][] GetDaysByWeek(DateTime start, DateTime end)
		{
			return GetDaysByWeek(start, end, DayOfWeek.Saturday);
		}

		public override IEnumerable<DateRange> CalculateDateRange(IEnumerable<DateTime> dates)
		{
			return CalculateDateRange(dates, DayOfWeek.Sunday, DayOfWeek.Saturday);
		}
	}

	public class CalendarMondayBased : Calendar
	{
		public CalendarMondayBased(ISchedule parent)
			: base(parent) { }

		public override bool AllowCustomNotes
		{
			get { return true; }
		}

		public override void Deserialize(XmlNode node)
		{
			Deserialize<CalendarMonthMondayBased, CalendarDayMondayBased, CalendarNote>(node, DayOfWeek.Monday, DayOfWeek.Sunday);
		}

		public override void UpdateDaysCollection()
		{
			UpdateDaysCollection<CalendarDayMondayBased>(DayOfWeek.Monday, DayOfWeek.Sunday);
		}

		public override void UpdateMonthCollection()
		{
			UpdateMonthCollection<CalendarMonthMondayBased>();
		}

		public override DateTime[][] GetDaysByWeek(DateTime start, DateTime end)
		{
			return GetDaysByWeek(start, end, DayOfWeek.Sunday);
		}

		public override IEnumerable<DateRange> CalculateDateRange(IEnumerable<DateTime> dates)
		{
			return CalculateDateRange(dates, DayOfWeek.Monday, DayOfWeek.Sunday);
		}
	}

	public abstract class CalendarMonth
	{
		protected DateTime _date;

		protected CalendarMonth(Calendar parent)
		{
			Parent = parent;
			Days = new List<CalendarDay>();
		}

		public Calendar Parent { get; private set; }
		public DateTime DaysRangeBegin { get; set; }
		public DateTime DaysRangeEnd { get; set; }
		public List<CalendarDay> Days { get; private set; }
		public CalendarOutputData OutputData { get; protected set; }

		public abstract DateTime Date { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<Date>" + _date + @"</Date>");
			result.AppendLine(@"<OutputData>" + OutputData.Serialize() + @"</OutputData>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Date":
						DateTime tempDate;
						if (DateTime.TryParse(childNode.InnerText, out tempDate))
							Date = tempDate;
						break;
					case "OutputData":
						OutputData.Deserialize(childNode);
						break;
				}
			}
		}
	}

	public class CalendarMonthSundayBased : CalendarMonth
	{
		public CalendarMonthSundayBased(Calendar parent)
			: base(parent)
		{
			OutputData = new CommonCalendarOutputData(this);
		}

		public override DateTime Date
		{
			get { return _date; }
			set
			{
				_date = value;
				DaysRangeBegin = _date;
				DaysRangeEnd = _date.AddMonths(1).AddDays(-1);
			}
		}
	}

	public class CalendarMonthMondayBased : CalendarMonth
	{
		public CalendarMonthMondayBased(Calendar parent)
			: base(parent)
		{
			OutputData = new CommonCalendarOutputData(this);
		}

		public override DateTime Date
		{
			get { return _date; }
			set
			{
				_date = value;
				var temp = value;
				while (temp.DayOfWeek != DayOfWeek.Monday)
					temp = temp.AddDays(-1);
				DaysRangeBegin = temp;

				temp = _date.AddMonths(1).AddDays(-1);
				while (temp.DayOfWeek != DayOfWeek.Sunday)
					temp = temp.AddDays(-1);
				DaysRangeEnd = temp;
			}
		}
	}

	public abstract class CalendarDay
	{
		protected string _userData;

		protected CalendarDay(Calendar parent)
		{
			Parent = parent;
			Logo = new ImageSource();
		}

		public Calendar Parent { get; private set; }
		public DateTime Date { get; set; }
		public bool BelongsToSchedules { get; set; }
		public bool HasNotes { get; set; }
		public bool EditedByUser { get; private set; }
		public ImageSource Logo { get; set; }
		public virtual string ImportedData
		{
			get { return String.Empty; }
		}

		public string Comment
		{
			get { return !String.IsNullOrEmpty(_userData) ? _userData : ImportedData; }
			set
			{
				if (ImportedData != value && !String.IsNullOrEmpty(value))
				{
					_userData = value;
					EditedByUser = true;
				}
				else
				{
					_userData = null;
					EditedByUser = false;
				}
			}
		}

		public string Summary
		{
			get
			{
				var result = new StringBuilder();

				if (!string.IsNullOrEmpty(Comment))
					result.AppendLine(Comment);
				return result.ToString();
			}
		}

		public bool ContainsData
		{
			get { return !string.IsNullOrEmpty(Summary) || Logo.ContainsData; }
		}

		public abstract int WeekDayIndex { get; }

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<Date>" + Date + @"</Date>");
			if (!string.IsNullOrEmpty(_userData))
				result.AppendLine(@"<UserData>" + _userData.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</UserData>");
			result.AppendLine(@"<Logo>" + Logo.Serialize() + @"</Logo>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Date":
						DateTime tempDate;
						if (DateTime.TryParse(childNode.InnerText, out tempDate))
							Date = tempDate;
						break;
					case "UserData":
					case "Comment1":
						_userData = childNode.InnerText;
						EditedByUser = true;
						break;
					case "Logo":
						Logo.Deserialize(childNode);
						break;
				}
			}
		}

		public void ClearData()
		{
			_userData = null;
			EditedByUser = false;
			Logo = new ImageSource();
		}
	}

	public class CalendarDaySundayBased : CalendarDay
	{
		public CalendarDaySundayBased(Calendar parent)
			: base(parent) { }

		public override int WeekDayIndex
		{
			get
			{
				switch (Date.DayOfWeek)
				{
					case DayOfWeek.Sunday:
						return 1;
					case DayOfWeek.Monday:
						return 2;
					case DayOfWeek.Tuesday:
						return 3;
					case DayOfWeek.Wednesday:
						return 4;
					case DayOfWeek.Thursday:
						return 5;
					case DayOfWeek.Friday:
						return 6;
					case DayOfWeek.Saturday:
						return 7;
					default:
						return 0;
				}
			}
		}
	}

	public class CalendarDayMondayBased : CalendarDay
	{
		public CalendarDayMondayBased(Calendar parent)
			: base(parent) { }

		public override int WeekDayIndex
		{
			get
			{
				switch (Date.DayOfWeek)
				{
					case DayOfWeek.Monday:
						return 1;
					case DayOfWeek.Tuesday:
						return 2;
					case DayOfWeek.Wednesday:
						return 3;
					case DayOfWeek.Thursday:
						return 4;
					case DayOfWeek.Friday:
						return 5;
					case DayOfWeek.Saturday:
						return 6;
					case DayOfWeek.Sunday:
						return 7;
					default:
						return 0;
				}
			}
		}
	}

	public abstract class CalendarNote
	{
		public static Color DefaultBackgroundColor = Color.LemonChiffon;

		protected ITextItem _note;
		protected Color _backgroundColor;

		protected CalendarNote(Calendar parent)
		{
			Parent = parent;
			_backgroundColor = DefaultBackgroundColor;

			Height = 25f;
		}

		public Calendar Parent { get; private set; }
		public DateTime StartDay { get; set; }
		public DateTime FinishDay { get; set; }
		public bool UserAdded { get; set; }

		public virtual ITextItem Note
		{
			get { return _note; }
			set { _note = value; }
		}

		public virtual Color BackgroundColor
		{
			get { return _backgroundColor; }
			set { _backgroundColor = value; }
		}

		public int Length
		{
			get { return FinishDay.Subtract(StartDay).Days; }
		}

		public Color ForeColor
		{
			get
			{
				var d = 0;
				var a = 1 - (0.299 * BackgroundColor.R + 0.587 * BackgroundColor.G + 0.114 * BackgroundColor.B) / 255;
				d = a < 0.5 ? 0 : 255;
				return Color.FromArgb(d, d, d);
			}
		}

		#region Output Data

		public float Top { get; set; }
		public float Left { get; set; }
		public float Right { get; set; }
		public float Height { get; set; }

		#endregion

		public virtual string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<StartDay>" + StartDay + @"</StartDay>");
			result.AppendLine(@"<FinishDay>" + FinishDay + @"</FinishDay>");
			result.AppendLine(@"<BackgroundColor>" + _backgroundColor.ToArgb() + @"</BackgroundColor>");
			if (_note is TextItem)
				result.AppendLine(@"<TextItem>" + _note.Serialize() + @"</TextItem>");
			else if (_note is TextGroup)
				result.AppendLine(@"<TextGroup>" + _note.Serialize() + @"</TextGroup>");
			return result.ToString();
		}

		public virtual void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				DateTime tempDate;
				switch (childNode.Name)
				{
					case "StartDay":
						if (DateTime.TryParse(childNode.InnerText, out tempDate))
							StartDay = tempDate;
						break;
					case "FinishDay":
						if (DateTime.TryParse(childNode.InnerText, out tempDate))
							FinishDay = tempDate;
						break;
					case "BackgroundColor":
						int tempInt;
						if (Int32.TryParse(childNode.InnerText, out tempInt))
							_backgroundColor = Color.FromArgb(tempInt);
						break;
					case "TextItem":
						{
							_note = new TextItem();
							_note.Deserialize(childNode);
						}
						break;
					case "TextGroup":
						{
							_note = new TextGroup();
							_note.Deserialize(childNode);
						}
						break;
				}
			}
		}
	}

	public class CommonCalendarNote : CalendarNote
	{
		public CommonCalendarNote(Calendar parent) : base(parent) { }
	}

	public class DateRange
	{
		public DateTime StartDate { get; set; }
		public DateTime FinishDate { get; set; }

		public string Range
		{
			get { return StartDate.ToString("MM/dd/yy") + "-" + FinishDate.ToString("MM/dd/yy"); }
		}
	}

	public abstract class CalendarOutputData
	{
		protected readonly List<ImageSource> _dayLogosPaths = new List<ImageSource>();
		protected string _encodedLogo;

		protected CalendarOutputData(CalendarMonth parent)
		{
			Parent = parent;
			Notes = new List<CalendarNote>();

			ShowCustomComment = false;
			ApplyForAllCustomComment = true;

			#region Basic
			ShowHeader = true;
			ShowBusinessName = true;
			ShowDecisionMaker = true;
			Header = String.Empty;
			ApplyForAllBasic = true;
			#endregion

			#region Style

			SlideColor = "gray";
			ApplyForAllThemeColor = true;

			ShowLogo = true;
			var defaultLogoPath = Path.Combine(ListManager.Instance.BigImageFolder.FullName, Common.ListManager.DefaultBigLogoFileName);
			if (File.Exists(defaultLogoPath))
				Logo = new Bitmap(defaultLogoPath);
			ApplyForAllLogo = true;

			ShowBigDate = false;

			#endregion
		}

		public List<CalendarNote> Notes { get; private set; }

		public CalendarMonth Parent { get; private set; }

		#region Basic
		public bool ShowHeader { get; set; }
		public bool ShowBusinessName { get; set; }
		public bool ShowDecisionMaker { get; set; }

		public string Header { get; set; }
		public bool ApplyForAllBasic { get; set; }
		#endregion

		#region Notes
		public bool ShowCustomComment { get; set; }
		public string CustomComment { get; set; }
		public bool ApplyForAllCustomComment { get; set; }
		#endregion

		#region Style
		public string SlideColor { get; set; }
		public bool ApplyForAllThemeColor { get; set; }
		public bool ShowLogo { get; set; }
		public Image Logo { get; set; }
		public bool ApplyForAllLogo { get; set; }
		public bool ShowBigDate { get; set; }
		#endregion

		#region Calculated Options
		public string SlideTitle
		{
			get { return ShowHeader ? Header : string.Empty; }
		}

		public string BusinessName
		{
			get { return !ShowBusinessName ? string.Empty : Parent.Parent.Schedule.BusinessName; }
		}

		public string DecisionMaker
		{
			get { return !ShowDecisionMaker ? string.Empty : Parent.Parent.Schedule.DecisionMaker; }
		}

		public string EncodedLogo
		{
			get
			{
				if (!String.IsNullOrEmpty(_encodedLogo)) return _encodedLogo;
				var converter = TypeDescriptor.GetConverter(typeof(Bitmap));
				_encodedLogo = Convert.ToBase64String((byte[])converter.ConvertTo(Logo, typeof(byte[])));
				return _encodedLogo;
			}
			set { _encodedLogo = value; }
		}

		public int SlideRGB
		{
			get
			{
				int result = Color.Black.ToArgb();
				switch (SlideColor)
				{
					case "black":
						result = Information.RGB(0, 0, 0);
						break;
					case "blue":
						result = Information.RGB(0, 0, 102);
						break;
					case "gray":
						result = Information.RGB(0, 0, 0);
						break;
					case "green":
						result = Information.RGB(0, 51, 0);
						break;
					case "orange":
						result = Information.RGB(153, 0, 0);
						break;
					case "teal":
						result = Information.RGB(0, 51, 102);
						break;
				}
				return result;
			}
		}

		public string LogoFile
		{
			get
			{
				string result = string.Empty;
				if (!ShowLogo || Logo == null) return result;
				result = Path.GetTempFileName();
				Logo.Save(result);
				return result;
			}
		}

		public string MonthText
		{
			get { return Parent.Date.ToString("MMMM yyyy"); }
		}

		public string BackgroundFileName
		{
			get { return String.Format("{0}{1}.png", Parent.Date.ToString("MMM").ToLower(), (ShowBigDate ? "1" : "2")); }
		}

		public string Comments
		{
			get
			{
				var result = string.Empty;
				if (ShowCustomComment)
					result = CustomComment;
				return result;
			}
		}

		public string[] DayOutput
		{
			get { return Parent.Days.Select(x => x.Summary).ToArray(); }
		}

		public List<ImageSource> DayLogoPaths
		{
			get { return _dayLogosPaths; }
		}

		public float FontSize
		{
			get { return 7; }
		}

		public virtual string TagA
		{
			get { return String.Empty; }
		}

		public virtual string TagB
		{
			get { return String.Empty; }
		}

		public virtual string TagC
		{
			get { return String.Empty; }
		}

		public virtual string TagD
		{
			get { return String.Empty; }
		}
		#endregion

		public virtual string Serialize()
		{
			var result = new StringBuilder();
			TypeDescriptor.GetConverter(typeof(Bitmap));

			#region Basic
			result.AppendLine(@"<ShowHeader>" + ShowHeader + @"</ShowHeader>");
			result.AppendLine(@"<ShowBusinessName>" + ShowBusinessName + @"</ShowBusinessName>");
			result.AppendLine(@"<ShowDecisionMaker>" + ShowDecisionMaker + @"</ShowDecisionMaker>");
			result.AppendLine(@"<Header>" + Header.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Header>");
			result.AppendLine(@"<ApplyForAllBasic>" + ApplyForAllBasic + @"</ApplyForAllBasic>");
			#endregion

			#region Notes
			result.AppendLine(@"<ShowCustomComment>" + ShowCustomComment + @"</ShowCustomComment>");
			if (!string.IsNullOrEmpty(CustomComment))
				result.AppendLine(@"<CustomComment>" + CustomComment.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</CustomComment>");
			result.AppendLine(@"<ApplyForAllCustomComment>" + ApplyForAllCustomComment + @"</ApplyForAllCustomComment>");
			#endregion

			#region Style

			result.AppendLine(@"<SlideColor>" + SlideColor.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideColor>");
			result.AppendLine(@"<ApplyForAllThemeColor>" + ApplyForAllThemeColor + @"</ApplyForAllThemeColor>");

			result.AppendLine(@"<ShowLogo>" + ShowLogo + @"</ShowLogo>");
			if (!String.IsNullOrEmpty(EncodedLogo))
				result.AppendLine(@"<Logo>" + EncodedLogo.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Logo>");
			result.AppendLine(@"<ApplyForAllLogo>" + ApplyForAllLogo + @"</ApplyForAllLogo>");

			result.AppendLine(@"<ShowBigDate>" + ShowBigDate + @"</ShowBigDate>");

			#endregion

			return result.ToString();
		}

		public virtual void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				bool tempBool;
				switch (childNode.Name)
				{
					#region Basic
					case "ShowHeader":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowHeader = tempBool;
						break;
					case "ShowBusinessName":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowBusinessName = tempBool;
						break;
					case "ShowDecisionMaker":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDecisionMaker = tempBool;
						break;
					case "Header":
						Header = childNode.InnerText;
						break;
					case "ApplyForAllBasic":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ApplyForAllBasic = tempBool;
						break;
					#endregion

					#region Notes
					case "ShowCustomComment":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowCustomComment = tempBool;
						break;
					case "CustomComment":
						CustomComment = childNode.InnerText;
						break;
					case "ApplyForAllCustomComment":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ApplyForAllCustomComment = tempBool;
						break;
					#endregion

					#region Style
					case "SlideColor":
						SlideColor = childNode.InnerText;
						break;
					case "ApplyForAllThemeColor":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ApplyForAllThemeColor = tempBool;
						break;

					case "ShowLogo":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLogo = tempBool;
						break;
					case "Logo":
						if (string.IsNullOrEmpty(childNode.InnerText))
							Logo = null;
						else
						{
							Logo = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
							_encodedLogo = childNode.InnerText;
						}
						break;
					case "ApplyForAllLogo":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ApplyForAllLogo = tempBool;
						break;

					case "ShowBigDate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowBigDate = tempBool;
						break;
					#endregion
				}
			}
		}

		public void PrepareDayLogoPaths()
		{
			_dayLogosPaths.Clear();
			foreach (var day in Parent.Days)
			{
				if (day.Logo.ContainsData)
				{
					day.Logo.PrepareOutputFile();
					_dayLogosPaths.Add(day.Logo);
				}
				else
					_dayLogosPaths.Add(new ImageSource());
			}
		}

		public void PrepareNotes()
		{
			Notes.Clear();
			Notes.AddRange(Parent.Parent.Notes.Where(x => x.StartDay >= Parent.DaysRangeBegin && x.FinishDay <= Parent.DaysRangeEnd));
		}
	}

	public class CommonCalendarOutputData : CalendarOutputData
	{
		public CommonCalendarOutputData(CalendarMonth parent) : base(parent) { }
	}

	public class CalendarLegend
	{
		public CalendarLegend()
		{
			Code = string.Empty;
			Description = string.Empty;
			Visible = true;
		}

		public string Code { get; set; }
		public string Description { get; set; }
		public bool Visible { get; set; }

		public string StringRepresentation
		{
			get { return Code + " = " + Description; }
		}

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<Code>" + Code.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Code>");
			result.AppendLine(@"<Description>" + Description.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Description>");
			result.AppendLine(@"<Visible>" + Visible + @"</Visible>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Code":
						Code = childNode.InnerText;
						break;
					case "Description":
						Description = childNode.InnerText;
						break;
					case "Visible":
						bool tempBool;
						if (bool.TryParse(childNode.InnerText, out tempBool))
							Visible = tempBool;
						break;
				}
			}
		}

		public CalendarLegend Clone()
		{
			var result = new CalendarLegend();
			result.Code = Code;
			result.Description = Description;
			result.Visible = Visible;
			return result;
		}
	}
}