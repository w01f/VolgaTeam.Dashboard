using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace CalendarBuilder.ConfigurationClasses
{
	internal class SettingsManager
	{
		public const string DefaultBigLogoFileName = @"Default.png";
		public const string DefaultSmallLogoFileName = @"Default2.png";
		public const string DefaultTinyLogoFileName = @"Default3.png";

		private static readonly SettingsManager _instance = new SettingsManager();

		private readonly string _appIDFile = string.Empty;
		private readonly string _defaultAdScheduleFolderPath = string.Empty;
		private readonly string _defaultSaveFolderPath = string.Empty;
		private readonly string _imageFolderPath = string.Empty;
		private readonly string _sharedSettingsFile = string.Empty;

		private SettingsManager()
		{
			_sharedSettingsFile = string.Format(@"{0}\newlocaldirect.com\xml\app\SharedSettings.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			_appIDFile = string.Format(@"{0}\newlocaldirect.com\xml\app\AppID.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			HelpLinksPath = string.Format(@"{0}\newlocaldirect.com\app\HelpUrls\NinjaCalHelp.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			SuccessModelsPath = string.Format(@"{0}\newlocaldirect.com\app\models\CalendarBuilder", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			IconPath = Path.Combine(Application.StartupPath, "icon.ico");

			LoadAppID();
			CheckSaveFolder();
			LoadSharedSettings();

			_defaultSaveFolderPath = Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Outgoing", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)), "AppID-" + AppID.ToString(), @"Saved_Schedules\Calendar Builder");
			if (Directory.Exists(_defaultSaveFolderPath))
				SaveFolder = _defaultSaveFolderPath;
			else
				SaveFolder = Application.StartupPath;

			_defaultAdScheduleFolderPath = Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Outgoing", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)), "AppID-" + AppID.ToString(), @"Saved_Schedules\Ad Schedule Builder");
			if (Directory.Exists(_defaultAdScheduleFolderPath))
				AdScheduleFolder = _defaultAdScheduleFolderPath;
			else
				AdScheduleFolder = Application.StartupPath;

			string listFolder = Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Outgoing", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)), "AppID-" + AppID.ToString(), @"User_lists");
			if (Directory.Exists(listFolder))
				ListFolder = listFolder;
			else
				ListFolder = Application.StartupPath;

			_imageFolderPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Artwork\PRINT\", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

			string folderPath = Path.Combine(_imageFolderPath, "Big Logos");
			if (Directory.Exists(folderPath))
				BigImageFolder = new DirectoryInfo(folderPath);
			else
				BigImageFolder = new DirectoryInfo(Application.StartupPath);

			folderPath = Path.Combine(_imageFolderPath, "Small Logos");
			if (Directory.Exists(folderPath))
				SmallImageFolder = new DirectoryInfo(folderPath);
			else
				SmallImageFolder = new DirectoryInfo(Application.StartupPath);

			folderPath = Path.Combine(_imageFolderPath, "Tiny Logos");
			if (Directory.Exists(folderPath))
				TinyImageFolder = new DirectoryInfo(folderPath);
			else
				TinyImageFolder = new DirectoryInfo(Application.StartupPath);

			folderPath = Path.Combine(_imageFolderPath, "Xtra Tiny Logos");
			if (Directory.Exists(folderPath))
				XtraTinyImageFolder = new DirectoryInfo(folderPath);
			else
				XtraTinyImageFolder = new DirectoryInfo(Application.StartupPath);

			TempPath = string.Format(@"{0}\newlocaldirect.com\Sync\Temp", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			if (!Directory.Exists(TempPath))
				Directory.CreateDirectory(TempPath);

			ViewSettings = new LocalSettings();
		}

		public string SaveFolder { get; set; }
		public string ListFolder { get; set; }
		public DirectoryInfo BigImageFolder { get; set; }
		public DirectoryInfo SmallImageFolder { get; set; }
		public DirectoryInfo TinyImageFolder { get; set; }
		public DirectoryInfo XtraTinyImageFolder { get; set; }
		public string HelpLinksPath { get; set; }
		public string SuccessModelsPath { get; set; }
		public string TempPath { get; set; }
		public string IconPath { get; set; }
		public string AdScheduleFolder { get; set; }

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
				switch (Orientation)
				{
					case "Landscape":
						return SizeWidth.ToString("0.##") + " x " + SizeHeght.ToString("0.##");
					case "Portrait":
						return SizeHeght.ToString("0.##") + " x " + SizeWidth.ToString("0.##");
					default:
						return string.Empty;
				}
			}
		}

		public string SlideFolder
		{
			get
			{
				switch (Orientation)
				{
					case "Landscape":
						if (SizeWidth == 10 && SizeHeght == 7.5)
							return "Slides43";
						else if (SizeWidth == 10.75 && SizeHeght == 8.25)
							return "Slides54";
						if (SizeWidth == 10 && SizeHeght == 5.63)
							return "Slides169";
						else
							return "Slides43";
					case "Portrait":
						if (SizeWidth == 10 && SizeHeght == 7.5)
							return "Slides34";
						else if (SizeWidth == 10.75 && SizeHeght == 8.25)
							return "Slides45";
						if (SizeWidth == 10 && SizeHeght == 5.63)
							return "Slides916";
						else
							return "Slides43";
					default:
						return "Slides43";
				}
			}
		}

		public static SettingsManager Instance
		{
			get { return _instance; }
		}

		private void CheckSaveFolder()
		{
			if (!Directory.Exists(Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Outgoing", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)), "AppID-" + AppID.ToString(), @"Saved_Schedules\Calendar Builder")))
				Directory.CreateDirectory(Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Outgoing", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)), "AppID-" + AppID.ToString(), @"Saved_Schedules\Calendar Builder"));

			if (!Directory.Exists(Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Outgoing", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)), "AppID-" + AppID.ToString(), @"User_lists")))
				Directory.CreateDirectory(Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Outgoing", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)), "AppID-" + AppID.ToString(), @"User_lists"));
		}

		public void LoadSharedSettings()
		{
			XmlNode node;
			double tempDouble;
			bool tempBool;
			if (File.Exists(_sharedSettingsFile))
			{
				var document = new XmlDocument();
				document.Load(_sharedSettingsFile);

				node = document.SelectSingleNode(@"/SharedSettings/SelectedWizard");
				if (node != null)
					SelectedWizard = node.InnerText;
				node = document.SelectSingleNode(@"/SharedSettings/SizeWidth");
				if (node != null)
				{
					tempDouble = 0;
					double.TryParse(node.InnerText, out tempDouble);
					if (tempDouble != 0)
						SizeWidth = tempDouble;
				}
				node = document.SelectSingleNode(@"/SharedSettings/SizeHeght");
				if (node != null)
				{
					tempDouble = 0;
					double.TryParse(node.InnerText, out tempDouble);
					if (tempDouble != 0)
						SizeHeght = tempDouble;
				}
				node = document.SelectSingleNode(@"/SharedSettings/Orientation");
				if (node != null)
					Orientation = node.InnerText;
				node = document.SelectSingleNode(@"/SharedSettings/SlideTemplateEnabled");
				if (node != null)
				{
					tempBool = false;
					bool.TryParse(node.InnerText, out tempBool);
					SlideTemplateEnabled = tempBool;
				}
			}
		}

		private void LoadAppID()
		{
			AppID = Guid.Empty;
			string appIDPath = Path.Combine(Application.StartupPath, _appIDFile);
			if (File.Exists(appIDPath))
			{
				var document = new XmlDocument();
				document.Load(appIDPath);

				XmlNode node = document.SelectSingleNode(@"/AppID");
				if (node != null)
					if (!string.IsNullOrEmpty(node.InnerText))
						AppID = new Guid(node.InnerText);
			}
		}
	}
}