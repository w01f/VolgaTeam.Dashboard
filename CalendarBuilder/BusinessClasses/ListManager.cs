using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace CalendarBuilder.BusinessClasses
{
    public enum PrintCirculationType
    {
        Daily = 0,
        Sunday
    }

    public enum RateType
    {
        CPM = 0,
        Fixed
    }

    public class ListManager
    {
        public string ListsFolder { get; set; }

        private const string AdvertisersFileName = @"Advertisers.xml";
        private const string DecisionMakersFileName = @"DecisionMakers.xml";
        private const string PrintStrategyFileName = @"Newspaper XML\Print Strategy.xml";
        private const string OnlineStrategyFileName = @"Online XML\Online Strategy.xml";
        private const string MobileStrategyFileName = @"Mobile XML\Mobile Strategy.xml";

        private static ListManager _instance = new ListManager();

        #region Common Lists
        public List<string> Advertisers { get; private set; }
        public List<string> DecisionMakers { get; private set; }
        public List<string> ClientTypes { get; private set; }
        public List<string> Statuses { get; private set; }
        public List<string> OutputHeaders { get; set; }
        public List<ImageSource> Images { get; set; }
        #endregion

        #region Print Lists
        public List<PrintSource> PrintSources { get; private set; }
        public List<string> PrintPageSizes { get; private set; }
        public List<PrintSection> PrintSections { get; private set; }
        #endregion

        #region Online Lists
        public List<DigitalCategory> OnlineCategories { get; private set; }
        public List<DigitalSource> OnlineSources { get; private set; }
        #endregion

        #region Mobile Lists
        public List<DigitalCategory> MobileCategories { get; private set; }
        public List<DigitalSource> MobileSources { get; private set; }
        #endregion

        private ListManager()
        {
            this.ListsFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Data", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));

            #region Common Lists
            this.Advertisers = new List<string>();
            this.DecisionMakers = new List<string>();
            this.ClientTypes = new List<string>();
            this.Statuses = new List<string>();
            this.OutputHeaders = new List<string>();
            this.Images = new List<ImageSource>();
            #endregion

            #region Print Lists
            this.PrintSources = new List<PrintSource>();
            this.PrintPageSizes = new List<string>();
            this.PrintSections = new List<PrintSection>();
            #endregion

            #region Online Classes
            this.OnlineCategories = new List<DigitalCategory>();
            this.OnlineSources = new List<DigitalSource>();
            #endregion

            #region Mobile Classes
            this.MobileCategories = new List<DigitalCategory>();
            this.MobileSources = new List<DigitalSource>();
            #endregion

            LoadLists();
        }

        public static ListManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private void LoadLists()
        {
            LoadAdvertisers();
            LoadDecisionMakers();
            LoadPrintStrategy();
            LoadOnlineStrategy();
            LoadMobileStrategy();
            LoadImages();
        }

        private void LoadAdvertisers()
        {
            this.Advertisers.Clear();
            string listPath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.ListFolder, AdvertisersFileName);
            if (File.Exists(listPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(listPath);

                XmlNode node = document.SelectSingleNode(@"/Advertisers");
                if (node != null)
                {
                    foreach (XmlNode childeNode in node.ChildNodes)
                    {
                        if (!this.Advertisers.Contains(childeNode.InnerText))
                            this.Advertisers.Add(childeNode.InnerText);
                    }
                }
            }
        }

        private void LoadDecisionMakers()
        {
            this.DecisionMakers.Clear();
            string listPath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.ListFolder, DecisionMakersFileName);
            if (File.Exists(listPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(listPath);

                XmlNode node = document.SelectSingleNode(@"/DecisionMakers");
                if (node != null)
                {
                    foreach (XmlNode childeNode in node.ChildNodes)
                    {
                        if (!this.DecisionMakers.Contains(childeNode.InnerText))
                            this.DecisionMakers.Add(childeNode.InnerText);
                    }
                }
            }
        }

        private void LoadPrintStrategy()
        {
            double tempDouble = 0;
            string filePath = string.Empty;

            this.PrintSources.Clear();
            this.PrintPageSizes.Clear();
            this.ClientTypes.Clear();
            this.PrintSections.Clear();
            this.Statuses.Clear();

            string listPath = Path.Combine(this.ListsFolder, PrintStrategyFileName);
            if (File.Exists(listPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(listPath);

                XmlNode node = document.SelectSingleNode(@"/PrintStrategy");
                if (node != null)
                {
                    foreach (XmlNode childeNode in node.ChildNodes)
                    {
                        switch (childeNode.Name)
                        {
                            case "Publication":
                                PrintSource dailySource = new PrintSource();
                                dailySource.Circulation = PrintCirculationType.Daily;
                                PrintSource sundaySource = new PrintSource();
                                sundaySource.Circulation = PrintCirculationType.Sunday;
                                foreach (XmlAttribute attribute in childeNode.Attributes)
                                    switch (attribute.Name)
                                    {
                                        case "Name":
                                            dailySource.Name = attribute.Value;
                                            sundaySource.Name = attribute.Value;
                                            break;
                                        case "Abbreviation":
                                            dailySource.Abbreviation = attribute.Value;
                                            sundaySource.Abbreviation = attribute.Value;
                                            break;
                                        case "DailyCirculation":
                                            if (double.TryParse(attribute.Value, out tempDouble))
                                                dailySource.Delivery = tempDouble;
                                            break;
                                        case "DailyReadership":
                                            if (double.TryParse(attribute.Value, out tempDouble))
                                                dailySource.Readership = tempDouble;
                                            break;
                                        case "SundayCirculation":
                                            if (double.TryParse(attribute.Value, out tempDouble))
                                                sundaySource.Delivery = tempDouble;
                                            break;
                                        case "SundayReadership":
                                            if (double.TryParse(attribute.Value, out tempDouble))
                                                sundaySource.Readership = tempDouble;
                                            break;
                                    }
                                this.PrintSources.Add(dailySource);
                                this.PrintSources.Add(sundaySource);
                                break;
                            case "AdSize":
                                foreach (XmlAttribute attribute in childeNode.Attributes)
                                    switch (attribute.Name)
                                    {
                                        case "Name":
                                            if (!this.PrintPageSizes.Contains(attribute.Value))
                                                this.PrintPageSizes.Add(attribute.Value);
                                            break;
                                    }
                                break;
                            case "Header":
                                foreach (XmlAttribute attribute in childeNode.Attributes)
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!this.OutputHeaders.Contains(attribute.Value))
                                                this.OutputHeaders.Add(attribute.Value);
                                            break;
                                    }
                                break;
                            case "ClientType":
                                foreach (XmlAttribute attribute in childeNode.Attributes)
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!this.ClientTypes.Contains(attribute.Value))
                                                this.ClientTypes.Add(attribute.Value);
                                            break;
                                    }
                                break;
                            case "Section":
                                PrintSection section = new PrintSection();
                                foreach (XmlAttribute attribute in childeNode.Attributes)
                                    switch (attribute.Name)
                                    {
                                        case "Name":
                                            section.Name = attribute.Value;
                                            break;
                                        case "Abbreviation":
                                            section.Abbreviation = attribute.Value;
                                            break;
                                    }
                                this.PrintSections.Add(section);
                                break;
                            case "Status":
                                foreach (XmlAttribute attribute in childeNode.Attributes)
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!this.Statuses.Contains(attribute.Value))
                                                this.Statuses.Add(attribute.Value);
                                            break;
                                    }
                                break;
                        }
                    }
                }
            }
        }

        private void LoadOnlineStrategy()
        {
            DigitalSource digitalSource = null;
            this.OnlineCategories.Clear();
            this.OnlineSources.Clear();
            string listPath = Path.Combine(this.ListsFolder, OnlineStrategyFileName);
            if (File.Exists(listPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(listPath);

                XmlNode node = document.SelectSingleNode(@"/OnlineStrategy");
                if (node != null)
                {
                    foreach (XmlNode childeNode in node.ChildNodes)
                    {
                        switch (childeNode.Name)
                        {
                            case "Category":
                                BusinessClasses.DigitalCategory category = new DigitalCategory();
                                GetDigitalCategories(childeNode, ref category);
                                if (!string.IsNullOrEmpty(category.Name))
                                    this.OnlineCategories.Add(category);
                                break;
                            case "Product":
                                digitalSource = new DigitalSource();
                                GetOnlineSourceProperties(childeNode, ref digitalSource);
                                if (!string.IsNullOrEmpty(digitalSource.Name))
                                    this.OnlineSources.Add(digitalSource);
                                break;
                            case "Header":
                                foreach (XmlAttribute attribute in childeNode.Attributes)
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!this.OutputHeaders.Contains(attribute.Value))
                                                this.OutputHeaders.Add(attribute.Value);
                                            break;
                                    }
                                break;
                        }
                    }
                }
            }
        }

        private void LoadMobileStrategy()
        {
            DigitalSource digitalSource = null;
            this.MobileCategories.Clear();
            this.MobileSources.Clear();
            string listPath = Path.Combine(this.ListsFolder, MobileStrategyFileName);
            if (File.Exists(listPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(listPath);

                XmlNode node = document.SelectSingleNode(@"/MobileStrategy");
                if (node != null)
                {
                    foreach (XmlNode childeNode in node.ChildNodes)
                    {
                        switch (childeNode.Name)
                        {
                            case "Category":
                                BusinessClasses.DigitalCategory category = new DigitalCategory();
                                GetDigitalCategories(childeNode, ref category);
                                if (!string.IsNullOrEmpty(category.Name))
                                    this.MobileCategories.Add(category);
                                break;
                            case "Product":
                                digitalSource = new DigitalSource();
                                GetMobileSourceProperties(childeNode, ref digitalSource);
                                if (!string.IsNullOrEmpty(digitalSource.Name))
                                    this.MobileSources.Add(digitalSource);
                                break;
                            case "Header":
                                foreach (XmlAttribute attribute in childeNode.Attributes)
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!this.OutputHeaders.Contains(attribute.Value))
                                                this.OutputHeaders.Add(attribute.Value);
                                            break;
                                    }
                                break;
                        }
                    }
                }
            }
        }

        private void LoadImages()
        {
            this.Images.Clear();
            foreach (FileInfo bigImageFile in ConfigurationClasses.SettingsManager.Instance.BigImageFolder.GetFiles("*.png"))
            {
                string imageFileName = Path.GetFileNameWithoutExtension(bigImageFile.FullName);
                string imageFileExtension = Path.GetExtension(bigImageFile.FullName);

                string smallImageFilePath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.SmallImageFolder.FullName, string.Format("{0}2{1}", new string[] { imageFileName, imageFileExtension }));
                string tinyImageFilePath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TinyImageFolder.FullName, string.Format("{0}3{1}", new string[] { imageFileName, imageFileExtension }));
                string xtraTinyImageFilePath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.XtraTinyImageFolder.FullName, string.Format("{0}4{1}", new string[] { imageFileName, imageFileExtension }));
                if (File.Exists(smallImageFilePath) && File.Exists(tinyImageFilePath) && File.Exists(xtraTinyImageFilePath))
                {
                    ImageSource imageSource = new ImageSource(null);
                    imageSource.BigImage = new Bitmap(bigImageFile.FullName);
                    imageSource.SmallImage = new Bitmap(smallImageFilePath);
                    imageSource.TinyImage = new Bitmap(tinyImageFilePath);
                    imageSource.XtraTinyImage = new Bitmap(xtraTinyImageFilePath);
                    this.Images.Add(imageSource);
                }
            }
        }

        private void GetOnlineSourceProperties(XmlNode node, ref DigitalSource digitalSource)
        {
            int tempInt = 0;
            double tempDouble = 0;

            foreach (XmlAttribute attribute in node.Attributes)
            {
                switch (attribute.Name)
                {
                    case "Name":
                        digitalSource.Name = attribute.Value;
                        break;
                    case "Category":
                        digitalSource.Category = this.OnlineCategories.Where(x => x.Name.Equals(attribute.Value)).FirstOrDefault();
                        break;
                    case "SubCategory":
                        digitalSource.SubCategory = attribute.Value;
                        break;
                    case "RateType":
                        switch (attribute.Value)
                        {
                            case "CPM":
                                digitalSource.RateType = RateType.CPM;
                                break;
                            case "Fixed":
                                digitalSource.RateType = RateType.Fixed;
                                break;
                        }
                        break;
                    case "Rate":
                        if (double.TryParse(attribute.Value, out tempDouble))
                            digitalSource.Rate = tempDouble;
                        else
                            digitalSource.Rate = null;
                        break;
                    case "Overview":
                        digitalSource.Overview = attribute.Value;
                        break;
                    case "Width":
                        if (int.TryParse(attribute.Value, out tempInt))
                            digitalSource.Width = tempInt;
                        else
                            digitalSource.Width = null;
                        break;
                    case "Height":
                        if (int.TryParse(attribute.Value, out tempInt))
                            digitalSource.Height = tempInt;
                        else
                            digitalSource.Height = null;
                        break;
                }
            }
        }

        private void GetMobileSourceProperties(XmlNode node, ref DigitalSource digitalSource)
        {
            int tempInt = 0;
            double tempDouble = 0;

            foreach (XmlAttribute attribute in node.Attributes)
            {
                switch (attribute.Name)
                {
                    case "Name":
                        digitalSource.Name = attribute.Value;
                        break;
                    case "Category":
                        digitalSource.Category = this.MobileCategories.Where(x => x.Name.Equals(attribute.Value)).FirstOrDefault();
                        break;
                    case "SubCategory":
                        digitalSource.SubCategory = attribute.Value;
                        break;
                    case "RateType":
                        switch (attribute.Value)
                        {
                            case "CPM":
                                digitalSource.RateType = RateType.CPM;
                                break;
                            case "Fixed":
                                digitalSource.RateType = RateType.Fixed;
                                break;
                        }
                        break;
                    case "Rate":
                        if (double.TryParse(attribute.Value, out tempDouble))
                            digitalSource.Rate = tempDouble;
                        else
                            digitalSource.Rate = null;
                        break;
                    case "Overview":
                        digitalSource.Overview = attribute.Value;
                        break;
                    case "Width":
                        if (int.TryParse(attribute.Value, out tempInt))
                            digitalSource.Width = tempInt;
                        else
                            digitalSource.Width = null;
                        break;
                    case "Height":
                        if (int.TryParse(attribute.Value, out tempInt))
                            digitalSource.Height = tempInt;
                        else
                            digitalSource.Height = null;
                        break;
                }
            }
        }

        private void GetDigitalCategories(XmlNode node, ref DigitalCategory category)
        {
            foreach (XmlAttribute attribute in node.Attributes)
            {
                switch (attribute.Name)
                {
                    case "Name":
                        category.Name = attribute.Value;
                        break;
                    case "Logo":
                        if (string.IsNullOrEmpty(attribute.Value))
                            category.Logo = null;
                        else
                            category.Logo = new Bitmap(new MemoryStream(Convert.FromBase64String(attribute.Value)));
                        break;
                    case "TooltipTitle":
                        category.TooltipTitle = attribute.Value;
                        break;
                    case "TooltipValue":
                        category.TooltipValue = attribute.Value;
                        break;
                }
            }
        }

        public void SaveAdvertisers()
        {
            StringBuilder xml = new StringBuilder();

            xml.AppendLine(@"<Advertisers>");
            foreach (string advertiser in this.Advertisers)
                xml.AppendLine(@"<Advertiser>" + advertiser.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Advertiser>");
            xml.AppendLine(@"</Advertisers>");

            string userConfigurationPath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.ListFolder, AdvertisersFileName);
            using (StreamWriter sw = new StreamWriter(userConfigurationPath, false))
            {
                sw.Write(xml);
                sw.Flush();
            }
        }

        public void SaveDecisionMakers()
        {
            StringBuilder xml = new StringBuilder();

            xml.AppendLine(@"<DecisionMakers>");
            foreach (string decisionMaker in this.DecisionMakers)
                xml.AppendLine(@"<DecisionMaker>" + decisionMaker.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DecisionMaker>");
            xml.AppendLine(@"</DecisionMakers>");

            string userConfigurationPath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.ListFolder, DecisionMakersFileName);
            using (StreamWriter sw = new StreamWriter(userConfigurationPath, false))
            {
                sw.Write(xml);
                sw.Flush();
            }
        }
    }

    #region Print Classes
    public class PrintSource
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public PrintCirculationType Circulation { get; set; }
        public double? Delivery { get; set; }
        public double? Readership { get; set; }

        public PrintSource()
        {
            this.Name = string.Empty;
            this.Abbreviation = string.Empty;
            this.Circulation = PrintCirculationType.Daily;
            this.Delivery = null;
            this.Readership = null;
        }
    }

    public class PrintSection
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public PrintSection()
        {
            this.Name = string.Empty;
            this.Abbreviation = string.Empty;
        }
    }
    #endregion

    #region Digital Classes
    public class DigitalCategory
    {
        public string Name { get; set; }
        public Image Logo { get; set; }
        public string TooltipTitle { get; set; }
        public string TooltipValue { get; set; }
    }

    public class DigitalSource
    {
        public string Name { get; set; }
        public DigitalCategory Category { get; set; }
        public string SubCategory { get; set; }
        public RateType RateType { get; set; }
        public double? Rate { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string Overview { get; set; }

        public DigitalSource()
        {
            this.Name = string.Empty;
            this.SubCategory = string.Empty;
            this.Overview = string.Empty;
        }
    }
    #endregion
}
