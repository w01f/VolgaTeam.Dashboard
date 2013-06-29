using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using AdScheduleBuilder.BusinessClasses;
using CalendarBuilder.ConfigurationClasses;
using CalendarBuilder.ToolForms;
using Microsoft.VisualBasic;

namespace CalendarBuilder.BusinessClasses
{
	public enum CalendarStyle
	{
		Simple = 0,
		Graphic,
		Advanced
	}

	public enum SalesStrategy
	{
		InPerson = 0,
		Email,
		Fax
	}

	public enum ColorOption
	{
		BlackWhite = 0,
		SpotColor,
		FullColor
	}

	public enum DayDataType
	{
		All = 0,
		Digital,
		Newspaper,
		Comment,
		Logo
	}


	public class ScheduleManager
	{
		private static readonly ScheduleManager _instance = new ScheduleManager();
		private Schedule _currentSchedule;

		private ScheduleManager() { }

		public static ScheduleManager Instance
		{
			get { return _instance; }
		}
		public event EventHandler<SavingingEventArgs> SettingsSaved;
		public bool CalendarLoaded { get; set; }

		public void CreateSchedule(string scheduleName)
		{
			string calendarFilePath = GetScheduleFileName(scheduleName);
			if (File.Exists(calendarFilePath))
				if (AppManager.ShowWarningQuestion(string.Format("An older Calendar is already saved with this same file name.\nDo you want to replace this file with a newer calendar?", scheduleName)) == DialogResult.Yes)
					File.Delete(calendarFilePath);
			OpenSchedule(calendarFilePath);
		}

		public void ImportSchedule(string sourceSchedulePath, bool buildAdvanced, bool buildGraphic, bool buildSimple)
		{
			var sourceSchedule = new AdScheduleBuilder.BusinessClasses.Schedule(sourceSchedulePath);
			var scheduleName = Path.GetFileNameWithoutExtension(sourceSchedule.ScheduleFile.FullName);
			var calendarFilePath = GetScheduleFileName(scheduleName);
			if (File.Exists(calendarFilePath))
				if (AppManager.ShowWarningQuestion(string.Format("An older Calendar is already saved with this same file name: {0}.\nDo you want to replace this file with a newer calendar?", scheduleName)) == DialogResult.Yes)
					File.Delete(calendarFilePath);
			OpenSchedule(calendarFilePath);
			_currentSchedule.ImportCalendars(sourceSchedule, buildAdvanced, buildGraphic, buildSimple);
			_currentSchedule.Save();
		}

		public void OpenSchedule(string scheduleFilePath)
		{
			_currentSchedule = new Schedule(scheduleFilePath);
			CalendarLoaded = true;
		}

		public string GetScheduleFileName(string calendarName)
		{
			return Path.Combine(SettingsManager.Instance.SaveFolder, calendarName + ".xml");
		}

		public Schedule GetLocalSchedule()
		{
			return new Schedule(_currentSchedule.CalendarFile.FullName);
		}

		public void SaveSchedule(Schedule localCalendar, bool quickSave, Control sender)
		{
			localCalendar.Save();
			_currentSchedule = localCalendar;
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Chill-Out for a few seconds...\nSaving settings...";
				form.TopMost = true;
				var thread = new Thread(delegate()
											{
												FormMain.Instance.Invoke((MethodInvoker)delegate()
																							{
																								if (SettingsSaved != null)
																									SettingsSaved(sender, new SavingingEventArgs(quickSave));
																							});
											});

				form.Show();
				Application.DoEvents();

				thread.Start();

				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();
			}
		}

		public ShortSchedule[] GetShortScheduleList(DirectoryInfo rootFolder, bool originFormat = true)
		{
			var calendarList = new List<ShortSchedule>();
			foreach (FileInfo file in rootFolder.GetFiles("*.xml"))
			{
				var schedule = new ShortSchedule(file);
				schedule.NeedToImport = !originFormat;
				if (!string.IsNullOrEmpty(schedule.BusinessName))
					calendarList.Add(schedule);
			}
			return calendarList.ToArray();
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
		public bool NeedToImport { get; set; }

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

		public string Type
		{
			get { return NeedToImport ? "ads" : "cal"; }
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
			Status = ListManager.Instance.Statuses.FirstOrDefault();
			SundayBased = true;
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
		public string Status { get; set; }
		public SalesStrategy SalesStrategy { get; set; }
		public DateTime? PresentationDate { get; set; }
		public bool SundayBased { get; set; }
		public DateTime? FlightDateStart { get; set; }
		public DateTime? FlightDateEnd { get; set; }

		public Calendar AdvancedCalendar { get; private set; }
		public Calendar GraphicCalendar { get; private set; }
		public Calendar SimpleCalendar { get; private set; }

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

				node = document.SelectSingleNode(@"/Schedule/Status");
				if (node != null)
					Status = node.InnerText;

				node = document.SelectSingleNode(@"/Schedule/SalesStrategy");
				if (node != null)
					if (int.TryParse(node.InnerText, out tempInt))
						SalesStrategy = (SalesStrategy)tempInt;

				node = document.SelectSingleNode(@"/Schedule/SundayBased");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						SundayBased = tempBool;

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
			if (SundayBased)
			{
				AdvancedCalendar = new CalendarSundayBased(this);
				GraphicCalendar = new CalendarSundayBased(this);
				SimpleCalendar = new CalendarSundayBased(this);
			}
			else
			{
				AdvancedCalendar = new CalendarMondayBased(this);
				GraphicCalendar = new CalendarMondayBased(this);
				SimpleCalendar = new CalendarMondayBased(this);
			}

			if (_calendarFile.Exists)
			{
				XmlNode node;
				var document = new XmlDocument();
				document.Load(_calendarFile.FullName);

				node = document.SelectSingleNode(@"/Schedule/AdvancedCalendar");
				if (node != null)
				{
					AdvancedCalendar.Deserialize(node);
				}
				else
				{
					AdvancedCalendar.UpdateDaysCollection();
					AdvancedCalendar.UpdateMonthCollection();
					AdvancedCalendar.UpdateNotesCollection();
				}

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

				node = document.SelectSingleNode(@"/Schedule/SimpleCalendar");
				if (node != null)
				{
					SimpleCalendar.Deserialize(node);
				}
				else
				{
					SimpleCalendar.UpdateDaysCollection();
					SimpleCalendar.UpdateMonthCollection();
					SimpleCalendar.UpdateNotesCollection();
				}
			}
		}

		public void Save()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
			var xml = new StringBuilder();

			xml.AppendLine(@"<Schedule>");
			xml.AppendLine(@"<BusinessName>" + BusinessName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</BusinessName>");
			if (!ListManager.Instance.Advertisers.Contains(BusinessName))
			{
				ListManager.Instance.Advertisers.Add(BusinessName);
				ListManager.Instance.SaveAdvertisers();
			}
			xml.AppendLine(@"<DecisionMaker>" + DecisionMaker.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DecisionMaker>");
			if (!ListManager.Instance.DecisionMakers.Contains(DecisionMaker))
			{
				ListManager.Instance.DecisionMakers.Add(DecisionMaker);
				ListManager.Instance.SaveDecisionMakers();
			}
			xml.AppendLine(@"<Status>" + (Status != null ? Status.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + @"</Status>");
			xml.AppendLine(@"<ClientType>" + ClientType.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</ClientType>");
			xml.AppendLine(@"<SalesStrategy>" + (int)SalesStrategy + @"</SalesStrategy>");
			xml.AppendLine(@"<SundayBased>" + SundayBased + @"</SundayBased>");
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

			xml.AppendLine(@"<AdvancedCalendar>" + AdvancedCalendar.Serialize() + @"</AdvancedCalendar>");
			xml.AppendLine(@"<GraphicCalendar>" + GraphicCalendar.Serialize() + @"</GraphicCalendar>");
			xml.AppendLine(@"<SimpleCalendar>" + SimpleCalendar.Serialize() + @"</SimpleCalendar>");

			xml.AppendLine(@"</Schedule>");

			using (var sw = new StreamWriter(_calendarFile.FullName, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}

		public void ImportCalendars(AdScheduleBuilder.BusinessClasses.Schedule sourceSchedule, bool buildAdvanced, bool buildGraphic, bool buildSimple)
		{
			BusinessName = sourceSchedule.BusinessName;
			DecisionMaker = sourceSchedule.DecisionMaker;
			ClientType = sourceSchedule.ClientType;
			PresentationDate = sourceSchedule.PresentationDate;
			FlightDateStart = sourceSchedule.FlightDateStart;
			FlightDateEnd = sourceSchedule.FlightDateEnd;
			Status = ListManager.Instance.Statuses.FirstOrDefault();
			SundayBased = true;
			ShowNewspaper = true;
			ShowDigital = true;
			ShowTV = false;
			ShowRadio = false;

			if (SundayBased)
			{
				AdvancedCalendar = new CalendarSundayBased(this);
				GraphicCalendar = new CalendarSundayBased(this);
				SimpleCalendar = new CalendarSundayBased(this);
			}
			else
			{
				AdvancedCalendar = new CalendarMondayBased(this);
				GraphicCalendar = new CalendarMondayBased(this);
				SimpleCalendar = new CalendarMondayBased(this);
			}

			AdvancedCalendar.UpdateDaysCollection();
			AdvancedCalendar.UpdateMonthCollection();
			AdvancedCalendar.UpdateNotesCollection();
			if (buildAdvanced)
				AdvancedCalendar.ImportDays(sourceSchedule, true);

			GraphicCalendar.UpdateDaysCollection();
			GraphicCalendar.UpdateMonthCollection();
			GraphicCalendar.UpdateNotesCollection();
			if (buildGraphic)
				GraphicCalendar.ImportDays(sourceSchedule, false);

			SimpleCalendar.UpdateDaysCollection();
			SimpleCalendar.UpdateMonthCollection();
			SimpleCalendar.UpdateNotesCollection();
			if (buildSimple)
				SimpleCalendar.ImportDays(sourceSchedule, false);
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

		public void ImportDays(AdScheduleBuilder.BusinessClasses.Schedule sourceSchedule, bool useAdvancedStyle)
		{
			foreach (CalendarDay day in Days)
			{
				var customNoteConstructor = new StringBuilder();
				foreach (Publication publication in sourceSchedule.Publications)
					foreach (Insert insert in publication.Inserts.Where(x => x.Date.Year == day.Date.Year && x.Date.Month == day.Date.Month && x.Date.Day == day.Date.Day))
					{
						var properties = new List<string>();
						if (!string.IsNullOrEmpty(insert.Publication))
							properties.Add(insert.Publication);
						if (!string.IsNullOrEmpty(insert.FullSection))
							properties.Add(insert.FullSection);
						if (!string.IsNullOrEmpty(insert.DimensionsShort))
							properties.Add(insert.DimensionsShort);
						if (!string.IsNullOrEmpty(insert.PageSize))
							properties.Add(insert.PageSize);
						if (!string.IsNullOrEmpty(insert.PercentOfPage))
							properties.Add(insert.PercentOfPage);
						if (!string.IsNullOrEmpty(insert.PublicationColor))
							properties.Add(insert.PublicationColor);
						properties.Add(insert.FinalRate.ToString("$#,##0"));
						customNoteConstructor.AppendLine(string.Join(", ", properties.ToArray()));
					}
				if (useAdvancedStyle)
					day.Newspaper.CustomNote = customNoteConstructor.ToString();
				else
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
						Months.Clear();
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
					month.OutputData.UpdateLegend();
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
						Months.Clear();
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
					month.OutputData.UpdateLegend();
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
							_date = tempDate;
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
			Digital = new DigitalProperties(this);
			Newspaper = new NewspaperProperties(this);
			Logo = new ImageSource(this);
		}

		public Calendar Parent { get; private set; }
		public DateTime Date { get; set; }
		public bool BelongsToSchedules { get; set; }
		public bool HasNotes { get; set; }
		public DigitalProperties Digital { get; set; }
		public NewspaperProperties Newspaper { get; set; }
		public string Comment1 { get; set; }
		public string Comment2 { get; set; }
		public ImageSource Logo { get; set; }

		public string Summary
		{
			get
			{
				var result = new StringBuilder();

				string temp = Digital.Summary;
				if (!string.IsNullOrEmpty(temp))
					result.AppendLine(temp);

				temp = Newspaper.Summary;
				if (!string.IsNullOrEmpty(temp))
					result.AppendLine(temp);

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
			result.AppendLine(@"<Digital>" + Digital.Serialize() + @"</Digital>");
			result.AppendLine(@"<Newspaper>" + Newspaper.Serialize() + @"</Newspaper>");
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
					case "Digital":
						Digital.Deserialize(childNode);
						break;
					case "Newspaper":
						Newspaper.Deserialize(childNode);
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

		public void ClearData(DayDataType dataToClear = DayDataType.All)
		{
			switch (dataToClear)
			{
				case DayDataType.Digital:
					Digital = new DigitalProperties(this);
					break;
				case DayDataType.Newspaper:
					Newspaper = new NewspaperProperties(this);
					break;
				case DayDataType.Comment:
					Comment1 = null;
					Comment2 = null;
					break;
				case DayDataType.Logo:
					Logo = new ImageSource(this);
					break;
				case DayDataType.All:
					Comment1 = null;
					Comment2 = null;
					Logo = new ImageSource(this);
					Digital = new DigitalProperties(this);
					Newspaper = new NewspaperProperties(this);
					break;
			}
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

	public class DigitalProperties
	{
		private readonly CalendarDay _parent;

		public DigitalProperties(CalendarDay parent)
		{
			_parent = parent;
			ShowCategory = true;
			ShowSubCategory = true;
			ShowProduct = true;
		}

		public string Category { get; set; }
		public string SubCategory { get; set; }
		public string ProductName { get; set; }
		public string CustomNote { get; set; }
		public string QuickListRecord { get; set; }

		public bool ShowCategory { get; set; }
		public bool ShowSubCategory { get; set; }
		public bool ShowProduct { get; set; }

		public DateTime Day
		{
			get { return _parent.Date; }
		}

		public string Summary
		{
			get
			{
				var result = new List<string>();
				if (!string.IsNullOrEmpty(CustomNote))
					result.Add(CustomNote);
				if (!string.IsNullOrEmpty(QuickListRecord))
					result.Add(QuickListRecord);
				if (!string.IsNullOrEmpty(Category) && ShowCategory)
					result.Add(Category);
				if (!string.IsNullOrEmpty(SubCategory) && ShowSubCategory)
					result.Add(SubCategory);
				if (!string.IsNullOrEmpty(ProductName) && ShowProduct)
					result.Add(ProductName);
				return string.Join(", ", result.ToArray());
			}
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			if (!string.IsNullOrEmpty(Category))
				result.AppendLine(@"<Category>" + Category.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Category>");
			if (!string.IsNullOrEmpty(SubCategory))
				result.AppendLine(@"<SubCategory>" + SubCategory.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SubCategory>");
			if (!string.IsNullOrEmpty(ProductName))
				result.AppendLine(@"<ProductName>" + ProductName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</ProductName>");
			if (!string.IsNullOrEmpty(CustomNote))
				result.AppendLine(@"<CustomNote>" + CustomNote.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</CustomNote>");
			if (!string.IsNullOrEmpty(QuickListRecord))
				result.AppendLine(@"<QuickListRecord>" + QuickListRecord.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</QuickListRecord>");

			result.AppendLine(@"<ShowCategory>" + ShowCategory.ToString() + @"</ShowCategory>");
			result.AppendLine(@"<ShowSubCategory>" + ShowSubCategory.ToString() + @"</ShowSubCategory>");
			result.AppendLine(@"<ShowProduct>" + ShowProduct.ToString() + @"</ShowProduct>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Category":
						Category = childNode.InnerText;
						break;
					case "SubCategory":
						SubCategory = childNode.InnerText;
						break;
					case "ProductName":
						ProductName = childNode.InnerText;
						break;
					case "CustomNote":
						CustomNote = childNode.InnerText;
						break;
					case "QuickListRecord":
						QuickListRecord = childNode.InnerText;
						break;
					case "ShowCategory":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowCategory = tempBool;
						break;
					case "ShowSubCategory":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSubCategory = tempBool;
						break;
					case "ShowProduct":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowProduct = tempBool;
						break;
				}
			}
		}

		public DigitalProperties Clone(CalendarDay newParent)
		{
			DigitalProperties result = null;
			var document = new XmlDocument();
			document.LoadXml(@"<Digital>" + Serialize() + @"</Digital>");
			result = new DigitalProperties(newParent);
			result.Deserialize(document.FirstChild);
			return result;
		}
	}

	public class NewspaperProperties
	{
		private readonly CalendarDay _parent;

		public NewspaperProperties(CalendarDay parent)
		{
			_parent = parent;
		}

		public string PublicationName { get; set; }
		public string Section { get; set; }
		public string PageSize { get; set; }
		public string Color { get; set; }
		public double? TotalCost { get; set; }
		public string CustomNote { get; set; }
		public string QuickListRecord { get; set; }

		public DateTime Day
		{
			get { return _parent.Date; }
		}

		public string SectionAbbreviation
		{
			get
			{
				PrintSection section = ListManager.Instance.PrintSections.Where(x => x.Name.Equals(Section)).FirstOrDefault();
				if (section != null)
					return section.Abbreviation;
				else if (!string.IsNullOrEmpty(Section))
					return Section.Substring(0, 2);
				else
					return string.Empty;
			}
		}

		public string PublicationAbbreviation
		{
			get
			{
				PrintSource printSource = ListManager.Instance.PrintSources.Where(x => x.Name.Equals(PublicationName)).FirstOrDefault();
				if (printSource != null)
					return printSource.Abbreviation;
				else if (!string.IsNullOrEmpty(PublicationName))
					return PublicationName.Substring(0, 3).ToUpper();
				else
					return string.Empty;
			}
		}

		public string Summary
		{
			get
			{
				var result = new List<string>();
				if (!string.IsNullOrEmpty(CustomNote))
					result.Add(CustomNote);
				if (!string.IsNullOrEmpty(QuickListRecord))
					result.Add(QuickListRecord);
				if (!string.IsNullOrEmpty(PublicationAbbreviation))
					result.Add(PublicationAbbreviation);
				if (!string.IsNullOrEmpty(SectionAbbreviation))
					result.Add(SectionAbbreviation);
				if (!string.IsNullOrEmpty(PageSize))
					result.Add(PageSize);
				if (!string.IsNullOrEmpty(Color))
					result.Add(Color);
				if (TotalCost.HasValue)
					result.Add(TotalCost.Value.ToString("$#,##0"));
				return string.Join(", ", result.ToArray());
			}
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			if (!string.IsNullOrEmpty(PublicationName))
				result.AppendLine(@"<PublicationName>" + PublicationName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</PublicationName>");
			if (!string.IsNullOrEmpty(Section))
				result.AppendLine(@"<Section>" + Section.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Section>");
			if (!string.IsNullOrEmpty(PageSize))
				result.AppendLine(@"<PageSize>" + PageSize.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</PageSize>");
			if (!string.IsNullOrEmpty(Color))
				result.AppendLine(@"<Color>" + Color.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Color>");
			if (TotalCost.HasValue)
				result.AppendLine(@"<TotalCost>" + TotalCost.Value.ToString() + @"</TotalCost>");
			if (!string.IsNullOrEmpty(CustomNote))
				result.AppendLine(@"<CustomNote>" + CustomNote.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</CustomNote>");
			if (!string.IsNullOrEmpty(QuickListRecord))
				result.AppendLine(@"<QuickListRecord>" + QuickListRecord.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</QuickListRecord>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			double tempDouble;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "PublicationName":
						PublicationName = childNode.InnerText;
						break;
					case "Section":
						Section = childNode.InnerText;
						break;
					case "PageSize":
						PageSize = childNode.InnerText;
						break;
					case "Color":
						Color = childNode.InnerText;
						break;
					case "TotalCost":
						TotalCost = null;
						if (double.TryParse(childNode.InnerText, out tempDouble))
							TotalCost = tempDouble;
						break;
					case "CustomNote":
						CustomNote = childNode.InnerText;
						break;
					case "QuickListRecord":
						QuickListRecord = childNode.InnerText;
						break;
				}
			}
		}

		public NewspaperProperties Clone(CalendarDay newParent)
		{
			NewspaperProperties result = null;
			var document = new XmlDocument();
			document.LoadXml(@"<Newspaper>" + Serialize() + @"</Newspaper>");
			result = new NewspaperProperties(newParent);
			result.Deserialize(document.FirstChild);
			return result;
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
			ShowMonth = true;
			ShowHeader = true;
			ShowBusinessName = true;
			ShowDecisionMaker = true;
			Header = string.Empty;
			ApplyForAllBasic = true;
			#endregion

			#region Cost
			ShowPrintTotalCostManual = false;
			ShowPrintTotalCostCalculated = false;
			ShowDigitalTotalCost = false;
			ShowTVTotalCost = false;
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

			#region Legend
			ShowLegend = false;
			Legend = new List<CalendarLegend>();
			ApplyForAllLegend = true;
			#endregion

			#region Style
			SlideColor = "gray";
			ApplyForAllThemeColor = true;

			ShowLogo = true;
			string defaultLogoPath = Path.Combine(SettingsManager.Instance.BigImageFolder.FullName, SettingsManager.DefaultBigLogoFileName);
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
		public bool ShowMonth { get; set; }
		public bool ShowHeader { get; set; }
		public bool ShowBusinessName { get; set; }
		public bool ShowDecisionMaker { get; set; }
		public string Header { get; set; }
		public bool ApplyForAllBasic { get; set; }
		#endregion

		#region Cost
		public bool ShowPrintTotalCostManual { get; set; }
		public bool ShowPrintTotalCostCalculated { get; set; }
		public bool ShowDigitalTotalCost { get; set; }
		public bool ShowTVTotalCost { get; set; }
		public double? PrintTotalCost { get; set; }
		public double? DigitalTotalCost { get; set; }
		public double? TVTotalCost { get; set; }
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

		#region Legend
		public bool ShowLegend { get; set; }
		public List<CalendarLegend> Legend { get; private set; }
		public bool ApplyForAllLegend { get; set; }
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
					else
						return Parent.Parent.Schedule.BusinessName;
				}
				else
					return string.Empty;
			}
			set
			{
				if (!value.Equals(Parent.Parent.Schedule.BusinessName))
					_businessName = value;
				else
					_businessName = string.Empty;
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
					else
						return Parent.Parent.Schedule.DecisionMaker;
				}
				else
					return string.Empty;
			}
			set
			{
				if (!value.Equals(Parent.Parent.Schedule.DecisionMaker))
					_decisionMaker = value;
				else
					_decisionMaker = string.Empty;
			}
		}

		public double PrintTotalCostCalculated
		{
			get { return Parent.Days.Select(x => x.Newspaper.TotalCost.HasValue ? x.Newspaper.TotalCost.Value : 0).Sum(); }
		}

		public int CalculatedActiveDays
		{
			get { return Parent.Days.Where(x => x.ContainsData).Count(); }
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

		public int CalculatedPrintAdsNumber
		{
			get { return Parent.Days.Where(x => !string.IsNullOrEmpty(x.Newspaper.Summary)).Count(); }
		}

		public int PrintAdsNumber
		{
			get
			{
				if (!_printAdsNumber.HasValue)
					return CalculatedPrintAdsNumber;
				else
					return _printAdsNumber.Value;
			}
			set
			{
				if (CalculatedPrintAdsNumber != value)
					_printAdsNumber = value;
				else
					_printAdsNumber = null;
			}
		}

		#region Slide Output Properties
		public string SlideName
		{
			get
			{
				string result = string.Empty;
				CalendarTemplate template = OutputManager.Instance.CalendarTemplates.Where(x => x.IsLarge == ShowBigDate && x.HasLogo == ShowLogo && x.Color.ToLower().Equals(SlideColor) && x.Month.ToLower().Equals(Parent.Date.ToString("MMM-yy").ToLower())).FirstOrDefault();
				if (template != null)
					result = template.TemplateName;
				return result;
			}
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

		public string SlideMasterName
		{
			get
			{
				string result = string.Empty;
				CalendarTemplate template = OutputManager.Instance.CalendarTemplates.Where(x => x.IsLarge == ShowBigDate && x.HasLogo == ShowLogo && x.Color.ToLower().Equals(SlideColor) && x.Month.ToLower().Equals(Parent.Date.ToString("MMM-yy").ToLower())).FirstOrDefault();
				if (template != null)
					result = template.SlideMaster;
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
				string result = string.Empty;
				if (ShowMonth)
					result = Parent.Date.ToString("MMMM yyyy");
				return result;
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

		public string LegendString
		{
			get
			{
				string result = string.Empty;
				if (ShowLegend)
				{
					result = string.Join(";  ", Legend.Select(x => x.StringRepresentation));
				}
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
			result.AppendLine(@"<ShowMonth>" + ShowMonth.ToString() + @"</ShowMonth>");
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
			result.AppendLine(@"<ShowPrintTotalCostCalculated>" + ShowPrintTotalCostCalculated.ToString() + @"</ShowPrintTotalCostCalculated>");
			result.AppendLine(@"<ShowDigitalTotalCost>" + ShowDigitalTotalCost.ToString() + @"</ShowDigitalTotalCost>");
			if (DigitalTotalCost.HasValue)
				result.AppendLine(@"<DigitalTotalCost>" + DigitalTotalCost.Value.ToString() + @"</DigitalTotalCost>");
			result.AppendLine(@"<ShowTVTotalCost>" + ShowTVTotalCost.ToString() + @"</ShowTVTotalCost>");
			if (TVTotalCost.HasValue)
				result.AppendLine(@"<TVTotalCost>" + TVTotalCost.Value.ToString() + @"</TVTotalCost>");
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

			#region Legend
			result.AppendLine(@"<ShowLegend>" + ShowLegend.ToString() + @"</ShowLegend>");
			result.AppendLine(@"<Legends>");
			foreach (CalendarLegend legend in Legend)
				result.AppendLine(@"<Legend>" + legend.Serialize() + @"</Legend>");
			result.AppendLine(@"</Legends>");
			result.AppendLine(@"<ApplyForAllLegend>" + ApplyForAllLegend.ToString() + @"</ApplyForAllLegend>");
			#endregion

			#region Style
			result.AppendLine(@"<SlideColor>" + SlideColor.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideColor>");
			result.AppendLine(@"<ApplyForAllThemeColor>" + ApplyForAllThemeColor.ToString() + @"</ApplyForAllThemeColor>");

			result.AppendLine(@"<ShowLogo>" + ShowLogo.ToString() + @"</ShowLogo>");
			if (Logo != null)
				result.AppendLine(@"<Logo>" + Convert.ToBase64String((byte[])converter.ConvertTo(Logo, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Logo>");
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
					case "ShowMonth":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowMonth = tempBool;
						break;
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
					case "ShowPrintTotalCostCalculated":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPrintTotalCostCalculated = tempBool;
						break;
					case "ShowDigitalTotalCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDigitalTotalCost = tempBool;
						break;
					case "DigitalTotalCost":
						if (double.TryParse(childNode.InnerText, out tempDouble))
							DigitalTotalCost = tempDouble;
						break;
					case "ShowTVTotalCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTVTotalCost = tempBool;
						break;
					case "TVTotalCost":
						if (double.TryParse(childNode.InnerText, out tempDouble))
							TVTotalCost = tempDouble;
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

					#region Legend
					case "ShowLegend":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLegend = tempBool;
						break;
					case "Legends":
						Legend.Clear();
						foreach (XmlNode legendNode in childNode.ChildNodes)
						{
							var legend = new CalendarLegend();
							legend.Deserialize(legendNode);
							Legend.Add(legend);
						}
						break;
					case "ApplyForAllLegend":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ApplyForAllLegend = tempBool;
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
							Logo = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
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

		public void UpdateLegend()
		{
			var _newLegends = new List<CalendarLegend>();
			var _legendsFromDays = new List<CalendarLegend>();

			var legend = new CalendarLegend();
			legend.Code = "bw";
			legend.Description = "black and white";
			_legendsFromDays.Add(legend);

			legend = new CalendarLegend();
			legend.Code = "sc";
			legend.Description = "spot color";
			_legendsFromDays.Add(legend);

			legend = new CalendarLegend();
			legend.Code = "fc";
			legend.Description = "full color";
			_legendsFromDays.Add(legend);

			foreach (CalendarDay day in Parent.Days)
			{
				if (!string.IsNullOrEmpty(day.Newspaper.PublicationName))
				{
					legend = new CalendarLegend();
					legend.Description = day.Newspaper.PublicationName;
					legend.Code = day.Newspaper.PublicationAbbreviation;
					if (!_legendsFromDays.Select(x => x.Description).Contains(legend.Description))
						_legendsFromDays.Add(legend);
				}
				if (!string.IsNullOrEmpty(day.Newspaper.Section))
				{
					legend = new CalendarLegend();
					legend.Description = day.Newspaper.Section;
					legend.Code = day.Newspaper.SectionAbbreviation;
					if (!_legendsFromDays.Select(x => x.Description).Contains(legend.Description))
						_legendsFromDays.Add(legend);
				}
			}
			_newLegends.AddRange(Legend.Where(x => _legendsFromDays.Select(y => y.Description).Contains(x.Description)));
			_newLegends.AddRange(_legendsFromDays.Where(x => !Legend.Select(y => y.Description).Contains(x.Description)));
			Legend.Clear();
			Legend.AddRange(_newLegends);
		}

		private string[] GetTotalTags()
		{
			var tagValues = new List<string>();
			if (ShowPrintTotalCostCalculated | ShowPrintTotalCostManual)
				tagValues.Add("Newspaper Investment: " + (ShowPrintTotalCostCalculated ? PrintTotalCostCalculated.ToString("$#,###.00") : (PrintTotalCost.HasValue ? PrintTotalCost.Value.ToString("$#,###.00") : string.Empty)));
			if (ShowDigitalTotalCost && DigitalTotalCost.HasValue)
				tagValues.Add("Digital Investment: " + DigitalTotalCost.Value.ToString("$#,###.00"));
			if (ShowTVTotalCost && TVTotalCost.HasValue)
				tagValues.Add("TV Investment: " + TVTotalCost.Value.ToString("$#,###.00"));
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

	public class ImageSource
	{
		private readonly CalendarDay _parent;

		public ImageSource(CalendarDay parent)
		{
			_parent = parent;
		}

		public Image BigImage { get; set; }
		public Image SmallImage { get; set; }
		public Image TinyImage { get; set; }
		public Image XtraTinyImage { get; set; }

		public DateTime? Day
		{
			get { return _parent != null ? (DateTime?)_parent.Date : null; }
		}

		public bool ContainsData
		{
			get { return XtraTinyImage != null; }
		}

		public string Serialize()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
			var result = new StringBuilder();
			result.Append("<BigImage>" + Convert.ToBase64String((byte[])converter.ConvertTo(BigImage, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</BigImage>");
			result.Append("<SmallImage>" + Convert.ToBase64String((byte[])converter.ConvertTo(SmallImage, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</SmallImage>");
			result.Append("<TinyImage>" + Convert.ToBase64String((byte[])converter.ConvertTo(TinyImage, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</TinyImage>");
			result.Append("<XtraTinyImage>" + Convert.ToBase64String((byte[])converter.ConvertTo(XtraTinyImage, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</XtraTinyImage>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "BigImage":
						if (string.IsNullOrEmpty(childNode.InnerText))
							BigImage = null;
						else
							BigImage = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
						break;
					case "SmallImage":
						if (string.IsNullOrEmpty(childNode.InnerText))
							SmallImage = null;
						else
							SmallImage = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
						break;
					case "TinyImage":
						if (string.IsNullOrEmpty(childNode.InnerText))
							TinyImage = null;
						else
							TinyImage = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
						break;
					case "XtraTinyImage":
						if (string.IsNullOrEmpty(childNode.InnerText))
							TinyImage = null;
						else
							XtraTinyImage = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
						break;
				}
			}
		}

		public ImageSource Clone(CalendarDay newParent)
		{
			var result = new ImageSource(newParent);
			result.BigImage = BigImage;
			result.SmallImage = SmallImage;
			result.TinyImage = TinyImage;
			result.XtraTinyImage = XtraTinyImage;
			return result;
		}
	}
}