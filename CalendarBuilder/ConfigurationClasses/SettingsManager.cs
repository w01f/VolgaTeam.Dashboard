using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace CalendarBuilder.ConfigurationClasses
{
    class SettingsManager
    {
        public const string DefaultBigLogoFileName = @"Default.png";
        public const string DefaultSmallLogoFileName = @"Default2.png";
        public const string DefaultTinyLogoFileName = @"Default3.png";

        private static SettingsManager _instance = new SettingsManager();

        private string _defaultSaveFolderPath = string.Empty;
        private string _sharedSettingsFile = string.Empty;
        private string _appIDFile = string.Empty;
        private string _imageFolderPath = string.Empty;

        public string SaveFolder { get; set; }
        public string ListFolder { get; set; }
        public DirectoryInfo BigImageFolder { get; set; }
        public DirectoryInfo SmallImageFolder { get; set; }
        public DirectoryInfo TinyImageFolder { get; set; }
        public DirectoryInfo XtraTinyImageFolder { get; set; }
        public string HelpLinksPath { get; set; }
        public string SuccessModelsPath { get; set; }
        public string TempPath { get; set; }

        public Guid AppID { get; set; }

        public string SelectedWizard { get; set; }
        public double SizeHeght { get; set; }
        public double SizeWidth { get; set; }
        public string Orientation { get; set; }
        public bool SlideTemplateEnabled { get; set; }

        public LocalSettings ViewSettings { get; private set; }

        public string Size
        {
            get
            {
                switch (this.Orientation)
                {
                    case "Landscape":
                        return this.SizeWidth.ToString("0.##") + " x " + this.SizeHeght.ToString("0.##");
                    case "Portrait":
                        return this.SizeHeght.ToString("0.##") + " x " + this.SizeWidth.ToString("0.##");
                    default:
                        return string.Empty;
                }
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

        private SettingsManager()
        {
            _sharedSettingsFile = string.Format(@"{0}\newlocaldirect.com\xml\app\SharedSettings.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            _appIDFile = string.Format(@"{0}\newlocaldirect.com\xml\app\AppID.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.HelpLinksPath = string.Format(@"{0}\newlocaldirect.com\app\HelpUrls\NinjaCalHelp.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.SuccessModelsPath = string.Format(@"{0}\newlocaldirect.com\app\models\CalendarBuilder", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));

            LoadAppID();
            CheckSaveFolder();
            LoadSharedSettings();

            _defaultSaveFolderPath = Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Outgoing", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles)), "AppID-" + this.AppID.ToString(), @"Saved_Schedules\Calendar Builder");
            if (Directory.Exists(_defaultSaveFolderPath))
                this.SaveFolder = _defaultSaveFolderPath;
            else
                this.SaveFolder = Application.StartupPath;

            string listFolder = Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Outgoing", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles)), "AppID-" + this.AppID.ToString(), @"User_lists");
            if (Directory.Exists(listFolder))
                this.ListFolder = listFolder;
            else
                this.ListFolder = Application.StartupPath;

            _imageFolderPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Artwork\PRINT\", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));

            string folderPath = Path.Combine(_imageFolderPath, "Big Logos");
            if (Directory.Exists(folderPath))
                this.BigImageFolder = new DirectoryInfo(folderPath);
            else
                this.BigImageFolder = new DirectoryInfo(Application.StartupPath);

            folderPath = Path.Combine(_imageFolderPath, "Small Logos");
            if (Directory.Exists(folderPath))
                this.SmallImageFolder = new DirectoryInfo(folderPath);
            else
                this.SmallImageFolder = new DirectoryInfo(Application.StartupPath);

            folderPath = Path.Combine(_imageFolderPath, "Tiny Logos");
            if (Directory.Exists(folderPath))
                this.TinyImageFolder = new DirectoryInfo(folderPath);
            else
                this.TinyImageFolder = new DirectoryInfo(Application.StartupPath);

            folderPath = Path.Combine(_imageFolderPath, "Xtra Tiny Logos");
            if (Directory.Exists(folderPath))
                this.XtraTinyImageFolder = new DirectoryInfo(folderPath);
            else
                this.XtraTinyImageFolder = new DirectoryInfo(Application.StartupPath);

            this.TempPath = string.Format(@"{0}\newlocaldirect.com\Sync\Temp", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            if (!Directory.Exists(this.TempPath))
                Directory.CreateDirectory(this.TempPath);

            this.ViewSettings = new LocalSettings();
        }

        public static SettingsManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private void CheckSaveFolder()
        {
            if (!Directory.Exists(Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Outgoing", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles)), "AppID-" + this.AppID.ToString(), @"Saved_Schedules\Calendar Builder")))
                Directory.CreateDirectory(Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Outgoing", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles)), "AppID-" + this.AppID.ToString(), @"Saved_Schedules\Calendar Builder"));

            if (!Directory.Exists(Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Outgoing", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles)), "AppID-" + this.AppID.ToString(), @"User_lists")))
                Directory.CreateDirectory(Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Outgoing", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles)), "AppID-" + this.AppID.ToString(), @"User_lists"));
        }

        public void LoadSharedSettings()
        {
            XmlNode node;
            double tempDouble;
            bool tempBool;
            if (File.Exists(_sharedSettingsFile))
            {
                XmlDocument document = new XmlDocument();
                document.Load(_sharedSettingsFile);

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
            }
        }

        private void LoadAppID()
        {
            this.AppID = Guid.Empty;
            string appIDPath = Path.Combine(Application.StartupPath, _appIDFile);
            if (File.Exists(appIDPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(appIDPath);

                XmlNode node = document.SelectSingleNode(@"/AppID");
                if (node != null)
                    if (!string.IsNullOrEmpty(node.InnerText))
                        this.AppID = new Guid(node.InnerText);
            }
        }
    }
}
