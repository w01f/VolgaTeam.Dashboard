using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace RadioScheduleBuilder.BusinessClasses
{
    public enum SalesStrategies
    {
        InPerson = 0,
        Email,
        Fax
    }

    public enum SpotType
    {
        Week = 0,
        Month
    }

    public class ScheduleManager
    {
        private static ScheduleManager _instance = new ScheduleManager();
        private Schedule _currentSchedule;

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

        public void OpenSchedule(string scheduleName, bool create)
        {
            string scheduleFilePath = GetScheduleFileName(scheduleName);
            if (create && File.Exists(scheduleFilePath))
                if (AppManager.ShowWarningQuestion(string.Format("An older Schedule is already saved with this same file name.\nDo you want to replace this file with a newer schedule?", scheduleName)) == DialogResult.Yes)
                    File.Delete(scheduleFilePath);
            _currentSchedule = new Schedule(scheduleFilePath);
        }

        public void OpenSchedule(string scheduleFilePath)
        {
            _currentSchedule = new Schedule(scheduleFilePath);
        }

        public string GetScheduleFileName(string scheduleName)
        {
            return Path.Combine(ConfigurationClasses.SettingsManager.Instance.SaveFolder, scheduleName + ".xml");
        }

        public Schedule GetLocalSchedule()
        {
            return new Schedule(_currentSchedule.ScheduleFile.FullName);
        }

        public void SaveSchedule(Schedule localSchedule, bool quickSave, Control sender)
        {
            localSchedule.Save();
            _currentSchedule = localSchedule;
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
                thread.Start();

                form.Show();

                while (thread.IsAlive)
                    System.Windows.Forms.Application.DoEvents();
                form.Close();
            }
        }

        public ShortSchedule[] GetShortScheduleList(DirectoryInfo rootFolder)
        {
            List<ShortSchedule> scheduleList = new List<ShortSchedule>();
            foreach (var file in rootFolder.GetFiles("*.xml"))
            {
                ShortSchedule schedule = new ShortSchedule(file);
                if (!string.IsNullOrEmpty(schedule.BusinessName))
                    scheduleList.Add(schedule);
            }
            return scheduleList.ToArray();
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
        private FileInfo _scheduleFile;

        public string BusinessName { get; set; }
        public string Status { get; set; }

        public string ShortFileName
        {
            get
            {
                return _scheduleFile.Name.Replace(_scheduleFile.Extension, "");
            }
        }

        public string FullFileName
        {
            get
            {
                return _scheduleFile.FullName;
            }
        }

        public DateTime LastModifiedDate
        {
            get
            {
                return _scheduleFile.LastWriteTime;
            }
        }

        public ShortSchedule(FileInfo file)
        {
            this.BusinessName = string.Empty;
            this.Status = ListManager.Instance.Statuses.FirstOrDefault();
            _scheduleFile = file;
            Load();
        }

        private void Load()
        {
            XmlNode node;
            if (_scheduleFile.Exists)
            {
                XmlDocument document = new XmlDocument();
                document.Load(_scheduleFile.FullName);

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
            if (_scheduleFile.Exists)
            {
                try
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(_scheduleFile.FullName);

                    node = document.SelectSingleNode(@"/Schedule/Status");
                    if (node != null)
                        node.InnerText = this.Status;
                    else
                    {
                        node = document.SelectSingleNode(@"/Schedule");
                        if (node != null)
                            node.InnerXml += (@"<Status>" + (this.Status != null ? this.Status.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + @"</Status>");
                    }
                    document.Save(_scheduleFile.FullName);
                }
                catch
                {
                }
            }
        }
    }

    public class Schedule
    {
        private FileInfo _scheduleFile { get; set; }
        public string BusinessName { get; set; }
        public string DecisionMaker { get; set; }
        public string ClientType { get; set; }
        public string Status { get; set; }
        public SalesStrategies SalesStrategy { get; set; }
        public DateTime? PresentationDate { get; set; }
        public DateTime? FlightDateStart { get; set; }
        public DateTime? FlightDateEnd { get; set; }
        public bool UseDemo { get; set; }
        public bool ImportDemo { get; set; }
        public string Demo { get; set; }
        public bool UseSource { get; set; }
        public string Source { get; set; }
        public bool RatingAsCPP { get; set; }

        public WeeklySection WeeklySchedule { get; set; }
        public MonthlySection MonthlySchedule { get; set; }

        public List<Daypart> Dayparts { get; private set; }
        public List<Station> Stations { get; private set; }

        public string Name
        {
            get
            {
                return _scheduleFile.Name.Replace(_scheduleFile.Extension, "");
            }
            set
            {
                _scheduleFile = new FileInfo(Path.Combine(_scheduleFile.Directory.FullName, value + ".xml"));
            }
        }

        public FileInfo ScheduleFile
        {
            get
            {
                return _scheduleFile;
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
            this.UseDemo = false;
            this.ImportDemo = false;
            this.Demo = string.Empty;
            this.RatingAsCPP = false;
            this.WeeklySchedule = new WeeklySection(this);
            this.MonthlySchedule = new MonthlySection(this);

            this.Dayparts = new List<Daypart>();
            this.Stations = new List<Station>();

            _scheduleFile = new FileInfo(fileName);
            if (!File.Exists(fileName))
            {
                StringBuilder xml = new StringBuilder();
                xml.AppendLine(@"<Schedule>");
                xml.AppendLine(@"<Status>" + (this.Status != null ? this.Status.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + @"</Status>");
                xml.AppendLine(@"</Schedule>");
                using (StreamWriter sw = new StreamWriter(_scheduleFile.FullName, false))
                {
                    sw.Write(xml);
                    sw.Flush();
                }
                _scheduleFile = new FileInfo(fileName);
            }
            else
                Load();

            this.Dayparts.AddRange(ListManager.Instance.Dayparts.Where(x => !this.Dayparts.Select(y => y.Name).Contains(x.Name)));
            this.Stations.AddRange(ListManager.Instance.Stations.Where(x => !this.Stations.Select(y => y.Name).Contains(x.Name)));
        }

        private void Load()
        {
            int tempInt;
            bool tempBool;
            DateTime tempDateTime;

            XmlNode node;
            if (_scheduleFile.Exists)
            {
                XmlDocument document = new XmlDocument();
                document.Load(_scheduleFile.FullName);

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
                        this.SalesStrategy = (SalesStrategies)tempInt;

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

                node = document.SelectSingleNode(@"/Schedule/UseDemo");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.UseDemo = tempBool;

                node = document.SelectSingleNode(@"/Schedule/ImportDemo");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.ImportDemo = tempBool;

                node = document.SelectSingleNode(@"/Schedule/RatingAsCPP");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.RatingAsCPP = tempBool;

                node = document.SelectSingleNode(@"/Schedule/Demo");
                if (node != null)
                    this.Demo = node.InnerText;

                node = document.SelectSingleNode(@"/Schedule/UseSource");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.UseSource = tempBool;

                node = document.SelectSingleNode(@"/Schedule/Source");
                if (node != null)
                    this.Source = node.InnerText;

                node = document.SelectSingleNode(@"/Schedule/WeeklySection");
                if (node != null)
                    this.WeeklySchedule.Deserialize(node);

                node = document.SelectSingleNode(@"/Schedule/MonthlySection");
                if (node != null)
                    this.MonthlySchedule.Deserialize(node);

                node = document.SelectSingleNode(@"/Schedule/Dayparts");
                if (node != null)
                {
                    this.Dayparts.Clear();
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        Daypart daypart = new Daypart();
                        daypart.Deserialize(childNode);
                        this.Dayparts.Add(daypart);
                    }
                }

                node = document.SelectSingleNode(@"/Schedule/Stations");
                if (node != null)
                {
                    this.Stations.Clear();
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        Station station = new Station();
                        station.Deserialize(childNode);
                        this.Stations.Add(station);
                    }
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
            xml.AppendLine(@"<UseDemo>" + this.UseDemo.ToString() + @"</UseDemo>");
            xml.AppendLine(@"<ImportDemo>" + this.ImportDemo.ToString() + @"</ImportDemo>");
            xml.AppendLine(@"<RatingAsCPP>" + this.RatingAsCPP.ToString() + @"</RatingAsCPP>");
            xml.AppendLine(@"<Demo>" + this.Demo.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Demo>");
            xml.AppendLine(@"<UseSource>" + this.UseSource.ToString() + @"</UseSource>");
            xml.AppendLine(@"<Source>" + (!string.IsNullOrEmpty(this.Source) ? this.Source.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + @"</Source>");
            xml.AppendLine(@"<WeeklySection>" + this.WeeklySchedule.Serialize() + @"</WeeklySection>");
            xml.AppendLine(@"<MonthlySection>" + this.MonthlySchedule.Serialize() + @"</MonthlySection>");

            xml.AppendLine(@"<Dayparts>");
            foreach (Daypart daypart in this.Dayparts)
                xml.AppendLine(daypart.Serialize());
            xml.AppendLine(@"</Dayparts>");

            xml.AppendLine(@"<Stations>");
            foreach (Station station in this.Stations)
                xml.AppendLine(station.Serialize());
            xml.AppendLine(@"</Stations>");

            xml.AppendLine(@"</Schedule>");

            using (StreamWriter sw = new StreamWriter(_scheduleFile.FullName, false))
            {
                sw.Write(xml);
                sw.Flush();
            }
        }
    }

    public class ScheduleSection
    {
        public const string ProgramDatasetName = "Schedule";
        public const string ProgramDataTableName = "Programs";
        public const string ProgramIndexDataColumnName = "Index";
        public const string ProgramStationDataColumnName = "Station";
        public const string ProgramNameDataColumnName = "Name";
        public const string ProgramDayDataColumnName = "Day";
        public const string ProgramDaypartDataColumnName = "Daypart";
        public const string ProgramTimeDataColumnName = "Time";
        public const string ProgramLengthDataColumnName = "Length";
        public const string ProgramRateDataColumnName = "Rate";
        public const string ProgramRatingDataColumnName = "Rating";
        public const string ProgramCPPDataColumnName = "CPP";
        public const string ProgramGRPDataColumnName = "GRP";
        public const string ProgramSpotDataColumnNamePrefix = "Spot";
        public const string ProgramTotalSpotDataColumnName = "TotalSpts";
        public const string ProgramCostDataColumnName = "Cost";
        public const string ProgramTotalCPPDataColumnName = "TotalCPP";

        public Schedule Parent { get; private set; }
        public List<Program> Programs { get; set; }
        public SpotType SpotType { get; set; }

        public DataSet DataSource { get; private set; }

        public event EventHandler<EventArgs> DataChanged;

        #region Options
        public bool ShowRate { get; set; }
        public bool ShowRating { get; set; }
        public bool ShowTime { get; set; }
        public bool ShowDay { get; set; }
        public bool ShowDaypart { get; set; }
        public bool ShowStation { get; set; }
        public bool ShowLenght { get; set; }
        public bool ShowCPP { get; set; }
        public bool ShowGRP { get; set; }
        public bool ShowSpots { get; set; }
        public bool ShowCost { get; set; }

        public bool ShowTotalPeriods { get; set; }
        public bool ShowTotalSpots { get; set; }
        public bool ShowTotalGRP { get; set; }
        public bool ShowTotalCPP { get; set; }
        public bool ShowAverageRate { get; set; }
        public bool ShowTotalRate { get; set; }
        public bool ShowNetRate { get; set; }
        public bool ShowDiscount { get; set; }
        #endregion

        #region Calculated Properies
        public int TotalPeriods
        {
            get
            {
                return this.Programs.Count > 0 ? this.Programs.FirstOrDefault().Spots.Count : 0;
            }
        }

        public double TotalCPP
        {
            get
            {
                return this.TotalGRP != 0 ? (this.TotalCost / (this.TotalGRP / (this.Parent.RatingAsCPP ? 1 : 1000))) : 0;
            }
        }

        public double TotalGRP
        {
            get
            {
                return this.Programs.Count > 0 ? (this.Programs.Select(x => x.GRP).Sum()) : 0;
            }
        }

        public double AvgRate
        {
            get
            {
                return this.TotalSpots != 0 ? (this.TotalCost / this.TotalSpots) : 0;
            }
        }

        public double TotalCost
        {
            get
            {
                return this.Programs.Count > 0 ? (this.Programs.Select(x => x.Cost).Sum()) : 0;
            }
        }

        public double NetRate
        {
            get
            {
                return this.TotalCost - this.Discount;
            }
        }

        public double Discount
        {
            get
            {
                return this.TotalCost * 0.15;
            }
        }

        public int TotalSpots
        {
            get
            {
                return this.Programs.Count > 0 ? this.Programs.Select(x => x.TotalSpots).Sum() : 0;
            }
        }
        #endregion

        public ScheduleSection(Schedule parent)
        {
            this.Parent = parent;
            this.Programs = new List<Program>();

            #region Options
            this.ShowRate = true;
            this.ShowRating = true;
            this.ShowTime = true;
            this.ShowDaypart = true;
            this.ShowDay = true;
            this.ShowStation = true;
            this.ShowLenght = false;
            this.ShowCPP = false;
            this.ShowGRP = false;
            this.ShowSpots = true;
            this.ShowCost = true;

            this.ShowTotalPeriods = true;
            this.ShowTotalSpots = true;
            this.ShowTotalGRP = true;
            this.ShowTotalCPP = true;
            this.ShowAverageRate = true;
            this.ShowTotalRate = true;
            this.ShowNetRate = false;
            this.ShowDiscount = false;
            #endregion
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            #region Options
            result.AppendLine(@"<ShowRate>" + this.ShowRate.ToString() + @"</ShowRate>");
            result.AppendLine(@"<ShowRating>" + this.ShowRating.ToString() + @"</ShowRating>");
            result.AppendLine(@"<ShowDay>" + this.ShowDay.ToString() + @"</ShowDay>");
            result.AppendLine(@"<ShowTime>" + this.ShowTime.ToString() + @"</ShowTime>");
            result.AppendLine(@"<ShowDaypart>" + this.ShowDaypart.ToString() + @"</ShowDaypart>");
            result.AppendLine(@"<ShowStation>" + this.ShowStation.ToString() + @"</ShowStation>");
            result.AppendLine(@"<ShowLenght>" + this.ShowLenght.ToString() + @"</ShowLenght>");
            result.AppendLine(@"<ShowCPP>" + this.ShowCPP.ToString() + @"</ShowCPP>");
            result.AppendLine(@"<ShowGRP>" + this.ShowGRP.ToString() + @"</ShowGRP>");
            result.AppendLine(@"<ShowSpots>" + this.ShowSpots.ToString() + @"</ShowSpots>");
            result.AppendLine(@"<ShowCost>" + this.ShowCost.ToString() + @"</ShowCost>");
            result.AppendLine(@"<ShowAverageRate>" + this.ShowAverageRate.ToString() + @"</ShowAverageRate>");
            result.AppendLine(@"<ShowDiscount>" + this.ShowDiscount.ToString() + @"</ShowDiscount>");
            result.AppendLine(@"<ShowNetRate>" + this.ShowNetRate.ToString() + @"</ShowNetRate>");
            result.AppendLine(@"<ShowTotalCPP>" + this.ShowTotalCPP.ToString() + @"</ShowTotalCPP>");
            result.AppendLine(@"<ShowTotalGRP>" + this.ShowTotalGRP.ToString() + @"</ShowTotalGRP>");
            result.AppendLine(@"<ShowTotalPeriods>" + this.ShowTotalPeriods.ToString() + @"</ShowTotalPeriods>");
            result.AppendLine(@"<ShowTotalRate>" + this.ShowTotalRate.ToString() + @"</ShowTotalRate>");
            result.AppendLine(@"<ShowTotalSpots>" + this.ShowTotalSpots.ToString() + @"</ShowTotalSpots>");
            #endregion

            result.AppendLine(@"<Programs>");
            foreach (Program program in this.Programs)
                result.AppendLine(program.Serialize());
            result.AppendLine(@"</Programs>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool;

            foreach (XmlNode childNode in node.ChildNodes)
                switch (childNode.Name)
                {
                    case "ShowAverageRate":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAverageRate = tempBool;
                        break;
                    case "ShowCPP":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowCPP = tempBool;
                        break;
                    case "ShowCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowCost = tempBool;
                        break;
                    case "ShowDay":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDay = tempBool;
                        break;
                    case "ShowDaypart":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDaypart = tempBool;
                        break;
                    case "ShowDiscount":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDiscount = tempBool;
                        break;
                    case "ShowGRP":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowGRP = tempBool;
                        break;
                    case "ShowLenght":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowLenght = tempBool;
                        break;
                    case "ShowNetRate":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowNetRate = tempBool;
                        break;
                    case "ShowRate":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowRate = tempBool;
                        break;
                    case "ShowRating":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowRating = tempBool;
                        break;
                    case "ShowStation":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowStation = tempBool;
                        break;
                    case "ShowTime":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTime = tempBool;
                        break;
                    case "ShowSpots":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSpots = tempBool;
                        break;
                    case "ShowTotalCPP":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalCPP = tempBool;
                        break;
                    case "ShowTotalGRP":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalGRP = tempBool;
                        break;
                    case "ShowTotalPeriods":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalPeriods = tempBool;
                        break;
                    case "ShowTotalRate":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalRate = tempBool;
                        break;
                    case "ShowTotalSpots":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalSpots = tempBool;
                        break;
                    case "Programs":
                        foreach (XmlNode programNode in childNode.ChildNodes)
                        {
                            Program program = new Program(this);
                            program.Deserialize(programNode);
                            this.Programs.Add(program);
                        }
                        break;
                }
        }

        public void GenerateDataSource()
        {
            if (this.DataSource != null)
                this.DataSource.Dispose();

            this.DataSource = new DataSet(ProgramDatasetName);

            #region Generate Programs Table
            DataTable table = new DataTable(ProgramDataTableName);

            DataColumn column = new DataColumn(ProgramIndexDataColumnName, typeof(int));
            table.Columns.Add(column);
            column = new DataColumn(ProgramStationDataColumnName, typeof(string));
            table.Columns.Add(column);
            column = new DataColumn(ProgramNameDataColumnName, typeof(string));
            table.Columns.Add(column);
            column = new DataColumn(ProgramDayDataColumnName, typeof(string));
            table.Columns.Add(column);
            column = new DataColumn(ProgramDaypartDataColumnName, typeof(string));
            table.Columns.Add(column);
            column = new DataColumn(ProgramTimeDataColumnName, typeof(string));
            table.Columns.Add(column);
            column = new DataColumn(ProgramLengthDataColumnName, typeof(string));
            table.Columns.Add(column);
            column = new DataColumn(ProgramRateDataColumnName, typeof(double));
            table.Columns.Add(column);
            column = new DataColumn(ProgramRatingDataColumnName, typeof(double));
            table.Columns.Add(column);
            List<string> totalSpotsExpression = new List<string>();

            if (this.Programs.FirstOrDefault() != null)
            {
                int spotIndex = 0;
                foreach (BusinessClasses.Spot spot in this.Programs.FirstOrDefault().Spots)
                {
                    string columnName = ProgramSpotDataColumnNamePrefix + spotIndex.ToString();
                    column = new DataColumn(columnName, typeof(int));
                    column.Caption = spot.ColumnName;
                    totalSpotsExpression.Add(string.Format("ISNULL({0},0)", columnName));
                    table.Columns.Add(column);
                    spotIndex++;
                }
            }

            column = new DataColumn(ProgramTotalSpotDataColumnName, typeof(int));
            if (totalSpotsExpression.Count > 0)
                column.Expression = string.Join(" + ", totalSpotsExpression.ToArray());
            table.Columns.Add(column);

            column = new DataColumn(ProgramGRPDataColumnName, typeof(double));
            string temp = string.Format("ISNULL({0},0) * {1}", new object[] { ProgramRatingDataColumnName, ProgramTotalSpotDataColumnName });
            column.Expression = temp;
            table.Columns.Add(column);

            column = new DataColumn(ProgramCostDataColumnName, typeof(double));
            temp = string.Format("ISNULL({0},0) * {1}", new object[] { ProgramRateDataColumnName, ProgramTotalSpotDataColumnName });
            column.Expression = temp;
            table.Columns.Add(column);

            column = new DataColumn(ProgramCPPDataColumnName, typeof(double));
            temp = string.Format("IIF(ISNULL({0},0) <> 0, (ISNULL({1},0)/(ISNULL({2},0)/{3})), 0)", new object[] { ProgramRatingDataColumnName, ProgramRateDataColumnName, ProgramRatingDataColumnName, (this.Parent.RatingAsCPP ? "1" : "1000") });
            column.Expression = temp;
            table.Columns.Add(column);

            column = new DataColumn(ProgramTotalCPPDataColumnName, typeof(double));
            temp = string.Format("Sum({0})/(Sum({1})/{2})", new object[] { ProgramCostDataColumnName, ProgramGRPDataColumnName, (this.Parent.RatingAsCPP ? "1" : "1000") });
            column.Expression = temp;
            table.Columns.Add(column);

            #region Fill Programs Data
            foreach (BusinessClasses.Program program in this.Programs)
            {
                DataRow row = table.NewRow();
                row.BeginEdit();
                row[ProgramIndexDataColumnName] = program.Index.ToString();
                row[ProgramNameDataColumnName] = program.Name;
                row[ProgramStationDataColumnName] = program.Station;
                row[ProgramDayDataColumnName] = program.Day;
                row[ProgramDaypartDataColumnName] = program.Daypart;
                row[ProgramTimeDataColumnName] = program.Time;
                row[ProgramLengthDataColumnName] = program.Length;
                if (program.Rate.HasValue)
                    row[ProgramRateDataColumnName] = program.Rate;
                else
                    row[ProgramRateDataColumnName] = DBNull.Value;
                if (program.Rating.HasValue)
                    row[ProgramRatingDataColumnName] = program.Rating;
                else
                    row[ProgramRatingDataColumnName] = DBNull.Value;
                for (int i = 0; i < program.Spots.Count; i++)
                {
                    if (program.Spots[i].Count.HasValue)
                        row[ProgramSpotDataColumnNamePrefix + i.ToString()] = program.Spots[i].Count;
                    else
                        row[ProgramSpotDataColumnNamePrefix + i.ToString()] = DBNull.Value;
                }
                row.EndEdit();
                table.Rows.Add(row);
            }
            #endregion

            table.RowChanged += new DataRowChangeEventHandler((sender, e) =>
            {
                UpdateProgramsFromDataSource(e.Row);
            });

            this.DataSource.Tables.Add(table);
            #endregion
        }

        private void UpdateProgramsFromDataSource(DataRow row)
        {
            int tempInt = 0;
            double tempDouble = 0;
            string temp = string.Empty;

            int index = -1;
            temp = row[ProgramIndexDataColumnName] != DBNull.Value ? row[ProgramIndexDataColumnName].ToString() : string.Empty;
            if (int.TryParse(temp, out tempInt))
                index = tempInt;
            Program program = this.Programs.Where(x => x.Index == index).FirstOrDefault();
            if (program != null)
            {
                temp = row[ProgramNameDataColumnName] != DBNull.Value ? row[ProgramNameDataColumnName].ToString() : string.Empty;
                program.Name = temp;
                temp = row[ProgramStationDataColumnName] != DBNull.Value ? row[ProgramStationDataColumnName].ToString() : string.Empty;
                program.Station = temp;
                temp = row[ProgramDayDataColumnName] != DBNull.Value ? row[ProgramDayDataColumnName].ToString() : string.Empty;
                program.Day = temp;
                temp = row[ProgramDaypartDataColumnName] != DBNull.Value ? row[ProgramDaypartDataColumnName].ToString() : string.Empty;
                program.Daypart = temp;
                temp = row[ProgramTimeDataColumnName] != DBNull.Value ? row[ProgramTimeDataColumnName].ToString() : string.Empty;
                program.Time = temp;
                temp = row[ProgramLengthDataColumnName] != DBNull.Value ? row[ProgramLengthDataColumnName].ToString() : string.Empty;
                program.Length = temp;
                temp = row[ProgramRateDataColumnName] != DBNull.Value ? row[ProgramRateDataColumnName].ToString() : string.Empty;
                if (double.TryParse(temp, out tempDouble))
                    program.Rate = tempDouble;
                else
                    program.Rate = null;
                temp = row[ProgramRatingDataColumnName] != DBNull.Value ? row[ProgramRatingDataColumnName].ToString() : string.Empty;
                if (double.TryParse(temp, out tempDouble))
                    program.Rating = tempDouble;
                else
                    program.Rating = null;
                for (int i = 0; i < program.Spots.Count; i++)
                {
                    temp = row[ProgramSpotDataColumnNamePrefix + i.ToString()] != DBNull.Value ? row[ProgramSpotDataColumnNamePrefix + i.ToString()].ToString() : string.Empty;
                    if (int.TryParse(temp, out tempInt))
                        program.Spots[i].Count = tempInt != 0 ? tempInt : (int?)null;
                    else
                        program.Spots[i].Count = null;
                }
            }

            if (this.DataChanged != null)
                this.DataChanged(null, new EventArgs());
        }

        public void AddProgram()
        {
            Program program = new Program(this);
            this.Programs.Add(program);
            program.RebuildSpots();
        }

        public void DeleteProgram(int programIndex)
        {
            if (programIndex >= 0 && programIndex < this.Programs.Count)
            {
                Program program = this.Programs[programIndex];
                this.Programs.Remove(program);
                RebuildProgramIndexes();
            }
        }

        private void RebuildProgramIndexes()
        {
            for (int i = 0; i < this.Programs.Count; i++)
                this.Programs[i].Index = i + 1;
        }

        public void RebuildSpots()
        {
            foreach (Program program in this.Programs)
                program.RebuildSpots();
        }
    }

    public class WeeklySection : ScheduleSection
    {
        public WeeklySection(Schedule parent)
            : base(parent)
        {
            this.SpotType = BusinessClasses.SpotType.Week;
        }
    }

    public class MonthlySection : ScheduleSection
    {
        public MonthlySection(Schedule parent)
            : base(parent)
        {
            this.SpotType = BusinessClasses.SpotType.Month;
        }
    }

    public class Program
    {
        private string _name = null;

        #region Basic Properties
        public ScheduleSection Parent { get; set; }
        public int Index { get; set; }
        public string Station { get; set; }
        public string Daypart { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public string Length { get; set; }
        public double? Rate { get; set; }
        public double? Rating { get; set; }
        public List<Spot> Spots { get; set; }
        #endregion

        #region Calculated Properties
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                string oldValue = _name;
                _name = value;
                if (string.IsNullOrEmpty(oldValue))
                    ApplyDefaultValues();
            }
        }

        public double CPP
        {
            get
            {
                double result = 0;
                if (this.Rate.HasValue && this.Rating.HasValue)
                    if (this.Rating.Value != 0)
                        result = this.Rate.Value / (this.Rating.Value / (this.Parent.Parent.RatingAsCPP ? 1 : 1000));
                return result;
            }
        }
        public double GRP
        {
            get
            {
                return (this.Rating.HasValue ? this.Rating.Value : 0) * this.TotalSpots;
            }
        }

        public double Cost
        {
            get
            {
                return (this.Rate.HasValue ? this.Rate.Value : 0) * this.TotalSpots;
            }
        }

        public int TotalSpots
        {
            get
            {
                return this.Spots.Select(x => x.Count.HasValue ? x.Count.Value : 0).Sum();
            }
        }
        #endregion

        public Program(ScheduleSection parent)
        {
            this.Parent = parent;
            this.Index = this.Parent.Programs.Count + 1;
            this.Station = this.Parent.Parent.Stations.Where(x => x.Available).Count() == 1 ? this.Parent.Parent.Stations.Where(x => x.Available).Select(x => x.Name).FirstOrDefault() : string.Empty;
            this.Daypart = string.Empty;
            this.Day = string.Empty;
            this.Time = string.Empty;
            this.Length = string.Empty;
            this.Spots = new List<Spot>();
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            if (!string.IsNullOrEmpty(_name))
            {
                result.Append(@"<Program ");
                result.Append("Name = \"" + _name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                result.Append("Station = \"" + this.Station.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                result.Append("Daypart = \"" + this.Daypart.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                result.Append("Day = \"" + this.Day.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                result.Append("Time = \"" + this.Time.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                result.Append("Length = \"" + this.Length.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                if (this.Rate.HasValue)
                    result.Append("Rate = \"" + this.Rate.Value + "\" ");
                if (this.Rating.HasValue)
                    result.Append("Rating = \"" + this.Rating.Value + "\" ");
                result.AppendLine(@">");

                result.AppendLine(@"<Spots>");
                foreach (Spot spot in this.Spots)
                    result.AppendLine(@"<Spot>" + spot.Serialize() + @"</Spot>");
                result.AppendLine(@"</Spots>");

                result.AppendLine(@"</Program>");
            }
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            double tempDouble;

            foreach (XmlAttribute programAttribute in node.Attributes)
                switch (programAttribute.Name)
                {
                    case "Name":
                        _name = programAttribute.Value;
                        break;
                    case "Station":
                        this.Station = programAttribute.Value;
                        break;
                    case "Daypart":
                        this.Daypart = programAttribute.Value;
                        break;
                    case "Day":
                        this.Day = programAttribute.Value;
                        break;
                    case "Time":
                        this.Time = programAttribute.Value;
                        break;
                    case "Length":
                        this.Length = programAttribute.Value;
                        break;
                    case "Rate":
                        if (double.TryParse(programAttribute.Value, out tempDouble))
                            this.Rate = tempDouble;
                        break;
                    case "Rating":
                        if (double.TryParse(programAttribute.Value, out tempDouble))
                            this.Rating = tempDouble;
                        break;
                }
            foreach (XmlNode childNode in node.ChildNodes)
                switch (childNode.Name)
                {
                    case "Spots":
                        foreach (XmlNode spotNode in childNode.ChildNodes)
                        {
                            Spot spot = new Spot(this);
                            spot.Deserialize(spotNode);
                            this.Spots.Add(spot);
                        }
                        break;
                }
        }

        public void ApplyDefaultValues()
        {
            SourceProgram[] source = ListManager.Instance.SourcePrograms.Where(x => x.Name.Equals(_name)).ToArray();
            if (source.Length > 0)
            {
                this.Daypart = source[0].Day;
                this.Day = source[0].Day;
                this.Time = source[0].Time;
            }
        }

        public void RebuildSpots()
        {
            this.Spots.Clear();
            if (this.Parent.Parent.FlightDateStart.HasValue && this.Parent.Parent.FlightDateEnd.HasValue)
            {
                DateTime spotDate = this.Parent.Parent.FlightDateStart.Value;
                DateTime endDate = this.Parent.Parent.FlightDateEnd.Value;
                while (spotDate < endDate)
                {
                    Spot spot = new Spot(this);
                    spot.Date = spotDate;
                    spotDate = this.Parent.SpotType == SpotType.Week ? spotDate.AddDays(7) : spotDate.AddMonths(1);
                    this.Spots.Add(spot);
                }
            }
        }
    }

    public class SourceProgram
    {
        public string Name { get; set; }
        public string Station { get; set; }
        public string Daypart { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public List<Demo> Demos { get; set; }

        public SourceProgram()
        {
            this.Name = string.Empty;
            this.Station = string.Empty;
            this.Daypart = string.Empty;
            this.Day = string.Empty;
            this.Time = string.Empty;
            this.Demos = new List<Demo>();
        }
    }

    public class Demo
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public Demo()
        {
            this.Name = string.Empty;
            this.Value = string.Empty;
        }
    }

    public class Daypart
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Available { get; set; }

        public Daypart()
        {
            this.Name = string.Empty;
            this.Code = string.Empty;
            this.Available = true;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.Append(@"<Daypart ");
            result.Append("Name = \"" + this.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            result.Append("Code = \"" + this.Code.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            result.Append("Available = \"" + this.Available.ToString() + "\" ");
            result.AppendLine(@"/>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool;
            foreach (XmlAttribute attribute in node.Attributes)
                switch (attribute.Name)
                {
                    case "Name":
                        this.Name = attribute.Value;
                        break;
                    case "Code":
                        this.Code = attribute.Value;
                        break;
                    case "Available":
                        if (bool.TryParse(attribute.Value, out tempBool))
                            this.Available = tempBool;
                        break;
                }
        }
    }

    public class Station
    {
        public string Name { get; set; }
        public Image Logo { get; set; }
        public bool Available { get; set; }

        public Station()
        {
            this.Name = string.Empty;
            this.Available = true;
        }

        public string Serialize()
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
            StringBuilder result = new StringBuilder();
            result.Append(@"<Station ");
            result.Append("Name = \"" + this.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            result.Append("Logo = \"" + Convert.ToBase64String((byte[])converter.ConvertTo(this.Logo, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            result.Append("Available = \"" + this.Available.ToString() + "\" ");
            result.AppendLine(@"/>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool;
            foreach (XmlAttribute attribute in node.Attributes)
                switch (attribute.Name)
                {
                    case "Name":
                        this.Name = attribute.Value;
                        break;
                    case "Logo":
                        if (string.IsNullOrEmpty(attribute.Value))
                            this.Logo = null;
                        else
                            this.Logo = new Bitmap(new MemoryStream(Convert.FromBase64String(attribute.Value)));
                        break;
                    case "Available":
                        if (bool.TryParse(attribute.Value, out tempBool))
                            this.Available = tempBool;
                        break;
                }
        }
    }

    public class Spot
    {
        private Program _parent = null;

        public DateTime Date { get; set; }
        public int? Count { get; set; }

        public string ColumnName
        {
            get
            {
                switch (_parent.Parent.SpotType)
                {
                    case SpotType.Month:
                        return Spot.GetMonthAbbreviation(this.Date.Month);
                    case SpotType.Week:
                        return string.Format("{0}\n{1}", new object[] { Spot.GetMonthAbbreviation(this.Date.Month), this.Date.Day.ToString("00") });
                    default:
                        return string.Empty;
                }
            }
        }

        public Spot(Program parent)
        {
            _parent = parent;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<Date>" + this.Date.ToString() + @"</Date>");
            if (this.Count.HasValue)
                result.AppendLine(@"<Count>" + this.Count.Value.ToString() + @"</Count>");

            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            DateTime tempDateTime = DateTime.MinValue;
            int tempInt = 0;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Date":
                        if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
                            this.Date = tempDateTime;
                        break;
                    case "Count":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.Count = tempInt;
                        break;
                }
            }
        }

        public static string GetMonthAbbreviation(int monthNumber)
        {
            string result = string.Empty;
            switch (monthNumber)
            {
                case 1:
                    result = "JA";
                    break;
                case 2:
                    result = "FE";
                    break;
                case 3:
                    result = "MR";
                    break;
                case 4:
                    result = "AP";
                    break;
                case 5:
                    result = "MY";
                    break;
                case 6:
                    result = "JN";
                    break;
                case 7:
                    result = "JL";
                    break;
                case 8:
                    result = "AU";
                    break;
                case 9:
                    result = "SE";
                    break;
                case 10:
                    result = "OC";
                    break;
                case 11:
                    result = "NV";
                    break;
                case 12:
                    result = "DE";
                    break;
            }
            return result;
        }
    }
}
