using System;
using System.IO;
using System.Text;
using System.Xml;

namespace NewBizWiz.Core.Common
{
	public class SettingsManager
	{
		private static SettingsManager _instance;
		private readonly string _appIDFile = string.Empty;
		private readonly string _sharedSettingsFile = string.Empty;
		private readonly string _dashboardCodeFilePath = String.Empty;

		private SettingsManager()
		{
			SettingsPath = String.Format(@"{0}\newlocaldirect.com\xml\app", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			_sharedSettingsFile = Path.Combine(SettingsPath, "SharedSettings.xml");
			_dashboardCodeFilePath = String.Format(@"{0}\newlocaldirect.com\app\dashboard.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			_appIDFile = Path.Combine(SettingsPath, "AppID.xml");
			TempPath = String.Format(@"{0}\newlocaldirect.com\Sync\Temp", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			if (!Directory.Exists(TempPath))
				Directory.CreateDirectory(TempPath);
			SharedListFolder = String.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Data", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			PowerPointLoaderPath = String.Format(@"{0}\newlocaldirect.com\app\PPTLAUNCHER\PPTLAUNCH.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			ThemeCollectionPath = String.Format(@"{0}\newlocaldirect.com\sync\Incoming\slides\SellerPointThemes", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			SlideMastersPath = String.Format(@"{0}\newlocaldirect.com\sync\Incoming\slides\SlidesTab", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			RateCardPath = String.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\RateCard", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			HelpBrowserSettingsPath = String.Format(@"{0}\newlocaldirect.com\app\HelpUrls\!Help_Browser.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			DashboardDefaultLogoPath = String.Format(@"{0}\newlocaldirect.com\app\home.png", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			SelectedWizard = String.Empty;

			DashboardName = "6 Minute Seller";
			DashboardCode = String.Empty;

			SelectedWizard = String.Empty;

			LoadSharedSettings();
		}

		public static SettingsManager Instance
		{
			get
			{
				if (_instance == null)
					_instance = new SettingsManager();
				return _instance;
			}
		}

		public string SettingsPath { get; set; }
		public string TempPath { get; set; }
		public string PowerPointLoaderPath { get; set; }
		public string SharedListFolder { get; set; }
		public string ThemeCollectionPath { get; set; }
		public string SlideMastersPath { get; set; }
		public string ActivityDataPath { get; set; }
		public string RateCardPath { get; set; }
		public string HelpBrowserSettingsPath { get; set; }
		public string OutgoingFolderPath { get; set; }

		public Guid AppID { get; set; }

		public string SelectedWizard { get; set; }
		public double SizeHeght { get; set; }
		public double SizeWidth { get; set; }
		public string Orientation { get; set; }

		public string DashboardName { get; set; }
		public string DashboardCode { get; set; }
		public string DashboardDefaultLogoPath { get; set; }
		
		public string Size
		{
			get
			{
				switch (Orientation)
				{
					case "Landscape":
						if (SizeWidth == 10 && SizeHeght == 7.5)
							return "4 x 3";
						if (SizeWidth == 10.75 && SizeHeght == 8.25)
							return "5 x 4";
						if (SizeWidth == 13 && SizeHeght == 7.32)
							return "16 x 9";
						return "4 x 3";
					case "Portrait":
						if (SizeWidth == 7.5 && SizeHeght == 10)
							return "3 x 4";
						if (SizeWidth == 8.25 && SizeHeght == 10.75)
							return "4 x 5";
						if (SizeWidth == 7.32 && SizeHeght == 13)
							return "9 x 16";
						return "4 x 3";
					default:
						return "4 x 3";
				}
			}
		}

		public string SlideSize
		{
			get
			{
				switch (Orientation)
				{
					case "Landscape":
						if (SizeWidth == 10 && SizeHeght == 7.5)
							return "Landscape 4 x 3";
						if (SizeWidth == 10.75 && SizeHeght == 8.25)
							return "Landscape 5 x 4";
						if (SizeWidth == 13 && SizeHeght == 7.32)
							return "Landscape 16 x 9";
						return "Landscape 4 x 3";
					case "Portrait":
						if (SizeWidth == 7.5 && SizeHeght == 10)
							return "Portrait 3 x 4";
						if (SizeWidth == 8.25 && SizeHeght == 10.75)
							return "Portrait 4 x 5";
						if (SizeWidth == 7.32 && SizeHeght == 13)
							return "Portrait 9 x 16";
						return "Landscape 4 x 3";
					default:
						return "Landscape 4 x 3";
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
						if (SizeWidth == 10.75 && SizeHeght == 8.25)
							return "Slides54";
						if (SizeWidth == 13 && SizeHeght == 7.32)
							return "Slides169";
						return "Slides43";
					case "Portrait":
						if (SizeWidth == 7.5 && SizeHeght == 10)
							return "Slides34";
						if (SizeWidth == 8.25 && SizeHeght == 10.75)
							return "Slides45";
						if (SizeWidth == 7.32 && SizeHeght == 13)
							return "Slides916";
						return "Slides43";
					default:
						return "Slides43";
				}
			}
		}

		public string SlideMasterFolder
		{
			get { return Size.Replace(" ", ""); }
		}

		private void LoadSharedSettings()
		{
			Orientation = "Landscape";
			SizeWidth = 10;
			SizeHeght = 7.5;
			SelectedWizard = String.Empty;

			if (File.Exists(_sharedSettingsFile))
			{
				var document = new XmlDocument();
				document.Load(_sharedSettingsFile);

				var node = document.SelectSingleNode(@"/SharedSettings/SelectedWizard");
				if (node != null)
					SelectedWizard = node.InnerText;
				node = document.SelectSingleNode(@"/SharedSettings/SizeWidth");
				double tempDouble;
				if (node != null)
				{
					if (double.TryParse(node.InnerText, out tempDouble))
						SizeWidth = tempDouble;
				}
				node = document.SelectSingleNode(@"/SharedSettings/SizeHeght");
				if (node != null)
				{
					if (double.TryParse(node.InnerText, out tempDouble))
						SizeHeght = tempDouble;
				}
				node = document.SelectSingleNode(@"/SharedSettings/Orientation");
				if (node != null)
					Orientation = node.InnerText;
			}
			LoadAppID();
			LoadDashdoardCode();
			ActivityDataPath = Path.Combine(OutgoingFolderPath, "user_data");
		}

		public void SaveSharedSettings()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<SharedSettings>");
			xml.AppendLine(@"<SelectedWizard>" + SelectedWizard.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedWizard>");
			xml.AppendLine(@"<SizeHeght>" + SizeHeght + @"</SizeHeght>");
			xml.AppendLine(@"<SizeWidth>" + SizeWidth + @"</SizeWidth>");
			xml.AppendLine(@"<Orientation>" + Orientation.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Orientation>");
			xml.AppendLine(@"</SharedSettings>");

			using (var sw = new StreamWriter(_sharedSettingsFile, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}

		private void LoadAppID()
		{
			AppID = Guid.Empty;
			if (File.Exists(_appIDFile))
			{
				var document = new XmlDocument();
				document.Load(_appIDFile);

				XmlNode node = document.SelectSingleNode(@"/AppID");
				if (node != null)
					if (!string.IsNullOrEmpty(node.InnerText))
						AppID = new Guid(node.InnerText);
			}
			if (AppID.Equals(Guid.Empty))
			{
				AppID = Guid.NewGuid();
				SaveAppID();
			}
			CheckAppIdFolders();
		}

		private void SaveAppID()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<AppID>" + AppID + @"</AppID>");
			if (!Directory.Exists(Path.GetDirectoryName(_appIDFile)))
				Directory.CreateDirectory(Path.GetDirectoryName(_appIDFile));
			using (var sw = new StreamWriter(_appIDFile, false))
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
				string localSettingsFolder = string.Format(@"{0}\newlocaldirect.com\xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
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

				string syncFolder = string.Format(@"{0}\newlocaldirect.com\sync", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
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

				var slideFolder = new DirectoryInfo(Path.Combine(syncFolder, "Incoming", "slides"));
				if (slideFolder.Exists)
					if (slideFolder.GetDirectories().Length > 0)
						firstRun = false;
			}
			catch { }
		}

		private void CheckAppIdFolders()
		{
			try
			{
				OutgoingFolderPath = string.Format(@"{0}\newlocaldirect.com\sync\Outgoing\AppID-" + AppID, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
				if (!Directory.Exists(OutgoingFolderPath))
					Directory.CreateDirectory(OutgoingFolderPath);

				string appIDFile = string.Format(@"{0}\newlocaldirect.com\sync\Outgoing\AppID-" + AppID + ".xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
				if (!File.Exists(appIDFile))
					File.Create(appIDFile);

				if (!Directory.Exists(Path.Combine(OutgoingFolderPath, "power_points")))
					Directory.CreateDirectory(Path.Combine(OutgoingFolderPath, "power_points"));
				if (!Directory.Exists(Path.Combine(OutgoingFolderPath, "saved_schedules")))
					Directory.CreateDirectory(Path.Combine(OutgoingFolderPath, "saved_schedules"));
				if (!Directory.Exists(Path.Combine(OutgoingFolderPath, "user_data")))
					Directory.CreateDirectory(Path.Combine(OutgoingFolderPath, "user_data"));
			}
			catch { }
		}

		private void LoadDashdoardCode()
		{
			if (!File.Exists(_dashboardCodeFilePath)) return;
			var document = new XmlDocument();
			document.Load(_dashboardCodeFilePath);

			var node = document.SelectSingleNode(@"/Settings/dashboard/DashboardCode");
			if (node != null)
			{
				DashboardCode = node.InnerText.Trim().ToLower();
			}
		}
	}
}