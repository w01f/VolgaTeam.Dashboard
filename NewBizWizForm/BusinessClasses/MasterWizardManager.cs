using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace NewBizWizForm.BusinessClasses
{
    class MasterWizardManager
    {
        public const string CleanslateFileName = @"{0}\Basic Slides\CleanSlate.ppt";
        public const string CoverFileName = @"{0}\Basic Slides\WizCover.ppt";
        public const string GenericCoverFileName = @"{0}\Basic Slides\WizCover2.ppt";

        public const string AdScheduleSlideFolderName = @"{0}\Newspaper Slides";
        public const string OnlineScheduleSlideFolderName = @"{0}\Online Slides";
        public const string MobileScheduleSlideFolderName = @"{0}\Mobile Slides";
        public const string TVScheduleSlideFolderName = @"{0}\TV Slides";
        public const string RadioScheduleSlideFolderName = @"{0}\Radio Slides";
        public const string CalendarSlideFolderName = @"{0}\Calendar Slides";

        #region Home Constants
        public const string LeadoffTemplatesFolder = @"{0}\Basic Slides\intro slide";
        public const string LeadOffSlideTemplate = @"intro-{0}.ppt";

        public const string ClientGoalsTemplatesFolder = @"{0}\Basic Slides\needs analysis";
        public const string ClientGoalsSlideTemplate = @"needs-{0}.ppt";

        public const string TargetCustomersTemplatesFolder = @"{0}\Basic Slides\target customer";
        public const string TargetCustomersSlideTemplate = @"target-{0}.ppt";

        public const string SimpleSummaryTemlatesFolder = @"{0}\Basic Slides\closing summary";
        public const string SimpleSummarySlideTemplate = @"closing-{0}.ppt";
        #endregion

        public static string MasterWizardsFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Dashboard", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
        public static string ScheduleBuildersFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\ScheduleBuilders", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));

        public static string NoRibbonLogoPath = string.Format(@"{0}\newlocaldirect.com\app\no_ribbon_logo.png", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
        public static string RibbonLogoPath = string.Format(@"{0}\newlocaldirect.com\app\ribbon_logo.png", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
        public static string WatermarkLogoPath = string.Format(@"{0}\newlocaldirect.com\app\dbwatermark.png", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
        public static string VersionLogoPath = string.Format(@"{0}\newlocaldirect.com\app\version.png", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));

        private static MasterWizardManager _instance = new MasterWizardManager();
        private MasterWizard _selectedWizard = null;
        public Dictionary<string, MasterWizard> MasterWizards { get; set; }
        public Image DefaultLogo { get; set; }
        public Image Watermark { get; set; }
        public Image Version { get; set; }


        private MasterWizardManager()
        {
            this.MasterWizards = new Dictionary<string, MasterWizard>();
            if (File.Exists(RibbonLogoPath))
                this.DefaultLogo = new Bitmap(RibbonLogoPath);
            else if (File.Exists(NoRibbonLogoPath))
                this.DefaultLogo = new Bitmap(NoRibbonLogoPath);
            else 
                this.DefaultLogo = Properties.Resources.MasterWizardLogo;

            if (File.Exists(WatermarkLogoPath))
                this.Watermark = new Bitmap(WatermarkLogoPath);

            if (File.Exists(VersionLogoPath))
                this.Version = new Bitmap(VersionLogoPath);

            Load();
        }

        public static MasterWizardManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public MasterWizard SelectedWizard
        {
            get
            {
                return _selectedWizard;
            }
            set
            {
                _selectedWizard = value;
            }
        }

        private void Load()
        {
            DirectoryInfo rootFolder = new DirectoryInfo(MasterWizardsFolder);
            if (rootFolder.Exists)
            {
                foreach (DirectoryInfo folder in rootFolder.GetDirectories())
                {
                    MasterWizard masterWizard = new MasterWizard();
                    masterWizard.Name = folder.Name;
                    masterWizard.Folder = folder;

                    FileInfo file = new FileInfo(Path.Combine(folder.FullName, folder.Name + ".png"));
                    masterWizard.LogoFile = file.Exists ? file : null;

                    this.MasterWizards.Add(masterWizard.Name, masterWizard);
                }
            }
        }

        public void SetMasterWizard()
        { 
            BusinessClasses.MasterWizard masterWizard = null;
            this.MasterWizards.TryGetValue(ConfigurationClasses.SettingsManager.Instance.SelectedWizard, out masterWizard);
            this.SelectedWizard = masterWizard;
        }
    }

    public class MasterWizard
    {
        public string Name { get; set; }
        public DirectoryInfo Folder { get; set; }
        public FileInfo LogoFile { get; set; }

        public string CleanslateFile
        {
            get
            {
                return Path.Combine(this.Folder.FullName,string.Format(MasterWizardManager.CleanslateFileName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }
        public string CoverFile
        {
            get
            {
                return Path.Combine(this.Folder.FullName,string.Format(MasterWizardManager.CoverFileName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }
        public string GenericCoverFile
        {
            get
            {
                return Path.Combine(this.Folder.FullName,string.Format(MasterWizardManager.GenericCoverFileName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }
        
        public string AdScheduleSlideFolder
        {
            get
            {
                return Path.Combine(this.Folder.FullName, string.Format(MasterWizardManager.AdScheduleSlideFolderName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }
        public string OnlineScheduleSlideFolder
        {
            get
            {
                return Path.Combine(this.Folder.FullName, string.Format(MasterWizardManager.OnlineScheduleSlideFolderName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }
        public string MobileScheduleSlideFolder
        {
            get
            {
                return Path.Combine(this.Folder.FullName, string.Format(MasterWizardManager.MobileScheduleSlideFolderName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }

        public string TVScheduleSlideFolder
        {
            get
            {
                return Path.Combine(MasterWizardManager.ScheduleBuildersFolder, string.Format(MasterWizardManager.TVScheduleSlideFolderName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }
        public string RadioScheduleSlideFolder
        {
            get
            {
                return Path.Combine(MasterWizardManager.ScheduleBuildersFolder, string.Format(MasterWizardManager.RadioScheduleSlideFolderName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }

        public string CalendarSlideFolder
        {
            get
            {
                return Path.Combine(this.Folder.FullName, string.Format(MasterWizardManager.CalendarSlideFolderName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }

        #region Home Folders
        public string LeadoffStatementsFolder
        {
            get
            {
                return Path.Combine(this.Folder.FullName,string.Format(MasterWizardManager.LeadoffTemplatesFolder, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }

        public string ClientGoalsFolder
        {
            get
            {
                return Path.Combine(this.Folder.FullName,string.Format(MasterWizardManager.ClientGoalsTemplatesFolder, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }

        public string TargetCustomersFolder
        {
            get
            {
                return Path.Combine(this.Folder.FullName,string.Format(MasterWizardManager.TargetCustomersTemplatesFolder, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }

        public string SimpleSummaryFolder
        {
            get
            {
                return Path.Combine(this.Folder.FullName,string.Format(MasterWizardManager.SimpleSummaryTemlatesFolder, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }
        #endregion
    }
}
