using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace CustomSlidesAddIn.ConfigurationClasses
{
    class SettingsManager
    {
        #region Constant Names
        public const string CustomSlideConfigFileName = "menutext.xml";
        public const string CustomSlideSourceFileName = "slide.ppt";
        public const string RegularDPIRibbonImageFileName = "preview.png";
        public const string HighDPIRibbonImageFileName = "preview2.png";

        public const string Slide43Folder = "4 x 3";
        public const string Slide54Folder = "5 x 4";
        public const string Slide169Folder = "16 x 9";
        public const string Slide34Folder = "3 x 4";
        public const string Slide45Folder = "4 x 5";
        #endregion

        private static SettingsManager _instance = new SettingsManager();

        #region Path Variables
        private string _configFilePath = string.Empty;
        private string _applicationLogoRegularDPIPath = string.Empty;
        private string _applicationLogoHighDPIPath = string.Empty;
        public string CustomSlidesRootPath { get; set; }
        public string HelpLinksPath { get; set; }
        #endregion

        #region Presentation Settings
        public double SizeHeght { get; set; }
        public double SizeWidth { get; set; }
        public string Orientation { get; set; }
        #endregion

        #region Application Settings
        public string ApplicationName { get; set; }
        public Image ApplicationLogo { get; private set; }
        public bool ChangeSizeAutomatically { get; set; }
        public bool HighDPI { get; set; }
        #endregion

        private SettingsManager()
        {
            string programFilesFolderPath = !string.IsNullOrEmpty(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFilesX86)) ? System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFilesX86) : System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles);
            _configFilePath = string.Format(@"{0}\newlocaldirect.com\slides4ppt\Config.xml", programFilesFolderPath);
            _applicationLogoRegularDPIPath = string.Format(@"{0}\newlocaldirect.com\slides4ppt\32.png", programFilesFolderPath);
            _applicationLogoHighDPIPath = string.Format(@"{0}\newlocaldirect.com\slides4ppt\40.png", programFilesFolderPath);

            this.CustomSlidesRootPath = string.Format(@"{0}\newlocaldirect.com\slides4ppt\slides", programFilesFolderPath);
            this.HelpLinksPath = string.Format(@"{0}\newlocaldirect.com\app\HelpUrls\CustomSlidesHelp.xml", programFilesFolderPath);

            this.ApplicationName = "Custom Slides";
            this.HighDPI = BusinessClasses.CommonMethods.IsHighDPI();

            LoadApplicationSettings();

            this.SizeWidth = 10;
            this.SizeHeght = 7.5;
            this.Orientation = "Landscape";
        }

        public static SettingsManager Instance
        {
            get
            {
                return _instance;
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

        public void LoadApplicationSettings()
        {
            XmlNode node;
            bool tempBool;

            if (File.Exists(_configFilePath))
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(_configFilePath);
                }
                catch
                {
                }

                node = document.SelectSingleNode(@"/ApplicationSettings/Name");
                if (node != null)
                    this.ApplicationName = node.InnerText;
                
                node = document.SelectSingleNode(@"/ApplicationSettings/ChangeSizeAutomatically");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.ChangeSizeAutomatically = tempBool;

            }

            if (this.HighDPI)
            {
                if (File.Exists(_applicationLogoHighDPIPath))
                    this.ApplicationLogo = new Bitmap(_applicationLogoHighDPIPath);
            }
            else
            {
                if (File.Exists(_applicationLogoRegularDPIPath))
                    this.ApplicationLogo = new Bitmap(_applicationLogoRegularDPIPath);
            }
        }
    }
}
