using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace CalendarBuilder.BusinessClasses
{
    public enum CalendarStyle
    {
        Simple = 0,
        Graphic,
        Advanced,
        TV
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
        private static ScheduleManager _instance = new ScheduleManager();
        private Schedule _currentCalendar;

        public event EventHandler<SavingingEventArgs> SettingsSaved;

        private ScheduleManager()
        {
        }

        public static ScheduleManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public void OpenCalendar(string scheduleName, bool create)
        {
            string calendarFilePath = GetCalendarFileName(scheduleName);
            if (create && File.Exists(calendarFilePath))
                if (AppManager.ShowWarningQuestion(string.Format("An older Calendar is already saved with this same file name.\nDo you want to replace this file with a newer calendar?", scheduleName)) == DialogResult.Yes)
                    File.Delete(calendarFilePath);
            _currentCalendar = new Schedule(calendarFilePath);
        }

        public void OpenCalendar(string scheduleFilePath)
        {
            _currentCalendar = new Schedule(scheduleFilePath);
        }

        public string GetCalendarFileName(string calendarName)
        {
            return Path.Combine(ConfigurationClasses.SettingsManager.Instance.SaveFolder, calendarName + ".xml");
        }

        public Schedule GetLocalCalendar()
        {
            return new Schedule(_currentCalendar.CalendarFile.FullName);
        }

        public void SaveCalendar(Schedule localCalendar, bool quickSave, Control sender)
        {
            localCalendar.Save();
            _currentCalendar = localCalendar;
            using (ToolForms.FormProgress form = new ToolForms.FormProgress())
            {
                form.laProgress.Text = "Chill-Out for a few seconds...\nSaving settings...";
                form.TopMost = true;
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                {
                    FormMain.Instance.Invoke((MethodInvoker)delegate
                    {
                        if (this.SettingsSaved != null)
                            this.SettingsSaved(sender, new SavingingEventArgs(quickSave));
                    });
                }));

                form.Show();
                System.Windows.Forms.Application.DoEvents();

                thread.Start();

                while (thread.IsAlive)
                    System.Windows.Forms.Application.DoEvents();
                form.Close();
            }
        }

        public ShortSchedule[] GetShortScheduleList(DirectoryInfo rootFolder)
        {
            List<ShortSchedule> calendarList = new List<ShortSchedule>();
            foreach (var file in rootFolder.GetFiles("*.xml"))
            {
                ShortSchedule schedule = new ShortSchedule(file);
                if (!string.IsNullOrEmpty(schedule.BusinessName))
                    calendarList.Add(schedule);
            }
            return calendarList.ToArray();
        }

        public void RemoveInstance()
        {
            this.SettingsSaved = null;
        }
    }

    public class SavingingEventArgs : EventArgs
    {
        public SavingingEventArgs(bool quickSave)
        {
            this.QuickSave = quickSave;
        }

        public bool QuickSave { get; set; }
    }

    public class ShortSchedule
    {
        private FileInfo _calendarFile;

        public string BusinessName { get; set; }
        public string Status { get; set; }

        public string ShortFileName
        {
            get
            {
                return _calendarFile.Name.Replace(_calendarFile.Extension, "");
            }
        }

        public string FullFileName
        {
            get
            {
                return _calendarFile.FullName;
            }
        }

        public DateTime LastModifiedDate
        {
            get
            {
                return _calendarFile.LastWriteTime;
            }
        }

        public ShortSchedule(FileInfo file)
        {
            this.BusinessName = string.Empty;
            this.Status = ListManager.Instance.Statuses.FirstOrDefault();
            _calendarFile = file;
            Load();
        }

        private void Load()
        {
            XmlNode node;
            if (_calendarFile.Exists)
            {
                XmlDocument document = new XmlDocument();
                document.Load(_calendarFile.FullName);

                node = document.SelectSingleNode(@"/Schedule/BusinessName");
                if (node != null)
                    this.BusinessName = node.InnerText;

                node = document.SelectSingleNode(@"/Schedule/Status");
                if (node != null)
                    this.Status = node.InnerText;
            }
        }

        public void Save()
        {
            XmlNode node;
            if (_calendarFile.Exists)
            {
                try
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(_calendarFile.FullName);

                    node = document.SelectSingleNode(@"/Schedule/Status");
                    if (node != null)
                        node.InnerText = this.Status;
                    else
                    {
                        node = document.SelectSingleNode(@"/Schedule");
                        if (node != null)
                            node.InnerXml += (@"<Status>" + (this.Status != null ? this.Status.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + @"</Status>");
                    }
                    document.Save(_calendarFile.FullName);
                }
                catch
                {
                }
            }
        }
    }

    public class Schedule
    {
        private FileInfo _calendarFile { get; set; }
        public string BusinessName { get; set; }
        public string DecisionMaker { get; set; }
        public string ClientType { get; set; }
        public string Status { get; set; }
        public SalesStrategy SalesStrategy { get; set; }
        public DateTime? PresentationDate { get; set; }
        public DateTime? FlightDateStart { get; set; }
        public DateTime? FlightDateEnd { get; set; }

        public Calendar AdvancedCalendar { get; private set; }
        public Calendar GraphicCalendar { get; private set; }
        public Calendar SimpleCalendar { get; private set; }
        public Calendar TVCalendar { get; private set; }

        public string Name
        {
            get
            {
                return _calendarFile.Name.Replace(_calendarFile.Extension, "");
            }
            set
            {
                _calendarFile = new FileInfo(Path.Combine(_calendarFile.Directory.FullName, value + ".xml"));
            }
        }

        public FileInfo CalendarFile
        {
            get
            {
                return _calendarFile;
            }
        }

        public string FlightDates
        {
            get
            {
                if (this.FlightDateStart.HasValue && this.FlightDateEnd.HasValue)
                    return this.FlightDateStart.Value.ToString("MM/dd/yy") + " - " + this.FlightDateEnd.Value.ToString("MM/dd/yy");
                else
                    return string.Empty;
            }
        }

        public Schedule(string fileName)
        {
            this.BusinessName = string.Empty;
            this.DecisionMaker = string.Empty;
            this.ClientType = string.Empty;
            this.Status = ListManager.Instance.Statuses.FirstOrDefault();
            this.AdvancedCalendar = new CalendarSundayBased(this);
            this.GraphicCalendar = new CalendarSundayBased(this);
            this.SimpleCalendar = new CalendarSundayBased(this);
            this.TVCalendar = new CalendarMondayBased(this);

            _calendarFile = new FileInfo(fileName);
            if (!File.Exists(fileName))
            {
                StringBuilder xml = new StringBuilder();
                xml.AppendLine(@"<Schedule>");
                xml.AppendLine(@"<Status>" + (this.Status != null ? this.Status.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + @"</Status>");
                xml.AppendLine(@"</Schedule>");
                using (StreamWriter sw = new StreamWriter(_calendarFile.FullName, false))
                {
                    sw.Write(xml);
                    sw.Flush();
                }
                _calendarFile = new FileInfo(fileName);
            }
            else
                Load();
        }

        private void Load()
        {
            int tempInt;
            DateTime tempDateTime;

            XmlNode node;
            if (_calendarFile.Exists)
            {
                XmlDocument document = new XmlDocument();
                document.Load(_calendarFile.FullName);

                node = document.SelectSingleNode(@"/Schedule/BusinessName");
                if (node != null)
                    this.BusinessName = node.InnerText;

                node = document.SelectSingleNode(@"/Schedule/DecisionMaker");
                if (node != null)
                    this.DecisionMaker = node.InnerText;

                node = document.SelectSingleNode(@"/Schedule/ClientType");
                if (node != null)
                    this.ClientType = node.InnerText;

                node = document.SelectSingleNode(@"/Schedule/Status");
                if (node != null)
                    this.Status = node.InnerText;

                node = document.SelectSingleNode(@"/Schedule/SalesStrategy");
                if (node != null)
                    if (int.TryParse(node.InnerText, out tempInt))
                        this.SalesStrategy = (SalesStrategy)tempInt;

                node = document.SelectSingleNode(@"/Schedule/PresentationDate");
                if (node != null)
                    if (DateTime.TryParse(node.InnerText, out tempDateTime))
                        this.PresentationDate = tempDateTime;

                node = document.SelectSingleNode(@"/Schedule/FlightDateStart");
                if (node != null)
                    if (DateTime.TryParse(node.InnerText, out tempDateTime))
                        this.FlightDateStart = tempDateTime;

                node = document.SelectSingleNode(@"/Schedule/FlightDateEnd");
                if (node != null)
                    if (DateTime.TryParse(node.InnerText, out tempDateTime))
                        this.FlightDateEnd = tempDateTime;

                node = document.SelectSingleNode(@"/Schedule/AdvancedCalendar");
                if (node != null)
                {
                    this.AdvancedCalendar.Deserialize(node);
                }
                else
                {
                    this.AdvancedCalendar.UpdateDaysCollection();
                    this.AdvancedCalendar.UpdateMonthCollection();
                    this.AdvancedCalendar.UpdateNotesCollection();
                }

                node = document.SelectSingleNode(@"/Schedule/GraphicCalendar");
                if (node != null)
                {
                    this.GraphicCalendar.Deserialize(node);
                }
                else
                {
                    this.GraphicCalendar.UpdateDaysCollection();
                    this.GraphicCalendar.UpdateMonthCollection();
                    this.GraphicCalendar.UpdateNotesCollection();
                }

                node = document.SelectSingleNode(@"/Schedule/SimpleCalendar");
                if (node != null)
                {
                    this.SimpleCalendar.Deserialize(node);
                }
                else
                {
                    this.SimpleCalendar.UpdateDaysCollection();
                    this.SimpleCalendar.UpdateMonthCollection();
                    this.SimpleCalendar.UpdateNotesCollection();
                }

                node = document.SelectSingleNode(@"/Schedule/TVCalendar");
                if (node != null)
                {
                    this.TVCalendar.Deserialize(node);
                }
                else
                {
                    this.TVCalendar.UpdateDaysCollection();
                    this.TVCalendar.UpdateMonthCollection();
                    this.TVCalendar.UpdateNotesCollection();
                }
            }
        }

        public void Save()
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
            StringBuilder xml = new StringBuilder();

            xml.AppendLine(@"<Schedule>");
            xml.AppendLine(@"<BusinessName>" + this.BusinessName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</BusinessName>");
            if (!ListManager.Instance.Advertisers.Contains(this.BusinessName))
            {
                ListManager.Instance.Advertisers.Add(this.BusinessName);
                ListManager.Instance.SaveAdvertisers();
            }
            xml.AppendLine(@"<DecisionMaker>" + this.DecisionMaker.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DecisionMaker>");
            if (!ListManager.Instance.DecisionMakers.Contains(this.DecisionMaker))
            {
                ListManager.Instance.DecisionMakers.Add(this.DecisionMaker);
                ListManager.Instance.SaveDecisionMakers();
            }
            xml.AppendLine(@"<Status>" + (this.Status != null ? this.Status.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + @"</Status>");
            xml.AppendLine(@"<ClientType>" + this.ClientType.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</ClientType>");
            xml.AppendLine(@"<SalesStrategy>" + (int)this.SalesStrategy + @"</SalesStrategy>");
            if (PresentationDate.HasValue)
                xml.AppendLine(@"<PresentationDate>" + this.PresentationDate.Value.ToString() + @"</PresentationDate>");
            if (this.FlightDateStart.HasValue)
                xml.AppendLine(@"<FlightDateStart>" + this.FlightDateStart.Value.ToString() + @"</FlightDateStart>");
            if (this.FlightDateEnd.HasValue)
                xml.AppendLine(@"<FlightDateEnd>" + this.FlightDateEnd.Value.ToString() + @"</FlightDateEnd>");

            xml.AppendLine(@"<AdvancedCalendar>" + this.AdvancedCalendar.Serialize() + @"</AdvancedCalendar>");
            xml.AppendLine(@"<GraphicCalendar>" + this.GraphicCalendar.Serialize() + @"</GraphicCalendar>");
            xml.AppendLine(@"<SimpleCalendar>" + this.SimpleCalendar.Serialize() + @"</SimpleCalendar>");
            xml.AppendLine(@"<TVCalendar>" + this.TVCalendar.Serialize() + @"</TVCalendar>");

            xml.AppendLine(@"</Schedule>");

            using (StreamWriter sw = new StreamWriter(_calendarFile.FullName, false))
            {
                sw.Write(xml);
                sw.Flush();
            }
        }


    }

    public abstract class Calendar
    {
        public Schedule Schedule { get; private set; }
        public List<CalendarMonth> Months { get; private set; }
        public List<CalendarDay> Days { get; private set; }
        public List<CalendarNote> Notes { get; private set; }

        public Calendar(Schedule parent)
        {
            this.Schedule = parent;
            this.Months = new List<CalendarMonth>();
            this.Days = new List<CalendarDay>();
            this.Notes = new List<CalendarNote>();
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Days>");
            foreach (CalendarDay day in this.Days)
                result.AppendLine(@"<Day>" + day.Serialize() + @"</Day>");
            result.AppendLine(@"</Days>");

            result.AppendLine(@"<Months>");
            foreach (CalendarMonth month in this.Months)
                result.AppendLine(@"<Month>" + month.Serialize() + @"</Month>");
            result.AppendLine(@"</Months>");

            result.AppendLine(@"<CalendarNotes>");
            foreach (CalendarNote note in this.Notes)
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
            if (this.Schedule.FlightDateStart.HasValue && this.Schedule.FlightDateEnd.HasValue)
            {
                List<CalendarNote> _notesToDelete = new List<CalendarNote>();
                foreach (CalendarNote note in this.Notes)
                {
                    if (note.FinishDay < this.Schedule.FlightDateStart.Value || note.StartDay > this.Schedule.FlightDateEnd.Value)
                        _notesToDelete.Add(note);
                    else if (note.StartDay < this.Schedule.FlightDateStart.Value)
                        note.StartDay = this.Schedule.FlightDateStart.Value;
                    else if (note.FinishDay > this.Schedule.FlightDateEnd.Value)
                        note.FinishDay = this.Schedule.FlightDateEnd.Value;
                    if (note.Length < 1)
                        _notesToDelete.Add(note);
                }
                foreach (CalendarNote note in _notesToDelete)
                    this.Notes.Remove(note);
            }
            else
                this.Notes.Clear();
            UpdateDayAndNoteLinks();
        }

        protected void UpdateDayAndNoteLinks()
        {
            foreach (CalendarDay day in this.Days)
                day.HasNotes = this.Notes.Where(x => day.Date >= x.StartDay && day.Date <= x.FinishDay).Count() > 0;
        }

        public void AddNote(DateRange range, string noteText = "")
        {
            CalendarNote newNote = new CalendarNote(this);
            newNote.StartDay = range.StartDate;
            newNote.FinishDay = range.FinishDate;
            newNote.Note = noteText;
            List<CalendarNote> _notesToDelete = new List<CalendarNote>();
            foreach (CalendarNote note in this.Notes)
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
                this.Notes.Remove(note);
            this.Notes.Add(newNote);
            UpdateDayAndNoteLinks();
        }

        public void DeleteNote(CalendarNote note)
        {
            this.Notes.Remove(note);
            UpdateDayAndNoteLinks();
        }
    }

    public class CalendarSundayBased : Calendar
    {
        public CalendarSundayBased(Schedule parent)
            : base(parent)
        {
        }

        public override void Deserialize(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Days":
                        this.Days.Clear();
                        foreach (XmlNode dayNode in childNode.ChildNodes)
                        {
                            CalendarDay day = new CalendarDaySundayBased(this);
                            day.Deserialize(dayNode);
                            this.Days.Add(day);
                        }
                        break;
                    case "Months":
                        this.Months.Clear();
                        foreach (XmlNode monthNode in childNode.ChildNodes)
                        {
                            CalendarMonth month = new CalendarMonthSundayBased(this);
                            month.Deserialize(monthNode);
                            this.Months.Add(month);
                        }
                        break;
                    case "CalendarNotes":
                        this.Months.Clear();
                        foreach (XmlNode noteNode in childNode.ChildNodes)
                        {
                            CalendarNote note = new CalendarNote(this);
                            note.Deserialize(noteNode);
                            if (note.StartDay != DateTime.MinValue && note.FinishDay != DateTime.MinValue)
                                this.Notes.Add(note);
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
            if (this.Schedule.FlightDateStart.HasValue && this.Schedule.FlightDateEnd.HasValue)
            {
                List<CalendarDay> days = new List<CalendarDay>();

                DateTime startDate = new DateTime(this.Schedule.FlightDateStart.Value.Year, this.Schedule.FlightDateStart.Value.Month, 1);
                while (startDate.DayOfWeek != DayOfWeek.Sunday)
                    startDate = startDate.AddDays(-1);

                DateTime endDate = new DateTime(this.Schedule.FlightDateEnd.Value.Month < 12 ? this.Schedule.FlightDateEnd.Value.Year : (this.Schedule.FlightDateEnd.Value.Year + 1), (this.Schedule.FlightDateEnd.Value.Month < 12 ? this.Schedule.FlightDateEnd.Value.Month + 1 : 1), 1).AddDays(-1);
                while (endDate.DayOfWeek != DayOfWeek.Saturday)
                    endDate = endDate.AddDays(1);

                while (startDate <= endDate)
                {
                    CalendarDay day = this.Days.Where(x => x.Date.Equals(startDate)).FirstOrDefault();
                    if (day == null)
                    {
                        day = new CalendarDaySundayBased(this);
                        day.Date = startDate;
                    }
                    day.BelongsToSchedules = day.Date >= this.Schedule.FlightDateStart & day.Date <= this.Schedule.FlightDateEnd;
                    days.Add(day);
                    startDate = startDate.AddDays(1);
                }
                this.Days.Clear();
                this.Days.AddRange(days);
            }
            else
                this.Days.Clear();
        }

        public override void UpdateMonthCollection()
        {
            if (this.Schedule.FlightDateStart.HasValue && this.Schedule.FlightDateEnd.HasValue)
            {
                List<CalendarMonth> months = new List<CalendarMonth>();
                DateTime startDate = new DateTime(this.Schedule.FlightDateStart.Value.Year, this.Schedule.FlightDateStart.Value.Month, 1);
                while (startDate <= this.Schedule.FlightDateEnd.Value)
                {
                    CalendarMonth month = this.Months.Where(x => x.Date.Equals(startDate)).FirstOrDefault();
                    if (month == null)
                    {
                        month = new CalendarMonthSundayBased(this);
                        month.Date = startDate;
                    }
                    month.Days.Clear();
                    month.Days.AddRange(this.Days.Where(x => x.Date >= month.DaysRangeBegin && x.Date <= month.DaysRangeEnd));
                    month.OutputData.UpdateLegend();
                    months.Add(month);
                    startDate = startDate.AddMonths(1);
                }
                this.Months.Clear();
                this.Months.AddRange(months);
            }
            else
                this.Months.Clear();
        }

        public override DateTime[][] GetDaysByWeek(DateTime start, DateTime end)
        {
            List<DateTime[]> weeks = new List<DateTime[]>();
            List<DateTime> week = new List<DateTime>();
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
            List<DateRange> result = new List<DateRange>();
            List<DateTime> selectedDates = new List<DateTime>();
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
                DateRange range = new DateRange();
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
            : base(parent)
        {
        }

        public override void Deserialize(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Days":
                        this.Days.Clear();
                        foreach (XmlNode dayNode in childNode.ChildNodes)
                        {
                            CalendarDay day = new CalendarDayMondayBased(this);
                            day.Deserialize(dayNode);
                            this.Days.Add(day);
                        }
                        break;
                    case "Months":
                        this.Months.Clear();
                        foreach (XmlNode monthNode in childNode.ChildNodes)
                        {
                            CalendarMonth month = new CalendarMonthMondayBased(this);
                            month.Deserialize(monthNode);
                            this.Months.Add(month);
                        }
                        break;
                    case "CalendarNotes":
                        this.Months.Clear();
                        foreach (XmlNode noteNode in childNode.ChildNodes)
                        {
                            CalendarNote note = new CalendarNote(this);
                            note.Deserialize(noteNode);
                            if (note.StartDay != DateTime.MinValue && note.FinishDay != DateTime.MinValue)
                                this.Notes.Add(note);
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
            if (this.Schedule.FlightDateStart.HasValue && this.Schedule.FlightDateEnd.HasValue)
            {
                List<CalendarDay> days = new List<CalendarDay>();

                DateTime startDate = new DateTime(this.Schedule.FlightDateStart.Value.Year, this.Schedule.FlightDateStart.Value.Month, 1);
                while (startDate.DayOfWeek != DayOfWeek.Monday)
                    startDate = startDate.AddDays(-1);

                DateTime endDate = new DateTime(this.Schedule.FlightDateEnd.Value.Month < 12 ? this.Schedule.FlightDateEnd.Value.Year : (this.Schedule.FlightDateEnd.Value.Year + 1), (this.Schedule.FlightDateEnd.Value.Month < 12 ? this.Schedule.FlightDateEnd.Value.Month + 1 : 1), 1).AddDays(-1);
                while (endDate.DayOfWeek != DayOfWeek.Sunday)
                    endDate = endDate.AddDays(1);

                while (startDate <= endDate)
                {
                    CalendarDay day = this.Days.Where(x => x.Date.Equals(startDate)).FirstOrDefault();
                    if (day == null)
                    {
                        day = new CalendarDayMondayBased(this);
                        day.Date = startDate;
                    }
                    day.BelongsToSchedules = day.Date >= this.Schedule.FlightDateStart & day.Date <= this.Schedule.FlightDateEnd;
                    days.Add(day);
                    startDate = startDate.AddDays(1);
                }
                this.Days.Clear();
                this.Days.AddRange(days);
            }
            else
                this.Days.Clear();
        }

        public override void UpdateMonthCollection()
        {
            if (this.Schedule.FlightDateStart.HasValue && this.Schedule.FlightDateEnd.HasValue)
            {
                List<CalendarMonth> months = new List<CalendarMonth>();
                DateTime startDate = new DateTime(this.Schedule.FlightDateStart.Value.Year, this.Schedule.FlightDateStart.Value.Month, 1);
                while (startDate <= this.Schedule.FlightDateEnd.Value)
                {
                    CalendarMonth month = this.Months.Where(x => x.Date.Equals(startDate)).FirstOrDefault();
                    if (month == null)
                    {
                        month = new CalendarMonthMondayBased(this);
                        month.Date = startDate;
                    }
                    month.Days.Clear();
                    month.Days.AddRange(this.Days.Where(x => x.Date >= month.DaysRangeBegin && x.Date <= month.DaysRangeEnd));
                    month.OutputData.UpdateLegend();
                    months.Add(month);
                    startDate = startDate.AddMonths(1);
                }
                this.Months.Clear();
                this.Months.AddRange(months);
            }
            else
                this.Months.Clear();
        }

        public override DateTime[][] GetDaysByWeek(DateTime start, DateTime end)
        {
            List<DateTime[]> weeks = new List<DateTime[]>();
            List<DateTime> week = new List<DateTime>();
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
            List<DateRange> result = new List<DateRange>();
            List<DateTime> selectedDates = new List<DateTime>();
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
                DateRange range = new DateRange();
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
        public Calendar Parent { get; private set; }
        public DateTime DaysRangeBegin { get; set; }
        public DateTime DaysRangeEnd { get; set; }
        public List<CalendarDay> Days { get; private set; }
        public CalendarOutputData OutputData { get; private set; }

        public abstract DateTime Date { get; set; }

        public CalendarMonth(Calendar parent)
        {
            this.Parent = parent;
            this.Days = new List<CalendarDay>();
            this.OutputData = new CalendarOutputData(this);
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Date>" + _date.ToString() + @"</Date>");
            result.AppendLine(@"<OutputData>" + this.OutputData.Serialize() + @"</OutputData>");
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
                        this.OutputData.Deserialize(childNode);
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
        }

        public override DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                this.DaysRangeBegin = _date;
                this.DaysRangeEnd = _date.AddMonths(1).AddDays(-1);
            }
        }
    }

    public class CalendarMonthMondayBased : CalendarMonth
    {
        public CalendarMonthMondayBased(Calendar parent)
            : base(parent)
        {
        }

        public override DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                DateTime temp = value;
                while (temp.DayOfWeek != DayOfWeek.Monday)
                    temp = temp.AddDays(-1);
                this.DaysRangeBegin = temp;

                temp = _date.AddMonths(1).AddDays(-1);
                while (temp.DayOfWeek != DayOfWeek.Sunday)
                    temp = temp.AddDays(-1);
                this.DaysRangeEnd = temp;
            }
        }
    }

    public abstract class CalendarDay
    {
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
                StringBuilder result = new StringBuilder();

                string temp = this.Digital.Summary;
                if (!string.IsNullOrEmpty(temp))
                    result.AppendLine(temp);

                temp = this.Newspaper.Summary;
                if (!string.IsNullOrEmpty(temp))
                    result.AppendLine(temp);

                if (!string.IsNullOrEmpty(this.Comment1))
                    result.AppendLine(this.Comment1);

                if (!string.IsNullOrEmpty(this.Comment2))
                    result.AppendLine(this.Comment2);
                return result.ToString();
            }
        }

        public bool ContainsData
        {
            get
            {
                return !string.IsNullOrEmpty(this.Summary) || this.Logo.ContainsData;
            }
        }

        public CalendarDay(Calendar parent)
        {
            this.Parent = parent;
            this.Digital = new DigitalProperties(this);
            this.Newspaper = new NewspaperProperties(this);
            this.Logo = new ImageSource(this);
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<Date>" + this.Date.ToString() + @"</Date>");
            result.AppendLine(@"<Digital>" + this.Digital.Serialize() + @"</Digital>");
            result.AppendLine(@"<Newspaper>" + this.Newspaper.Serialize() + @"</Newspaper>");
            if (!string.IsNullOrEmpty(this.Comment1))
                result.AppendLine(@"<Comment1>" + this.Comment1.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Comment1>");
            if (!string.IsNullOrEmpty(this.Comment2))
                result.AppendLine(@"<Comment2>" + this.Comment2.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Comment2>");
            result.AppendLine(@"<Logo>" + this.Logo.Serialize() + @"</Logo>");
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
                            this.Date = tempDate;
                        break;
                    case "Digital":
                        this.Digital.Deserialize(childNode);
                        break;
                    case "Newspaper":
                        this.Newspaper.Deserialize(childNode);
                        break;
                    case "Comment1":
                        this.Comment1 = childNode.InnerText;
                        break;
                    case "Comment2":
                        this.Comment2 = childNode.InnerText;
                        break;
                    case "Logo":
                        this.Logo.Deserialize(childNode);
                        break;
                }
            }
        }

        public abstract int WeekDayIndex { get; }

        public void ClearData(DayDataType dataToClear = DayDataType.All)
        {
            switch (dataToClear)
            {
                case DayDataType.Digital:
                    this.Digital = new DigitalProperties(this);
                    break;
                case DayDataType.Newspaper:
                    this.Newspaper = new NewspaperProperties(this);
                    break;
                case DayDataType.Comment:
                    this.Comment1 = null;
                    this.Comment2 = null;
                    break;
                case DayDataType.Logo:
                    this.Logo = new ImageSource(this);
                    break;
                case DayDataType.All:
                    this.Comment1 = null;
                    this.Comment2 = null;
                    this.Logo = new ImageSource(this);
                    this.Digital = new DigitalProperties(this);
                    this.Newspaper = new NewspaperProperties(this);
                    break;
            }
        }
    }

    public class CalendarDaySundayBased : CalendarDay
    {
        public CalendarDaySundayBased(Calendar parent)
            : base(parent)
        {
        }

        public override int WeekDayIndex
        {
            get
            {
                switch (this.Date.DayOfWeek)
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
            : base(parent)
        {
        }

        public override int WeekDayIndex
        {
            get
            {
                switch (this.Date.DayOfWeek)
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
        public Calendar Parent { get; private set; }
        public DateTime StartDay { get; set; }
        public DateTime FinishDay { get; set; }
        public Color BackgroundColor { get; set; }
        public string Note { get; set; }

        #region Output Data
        public float Top { get; set; }
        public float Left { get; set; }
        public float Right { get; set; }
        public float Height { get; set; }
        #endregion

        public int Length
        {
            get
            {
                return (this.FinishDay - this.StartDay).Days;
            }
        }

        public Color ForeColor
        {
            get
            {
                int d = 0;
                // Counting the perceptive luminance - human eye favors green color... 
                double a = 1 - (0.299 * this.BackgroundColor.R + 0.587 * this.BackgroundColor.G + 0.114 * this.BackgroundColor.B) / 255;

                if (a < 0.5)
                    d = 0; // bright colors - black font
                else
                    d = 255; // dark colors - white font

                return Color.FromArgb(d, d, d);
            }
        }

        public CalendarNote(Calendar parent)
        {
            this.Parent = parent;
            this.BackgroundColor = Color.LemonChiffon;
            this.Note = string.Empty;

            this.Height = 25f;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<StartDay>" + this.StartDay.ToString() + @"</StartDay>");
            result.AppendLine(@"<FinishDay>" + this.FinishDay.ToString() + @"</FinishDay>");
            result.AppendLine(@"<BackgroundColor>" + this.BackgroundColor.ToArgb().ToString() + @"</BackgroundColor>");
            result.AppendLine(@"<Note>" + this.Note.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Note>");
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
                            this.StartDay = tempDate;
                        break;
                    case "FinishDay":
                        if (DateTime.TryParse(childNode.InnerText, out tempDate))
                            this.FinishDay = tempDate;
                        break;
                    case "BackgroundColor":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.BackgroundColor = Color.FromArgb(tempInt);
                        break;
                    case "Note":
                        this.Note = childNode.InnerText;
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
            get
            {
                return this.StartDate.ToString("MM/dd/yy") + "-" + this.FinishDate.ToString("MM/dd/yy");
            }
        }
    }

    public class DigitalProperties
    {
        private CalendarDay _parent = null;
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
            get
            {
                return _parent.Date;
            }
        }

        public string Summary
        {
            get
            {
                List<string> result = new List<string>();
                if (!string.IsNullOrEmpty(this.CustomNote))
                    result.Add(this.CustomNote);
                if (!string.IsNullOrEmpty(this.QuickListRecord))
                    result.Add(this.QuickListRecord);
                if (!string.IsNullOrEmpty(this.Category) && this.ShowCategory)
                    result.Add(this.Category);
                if (!string.IsNullOrEmpty(this.SubCategory) && this.ShowSubCategory)
                    result.Add(this.SubCategory);
                if (!string.IsNullOrEmpty(this.ProductName) && this.ShowProduct)
                    result.Add(this.ProductName);
                return string.Join(", ", result.ToArray());
            }
        }

        public DigitalProperties(CalendarDay parent)
        {
            _parent = parent;
            this.ShowCategory = true;
            this.ShowSubCategory = true;
            this.ShowProduct = true;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            if (!string.IsNullOrEmpty(this.Category))
                result.AppendLine(@"<Category>" + this.Category.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Category>");
            if (!string.IsNullOrEmpty(this.SubCategory))
                result.AppendLine(@"<SubCategory>" + this.SubCategory.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SubCategory>");
            if (!string.IsNullOrEmpty(this.ProductName))
                result.AppendLine(@"<ProductName>" + this.ProductName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</ProductName>");
            if (!string.IsNullOrEmpty(this.CustomNote))
                result.AppendLine(@"<CustomNote>" + this.CustomNote.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</CustomNote>");
            if (!string.IsNullOrEmpty(this.QuickListRecord))
                result.AppendLine(@"<QuickListRecord>" + this.QuickListRecord.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</QuickListRecord>");

            result.AppendLine(@"<ShowCategory>" + this.ShowCategory.ToString() + @"</ShowCategory>");
            result.AppendLine(@"<ShowSubCategory>" + this.ShowSubCategory.ToString() + @"</ShowSubCategory>");
            result.AppendLine(@"<ShowProduct>" + this.ShowProduct.ToString() + @"</ShowProduct>");

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
                        this.Category = childNode.InnerText;
                        break;
                    case "SubCategory":
                        this.SubCategory = childNode.InnerText;
                        break;
                    case "ProductName":
                        this.ProductName = childNode.InnerText;
                        break;
                    case "CustomNote":
                        this.CustomNote = childNode.InnerText;
                        break;
                    case "QuickListRecord":
                        this.QuickListRecord = childNode.InnerText;
                        break;
                    case "ShowCategory":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowCategory = tempBool;
                        break;
                    case "ShowSubCategory":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSubCategory = tempBool;
                        break;
                    case "ShowProduct":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowProduct = tempBool;
                        break;
                }
            }
        }

        public DigitalProperties Clone(CalendarDay newParent)
        {
            DigitalProperties result = null;
            XmlDocument document = new XmlDocument();
            document.LoadXml(@"<Digital>" + this.Serialize() + @"</Digital>");
            result = new DigitalProperties(newParent);
            result.Deserialize(document.FirstChild);
            return result;
        }
    }

    public class NewspaperProperties
    {
        private CalendarDay _parent = null;
        public string PublicationName { get; set; }
        public string Section { get; set; }
        public string PageSize { get; set; }
        public string Color { get; set; }
        public double? TotalCost { get; set; }
        public string CustomNote { get; set; }
        public string QuickListRecord { get; set; }

        public DateTime Day
        {
            get
            {
                return _parent.Date;
            }
        }

        public string SectionAbbreviation
        {
            get
            {
                PrintSection section = ListManager.Instance.PrintSections.Where(x => x.Name.Equals(this.Section)).FirstOrDefault();
                if (section != null)
                    return section.Abbreviation;
                else if (!string.IsNullOrEmpty(this.Section))
                    return this.Section.Substring(0, 2);
                else
                    return string.Empty;
            }
        }

        public string PublicationAbbreviation
        {
            get
            {
                PrintSource printSource = ListManager.Instance.PrintSources.Where(x => x.Name.Equals(this.PublicationName)).FirstOrDefault();
                if (printSource != null)
                    return printSource.Abbreviation;
                else if (!string.IsNullOrEmpty(this.PublicationName))
                    return this.PublicationName.Substring(0, 3).ToUpper();
                else
                    return string.Empty;
            }
        }

        public string Summary
        {
            get
            {
                List<string> result = new List<string>();
                if (!string.IsNullOrEmpty(this.CustomNote))
                    result.Add(this.CustomNote);
                if (!string.IsNullOrEmpty(this.QuickListRecord))
                    result.Add(this.QuickListRecord);
                if (!string.IsNullOrEmpty(this.PublicationAbbreviation))
                    result.Add(this.PublicationAbbreviation);
                if (!string.IsNullOrEmpty(this.SectionAbbreviation))
                    result.Add(this.SectionAbbreviation);
                if (!string.IsNullOrEmpty(this.PageSize))
                    result.Add(this.PageSize);
                if (!string.IsNullOrEmpty(this.Color))
                    result.Add(this.Color);
                if (this.TotalCost.HasValue)
                    result.Add(this.TotalCost.Value.ToString("$#,##0"));
                return string.Join(", ", result.ToArray());
            }
        }

        public NewspaperProperties(CalendarDay parent)
        {
            _parent = parent;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            if (!string.IsNullOrEmpty(this.PublicationName))
                result.AppendLine(@"<PublicationName>" + this.PublicationName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</PublicationName>");
            if (!string.IsNullOrEmpty(this.Section))
                result.AppendLine(@"<Section>" + this.Section.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Section>");
            if (!string.IsNullOrEmpty(this.PageSize))
                result.AppendLine(@"<PageSize>" + this.PageSize.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</PageSize>");
            if (!string.IsNullOrEmpty(this.Color))
                result.AppendLine(@"<Color>" + this.Color.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Color>");
            if (this.TotalCost.HasValue)
                result.AppendLine(@"<TotalCost>" + this.TotalCost.Value.ToString() + @"</TotalCost>");
            if (!string.IsNullOrEmpty(this.CustomNote))
                result.AppendLine(@"<CustomNote>" + this.CustomNote.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</CustomNote>");
            if (!string.IsNullOrEmpty(this.QuickListRecord))
                result.AppendLine(@"<QuickListRecord>" + this.QuickListRecord.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</QuickListRecord>");
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
                        this.PublicationName = childNode.InnerText;
                        break;
                    case "Section":
                        this.Section = childNode.InnerText;
                        break;
                    case "PageSize":
                        this.PageSize = childNode.InnerText;
                        break;
                    case "Color":
                        this.Color = childNode.InnerText;
                        break;
                    case "TotalCost":
                        this.TotalCost = null;
                        if (double.TryParse(childNode.InnerText, out tempDouble))
                            this.TotalCost = tempDouble;
                        break;
                    case "CustomNote":
                        this.CustomNote = childNode.InnerText;
                        break;
                    case "QuickListRecord":
                        this.QuickListRecord = childNode.InnerText;
                        break;
                }
            }
        }

        public NewspaperProperties Clone(CalendarDay newParent)
        {
            NewspaperProperties result = null;
            XmlDocument document = new XmlDocument();
            document.LoadXml(@"<Newspaper>" + this.Serialize() + @"</Newspaper>");
            result = new NewspaperProperties(newParent);
            result.Deserialize(document.FirstChild);
            return result;
        }
    }

    public class CalendarOutputData
    {
        private List<string> _dayLogosPaths = new List<string>();

        public List<CalendarNote> Notes { get; private set; }

        public CalendarMonth Parent { get; private set; }

        #region Basic
        public bool ShowMonth { get; set; }
        public bool ShowHeader { get; set; }
        public bool ShowBusinessName { get; set; }
        public bool ShowDecisionMaker { get; set; }
        private string _businessName = string.Empty;
        private string _decisionMaker = string.Empty;
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
        public bool ShowCustomComment { get; set; }
        public string CustomComment { get; set; }
        public bool ApplyForAllCustomComment { get; set; }

        private int? _activeDays;
        private int? _printAdsNumber;
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
                if (this.ShowBusinessName)
                {
                    if (!string.IsNullOrEmpty(_businessName))
                        return _businessName;
                    else
                        return this.Parent.Parent.Schedule.BusinessName;
                }
                else
                    return string.Empty;
            }
            set
            {
                if (!value.Equals(this.Parent.Parent.Schedule.BusinessName))
                    _businessName = value;
                else
                    _businessName = string.Empty;
            }
        }

        public string DecisionMaker
        {
            get
            {
                if (this.ShowDecisionMaker)
                {
                    if (!string.IsNullOrEmpty(_decisionMaker))
                        return _decisionMaker;
                    else
                        return this.Parent.Parent.Schedule.DecisionMaker;
                }
                else
                    return string.Empty;
            }
            set
            {
                if (!value.Equals(this.Parent.Parent.Schedule.DecisionMaker))
                    _decisionMaker = value;
                else
                    _decisionMaker = string.Empty;
            }
        }

        public double PrintTotalCostCalculated
        {
            get
            {
                return this.Parent.Days.Select(x => x.Newspaper.TotalCost.HasValue ? x.Newspaper.TotalCost.Value : 0).Sum();
            }
        }

        public int CalculatedActiveDays
        {
            get
            {
                return this.Parent.Days.Where(x => x.ContainsData).Count();
            }
        }

        public int ActiveDays
        {
            get
            {
                if (!_activeDays.HasValue)
                    return this.CalculatedActiveDays;
                else
                    return _activeDays.Value;
            }
            set
            {
                if (this.CalculatedActiveDays != value)
                    _activeDays = value;
                else
                    _activeDays = null;
            }
        }

        public int CalculatedPrintAdsNumber
        {
            get
            {
                return this.Parent.Days.Where(x => !string.IsNullOrEmpty(x.Newspaper.Summary)).Count();
            }
        }

        public int PrintAdsNumber
        {
            get
            {
                if (!_printAdsNumber.HasValue)
                    return this.CalculatedPrintAdsNumber;
                else
                    return _printAdsNumber.Value;
            }
            set
            {
                if (this.CalculatedPrintAdsNumber != value)
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
                BusinessClasses.CalendarTemplate template = BusinessClasses.OutputManager.Instance.CalendarTemplates.Where(x => x.IsLarge == this.ShowBigDate && x.HasLogo == this.ShowLogo && x.Color.ToLower().Equals(this.SlideColor) && x.Month.ToLower().Equals(this.Parent.Date.ToString("MMM-yy").ToLower())).FirstOrDefault();
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
                switch (this.SlideColor)
                {
                    case "black":
                        result = Microsoft.VisualBasic.Information.RGB(0, 0, 0);
                        break;
                    case "blue":
                        result = Microsoft.VisualBasic.Information.RGB(0, 0, 102);
                        break;
                    case "gray":
                        result = Microsoft.VisualBasic.Information.RGB(0, 0, 0);
                        break;
                    case "green":
                        result = Microsoft.VisualBasic.Information.RGB(0, 51, 0);
                        break;
                    case "orange":
                        result = Microsoft.VisualBasic.Information.RGB(153, 0, 0);
                        break;
                    case "teal":
                        result = Microsoft.VisualBasic.Information.RGB(0, 51, 102);
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
                BusinessClasses.CalendarTemplate template = BusinessClasses.OutputManager.Instance.CalendarTemplates.Where(x => x.IsLarge == this.ShowBigDate && x.HasLogo == this.ShowLogo && x.Color.ToLower().Equals(this.SlideColor) && x.Month.ToLower().Equals(this.Parent.Date.ToString("MMM-yy").ToLower())).FirstOrDefault();
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
                if (this.ShowLogo && this.Logo != null)
                {
                    result = System.IO.Path.GetTempFileName();
                    this.Logo.Save(result);
                }
                return result;
            }
        }

        public string SlideTitle
        {
            get
            {
                return this.ShowHeader ? this.Header : string.Empty;
            }
        }

        public string MonthText
        {
            get
            {
                string result = string.Empty;
                if (this.ShowMonth)
                    result = this.Parent.Date.ToString("MMMM yyyy");
                return result;
            }
        }

        public string BackgroundSheetName
        {
            get
            {
                string result = string.Empty;
                result = this.Parent.Date.ToString("MMM").ToLower() + (this.ShowBigDate ? "1" : "2");
                return result;
            }
        }

        public string Comments
        {
            get
            {
                string result = string.Empty;
                if (this.ShowCustomComment)
                    result = this.CustomComment;
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
                if (this.ShowLegend)
                {
                    result = string.Join(";  ", this.Legend.Select(x => x.StringRepresentation));
                }
                return result;
            }
        }

        public string[] DayOutput
        {
            get
            {
                return this.Parent.Days.Select(x => x.Summary).ToArray();
            }
        }

        public string[] DayLogoPaths
        {
            get
            {
                return _dayLogosPaths.ToArray();
            }
        }

        public float FontSize
        {
            get
            {
                return 7;
            }
        }
        #endregion
        #endregion

        public CalendarOutputData(CalendarMonth parent)
        {
            this.Parent = parent;
            this.Notes = new List<CalendarNote>();

            #region Basic
            this.ShowMonth = true;
            this.ShowHeader = true;
            this.ShowBusinessName = true;
            this.ShowDecisionMaker = true;
            this.Header = string.Empty;
            this.ApplyForAllBasic = true;
            #endregion

            #region Cost
            this.ShowPrintTotalCostManual = false;
            this.ShowPrintTotalCostCalculated = false;
            this.ShowDigitalTotalCost = false;
            this.ShowTVTotalCost = false;
            this.ApplyForAlCost = true;
            #endregion

            #region Notes
            this.ShowCustomComment = false;
            this.ApplyForAllCustomComment = true;

            this.ShowActiveDays = false;
            this.ShowPrintAdsNumber = false;
            this.ShowImpressions = false;
            this.ShowDigitalCPM = false;
            this.ApplyForAllOtherNumbers = true;
            #endregion

            #region Legend
            this.ShowLegend = false;
            this.Legend = new List<CalendarLegend>();
            this.ApplyForAllLegend = true;
            #endregion

            #region Style
            this.SlideColor = "gray";
            this.ApplyForAllThemeColor = true;

            this.ShowLogo = true;
            string defaultLogoPath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.BigImageFolder.FullName, ConfigurationClasses.SettingsManager.DefaultBigLogoFileName);
            if (File.Exists(defaultLogoPath))
                this.Logo = new Bitmap(defaultLogoPath);
            this.ApplyForAllLogo = true;

            this.ShowBigDate = true;
            #endregion
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));

            #region Basic
            result.AppendLine(@"<ShowMonth>" + this.ShowMonth.ToString() + @"</ShowMonth>");
            result.AppendLine(@"<ShowHeader>" + this.ShowHeader.ToString() + @"</ShowHeader>");
            result.AppendLine(@"<ShowBusinessName>" + this.ShowBusinessName.ToString() + @"</ShowBusinessName>");
            result.AppendLine(@"<ShowDecisionMaker>" + this.ShowDecisionMaker.ToString() + @"</ShowDecisionMaker>");
            result.AppendLine(@"<Header>" + this.Header.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Header>");
            result.AppendLine(@"<BusinessName>" + _businessName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</BusinessName>");
            result.AppendLine(@"<DecisionMaker>" + _decisionMaker.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DecisionMaker>");
            result.AppendLine(@"<ApplyForAllBasic>" + this.ApplyForAllBasic.ToString() + @"</ApplyForAllBasic>");
            #endregion

            #region Cost
            if (this.PrintTotalCost.HasValue)
                result.AppendLine(@"<PrintTotalCost>" + this.PrintTotalCost.Value.ToString() + @"</PrintTotalCost>");
            result.AppendLine(@"<ShowPrintTotalCostManual>" + this.ShowPrintTotalCostManual.ToString() + @"</ShowPrintTotalCostManual>");
            result.AppendLine(@"<ShowPrintTotalCostCalculated>" + this.ShowPrintTotalCostCalculated.ToString() + @"</ShowPrintTotalCostCalculated>");
            result.AppendLine(@"<ShowDigitalTotalCost>" + this.ShowDigitalTotalCost.ToString() + @"</ShowDigitalTotalCost>");
            if (this.DigitalTotalCost.HasValue)
                result.AppendLine(@"<DigitalTotalCost>" + this.DigitalTotalCost.Value.ToString() + @"</DigitalTotalCost>");
            result.AppendLine(@"<ShowTVTotalCost>" + this.ShowTVTotalCost.ToString() + @"</ShowTVTotalCost>");
            if (this.TVTotalCost.HasValue)
                result.AppendLine(@"<TVTotalCost>" + this.TVTotalCost.Value.ToString() + @"</TVTotalCost>");
            result.AppendLine(@"<ApplyForAllCost>" + this.ApplyForAlCost.ToString() + @"</ApplyForAllCost>");
            #endregion

            #region Notes
            result.AppendLine(@"<ShowCustomComment>" + this.ShowCustomComment.ToString() + @"</ShowCustomComment>");
            if (!string.IsNullOrEmpty(this.CustomComment))
                result.AppendLine(@"<CustomComment>" + this.CustomComment.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</CustomComment>");
            result.AppendLine(@"<ApplyForAllCustomComment>" + this.ApplyForAllCustomComment.ToString() + @"</ApplyForAllCustomComment>");

            if (_activeDays.HasValue)
                result.AppendLine(@"<ActiveDays>" + _activeDays.Value.ToString() + @"</ActiveDays>");
            if (_printAdsNumber.HasValue)
                result.AppendLine(@"<PrintAdsNumber>" + _printAdsNumber.Value.ToString() + @"</PrintAdsNumber>");
            result.AppendLine(@"<ShowActiveDays>" + this.ShowActiveDays.ToString() + @"</ShowActiveDays>");
            result.AppendLine(@"<ShowPrintAdsNumber>" + this.ShowPrintAdsNumber.ToString() + @"</ShowPrintAdsNumber>");
            result.AppendLine(@"<ShowImpressions>" + this.ShowImpressions.ToString() + @"</ShowImpressions>");
            result.AppendLine(@"<ShowDigitalCPM>" + this.ShowDigitalCPM.ToString() + @"</ShowDigitalCPM>");
            result.AppendLine(@"<ApplyForAllOtherNumbers>" + this.ApplyForAllOtherNumbers.ToString() + @"</ApplyForAllOtherNumbers>");
            if (this.Impressions.HasValue)
                result.AppendLine(@"<Impressions>" + this.Impressions.Value.ToString() + @"</Impressions>");
            if (this.DigitalCPM.HasValue)
                result.AppendLine(@"<DigitalCPM>" + this.DigitalCPM.Value.ToString() + @"</DigitalCPM>");
            #endregion

            #region Legend
            result.AppendLine(@"<ShowLegend>" + this.ShowLegend.ToString() + @"</ShowLegend>");
            result.AppendLine(@"<Legends>");
            foreach (CalendarLegend legend in this.Legend)
                result.AppendLine(@"<Legend>" + legend.Serialize() + @"</Legend>");
            result.AppendLine(@"</Legends>");
            result.AppendLine(@"<ApplyForAllLegend>" + this.ApplyForAllLegend.ToString() + @"</ApplyForAllLegend>");
            #endregion

            #region Style
            result.AppendLine(@"<SlideColor>" + this.SlideColor.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideColor>");
            result.AppendLine(@"<ApplyForAllThemeColor>" + this.ApplyForAllThemeColor.ToString() + @"</ApplyForAllThemeColor>");

            result.AppendLine(@"<ShowLogo>" + this.ShowLogo.ToString() + @"</ShowLogo>");
            if (this.Logo != null)
                result.AppendLine(@"<Logo>" + Convert.ToBase64String((byte[])converter.ConvertTo(this.Logo, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Logo>");
            result.AppendLine(@"<ApplyForAllLogo>" + this.ApplyForAllLogo.ToString() + @"</ApplyForAllLogo>");

            result.AppendLine(@"<ShowBigDate>" + this.ShowBigDate + @"</ShowBigDate>");
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
                            this.ShowMonth = tempBool;
                        break;
                    case "ShowHeader":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowHeader = tempBool;
                        break;
                    case "ShowBusinessName":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowBusinessName = tempBool;
                        break;
                    case "ShowDecisionMaker":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDecisionMaker = tempBool;
                        break;
                    case "Header":
                        this.Header = childNode.InnerText;
                        break;
                    case "BusinessName":
                        _businessName = childNode.InnerText;
                        break;
                    case "DecisionMaker":
                        _decisionMaker = childNode.InnerText;
                        break;
                    case "ApplyForAllBasic":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ApplyForAllBasic = tempBool;
                        break;
                    #endregion

                    #region Cost
                    case "PrintTotalCost":
                        if (double.TryParse(childNode.InnerText, out tempDouble))
                            this.PrintTotalCost = tempDouble;
                        break;
                    case "ShowPrintTotalCostManual":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPrintTotalCostManual = tempBool;
                        break;
                    case "ShowPrintTotalCostCalculated":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPrintTotalCostCalculated = tempBool;
                        break;
                    case "ShowDigitalTotalCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDigitalTotalCost = tempBool;
                        break;
                    case "DigitalTotalCost":
                        if (double.TryParse(childNode.InnerText, out tempDouble))
                            this.DigitalTotalCost = tempDouble;
                        break;
                    case "ShowTVTotalCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTVTotalCost = tempBool;
                        break;
                    case "TVTotalCost":
                        if (double.TryParse(childNode.InnerText, out tempDouble))
                            this.TVTotalCost = tempDouble;
                        break;
                    case "ApplyForAllCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ApplyForAlCost = tempBool;
                        break;
                    #endregion

                    #region Notes
                    case "ShowCustomComment":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowCustomComment = tempBool;
                        break;
                    case "CustomComment":
                        this.CustomComment = childNode.InnerText;
                        break;
                    case "ApplyForAllCustomComment":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ApplyForAllCustomComment = tempBool;
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
                            this.ShowActiveDays = tempBool;
                        break;
                    case "ShowPrintAdsNumber":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPrintAdsNumber = tempBool;
                        break;
                    case "ShowImpressions":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowImpressions = tempBool;
                        break;
                    case "ShowDigitalCPM":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDigitalCPM = tempBool;
                        break;
                    case "Impressions":
                        if (double.TryParse(childNode.InnerText, out tempDouble))
                            this.Impressions = tempDouble;
                        break;
                    case "DigitalCPM":
                        if (double.TryParse(childNode.InnerText, out tempDouble))
                            this.DigitalCPM = tempDouble;
                        break;
                    case "ApplyForAllOtherNumbers":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ApplyForAllOtherNumbers = tempBool;
                        break;
                    #endregion

                    #region Legend
                    case "ShowLegend":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowLegend = tempBool;
                        break;
                    case "Legends":
                        this.Legend.Clear();
                        foreach (XmlNode legendNode in childNode.ChildNodes)
                        {
                            CalendarLegend legend = new CalendarLegend();
                            legend.Deserialize(legendNode);
                            this.Legend.Add(legend);
                        }
                        break;
                    case "ApplyForAllLegend":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ApplyForAllLegend = tempBool;
                        break;
                    #endregion

                    #region Style
                    case "SlideColor":
                        this.SlideColor = childNode.InnerText;
                        break;
                    case "ApplyForAllThemeColor":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ApplyForAllThemeColor = tempBool;
                        break;

                    case "ShowLogo":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowLogo = tempBool;
                        break;
                    case "Logo":
                        if (string.IsNullOrEmpty(childNode.InnerText))
                            this.Logo = null;
                        else
                            this.Logo = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
                        break;
                    case "ApplyForAllLogo":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ApplyForAllLogo = tempBool;
                        break;

                    case "ShowBigDate":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowBigDate = tempBool;
                        break;
                    #endregion
                }
            }
        }

        public void UpdateLegend()
        {
            List<CalendarLegend> _newLegends = new List<CalendarLegend>();
            List<CalendarLegend> _legendsFromDays = new List<CalendarLegend>();

            CalendarLegend legend = new CalendarLegend();
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

            foreach (CalendarDay day in this.Parent.Days)
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
            _newLegends.AddRange(this.Legend.Where(x => _legendsFromDays.Select(y => y.Description).Contains(x.Description)));
            _newLegends.AddRange(_legendsFromDays.Where(x => !this.Legend.Select(y => y.Description).Contains(x.Description)));
            this.Legend.Clear();
            this.Legend.AddRange(_newLegends);
        }

        private string[] GetTotalTags()
        {
            List<string> tagValues = new List<string>();
            if (this.ShowPrintTotalCostCalculated | this.ShowPrintTotalCostManual)
                tagValues.Add("Newspaper Investment: " + (this.ShowPrintTotalCostCalculated ? this.PrintTotalCostCalculated.ToString("$#,###.00") : (this.PrintTotalCost.HasValue ? this.PrintTotalCost.Value.ToString("$#,###.00") : string.Empty)));
            if (this.ShowDigitalTotalCost && this.DigitalTotalCost.HasValue)
                tagValues.Add("Digital Investment: " + this.DigitalTotalCost.Value.ToString("$#,###.00"));
            if (this.ShowTVTotalCost && this.TVTotalCost.HasValue)
                tagValues.Add("TV Investment: " + this.TVTotalCost.Value.ToString("$#,###.00"));
            if (this.ShowActiveDays)
                tagValues.Add("# of Active Days: " + this.ActiveDays.ToString("#,##0"));
            if (this.ShowPrintAdsNumber)
                tagValues.Add("# of Newspaper Ads: " + this.PrintAdsNumber.ToString("#,##0"));
            if (this.ShowImpressions && this.Impressions.HasValue)
                tagValues.Add("Monthly Imressions: " + this.Impressions.Value.ToString("#,##0.0"));
            if (this.ShowDigitalCPM && this.DigitalCPM.HasValue)
                tagValues.Add("Digital CPM: " + this.DigitalCPM.Value.ToString("$#,###.0"));
            return tagValues.ToArray();
        }

        public void PrepareDayLogoPaths()
        {
            _dayLogosPaths.Clear();
            foreach (CalendarDay day in this.Parent.Days)
            {
                if (day.Logo.TinyImage != null)
                {
                    string filePath = string.Empty;
                    filePath = System.IO.Path.GetTempFileName();
                    day.Logo.TinyImage.Save(filePath);
                    _dayLogosPaths.Add(filePath);
                }
                else
                    _dayLogosPaths.Add(string.Empty);
            }
        }

        public void PrepareNotes()
        {
            this.Notes.Clear();
            this.Notes.AddRange(this.Parent.Parent.Notes.Where(x => x.StartDay >= this.Parent.Date && x.FinishDay <= this.Parent.Date));
        }
    }

    public class CalendarLegend
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public bool Visible { get; set; }

        public string StringRepresentation
        {
            get
            {
                return this.Code + " = " + this.Description;
            }
        }

        public CalendarLegend()
        {
            this.Code = string.Empty;
            this.Description = string.Empty;
            this.Visible = true;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<Code>" + this.Code.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Code>");
            result.AppendLine(@"<Description>" + this.Description.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Description>");
            result.AppendLine(@"<Visible>" + this.Visible.ToString() + @"</Visible>");
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
                        this.Code = childNode.InnerText;
                        break;
                    case "Description":
                        this.Description = childNode.InnerText;
                        break;
                    case "Visible":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Visible = tempBool;
                        break;
                }
            }
        }

        public CalendarLegend Clone()
        {
            CalendarLegend result = new CalendarLegend();
            result.Code = this.Code;
            result.Description = this.Description;
            result.Visible = this.Visible;
            return result;
        }
    }

    public class ImageSource
    {
        private CalendarDay _parent = null;
        public Image BigImage { get; set; }
        public Image SmallImage { get; set; }
        public Image TinyImage { get; set; }
        public Image XtraTinyImage { get; set; }

        public DateTime? Day
        {
            get
            {
                return _parent != null ? (DateTime?)_parent.Date : null;
            }
        }

        public bool ContainsData
        {
            get
            {
                return this.XtraTinyImage != null;
            }
        }

        public ImageSource(CalendarDay parent)
        {
            _parent = parent;
        }

        public string Serialize()
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
            StringBuilder result = new StringBuilder();
            result.Append("<BigImage>" + Convert.ToBase64String((byte[])converter.ConvertTo(this.BigImage, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</BigImage>");
            result.Append("<SmallImage>" + Convert.ToBase64String((byte[])converter.ConvertTo(this.SmallImage, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</SmallImage>");
            result.Append("<TinyImage>" + Convert.ToBase64String((byte[])converter.ConvertTo(this.TinyImage, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</TinyImage>");
            result.Append("<XtraTinyImage>" + Convert.ToBase64String((byte[])converter.ConvertTo(this.XtraTinyImage, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</XtraTinyImage>");
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
                            this.BigImage = null;
                        else
                            this.BigImage = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
                        break;
                    case "SmallImage":
                        if (string.IsNullOrEmpty(childNode.InnerText))
                            this.SmallImage = null;
                        else
                            this.SmallImage = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
                        break;
                    case "TinyImage":
                        if (string.IsNullOrEmpty(childNode.InnerText))
                            this.TinyImage = null;
                        else
                            this.TinyImage = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
                        break;
                    case "XtraTinyImage":
                        if (string.IsNullOrEmpty(childNode.InnerText))
                            this.TinyImage = null;
                        else
                            this.XtraTinyImage = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
                        break;
                }
            }
        }

        public ImageSource Clone(CalendarDay newParent)
        {
            ImageSource result = new ImageSource(newParent);
            result.BigImage = this.BigImage;
            result.SmallImage = this.SmallImage;
            result.TinyImage = this.TinyImage;
            result.XtraTinyImage = this.XtraTinyImage;
            return result;
        }
    }
}
