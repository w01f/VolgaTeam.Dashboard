using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace AdScheduleBuilder.BusinessClasses
{
    public enum SalesStrategies
    {
        InPerson = 0,
        Email,
        Fax
    }

    public enum AdPricingStrategies
    {
        StandartPCI = 0,
        FlatModular,
        SharePage
    }

    public enum ColorOptions
    {
        BlackWhite = 0,
        SpotColor,
        FullColor
    }

    public enum ColorPricingType
    {
        CostPerAd = 0,
        PercentOfAdRate,
        ColorIncluded,
        CostPerInch,
        None
    }

    public enum CirculationType
    {
        Daily = 0,
        Sunday
    }

    public class ScheduleManager
    {
        private const string ScheduleStoragePath = @"";
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
        public string AccountNumber { get; set; }
        public string Status { get; set; }
        public SalesStrategies SalesStrategy { get; set; }
        public DateTime PresentationDate { get; set; }
        public DateTime FlightDateStart { get; set; }
        public DateTime FlightDateEnd { get; set; }
        public List<Publication> Publications { get; set; }

        public ConfigurationClasses.ScheduleViewSettings ViewSettings { get; set; }

        private List<DateTime> _scheduleMonths = new List<DateTime>();

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

        public object PresentationDateObject
        {
            get
            {
                if (this.PresentationDate.Equals(DateTime.MaxValue) || this.PresentationDate.Equals(DateTime.MinValue))
                {
                    return null;
                }
                else
                    return this.PresentationDate;
            }
        }

        public object FlightDateStartObject
        {
            get
            {
                if (this.FlightDateStart.Equals(DateTime.MaxValue) || this.FlightDateStart.Equals(DateTime.MinValue))
                {
                    return null;
                }
                else
                    return this.FlightDateStart;
            }
        }

        public object FlightDateEndObject
        {
            get
            {
                if (this.FlightDateEnd.Equals(DateTime.MaxValue) || this.FlightDateEnd.Equals(DateTime.MinValue))
                {
                    return null;
                }
                else
                    return this.FlightDateEnd;
            }
        }

        public DateTime[] ScheduleMonths
        {
            get
            {
                return _scheduleMonths.ToArray();
            }
        }

        public Schedule(string fileName)
        {
            this.ClientType = string.Empty;
            this.AccountNumber = string.Empty;
            this.Status = ListManager.Instance.Statuses.FirstOrDefault();
            this.Publications = new List<Publication>();
            this.ViewSettings = new ConfigurationClasses.ScheduleViewSettings();

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
        }

        private void Load()
        {
            int tempInt;
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

                node = document.SelectSingleNode(@"/Schedule/AccountNumber");
                if (node != null)
                    this.AccountNumber = node.InnerText;

                node = document.SelectSingleNode(@"/Schedule/Status");
                if (node != null)
                    this.Status = node.InnerText;

                node = document.SelectSingleNode(@"/Schedule/SalesStrategy");
                if (node != null)
                {
                    tempInt = 0;
                    int.TryParse(node.InnerText, out tempInt);
                    this.SalesStrategy = (SalesStrategies)tempInt;
                }

                node = document.SelectSingleNode(@"/Schedule/PresentationDate");
                if (node != null)
                {
                    tempDateTime = DateTime.MaxValue;
                    DateTime.TryParse(node.InnerText, out tempDateTime);
                    this.PresentationDate = tempDateTime;
                }

                node = document.SelectSingleNode(@"/Schedule/FlightDateStart");
                if (node != null)
                {
                    tempDateTime = DateTime.MaxValue;
                    DateTime.TryParse(node.InnerText, out tempDateTime);
                    this.FlightDateStart = tempDateTime;
                }

                node = document.SelectSingleNode(@"/Schedule/FlightDateEnd");
                if (node != null)
                {
                    tempDateTime = DateTime.MaxValue;
                    DateTime.TryParse(node.InnerText, out tempDateTime);
                    this.FlightDateEnd = tempDateTime;
                }

                node = document.SelectSingleNode(@"/Schedule/ViewSettings");
                if (node != null)
                {
                    this.ViewSettings.Deserialize(node);
                }

                node = document.SelectSingleNode(@"/Schedule/Publications");
                if (node != null)
                {
                    foreach (XmlNode publicationNode in node.ChildNodes)
                    {
                        Publication publication = new Publication(this);
                        publication.Deserialize(publicationNode);
                        this.Publications.Add(publication);
                    }
                }
            }

            UpdateScheduleMonths();
        }

        public void Save()
        {
            UpdateScheduleMonths();

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

            xml.AppendLine(@"<ClientType>" + this.ClientType.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</ClientType>");
            xml.AppendLine(@"<AccountNumber>" + this.AccountNumber.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</AccountNumber>");
            xml.AppendLine(@"<Status>" + (this.Status != null ? this.Status.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + @"</Status>");
            xml.AppendLine(@"<SalesStrategy>" + (int)this.SalesStrategy + @"</SalesStrategy>");
            xml.AppendLine(@"<PresentationDate>" + this.PresentationDate.ToString() + @"</PresentationDate>");
            xml.AppendLine(@"<FlightDateStart>" + this.FlightDateStart.ToString() + @"</FlightDateStart>");
            xml.AppendLine(@"<FlightDateEnd>" + this.FlightDateEnd.ToString() + @"</FlightDateEnd>");

            xml.AppendLine(@"<ViewSettings>" + this.ViewSettings.Serialize() + @"</ViewSettings>");

            xml.AppendLine(@"<Publications>");
            foreach (Publication publication in this.Publications)
                xml.AppendLine(publication.Serialize());
            xml.AppendLine(@"</Publications>");
            xml.AppendLine(@"</Schedule>");

            using (StreamWriter sw = new StreamWriter(_scheduleFile.FullName, false))
            {
                sw.Write(xml);
                sw.Flush();
            }
        }

        public void AddPublication()
        {
            this.Publications.Add(new Publication(this));
        }

        public void UpPublication(int position)
        {
            if (position > 0)
            {
                this.Publications[position].Index--;
                this.Publications[position - 1].Index++;
                this.Publications.Sort((x, y) => x.Index.CompareTo(y.Index));
            }
        }

        public void DownPublication(int position)
        {
            if (position < this.Publications.Count - 1)
            {
                this.Publications[position].Index++;
                this.Publications[position + 1].Index--;
                this.Publications.Sort((x, y) => x.Index.CompareTo(y.Index));
            }
        }

        public void RebuildPublicationIndexes()
        {
            for (int i = 0; i < this.Publications.Count; i++)
                this.Publications[i].Index = i + 1;
        }

        private void UpdateScheduleMonths()
        {
            DateTime startDate = this.FlightDateStart;
            DateTime endDate = new DateTime(this.FlightDateEnd.Month < 12 ? this.FlightDateEnd.Year : (this.FlightDateEnd.Year + 1), (this.FlightDateEnd.Month < 12 ? this.FlightDateEnd.Month + 1 : 1), 1);
            _scheduleMonths.Clear();
            while (startDate < endDate)
            {
                _scheduleMonths.Add(new DateTime(startDate.Year, startDate.Month, 1));
                startDate = startDate.AddMonths(1);
            }
        }
    }

    public class Publication
    {
        private ColorOptions _colorOptions;
        private ColorPricingType _colorPricing;
        private string _name = null;

        public Schedule Parent { get; set; }

        public Guid UniqueID { get; set; }
        public string Abbreviation { get; set; }
        public Image BigLogo { get; set; }
        public Image SmallLogo { get; set; }
        public Image TinyLogo { get; set; }
        public double? DailyDelivery { get; set; }
        public double? SundayDelivery { get; set; }
        public double? DailyReadership { get; set; }
        public double? SundayReadership { get; set; }
        public double Index { get; set; }
        public AdPricingStrategies AdPricingStrategy { get; set; }
        public List<Insert> Inserts { get; set; }
        public double ColorInchRate { get; set; }
        public string Note { get; set; }

        public SizeOptions SizeOptions { get; set; }

        public ConfigurationClasses.PublicationViewSettings ViewSettings { get; set; }

        #region Available Weekdays
        public bool AllowSundaySelect { get; set; }
        public bool AllowMondaySelect { get; set; }
        public bool AllowTuesdaySelect { get; set; }
        public bool AllowWednesdaySelect { get; set; }
        public bool AllowThursdaySelect { get; set; }
        public bool AllowFridaySelect { get; set; }
        public bool AllowSaturdaySelect { get; set; }
        public List<DateTime> AvailableDays { get; private set; }
        #endregion

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

        public ColorOptions ColorOption
        {
            get
            {
                return _colorOptions;
            }
            set
            {
                _colorOptions = value;
                if (value == ColorOptions.BlackWhite)
                {
                    this.ColorPricing = ColorPricingType.None;
                    this.ColorInchRate = 0;
                    foreach (Insert insert in this.Inserts)
                    {
                        insert.ColorPricing = 0;
                        insert.ColorPricingPercent = 0;
                    }
                }

            }
        }

        public ColorPricingType ColorPricing
        {
            get
            {
                return _colorPricing;
            }
            set
            {
                _colorPricing = value;
                switch (value)
                {
                    case ColorPricingType.CostPerAd:
                    case ColorPricingType.None:
                        foreach (Insert insert in this.Inserts)
                            insert.ColorPricingPercent = 0;
                        this.ColorInchRate = 0;
                        break;
                    case ColorPricingType.PercentOfAdRate:
                        foreach (Insert insert in this.Inserts)
                            insert.ColorPricing = 0;
                        this.ColorInchRate = 0;
                        break;
                    case ColorPricingType.ColorIncluded:
                        foreach (Insert insert in this.Inserts)
                        {
                            insert.ColorPricing = 0;
                            insert.ColorPricingPercent = 0;
                        }
                        this.ColorInchRate = 0;
                        break;
                    case ColorPricingType.CostPerInch:
                        foreach (Insert insert in this.Inserts)
                        {
                            insert.ColorPricing = 0;
                            insert.ColorPricingPercent = 0;
                        }
                        break;
                }
            }
        }

        public double? TotalSquare
        {
            get
            {
                if (this.SizeOptions.Square.HasValue)
                    return this.SizeOptions.Square * this.TotalInserts;
                else
                    return null;
            }
        }

        public double TotalInserts
        {
            get
            {
                return this.Inserts.Count(x => x.DateObject != null);
            }
        }

        public double AvgADRate
        {
            get
            {
                return this.TotalInserts != 0 ? this.Inserts.Where(x => x.DateObject != null).Sum(x => x.ADRate) / this.TotalInserts : 0;
            }
        }

        public double AvgPCIRate
        {
            get
            {
                return this.TotalInserts != 0 ? this.Inserts.Where(x => x.DateObject != null).Sum(x => (x.PCIRate.HasValue ? x.PCIRate.Value : 0)) / this.TotalInserts : 0;
            }
        }

        public double TotalDiscountRate
        {
            get
            {
                return this.Inserts.Where(x => x.DateObject != null).Sum(x => x.DiscountRate);
            }
        }

        public double TotalColorPricingCalculated
        {
            get
            {
                return this.Inserts.Where(x => x.DateObject != null).Sum(x => x.ColorPricingCalculated);
            }
        }

        public double AvgFinalRate
        {
            get
            {
                return this.TotalInserts != 0 ? this.Inserts.Where(x => x.DateObject != null).Sum(x => x.FinalRate) / this.TotalInserts : 0;
            }
        }

        public double TotalFinalRate
        {
            get
            {
                return this.Inserts.Where(x => x.DateObject != null).Sum(x => x.FinalRate);
            }
        }

        public string InsertSuffix
        {
            get
            {
                char[] alpha = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
                int lettersNumbers = ((int)this.Index / 25) + 1;
                string result = string.Empty;
                for (int i = 0; i < lettersNumbers; i++)
                    result += alpha[((int)this.Index - 1) - 25 * i].ToString();
                return result;
            }
        }

        public Publication(Schedule parent)
        {
            this.Parent = parent;
            this.UniqueID = Guid.NewGuid();
            _colorPricing = ColorPricingType.None;
            this.Inserts = new List<Insert>();
            this.Index = this.Parent.Publications.Count + 1;
            this.Note = string.Empty;
            this.ViewSettings = new ConfigurationClasses.PublicationViewSettings();
            this.SizeOptions = new SizeOptions();
            this.AdPricingStrategy = ListManager.Instance.DefaultPricingStrategy;

            this.AllowSundaySelect = true;
            this.AllowMondaySelect = true;
            this.AllowTuesdaySelect = true;
            this.AllowWednesdaySelect = true;
            this.AllowThursdaySelect = true;
            this.AllowFridaySelect = true;
            this.AllowSaturdaySelect = true;
            this.AvailableDays = new List<DateTime>();
        }

        public string Serialize()
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
            StringBuilder xml = new StringBuilder();

            xml.Append(@"<Publication ");
            xml.Append("Name = \"" + this.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("UniqueID = \"" + this.UniqueID.ToString() + "\" ");
            xml.Append("Abbreviation = \"" + this.Abbreviation.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("DailyDelivery = \"" + (this.DailyDelivery ?? 0) + "\" ");
            xml.Append("DailyReadership = \"" + (this.DailyReadership ?? 0) + "\" ");
            xml.Append("SundayDelivery = \"" + (this.SundayDelivery ?? 0) + "\" ");
            xml.Append("SundayReadership = \"" + (this.SundayReadership ?? 0) + "\" ");
            xml.Append("Index = \"" + this.Index + "\" ");
            xml.Append("AdPricingStrategy = \"" + (int)this.AdPricingStrategy + "\" ");
            xml.Append("ColorOption = \"" + (int)this.ColorOption + "\" ");
            xml.Append("ColorPricing = \"" + (int)this.ColorPricing + "\" ");
            xml.Append("ColorInchRate = \"" + this.ColorInchRate.ToString() + "\" ");
            xml.Append("Note = \"" + this.Note.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("BigLogo = \"" + Convert.ToBase64String((byte[])converter.ConvertTo(this.BigLogo, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("SmallLogo = \"" + Convert.ToBase64String((byte[])converter.ConvertTo(this.SmallLogo, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("TinyLogo = \"" + Convert.ToBase64String((byte[])converter.ConvertTo(this.TinyLogo, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("AllowSundaySelect = \"" + this.AllowSundaySelect.ToString() + "\" ");
            xml.Append("AllowMondaySelect = \"" + this.AllowMondaySelect.ToString() + "\" ");
            xml.Append("AllowTuesdaySelect = \"" + this.AllowTuesdaySelect.ToString() + "\" ");
            xml.Append("AllowWednesdaySelect = \"" + this.AllowWednesdaySelect.ToString() + "\" ");
            xml.Append("AllowThursdaySelect = \"" + this.AllowThursdaySelect.ToString() + "\" ");
            xml.Append("AllowFridaySelect = \"" + this.AllowFridaySelect.ToString() + "\" ");
            xml.Append("AllowSaturdaySelect = \"" + this.AllowSaturdaySelect.ToString() + "\" ");
            xml.AppendLine(@">");
            xml.AppendLine(this.SizeOptions.Serialize());
            xml.AppendLine(@"<ViewSettings>" + this.ViewSettings.Serialize() + @"</ViewSettings>");
            foreach (Insert insert in this.Inserts)
                xml.AppendLine(insert.Serialize());
            xml.AppendLine(@"</Publication>");

            return xml.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            DateTime tempDateTime = DateTime.MinValue;
            int tempInt = 0;
            bool tempBool;
            double tempDouble;
            Guid tempGuid;

            foreach (XmlAttribute attribute in node.Attributes)
                switch (attribute.Name)
                {
                    case "Name":
                        this.Name = attribute.Value;
                        break;
                    case "UniqueID":
                        if (Guid.TryParse(attribute.Value, out tempGuid))
                            this.UniqueID = tempGuid;
                        break;
                    case "Abbreviation":
                        this.Abbreviation = attribute.Value;
                        break;
                    case "DailyDelivery":
                        tempDouble = 0;
                        this.DailyDelivery = null;
                        double.TryParse(attribute.Value, out tempDouble);
                        if (tempDouble > 0)
                            this.DailyDelivery = tempDouble;
                        break;
                    case "DailyReadership":
                        tempDouble = 0;
                        this.DailyReadership = null;
                        double.TryParse(attribute.Value, out tempDouble);
                        if (tempDouble > 0)
                            this.DailyReadership = tempDouble;
                        break;
                    case "SundayDelivery":
                        tempDouble = 0;
                        this.SundayDelivery = null;
                        double.TryParse(attribute.Value, out tempDouble);
                        if (tempDouble > 0)
                            this.SundayDelivery = tempDouble;
                        break;
                    case "SundayReadership":
                        tempDouble = 0;
                        this.SundayReadership = null;
                        double.TryParse(attribute.Value, out tempDouble);
                        if (tempDouble > 0)
                            this.SundayReadership = tempDouble;
                        break;
                    case "Index":
                        tempInt = 0;
                        int.TryParse(attribute.Value, out tempInt);
                        this.Index = tempInt;
                        break;
                    case "AdPricingStrategy":
                        tempInt = 0;
                        int.TryParse(attribute.Value, out tempInt);
                        this.AdPricingStrategy = (AdPricingStrategies)tempInt;
                        break;
                    case "ColorOption":
                        tempInt = 0;
                        int.TryParse(attribute.Value, out tempInt);
                        this.ColorOption = (ColorOptions)tempInt;
                        break;
                    case "ColorPricing":
                        tempInt = 0;
                        int.TryParse(attribute.Value, out tempInt);
                        this.ColorPricing = (ColorPricingType)tempInt;
                        break;
                    case "ColorInchRate":
                        tempDouble = 0;
                        double.TryParse(attribute.Value, out tempDouble);
                        this.ColorInchRate = tempDouble;
                        break;
                    case "Note":
                        this.Note = attribute.Value;
                        break;
                    case "BigLogo":
                        if (string.IsNullOrEmpty(attribute.Value))
                            this.BigLogo = null;
                        else
                            this.BigLogo = new Bitmap(new MemoryStream(Convert.FromBase64String(attribute.Value)));
                        break;
                    case "SmallLogo":
                        if (string.IsNullOrEmpty(attribute.Value))
                            this.SmallLogo = null;
                        else
                            this.SmallLogo = new Bitmap(new MemoryStream(Convert.FromBase64String(attribute.Value)));
                        break;
                    case "TinyLogo":
                        if (string.IsNullOrEmpty(attribute.Value))
                            this.TinyLogo = null;
                        else
                            this.TinyLogo = new Bitmap(new MemoryStream(Convert.FromBase64String(attribute.Value)));
                        break;

                    case "AllowSundaySelect":
                        if (bool.TryParse(attribute.Value, out tempBool))
                            this.AllowSundaySelect = tempBool;
                        break;
                    case "AllowMondaySelect":
                        if (bool.TryParse(attribute.Value, out tempBool))
                            this.AllowMondaySelect = tempBool;
                        break;
                    case "AllowTuesdaySelect":
                        if (bool.TryParse(attribute.Value, out tempBool))
                            this.AllowTuesdaySelect = tempBool;
                        break;
                    case "AllowWednesdaySelect":
                        if (bool.TryParse(attribute.Value, out tempBool))
                            this.AllowWednesdaySelect = tempBool;
                        break;
                    case "AllowThursdaySelect":
                        if (bool.TryParse(attribute.Value, out tempBool))
                            this.AllowThursdaySelect = tempBool;
                        break;
                    case "AllowFridaySelect":
                        if (bool.TryParse(attribute.Value, out tempBool))
                            this.AllowFridaySelect = tempBool;
                        break;
                    case "AllowSaturdaySelect":
                        if (bool.TryParse(attribute.Value, out tempBool))
                            this.AllowSaturdaySelect = tempBool;
                        break;
                }
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "SizeOptions":
                        this.SizeOptions.Deserialize(childNode);
                        break;
                    case "ViewSettings":
                        this.ViewSettings.Deserialize(childNode);
                        break;
                    case "Insert":
                        Insert insert = new Insert(this);
                        insert.Deserialize(childNode);
                        this.Inserts.Add(insert);
                        break;
                }
            }
        }

        public void AddInsert()
        {
            this.Inserts.Add(new Insert(this));
        }

        public void SortInserts()
        {
            this.Inserts.Sort((x, y) => x.Date.CompareTo(y.Date) == 0 ? x.Index.CompareTo(y.Index) : x.Date.CompareTo(y.Date));
        }

        public void RebuildInserts()
        {
            List<Insert> temp = new List<Insert>();
            temp.AddRange(this.Inserts);
            temp.Sort((x, y) => x.Date.CompareTo(y.Date) == 0 ? x.Index.CompareTo(y.Index) : x.Date.CompareTo(y.Date));

            DateTime startDate = this.Parent.FlightDateStart;
            DateTime endDate = startDate.AddDays(7);
            do
            {
                IEnumerable<Insert> insertsPerWeek =
                from insert in temp
                where insert.Date >= startDate && insert.Date < endDate
                select insert;
                for (int i = 0; i < insertsPerWeek.Count(); i++)
                {
                    this.Inserts[this.Inserts.IndexOf(insertsPerWeek.ElementAt(i))].Index = i + 1;
                }
                startDate = endDate;
                endDate = startDate.AddDays(7);
            }
            while (startDate < this.Parent.FlightDateEnd);
        }

        public void CloneInsert(Insert originalInsert, DateTime[] cloneDates, bool copyPCIRate, bool copyDiscounts, bool copyColorRate, bool comment, bool section, bool deadline, bool mechanicals)
        {
            foreach (DateTime cloneDate in cloneDates)
            {
                Insert insert = new Insert(this);
                insert.Date = cloneDate;
                if (copyPCIRate)
                {
                    insert.PCIRate = originalInsert.PCIRate;
                    insert.ADRate = originalInsert.ADRate;
                }
                if (copyDiscounts)
                    insert.Discounts = originalInsert.Discounts;
                if (copyColorRate)
                {
                    insert.ColorPricing = originalInsert.ColorPricing;
                    insert.ColorPricingPercent = originalInsert.ColorPricingPercent;
                }
                if (comment)
                {
                    insert.CustomComment = originalInsert.CustomComment;
                    foreach (NameCodePair originalComment in originalInsert.Comments)
                    {
                        NameCodePair newComment = new NameCodePair();
                        newComment.Name = originalComment.Name;
                        newComment.Code = originalComment.Code;
                        insert.Comments.Add(newComment);
                    }
                }
                if (section)
                    insert.Section = originalInsert.Section;
                if (deadline)
                    insert.Deadline = originalInsert.Deadline;
                if (mechanicals)
                    insert.Mechanicals = originalInsert.Mechanicals;
                this.Inserts.Add(insert);
            }
            RebuildInserts();
        }

        public void CopyPCIRate(double value)
        {
            foreach (Insert insert in this.Inserts)
                insert.PCIRate = value;
        }

        public void CopyAdRate(double value)
        {
            foreach (Insert insert in this.Inserts)
                insert.ADRate = value;
        }

        public void CopyDiscounts(double value)
        {
            foreach (Insert insert in this.Inserts)
                insert.Discounts = value;
        }

        public void CopyColorRate(double value)
        {
            foreach (Insert insert in this.Inserts)
                insert.ColorPricing = value;
        }

        public void CopyColorRatePercent(double value)
        {
            foreach (Insert insert in this.Inserts)
                insert.ColorPricingPercent = value;
        }

        public void RefreshAvailableDays()
        {
            this.AvailableDays.Clear();
            DateTime dayInSchedule = this.Parent.FlightDateStart;
            while (dayInSchedule <= this.Parent.FlightDateEnd)
            {
                if ((dayInSchedule.DayOfWeek == DayOfWeek.Sunday && this.AllowSundaySelect) ||
                    (dayInSchedule.DayOfWeek == DayOfWeek.Monday && this.AllowMondaySelect) ||
                    (dayInSchedule.DayOfWeek == DayOfWeek.Tuesday && this.AllowTuesdaySelect) ||
                    (dayInSchedule.DayOfWeek == DayOfWeek.Wednesday && this.AllowWednesdaySelect) ||
                    (dayInSchedule.DayOfWeek == DayOfWeek.Thursday && this.AllowThursdaySelect) ||
                    (dayInSchedule.DayOfWeek == DayOfWeek.Friday && this.AllowFridaySelect) ||
                    (dayInSchedule.DayOfWeek == DayOfWeek.Saturday && this.AllowSaturdaySelect))
                    this.AvailableDays.Add(dayInSchedule);
                dayInSchedule = dayInSchedule.AddDays(1);
            }
        }

        public void ApplyDefaultValues()
        {
            PublicationSource[] sourceSet = ListManager.Instance.PublicationSources.Where(x => x.Name.Equals(_name)).ToArray();
            if (sourceSet.Length > 0)
            {
                this.BigLogo = sourceSet[0].BigLogo != null ? new Bitmap(sourceSet[0].BigLogo) : null; ;
                this.SmallLogo = sourceSet[0].SmallLogo != null ? new Bitmap(sourceSet[0].SmallLogo) : null;
                this.TinyLogo = sourceSet[0].TinyLogo != null ? new Bitmap(sourceSet[0].TinyLogo) : null;
                this.Abbreviation = sourceSet[0].Abbreviation;
                this.DailyDelivery = sourceSet.Where(x => x.Circulation == CirculationType.Daily).Select(x => x.Delivery).FirstOrDefault();
                this.DailyReadership = sourceSet.Where(x => x.Circulation == CirculationType.Daily).Select(x => x.Readership).FirstOrDefault();
                this.SundayDelivery = sourceSet.Where(x => x.Circulation == CirculationType.Sunday).Select(x => x.Delivery).FirstOrDefault();
                this.SundayReadership = sourceSet.Where(x => x.Circulation == CirculationType.Sunday).Select(x => x.Readership).FirstOrDefault();
                this.AllowSundaySelect = sourceSet[0].AllowSundaySelect;
                this.AllowMondaySelect = sourceSet[0].AllowMondaySelect;
                this.AllowTuesdaySelect = sourceSet[0].AllowTuesdaySelect;
                this.AllowWednesdaySelect = sourceSet[0].AllowWednesdaySelect;
                this.AllowThursdaySelect = sourceSet[0].AllowThursdaySelect;
                this.AllowFridaySelect = sourceSet[0].AllowFridaySelect;
                this.AllowSaturdaySelect = sourceSet[0].AllowSaturdaySelect;
            }
            else
            {
                string filePath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.BigImageFolder.FullName, ConfigurationClasses.SettingsManager.DefaultBigLogoFileName);
                if (File.Exists(filePath))
                    this.BigLogo = new Bitmap(filePath);
                else
                    this.BigLogo = null;
                filePath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.SmallImageFolder.FullName, ConfigurationClasses.SettingsManager.DefaultSmallLogoFileName);
                if (File.Exists(filePath))
                    this.SmallLogo = new Bitmap(filePath);
                else
                    this.SmallLogo = null;
                filePath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TinyImageFolder.FullName, ConfigurationClasses.SettingsManager.DefaultTinyLogoFileName);
                if (File.Exists(filePath))
                    this.TinyLogo = new Bitmap(filePath);
                else
                    this.TinyLogo = null;
                this.Abbreviation = _name.Substring(0, 3).ToUpper();
            }
        }

        public Publication Clone()
        {
            Publication result = new Publication(this.Parent);
            XmlDocument document = new XmlDocument();
            document.LoadXml(this.Serialize());
            result.Deserialize(document.FirstChild);
            result.UniqueID = Guid.NewGuid();
            result.Index = this.Index + 0.5;
            this.Parent.Publications.Add(result);
            this.Parent.Publications.Sort((x, y) => x.Index.CompareTo(y.Index));
            this.Parent.RebuildPublicationIndexes();
            return result;
        }
    }

    public class Insert
    {
        private double _pciRate = 0;
        private double _adRate = 0;

        public Publication Parent { get; private set; }
        public Guid UniqueID { get; set; }
        public int Index { get; set; }
        public DateTime Date { get; set; }
        public double Discounts { get; set; }
        public double ColorPricing { get; set; }
        public double ColorPricingPercent { get; set; }
        public string Section { get; set; }
        public string CustomComment { get; set; }
        public string Deadline { get; set; }
        public string Mechanicals { get; set; }
        public List<NameCodePair> Comments { get; private set; }

        public Guid ParentID
        {
            get
            {
                return this.Parent.UniqueID;
            }
        }

        public string ID
        {
            get
            {
                return (this.Parent.Inserts.IndexOf(this) + 1).ToString("000") + this.Parent.InsertSuffix;
            }
        }

        public object DateObject
        {

            get
            {
                if (this.Date.Equals(DateTime.MaxValue) || this.Date.Equals(DateTime.MinValue))
                    return null;
                else
                    return this.Date;
            }
            set
            {
                if (value == null)
                    this.Date = DateTime.MaxValue;
                else
                    this.Date = (DateTime)value;
                this.Parent.RebuildInserts();
            }
        }

        public double? PCIRate
        {
            get
            {
                switch (this.Parent.AdPricingStrategy)
                {
                    case AdPricingStrategies.StandartPCI:
                        return _pciRate;
                    case AdPricingStrategies.FlatModular:
                        if (this.Parent.SizeOptions.Square.HasValue && this.Parent.SizeOptions.Square.Value > 0)
                            return _adRate / this.Parent.SizeOptions.Square.Value;
                        else
                            return null;
                    case AdPricingStrategies.SharePage:
                        return null;
                    default:
                        return null;
                }
            }
            set
            {
                if (this.Parent.AdPricingStrategy == AdPricingStrategies.StandartPCI && value.HasValue)
                    _pciRate = value.Value;
            }
        }

        public double ADRate
        {
            get
            {
                switch (this.Parent.AdPricingStrategy)
                {
                    case AdPricingStrategies.StandartPCI:
                        if (this.Parent.SizeOptions.Square.HasValue)
                            return _pciRate * this.Parent.SizeOptions.Square.Value;
                        else
                            return 0;
                    case AdPricingStrategies.FlatModular:
                    case AdPricingStrategies.SharePage:
                        return _adRate;
                    default:
                        return 0;
                }
            }
            set
            {
                if (this.Parent.AdPricingStrategy == AdPricingStrategies.FlatModular || this.Parent.AdPricingStrategy == AdPricingStrategies.SharePage)
                    _adRate = value;
            }
        }

        public double ColorPricingCalculated
        {
            get
            {
                switch (this.Parent.ColorPricing)
                {
                    case ColorPricingType.CostPerAd:
                        return this.ColorPricing;
                    case ColorPricingType.PercentOfAdRate:
                        return this.ADRate * (this.ColorPricingPercent / 100.00);
                    case ColorPricingType.ColorIncluded:
                        return 0;
                    case ColorPricingType.CostPerInch:
                        if (this.Parent.SizeOptions.Square.HasValue)
                            return this.Parent.SizeOptions.Square.Value * this.Parent.ColorInchRate;
                        else
                            return 0;
                    default:
                        return 0;
                }
            }
        }

        public object ColorPricingObject
        {
            get
            {
                if (this.Parent.ColorOption == ColorOptions.BlackWhite)
                    return "B-W";
                else if (this.Parent.ColorPricing == ColorPricingType.ColorIncluded)
                    return "Included";
                else
                    return this.ColorPricingCalculated;
            }
            set
            {
                if (value == null)
                {
                    this.ColorPricing = 0;
                }
                else
                {
                    double temp = 0;
                    double.TryParse(value.ToString(), out temp);
                    this.ColorPricing = temp;
                }
            }
        }

        public double DiscountRate
        {
            get
            {
                return this.ADRate * (this.Discounts / 100.00);
            }
        }

        public double FinalRate
        {
            get
            {
                return this.ADRate - this.DiscountRate + this.ColorPricingCalculated;
            }
        }

        public string SectionAbbreviation
        {
            get
            {
                Section section = ListManager.Instance.Sections.Where(x => x.Name.Equals(this.Section)).FirstOrDefault();
                if (section != null)
                    return section.Abbreviation;
                else
                    return this.Section.Substring(0, 2);
            }
        }

        public double? PublicationSquare
        {
            get
            {
                return this.Parent.SizeOptions.Square;
            }
        }

        public string FullComment
        {
            get
            {
                List<string> comments = new List<string>();
                if (!string.IsNullOrEmpty(this.CustomComment))
                    comments.Add(this.CustomComment);
                foreach (NameCodePair comment in this.Comments)
                {
                    if (!string.IsNullOrEmpty(comment.Code) && (this.Comments.Count + (!string.IsNullOrEmpty(this.CustomComment) ? 1 : 0)) >= ListManager.Instance.SelectedCommentsBorderValue)
                        comments.Add(comment.Code);
                    else if (!string.IsNullOrEmpty(comment.Name))
                        comments.Add(comment.Name);
                }
                return string.Join(", ", comments.ToArray());
            }
        }

        #region Output Stuff
        public DateTime EndDate
        {
            get
            {
                return this.Date.AddHours(1);
            }
        }

        public string PageSize
        {
            get
            {
                return this.Parent.SizeOptions.PageSize;
            }
        }

        public string PageSizeOutput
        {
            get
            {
                return !string.IsNullOrEmpty(this.Parent.SizeOptions.PageSize) ? this.Parent.SizeOptions.PageSize : "N/A";
            }
        }

        public string PercentOfPage
        {
            get
            {
                return this.Parent.SizeOptions.PercentOfPage;
            }
        }

        public string PercentOfPageOutput
        {
            get
            {
                return !string.IsNullOrEmpty(this.Parent.SizeOptions.PercentOfPage) ? this.Parent.SizeOptions.PercentOfPage : "N/A";
            }
        }

        public string MechanicalsOutput
        {
            get
            {
                return !string.IsNullOrEmpty(this.Mechanicals) ? this.Mechanicals : "N/A";
            }
        }

        public string Publication
        {
            get
            {
                return this.Parent.Name;
            }
        }

        public string PublicationAbbreviation
        {
            get
            {
                return this.Parent.Abbreviation;
            }
        }

        public string SquareStringFormatted
        {
            get
            {
                return this.PublicationSquare.HasValue && this.PublicationSquare.Value > 0 ? (this.PublicationSquare.Value.ToString("#,###.00#")) : null;
            }
        }

        public string Dimensions
        {
            get
            {
                return this.Parent.SizeOptions.Dimensions;
            }
        }

        public string DimensionsOutput
        {
            get
            {
                return !string.IsNullOrEmpty(this.Parent.SizeOptions.Dimensions) ? this.Parent.SizeOptions.Dimensions : "N/A";
            }
        }

        public string DimensionsShort
        {
            get
            {
                return this.Parent.SizeOptions.DimensionsShort;
            }
        }

        public string DeadlineForOutput
        {
            get
            {
                string result = string.Empty;
                if (this.Deadline.ToLower().Contains("day"))
                {

                    Regex re = new Regex(@"\d+");
                    Match m = re.Match(this.Deadline);
                    if (m.Success)
                    {
                        int daysNumber = 0;
                        if (int.TryParse(m.Value, out daysNumber))
                        {
                            DateTime deadline = this.Date.AddDays(0 - daysNumber);
                            while (deadline.DayOfWeek == DayOfWeek.Saturday || deadline.DayOfWeek == DayOfWeek.Sunday)
                                deadline = deadline.AddDays(-1);
                            result = deadline.ToString("ddd, MM/dd/yy");
                        }
                    }
                }
                else
                    result = this.Deadline;
                return result;
            }
        }

        public string Delivery
        {
            get
            {
                string result = string.Empty;
                if (this.Date.DayOfWeek == DayOfWeek.Sunday)
                {
                    if (this.Parent.SundayDelivery != null)
                        if (this.Parent.SundayDelivery.Value > 0)
                            result = this.Parent.SundayDelivery.Value.ToString("#,##0");
                }
                else
                {
                    if (this.Parent.DailyDelivery != null)
                        if (this.Parent.DailyDelivery.Value > 0)
                            result = this.Parent.DailyDelivery.Value.ToString("#,##0");
                }
                return result;
            }
        }

        public string Readership
        {
            get
            {
                string result = string.Empty;
                if (this.Date.DayOfWeek == DayOfWeek.Sunday)
                {
                    if (this.Parent.SundayReadership != null)
                        if (this.Parent.SundayReadership.Value > 0)
                            result = this.Parent.SundayReadership.Value.ToString("#,##0");
                }
                else
                {
                    if (this.Parent.DailyReadership != null)
                        if (this.Parent.DailyReadership.Value > 0)
                            result = this.Parent.DailyReadership.Value.ToString("#,##0");
                }
                return result;
            }
        }

        public Image MultiGridLogo
        {
            get
            {
                return this.Parent.TinyLogo;
            }
        }

        public double PublicationIndex
        {
            get
            {
                return this.Parent.Index;
            }
        }

        public string PublicationColor
        {
            get
            {
                switch (this.Parent.ColorOption)
                {
                    case ColorOptions.BlackWhite:
                        return "bw";
                    case ColorOptions.SpotColor:
                        return "sc";
                    case ColorOptions.FullColor:
                        return "fc";
                    default:
                        return string.Empty;
                }
            }
        }
        #endregion

        public Insert(Publication parent)
        {
            this.Parent = parent;
            this.UniqueID = Guid.NewGuid();
            this.Date = DateTime.MinValue;
            this.Index = 1;
            this.Section = string.Empty;
            this.CustomComment = string.Empty;
            this.Deadline = string.Empty;
            this.Comments = new List<NameCodePair>();
        }

        public string Serialize()
        {
            StringBuilder xml = new StringBuilder();

            xml.Append(@"<Insert ");
            xml.Append("Index = \"" + this.Index + "\" ");
            xml.Append("Date = \"" + this.Date + "\" ");
            xml.Append("PCIRate = \"" + (this.PCIRate.HasValue ? this.PCIRate.Value : 0) + "\" ");
            xml.Append("ADRate = \"" + this.ADRate + "\" ");
            xml.Append("Discounts = \"" + this.Discounts + "\" ");
            xml.Append("ColorPricing = \"" + this.ColorPricing + "\" ");
            xml.Append("ColorPricingPercent = \"" + this.ColorPricingPercent + "\" ");
            xml.Append("Section = \"" + this.Section.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("CustomComment = \"" + this.CustomComment.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("Deadline = \"" + this.Deadline.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("Mechanicals = \"" + (this.Mechanicals != null ? this.Mechanicals.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + "\" ");
            xml.AppendLine(@">");
            foreach (NameCodePair comment in this.Comments)
                xml.AppendLine(comment.Serialize());
            xml.AppendLine(@"</Insert>");

            return xml.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            DateTime tempDateTime = DateTime.MinValue;
            int tempInt = 0;
            double tempDouble;

            foreach (XmlAttribute attribute in node.Attributes)
                switch (attribute.Name)
                {
                    case "Index":
                        if (int.TryParse(attribute.Value, out tempInt))
                            this.Index = tempInt;
                        break;
                    case "Date":
                        if (DateTime.TryParse(attribute.Value, out tempDateTime))
                            this.Date = tempDateTime;
                        break;
                    case "PCIRate":
                        if (double.TryParse(attribute.Value, out tempDouble))
                            this.PCIRate = tempDouble;
                        break;
                    case "ADRate":
                        if (double.TryParse(attribute.Value, out tempDouble))
                            this.ADRate = tempDouble;
                        break;
                    case "Discounts":
                        if (double.TryParse(attribute.Value, out tempDouble))
                            this.Discounts = tempDouble;
                        break;
                    case "ColorPricing":
                        if (double.TryParse(attribute.Value, out tempDouble))
                            this.ColorPricing = tempDouble;
                        break;
                    case "ColorPricingPercent":
                        if (double.TryParse(attribute.Value, out tempDouble))
                            this.ColorPricingPercent = tempDouble;
                        break;
                    case "Section":
                        this.Section = attribute.Value;
                        break;
                    case "CustomComment":
                        this.CustomComment = attribute.Value;
                        break;
                    case "Deadline":
                        this.Deadline = attribute.Value;
                        break;
                    case "Mechanicals":
                        if (!string.IsNullOrEmpty(attribute.Value))
                            this.Mechanicals = attribute.Value;
                        break;
                }
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "NameCodePair":
                        NameCodePair comment = new NameCodePair();
                        comment.Deserialize(childNode);
                        this.Comments.Add(comment);
                        break;
                }
            }
        }

        public void SaveRates()
        {
            if (this.Parent.AdPricingStrategy == AdPricingStrategies.StandartPCI)
                _adRate = Math.Round(this.ADRate, 2);
            else
                _pciRate = this.PCIRate.HasValue ? Math.Round(this.PCIRate.Value, 2) : 0;
        }

        public void ResetRates()
        {
            _pciRate = 0;
            _adRate = 0;
        }
    }

    public class SizeOptions
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public string WidthMeasure { get; set; }
        public string HeightMeasure { get; set; }
        public bool EnableSquare { get; set; }
        public string PageSize { get; set; }
        public bool EnablePageSize { get; set; }
        public string RateCard { get; set; }
        public string PercentOfPage { get; set; }

        #region Calculated Options
        public string ShortWidthMeasure
        {
            get
            {
                switch (this.WidthMeasure.ToLower())
                {
                    case "columns":
                        return " col.";
                    case "inches":
                        return "''";
                    case "depth":
                        return " depth";
                    default:
                        return string.Empty;
                }
            }
        }

        public string ShortHeightMeasure
        {
            get
            {
                switch (this.HeightMeasure.ToLower())
                {
                    case "columns":
                        return " col.";
                    case "inches":
                        return "''";
                    case "depth":
                        return " depth";
                    default:
                        return string.Empty;
                }
            }
        }

        public double? Square
        {
            get
            {
                if (this.Width > 0 && this.Height > 0)
                    return this.Width * this.Height;
                else
                    return null;
            }
        }

        public string Dimensions
        {
            get
            {
                return this.Square.HasValue ? (string.Format("{0}{1} x {2}{3}", new object[] { this.Width.ToString("#,##0.00"), this.ShortWidthMeasure, this.Height.ToString("#,###.00#"), this.ShortHeightMeasure })) : null;
            }
        }

        public string DimensionsShort
        {
            get
            {
                return this.Square.HasValue ? (string.Format("{0}x{1}", new object[] { this.Width.ToString("#,##0.00"), this.Height.ToString("#,###.00#") })) : "N/A";
            }
        }

        public string PageSizeOutput
        {
            get
            {
                return !string.IsNullOrEmpty(this.PageSize) ? this.PageSize : "N/A";
            }
        }

        public string PercentOfPageOutput
        {
            get
            {
                return !string.IsNullOrEmpty(this.PercentOfPage) ? this.PercentOfPage : "N/A";
            }
        }

        public ShareUnit RelatedShareUnit
        {
            get
            {
                BusinessClasses.ShareUnit shareUnit = new BusinessClasses.ShareUnit();
                shareUnit.Width = this.Width.ToString("#,##0.00");
                shareUnit.WidthMeasureUnit = this.WidthMeasure;
                shareUnit.Height = this.Height.ToString("#,##0.00#");
                shareUnit.HeightMeasureUnit = this.HeightMeasure;
                return shareUnit;
            }
        }

        #endregion

        public SizeOptions()
        {
            ResetToDefaults(AdPricingStrategies.StandartPCI);
        }

        public string Serialize()
        {
            StringBuilder xml = new StringBuilder();

            xml.Append(@"<SizeOptions ");
            xml.Append("Width = \"" + this.Width.ToString() + "\" ");
            xml.Append("Height = \"" + this.Height.ToString() + "\" ");
            xml.Append("WidthMeasure = \"" + this.WidthMeasure.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("HeightMeasure = \"" + this.HeightMeasure.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("EnableSquare = \"" + this.EnableSquare.ToString() + "\" ");
            xml.Append("PageSize = \"" + (this.PageSize != null ? this.PageSize.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + "\" ");
            xml.Append("EnablePageSize = \"" + this.EnablePageSize.ToString() + "\" ");
            xml.Append("RateCard = \"" + (this.RateCard != null ? this.RateCard.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + "\" ");
            xml.Append("PercentOfPage = \"" + (this.PercentOfPage != null ? this.PercentOfPage.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + "\" ");
            xml.AppendLine(@"/>");

            return xml.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool;
            double tempDouble;

            foreach (XmlAttribute attribute in node.Attributes)
                switch (attribute.Name)
                {
                    case "Width":
                        if (double.TryParse(attribute.Value, out tempDouble))
                            this.Width = tempDouble;
                        break;
                    case "Height":
                        if (double.TryParse(attribute.Value, out tempDouble))
                            this.Height = tempDouble;
                        break;
                    case "WidthMeasure":
                        this.WidthMeasure = attribute.Value;
                        break;
                    case "HeightMeasure":
                        this.HeightMeasure = attribute.Value;
                        break;
                    case "EnableSquare":
                        if (bool.TryParse(attribute.Value, out tempBool))
                            this.EnableSquare = tempBool;
                        break;
                    case "PageSize":
                        if (!string.IsNullOrEmpty(attribute.Value))
                            this.PageSize = attribute.Value;
                        break;
                    case "EnablePageSize":
                        if (bool.TryParse(attribute.Value, out tempBool))
                            this.EnablePageSize = tempBool;
                        break;
                    case "RateCard":
                        if (!string.IsNullOrEmpty(attribute.Value))
                            this.RateCard = attribute.Value;
                        break;
                    case "PercentOfPage":
                        if (!string.IsNullOrEmpty(attribute.Value))
                            this.PercentOfPage = attribute.Value;
                        break;
                }
        }

        public void ResetToDefaults(AdPricingStrategies pricing)
        {
            this.WidthMeasure = "Columns";
            this.HeightMeasure = "Inches";
            this.Width = 0;
            this.Height = 0;
            this.RateCard = null;
            this.PercentOfPage = null;
            switch (pricing)
            {
                case AdPricingStrategies.StandartPCI:
                    this.EnableSquare = true;
                    break;
                case AdPricingStrategies.FlatModular:
                    this.EnableSquare = false;
                    break;
                case AdPricingStrategies.SharePage:
                    this.EnableSquare = false;
                    break;
            }
        }
    }

    public class PublicationSource : ICloneable
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public Image BigLogo { get; set; }
        public string BigLogoFileName { get; set; }
        public Image SmallLogo { get; set; }
        public string SmallLogoFileName { get; set; }
        public Image TinyLogo { get; set; }
        public string TinyLogoFileName { get; set; }
        public CirculationType Circulation { get; set; }
        public double? Delivery { get; set; }
        public double? Readership { get; set; }

        public bool AllowSundaySelect { get; set; }
        public bool AllowMondaySelect { get; set; }
        public bool AllowTuesdaySelect { get; set; }
        public bool AllowWednesdaySelect { get; set; }
        public bool AllowThursdaySelect { get; set; }
        public bool AllowFridaySelect { get; set; }
        public bool AllowSaturdaySelect { get; set; }

        public PublicationSource()
        {
            this.Name = string.Empty;
            this.Abbreviation = string.Empty;
            this.BigLogo = null;
            this.SmallLogo = null;
            this.TinyLogo = null;
            this.Circulation = CirculationType.Daily;
            this.Delivery = null;
            this.Readership = null;
        }

        public object Clone()
        {
            PublicationSource clone = new PublicationSource();
            clone.Name = this.Name;
            clone.Abbreviation = this.Abbreviation;
            clone.BigLogo = this.BigLogo;
            clone.SmallLogo = this.SmallLogo;
            clone.TinyLogo = this.TinyLogo;
            clone.Circulation = this.Circulation;
            clone.Delivery = this.Delivery;
            clone.Readership = this.Readership;
            return clone;
        }
    }
}
