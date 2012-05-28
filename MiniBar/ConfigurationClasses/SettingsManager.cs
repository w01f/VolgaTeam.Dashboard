using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace MiniBar.ConfigurationClasses
{
    class SettingsManager
    {
        #region Constant Names
        public const string RegularSyncName = @"adSync4.exe";
        public const string SilentSyncName = @"adSync5.exe";
        public const string SyncSettingsFileName = @"syncfile.xml";
        public const string NBWApplicationManifestFileName = "Manifest.xml";
        #endregion

        private static SettingsManager _instance = new SettingsManager();

        #region Path Variables
        private string _defaultSaveFolderPath = string.Empty;
        private string _sharedSettingsFile = string.Empty;
        private string _minibarSettingsFile = string.Empty;
        private string _minibarAppSettingsFile = string.Empty;
        private string _webcastSettingsFile = string.Empty;
        private string _dashboardNamePath = string.Empty;
        private string _salesDepotNamePath = string.Empty;
        private string _appIDFile = string.Empty;
        private string _slidesFolderPath = string.Empty;
        private string _approvedLibrariesFile = string.Empty;
        public string SyncFilesSourcePath { get; set; }
        public string NBWApplicationsRootPath { get; set; }
        public string DashboardPath { get; set; }
        public string DashboardLogoPath { get; set; }
        public string DashboardIconPath { get; set; }
        public string SalesDepotExecutablePath { get; set; }
        public string SalesDepotRemoteRootPath { get; set; }
        public string SalesDepotLogoPath { get; set; }
        public string SalesDepotIconPath { get; set; }
        public string ClientLogosPath { get; set; }
        public string SalesGalleryPath { get; set; }
        public string WebArtPath { get; set; }
        public string LibraryPath { get; set; }
        public string ClipartPath { get; set; }
        public string ResetPath { get; set; }
        public string SyncSettingsFolderPath { get; set; }
        public string HelpLinksPath { get; set; }
        public string MinibarLoaderPath { get; set; }
        public string PowerPointLoaderPath { get; set; }
        public string ServiceDataFilePath { get; set; }
        #endregion

        public Guid AppID { get; set; }

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

        #region Minibar Settings
        private DateTime _nextSync = DateTime.Now;
        public DateTime LastSync { get; set; }
        public DateTime NextSync
        {
            get
            {
                DateTime now = DateTime.Now;
                DateTime next = new DateTime(now.Year, now.Month, now.Day, this.SyncHourly ? now.Hour : _nextSync.Hour, _nextSync.Minute, _nextSync.Second);
                if (next < now)
                    return this.SyncHourly ? next.AddHours(1) : next.AddDays(1);
                else
                    return next;
            }
        }
        public bool SyncHourly { get; set; }
        public bool OwnControl { get; set; }
        public bool AutoRunNormal { get; set; }
        public bool AutoRunHidden { get; set; }
        public bool AutoRunFloat { get; set; }
        public bool HideAll { get; set; }
        public bool HideSpecificProgram { get; set; }
        public bool HideSpecificProgramMaximized { get; set; }
        public bool VisiblePowerPoint { get; set; }
        public bool VisiblePowerPointMaximaized { get; set; }
        public bool CloseShutdown { get; set; }
        public bool CloseHide { get; set; }
        public bool CloseFloat { get; set; }
        public bool OnPrimaryScreen { get; set; }
        public int FloaterLeft { get; set; }
        public int FloaterTop { get; set; }
        public List<string> PrimaryApplications { get; set; }
        public bool QuickRetraction { get; set; }
        #endregion

        #region WebcastSettings
        public string Location { get; set; }
        public List<string> MeetingIDs { get; set; }
        #endregion

        public bool UseRemoteSalesDepot { get; set; }

        private SettingsManager()
        {
            _sharedSettingsFile = string.Format(@"{0}\newlocaldirect.com\xml\app\SharedSettings.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            _minibarSettingsFile = string.Format(@"{0}\newlocaldirect.com\xml\app\MinibarSettings.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            _webcastSettingsFile = string.Format(@"{0}\newlocaldirect.com\app\Minibar\webcast.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            _appIDFile = string.Format(@"{0}\newlocaldirect.com\xml\app\AppID.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            _dashboardNamePath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\Tab2Name.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            _salesDepotNamePath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\SDName.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            _slidesFolderPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\slides", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            _approvedLibrariesFile = string.Format(@"{0}\newlocaldirect.com\Sales Depot\ApprovedLibraries.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));

            this.SyncFilesSourcePath = string.Format(@"{0}\newlocaldirect.com\app\adsync_patch", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.NBWApplicationsRootPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\applications", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.DashboardPath = string.Format(@"{0}\newlocaldirect.com\app\adSALESapp.exe", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.DashboardLogoPath = string.Format(@"{0}\newlocaldirect.com\app\tab2btn.png", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.DashboardIconPath = string.Format(@"{0}\newlocaldirect.com\app\tab2icon.ico", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.SalesDepotExecutablePath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\SalesDepot.exe", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.SalesDepotRemoteRootPath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\Remote Libraries", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.SalesDepotLogoPath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\sdbutton.png", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.SalesDepotIconPath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\sdicon.ico", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.ClientLogosPath = string.Format(@"{0}\newlocaldirect.com\app\Client Logos\ClientLogos.exe", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.SalesGalleryPath = string.Format(@"{0}\newlocaldirect.com\app\Sales Gallery\SalesGallery.exe", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.WebArtPath = string.Format(@"{0}\newlocaldirect.com\app\Web Art\WebArt.exe", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.LibraryPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\libraries", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.ClipartPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\gallery", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.ResetPath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\Reset.exe", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.SyncSettingsFolderPath = string.Format(@"{0}\newlocaldirect.com\!Update_Settings", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.HelpLinksPath = string.Format(@"{0}\newlocaldirect.com\app\HelpUrls\MinibarHelp.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.MinibarLoaderPath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\MiniBarLoader.exe", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.PowerPointLoaderPath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\PowerPointLoader.exe", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
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

            this.PrimaryApplications = new List<string>();
            this.PrimaryApplications.AddRange(new string[]{"excel",
                                                            "winword",
                                                            "iexplore",
                                                            "skype",
                                                            "ois",
                                                            "chrome",
                                                            "safari",
                                                            "firefox",
                                                            "AcroRd32",
                                                            "vmware",
                                                            "SalesDepot",
                                                            "Outlook",
                                                            "Filezilla",
                                                            "OneDomain",
                                                            "PWConsole",
                                                            "MediaOffice"});
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

            LoadAppID();
            LoadDashboardName();
            LoadSalesDepotName();
            LoadApprovedLibraries();
            LoadWebcastSettings();
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

        private void LoadAppID()
        {
            this.AppID = Guid.Empty;
            string appIDPath = Path.Combine(Application.StartupPath.ToLower().Replace(@"\minibar", ""), _appIDFile);
            if (File.Exists(appIDPath))
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(appIDPath);
                }
                catch
                {
                }

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
            this.ServiceDataFilePath = string.Format(@"{0}\newlocaldirect.com\sync\Outgoing\AppID-" + this.AppID.ToString() + ".xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            BusinessClasses.ServiceDataManager.Instance.LoadData();
            CheckAppIDFolders();
        }

        private void SaveAppID()
        {
            StringBuilder xml = new StringBuilder();

            xml.AppendLine(@"<AppID>" + this.AppID.ToString() + @"</AppID>");

            string appIDPath = Path.Combine(Application.StartupPath.ToLower().Replace(@"\minibar", ""), _appIDFile);
            using (StreamWriter sw = new StreamWriter(appIDPath, false))
            {
                sw.Write(xml);
                sw.Flush();
                sw.Close();
            }
        }

        public void CreateStaticFolders()
        {
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
            }
            catch
            {
            }
        }

        public void DeleteStaticFolders()
        {
            string localSettingsFolder = string.Format(@"{0}\newlocaldirect.com\xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            foreach (DirectoryInfo xmlFolder in (new DirectoryInfo(localSettingsFolder)).GetDirectories())
                xmlFolder.Delete(true);

            string incomingFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            foreach (DirectoryInfo incomingSubFolder in (new DirectoryInfo(incomingFolder)).GetDirectories())
                incomingSubFolder.Delete(true);
        }

        private void CheckAppIDFolders()
        {
            try
            {
                string appIDFolder = string.Format(@"{0}\newlocaldirect.com\sync\Outgoing\AppID-" + this.AppID.ToString(), System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
                if (!Directory.Exists(appIDFolder))
                    Directory.CreateDirectory(appIDFolder);

                if (!Directory.Exists(Path.Combine(appIDFolder, "power_points")))
                    Directory.CreateDirectory(Path.Combine(appIDFolder, "power_points"));
                if (!Directory.Exists(Path.Combine(appIDFolder, "saved_schedules")))
                    Directory.CreateDirectory(Path.Combine(appIDFolder, "saved_schedules"));
                if (!Directory.Exists(Path.Combine(appIDFolder, "saved_dashboard")))
                    Directory.CreateDirectory(Path.Combine(appIDFolder, "saved_dashboard"));
                if (!Directory.Exists(Path.Combine(appIDFolder, "user_data")))
                    Directory.CreateDirectory(Path.Combine(appIDFolder, "user_data"));
            }
            catch
            {
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

        private void LoadApprovedLibraries()
        {
            this.UseRemoteSalesDepot = true;
            if (File.Exists(_approvedLibrariesFile))
            {
                XmlDocument document = new XmlDocument();
                document.Load(_approvedLibrariesFile);

                XmlNode node = document.SelectSingleNode(@"/ApprovedLibraries");
                if (node != null)
                    foreach (XmlNode userNode in node.ChildNodes)
                        if (userNode.Name.Equals("User"))
                        {
                            string userName = string.Empty;
                            bool useRemoteLibraries = false;
                            foreach (XmlAttribute attribute in userNode.Attributes)
                            {
                                switch (attribute.Name)
                                {
                                    case "Name":
                                        userName = attribute.Value;
                                        break;
                                    case "UseRemoteLibraries":
                                        bool.TryParse(attribute.Value, out useRemoteLibraries);
                                        break;
                                }
                            }
                            if (userName.Equals(Environment.UserName))
                            {
                                this.UseRemoteSalesDepot = useRemoteLibraries;
                                break;
                            }
                        }
            }
        }

        public void LoadMinibarSettings()
        {
            lock (AppManager.Locker)
            {
                this.LastSync = DateTime.Now;
                this.SyncHourly = false;
                this.OwnControl = false;
                this.OnPrimaryScreen = true;
                this.QuickRetraction = true;
                this.FloaterLeft = 0;
                this.FloaterTop = 0;

                XmlNode node;
                DateTime tempDateTime;
                bool tempBool;
                int tempInt;
                if (File.Exists(_minibarSettingsFile))
                {
                    XmlDocument document = new XmlDocument();
                    try
                    {
                        document.Load(_minibarSettingsFile);
                    }
                    catch
                    {
                    }

                    node = document.SelectSingleNode(@"/MinibarSettings/LastSync");
                    if (node != null)
                        if (DateTime.TryParse(node.InnerText, out tempDateTime))
                            this.LastSync = tempDateTime;
                    node = document.SelectSingleNode(@"/MinibarSettings/SyncHourly");
                    if (node != null)
                        if (bool.TryParse(node.InnerText, out tempBool))
                            this.SyncHourly = tempBool;
                    node = document.SelectSingleNode(@"/MinibarSettings/OwnControl");
                    if (node != null)
                        if (bool.TryParse(node.InnerText, out tempBool))
                            this.OwnControl = tempBool;
                    node = document.SelectSingleNode(@"/MinibarSettings/OnPrimaryScreen");
                    if (node != null)
                        if (bool.TryParse(node.InnerText, out tempBool))
                            this.OnPrimaryScreen = tempBool;
                    node = document.SelectSingleNode(@"/MinibarSettings/FloaterLeft");
                    if (node != null)
                        if (int.TryParse(node.InnerText, out tempInt))
                            this.FloaterLeft = tempInt;
                    node = document.SelectSingleNode(@"/MinibarSettings/FloaterTop");
                    if (node != null)
                        if (int.TryParse(node.InnerText, out tempInt))
                            this.FloaterTop = tempInt;
                    //node = document.SelectSingleNode(@"/MinibarSettings/QuickRetraction");
                    //if (node != null)
                    //    if (bool.TryParse(node.InnerText, out tempBool))
                    //        this.QuickRetraction = tempBool;
                }

                if (File.Exists(Path.Combine(this.SyncSettingsFolderPath, SyncSettingsFileName)))
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(Path.Combine(this.SyncSettingsFolderPath, SyncSettingsFileName));

                    node = document.SelectSingleNode(@"/Settings/MediaProperty/SyncTime");
                    if (node != null)
                    {
                        tempDateTime = DateTime.Now;
                        DateTime.TryParse(node.InnerText, out tempDateTime);
                        _nextSync = tempDateTime;
                    }
                }
            }
        }

        public void LoadMinibarApplicationSettings()
        {
            lock (AppManager.Locker)
            {
                this.AutoRunNormal = true;
                this.AutoRunHidden = false;
                this.AutoRunFloat = false;
                this.HideAll = true;
                this.HideSpecificProgram = false;
                this.HideSpecificProgramMaximized = false;
                this.VisiblePowerPoint = false;
                this.VisiblePowerPointMaximaized = false;
                this.CloseShutdown = false;
                this.CloseHide = true;
                this.CloseFloat = false;

                XmlNode node;
                bool tempBool;
                if (this.OwnControl)
                    _minibarAppSettingsFile = string.Format(@"{0}\newlocaldirect.com\xml\app\MinibarAppSettings.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
                else
                    _minibarAppSettingsFile = string.Format(@"{0}\newlocaldirect.com\app\Minibar\MinibarAppSettings.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));

                if (File.Exists(_minibarAppSettingsFile))
                {
                    XmlDocument document = new XmlDocument();
                    try
                    {
                        document.Load(_minibarAppSettingsFile);
                    }
                    catch
                    {
                    }

                    node = document.SelectSingleNode(@"/MinibarSettings/AutoRunNormal");
                    if (node != null)
                        if (bool.TryParse(node.InnerText, out tempBool))
                            this.AutoRunNormal = tempBool;
                    node = document.SelectSingleNode(@"/MinibarSettings/AutoRunHidden");
                    if (node != null)
                        if (bool.TryParse(node.InnerText, out tempBool))
                            this.AutoRunHidden = tempBool;
                    node = document.SelectSingleNode(@"/MinibarSettings/AutoRunFloat");
                    if (node != null)
                        if (bool.TryParse(node.InnerText, out tempBool))
                            this.AutoRunFloat = tempBool;

                    node = document.SelectSingleNode(@"/MinibarSettings/HideAll");
                    if (node != null)
                        if (bool.TryParse(node.InnerText, out tempBool))
                            this.HideAll = tempBool;
                    node = document.SelectSingleNode(@"/MinibarSettings/HideSpecificProgram");
                    if (node != null)
                        if (bool.TryParse(node.InnerText, out tempBool))
                            this.HideSpecificProgram = tempBool;
                    node = document.SelectSingleNode(@"/MinibarSettings/HideSpecificProgramMaximized");
                    if (node != null)
                        if (bool.TryParse(node.InnerText, out tempBool))
                            this.HideSpecificProgramMaximized = tempBool;
                    node = document.SelectSingleNode(@"/MinibarSettings/VisiblePowerPoint");
                    if (node != null)
                        if (bool.TryParse(node.InnerText, out tempBool))
                            this.VisiblePowerPoint = tempBool;
                    node = document.SelectSingleNode(@"/MinibarSettings/VisiblePowerPointMaximaized");
                    if (node != null)
                        if (bool.TryParse(node.InnerText, out tempBool))
                            this.VisiblePowerPointMaximaized = tempBool;

                    node = document.SelectSingleNode(@"/MinibarSettings/CloseShutdown");
                    if (node != null)
                        if (bool.TryParse(node.InnerText, out tempBool))
                            this.CloseShutdown = tempBool;
                    node = document.SelectSingleNode(@"/MinibarSettings/CloseHide");
                    if (node != null)
                        if (bool.TryParse(node.InnerText, out tempBool))
                            this.CloseHide = tempBool;
                    node = document.SelectSingleNode(@"/MinibarSettings/CloseFloat");
                    if (node != null)
                        if (bool.TryParse(node.InnerText, out tempBool))
                            this.CloseFloat = tempBool;
                    node = document.SelectSingleNode(@"/MinibarSettings/Applications");
                    if (node != null)
                    {
                        if (node.ChildNodes.Count > 0)
                            this.PrimaryApplications.Clear();
                        foreach (XmlNode childNode in node.ChildNodes)
                            switch (childNode.Name)
                            {
                                case "Application":
                                    this.PrimaryApplications.Add(childNode.InnerText);
                                    break;
                            }
                    }
                }
            }
        }

        public void SaveMinibarSettings()
        {
            StringBuilder xml = new StringBuilder();

            xml.AppendLine(@"<MinibarSettings>");
            xml.AppendLine(@"<LastSync>" + this.LastSync.ToString() + @"</LastSync>");
            xml.AppendLine(@"<SyncHourly>" + this.SyncHourly.ToString() + @"</SyncHourly>");
            xml.AppendLine(@"<OwnControl>" + this.OwnControl.ToString() + @"</OwnControl>");
            xml.AppendLine(@"<OnPrimaryScreen>" + this.OnPrimaryScreen.ToString() + @"</OnPrimaryScreen>");
            xml.AppendLine(@"<FloaterTop>" + this.FloaterTop.ToString() + @"</FloaterTop>");
            xml.AppendLine(@"<FloaterLeft>" + this.FloaterLeft.ToString() + @"</FloaterLeft>");
            xml.AppendLine(@"<QuickRetraction>" + this.QuickRetraction.ToString() + @"</QuickRetraction>");
            xml.AppendLine(@"</MinibarSettings>");

            using (StreamWriter sw = new StreamWriter(_minibarSettingsFile, false))
            {
                sw.Write(xml);
                sw.Flush();
            }

            if (this.OwnControl)
            {
                xml.Clear();
                xml.AppendLine(@"<MinibarSettings>");
                xml.AppendLine(@"<AutoRunNormal>" + this.AutoRunNormal.ToString() + @"</AutoRunNormal>");
                xml.AppendLine(@"<AutoRunHidden>" + this.AutoRunHidden.ToString() + @"</AutoRunHidden>");
                xml.AppendLine(@"<AutoRunFloat>" + this.AutoRunFloat.ToString() + @"</AutoRunFloat>");

                xml.AppendLine(@"<HideAll>" + this.HideAll.ToString() + @"</HideAll>");
                xml.AppendLine(@"<HideSpecificProgram>" + this.HideSpecificProgram.ToString() + @"</HideSpecificProgram>");
                xml.AppendLine(@"<HideSpecificProgramMaximized>" + this.HideSpecificProgramMaximized.ToString() + @"</HideSpecificProgramMaximized>");
                xml.AppendLine(@"<VisiblePowerPoint>" + this.VisiblePowerPoint.ToString() + @"</VisiblePowerPoint>");
                xml.AppendLine(@"<VisiblePowerPointMaximaized>" + this.VisiblePowerPointMaximaized.ToString() + @"</VisiblePowerPointMaximaized>");

                xml.AppendLine(@"<CloseShutdown>" + this.CloseShutdown.ToString() + @"</CloseShutdown>");
                xml.AppendLine(@"<CloseHide>" + this.CloseHide.ToString() + @"</CloseHide>");
                xml.AppendLine(@"<CloseFloat>" + this.CloseFloat.ToString() + @"</CloseFloat>");

                xml.AppendLine(@"<Applications>");
                foreach (string application in this.PrimaryApplications)
                    xml.AppendLine(@"<Application>" + application.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Application>");
                xml.AppendLine(@"</Applications>");
                xml.AppendLine(@"</MinibarSettings>");

                _minibarAppSettingsFile = string.Format(@"{0}\newlocaldirect.com\xml\app\MinibarAppSettings.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
                using (StreamWriter sw = new StreamWriter(_minibarAppSettingsFile, false))
                {
                    sw.Write(xml);
                    sw.Flush();
                }
            }
        }

        public void LoadWebcastSettings()
        {
            this.Location = string.Empty;
            this.MeetingIDs = new List<string>();

            XmlNode node;
            if (File.Exists(_webcastSettingsFile))
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(_webcastSettingsFile);
                }
                catch
                {
                }
                node = document.SelectSingleNode(@"/Webcast");
                if (node != null)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                        switch (childNode.Name)
                        {
                            case "Location":
                                this.Location = childNode.InnerText;
                                break;
                            case "MeetingID":
                                this.MeetingIDs.Add(childNode.InnerText);
                                break;
                        }
                }
            }
        }
    }
}
