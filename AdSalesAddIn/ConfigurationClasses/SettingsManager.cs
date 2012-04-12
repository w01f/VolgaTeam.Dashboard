using System;
using System.IO;
using System.Text;
using System.Xml;

namespace AdSalesAddIn.ConfigurationClasses
{
    class SettingsManager
    {
        #region Constant Names
        public const string NBWApplicationManifestFileName = "Manifest.xml";
        public const string RegularDPIRibbonImageFileName = "rbn.png";
        public const string HighDPIRibbonImageFileName = "rbn2.png";
        #endregion

        private static SettingsManager _instance = new SettingsManager();

        #region Path Variables
        private string _sharedSettingsFile = string.Empty;
        private string _dashboardNamePath = string.Empty;
        private string _salesDepotNamePath = string.Empty;
        private string _slidesFolderPath = string.Empty;
        public string NBWApplicationsRootPath { get; set; }
        public string DashboardPath { get; set; }
        public string DashboardLogoPath { get; set; }
        public string SalesDepotExecutablePath { get; set; }
        public string SalesDepotLogoPath { get; set; }
        public string ClientLogosPath { get; set; }
        public string SalesGalleryPath { get; set; }
        public string WebArtPath { get; set; }
        public string LibraryPath { get; set; }
        public string ClipartPath { get; set; }
        public string HelpLinksPath { get; set; }
        public string MinibarPath { get; set; }
        #endregion

        #region Shared Settings
        public string SelectedWizard { get; set; }
        public double SizeHeght { get; set; }
        public double SizeWidth { get; set; }
        public string Orientation { get; set; }
        public bool SlideTemplateEnabled { get; set; }
        #endregion

        #region Application Names
        public string DashboardName { get; set; }
        public string SalesDepotName { get; set; }
        #endregion

        private SettingsManager()
        {
            string programFilesFolderPath = !string.IsNullOrEmpty(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFilesX86)) ? System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFilesX86) : System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles);
            _sharedSettingsFile = string.Format(@"{0}\newlocaldirect.com\xml\app\SharedSettings.xml", programFilesFolderPath);
            _dashboardNamePath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\Tab2Name.xml", programFilesFolderPath);
            _salesDepotNamePath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\SDName.xml", programFilesFolderPath);
            _slidesFolderPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\slides", programFilesFolderPath);

            this.NBWApplicationsRootPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\applications", programFilesFolderPath);
            this.DashboardPath = string.Format(@"{0}\newlocaldirect.com\app\adSALESapp.exe", programFilesFolderPath);
            this.DashboardLogoPath = string.Format(@"{0}\newlocaldirect.com\app", programFilesFolderPath);
            this.SalesDepotExecutablePath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\SalesDepot.exe", programFilesFolderPath);
            this.SalesDepotLogoPath = string.Format(@"{0}\newlocaldirect.com\Sales Depot", programFilesFolderPath);
            this.ClientLogosPath = string.Format(@"{0}\newlocaldirect.com\app\Client Logos\ClientLogos.exe", programFilesFolderPath);
            this.SalesGalleryPath = string.Format(@"{0}\newlocaldirect.com\app\Sales Gallery\SalesGallery.exe", programFilesFolderPath);
            this.WebArtPath = string.Format(@"{0}\newlocaldirect.com\app\Web Art\WebArt.exe", programFilesFolderPath);
            this.LibraryPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\libraries", programFilesFolderPath);
            this.ClipartPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\gallery", programFilesFolderPath);
            this.HelpLinksPath = string.Format(@"{0}\newlocaldirect.com\app\HelpUrls\MinibarHelp.xml", programFilesFolderPath);
            this.MinibarPath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\MiniBar.exe", programFilesFolderPath);

            LoadSharedSettings();
        }

        public static SettingsManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public string SlideFolder
        {
            get
            {
                switch (this.Orientation)
                {
                    case "Landscape":
                        if (this.SizeWidth == 10 && this.SizeHeght == 7.5)
                            return "Slides43";
                        else if (this.SizeWidth == 10.75 && this.SizeHeght == 8.25)
                            return "Slides54";
                        if (this.SizeWidth == 10 && this.SizeHeght == 5.63)
                            return "Slides169";
                        else
                            return "Slides43";
                    case "Portrait":
                        if (this.SizeWidth == 10 && this.SizeHeght == 7.5)
                            return "Slides34";
                        else if (this.SizeWidth == 10.75 && this.SizeHeght == 8.25)
                            return "Slides45";
                        if (this.SizeWidth == 10 && this.SizeHeght == 5.63)
                            return "Slides916";
                        else
                            return "Slides43";
                    default:
                        return "Slides43";
                }
            }
        }

        public string SlideSize
        {
            get
            {
                switch (this.Orientation)
                {
                    case "Landscape":
                        if (this.SizeWidth == 10 && this.SizeHeght == 7.5)
                            return "Landscape 4 x 3";
                        else if (this.SizeWidth == 10.75 && this.SizeHeght == 8.25)
                            return "Landscape 5 x 4";
                        if (this.SizeWidth == 10 && this.SizeHeght == 5.63)
                            return "Landscape 16 x 9";
                        else
                            return "Landscape 4 x 3";
                    case "Portrait":
                        if (this.SizeWidth == 10 && this.SizeHeght == 7.5)
                            return "Portrait 3 x 4";
                        else if (this.SizeWidth == 10.75 && this.SizeHeght == 8.25)
                            return "Portrait 4 x 5";
                        if (this.SizeWidth == 10 && this.SizeHeght == 5.63)
                            return "Portrait 9 x 16";
                        else
                            return "Landscape 4 x 3";
                    default:
                        return "Landscape 4 x 3";
                }
            }
        }

        public bool SlidesReadyFull
        {
            get
            {
                bool result = Directory.Exists(_slidesFolderPath);
                if (result)
                    result = Directory.Exists(Path.Combine(_slidesFolderPath, "Artwork")) &
                             Directory.Exists(Path.Combine(_slidesFolderPath, "Calendar")) &
                             Directory.Exists(Path.Combine(_slidesFolderPath, "Dashboard")) &
                             Directory.Exists(Path.Combine(_slidesFolderPath, "Data")) &
                             Directory.Exists(Path.Combine(_slidesFolderPath, "ExcelOutput03")) &
                             Directory.Exists(Path.Combine(_slidesFolderPath, "ExcelOutput07"));
                return result;
            }
        }

        public void LoadSharedSettings()
        {
            this.SelectedWizard = string.Empty;
            this.SizeWidth = 10;
            this.SizeHeght = 7.5;
            this.Orientation = "Landscape";
            this.SlideTemplateEnabled = false;

            XmlNode node;
            double tempDouble;
            bool tempBool;
            if (File.Exists(_sharedSettingsFile))
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(_sharedSettingsFile);
                }
                catch
                {
                }

                node = document.SelectSingleNode(@"/SharedSettings/SelectedWizard");
                if (node != null)
                    this.SelectedWizard = node.InnerText;
                node = document.SelectSingleNode(@"/SharedSettings/SizeWidth");
                if (node != null)
                {
                    tempDouble = 0;
                    double.TryParse(node.InnerText, out tempDouble);
                    if (tempDouble != 0)
                        this.SizeWidth = tempDouble;
                }
                node = document.SelectSingleNode(@"/SharedSettings/SizeHeght");
                if (node != null)
                {
                    tempDouble = 0;
                    double.TryParse(node.InnerText, out tempDouble);
                    if (tempDouble != 0)
                        this.SizeHeght = tempDouble;
                }
                node = document.SelectSingleNode(@"/SharedSettings/Orientation");
                if (node != null)
                    this.Orientation = node.InnerText;
                node = document.SelectSingleNode(@"/SharedSettings/SlideTemplateEnabled");
                if (node != null)
                {
                    tempBool = false;
                    bool.TryParse(node.InnerText, out tempBool);
                    this.SlideTemplateEnabled = tempBool;
                }
                node = document.SelectSingleNode(@"/SharedSettings/HideAdSchedule");
            }

            LoadDashboardName();
            LoadSalesDepotName();
        }

        public void SaveSharedSettings()
        {
            StringBuilder xml = new StringBuilder();

            xml.AppendLine(@"<SharedSettings>");
            xml.AppendLine(@"<SelectedWizard>" + this.SelectedWizard.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedWizard>");
            xml.AppendLine(@"<SizeHeght>" + this.SizeHeght.ToString() + @"</SizeHeght>");
            xml.AppendLine(@"<SizeWidth>" + this.SizeWidth.ToString() + @"</SizeWidth>");
            xml.AppendLine(@"<Orientation>" + this.Orientation.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Orientation>");
            xml.AppendLine(@"<SlideTemplateEnabled>" + this.SlideTemplateEnabled.ToString() + @"</SlideTemplateEnabled>");
            xml.AppendLine(@"</SharedSettings>");

            using (StreamWriter sw = new StreamWriter(_sharedSettingsFile, false))
            {
                sw.Write(xml);
                sw.Flush();
            }
        }



        private void LoadDashboardName()
        {
            this.DashboardName = "Dashboard";

            XmlNode node;
            if (File.Exists(_dashboardNamePath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(_dashboardNamePath);

                node = document.SelectSingleNode(@"/Tab2Name");
                if (node != null)
                    this.DashboardName = node.InnerText;
            }
        }

        private void LoadSalesDepotName()
        {
            this.SalesDepotName = "Sales Depot";

            XmlNode node;
            if (File.Exists(_salesDepotNamePath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(_salesDepotNamePath);

                node = document.SelectSingleNode(@"/SDName");
                if (node != null)
                    this.SalesDepotName = node.InnerText;
            }
        }
    }
}
