using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace MobileScheduleBuilder.BusinessClasses
{
    public enum RateType
    {
        CPM = 0,
        Fixed
    }

    public enum FormulaType
    {
        CPM = 0,
        Investment,
        Impressions
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
        public string Status { get; set; }
        public DateTime PresentationDate { get; set; }
        public DateTime FlightDateStart { get; set; }
        public DateTime FlightDateEnd { get; set; }
        public bool ApplySettingsForeAllProducts { get; set; }
        public List<Product> Products { get; set; }
        public ProductPackage ProductPackage { get; set; }
        public ProductSummarySettings ProductSummarySettings { get; set; }
        public ProductBundleSettings ProductBundleSettings { get; set; }

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

        public string FlightDates
        {
            get
            {
                if (this.FlightDateStart != DateTime.MinValue && this.FlightDateEnd != DateTime.MinValue)
                    return this.FlightDateStart.ToString("MM/dd/yy") + " - " + this.FlightDateEnd.ToString("MM/dd/yy");
                else
                    return string.Empty;
            }
        }

        public double MonthlyInvestment
        {
            get
            {
                return this.Products.Select(x => (x.MonthlyInvestment.HasValue ? x.MonthlyInvestment.Value : 0)).Sum();
            }
        }

        public double MonthlyImpressions
        {
            get
            {
                return this.Products.Select(x => (x.MonthlyImpressions.HasValue ? x.MonthlyImpressions.Value : 0)).Sum();
            }
        }

        public bool EnableMonthlyOnSummary
        {
            get
            {
                bool result = false;
                foreach (Product product in this.Products)
                    result = result | (product.MonthlyImpressions.HasValue || product.MonthlyInvestment.HasValue);
                return result;
            }
        }

        public double TotalInvestment
        {
            get
            {
                return this.Products.Select(x => (x.TotalInvestment.HasValue ? x.TotalInvestment.Value : 0)).Sum();
            }
        }

        public double TotalImpressions
        {
            get
            {
                return this.Products.Select(x => (x.TotalImpressions.HasValue ? x.TotalImpressions.Value : 0)).Sum();
            }
        }

        public bool EnableTotalOnSummary
        {
            get
            {
                bool result = false;
                foreach (Product product in this.Products)
                    result = result | (product.TotalImpressions.HasValue || product.TotalInvestment.HasValue);
                return result;
            }
        }

        public Schedule(string fileName)
        {
            this.Status = ListManager.Instance.Statuses.FirstOrDefault();
            this.Products = new List<Product>();
            this.ProductPackage = new ProductPackage(this);
            this.ProductSummarySettings = new ProductSummarySettings();
            this.ProductBundleSettings = new ProductBundleSettings();

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

                node = document.SelectSingleNode(@"/Schedule/Status");
                if (node != null)
                    this.Status = node.InnerText;

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

                node = document.SelectSingleNode(@"/Schedule/ApplySettingsForeAllProducts");
                if (node != null)
                {
                    tempBool = false;
                    bool.TryParse(node.InnerText, out tempBool);
                    this.ApplySettingsForeAllProducts = tempBool;
                }

                node = document.SelectSingleNode(@"/Schedule/ProductSummarySettings");
                if (node != null)
                    this.ProductSummarySettings.Deserialize(node);
                node = document.SelectSingleNode(@"/Schedule/ProductBundleSettings");
                if (node != null)
                    this.ProductBundleSettings.Deserialize(node);
                node = document.SelectSingleNode(@"/Schedule/Products");
                if (node != null)
                {
                    foreach (XmlNode productNode in node.ChildNodes)
                    {
                        Product product = new Product(this);
                        product.Deserialize(productNode);
                        this.Products.Add(product);
                    }
                }

                node = document.SelectSingleNode(@"/Schedule/ProductPackage");
                if (node != null)
                {
                    this.ProductPackage.Deserialize(node);
                }
            }
            if (string.IsNullOrEmpty(this.ProductPackage.Description))
                this.ProductPackage.UpdateWebProducts();
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
            xml.AppendLine(@"<PresentationDate>" + this.PresentationDate.ToString() + @"</PresentationDate>");
            xml.AppendLine(@"<FlightDateStart>" + this.FlightDateStart.ToString() + @"</FlightDateStart>");
            xml.AppendLine(@"<FlightDateEnd>" + this.FlightDateEnd.ToString() + @"</FlightDateEnd>");
            xml.AppendLine(@"<ApplySettingsForeAllProducts>" + this.ApplySettingsForeAllProducts.ToString() + @"</ApplySettingsForeAllProducts>");
            xml.AppendLine(@"<ProductSummarySettings>" + this.ProductSummarySettings.Serialize() + @"</ProductSummarySettings>");
            xml.AppendLine(@"<ProductBundleSettings>" + this.ProductBundleSettings.Serialize() + @"</ProductBundleSettings>");

            xml.AppendLine(@"<Products>");
            foreach (Product product in this.Products)
            {
                xml.AppendLine(product.Serialize());
            }
            xml.AppendLine(@"</Products>");
            xml.AppendLine(this.ProductPackage.Serialize());
            xml.AppendLine(@"</Schedule>");

            using (StreamWriter sw = new StreamWriter(_scheduleFile.FullName, false))
            {
                sw.Write(xml);
                sw.Flush();
            }
        }

        public void AddProduct(string categoryName)
        {
            Product product = new Product(this);
            product.Category = categoryName;
            this.Products.Add(product);
        }

        public void UpProduct(int position)
        {
            if (position > 0)
            {
                this.Products[position].Index--;
                this.Products[position - 1].Index++;
                this.Products.Sort((x, y) => x.Index.CompareTo(y.Index));
            }
        }

        public void DownProduct(int position)
        {
            if (position < this.Products.Count - 1)
            {
                this.Products[position].Index++;
                this.Products[position + 1].Index--;
                this.Products.Sort((x, y) => x.Index.CompareTo(y.Index));
            }
        }

        public void RebuildProductIndexes()
        {
            for (int i = 0; i < this.Products.Count; i++)
                this.Products[i].Index = i + 1;
        }
    }

    public class Product
    {
        private string _name = null;

        #region Basic Properties
        public Schedule Parent { get; set; }
        public Guid UniqueID { get; set; }
        public int Index { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public RateType RateType { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string Description { get; set; }
        #endregion

        #region Additional Properties
        public string SlideHeader { get; set; }
        public List<string> Websites { get; set; }
        public string CustomWebsite1 { get; set; }
        public string CustomWebsite2 { get; set; }
        public int? ActiveDays { get; set; }
        public int? TotalAds { get; set; }
        public string DurationType { get; set; }
        public int? DurationValue { get; set; }
        public string Strength1 { get; set; }
        public string Strength2 { get; set; }
        public string Comment { get; set; }
        public double? AdRate { get; set; }
        public double? MonthlyInvestment { get; set; }
        public double? MonthlyImpressions { get; set; }
        public double? MonthlyCPM { get; set; }
        public double? TotalInvestment { get; set; }
        public double? TotalImpressions { get; set; }
        public double? TotalCPM { get; set; }
        public double? DefaultRate { get; set; }
        public FormulaType Formula { get; set; }
        #endregion

        #region Show Properties
        private bool _showCPMButton = true;
        public bool ShowPresentationDate { get; set; }
        public bool ShowBusinessName { get; set; }
        public bool ShowDecisionMaker { get; set; }
        public bool ShowWebsite { get; set; }
        public bool ShowCustomWebsite1 { get; set; }
        public bool ShowCustomWebsite2 { get; set; }
        public bool ShowProduct { get; set; }
        public bool ShowDescription { get; set; }
        public bool ShowDimensions { get; set; }
        public bool ShowFlightDates { get; set; }
        public bool ShowActiveDays { get; set; }
        public bool ShowTotalAds { get; set; }
        public bool ShowAdRate { get; set; }
        public bool ShowMonthlyInvestment { get; set; }
        public bool ShowTotalInvestment { get; set; }
        public bool ShowMonthlyImpressions { get; set; }
        public bool ShowTotalImpressions { get; set; }
        public bool ShowComments { get; set; }
        public bool ShowDuration { get; set; }
        public bool ShowCommentText { get; set; }
        public bool ShowStrength1 { get; set; }
        public bool ShowStrength2 { get; set; }
        public bool ShowImages { get; set; }
        public bool ShowScreenshot { get; set; }
        public bool ShowSignature { get; set; }

        public bool ShowCPMButton
        {
            get
            {
                return (this.ShowMonthlyImpressions | this.ShowTotalImpressions) & _showCPMButton;
            }
            set
            {
                _showCPMButton = value;
            }
        }

        public bool EnableCPMButton
        {
            get
            {
                return this.ShowMonthlyImpressions | this.ShowTotalImpressions;
            }
        }

        public bool ShowMonthlyCPM
        {
            get
            {
                return this.ShowMonthlyInvestment & this.ShowMonthlyImpressions & _showCPMButton;
            }
        }

        public bool ShowTotalCPM
        {
            get
            {
                return this.ShowTotalInvestment & this.ShowTotalImpressions & _showCPMButton;
            }
        }
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

        public string WebCategory
        {
            get
            {
                return string.Format("{0}{1}", new object[] { this.Category, !string.IsNullOrEmpty(this.SubCategory) ? (@"\" + this.SubCategory) : (ListManager.Instance.ProductSources.Where(x => x.Category.Name.Equals(this.Category) && !string.IsNullOrEmpty(x.SubCategory)).Count() > 0 ? " (select)" : string.Empty) });
            }
            set
            {
            }
        }

        public string RateTypeText
        {
            get
            {
                switch (this.RateType)
                {
                    case BusinessClasses.RateType.CPM:
                        return "CPM";
                    case BusinessClasses.RateType.Fixed:
                        return "Fixed";
                    default:
                        return string.Empty;
                }
            }
            set
            {
                switch (value)
                {
                    case "CPM":
                        this.RateType = BusinessClasses.RateType.CPM;
                        break;
                    case "Fixed":
                        this.RateType = BusinessClasses.RateType.Fixed;
                        break;
                }
            }
        }

        public string Dimensions
        {
            get
            {
                if (this.Width.HasValue && this.Height.HasValue)
                    return this.Width.Value.ToString() + " x " + this.Height.Value.ToString();
                else
                    return string.Empty;
            }
        }

        public double? MonthlyInvestmentCalculated
        {
            get
            {
                if (this.Formula == FormulaType.Investment)
                    return this.MonthlyImpressions.HasValue && this.MonthlyCPMCalculated.HasValue ? this.MonthlyCPMCalculated.Value * (this.MonthlyImpressions.Value / 1000.00) : (double?)null;
                else
                    return this.MonthlyInvestment;
            }
        }

        public double? TotalInvestmentCalculated
        {
            get
            {
                if (this.Formula == FormulaType.Investment)
                    return this.TotalImpressions.HasValue && this.TotalCPMCalculated.HasValue ? (this.TotalCPMCalculated.Value * (this.TotalImpressions.Value / 1000.00)) : (double?)null;
                else
                    return this.TotalInvestment;
            }
        }

        public double? MonthlyImpressionsCalculated
        {
            get
            {
                if (this.Formula == FormulaType.Impressions)
                    return this.MonthlyCPMCalculated.HasValue && this.MonthlyInvestment.HasValue ? (this.MonthlyCPMCalculated.Value != 0 ? ((this.MonthlyInvestment.Value * 1000) / this.MonthlyCPMCalculated.Value) : (double?)null) : (double?)null;
                else
                    return this.MonthlyImpressions;
            }
        }

        public double? TotalImpressionsCalculated
        {
            get
            {
                if (this.Formula == FormulaType.Impressions)
                    return this.TotalCPMCalculated.HasValue && this.TotalInvestment.HasValue ? (this.TotalCPMCalculated.Value != 0 ? ((this.TotalInvestment.Value * 1000) / this.TotalCPMCalculated.Value) : (double?)null) : (double?)null;
                else
                    return this.TotalImpressions;
            }
        }

        public double? MonthlyCPMCalculated
        {
            get
            {
                if (this.Formula == FormulaType.CPM)
                    return this.MonthlyImpressions.HasValue && this.MonthlyInvestment.HasValue ? (this.MonthlyImpressions.Value != 0 ? (this.MonthlyInvestment.Value / (this.MonthlyImpressions.Value / 1000.00)) : (double?)null) : (double?)null;
                else
                    return !this.MonthlyCPM.HasValue && this.RateType == BusinessClasses.RateType.CPM ? this.DefaultRate : this.MonthlyCPM;
            }
        }

        public double? TotalCPMCalculated
        {
            get
            {
                if (this.Formula == FormulaType.CPM)
                    return this.TotalImpressions.HasValue && this.TotalInvestment.HasValue ? (this.TotalImpressions.Value != 0 ? (this.TotalInvestment.Value / (this.TotalImpressions.Value / 1000.00)) : (double?)null) : (double?)null;
                else
                    return !this.TotalCPM.HasValue && this.RateType == BusinessClasses.RateType.CPM ? this.DefaultRate : this.TotalCPM;
            }
        }

        public int WeeksDuration
        {
            get
            {
                int result = 0;
                TimeSpan diff = this.Parent.FlightDateEnd.Subtract(this.Parent.FlightDateStart);
                result = diff.Days / 7;
                return result;
            }
        }

        public int MonthDuraton
        {
            get
            {
                return Math.Abs((this.Parent.FlightDateEnd.Month - this.Parent.FlightDateStart.Month) + 12 * (this.Parent.FlightDateEnd.Year - this.Parent.FlightDateStart.Year));
            }
        }

        public string AllWebsites
        {
            get
            {
                List<string> websites = new List<string>();
                websites.AddRange(this.Websites);
                if (this.ShowCustomWebsite1)
                    websites.Add(this.CustomWebsite1);
                if (this.ShowCustomWebsite2)
                    websites.Add(this.CustomWebsite2);
                return string.Join(", ", websites.ToArray());
            }
        }
        #endregion

        public Product(Schedule parent)
        {
            this.Parent = parent;
            this.UniqueID = Guid.NewGuid();
            this.Index = this.Parent.Products.Count + 1;
            this.Category = string.Empty;
            this.SubCategory = string.Empty;
            this.Description = string.Empty;
            this.Websites = new List<string>();
            this.CustomWebsite1 = string.Empty;
            this.CustomWebsite2 = string.Empty;
            this.Strength1 = string.Empty;
            this.Strength2 = string.Empty;
            this.Comment = string.Empty;
            this.DurationType = string.Empty;
            this.SlideHeader = string.Empty;

            this.ShowPresentationDate = true;
            this.ShowBusinessName = true;
            this.ShowDecisionMaker = true;
            this.ShowWebsite = true;
            this.ShowCustomWebsite1 = false;
            this.ShowCustomWebsite2 = false;
            this.ShowProduct = true;
            this.ShowDescription = true;
            this.ShowDimensions = true;
            this.ShowFlightDates = true;
            this.ShowActiveDays = false;
            this.ShowTotalAds = false;
            this.ShowAdRate = false;
            this.ShowMonthlyInvestment = true;
            this.ShowTotalInvestment = false;
            this.ShowMonthlyImpressions = true;
            this.ShowTotalImpressions = false;
            this.ShowComments = true;
            this.ShowDuration = true;
            this.ShowCommentText = true;
            this.ShowStrength1 = true;
            this.ShowStrength2 = true;
            this.ShowImages = true;
            this.ShowScreenshot = false;
            this.ShowSignature = true;

            this.Formula = ListManager.Instance.DefaultFormula;
        }

        public string Serialize()
        {
            StringBuilder xml = new StringBuilder();

            xml.Append(@"<Product ");
            #region Basic Properties
            xml.Append("Name = \"" + this.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("Index = \"" + this.Index + "\" ");
            xml.Append("Category = \"" + this.Category.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("SubCategory = \"" + this.SubCategory.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("RateType = \"" + (int)this.RateType + "\" ");
            xml.Append("Width = \"" + (this.Width.HasValue ? this.Width.Value.ToString() : string.Empty) + "\" ");
            xml.Append("Height = \"" + (this.Height.HasValue ? this.Height.Value.ToString() : string.Empty) + "\" ");
            xml.Append("Description = \"" + this.Description.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            #endregion
            #region Additional Properties
            xml.Append("SlideHeader = \"" + this.SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("CustomWebsite1 = \"" + this.CustomWebsite1.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("CustomWebsite2 = \"" + this.CustomWebsite2.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("ActiveDays = \"" + (this.ActiveDays.HasValue ? this.ActiveDays.Value.ToString() : string.Empty) + "\" ");
            xml.Append("TotalAds = \"" + (this.TotalAds.HasValue ? this.TotalAds.Value.ToString() : string.Empty) + "\" ");
            xml.Append("DurationType = \"" + this.DurationType.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("DurationValue = \"" + (this.DurationValue.HasValue ? this.DurationValue.Value.ToString() : string.Empty) + "\" ");
            xml.Append("Strength1 = \"" + this.Strength1.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("Strength2 = \"" + this.Strength2.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("Comment = \"" + this.Comment.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("AdRate = \"" + (this.AdRate.HasValue ? this.AdRate.Value.ToString() : string.Empty) + "\" ");
            xml.Append("MonthlyInvestment = \"" + (this.MonthlyInvestment.HasValue ? this.MonthlyInvestment.Value.ToString() : string.Empty) + "\" ");
            xml.Append("MonthlyImpressions = \"" + (this.MonthlyImpressions.HasValue ? this.MonthlyImpressions.Value.ToString() : string.Empty) + "\" ");
            xml.Append("MonthlyCPM = \"" + (this.MonthlyCPM.HasValue ? this.MonthlyCPM.Value.ToString() : string.Empty) + "\" ");
            xml.Append("TotalInvestment = \"" + (this.TotalInvestment.HasValue ? this.TotalInvestment.Value.ToString() : string.Empty) + "\" ");
            xml.Append("TotalImpressions = \"" + (this.TotalImpressions.HasValue ? this.TotalImpressions.Value.ToString() : string.Empty) + "\" ");
            xml.Append("TotalCPM = \"" + (this.TotalCPM.HasValue ? this.TotalCPM.Value.ToString() : string.Empty) + "\" ");
            xml.Append("DefaultRate = \"" + (this.DefaultRate.HasValue ? this.DefaultRate.Value.ToString() : string.Empty) + "\" ");
            xml.Append("Formula = \"" + (int)this.Formula + "\" ");
            #endregion
            #region Show Properties
            xml.Append("ShowActiveDays = \"" + this.ShowActiveDays.ToString() + "\" ");
            xml.Append("ShowAdRate = \"" + this.ShowAdRate.ToString() + "\" ");
            xml.Append("ShowBusinessName = \"" + this.ShowBusinessName.ToString() + "\" ");
            xml.Append("ShowCommentText = \"" + this.ShowCommentText.ToString() + "\" ");
            xml.Append("ShowComments = \"" + this.ShowComments.ToString() + "\" ");
            xml.Append("ShowCPMButton = \"" + this.ShowCPMButton.ToString() + "\" ");
            xml.Append("ShowDecisionMaker = \"" + this.ShowDecisionMaker.ToString() + "\" ");
            xml.Append("ShowDescription = \"" + this.ShowDescription.ToString() + "\" ");
            xml.Append("ShowDimensions = \"" + this.ShowDimensions.ToString() + "\" ");
            xml.Append("ShowFlightDates = \"" + this.ShowFlightDates.ToString() + "\" ");
            xml.Append("ShowMonthlyImpressions = \"" + this.ShowMonthlyImpressions.ToString() + "\" ");
            xml.Append("ShowMonthlyInvestment = \"" + this.ShowMonthlyInvestment.ToString() + "\" ");
            xml.Append("ShowPresentationDate = \"" + this.ShowPresentationDate.ToString() + "\" ");
            xml.Append("ShowProduct = \"" + this.ShowProduct.ToString() + "\" ");
            xml.Append("ShowStrength1 = \"" + this.ShowStrength1.ToString() + "\" ");
            xml.Append("ShowStrength2 = \"" + this.ShowStrength2.ToString() + "\" ");
            xml.Append("ShowTotalAds = \"" + this.ShowTotalAds.ToString() + "\" ");
            xml.Append("ShowTotalImpressions = \"" + this.ShowTotalImpressions.ToString() + "\" ");
            xml.Append("ShowTotalInvestment = \"" + this.ShowTotalInvestment.ToString() + "\" ");
            xml.Append("ShowDuration = \"" + this.ShowDuration.ToString() + "\" ");
            xml.Append("ShowWebsite = \"" + this.ShowWebsite.ToString() + "\" ");
            xml.Append("ShowCustomWebsite1 = \"" + this.ShowCustomWebsite1.ToString() + "\" ");
            xml.Append("ShowCustomWebsite2 = \"" + this.ShowCustomWebsite2.ToString() + "\" ");
            xml.Append("ShowImages = \"" + this.ShowImages.ToString() + "\" ");
            xml.Append("ShowScreenshot = \"" + this.ShowScreenshot.ToString() + "\" ");
            xml.Append("ShowSignature = \"" + this.ShowSignature.ToString() + "\" ");
            #endregion
            xml.AppendLine(@">");
            foreach (string website in this.Websites)
                xml.AppendLine(@"<Website>" + website.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Website>");
            xml.AppendLine(@"</Product>");

            return xml.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            int tempInt;
            bool tempBool;
            double tempDouble;

            foreach (XmlAttribute productAttribute in node.Attributes)
                switch (productAttribute.Name)
                {
                    #region Basic Properties
                    case "Name":
                        this.Name = productAttribute.Value;
                        break;
                    case "Index":
                        if (int.TryParse(productAttribute.Value, out tempInt))
                            this.Index = tempInt;
                        break;
                    case "Category":
                        this.Category = productAttribute.Value;
                        break;
                    case "SubCategory":
                        this.SubCategory = productAttribute.Value;
                        break;
                    case "RateType":
                        if (int.TryParse(productAttribute.Value, out tempInt))
                            this.RateType = (RateType)tempInt;
                        break;
                    case "Width":
                        if (int.TryParse(productAttribute.Value, out tempInt))
                            this.Width = tempInt;
                        else
                            this.Width = null;
                        break;
                    case "Height":
                        if (int.TryParse(productAttribute.Value, out tempInt))
                            this.Height = tempInt;
                        else
                            this.Height = null;
                        break;
                    case "Description":
                        this.Description = productAttribute.Value;
                        break;
                    #endregion

                    #region Additional Properties
                    case "SlideHeader":
                        this.SlideHeader = productAttribute.Value;
                        break;
                    case "CustomWebsite1":
                        this.CustomWebsite1 = productAttribute.Value;
                        break;
                    case "CustomWebsite2":
                        this.CustomWebsite2 = productAttribute.Value;
                        break;
                    case "ActiveDays":
                        if (int.TryParse(productAttribute.Value, out tempInt))
                            this.ActiveDays = tempInt;
                        else
                            this.ActiveDays = null;
                        break;
                    case "TotalAds":
                        if (int.TryParse(productAttribute.Value, out tempInt))
                            this.TotalAds = tempInt;
                        else
                            this.TotalAds = null;
                        break;
                    case "DurationType":
                        this.DurationType = productAttribute.Value;
                        break;
                    case "DurationValue":
                        if (int.TryParse(productAttribute.Value, out tempInt))
                            this.DurationValue = tempInt;
                        else
                            this.DurationValue = null;
                        break;
                    case "Strength1":
                        this.Strength1 = productAttribute.Value;
                        break;
                    case "Strength2":
                        this.Strength2 = productAttribute.Value;
                        break;
                    case "Comment":
                        this.Comment = productAttribute.Value;
                        break;
                    case "AdRate":
                        if (double.TryParse(productAttribute.Value, out tempDouble))
                            this.AdRate = tempDouble;
                        else
                            this.AdRate = null;
                        break;
                    case "MonthlyInvestment":
                        if (double.TryParse(productAttribute.Value, out tempDouble))
                            this.MonthlyInvestment = tempDouble;
                        else
                            this.MonthlyInvestment = null;
                        break;
                    case "MonthlyImpressions":
                        if (double.TryParse(productAttribute.Value, out tempDouble))
                            this.MonthlyImpressions = tempDouble;
                        else
                            this.MonthlyImpressions = null;
                        break;
                    case "MonthlyCPM":
                        if (double.TryParse(productAttribute.Value, out tempDouble))
                            this.MonthlyCPM = tempDouble;
                        else
                            this.MonthlyCPM = null;
                        break;
                    case "TotalInvestment":
                        if (double.TryParse(productAttribute.Value, out tempDouble))
                            this.TotalInvestment = tempDouble;
                        else
                            this.TotalInvestment = null;
                        break;
                    case "TotalImpressions":
                        if (double.TryParse(productAttribute.Value, out tempDouble))
                            this.TotalImpressions = tempDouble;
                        else
                            this.TotalImpressions = null;
                        break;
                    case "TotalCPM":
                        if (double.TryParse(productAttribute.Value, out tempDouble))
                            this.TotalCPM = tempDouble;
                        else
                            this.TotalCPM = null;
                        break;
                    case "DefaultRate":
                        if (double.TryParse(productAttribute.Value, out tempDouble))
                            this.DefaultRate = tempDouble;
                        else
                            this.DefaultRate = null;
                        break;

                    case "Formula":
                        if (int.TryParse(productAttribute.Value, out tempInt))
                            this.Formula = (FormulaType)tempInt;
                        break;
                    #endregion

                    #region Show Properties
                    case "ShowBusinessName":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowBusinessName = tempBool;
                        break;
                    case "ShowDecisionMaker":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowDecisionMaker = tempBool;
                        break;
                    case "ShowPresentationDate":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowPresentationDate = tempBool;
                        break;
                    case "ShowWebsite":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowWebsite = tempBool;
                        break;
                    case "ShowCustomWebsite1":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowCustomWebsite1 = tempBool;
                        break;
                    case "ShowCustomWebsite2":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowCustomWebsite2 = tempBool;
                        break;
                    case "ShowActiveDays":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowActiveDays = tempBool;
                        break;
                    case "ShowAdRate":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowAdRate = tempBool;
                        break;
                    case "ShowComments":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowComments = tempBool;
                        break;
                    case "ShowCommentText":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowCommentText = tempBool;
                        break;
                    case "ShowCPMButton":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowCPMButton = tempBool;
                        break;
                    case "ShowDescription":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowDescription = tempBool;
                        break;
                    case "ShowDimensions":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowDimensions = tempBool;
                        break;
                    case "ShowFlightDates":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowFlightDates = tempBool;
                        break;
                    case "ShowMonthlyImpressions":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowMonthlyImpressions = tempBool;
                        break;
                    case "ShowMonthlyInvestment":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowMonthlyInvestment = tempBool;
                        break;
                    case "ShowProduct":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowProduct = tempBool;
                        break;
                    case "ShowStrength1":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowStrength1 = tempBool;
                        break;
                    case "ShowStrength2":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowStrength2 = tempBool;
                        break;
                    case "ShowTotalAds":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowTotalAds = tempBool;
                        break;
                    case "ShowTotalImpressions":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowTotalImpressions = tempBool;
                        break;
                    case "ShowTotalInvestment":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowTotalInvestment = tempBool;
                        break;
                    case "ShowDuration":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowDuration = tempBool;
                        break;
                    case "ShowImages":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowImages = tempBool;
                        break;
                    case "ShowScreenshot":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowScreenshot = tempBool;
                        break;
                    case "ShowSignature":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowSignature = tempBool;
                        break;
                    #endregion
                }
            this.Websites.Clear();
            foreach (XmlNode websiteNode in node.ChildNodes)
            {
                if (websiteNode.Name.Equals("Website"))
                    this.Websites.Add(websiteNode.InnerText);
            }
        }

        public void ApplyDefaultValues()
        {
            ProductSource source = ListManager.Instance.ProductSources.Where(x => x.Name.Equals(_name) && x.Category.Name.Equals(this.Category) && (x.SubCategory.Equals(this.SubCategory) || string.IsNullOrEmpty(this.SubCategory))).FirstOrDefault();
            if (source != null)
            {
                this.RateType = source.RateType;
                this.DefaultRate = source.Rate;
                this.Width = source.Width;
                this.Height = source.Height;
                this.Description = source.Overview;
            }
        }

        public string GetSlideSource()
        {
            string templateName = string.Empty;
            BusinessClasses.SlideSource slideSource = BusinessClasses.ListManager.Instance.SlideSources.Where(x => x.ShowActiveDays == this.ShowActiveDays &&
                                                                        x.ShowAdRate == this.ShowAdRate &&
                                                                        x.ShowBusinessName == this.ShowBusinessName &&
                                                                        x.ShowComments == this.ShowComments &&
                                                                        x.ShowDecisionMaker == this.ShowDecisionMaker &&
                                                                        x.ShowDescription == this.ShowDescription &&
                                                                        x.ShowDimensions == this.ShowDimensions &&
                                                                        x.ShowDuration == (this.ShowDuration & this.ShowFlightDates) &&
                                                                        x.ShowFlightDates == this.ShowFlightDates &&
                                                                        x.ShowImages == this.ShowImages &&
                                                                        x.ShowMonthlyCPM == this.ShowMonthlyCPM &&
                                                                        x.ShowMonthlyImpressions == this.ShowMonthlyImpressions &&
                                                                        x.ShowMonthlyInvestment == this.ShowMonthlyInvestment &&
                                                                        x.ShowPresentationDate == this.ShowPresentationDate &&
                                                                        x.ShowProduct == this.ShowProduct &&
                                                                        x.ShowScreenshot == this.ShowScreenshot &&
                                                                        x.ShowSignature == this.ShowSignature &&
                                                                        x.ShowTotalAds == this.ShowTotalAds &&
                                                                        x.ShowTotalCPM == this.ShowTotalCPM &&
                                                                        x.ShowTotalImpressions == this.ShowTotalImpressions &&
                                                                        x.ShowTotalInvestment == this.ShowTotalInvestment &&
                                                                        x.ShowWebsite == this.ShowWebsite).FirstOrDefault();
            if (slideSource != null)
                templateName = Path.Combine(BusinessClasses.OutputManager.Instance.OneSheetsTemplatesFolderPath, slideSource.TemplateName);
            return templateName;
        }
    }

    public class ProductPackage
    {
        #region Basic Properties
        public string Name { get; set; }
        public Schedule Parent { get; set; }
        public Guid UniqueID { get; set; }
        #endregion

        #region Additional Properties
        public string SlideHeader { get; set; }
        public List<string> Websites { get; set; }
        public string CustomWebsite1 { get; set; }
        public string CustomWebsite2 { get; set; }
        public string Description { get; set; }
        public int? ActiveDays { get; set; }
        public int? TotalAds { get; set; }
        public string DurationType { get; set; }
        public int? DurationValue { get; set; }
        public string Strength1 { get; set; }
        public string Strength2 { get; set; }
        public string Comment { get; set; }
        public double? AdRate { get; set; }
        public double? MonthlyInvestment { get; set; }
        public double? MonthlyImpressions { get; set; }
        public double? MonthlyCPM { get; set; }
        public double? TotalInvestment { get; set; }
        public double? TotalImpressions { get; set; }
        public double? TotalCPM { get; set; }
        public FormulaType Formula { get; set; }
        #endregion

        #region Show Properties
        private bool _showCPMButton = true;
        public bool ShowPresentationDate { get; set; }
        public bool ShowBusinessName { get; set; }
        public bool ShowDecisionMaker { get; set; }
        public bool ShowWebsite { get; set; }
        public bool ShowCustomWebsite1 { get; set; }
        public bool ShowCustomWebsite2 { get; set; }
        public bool ShowFlightDates { get; set; }
        public bool ShowActiveDays { get; set; }
        public bool ShowTotalAds { get; set; }
        public bool ShowAdRate { get; set; }
        public bool ShowMonthlyInvestment { get; set; }
        public bool ShowTotalInvestment { get; set; }
        public bool ShowMonthlyImpressions { get; set; }
        public bool ShowTotalImpressions { get; set; }
        public bool ShowComments { get; set; }
        public bool ShowDuration { get; set; }
        public bool ShowCommentText { get; set; }
        public bool ShowStrength1 { get; set; }
        public bool ShowStrength2 { get; set; }
        public bool ShowImages { get; set; }
        public bool ShowScreenshot { get; set; }
        public bool ShowSignature { get; set; }

        public bool ShowCPMButton
        {
            get
            {
                return (this.ShowMonthlyImpressions | this.ShowTotalImpressions) & _showCPMButton;
            }
            set
            {
                _showCPMButton = value;
            }
        }

        public bool EnableCPMButton
        {
            get
            {
                return this.ShowMonthlyImpressions | this.ShowTotalImpressions;
            }
        }

        public bool ShowMonthlyCPM
        {
            get
            {
                return this.ShowMonthlyInvestment & this.ShowMonthlyImpressions & _showCPMButton;
            }
        }

        public bool ShowTotalCPM
        {
            get
            {
                return this.ShowTotalInvestment & this.ShowTotalImpressions & _showCPMButton;
            }
        }
        #endregion

        #region Calculated Properties
        public double? MonthlyInvestmentCalculated
        {
            get
            {
                if (this.Formula == FormulaType.Investment)
                    return this.MonthlyImpressions.HasValue && this.MonthlyCPM.HasValue ? (this.MonthlyCPM.Value * (this.MonthlyImpressions.Value / 1000.00)) : (double?)null;
                else
                    return this.MonthlyInvestment;
            }
        }

        public double? TotalInvestmentCalculated
        {
            get
            {
                if (this.Formula == FormulaType.Investment)
                    return this.TotalImpressions.HasValue && this.TotalCPM.HasValue ? (this.TotalCPM.Value * (this.TotalImpressions.Value / 1000.00)) : (double?)null;
                else
                    return this.TotalInvestment;
            }
        }

        public double? MonthlyImpressionsCalculated
        {
            get
            {
                if (this.Formula == FormulaType.Impressions)
                    return this.MonthlyCPM.HasValue && this.MonthlyInvestment.HasValue ? (this.MonthlyCPM.Value != 0 ? ((this.MonthlyInvestment.Value * 1000) / this.MonthlyCPM.Value) : (double?)null) : (double?)null;
                else
                    return this.MonthlyImpressions;
            }
        }

        public double? TotalImpressionsCalculated
        {
            get
            {
                if (this.Formula == FormulaType.Impressions)
                    return this.TotalCPM.HasValue && this.TotalInvestment.HasValue ? (this.TotalCPM.Value != 0 ? ((this.TotalInvestment.Value * 1000) / this.TotalCPM.Value) : (double?)null) : (double?)null;
                else
                    return this.TotalImpressions;
            }
        }

        public double? MonthlyCPMCalculated
        {
            get
            {
                if (this.Formula == FormulaType.CPM)
                    return this.MonthlyImpressions.HasValue && this.MonthlyInvestment.HasValue ? (this.MonthlyImpressions.Value != 0 ? (this.MonthlyInvestment.Value / (this.MonthlyImpressions.Value / 1000.00)) : (double?)null) : (double?)null;
                else
                    return this.MonthlyCPM;
            }
        }

        public double? TotalCPMCalculated
        {
            get
            {
                if (this.Formula == FormulaType.CPM)
                    return this.TotalImpressions.HasValue && this.TotalInvestment.HasValue ? (this.TotalImpressions.Value != 0 ? (this.TotalInvestment.Value / (this.TotalImpressions.Value / 1000.00)) : (double?)null) : (double?)null;
                else
                    return this.TotalCPM;
            }
        }

        public int WeeksDuration
        {
            get
            {
                int result = 0;
                TimeSpan diff = this.Parent.FlightDateEnd.Subtract(this.Parent.FlightDateStart);
                result = diff.Days / 7;
                return result;
            }
        }

        public int MonthDuraton
        {
            get
            {
                return Math.Abs((this.Parent.FlightDateEnd.Month - this.Parent.FlightDateStart.Month) + 12 * (this.Parent.FlightDateEnd.Year - this.Parent.FlightDateStart.Year));
            }
        }

        public string AllWebsites
        {
            get
            {
                List<string> websites = new List<string>();
                websites.AddRange(this.Websites);
                if (this.ShowCustomWebsite1)
                    websites.Add(this.CustomWebsite1);
                if (this.ShowCustomWebsite2)
                    websites.Add(this.CustomWebsite2);
                return string.Join(", ", websites.ToArray());
            }
        }
        #endregion

        public ProductPackage(Schedule parent)
        {
            this.Parent = parent;
            this.UniqueID = Guid.NewGuid();
            this.Name = "Mobile Products";
            this.Websites = new List<string>();
            this.CustomWebsite1 = string.Empty;
            this.CustomWebsite2 = string.Empty;
            this.Strength1 = string.Empty;
            this.Strength2 = string.Empty;
            this.Comment = string.Empty;
            this.Description = string.Empty;
            this.DurationType = string.Empty;
            this.SlideHeader = string.Empty;

            this.ShowPresentationDate = true;
            this.ShowBusinessName = true;
            this.ShowDecisionMaker = true;
            this.ShowWebsite = true;
            this.ShowCustomWebsite1 = false;
            this.ShowCustomWebsite2 = false;
            this.ShowFlightDates = true;
            this.ShowActiveDays = false;
            this.ShowTotalAds = false;
            this.ShowAdRate = false;
            this.ShowMonthlyInvestment = true;
            this.ShowTotalInvestment = false;
            this.ShowMonthlyImpressions = true;
            this.ShowTotalImpressions = false;
            this.ShowComments = true;
            this.ShowDuration = true;
            this.ShowCommentText = true;
            this.ShowStrength1 = true;
            this.ShowStrength2 = true;
            this.ShowImages = true;
            this.ShowScreenshot = false;
            this.ShowSignature = true;

            this.Formula = ListManager.Instance.DefaultFormula;
        }

        public string Serialize()
        {
            StringBuilder xml = new StringBuilder();

            xml.Append(@"<ProductPackage ");
            #region Additional Properties
            xml.Append("SlideHeader = \"" + this.SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("CustomWebsite1 = \"" + this.CustomWebsite1.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("CustomWebsite2 = \"" + this.CustomWebsite2.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("ActiveDays = \"" + (this.ActiveDays.HasValue ? this.ActiveDays.Value.ToString() : string.Empty) + "\" ");
            xml.Append("TotalAds = \"" + (this.TotalAds.HasValue ? this.TotalAds.Value.ToString() : string.Empty) + "\" ");
            xml.Append("Description = \"" + this.Description.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("DurationType = \"" + this.DurationType.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("DurationValue = \"" + (this.DurationValue.HasValue ? this.DurationValue.Value.ToString() : string.Empty) + "\" ");
            xml.Append("Strength1 = \"" + this.Strength1.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("Strength2 = \"" + this.Strength2.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("Comment = \"" + this.Comment.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("AdRate = \"" + (this.AdRate.HasValue ? this.AdRate.Value.ToString() : string.Empty) + "\" ");
            xml.Append("MonthlyInvestment = \"" + (this.MonthlyInvestment.HasValue ? this.MonthlyInvestment.Value.ToString() : string.Empty) + "\" ");
            xml.Append("MonthlyImpressions = \"" + (this.MonthlyImpressions.HasValue ? this.MonthlyImpressions.Value.ToString() : string.Empty) + "\" ");
            xml.Append("MonthlyCPM = \"" + (this.MonthlyCPM.HasValue ? this.MonthlyCPM.Value.ToString() : string.Empty) + "\" ");
            xml.Append("TotalInvestment = \"" + (this.TotalInvestment.HasValue ? this.TotalInvestment.Value.ToString() : string.Empty) + "\" ");
            xml.Append("TotalImpressions = \"" + (this.TotalImpressions.HasValue ? this.TotalImpressions.Value.ToString() : string.Empty) + "\" ");
            xml.Append("TotalCPM = \"" + (this.TotalCPM.HasValue ? this.TotalCPM.Value.ToString() : string.Empty) + "\" ");
            xml.Append("Formula = \"" + (int)this.Formula + "\" ");
            #endregion
            #region Show Properties
            xml.Append("ShowActiveDays = \"" + this.ShowActiveDays.ToString() + "\" ");
            xml.Append("ShowAdRate = \"" + this.ShowAdRate.ToString() + "\" ");
            xml.Append("ShowBusinessName = \"" + this.ShowBusinessName.ToString() + "\" ");
            xml.Append("ShowCommentText = \"" + this.ShowCommentText.ToString() + "\" ");
            xml.Append("ShowComments = \"" + this.ShowComments.ToString() + "\" ");
            xml.Append("ShowCPMButton = \"" + this.ShowCPMButton.ToString() + "\" ");
            xml.Append("ShowDecisionMaker = \"" + this.ShowDecisionMaker.ToString() + "\" ");
            xml.Append("ShowFlightDates = \"" + this.ShowFlightDates.ToString() + "\" ");
            xml.Append("ShowMonthlyImpressions = \"" + this.ShowMonthlyImpressions.ToString() + "\" ");
            xml.Append("ShowMonthlyInvestment = \"" + this.ShowMonthlyInvestment.ToString() + "\" ");
            xml.Append("ShowPresentationDate = \"" + this.ShowPresentationDate.ToString() + "\" ");
            xml.Append("ShowStrength1 = \"" + this.ShowStrength1.ToString() + "\" ");
            xml.Append("ShowStrength2 = \"" + this.ShowStrength2.ToString() + "\" ");
            xml.Append("ShowTotalAds = \"" + this.ShowTotalAds.ToString() + "\" ");
            xml.Append("ShowTotalImpressions = \"" + this.ShowTotalImpressions.ToString() + "\" ");
            xml.Append("ShowTotalInvestment = \"" + this.ShowTotalInvestment.ToString() + "\" ");
            xml.Append("ShowDuration = \"" + this.ShowDuration.ToString() + "\" ");
            xml.Append("ShowWebsite = \"" + this.ShowWebsite.ToString() + "\" ");
            xml.Append("ShowCustomWebsite1 = \"" + this.ShowCustomWebsite1.ToString() + "\" ");
            xml.Append("ShowCustomWebsite2 = \"" + this.ShowCustomWebsite2.ToString() + "\" ");
            xml.Append("ShowImages = \"" + this.ShowImages.ToString() + "\" ");
            xml.Append("ShowScreenshot = \"" + this.ShowScreenshot.ToString() + "\" ");
            xml.Append("ShowSignature = \"" + this.ShowSignature.ToString() + "\" ");
            #endregion
            xml.AppendLine(@">");
            foreach (string website in this.Websites)
                xml.AppendLine(@"<Website>" + website.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Website>");
            xml.AppendLine(@"</ProductPackage>");

            return xml.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            int tempInt;
            bool tempBool;
            double tempDouble;

            foreach (XmlAttribute productAttribute in node.Attributes)
                switch (productAttribute.Name)
                {
                    #region Additional Properties
                    case "SlideHeader":
                        this.SlideHeader = productAttribute.Value;
                        break;
                    case "CustomWebsite1":
                        this.CustomWebsite1 = productAttribute.Value;
                        break;
                    case "CustomWebsite2":
                        this.CustomWebsite2 = productAttribute.Value;
                        break;
                    case "ActiveDays":
                        if (int.TryParse(productAttribute.Value, out tempInt))
                            this.ActiveDays = tempInt;
                        else
                            this.ActiveDays = null;
                        break;
                    case "TotalAds":
                        if (int.TryParse(productAttribute.Value, out tempInt))
                            this.TotalAds = tempInt;
                        else
                            this.TotalAds = null;
                        break;
                    case "DurationType":
                        this.DurationType = productAttribute.Value;
                        break;
                    case "DurationValue":
                        if (int.TryParse(productAttribute.Value, out tempInt))
                            this.DurationValue = tempInt;
                        else
                            this.DurationValue = null;
                        break;
                    case "Strength1":
                        this.Strength1 = productAttribute.Value;
                        break;
                    case "Strength2":
                        this.Strength2 = productAttribute.Value;
                        break;
                    case "Comment":
                        this.Comment = productAttribute.Value;
                        break;
                    case "Description":
                        this.Description = productAttribute.Value;
                        break;
                    case "AdRate":
                        if (double.TryParse(productAttribute.Value, out tempDouble))
                            this.AdRate = tempDouble;
                        else
                            this.AdRate = null;
                        break;
                    case "MonthlyInvestment":
                        if (double.TryParse(productAttribute.Value, out tempDouble))
                            this.MonthlyInvestment = tempDouble;
                        else
                            this.MonthlyInvestment = null;
                        break;
                    case "MonthlyImpressions":
                        if (double.TryParse(productAttribute.Value, out tempDouble))
                            this.MonthlyImpressions = tempDouble;
                        else
                            this.MonthlyImpressions = null;
                        break;
                    case "MonthlyCPM":
                        if (double.TryParse(productAttribute.Value, out tempDouble))
                            this.MonthlyCPM = tempDouble;
                        else
                            this.MonthlyCPM = null;
                        break;
                    case "TotalInvestment":
                        if (double.TryParse(productAttribute.Value, out tempDouble))
                            this.TotalInvestment = tempDouble;
                        else
                            this.TotalInvestment = null;
                        break;
                    case "TotalImpressions":
                        if (double.TryParse(productAttribute.Value, out tempDouble))
                            this.TotalImpressions = tempDouble;
                        else
                            this.TotalImpressions = null;
                        break;
                    case "TotalCPM":
                        if (double.TryParse(productAttribute.Value, out tempDouble))
                            this.TotalCPM = tempDouble;
                        else
                            this.TotalCPM = null;
                        break;
                    case "Formula":
                        if (int.TryParse(productAttribute.Value, out tempInt))
                            this.Formula = (FormulaType)tempInt;
                        break;
                    #endregion

                    #region Show Properties
                    case "ShowBusinessName":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowBusinessName = tempBool;
                        break;
                    case "ShowDecisionMaker":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowDecisionMaker = tempBool;
                        break;
                    case "ShowPresentationDate":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowPresentationDate = tempBool;
                        break;
                    case "ShowWebsite":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowWebsite = tempBool;
                        break;
                    case "ShowCustomWebsite1":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowCustomWebsite1 = tempBool;
                        break;
                    case "ShowCustomWebsite2":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowCustomWebsite2 = tempBool;
                        break;
                    case "ShowActiveDays":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowActiveDays = tempBool;
                        break;
                    case "ShowAdRate":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowAdRate = tempBool;
                        break;
                    case "ShowComments":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowComments = tempBool;
                        break;
                    case "ShowCommentText":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowCommentText = tempBool;
                        break;
                    case "ShowCPMButton":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowCPMButton = tempBool;
                        break;
                    case "ShowFlightDates":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowFlightDates = tempBool;
                        break;
                    case "ShowMonthlyImpressions":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowMonthlyImpressions = tempBool;
                        break;
                    case "ShowMonthlyInvestment":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowMonthlyInvestment = tempBool;
                        break;
                    case "ShowStrength1":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowStrength1 = tempBool;
                        break;
                    case "ShowStrength2":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowStrength2 = tempBool;
                        break;
                    case "ShowTotalAds":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowTotalAds = tempBool;
                        break;
                    case "ShowTotalImpressions":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowTotalImpressions = tempBool;
                        break;
                    case "ShowTotalInvestment":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowTotalInvestment = tempBool;
                        break;
                    case "ShowDuration":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowDuration = tempBool;
                        break;
                    case "ShowImages":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowImages = tempBool;
                        break;
                    case "ShowScreenshot":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowScreenshot = tempBool;
                        break;
                    case "ShowSignature":
                        if (bool.TryParse(productAttribute.Value, out tempBool))
                            this.ShowSignature = tempBool;
                        break;
                    #endregion
                }
            this.Websites.Clear();
            foreach (XmlNode websiteNode in node.ChildNodes)
            {
                if (websiteNode.Name.Equals("Website"))
                    this.Websites.Add(websiteNode.InnerText);
            }
        }

        public void UpdateWebProducts()
        {
            List<string> result = new List<string>();
            if (this.Parent.Products.Count > 0)
                result.AddRange(this.Parent.Products.Take(this.Parent.Products.Count > 6 ? 6 : this.Parent.Products.Count).Select(x => x.Name));
            this.Description = result.Count > 0 ? ("Mobile Package: " + string.Join(", ", result.ToArray())) : string.Empty;
        }

        public string GetSlideSource()
        {
            string templateName = string.Empty;
            BusinessClasses.SlideSource slideSource = BusinessClasses.ListManager.Instance.SlideSources.Where(x => x.ShowActiveDays == this.ShowActiveDays &&
                                                                        x.ShowAdRate == this.ShowAdRate &&
                                                                        x.ShowBusinessName == this.ShowBusinessName &&
                                                                        x.ShowComments == this.ShowComments &&
                                                                        x.ShowDecisionMaker == this.ShowDecisionMaker &&
                                                                        x.ShowDuration == (this.ShowDuration & this.ShowFlightDates) &&
                                                                        x.ShowFlightDates == this.ShowFlightDates &&
                                                                        x.ShowImages == this.ShowImages &&
                                                                        x.ShowMonthlyCPM == this.ShowMonthlyCPM &&
                                                                        x.ShowMonthlyImpressions == this.ShowMonthlyImpressions &&
                                                                        x.ShowMonthlyInvestment == this.ShowMonthlyInvestment &&
                                                                        x.ShowPresentationDate == this.ShowPresentationDate &&
                                                                        x.ShowProduct == true &&
                                                                        x.ShowScreenshot == this.ShowScreenshot &&
                                                                        x.ShowSignature == this.ShowSignature &&
                                                                        x.ShowTotalAds == this.ShowTotalAds &&
                                                                        x.ShowTotalCPM == this.ShowTotalCPM &&
                                                                        x.ShowTotalImpressions == this.ShowTotalImpressions &&
                                                                        x.ShowTotalInvestment == this.ShowTotalInvestment &&
                                                                        x.ShowWebsite == this.ShowWebsite).FirstOrDefault();
            if (slideSource != null)
                templateName = Path.Combine(BusinessClasses.OutputManager.Instance.OneSheetsTemplatesFolderPath, slideSource.TemplateName);
            return templateName;
        }
    }

    public class ProductSource
    {
        public string Name { get; set; }
        public Category Category { get; set; }
        public string SubCategory { get; set; }
        public RateType RateType { get; set; }
        public double? Rate { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string Overview { get; set; }

        public ProductSource()
        {
            this.Name = string.Empty;
            this.SubCategory = string.Empty;
            this.Overview = string.Empty;
        }
    }

    public class Category
    {
        public string Name { get; set; }
        public Image Logo { get; set; }
        public string TooltipTitle { get; set; }
        public string TooltipValue { get; set; }
    }

    public class SlideSource
    {
        public string TemplateName { get; set; }
        public bool ShowWebsite { get; set; }
        public bool ShowBusinessName { get; set; }
        public bool ShowDecisionMaker { get; set; }
        public bool ShowPresentationDate { get; set; }
        public bool ShowProduct { get; set; }
        public bool ShowDescription { get; set; }
        public bool ShowDimensions { get; set; }
        public bool ShowFlightDates { get; set; }
        public bool ShowActiveDays { get; set; }
        public bool ShowTotalAds { get; set; }
        public bool ShowAdRate { get; set; }
        public bool ShowMonthlyInvestment { get; set; }
        public bool ShowTotalInvestment { get; set; }
        public bool ShowMonthlyImpressions { get; set; }
        public bool ShowTotalImpressions { get; set; }
        public bool ShowMonthlyCPM { get; set; }
        public bool ShowTotalCPM { get; set; }
        public bool ShowComments { get; set; }
        public bool ShowDuration { get; set; }
        public bool ShowImages { get; set; }
        public bool ShowScreenshot { get; set; }
        public bool ShowSignature { get; set; }

        public SlideSource()
        {
            this.TemplateName = string.Empty;
        }
    }

    public class ProductSummarySettings
    {
        public bool ShowWebsites { get; set; }
        public bool ShowDimensions { get; set; }
        public bool ShowImpressions { get; set; }
        public bool ShowTotalAds { get; set; }
        public bool ShowActiveDays { get; set; }
        public bool ShowAdRate { get; set; }
        public bool ShowInvestment { get; set; }
        public bool ShowCPM { get; set; }

        public bool ShowMonthlyImpressions { get; set; }
        public bool ShowTotalImpressions { get; set; }
        public bool ShowMonthlyInvestment { get; set; }
        public bool ShowTotalInvestment { get; set; }

        public bool ShowTotalsOnLastOnly { get; set; }

        public string SlideHeader { get; set; }

        #region Output Settings

        public string TotalHeader1 { get; set; }
        public string TotalValue1 { get; set; }
        public string TotalHeader2 { get; set; }
        public string TotalValue2 { get; set; }
        public string TotalHeader3 { get; set; }
        public string TotalValue3 { get; set; }
        public string TotalHeader4 { get; set; }
        public string TotalValue4 { get; set; }

        #endregion

        public ProductSummarySettings()
        {
            this.ShowWebsites = true;
            this.ShowDimensions = true;
            this.ShowImpressions = true;
            this.ShowTotalAds = true;
            this.ShowActiveDays = true;
            this.ShowAdRate = true;
            this.ShowInvestment = true;
            this.ShowCPM = true;

            this.ShowMonthlyImpressions = true;
            this.ShowTotalImpressions = true;
            this.ShowMonthlyInvestment = true;
            this.ShowTotalInvestment = true;

            this.ShowTotalsOnLastOnly = false;

            this.SlideHeader = string.Empty;

            #region Output Settings
            this.TotalHeader1 = string.Empty;
            this.TotalValue1 = string.Empty;
            this.TotalHeader2 = string.Empty;
            this.TotalValue2 = string.Empty;
            this.TotalHeader3 = string.Empty;
            this.TotalValue3 = string.Empty;
            this.TotalHeader4 = string.Empty;
            this.TotalValue4 = string.Empty;
            #endregion
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<ShowActiveDays>" + this.ShowActiveDays.ToString() + @"</ShowActiveDays>");
            result.AppendLine(@"<ShowAdRate>" + this.ShowAdRate.ToString() + @"</ShowAdRate>");
            result.AppendLine(@"<ShowCPM>" + this.ShowCPM.ToString() + @"</ShowCPM>");
            result.AppendLine(@"<ShowDimensions>" + this.ShowDimensions.ToString() + @"</ShowDimensions>");
            result.AppendLine(@"<ShowImpressions>" + this.ShowImpressions.ToString() + @"</ShowImpressions>");
            result.AppendLine(@"<ShowInvestment>" + this.ShowInvestment.ToString() + @"</ShowInvestment>");
            result.AppendLine(@"<ShowMonthlyImpressions>" + this.ShowMonthlyImpressions.ToString() + @"</ShowMonthlyImpressions>");
            result.AppendLine(@"<ShowMonthlyInvestment>" + this.ShowMonthlyInvestment.ToString() + @"</ShowMonthlyInvestment>");
            result.AppendLine(@"<ShowTotalAds>" + this.ShowTotalAds.ToString() + @"</ShowTotalAds>");
            result.AppendLine(@"<ShowTotalImpressions>" + this.ShowTotalImpressions.ToString() + @"</ShowTotalImpressions>");
            result.AppendLine(@"<ShowTotalInvestment>" + this.ShowTotalInvestment.ToString() + @"</ShowTotalInvestment>");
            result.AppendLine(@"<ShowWebsites>" + this.ShowWebsites.ToString() + @"</ShowWebsites>");
            result.AppendLine(@"<ShowTotalsOnLastOnly>" + this.ShowTotalsOnLastOnly.ToString() + @"</ShowTotalsOnLastOnly>");
            result.AppendLine(@"<SlideHeader>" + this.SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");

            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool = false;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "ShowActiveDays":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowActiveDays = tempBool;
                        break;
                    case "ShowAdRate":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAdRate = tempBool;
                        break;
                    case "ShowCPM":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowCPM = tempBool;
                        break;
                    case "ShowDimensions":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDimensions = tempBool;
                        break;
                    case "ShowImpressions":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowImpressions = tempBool;
                        break;
                    case "ShowInvestment":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowInvestment = tempBool;
                        break;
                    case "ShowMonthlyImpressions":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowMonthlyImpressions = tempBool;
                        break;
                    case "ShowMonthlyInvestment":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowMonthlyInvestment = tempBool;
                        break;
                    case "ShowTotalAds":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalAds = tempBool;
                        break;
                    case "ShowTotalImpressions":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalImpressions = tempBool;
                        break;
                    case "ShowTotalInvestment":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalInvestment = tempBool;
                        break;
                    case "ShowWebsites":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowWebsites = tempBool;
                        break;
                    case "ShowTotalsOnLastOnly":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalsOnLastOnly = tempBool;
                        break;
                    case "SlideHeader":
                        this.SlideHeader = childNode.InnerText;
                        break;
                }
            }
        }
    }

    public class ProductBundleSettings
    {
        public bool ShowWebsites { get; set; }
        public bool ShowDimensions { get; set; }
        public bool ShowTotalAds { get; set; }
        public bool ShowActiveDays { get; set; }
        public bool ShowAdRate { get; set; }

        public bool ShowMonthlyImpressions { get; set; }
        public bool ShowTotalImpressions { get; set; }
        public bool ShowMonthlyInvestment { get; set; }
        public bool ShowTotalInvestment { get; set; }
        public bool ShowMonthlyCPM { get; set; }
        public bool ShowTotalCPM { get; set; }

        public bool ShowTotalsOnLastOnly { get; set; }

        public string SlideHeader { get; set; }
        public double? TotalMonthlyImpressions { get; set; }
        public double? TotalMonthlyInvestments { get; set; }
        public double? TotalImpressions { get; set; }
        public double? TotalInvestments { get; set; }



        #region Output Settings

        public string TotalHeader1 { get; set; }
        public string TotalValue1 { get; set; }
        public string TotalCPM1 { get; set; }
        public string TotalHeader2 { get; set; }
        public string TotalValue2 { get; set; }
        public string TotalCPM2 { get; set; }
        public string TotalHeader3 { get; set; }
        public string TotalValue3 { get; set; }
        public string TotalCPM3 { get; set; }
        public string TotalHeader4 { get; set; }
        public string TotalValue4 { get; set; }
        public string TotalCPM4 { get; set; }

        #endregion


        public ProductBundleSettings()
        {
            this.ShowWebsites = true;
            this.ShowDimensions = true;
            this.ShowTotalAds = true;
            this.ShowActiveDays = true;
            this.ShowAdRate = true;

            this.ShowMonthlyImpressions = true;
            this.ShowTotalImpressions = true;
            this.ShowMonthlyInvestment = true;
            this.ShowTotalInvestment = true;

            this.ShowTotalsOnLastOnly = false;

            this.SlideHeader = string.Empty;

            #region Output Settings
            this.TotalHeader1 = string.Empty;
            this.TotalValue1 = string.Empty;
            this.TotalCPM1 = string.Empty;
            this.TotalHeader2 = string.Empty;
            this.TotalValue2 = string.Empty;
            this.TotalCPM2 = string.Empty;
            this.TotalHeader3 = string.Empty;
            this.TotalValue3 = string.Empty;
            this.TotalCPM3 = string.Empty;
            this.TotalHeader4 = string.Empty;
            this.TotalValue4 = string.Empty;
            this.TotalCPM4 = string.Empty;
            #endregion
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<ShowActiveDays>" + this.ShowActiveDays.ToString() + @"</ShowActiveDays>");
            result.AppendLine(@"<ShowAdRate>" + this.ShowAdRate.ToString() + @"</ShowAdRate>");
            result.AppendLine(@"<ShowDimensions>" + this.ShowDimensions.ToString() + @"</ShowDimensions>");
            result.AppendLine(@"<ShowMonthlyImpressions>" + this.ShowMonthlyImpressions.ToString() + @"</ShowMonthlyImpressions>");
            result.AppendLine(@"<ShowMonthlyInvestment>" + this.ShowMonthlyInvestment.ToString() + @"</ShowMonthlyInvestment>");
            result.AppendLine(@"<ShowTotalAds>" + this.ShowTotalAds.ToString() + @"</ShowTotalAds>");
            result.AppendLine(@"<ShowTotalImpressions>" + this.ShowTotalImpressions.ToString() + @"</ShowTotalImpressions>");
            result.AppendLine(@"<ShowTotalInvestment>" + this.ShowTotalInvestment.ToString() + @"</ShowTotalInvestment>");
            result.AppendLine(@"<ShowWebsites>" + this.ShowWebsites.ToString() + @"</ShowWebsites>");
            result.AppendLine(@"<ShowMonthlyCPM>" + this.ShowMonthlyCPM.ToString() + @"</ShowMonthlyCPM>");
            result.AppendLine(@"<ShowTotalCPM>" + this.ShowTotalCPM.ToString() + @"</ShowTotalCPM>");
            result.AppendLine(@"<ShowTotalsOnLastOnly>" + this.ShowTotalsOnLastOnly.ToString() + @"</ShowTotalsOnLastOnly>");
            result.AppendLine(@"<SlideHeader>" + this.SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");
            result.AppendLine(@"<TotalMonthlyImpressions>" + (this.TotalMonthlyImpressions.HasValue ? this.TotalMonthlyImpressions.Value.ToString() : string.Empty) + @"</TotalMonthlyImpressions>");
            result.AppendLine(@"<TotalMonthlyInvestments>" + (this.TotalMonthlyInvestments.HasValue ? this.TotalMonthlyInvestments.Value.ToString() : string.Empty) + @"</TotalMonthlyInvestments>");
            result.AppendLine(@"<TotalImpressions>" + (this.TotalImpressions.HasValue ? this.TotalImpressions.Value.ToString() : string.Empty) + @"</TotalImpressions>");
            result.AppendLine(@"<TotalInvestments>" + (this.TotalInvestments.HasValue ? this.TotalInvestments.Value.ToString() : string.Empty) + @"</TotalInvestments>");

            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool = false;
            double tempDouble = 0;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "ShowActiveDays":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowActiveDays = tempBool;
                        break;
                    case "ShowAdRate":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAdRate = tempBool;
                        break;
                    case "ShowDimensions":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDimensions = tempBool;
                        break;
                    case "ShowMonthlyImpressions":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowMonthlyImpressions = tempBool;
                        break;
                    case "ShowMonthlyInvestment":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowMonthlyInvestment = tempBool;
                        break;
                    case "ShowTotalAds":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalAds = tempBool;
                        break;
                    case "ShowTotalImpressions":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalImpressions = tempBool;
                        break;
                    case "ShowTotalInvestment":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalInvestment = tempBool;
                        break;
                    case "ShowWebsites":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowWebsites = tempBool;
                        break;
                    case "ShowMonthlyCPM":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowMonthlyCPM = tempBool;
                        break;
                    case "ShowTotalCPM":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalCPM = tempBool;
                        break;
                    case "ShowTotalsOnLastOnly":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalsOnLastOnly = tempBool;
                        break;
                    case "SlideHeader":
                        this.SlideHeader = childNode.InnerText;
                        break;
                    case "TotalMonthlyImpressions":
                        if (double.TryParse(childNode.InnerText, out tempDouble))
                            this.TotalMonthlyImpressions = tempDouble;
                        else
                            this.TotalMonthlyImpressions = null;
                        break;
                    case "TotalMonthlyInvestments":
                        if (double.TryParse(childNode.InnerText, out tempDouble))
                            this.TotalMonthlyInvestments = tempDouble;
                        else
                            this.TotalMonthlyInvestments = null;
                        break;
                    case "TotalImpressions":
                        if (double.TryParse(childNode.InnerText, out tempDouble))
                            this.TotalImpressions = tempDouble;
                        else
                            this.TotalImpressions = null;
                        break;
                    case "TotalInvestments":
                        if (double.TryParse(childNode.InnerText, out tempDouble))
                            this.TotalInvestments = tempDouble;
                        else
                            this.TotalInvestments = null;
                        break;
                }
            }
        }
    }
}
