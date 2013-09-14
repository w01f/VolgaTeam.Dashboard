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
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;
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
		public event EventHandler<SavingingEventArgs> SettingsSaved;

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
			var calendarList = new List<ShortSchedule>();
			foreach (FileInfo file in rootFolder.GetFiles("*.xml"))
			{
				var schedule = new ShortSchedule(file);
				calendarList.Add(schedule);
			}
			return calendarList.ToArray();
		}

		public void CreateSchedule(string scheduleName)
		{
			string calendarFilePath = GetScheduleFileName(scheduleName);
			OpenSchedule(calendarFilePath);
		}

		public static void ImportSchedule(string sourceSchedulePath, string newName)
		{
			var sourceSchedule = new AdSchedule.Schedule(sourceSchedulePath);

			var newSchedule = new Schedule(GetScheduleFileName(newName));

			newSchedule.BusinessName = sourceSchedule.BusinessName;
			newSchedule.DecisionMaker = sourceSchedule.DecisionMaker;
			newSchedule.ClientType = sourceSchedule.ClientType;
			newSchedule.PresentationDate = sourceSchedule.PresentationDate;
			newSchedule.FlightDateStart = sourceSchedule.FlightDateStart;
			newSchedule.FlightDateEnd = sourceSchedule.FlightDateEnd;
			newSchedule.Status = ListManager.Instance.Statuses.FirstOrDefault();
			newSchedule.ShowNewspaper = true;
			newSchedule.ShowDigital = true;
			newSchedule.ShowTV = false;
			newSchedule.ShowRadio = false;

			newSchedule.GraphicCalendar = new CalendarSundayBased(newSchedule);
			newSchedule.GraphicCalendar.UpdateDaysCollection();
			newSchedule.GraphicCalendar.UpdateMonthCollection();
			newSchedule.GraphicCalendar.UpdateNotesCollection();
			newSchedule.GraphicCalendar.ImportDays(sourceSchedule);

			newSchedule.Save();
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
				SettingsSaved(sender, new SavingingEventArgs(quickSave));
		}

		public void RemoveInstance()
		{
			SettingsSaved = null;
		}
	}

	public class SavingingEventArgs : EventArgs
	{
		public SavingingEventArgs(bool quickSave)
		{
			QuickSave = quickSave;
		}

		public bool QuickSave { get; set; }
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
			XmlNode node;
			if (_calendarFile.Exists)
			{
				var document = new XmlDocument();
				document.Load(_calendarFile.FullName);

				node = document.SelectSingleNode(@"/Schedule/BusinessName");
				if (node != null)
					BusinessName = node.InnerText;

				node = document.SelectSingleNode(@"/Schedule/Status");
				if (node != null)
					Status = node.InnerText;
			}
		}

		public void Save()
		{
			XmlNode node;
			if (_calendarFile.Exists)
			{
				try
				{
					var document = new XmlDocument();
					document.Load(_calendarFile.FullName);

					node = document.SelectSingleNode(@"/Schedule/Status");
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

	public class Schedule
	{
		public Schedule(string fileName)
		{
			BusinessName = string.Empty;
			DecisionMaker = string.Empty;
			ClientType = string.Empty;
			AccountNumber = string.Empty;
			Status = ListManager.Instance.Statuses.FirstOrDefault();
			ShowNewspaper = true;
			ShowDigital = true;
			ShowTV = false;
			ShowRadio = false;

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
		public string BusinessName { get; set; }
		public string DecisionMaker { get; set; }
		public string ClientType { get; set; }
		public string AccountNumber { get; set; }
		public string Status { get; set; }
		public DateTime? PresentationDate { get; set; }
		public DateTime? FlightDateStart { get; set; }
		public DateTime? FlightDateEnd { get; set; }

		public Calendar GraphicCalendar { get; set; }

		public bool ShowNewspaper { get; set; }
		public bool ShowDigital { get; set; }
		public bool ShowTV { get; set; }
		public bool ShowRadio { get; set; }

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
				else
					return string.Empty;
			}
		}

		private void LoadSettings()
		{
			int tempInt;
			DateTime tempDateTime;
			bool tempBool;

			XmlNode node;
			if (_calendarFile.Exists)
			{
				var document = new XmlDocument();
				document.Load(_calendarFile.FullName);

				node = document.SelectSingleNode(@"/Schedule/BusinessName");
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

				node = document.SelectSingleNode(@"/Schedule/ShowNewspaper");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						ShowNewspaper = tempBool;

				node = document.SelectSingleNode(@"/Schedule/ShowDigital");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						ShowDigital = tempBool;

				node = document.SelectSingleNode(@"/Schedule/ShowTV");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						ShowTV = tempBool;

				node = document.SelectSingleNode(@"/Schedule/ShowRadio");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						ShowRadio = tempBool;
			}
		}

		private void LoadCalendars()
		{
			GraphicCalendar = new CalendarSundayBased(this);
			if (_calendarFile.Exists)
			{
				XmlNode node;
				var document = new XmlDocument();
				document.Load(_calendarFile.FullName);

				node = document.SelectSingleNode(@"/Schedule/GraphicCalendar");
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
		}

		public void Save()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<Schedule>");
			xml.AppendLine(@"<BusinessName>" + BusinessName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</BusinessName>");
			if (!Common.ListManager.Instance.Advertisers.Contains(BusinessName))
			{
				Common.ListManager.Instance.Advertisers.Add(BusinessName);
				Common.ListManager.Instance.SaveAdvertisers();
			}
			xml.AppendLine(@"<DecisionMaker>" + DecisionMaker.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DecisionMaker>");
			if (!Common.ListManager.Instance.DecisionMakers.Contains(DecisionMaker))
			{
				Common.ListManager.Instance.DecisionMakers.Add(DecisionMaker);
				Common.ListManager.Instance.SaveDecisionMakers();
			}
			xml.AppendLine(@"<Status>" + (Status != null ? Status.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + @"</Status>");
			xml.AppendLine(@"<ClientType>" + ClientType.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</ClientType>");
			xml.AppendLine(@"<AccountNumber>" + AccountNumber.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</AccountNumber>");
			xml.AppendLine(@"<ShowNewspaper>" + ShowNewspaper + @"</ShowNewspaper>");
			xml.AppendLine(@"<ShowDigital>" + ShowDigital + @"</ShowDigital>");
			xml.AppendLine(@"<ShowTV>" + ShowTV + @"</ShowTV>");
			xml.AppendLine(@"<ShowRadio>" + ShowRadio + @"</ShowRadio>");
			if (PresentationDate.HasValue)
				xml.AppendLine(@"<PresentationDate>" + PresentationDate.Value.ToString() + @"</PresentationDate>");
			if (FlightDateStart.HasValue)
				xml.AppendLine(@"<FlightDateStart>" + FlightDateStart.Value.ToString() + @"</FlightDateStart>");
			if (FlightDateEnd.HasValue)
				xml.AppendLine(@"<FlightDateEnd>" + FlightDateEnd.Value.ToString() + @"</FlightDateEnd>");

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
		public Calendar(Schedule parent)
		{
			Schedule = parent;
			Months = new List<CalendarMonth>();
			Days = new List<CalendarDay>();
			Notes = new List<CalendarNote>();
		}

		public Schedule Schedule { get; private set; }
		public List<CalendarMonth> Months { get; private set; }
		public List<CalendarDay> Days { get; private set; }
		public List<CalendarNote> Notes { get; private set; }

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<Days>");
			foreach (CalendarDay day in Days)
				result.AppendLine(@"<Day>" + day.Serialize() + @"</Day>");
			result.AppendLine(@"</Days>");

			result.AppendLine(@"<Months>");
			foreach (CalendarMonth month in Months)
				result.AppendLine(@"<Month>" + month.Serialize() + @"</Month>");
			result.AppendLine(@"</Months>");

			result.AppendLine(@"<CalendarNotes>");
			foreach (CalendarNote note in Notes)
				result.AppendLine(@"<CalendarNote>" + note.Serialize() + @"</CalendarNote>");
			result.AppendLine(@"</CalendarNotes>");
			return result.ToString();
		}

		public abstract void Deserialize(XmlNode node);

		public abstract void UpdateDaysCollection();

		public abstract void UpdateMonthCollection();

		public abstract DateRange[] CalculateDateRange(DateTime[] dates);

		public abstract DateTime[][] GetDaysByWeek(DateTime start, DateTime end);

		public void UpdateNotesCollection()
		{
			if (Schedule.FlightDateStart.HasValue && Schedule.FlightDateEnd.HasValue)
			{
				var _notesToDelete = new List<CalendarNote>();
				foreach (CalendarNote note in Notes)
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
				foreach (CalendarNote note in _notesToDelete)
					Notes.Remove(note);
			}
			else
				Notes.Clear();
			UpdateDayAndNoteLinks();
		}

		protected void UpdateDayAndNoteLinks()
		{
			foreach (CalendarDay day in Days)
				day.HasNotes = Notes.Where(x => day.Date >= x.StartDay && day.Date <= x.FinishDay).Count() > 0;
		}

		public void AddNote(DateRange range, string noteText = "")
		{
			var newNote = new CalendarNote(this);
			newNote.StartDay = range.StartDate;
			newNote.FinishDay = range.FinishDate;
			newNote.Note = noteText;
			var _notesToDelete = new List<CalendarNote>();
			foreach (CalendarNote note in Notes)
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
			foreach (CalendarNote note in _notesToDelete)
				Notes.Remove(note);
			Notes.Add(newNote);
			UpdateDayAndNoteLinks();
		}

		public void DeleteNote(CalendarNote note)
		{
			Notes.Remove(note);
			UpdateDayAndNoteLinks();
		}

		public void ImportDays(AdSchedule.Schedule sourceSchedule)
		{
			foreach (CalendarDay day in Days)
			{
				var customNoteConstructor = new StringBuilder();
				foreach (PrintProduct publication in sourceSchedule.PrintProducts)
					foreach (var insert in publication.Inserts.Where(x => x.Date.Year == day.Date.Year && x.Date.Month == day.Date.Month && x.Date.Day == day.Date.Day))
					{
						var properties = new List<string>();
						if (!string.IsNullOrEmpty(insert.Publication))
							properties.Add(sourceSchedule.ViewSettings.CalendarViewSettings.ShowAbbreviationOnly ? insert.PublicationAbbreviation : insert.Publication);
						if (!string.IsNullOrEmpty(insert.FullSection) && sourceSchedule.ViewSettings.CalendarViewSettings.ShowSection)
							properties.Add(insert.FullSection);
						if (!string.IsNullOrEmpty(insert.DimensionsShort) && sourceSchedule.ViewSettings.CalendarViewSettings.ShowAdSize)
							properties.Add(insert.DimensionsShort);
						if (!string.IsNullOrEmpty(insert.PageSize) && sourceSchedule.ViewSettings.CalendarViewSettings.ShowPageSize)
							properties.Add(insert.PageSize);
						if (!string.IsNullOrEmpty(insert.PercentOfPage) && sourceSchedule.ViewSettings.CalendarViewSettings.ShowPercentOfPage)
							properties.Add(insert.PercentOfPage);
						if (!string.IsNullOrEmpty(insert.PublicationColor) && sourceSchedule.ViewSettings.CalendarViewSettings.ShowColor)
							properties.Add(insert.PublicationColor);
						if (sourceSchedule.ViewSettings.CalendarViewSettings.ShowAvgCost)
							properties.Add(insert.FinalRate.ToString("$#,##0"));
						customNoteConstructor.AppendLine(string.Join(", ", properties.ToArray()));
					}
				day.Comment1 = customNoteConstructor.ToString();
			}
		}
	}

	public class CalendarSundayBased : Calendar
	{
		public CalendarSundayBased(Schedule parent)
			: base(parent) { }

		public override void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Days":
						Days.Clear();
						foreach (XmlNode dayNode in childNode.ChildNodes)
						{
							CalendarDay day = new CalendarDaySundayBased(this);
							day.Deserialize(dayNode);
							Days.Add(day);
						}
						break;
					case "Months":
						Months.Clear();
						foreach (XmlNode monthNode in childNode.ChildNodes)
						{
							CalendarMonth month = new CalendarMonthSundayBased(this);
							month.Deserialize(monthNode);
							Months.Add(month);
						}
						break;
					case "CalendarNotes":
						Notes.Clear();
						foreach (XmlNode noteNode in childNode.ChildNodes)
						{
							var note = new CalendarNote(this);
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

		public override void UpdateDaysCollection()
		{
			if (Schedule.FlightDateStart.HasValue && Schedule.FlightDateEnd.HasValue)
			{
				var days = new List<CalendarDay>();

				var startDate = new DateTime(Schedule.FlightDateStart.Value.Year, Schedule.FlightDateStart.Value.Month, 1);
				while (startDate.DayOfWeek != DayOfWeek.Sunday)
					startDate = startDate.AddDays(-1);

				DateTime endDate = new DateTime(Schedule.FlightDateEnd.Value.Month < 12 ? Schedule.FlightDateEnd.Value.Year : (Schedule.FlightDateEnd.Value.Year + 1), (Schedule.FlightDateEnd.Value.Month < 12 ? Schedule.FlightDateEnd.Value.Month + 1 : 1), 1).AddDays(-1);
				while (endDate.DayOfWeek != DayOfWeek.Saturday)
					endDate = endDate.AddDays(1);

				while (startDate <= endDate)
				{
					CalendarDay day = Days.Where(x => x.Date.Equals(startDate)).FirstOrDefault();
					if (day == null)
					{
						day = new CalendarDaySundayBased(this);
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

		public override void UpdateMonthCollection()
		{
			if (Schedule.FlightDateStart.HasValue && Schedule.FlightDateEnd.HasValue)
			{
				var months = new List<CalendarMonth>();
				var startDate = new DateTime(Schedule.FlightDateStart.Value.Year, Schedule.FlightDateStart.Value.Month, 1);
				while (startDate <= Schedule.FlightDateEnd.Value)
				{
					CalendarMonth month = Months.Where(x => x.Date.Equals(startDate)).FirstOrDefault();
					if (month == null)
					{
						month = new CalendarMonthSundayBased(this);
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

		public override DateTime[][] GetDaysByWeek(DateTime start, DateTime end)
		{
			var weeks = new List<DateTime[]>();
			var week = new List<DateTime>();
			while (!(start > end))
			{
				week.Add(start);
				if (start.DayOfWeek == DayOfWeek.Saturday)
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

		public override DateRange[] CalculateDateRange(DateTime[] dates)
		{
			var result = new List<DateRange>();
			var selectedDates = new List<DateTime>();
			selectedDates.AddRange(dates);
			selectedDates.Sort();

			DateTime firstSelectedDate = selectedDates.FirstOrDefault();
			DateTime lastSelectedDate = selectedDates.LastOrDefault();
			//Get Sunday nearest to last selected sate
			DateTime firstWeekday = firstSelectedDate;
			while (firstWeekday.DayOfWeek != DayOfWeek.Sunday)
				firstWeekday = firstWeekday.AddDays(-1);
			//Get Saturday nearest to last selected sate
			DateTime lastWeekday = firstSelectedDate;
			while (lastWeekday.DayOfWeek != DayOfWeek.Saturday)
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
			return result.ToArray();
		}
	}

	public class CalendarMondayBased : Calendar
	{
		public CalendarMondayBased(Schedule parent)
			: base(parent) { }

		public override void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Days":
						Days.Clear();
						foreach (XmlNode dayNode in childNode.ChildNodes)
						{
							CalendarDay day = new CalendarDayMondayBased(this);
							day.Deserialize(dayNode);
							Days.Add(day);
						}
						break;
					case "Months":
						Months.Clear();
						foreach (XmlNode monthNode in childNode.ChildNodes)
						{
							CalendarMonth month = new CalendarMonthMondayBased(this);
							month.Deserialize(monthNode);
							Months.Add(month);
						}
						break;
					case "CalendarNotes":
						Notes.Clear();
						foreach (XmlNode noteNode in childNode.ChildNodes)
						{
							var note = new CalendarNote(this);
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

		public override void UpdateDaysCollection()
		{
			if (Schedule.FlightDateStart.HasValue && Schedule.FlightDateEnd.HasValue)
			{
				var days = new List<CalendarDay>();

				var startDate = new DateTime(Schedule.FlightDateStart.Value.Year, Schedule.FlightDateStart.Value.Month, 1);
				while (startDate.DayOfWeek != DayOfWeek.Monday)
					startDate = startDate.AddDays(-1);

				DateTime endDate = new DateTime(Schedule.FlightDateEnd.Value.Month < 12 ? Schedule.FlightDateEnd.Value.Year : (Schedule.FlightDateEnd.Value.Year + 1), (Schedule.FlightDateEnd.Value.Month < 12 ? Schedule.FlightDateEnd.Value.Month + 1 : 1), 1).AddDays(-1);
				while (endDate.DayOfWeek != DayOfWeek.Sunday)
					endDate = endDate.AddDays(1);

				while (startDate <= endDate)
				{
					CalendarDay day = Days.Where(x => x.Date.Equals(startDate)).FirstOrDefault();
					if (day == null)
					{
						day = new CalendarDayMondayBased(this);
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

		public override void UpdateMonthCollection()
		{
			if (Schedule.FlightDateStart.HasValue && Schedule.FlightDateEnd.HasValue)
			{
				var months = new List<CalendarMonth>();
				var startDate = new DateTime(Schedule.FlightDateStart.Value.Year, Schedule.FlightDateStart.Value.Month, 1);
				while (startDate <= Schedule.FlightDateEnd.Value)
				{
					CalendarMonth month = Months.Where(x => x.Date.Equals(startDate)).FirstOrDefault();
					if (month == null)
					{
						month = new CalendarMonthMondayBased(this);
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

		public override DateTime[][] GetDaysByWeek(DateTime start, DateTime end)
		{
			var weeks = new List<DateTime[]>();
			var week = new List<DateTime>();
			while (!(start > end))
			{
				week.Add(start);
				if (start.DayOfWeek == DayOfWeek.Sunday)
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

		public override DateRange[] CalculateDateRange(DateTime[] dates)
		{
			var result = new List<DateRange>();
			var selectedDates = new List<DateTime>();
			selectedDates.AddRange(dates);
			selectedDates.Sort();

			DateTime firstSelectedDate = selectedDates.FirstOrDefault();
			DateTime lastSelectedDate = selectedDates.LastOrDefault();
			//Get Monday nearest to last selected sate
			DateTime firstWeekday = firstSelectedDate;
			while (firstWeekday.DayOfWeek != DayOfWeek.Monday)
				firstWeekday = firstWeekday.AddDays(-1);
			//Get Sunday nearest to last selected sate
			DateTime lastWeekday = firstSelectedDate;
			while (lastWeekday.DayOfWeek != DayOfWeek.Sunday)
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
			return result.ToArray();
		}
	}

	public abstract class CalendarMonth
	{
		protected DateTime _date;

		public CalendarMonth(Calendar parent)
		{
			Parent = parent;
			Days = new List<CalendarDay>();
			OutputData = new CalendarOutputData(this);
		}

		public Calendar Parent { get; private set; }
		public DateTime DaysRangeBegin { get; set; }
		public DateTime DaysRangeEnd { get; set; }
		public List<CalendarDay> Days { get; private set; }
		public CalendarOutputData OutputData { get; private set; }

		public abstract DateTime Date { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<Date>" + _date.ToString() + @"</Date>");
			result.AppendLine(@"<OutputData>" + OutputData.Serialize() + @"</OutputData>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			DateTime tempDate;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Date":
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
			: base(parent) { }

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
			: base(parent) { }

		public override DateTime Date
		{
			get { return _date; }
			set
			{
				_date = value;
				DateTime temp = value;
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
		public CalendarDay(Calendar parent)
		{
			Parent = parent;
			Logo = new ImageSource();
		}

		public Calendar Parent { get; private set; }
		public DateTime Date { get; set; }
		public bool BelongsToSchedules { get; set; }
		public bool HasNotes { get; set; }
		public string Comment1 { get; set; }
		public string Comment2 { get; set; }
		public ImageSource Logo { get; set; }

		public string Summary
		{
			get
			{
				var result = new StringBuilder();

				if (!string.IsNullOrEmpty(Comment1))
					result.AppendLine(Comment1);

				if (!string.IsNullOrEmpty(Comment2))
					result.AppendLine(Comment2);
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

			result.AppendLine(@"<Date>" + Date.ToString() + @"</Date>");
			if (!string.IsNullOrEmpty(Comment1))
				result.AppendLine(@"<Comment1>" + Comment1.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Comment1>");
			if (!string.IsNullOrEmpty(Comment2))
				result.AppendLine(@"<Comment2>" + Comment2.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Comment2>");
			result.AppendLine(@"<Logo>" + Logo.Serialize() + @"</Logo>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			DateTime tempDate;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Date":
						if (DateTime.TryParse(childNode.InnerText, out tempDate))
							Date = tempDate;
						break;
					case "Comment1":
						Comment1 = childNode.InnerText;
						break;
					case "Comment2":
						Comment2 = childNode.InnerText;
						break;
					case "Logo":
						Logo.Deserialize(childNode);
						break;
				}
			}
		}

		public void ClearData()
		{
			Comment1 = null;
			Comment2 = null;
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

	public class CalendarNote
	{
		public CalendarNote(Calendar parent)
		{
			Parent = parent;
			BackgroundColor = Color.LemonChiffon;
			Note = string.Empty;

			Height = 25f;
		}

		public Calendar Parent { get; private set; }
		public DateTime StartDay { get; set; }
		public DateTime FinishDay { get; set; }
		public Color BackgroundColor { get; set; }
		public string Note { get; set; }

		public int Length
		{
			get { return (FinishDay - StartDay).Days; }
		}

		public Color ForeColor
		{
			get
			{
				int d = 0;
				// Counting the perceptive luminance - human eye favors green color... 
				double a = 1 - (0.299 * BackgroundColor.R + 0.587 * BackgroundColor.G + 0.114 * BackgroundColor.B) / 255;

				if (a < 0.5)
					d = 0; // bright colors - black font
				else
					d = 255; // dark colors - white font

				return Color.FromArgb(d, d, d);
			}
		}

		#region Output Data
		public float Top { get; set; }
		public float Left { get; set; }
		public float Right { get; set; }
		public float Height { get; set; }
		#endregion

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<StartDay>" + StartDay.ToString() + @"</StartDay>");
			result.AppendLine(@"<FinishDay>" + FinishDay.ToString() + @"</FinishDay>");
			result.AppendLine(@"<BackgroundColor>" + BackgroundColor.ToArgb().ToString() + @"</BackgroundColor>");
			result.AppendLine(@"<Note>" + Note.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Note>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			DateTime tempDate;
			int tempInt;

			foreach (XmlNode childNode in node.ChildNodes)
			{
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
						if (int.TryParse(childNode.InnerText, out tempInt))
							BackgroundColor = Color.FromArgb(tempInt);
						break;
					case "Note":
						Note = childNode.InnerText;
						break;
				}
			}
		}
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

	public class CalendarOutputData
	{
		private readonly List<string> _dayLogosPaths = new List<string>();

		public CalendarOutputData(CalendarMonth parent)
		{
			Parent = parent;
			Notes = new List<CalendarNote>();

			#region Basic
			ShowHeader = true;
			ShowBusinessName = true;
			ShowDecisionMaker = true;
			Header = string.Empty;
			ApplyForAllBasic = true;
			#endregion

			#region Cost
			ShowPrintTotalCostManual = false;
			ShowDigitalTotalCost = false;
			ApplyForAlCost = true;
			#endregion

			#region Notes
			ShowCustomComment = false;
			ApplyForAllCustomComment = true;

			ShowActiveDays = false;
			ShowPrintAdsNumber = false;
			ShowImpressions = false;
			ShowDigitalCPM = false;
			ApplyForAllOtherNumbers = true;
			#endregion

			#region Style
			SlideColor = "gray";
			ApplyForAllThemeColor = true;

			ShowLogo = true;
			string defaultLogoPath = Path.Combine(ListManager.Instance.BigImageFolder.FullName, Common.ListManager.DefaultBigLogoFileName);
			if (File.Exists(defaultLogoPath))
				Logo = new Bitmap(defaultLogoPath);
			ApplyForAllLogo = true;

			ShowBigDate = true;
			#endregion
		}

		public List<CalendarNote> Notes { get; private set; }

		public CalendarMonth Parent { get; private set; }

		#region Basic
		private string _businessName = string.Empty;
		private string _decisionMaker = string.Empty;
		public bool ShowHeader { get; set; }
		public bool ShowBusinessName { get; set; }
		public bool ShowDecisionMaker { get; set; }
		public string Header { get; set; }
		public bool ApplyForAllBasic { get; set; }
		#endregion

		#region Cost
		public bool ShowPrintTotalCostManual { get; set; }
		public bool ShowDigitalTotalCost { get; set; }
		public double? PrintTotalCost { get; set; }
		public double? DigitalTotalCost { get; set; }
		public bool ApplyForAlCost { get; set; }
		#endregion

		#region Notes
		private int? _activeDays;
		private int? _printAdsNumber;
		public bool ShowCustomComment { get; set; }
		public string CustomComment { get; set; }
		public bool ApplyForAllCustomComment { get; set; }

		public bool ShowActiveDays { get; set; }
		public bool ShowPrintAdsNumber { get; set; }
		public bool ShowImpressions { get; set; }
		public bool ShowDigitalCPM { get; set; }
		public double? Impressions { get; set; }
		public double? DigitalCPM { get; set; }
		public bool ApplyForAllOtherNumbers { get; set; }
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
		public string BusinessName
		{
			get
			{
				if (ShowBusinessName)
				{
					if (!string.IsNullOrEmpty(_businessName))
						return _businessName;
					return Parent.Parent.Schedule.BusinessName;
				}
				return string.Empty;
			}
		}

		public string DecisionMaker
		{
			get
			{
				if (ShowDecisionMaker)
				{
					if (!string.IsNullOrEmpty(_decisionMaker))
						return _decisionMaker;
					return Parent.Parent.Schedule.DecisionMaker;
				}
				return string.Empty;
			}
		}

		public int CalculatedActiveDays
		{
			get { return Parent.Days.Where(x => x.ContainsData || x.HasNotes).Count(); }
		}

		public int ActiveDays
		{
			get
			{
				if (!_activeDays.HasValue)
					return CalculatedActiveDays;
				else
					return _activeDays.Value;
			}
			set
			{
				if (CalculatedActiveDays != value)
					_activeDays = value;
				else
					_activeDays = null;
			}
		}

		public int PrintAdsNumber
		{
			get { return _printAdsNumber.HasValue ? _printAdsNumber.Value : 0; }
			set { _printAdsNumber = value; }
		}

		private string _encodedLogo;
		public string EncodedLogo
		{
			get
			{
				if (String.IsNullOrEmpty(_encodedLogo))
				{
					TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
					_encodedLogo = Convert.ToBase64String((byte[])converter.ConvertTo(Logo, typeof(byte[])));
				}
				return _encodedLogo;
			}
			set { _encodedLogo = value; }
		}

		public Color SlideColorLight
		{
			get
			{
				switch (SlideColor)
				{
					case "black":
						return Color.White;
					case "blue":
						return Color.LightBlue;
					case "gray":
						return Color.LightGray;
					case "green":
						return Color.LightGreen;
					case "orange":
						return Color.FromArgb(255, 224, 192);
					case "teal":
						return Color.Cyan;
					default:
						return Color.White;
				}
			}
		}

		public Color SlideColorDark
		{
			get
			{
				switch (SlideColor)
				{
					case "black":
						return Color.Black;
					case "blue":
						return Color.Blue;
					case "gray":
						return Color.Gray;
					case "green":
						return Color.Green;
					case "orange":
						return Color.Orange;
					case "teal":
						return Color.Teal;
					default:
						return Color.Black;
				}
			}
		}

		#region Slide Output Properties
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
				if (ShowLogo && Logo != null)
				{
					result = Path.GetTempFileName();
					Logo.Save(result);
				}
				return result;
			}
		}

		public string SlideTitle
		{
			get { return ShowHeader ? Header : string.Empty; }
		}

		public string MonthText
		{
			get
			{
				return Parent.Date.ToString("MMMM yyyy");
			}
		}

		public string BackgroundFileName
		{
			get { return String.Format("{0}{1}.png", Parent.Date.ToString("MMM").ToLower(), (ShowBigDate ? "1" : "2")); }
		}

		public string Comments
		{
			get
			{
				string result = string.Empty;
				if (ShowCustomComment)
					result = CustomComment;
				return result;
			}
		}

		public string TagA
		{
			get
			{
				string result = string.Empty;
				string[] tagValues = GetTotalTags();
				if (tagValues.Length > 0)
					result = tagValues[0];
				return result;
			}
		}

		public string TagB
		{
			get
			{
				string result = string.Empty;
				string[] tagValues = GetTotalTags();
				if (tagValues.Length > 1)
					result = tagValues[1];
				return result;
			}
		}

		public string TagC
		{
			get
			{
				string result = string.Empty;
				string[] tagValues = GetTotalTags();
				if (tagValues.Length > 2)
					result = tagValues[2];
				return result;
			}
		}

		public string TagD
		{
			get
			{
				string result = string.Empty;
				string[] tagValues = GetTotalTags();
				if (tagValues.Length > 3)
					result = tagValues[3];
				return result;
			}
		}

		public string[] DayOutput
		{
			get { return Parent.Days.Select(x => x.Summary).ToArray(); }
		}

		public string[] DayLogoPaths
		{
			get { return _dayLogosPaths.ToArray(); }
		}

		public float FontSize
		{
			get { return 7; }
		}
		#endregion

		#endregion

		public string Serialize()
		{
			var result = new StringBuilder();
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));

			#region Basic
			result.AppendLine(@"<ShowHeader>" + ShowHeader.ToString() + @"</ShowHeader>");
			result.AppendLine(@"<ShowBusinessName>" + ShowBusinessName.ToString() + @"</ShowBusinessName>");
			result.AppendLine(@"<ShowDecisionMaker>" + ShowDecisionMaker.ToString() + @"</ShowDecisionMaker>");
			result.AppendLine(@"<Header>" + Header.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Header>");
			result.AppendLine(@"<BusinessName>" + _businessName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</BusinessName>");
			result.AppendLine(@"<DecisionMaker>" + _decisionMaker.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DecisionMaker>");
			result.AppendLine(@"<ApplyForAllBasic>" + ApplyForAllBasic.ToString() + @"</ApplyForAllBasic>");
			#endregion

			#region Cost
			if (PrintTotalCost.HasValue)
				result.AppendLine(@"<PrintTotalCost>" + PrintTotalCost.Value.ToString() + @"</PrintTotalCost>");
			result.AppendLine(@"<ShowPrintTotalCostManual>" + ShowPrintTotalCostManual.ToString() + @"</ShowPrintTotalCostManual>");
			result.AppendLine(@"<ShowDigitalTotalCost>" + ShowDigitalTotalCost.ToString() + @"</ShowDigitalTotalCost>");
			if (DigitalTotalCost.HasValue)
				result.AppendLine(@"<DigitalTotalCost>" + DigitalTotalCost.Value.ToString() + @"</DigitalTotalCost>");
			result.AppendLine(@"<ApplyForAllCost>" + ApplyForAlCost.ToString() + @"</ApplyForAllCost>");
			#endregion

			#region Notes
			result.AppendLine(@"<ShowCustomComment>" + ShowCustomComment.ToString() + @"</ShowCustomComment>");
			if (!string.IsNullOrEmpty(CustomComment))
				result.AppendLine(@"<CustomComment>" + CustomComment.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</CustomComment>");
			result.AppendLine(@"<ApplyForAllCustomComment>" + ApplyForAllCustomComment.ToString() + @"</ApplyForAllCustomComment>");

			if (_activeDays.HasValue)
				result.AppendLine(@"<ActiveDays>" + _activeDays.Value.ToString() + @"</ActiveDays>");
			if (_printAdsNumber.HasValue)
				result.AppendLine(@"<PrintAdsNumber>" + _printAdsNumber.Value.ToString() + @"</PrintAdsNumber>");
			result.AppendLine(@"<ShowActiveDays>" + ShowActiveDays.ToString() + @"</ShowActiveDays>");
			result.AppendLine(@"<ShowPrintAdsNumber>" + ShowPrintAdsNumber.ToString() + @"</ShowPrintAdsNumber>");
			result.AppendLine(@"<ShowImpressions>" + ShowImpressions.ToString() + @"</ShowImpressions>");
			result.AppendLine(@"<ShowDigitalCPM>" + ShowDigitalCPM.ToString() + @"</ShowDigitalCPM>");
			result.AppendLine(@"<ApplyForAllOtherNumbers>" + ApplyForAllOtherNumbers.ToString() + @"</ApplyForAllOtherNumbers>");
			if (Impressions.HasValue)
				result.AppendLine(@"<Impressions>" + Impressions.Value.ToString() + @"</Impressions>");
			if (DigitalCPM.HasValue)
				result.AppendLine(@"<DigitalCPM>" + DigitalCPM.Value.ToString() + @"</DigitalCPM>");
			#endregion

			#region Style
			result.AppendLine(@"<SlideColor>" + SlideColor.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideColor>");
			result.AppendLine(@"<ApplyForAllThemeColor>" + ApplyForAllThemeColor.ToString() + @"</ApplyForAllThemeColor>");

			result.AppendLine(@"<ShowLogo>" + ShowLogo.ToString() + @"</ShowLogo>");
			if (!String.IsNullOrEmpty(EncodedLogo))
				result.AppendLine(@"<Logo>" + EncodedLogo.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Logo>");
			result.AppendLine(@"<ApplyForAllLogo>" + ApplyForAllLogo.ToString() + @"</ApplyForAllLogo>");

			result.AppendLine(@"<ShowBigDate>" + ShowBigDate + @"</ShowBigDate>");
			#endregion

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool = false;
			double tempDouble;
			int tempInt;

			foreach (XmlNode childNode in node.ChildNodes)
			{
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
					case "BusinessName":
						_businessName = childNode.InnerText;
						break;
					case "DecisionMaker":
						_decisionMaker = childNode.InnerText;
						break;
					case "ApplyForAllBasic":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ApplyForAllBasic = tempBool;
						break;
					#endregion

					#region Cost
					case "PrintTotalCost":
						if (double.TryParse(childNode.InnerText, out tempDouble))
							PrintTotalCost = tempDouble;
						break;
					case "ShowPrintTotalCostManual":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPrintTotalCostManual = tempBool;
						break;
					case "ShowDigitalTotalCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDigitalTotalCost = tempBool;
						break;
					case "DigitalTotalCost":
						if (double.TryParse(childNode.InnerText, out tempDouble))
							DigitalTotalCost = tempDouble;
						break;
					case "ApplyForAllCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ApplyForAlCost = tempBool;
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

					case "ActiveDays":
						if (int.TryParse(childNode.InnerText, out tempInt))
							_activeDays = tempInt;
						break;
					case "PrintAdsNumber":
						if (int.TryParse(childNode.InnerText, out tempInt))
							_printAdsNumber = tempInt;
						break;
					case "ShowActiveDays":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowActiveDays = tempBool;
						break;
					case "ShowPrintAdsNumber":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPrintAdsNumber = tempBool;
						break;
					case "ShowImpressions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowImpressions = tempBool;
						break;
					case "ShowDigitalCPM":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDigitalCPM = tempBool;
						break;
					case "Impressions":
						if (double.TryParse(childNode.InnerText, out tempDouble))
							Impressions = tempDouble;
						break;
					case "DigitalCPM":
						if (double.TryParse(childNode.InnerText, out tempDouble))
							DigitalCPM = tempDouble;
						break;
					case "ApplyForAllOtherNumbers":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ApplyForAllOtherNumbers = tempBool;
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

		private string[] GetTotalTags()
		{
			var tagValues = new List<string>();
			if (ShowPrintTotalCostManual)
				tagValues.Add("Newspaper Investment: " + ((PrintTotalCost.HasValue ? PrintTotalCost.Value.ToString("$#,###.00") : string.Empty)));
			if (ShowDigitalTotalCost && DigitalTotalCost.HasValue)
				tagValues.Add("Digital Investment: " + DigitalTotalCost.Value.ToString("$#,###.00"));
			if (ShowActiveDays)
				tagValues.Add("# of Active Days: " + ActiveDays.ToString("#,##0"));
			if (ShowPrintAdsNumber)
				tagValues.Add("# of Newspaper Ads: " + PrintAdsNumber.ToString("#,##0"));
			if (ShowImpressions && Impressions.HasValue)
				tagValues.Add("Monthly Imressions: " + Impressions.Value.ToString("#,##0.0"));
			if (ShowDigitalCPM && DigitalCPM.HasValue)
				tagValues.Add("Digital CPM: " + DigitalCPM.Value.ToString("$#,###.0"));
			return tagValues.ToArray();
		}

		public void PrepareDayLogoPaths()
		{
			_dayLogosPaths.Clear();
			foreach (CalendarDay day in Parent.Days)
			{
				if (day.Logo.TinyImage != null)
				{
					string filePath = string.Empty;
					filePath = Path.GetTempFileName();
					day.Logo.TinyImage.Save(filePath);
					_dayLogosPaths.Add(filePath);
				}
				else
					_dayLogosPaths.Add(string.Empty);
			}
		}

		public void PrepareNotes()
		{
			Notes.Clear();
			Notes.AddRange(Parent.Parent.Notes.Where(x => x.StartDay >= Parent.Date && x.FinishDay < Parent.Date.AddMonths(1)));
		}
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
			result.AppendLine(@"<Visible>" + Visible.ToString() + @"</Visible>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool = false;

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