using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace AdScheduleBuilder.BusinessClasses
{
    public class ListManager
    {
        public string ListsFolder { get; set; }

        private const string AdvertisersFileName = @"Advertisers.xml";
        private const string DecisionMakersFileName = @"DecisionMakers.xml";
        private const string PrintStrategyFileName = @"Newspaper XML\Print Strategy.xml";

        private static ListManager _instance = new ListManager();

        public List<string> Advertisers { get; set; }
        public List<string> DecisionMakers { get; set; }
        public List<PublicationSource> PublicationSources { get; set; }
        public List<PublicationSource> Readerships { get; set; }
        public List<string> PageSizes { get; set; }
        public List<NameCodePair> Notes { get; set; }
        public List<string> OutputHeaders { get; set; }
        public List<string> ClientTypes { get; set; }
        public List<NameCodePair> Sections { get; set; }
        public List<MechanicalType> Mechanicals { get; set; }
        public List<string> Deadlines { get; set; }
        public List<string> Statuses { get; set; }
        public List<ShareUnit> ShareUnits { get; set; }
        public int SelectedCommentsBorderValue { get; set; }
        public int SelectedSectionsBorderValue { get; set; }

        public AdPricingStrategies DefaultPricingStrategy { get; set; }
        public ColorPricingType DefaultColorPricing { get; set; }
        public ColorOptions DefaultColor { get; set; }

        public ConfigurationClasses.HomeViewSettings DefaultHomeViewSettings { get; private set; }

        public ConfigurationClasses.PrintScheduleViewSettings DefaultPrintScheduleViewSettings { get; private set; }

        public ConfigurationClasses.PublicationBasicOverviewSettings DefaultPublicationBasicOverviewSettings { get; private set; }
        public ConfigurationClasses.PublicationMultiSummarySettings DefaultPublicationMultiSummarySettings { get; private set; }
        public ConfigurationClasses.SnapshotViewSettings DefaultSnapshotViewSettings { get; private set; }

        public ConfigurationClasses.GridColumnsState DefaultDetailedGridColumnState { get; private set; }
        public ConfigurationClasses.AdNotesState DefaultDetailedGridAdNotesState { get; private set; }
        public ConfigurationClasses.SlideBulletsState DefaultDetailedGridSlideBulletsState { get; private set; }
        public ConfigurationClasses.SlideHeaderState DefaultDetailedGridSlideHeaderState { get; private set; }

        public ConfigurationClasses.GridColumnsState DefaultMultiGridColumnState { get; private set; }
        public ConfigurationClasses.AdNotesState DefaultMultiGridAdNotesState { get; private set; }
        public ConfigurationClasses.SlideBulletsState DefaultMultiGridSlideBulletsState { get; private set; }
        public ConfigurationClasses.SlideHeaderState DefaultMultiGridSlideHeaderState { get; private set; }

        public ConfigurationClasses.CalendarViewSettings DefaultCalendarViewSettings { get; private set; }

        private ListManager()
        {
            this.ListsFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Data", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.Advertisers = new List<string>();
            this.DecisionMakers = new List<string>();
            this.PublicationSources = new List<PublicationSource>();
            this.Readerships = new List<PublicationSource>();
            this.PageSizes = new List<string>();
            this.Mechanicals = new List<MechanicalType>();
            this.Notes = new List<NameCodePair>();
            this.OutputHeaders = new List<string>();
            this.ClientTypes = new List<string>();
            this.Sections = new List<NameCodePair>();
            this.Deadlines = new List<string>();
            this.Statuses = new List<string>();
            this.ShareUnits = new List<ShareUnit>();

            this.DefaultHomeViewSettings = new ConfigurationClasses.HomeViewSettings();

            this.DefaultPrintScheduleViewSettings = new ConfigurationClasses.PrintScheduleViewSettings();

            this.DefaultPublicationBasicOverviewSettings = new ConfigurationClasses.PublicationBasicOverviewSettings();
            this.DefaultPublicationMultiSummarySettings = new ConfigurationClasses.PublicationMultiSummarySettings();
            this.DefaultSnapshotViewSettings = new ConfigurationClasses.SnapshotViewSettings();

            this.DefaultDetailedGridColumnState = new ConfigurationClasses.GridColumnsState();
            this.DefaultDetailedGridAdNotesState = new ConfigurationClasses.AdNotesState();
            this.DefaultDetailedGridSlideBulletsState = new ConfigurationClasses.SlideBulletsState();
            this.DefaultDetailedGridSlideHeaderState = new ConfigurationClasses.SlideHeaderState();

            this.DefaultMultiGridColumnState = new ConfigurationClasses.GridColumnsState();
            this.DefaultMultiGridAdNotesState = new ConfigurationClasses.AdNotesState();
            this.DefaultMultiGridSlideBulletsState = new ConfigurationClasses.SlideBulletsState();
            this.DefaultMultiGridSlideHeaderState = new ConfigurationClasses.SlideHeaderState();

            this.DefaultCalendarViewSettings = new ConfigurationClasses.CalendarViewSettings();

            LoadLists();

            if (this.DefaultPrintScheduleViewSettings.DefaultPCI)
                this.DefaultPricingStrategy = AdPricingStrategies.StandartPCI;
            else if (this.DefaultPrintScheduleViewSettings.DefaultFlat)
                this.DefaultPricingStrategy = AdPricingStrategies.FlatModular;
            else if (this.DefaultPrintScheduleViewSettings.DefaultShare)
                this.DefaultPricingStrategy = AdPricingStrategies.SharePage;

            if (this.DefaultPrintScheduleViewSettings.DefaultBlackWhite)
                this.DefaultColor = ColorOptions.BlackWhite;
            else if (this.DefaultPrintScheduleViewSettings.DefaultSpotColor)
                this.DefaultColor = ColorOptions.SpotColor;
            else if (this.DefaultPrintScheduleViewSettings.DefaultFullColor)
                this.DefaultColor = ColorOptions.FullColor;

            if (this.DefaultPrintScheduleViewSettings.DefaultCostPerAd)
                this.DefaultColorPricing = ColorPricingType.CostPerAd;
            else if (this.DefaultPrintScheduleViewSettings.DefaultPercentOfAd)
                this.DefaultColorPricing = ColorPricingType.PercentOfAdRate;
            else if (this.DefaultPrintScheduleViewSettings.DefaultColorIncluded)
                this.DefaultColorPricing = ColorPricingType.ColorIncluded;
            else if (this.DefaultPrintScheduleViewSettings.DefaultCostPerInch)
                this.DefaultColorPricing = ColorPricingType.CostPerInch;

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

        private void LoadPrintStrategy()
        {
            double tempDouble = 0;
            bool tempBool;
            int tempInt;
            string filePath = string.Empty;

            this.PublicationSources.Clear();
            this.Readerships.Clear();
            this.PageSizes.Clear();
            this.Notes.Clear();
            this.OutputHeaders.Clear();
            this.ClientTypes.Clear();
            this.Sections.Clear();
            this.Mechanicals.Clear();
            this.Deadlines.Clear();
            this.Statuses.Clear();

            PublicationSource defaultPublication = new PublicationSource();
            filePath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.BigImageFolder.FullName, ConfigurationClasses.SettingsManager.DefaultBigLogoFileName);
            defaultPublication.Name = "Default";
            if (File.Exists(filePath))
                defaultPublication.BigLogo = new Bitmap(filePath);
            else
                defaultPublication.BigLogo = null;
            filePath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.SmallImageFolder.FullName, ConfigurationClasses.SettingsManager.DefaultSmallLogoFileName);
            if (File.Exists(filePath))
                defaultPublication.SmallLogo = new Bitmap(filePath);
            else
                defaultPublication.SmallLogo = null;
            filePath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TinyImageFolder.FullName, ConfigurationClasses.SettingsManager.DefaultTinyLogoFileName);
            if (File.Exists(filePath))
                defaultPublication.TinyLogo = new Bitmap(filePath);
            else
                defaultPublication.TinyLogo = null;
            this.PublicationSources.Add(defaultPublication);


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
                                PublicationSource dailySource = new PublicationSource();
                                dailySource.Circulation = CirculationType.Daily;
                                PublicationSource sundaySource = new PublicationSource();
                                sundaySource.Circulation = CirculationType.Sunday;
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
                                        case "BigLogo":
                                            dailySource.BigLogo = null;
                                            dailySource.BigLogoFileName = attribute.Value;
                                            filePath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.BigImageFolder.FullName, attribute.Value);
                                            if (!File.Exists(filePath))
                                                filePath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.BigImageFolder.FullName, ConfigurationClasses.SettingsManager.DefaultBigLogoFileName);
                                            if (File.Exists(filePath))
                                                dailySource.BigLogo = new Bitmap(filePath);
                                            sundaySource.BigLogo = dailySource.BigLogo;
                                            sundaySource.BigLogoFileName = dailySource.BigLogoFileName;
                                            break;
                                        case "LittleLogo":
                                            dailySource.SmallLogo = null;
                                            dailySource.SmallLogoFileName = attribute.Value;
                                            filePath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.SmallImageFolder.FullName, attribute.Value);
                                            if (!File.Exists(filePath))
                                                filePath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.SmallImageFolder.FullName, ConfigurationClasses.SettingsManager.DefaultSmallLogoFileName);
                                            if (File.Exists(filePath))
                                                dailySource.SmallLogo = new Bitmap(filePath);
                                            sundaySource.SmallLogo = dailySource.SmallLogo;
                                            sundaySource.SmallLogoFileName = dailySource.SmallLogoFileName;
                                            break;
                                        case "TinyLogo":
                                            dailySource.TinyLogo = null;
                                            dailySource.TinyLogoFileName = attribute.Value;
                                            filePath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TinyImageFolder.FullName, attribute.Value);
                                            if (!File.Exists(filePath))
                                                filePath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TinyImageFolder.FullName, ConfigurationClasses.SettingsManager.DefaultTinyLogoFileName);
                                            if (File.Exists(filePath))
                                                dailySource.TinyLogo = new Bitmap(filePath);
                                            sundaySource.TinyLogo = dailySource.TinyLogo;
                                            sundaySource.TinyLogoFileName = dailySource.TinyLogoFileName;
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
                                        case "AllowSundaySelect":
                                            if (bool.TryParse(attribute.Value, out tempBool))
                                            {
                                                sundaySource.AllowSundaySelect = tempBool;
                                                dailySource.AllowSundaySelect = tempBool;
                                            }
                                            break;
                                        case "AllowMondaySelect":
                                            if (bool.TryParse(attribute.Value, out tempBool))
                                            {
                                                sundaySource.AllowMondaySelect = tempBool;
                                                dailySource.AllowMondaySelect = tempBool;
                                            }
                                            break;
                                        case "AllowTuesdaySelect":
                                            if (bool.TryParse(attribute.Value, out tempBool))
                                            {
                                                sundaySource.AllowTuesdaySelect = tempBool;
                                                dailySource.AllowTuesdaySelect = tempBool;
                                            }
                                            break;
                                        case "AllowWednesdaySelect":
                                            if (bool.TryParse(attribute.Value, out tempBool))
                                            {
                                                sundaySource.AllowWednesdaySelect = tempBool;
                                                dailySource.AllowWednesdaySelect = tempBool;
                                            }
                                            break;
                                        case "AllowThursdaySelect":
                                            if (bool.TryParse(attribute.Value, out tempBool))
                                            {
                                                sundaySource.AllowThursdaySelect = tempBool;
                                                dailySource.AllowThursdaySelect = tempBool;
                                            }
                                            break;
                                        case "AllowFridaySelect":
                                            if (bool.TryParse(attribute.Value, out tempBool))
                                            {
                                                sundaySource.AllowFridaySelect = tempBool;
                                                dailySource.AllowFridaySelect = tempBool;
                                            }
                                            break;
                                        case "AllowSaturdaySelect":
                                            if (bool.TryParse(attribute.Value, out tempBool))
                                            {
                                                sundaySource.AllowSaturdaySelect = tempBool;
                                                dailySource.AllowSaturdaySelect = tempBool;
                                            }
                                            break;
                                    }
                                this.PublicationSources.Add(dailySource);
                                this.PublicationSources.Add(sundaySource);
                                break;
                            case "AdSize":
                                foreach (XmlAttribute attribute in childeNode.Attributes)
                                    switch (attribute.Name)
                                    {
                                        case "Name":
                                            if (!this.PageSizes.Contains(attribute.Value))
                                                this.PageSizes.Add(attribute.Value);
                                            break;
                                    }
                                break;
                            case "Note":
                                NameCodePair note = new NameCodePair();
                                foreach (XmlAttribute attribute in childeNode.Attributes)
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            note.Name = attribute.Value;
                                            break;
                                        case "Code":
                                            note.Code = attribute.Value;
                                            break;
                                    }
                                if (!this.Notes.Select(x => x.Name).Contains(note.Name))
                                    this.Notes.Add(note);
                                break;
                            case "SelectedNotesBorderValue":
                                if (int.TryParse(childeNode.InnerText, out tempInt))
                                    this.SelectedCommentsBorderValue = tempInt;
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
                                NameCodePair section = new NameCodePair();
                                foreach (XmlAttribute attribute in childeNode.Attributes)
                                    switch (attribute.Name)
                                    {
                                        case "Name":
                                            section.Name = attribute.Value;
                                            break;
                                        case "Abbreviation":
                                            section.Code = attribute.Value;
                                            break;
                                    }
                                this.Sections.Add(section);
                                break;
                            case "SelectedSectionsBorderValue":
                                if (int.TryParse(childeNode.InnerText, out tempInt))
                                    this.SelectedSectionsBorderValue = tempInt;
                                break;
                            case "Mechanicals":
                                MechanicalType mechanicalType = new MechanicalType();
                                foreach (XmlAttribute attribute in childeNode.Attributes)
                                    switch (attribute.Name)
                                    {
                                        case "Name":
                                            mechanicalType.Name = attribute.Value;
                                            break;
                                    }
                                foreach (XmlNode mechanicalNode in childeNode.ChildNodes)
                                {
                                    MechanicalItem mechanicalItem = new MechanicalItem();
                                    foreach (XmlAttribute attribute in mechanicalNode.Attributes)
                                        switch (attribute.Name)
                                        {
                                            case "Name":
                                                mechanicalItem.Name = attribute.Value;
                                                break;
                                            case "Value":
                                                mechanicalItem.Value = attribute.Value;
                                                break;
                                        }
                                    if (mechanicalType.Items.Count(x => x.Name.Equals(mechanicalItem.Name)) == 0)
                                        mechanicalType.Items.Add(mechanicalItem);
                                }
                                if (this.Mechanicals.Count(x => x.Name.Equals(mechanicalType.Name)) == 0)
                                    this.Mechanicals.Add(mechanicalType);
                                break;
                            case "Deadline":
                                foreach (XmlAttribute attribute in childeNode.Attributes)
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!this.Deadlines.Contains(attribute.Value))
                                                this.Deadlines.Add(attribute.Value);
                                            break;
                                    }
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
                            case "ShareUnit":
                                ShareUnit shareUnit = new ShareUnit();
                                foreach (XmlAttribute attribute in childeNode.Attributes)
                                    switch (attribute.Name)
                                    {
                                        case "RateCard":
                                            shareUnit.RateCard = attribute.Value;
                                            break;
                                        case "PercentOfPage":
                                            shareUnit.PercentOfPage = attribute.Value;
                                            break;
                                        case "Width":
                                            shareUnit.Width = attribute.Value;
                                            break;
                                        case "WidthMeasureUnit":
                                            shareUnit.WidthMeasureUnit = attribute.Value;
                                            break;
                                        case "Height":
                                            shareUnit.Height = attribute.Value;
                                            break;
                                        case "HeightMeasureUnit":
                                            shareUnit.HeightMeasureUnit = attribute.Value;
                                            break;
                                    }
                                this.ShareUnits.Add(shareUnit);
                                break;
                            case "DefaultHomeViewSettings":
                                this.DefaultHomeViewSettings.Deserialize(childeNode);
                                break;
                            case "DefaultPrintScheduleViewSettings":
                                this.DefaultPrintScheduleViewSettings.Deserialize(childeNode);
                                break;
                            case "DefaultPublicationBasicOverviewSettings":
                                this.DefaultPublicationBasicOverviewSettings.Deserialize(childeNode);
                                break;
                            case "DefaultPublicationMultiSummarySettings":
                                this.DefaultPublicationMultiSummarySettings.Deserialize(childeNode);
                                break;
                            case "DefaultSnapshotViewSettings":
                                this.DefaultSnapshotViewSettings.Deserialize(childeNode);
                                break;
                            case "DefaultDetailedGridColumnState":
                                this.DefaultDetailedGridColumnState.Deserialize(childeNode);
                                break;
                            case "DefaultDetailedGridAdNotesState":
                                this.DefaultDetailedGridAdNotesState.Deserialize(childeNode);
                                break;
                            case "DefaultDetailedGridSlideBulletsState":
                                this.DefaultDetailedGridSlideBulletsState.Deserialize(childeNode);
                                break;
                            case "DefaultDetailedGridSlideHeaderState":
                                this.DefaultDetailedGridSlideHeaderState.Deserialize(childeNode);
                                break;
                            case "DefaultMultiGridColumnState":
                                this.DefaultMultiGridColumnState.Deserialize(childeNode);
                                break;
                            case "DefaultMultiGridAdNotesState":
                                this.DefaultMultiGridAdNotesState.Deserialize(childeNode);
                                break;
                            case "DefaultMultiGridSlideBulletsState":
                                this.DefaultMultiGridSlideBulletsState.Deserialize(childeNode);
                                break;
                            case "DefaultMultiGridSlideHeaderState":
                                this.DefaultMultiGridSlideHeaderState.Deserialize(childeNode);
                                break;
                            case "DefaultCalendarViewSettings":
                                this.DefaultCalendarViewSettings.Deserialize(childeNode);
                                break;
                        }
                    }
                }
            }
            this.Readerships.AddRange(this.PublicationSources.Select(x => x.Clone() as PublicationSource));
        }

        private void LoadLists()
        {
            LoadAdvertisers();
            LoadDecisionMakers();
            LoadPrintStrategy();
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

    public class MechanicalType
    {
        public string Name { get; set; }
        public List<MechanicalItem> Items { get; set; }

        public MechanicalType()
        {
            this.Name = string.Empty;
            this.Items = new List<MechanicalItem>();
        }
    }

    public class MechanicalItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }

        public MechanicalItem()
        {
            this.Name = string.Empty;
            this.Value = string.Empty;
            this.Selected = false;
        }
    }

    public class ShareUnit
    {
        public string RateCard { get; set; }
        public string PercentOfPage { get; set; }
        public string Width { get; set; }
        public string WidthMeasureUnit { get; set; }
        public string Height { get; set; }
        public string HeightMeasureUnit { get; set; }

        public string ShortWidthMeasure
        {
            get
            {
                switch (this.WidthMeasureUnit.ToLower())
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
                switch (this.HeightMeasureUnit.ToLower())
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

        public double WidthValue
        {
            get
            {
                double temp;
                if (double.TryParse(this.Width, out temp))
                    return temp;
                else
                    return 0;
            }
        }

        public double HeightValue
        {
            get
            {
                double temp;
                if (double.TryParse(this.Height, out temp))
                    return temp;
                else
                    return 0;
            }
        }

        public string Dimensions
        {
            get
            {
                return !string.IsNullOrEmpty(this.Width) && !string.IsNullOrEmpty(this.Height) ? (string.Format("{0}{1} x {2}{3}", new object[] { this.WidthValue.ToString("#,##0.00"), this.ShortWidthMeasure, this.HeightValue.ToString("#,##0.00"), this.ShortHeightMeasure })) : string.Empty;
            }
        }

        public ShareUnit()
        {
            this.RateCard = string.Empty;
            this.PercentOfPage = string.Empty;
            this.Width = string.Empty;
            this.WidthMeasureUnit = string.Empty;
            this.Height = string.Empty;
            this.HeightMeasureUnit = string.Empty;
        }
    }

    public class NameCodePair
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public NameCodePair()
        {
            this.Name = string.Empty;
            this.Code = string.Empty;
        }

        public string Serialize()
        {
            StringBuilder xml = new StringBuilder();

            xml.Append(@"<NameCodePair ");
            xml.Append("Name = \"" + this.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.Append("Code = \"" + this.Code.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
            xml.AppendLine(@"/>");

            return xml.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            foreach (XmlAttribute attribute in node.Attributes)
                switch (attribute.Name)
                {
                    case "Name":
                        this.Name = attribute.Value;
                        break;
                    case "Code":
                        this.Code = attribute.Value;
                        break;
                }
        }
    }
}