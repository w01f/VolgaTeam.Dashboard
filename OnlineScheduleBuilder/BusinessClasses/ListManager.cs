using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace OnlineScheduleBuilder.BusinessClasses
{
    public class ListManager
    {
        public string ListsFolder { get; set; }

        private const string AdvertisersFileName = @"Advertisers.xml";
        private const string DecisionMakersFileName = @"DecisionMakers.xml";
        private const string OnlineStrategyFileName = @"Online XML\Online Strategy.xml";

        private static ListManager _instance = new ListManager();

        public List<string> Advertisers { get; set; }
        public List<string> DecisionMakers { get; set; }
        public List<string> SlideHeaders { get; set; }
        public List<string> Websites { get; set; }
        public List<string> Strengths { get; set; }
        public List<Category> Categories { get; set; }
        public List<ProductSource> ProductSources { get; set; }
        public List<SlideSource> SlideSources { get; set; }
        public List<string> Statuses { get; set; }
        public FormulaType DefaultFormula { get; set; }

        private ListManager()
        {
            this.ListsFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Data", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.Advertisers = new List<string>();
            this.DecisionMakers = new List<string>();
            this.SlideHeaders = new List<string>();
            this.Websites = new List<string>();
            this.Strengths = new List<string>();
            this.Categories = new List<Category>();
            this.ProductSources = new List<ProductSource>();
            this.SlideSources = new List<SlideSource>();
            this.Statuses = new List<string>();
            LoadLists();
        }

        public static ListManager Instance
        {
            get
            {
                return _instance;
            }
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

        private void LoadOnlineStrategy()
        {
            ProductSource productSource = null;
            this.SlideHeaders.Clear();
            this.Websites.Clear();
            this.Strengths.Clear();
            this.Categories.Clear();
            this.ProductSources.Clear();
            this.SlideSources.Clear();
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
                            case "Header":
                                foreach (XmlAttribute attribute in childeNode.Attributes)
                                {
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!string.IsNullOrEmpty(attribute.Value) && !this.SlideHeaders.Contains(attribute.Value))
                                                this.SlideHeaders.Add(attribute.Value);
                                            break;
                                    }
                                }
                                break;
                            case "Site":
                                foreach (XmlAttribute attribute in childeNode.Attributes)
                                {
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!string.IsNullOrEmpty(attribute.Value) && !this.Websites.Contains(attribute.Value))
                                                this.Websites.Add(attribute.Value);
                                            break;
                                    }
                                }
                                break;
                            case "Strength":
                                foreach (XmlAttribute attribute in childeNode.Attributes)
                                {
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!string.IsNullOrEmpty(attribute.Value) && !this.Strengths.Contains(attribute.Value))
                                                this.Strengths.Add(attribute.Value);
                                            break;
                                    }
                                }
                                break;
                            case "DefaultFormula":
                                switch (childeNode.InnerText.ToLower().Trim())
                                {
                                    case "cpm":
                                        this.DefaultFormula = FormulaType.CPM;
                                        break;
                                    case "investment":
                                        this.DefaultFormula = FormulaType.Investment;
                                        break;
                                    case "impressions":
                                        this.DefaultFormula = FormulaType.Impressions;
                                        break;
                                }
                                break;
                            case "Category":
                                BusinessClasses.Category category = new Category();
                                GetCategories(childeNode, ref category);
                                if (!string.IsNullOrEmpty(category.Name))
                                    this.Categories.Add(category);
                                break;
                            case "Product":
                                productSource = new ProductSource();
                                GetProductProperties(childeNode, ref productSource);
                                if (!string.IsNullOrEmpty(productSource.Name))
                                    this.ProductSources.Add(productSource);
                                break;
                            case "SlideSource":
                                BusinessClasses.SlideSource slideSource = new SlideSource();
                                GetSlideSourceProperties(childeNode, ref slideSource);
                                if (!string.IsNullOrEmpty(slideSource.TemplateName))
                                    this.SlideSources.Add(slideSource);
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

        private void GetProductProperties(XmlNode node, ref ProductSource productSource)
        {
            int tempInt = 0;
            double tempDouble = 0;

            foreach (XmlAttribute attribute in node.Attributes)
            {
                switch (attribute.Name)
                {
                    case "Name":
                        productSource.Name = attribute.Value;
                        break;
                    case "Category":
                        productSource.Category = this.Categories.Where(x => x.Name.Equals(attribute.Value)).FirstOrDefault();
                        break;
                    case "SubCategory":
                        productSource.SubCategory = attribute.Value;
                        break;
                    case "RateType":
                        switch (attribute.Value)
                        {
                            case "CPM":
                                productSource.RateType = RateType.CPM;
                                break;
                            case "Fixed":
                                productSource.RateType = RateType.Fixed;
                                break;
                        }
                        break;
                    case "Rate":
                        if (double.TryParse(attribute.Value, out tempDouble))
                            productSource.Rate = tempDouble;
                        else
                            productSource.Rate = null;
                        break;
                    case "Overview":
                        productSource.Overview = attribute.Value;
                        break;
                    case "Width":
                        if (int.TryParse(attribute.Value, out tempInt))
                            productSource.Width = tempInt;
                        else
                            productSource.Width = null;
                        break;
                    case "Height":
                        if (int.TryParse(attribute.Value, out tempInt))
                            productSource.Height = tempInt;
                        else
                            productSource.Height = null;
                        break;
                }
            }
        }

        private void GetCategories(XmlNode node, ref Category category)
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

        private void GetSlideSourceProperties(XmlNode node, ref SlideSource slideSource)
        {
            bool tempBool = false;

            foreach (XmlAttribute attribute in node.Attributes)
            {
                switch (attribute.Name)
                {
                    case "Name":
                        slideSource.TemplateName = attribute.Value;
                        break;
                    case "ShowActiveDays":
                        tempBool = false;
                        bool.TryParse(attribute.Value, out tempBool);
                        slideSource.ShowActiveDays = tempBool;
                        break;
                    case "ShowAdRate":
                        tempBool = false;
                        bool.TryParse(attribute.Value, out tempBool);
                        slideSource.ShowAdRate = tempBool;
                        break;
                    case "ShowBusinessName":
                        tempBool = false;
                        bool.TryParse(attribute.Value, out tempBool);
                        slideSource.ShowBusinessName = tempBool;
                        break;
                    case "ShowComments":
                        tempBool = false;
                        bool.TryParse(attribute.Value, out tempBool);
                        slideSource.ShowComments = tempBool;
                        break;
                    case "ShowDecisionMaker":
                        tempBool = false;
                        bool.TryParse(attribute.Value, out tempBool);
                        slideSource.ShowDecisionMaker = tempBool;
                        break;
                    case "ShowDescription":
                        tempBool = false;
                        bool.TryParse(attribute.Value, out tempBool);
                        slideSource.ShowDescription = tempBool;
                        break;
                    case "ShowDimensions":
                        tempBool = false;
                        bool.TryParse(attribute.Value, out tempBool);
                        slideSource.ShowDimensions = tempBool;
                        break;
                    case "ShowDuration":
                        tempBool = false;
                        bool.TryParse(attribute.Value, out tempBool);
                        slideSource.ShowDuration = tempBool;
                        break;
                    case "ShowFlightDates":
                        tempBool = false;
                        bool.TryParse(attribute.Value, out tempBool);
                        slideSource.ShowFlightDates = tempBool;
                        break;
                    case "ShowImages":
                        tempBool = false;
                        bool.TryParse(attribute.Value, out tempBool);
                        slideSource.ShowImages = tempBool;
                        break;
                    case "ShowMonthlyCPM":
                        tempBool = false;
                        bool.TryParse(attribute.Value, out tempBool);
                        slideSource.ShowMonthlyCPM = tempBool;
                        break;
                    case "ShowMonthlyImpressions":
                        tempBool = false;
                        bool.TryParse(attribute.Value, out tempBool);
                        slideSource.ShowMonthlyImpressions = tempBool;
                        break;
                    case "ShowMonthlyInvestment":
                        tempBool = false;
                        bool.TryParse(attribute.Value, out tempBool);
                        slideSource.ShowMonthlyInvestment = tempBool;
                        break;
                    case "ShowPresentationDate":
                        tempBool = false;
                        bool.TryParse(attribute.Value, out tempBool);
                        slideSource.ShowPresentationDate = tempBool;
                        break;
                    case "ShowProduct":
                        tempBool = false;
                        bool.TryParse(attribute.Value, out tempBool);
                        slideSource.ShowProduct = tempBool;
                        break;
                    case "ShowScreenshot":
                        tempBool = false;
                        bool.TryParse(attribute.Value, out tempBool);
                        slideSource.ShowScreenshot = tempBool;
                        break;
                    case "ShowSignature":
                        tempBool = false;
                        bool.TryParse(attribute.Value, out tempBool);
                        slideSource.ShowSignature = tempBool;
                        break;
                    case "ShowTotalAds":
                        tempBool = false;
                        bool.TryParse(attribute.Value, out tempBool);
                        slideSource.ShowTotalAds = tempBool;
                        break;
                    case "ShowTotalCPM":
                        tempBool = false;
                        bool.TryParse(attribute.Value, out tempBool);
                        slideSource.ShowTotalCPM = tempBool;
                        break;
                    case "ShowTotalImpressions":
                        tempBool = false;
                        bool.TryParse(attribute.Value, out tempBool);
                        slideSource.ShowTotalImpressions = tempBool;
                        break;
                    case "ShowTotalInvestment":
                        tempBool = false;
                        bool.TryParse(attribute.Value, out tempBool);
                        slideSource.ShowTotalInvestment = tempBool;
                        break;
                    case "ShowWebsite":
                        tempBool = false;
                        bool.TryParse(attribute.Value, out tempBool);
                        slideSource.ShowWebsite = tempBool;
                        break;
                }
            }
        }

        private void LoadLists()
        {
            LoadAdvertisers();
            LoadDecisionMakers();
            LoadOnlineStrategy();
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
}
