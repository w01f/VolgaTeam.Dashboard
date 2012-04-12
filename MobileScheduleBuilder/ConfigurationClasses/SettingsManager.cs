using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace MobileScheduleBuilder.ConfigurationClasses
{
    class SettingsManager
    {
        private static SettingsManager _instance = new SettingsManager();

        private string _defaultSaveFolderPath = string.Empty;
        private string _sharedSettingsFile = string.Empty;
        private string _appIDFile = string.Empty;

        public string SaveFolder { get; set; }
        public string ListFolder { get; set; }
        public string HelpLinksPath { get; set; }
        public string SuccessModelsPath { get; set; }
        public string TempPath { get; set; }
        public string InventoryImagePath { get; set; }

        public Guid AppID { get; set; }

        public string SelectedWizard { get; set; }
        public double SizeHeght { get; set; }
        public double SizeWidth { get; set; }
        public string Orientation { get; set; }
        public bool SlideTemplateEnabled { get; set; }

        public string Size
        {
            get
            {
                switch (this.Orientation)
                {
                    case "Landscape":
                        if (this.SizeWidth == 10 && this.SizeHeght == 7.5)
                            return "4 x 3";
                        else if (this.SizeWidth == 10.75 && this.SizeHeght == 8.25)
                            return "5 x 4";
                        if (this.SizeWidth == 10 && this.SizeHeght == 5.63)
                            return "16 x 9";
                        else
                            return "4 x 3";
                    case "Portrait":
                        if (this.SizeWidth == 10 && this.SizeHeght == 7.5)
                            return "3 x 4";
                        else if (this.SizeWidth == 10.75 && this.SizeHeght == 8.25)
                            return "4 x 5";
                        if (this.SizeWidth == 10 && this.SizeHeght == 5.63)
                            return "9 x 16";
                        else
                            return "4 x 3";
                    default:
                        return "4 x 3";
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
            this.HelpLinksPath = string.Format(@"{0}\newlocaldirect.com\app\HelpUrls\MobileHelp.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.SuccessModelsPath = string.Format(@"{0}\newlocaldirect.com\app\models\MobileScheduleBuilder", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.InventoryImagePath = string.Format(@"{0}\newlocaldirect.com\app\mobile_ribbon_logo.png", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));

            LoadAppID();
            CheckSaveFolder();
            LoadSharedSettings();

            _defaultSaveFolderPath = Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Outgoing", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles)), "AppID-" + this.AppID.ToString(), @"Saved_Schedules\Mobile Schedule Builder");
            if (Directory.Exists(_defaultSaveFolderPath))
                this.SaveFolder = _defaultSaveFolderPath;
            else
                this.SaveFolder = Application.StartupPath;

            string listFolder = Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Outgoing", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles)), "AppID-" + this.AppID.ToString(), @"User_lists");
            if (Directory.Exists(listFolder))
                this.ListFolder = listFolder;
            else
                this.ListFolder = Application.StartupPath;

            this.TempPath = string.Format(@"{0}\newlocaldirect.com\Sync\Temp", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            if (!Directory.Exists(this.TempPath))
                Directory.CreateDirectory(this.TempPath);
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
            if (!Directory.Exists(Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Outgoing", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles)), "AppID-" + this.AppID.ToString(), @"Saved_Schedules\Mobile Schedule Builder")))
                Directory.CreateDirectory(Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Outgoing", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles)), "AppID-" + this.AppID.ToString(), @"Saved_Schedules\Mobile Schedule Builder"));

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
