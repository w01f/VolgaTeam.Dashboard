using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;

namespace NewBizWiz.Core.Common
{
	public class MasterWizardManager
	{
		public const string CleanslateFileName = @"{0}\Basic Slides\CleanSlate.ppt";
		public const string CoverFileName = @"{0}\Basic Slides\WizCover.ppt";
		public const string GenericCoverFileName = @"{0}\Basic Slides\WizCover2.ppt";
		public const string ContentsFolderPath = @"{0}\Contents Slides";
		public const string ContentsFileName = @"Contents{0}.ppt";
		public const string PageNumbersFileName = @"!pagenumber.ppt";

		public const string AdScheduleSlideFolderName = @"{0}\Newspaper Slides";
		public const string OnlineScheduleSlideFolderName = @"{0}\Online Slides";
		public const string MobileScheduleSlideFolderName = @"{0}\Mobile Slides";
		public const string TVScheduleSlideFolderName = @"{0}\TV Slides";
		public const string RadioScheduleSlideFolderName = @"{0}\Radio Slides";
		public const string CalendarSlideFolderName = @"{0}\Calendar Slides";

		public static string MasterWizardsFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Dashboard", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
		public static string ScheduleBuildersFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\ScheduleBuilders", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

		public static string WatermarkLogoPath = string.Format(@"{0}\newlocaldirect.com\app\dbwatermark.png", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
		public static string VersionLogoPath = string.Format(@"{0}\newlocaldirect.com\app\version.png", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

		private static readonly MasterWizardManager _instance = new MasterWizardManager();

		#region Home Constants
		public const string LeadoffTemplatesFolder = @"{0}\Basic Slides\intro slide";
		public const string LeadOffSlideTemplate = @"intro-{0}.ppt";

		public const string ClientGoalsTemplatesFolder = @"{0}\Basic Slides\needs analysis";
		public const string ClientGoalsSlideTemplate = @"needs-{0}.ppt";

		public const string TargetCustomersTemplatesFolder = @"{0}\Basic Slides\target customer";
		public const string TargetCustomersSlideTemplate = @"target-{0}.ppt";

		public const string SimpleSummaryTemlatesFolder = @"{0}\Basic Slides\closing summary";
		public const string SimpleSummarySlideTemplate = @"closing-{0}.ppt";
		public const string SimpleSummaryTableTemplate = @"product_table_{0}.ppt";
		#endregion

		private MasterWizardManager()
		{
			MasterWizards = new Dictionary<string, MasterWizard>();

			if (File.Exists(WatermarkLogoPath))
				Watermark = new Bitmap(WatermarkLogoPath);

			if (File.Exists(VersionLogoPath))
				Version = new Bitmap(VersionLogoPath);

			Load();
		}

		public Dictionary<string, MasterWizard> MasterWizards { get; set; }
		public Image Watermark { get; set; }
		public Image Version { get; set; }

		public static MasterWizardManager Instance
		{
			get { return _instance; }
		}

		public MasterWizard SelectedWizard { get; set; }

		private void Load()
		{
			var rootFolder = new DirectoryInfo(MasterWizardsFolder);
			if (rootFolder.Exists)
			{
				foreach (DirectoryInfo folder in rootFolder.GetDirectories())
				{
					var masterWizard = new MasterWizard();
					masterWizard.Name = folder.Name;
					masterWizard.Folder = folder;

					var file = new FileInfo(Path.Combine(folder.FullName, folder.Name + ".png"));
					masterWizard.LogoFile = file.Exists ? file : null;

					masterWizard.Init();
					if (!masterWizard.Hide)
						MasterWizards.Add(masterWizard.Name, masterWizard);
				}
			}
		}

		public void SetMasterWizard()
		{
			MasterWizard masterWizard = null;
			MasterWizards.TryGetValue(SettingsManager.Instance.SelectedWizard, out masterWizard);
			SelectedWizard = masterWizard;
		}
	}

	public class MasterWizard
	{
		public string Name { get; set; }
		public DirectoryInfo Folder { get; set; }
		public FileInfo LogoFile { get; set; }
		public bool Hide { get; set; }
		public bool Has43 { get; set; }
		public bool Has54 { get; set; }
		public bool Has34 { get; set; }
		public bool Has45 { get; set; }
		public bool Has169 { get; set; }
		public bool HasSlideTemplate43 { get; set; }
		public bool HasSlideTemplate54 { get; set; }
		public bool HasSlideTemplate34 { get; set; }
		public bool HasSlideTemplate45 { get; set; }
		public bool HasSlideTemplate169 { get; set; }

		public string CleanslateFile
		{
			get { return Path.Combine(Folder.FullName, string.Format(MasterWizardManager.CleanslateFileName, SettingsManager.Instance.SlideFolder)); }
		}

		public string GenericCoverFile
		{
			get { return Path.Combine(Folder.FullName, string.Format(MasterWizardManager.GenericCoverFileName, SettingsManager.Instance.SlideFolder)); }
		}

		public string PageNumbersFile
		{
			get { return Path.Combine(Folder.FullName, string.Format(MasterWizardManager.ContentsFolderPath, SettingsManager.Instance.SlideFolder), MasterWizardManager.PageNumbersFileName); }
		}

		public string ContentsFolder
		{
			get { return Path.Combine(Folder.FullName, string.Format(MasterWizardManager.ContentsFolderPath, SettingsManager.Instance.SlideFolder)); }
		}

		public string CoverFile
		{
			get { return Path.Combine(Folder.FullName, string.Format(MasterWizardManager.CoverFileName, SettingsManager.Instance.SlideFolder)); }
		}
		public string AdScheduleSlideFolder
		{
			get { return Path.Combine(Folder.FullName, string.Format(MasterWizardManager.AdScheduleSlideFolderName, SettingsManager.Instance.SlideFolder)); }
		}
		public string OnlineScheduleSlideFolder
		{
			get { return Path.Combine(Folder.FullName, string.Format(MasterWizardManager.OnlineScheduleSlideFolderName, SettingsManager.Instance.SlideFolder)); }
		}
		public string MobileScheduleSlideFolder
		{
			get { return Path.Combine(Folder.FullName, string.Format(MasterWizardManager.MobileScheduleSlideFolderName, SettingsManager.Instance.SlideFolder)); }
		}

		public string TVScheduleSlideFolder
		{
			get { return Path.Combine(MasterWizardManager.ScheduleBuildersFolder, string.Format(MasterWizardManager.TVScheduleSlideFolderName, SettingsManager.Instance.SlideFolder)); }
		}
		public string RadioScheduleSlideFolder
		{
			get { return Path.Combine(MasterWizardManager.ScheduleBuildersFolder, string.Format(MasterWizardManager.RadioScheduleSlideFolderName, SettingsManager.Instance.SlideFolder)); }
		}

		public string CalendarSlideFolder
		{
			get { return Path.Combine(Folder.FullName, string.Format(MasterWizardManager.CalendarSlideFolderName, SettingsManager.Instance.SlideFolder)); }
		}

		#region Home Folders
		public string LeadoffStatementsFolder
		{
			get { return Path.Combine(Folder.FullName, string.Format(MasterWizardManager.LeadoffTemplatesFolder, SettingsManager.Instance.SlideFolder)); }
		}

		public string ClientGoalsFolder
		{
			get { return Path.Combine(Folder.FullName, string.Format(MasterWizardManager.ClientGoalsTemplatesFolder, SettingsManager.Instance.SlideFolder)); }
		}

		public string TargetCustomersFolder
		{
			get { return Path.Combine(Folder.FullName, string.Format(MasterWizardManager.TargetCustomersTemplatesFolder, SettingsManager.Instance.SlideFolder)); }
		}

		public string SimpleSummaryFolder
		{
			get { return Path.Combine(Folder.FullName, string.Format(MasterWizardManager.SimpleSummaryTemlatesFolder, SettingsManager.Instance.SlideFolder)); }
		}

		public string SimpleSummaryTableFolder
		{
			get { return Path.Combine(SimpleSummaryFolder, "tables"); }
		}

		public string SimpleSummaryTableIconFolder
		{
			get { return Path.Combine(SimpleSummaryTableFolder, "icons"); }
		}
		#endregion

		public void Init()
		{
			LoadSettings();

			Has43 = Directory.Exists(Path.Combine(Folder.FullName, "Slides43"));
			Has54 = Directory.Exists(Path.Combine(Folder.FullName, "Slides54"));
			Has34 = Directory.Exists(Path.Combine(Folder.FullName, "Slides34"));
			Has45 = Directory.Exists(Path.Combine(Folder.FullName, "Slides45"));
			Has169 = Directory.Exists(Path.Combine(Folder.FullName, "Slides169"));
			HasSlideTemplate43 = Has43;
			HasSlideTemplate54 = Has54;
			HasSlideTemplate34 = Has34;
			HasSlideTemplate45 = Has45;
			HasSlideTemplate169 = Has169;
			var file = new FileInfo(Path.Combine(Folder.FullName, Folder.Name + ".png"));
			LogoFile = file.Exists ? file : null;
		}


		private void LoadSettings()
		{
			XmlNode node;
			bool tempBool = false;

			Hide = false;
			string settingsFile = Path.Combine(Folder.FullName, "Settings.xml");
			if (File.Exists(settingsFile))
			{
				var document = new XmlDocument();
				try
				{
					document.Load(settingsFile);
					node = document.SelectSingleNode(@"/settings/hide");
					if (node != null)
						if (bool.TryParse(node.InnerText, out tempBool))
							Hide = tempBool;
				}
				catch { }
			}
		}
	}
}