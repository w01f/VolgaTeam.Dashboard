using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace NewBizWizForm.ConfigurationClasses
{
    class SettingsManager
    {
        private const string DashboardCodeFile = @"dashboard.xml";
        private static SettingsManager _instance = new SettingsManager();

        private string _defaultSaveFolderPath = string.Empty;
        private string _sharedSettingsFile = string.Empty;
        private string _dashboardSettingsFile = string.Empty;
        private string _dashboardNamePath = string.Empty;
        private string _appIDFile = string.Empty;
        public string MinibarApplicationPath { get; set; }
        public string OneDomainApplicationPath { get; set; }
        public string SalesDepotApplicationPath { get; set; }
        public string HelpLinksPath { get; set; }
        public string DashboardSaveFolder { get; set; }
        public int LastUsedLogoIndex { get; set; }
        public Guid AppID { get; set; }

        public int DashboardCode { get; set; }
        public string DashboardName { get; set; }
        public string IconPath { get; set; }

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
            _dashboardSettingsFile = string.Format(@"{0}\newlocaldirect.com\xml\app\DashboardSettings.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            _appIDFile = string.Format(@"{0}\newlocaldirect.com\xml\app\AppID.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            _dashboardNamePath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\Tab2Name.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.MinibarApplicationPath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\MiniBar.exe", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.OneDomainApplicationPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\applications\APP_One_Domain\OneDomain.exe", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.SalesDepotApplicationPath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\SalesDepot.exe", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.HelpLinksPath = string.Format(@"{0}\newlocaldirect.com\app\HelpUrls\DashboardHelp.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.IconPath = string.Format(@"{0}\newlocaldirect.com\app\tab2icon.ico", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.LastUsedLogoIndex = 0;
            this.DashboardCode = 1;
            this.DashboardName = "Schedule APP";
            this.SelectedWizard = string.Empty;
            this.DashboardSaveFolder = string.Empty;
        }

        public static SettingsManager Instance
        {
            get
            {
                return _instance;
            }
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
            LoadAppID();
            InitDashboardSaveFolder();
            LoadDashboardSettings();
            LoadDashdoardCode();
            LoadDashboardName();
            BusinessClasses.MasterWizardManager.Instance.SetMasterWizard();
        }

        public void LoadDashboardSettings()
        {
            XmlNode node;
            if (File.Exists(_dashboardSettingsFile))
            {
                XmlDocument document = new XmlDocument();
                document.Load(_dashboardSettingsFile);

                node = document.SelectSingleNode(@"/DashboardSettings/LastUsedLogoIndex");
                if (node != null)
                {
                    int temp = 0;
                    int.TryParse(node.InnerText, out temp);
                    this.LastUsedLogoIndex = temp;
                }
                node = document.SelectSingleNode(@"/DashboardSettings/SalesRepState");
                if (node != null)
                {
                    ViewSettingsManager.Instance.CoverState.DeserializeSalesRep(node);
                }
            }
        }


        public void SaveDashboardSettings()
        {
            StringBuilder xml = new StringBuilder();

            xml.AppendLine(@"<DashboardSettings>");
            xml.AppendLine(@"<LastUsedLogoIndex>" + this.LastUsedLogoIndex.ToString() + @"</LastUsedLogoIndex>");
            xml.AppendLine(@"<SalesRepState>" + ViewSettingsManager.Instance.CoverState.SerializeSalesRep() + @"</SalesRepState>");
            xml.AppendLine(@"</DashboardSettings>");

            string userConfigurationPath = Path.Combine(Application.StartupPath, _dashboardSettingsFile);
            using (StreamWriter sw = new StreamWriter(userConfigurationPath, false))
            {
                sw.Write(xml);
                sw.Flush();
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
            if (this.AppID.Equals(Guid.Empty))
            {
                this.AppID = Guid.NewGuid();
                SaveAppID();
            }
            CheckAppIdFolders();
        }

        private void SaveAppID()
        {
            StringBuilder xml = new StringBuilder();

            xml.AppendLine(@"<AppID>" + this.AppID.ToString() + @"</AppID>");

            string appIDPath = Path.Combine(Application.StartupPath, _appIDFile);
            using (StreamWriter sw = new StreamWriter(appIDPath, false))
            {
                sw.Write(xml);
                sw.Flush();
            }
        }

        public void CheckStaticFolders(out bool firstRun)
        {
            firstRun = true;
            try
            {
                string localSettingsFolder = string.Format(@"{0}\newlocaldirect.com\xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
                if (!Directory.Exists(localSettingsFolder))
                    Directory.CreateDirectory(localSettingsFolder);
                if (!Directory.Exists(Path.Combine(localSettingsFolder, "app")))
                    Directory.CreateDirectory(Path.Combine(localSettingsFolder, "app"));
                if (!Directory.Exists(Path.Combine(localSettingsFolder, "app_one_domain")))
                    Directory.CreateDirectory(Path.Combine(localSettingsFolder, "app_one_domain"));
                if (!Directory.Exists(Path.Combine(localSettingsFolder, "update_logs")))
                    Directory.CreateDirectory(Path.Combine(localSettingsFolder, "update_logs"));
                if (!Directory.Exists(Path.Combine(localSettingsFolder, "app_pro_slides")))
                    Directory.CreateDirectory(Path.Combine(localSettingsFolder, "app_pro_slides"));
                if (!Directory.Exists(Path.Combine(localSettingsFolder, "app_media_library")))
                    Directory.CreateDirectory(Path.Combine(localSettingsFolder, "app_media_library"));
                if (!Directory.Exists(Path.Combine(localSettingsFolder, "sales depot")))
                    Directory.CreateDirectory(Path.Combine(localSettingsFolder, "sales depot"));
                if (!Directory.Exists(Path.Combine(localSettingsFolder, "sales depot", "Settings")))
                    Directory.CreateDirectory(Path.Combine(localSettingsFolder, "sales depot", "Settings"));

                string syncFolder = string.Format(@"{0}\newlocaldirect.com\sync", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
                if (!Directory.Exists(syncFolder))
                    Directory.CreateDirectory(syncFolder);
                if (!Directory.Exists(Path.Combine(syncFolder, "Incoming")))
                    Directory.CreateDirectory(Path.Combine(syncFolder, "Incoming"));
                if (!Directory.Exists(Path.Combine(syncFolder, "Incoming", "applications")))
                    Directory.CreateDirectory(Path.Combine(syncFolder, "Incoming", "applications"));
                if (!Directory.Exists(Path.Combine(syncFolder, "Incoming", "gallery")))
                    Directory.CreateDirectory(Path.Combine(syncFolder, "Incoming", "gallery"));
                if (!Directory.Exists(Path.Combine(syncFolder, "Incoming", "libraries")))
                    Directory.CreateDirectory(Path.Combine(syncFolder, "Incoming", "libraries"));
                if (!Directory.Exists(Path.Combine(syncFolder, "Incoming", "slides")))
                    Directory.CreateDirectory(Path.Combine(syncFolder, "Incoming", "slides"));
                if (!Directory.Exists(Path.Combine(syncFolder, "Incoming", "update")))
                    Directory.CreateDirectory(Path.Combine(syncFolder, "Incoming", "update"));

                DirectoryInfo slideFolder = new DirectoryInfo(Path.Combine(syncFolder, "Incoming", "slides"));
                if (slideFolder.Exists)
                    if (slideFolder.GetDirectories().Length > 0)
                        firstRun = false;
            }
            catch
            {
            }
        }

        private void CheckAppIdFolders()
        {
            try
            {
                string appIDFolder = string.Format(@"{0}\newlocaldirect.com\sync\Outgoing\AppID-" + this.AppID.ToString(), System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
                if (!Directory.Exists(appIDFolder))
                    Directory.CreateDirectory(appIDFolder);

                string appIDFile = string.Format(@"{0}\newlocaldirect.com\sync\Outgoing\AppID-" + this.AppID.ToString() + ".xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
                if (!File.Exists(appIDFile))
                    File.Create(appIDFile);

                if (!Directory.Exists(Path.Combine(appIDFolder, "power_points")))
                    Directory.CreateDirectory(Path.Combine(appIDFolder, "power_points"));
                if (!Directory.Exists(Path.Combine(appIDFolder, "saved_schedules")))
                    Directory.CreateDirectory(Path.Combine(appIDFolder, "saved_schedules"));
                if (!Directory.Exists(Path.Combine(appIDFolder, "user_data")))
                    Directory.CreateDirectory(Path.Combine(appIDFolder, "user_data"));
            }
            catch
            {
            }
        }

        private void InitDashboardSaveFolder()
        {
            this.DashboardSaveFolder = Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Outgoing\AppID-" + this.AppID.ToString(), System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles)), "saved_dashboard");
            if (!Directory.Exists(this.DashboardSaveFolder))
                Directory.CreateDirectory(this.DashboardSaveFolder);
        }

        private void LoadDashdoardCode()
        {
            string dashboardCodePath = Path.Combine(Application.StartupPath, DashboardCodeFile);
            if (File.Exists(dashboardCodePath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(dashboardCodePath);

                XmlNode node = document.SelectSingleNode(@"/Settings/dashboard/DashboardCode");
                if (node != null)
                {
                    int temp = 1;
                    int.TryParse(node.InnerText, out temp);
                    this.DashboardCode = temp;
                }
            }
        }

        private void LoadDashboardName()
        {
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
    }
}
