using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using MiniBar.BusinessClasses;

namespace MiniBar.ConfigurationClasses
{
	internal class SettingsManager
	{
		#region Constant Names
		public const string RegularSyncName = @"adSync4.exe";
		public const string SilentSyncName = @"adSync5.exe";
		public const string SyncSettingsFileName = @"syncfile.xml";
		public const string NBWApplicationManifestFileName = "Manifest.xml";
		#endregion

		private static readonly SettingsManager _instance = new SettingsManager();

		#region Path Variables
		private readonly string _appIDFile = string.Empty;
		private readonly string _dashboardNamePath = string.Empty;
		private readonly string _minibarSettingsFile = string.Empty;
		private readonly string _sharedSettingsFile = string.Empty;
		private readonly string _slidesFolderPath = string.Empty;
		private readonly string _webcastSettingsFile = string.Empty;
		private string _minibarAppSettingsFile = string.Empty;
		public TabPageSettings TabPageSettings { get; private set; }
		public string SyncFilesSourcePath { get; set; }
		public string NBWApplicationsRootPath { get; set; }
		public string DashboardPath { get; set; }
		public string DashboardLogoPath { get; set; }
		public string DashboardIconPath { get; set; }
		public SalesDepotSettings SalesDepotSettings { get; private set; }
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
		public string TeamViewerQSPath { get; set; }
		public string TeamViewerQJPath { get; set; }
		#endregion

		private SettingsManager()
		{
			_sharedSettingsFile = string.Format(@"{0}\newlocaldirect.com\xml\app\SharedSettings.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			_minibarSettingsFile = string.Format(@"{0}\newlocaldirect.com\xml\app\MinibarSettings.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			_webcastSettingsFile = string.Format(@"{0}\newlocaldirect.com\app\Minibar\webcast.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			_appIDFile = string.Format(@"{0}\newlocaldirect.com\xml\app\AppID.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			_dashboardNamePath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\Tab2Name.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			_slidesFolderPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\slides", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			SyncFilesSourcePath = string.Format(@"{0}\newlocaldirect.com\app\adsync_patch", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			NBWApplicationsRootPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\applications", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			DashboardPath = string.Format(@"{0}\newlocaldirect.com\app\adSALESapp.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			DashboardLogoPath = string.Format(@"{0}\newlocaldirect.com\app\tab2btn.png", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			DashboardIconPath = string.Format(@"{0}\newlocaldirect.com\app\tab2icon.ico", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			ClientLogosPath = string.Format(@"{0}\newlocaldirect.com\app\Client Logos\ClientLogos.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			SalesGalleryPath = string.Format(@"{0}\newlocaldirect.com\app\Sales Gallery\SalesGallery.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			WebArtPath = string.Format(@"{0}\newlocaldirect.com\app\Web Art\WebArt.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			LibraryPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\libraries", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			ClipartPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\gallery", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			ResetPath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\Reset.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			SyncSettingsFolderPath = string.Format(@"{0}\newlocaldirect.com\!Update_Settings", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			HelpLinksPath = string.Format(@"{0}\newlocaldirect.com\app\HelpUrls\MinibarHelp.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			MinibarLoaderPath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\MiniBarLoader.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			PowerPointLoaderPath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\PowerPointLoader.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			TeamViewerQSPath = string.Format(@"{0}\newlocaldirect.com\app\TeamViewerQS_en.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			TeamViewerQJPath = string.Format(@"{0}\newlocaldirect.com\app\TeamViewerQJ_en.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

			TabPageSettings = new TabPageSettings();
			SalesDepotSettings = new SalesDepotSettings();
		}

		public Guid AppID { get; set; }


		public static SettingsManager Instance
		{
			get { return _instance; }
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

		public string SlideSize
		{
			get
			{
				switch (Orientation)
				{
					case "Landscape":
						if (SizeWidth == 10 && SizeHeght == 7.5)
							return "Landscape 4 x 3";
						else if (SizeWidth == 10.75 && SizeHeght == 8.25)
							return "Landscape 5 x 4";
						if (SizeWidth == 10 && SizeHeght == 5.63)
							return "Landscape 16 x 9";
						else
							return "Landscape 4 x 3";
					case "Portrait":
						if (SizeWidth == 10 && SizeHeght == 7.5)
							return "Portrait 3 x 4";
						else if (SizeWidth == 10.75 && SizeHeght == 8.25)
							return "Portrait 4 x 5";
						if (SizeWidth == 10 && SizeHeght == 5.63)
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

		#region Shared Settings
		public string SelectedWizard { get; set; }
		public double SizeHeght { get; set; }
		public double SizeWidth { get; set; }
		public string Orientation { get; set; }
		public bool SlideTemplateEnabled { get; set; }
		#endregion

		#region Application Names
		public string DashboardName { get; set; }
		#endregion

		#region Minibar Settings
		private DateTime _nextSync = DateTime.Now;
		public DateTime LastSync { get; set; }
		public DateTime NextSync
		{
			get
			{
				DateTime now = DateTime.Now;
				var next = new DateTime(now.Year, now.Month, now.Day, SyncHourly ? now.Hour : _nextSync.Hour, _nextSync.Minute, _nextSync.Second);
				if (next < now)
					return SyncHourly ? next.AddHours(1) : next.AddDays(1);
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

		public void LoadSharedSettings()
		{
			SelectedWizard = string.Empty;
			SizeWidth = 10;
			SizeHeght = 7.5;
			Orientation = "Landscape";
			SlideTemplateEnabled = false;

			PrimaryApplications = new List<string>();
			PrimaryApplications.AddRange(new[]
			                             {
				                             "excel",
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
				                             "MediaOffice"
			                             });
			XmlNode node;
			double tempDouble;
			bool tempBool;
			if (File.Exists(_sharedSettingsFile))
			{
				var document = new XmlDocument();
				try
				{
					document.Load(_sharedSettingsFile);
				}
				catch { }

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
				node = document.SelectSingleNode(@"/SharedSettings/HideAdSchedule");
			}

			LoadAppID();
			LoadDashboardName();
			LoadWebcastSettings();
		}

		public void SaveSharedSettings()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<SharedSettings>");
			xml.AppendLine(@"<SelectedWizard>" + SelectedWizard.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedWizard>");
			xml.AppendLine(@"<SizeHeght>" + SizeHeght.ToString() + @"</SizeHeght>");
			xml.AppendLine(@"<SizeWidth>" + SizeWidth.ToString() + @"</SizeWidth>");
			xml.AppendLine(@"<Orientation>" + Orientation.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Orientation>");
			xml.AppendLine(@"<SlideTemplateEnabled>" + SlideTemplateEnabled.ToString() + @"</SlideTemplateEnabled>");
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
			string appIDPath = Path.Combine(Application.StartupPath.ToLower().Replace(@"\minibar", ""), _appIDFile);
			if (File.Exists(appIDPath))
			{
				var document = new XmlDocument();
				try
				{
					document.Load(appIDPath);
				}
				catch { }

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
			ServiceDataFilePath = string.Format(@"{0}\newlocaldirect.com\sync\Outgoing\AppID-" + AppID.ToString() + ".xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			ServiceDataManager.Instance.LoadData();
			CheckAppIDFolders();
		}

		private void SaveAppID()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<AppID>" + AppID.ToString() + @"</AppID>");

			string appIDPath = Path.Combine(Application.StartupPath.ToLower().Replace(@"\minibar", ""), _appIDFile);
			using (var sw = new StreamWriter(appIDPath, false))
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
			}
			catch { }
		}

		public void DeleteStaticFolders()
		{
			string localSettingsFolder = string.Format(@"{0}\newlocaldirect.com\xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			foreach (DirectoryInfo xmlFolder in (new DirectoryInfo(localSettingsFolder)).GetDirectories())
				xmlFolder.Delete(true);

			string incomingFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			foreach (DirectoryInfo incomingSubFolder in (new DirectoryInfo(incomingFolder)).GetDirectories())
				incomingSubFolder.Delete(true);
		}

		private void CheckAppIDFolders()
		{
			try
			{
				string appIDFolder = string.Format(@"{0}\newlocaldirect.com\sync\Outgoing\AppID-" + AppID.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
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
			catch { }
		}

		private void LoadDashboardName()
		{
			DashboardName = "Dashboard";

			XmlNode node;
			if (File.Exists(_dashboardNamePath))
			{
				var document = new XmlDocument();
				document.Load(_dashboardNamePath);

				node = document.SelectSingleNode(@"/Tab2Name");
				if (node != null)
					DashboardName = node.InnerText;
			}
		}

		public void LoadMinibarSettings()
		{
			lock (AppManager.Locker)
			{
				LastSync = DateTime.Now;
				SyncHourly = false;
				OwnControl = false;
				OnPrimaryScreen = true;
				QuickRetraction = true;
				FloaterLeft = 0;
				FloaterTop = 0;

				XmlNode node;
				DateTime tempDateTime;
				bool tempBool;
				int tempInt;
				if (File.Exists(_minibarSettingsFile))
				{
					var document = new XmlDocument();
					try
					{
						document.Load(_minibarSettingsFile);
					}
					catch { }

					node = document.SelectSingleNode(@"/MinibarSettings/LastSync");
					if (node != null)
						if (DateTime.TryParse(node.InnerText, out tempDateTime))
							LastSync = tempDateTime;
					node = document.SelectSingleNode(@"/MinibarSettings/SyncHourly");
					if (node != null)
						if (bool.TryParse(node.InnerText, out tempBool))
							SyncHourly = tempBool;
					node = document.SelectSingleNode(@"/MinibarSettings/OwnControl");
					if (node != null)
						if (bool.TryParse(node.InnerText, out tempBool))
							OwnControl = tempBool;
					node = document.SelectSingleNode(@"/MinibarSettings/OnPrimaryScreen");
					if (node != null)
						if (bool.TryParse(node.InnerText, out tempBool))
							OnPrimaryScreen = tempBool;
					node = document.SelectSingleNode(@"/MinibarSettings/FloaterLeft");
					if (node != null)
						if (int.TryParse(node.InnerText, out tempInt))
							FloaterLeft = tempInt;
					node = document.SelectSingleNode(@"/MinibarSettings/FloaterTop");
					if (node != null)
						if (int.TryParse(node.InnerText, out tempInt))
							FloaterTop = tempInt;
					//node = document.SelectSingleNode(@"/MinibarSettings/QuickRetraction");
					//if (node != null)
					//    if (bool.TryParse(node.InnerText, out tempBool))
					//        this.QuickRetraction = tempBool;
				}

				if (File.Exists(Path.Combine(SyncSettingsFolderPath, SyncSettingsFileName)))
				{
					var document = new XmlDocument();
					document.Load(Path.Combine(SyncSettingsFolderPath, SyncSettingsFileName));

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
				AutoRunNormal = true;
				AutoRunHidden = false;
				AutoRunFloat = false;
				HideAll = true;
				HideSpecificProgram = false;
				HideSpecificProgramMaximized = false;
				VisiblePowerPoint = false;
				VisiblePowerPointMaximaized = false;
				CloseShutdown = false;
				CloseHide = true;
				CloseFloat = false;

				XmlNode node;
				bool tempBool;
				if (OwnControl)
					_minibarAppSettingsFile = string.Format(@"{0}\newlocaldirect.com\xml\app\MinibarAppSettings.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
				else
					_minibarAppSettingsFile = string.Format(@"{0}\newlocaldirect.com\app\Minibar\MinibarAppSettings.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

				if (File.Exists(_minibarAppSettingsFile))
				{
					var document = new XmlDocument();
					try
					{
						document.Load(_minibarAppSettingsFile);
					}
					catch { }

					node = document.SelectSingleNode(@"/MinibarSettings/AutoRunNormal");
					if (node != null)
						if (bool.TryParse(node.InnerText, out tempBool))
							AutoRunNormal = tempBool;
					node = document.SelectSingleNode(@"/MinibarSettings/AutoRunHidden");
					if (node != null)
						if (bool.TryParse(node.InnerText, out tempBool))
							AutoRunHidden = tempBool;
					node = document.SelectSingleNode(@"/MinibarSettings/AutoRunFloat");
					if (node != null)
						if (bool.TryParse(node.InnerText, out tempBool))
							AutoRunFloat = tempBool;

					node = document.SelectSingleNode(@"/MinibarSettings/HideAll");
					if (node != null)
						if (bool.TryParse(node.InnerText, out tempBool))
							HideAll = tempBool;
					node = document.SelectSingleNode(@"/MinibarSettings/HideSpecificProgram");
					if (node != null)
						if (bool.TryParse(node.InnerText, out tempBool))
							HideSpecificProgram = tempBool;
					node = document.SelectSingleNode(@"/MinibarSettings/HideSpecificProgramMaximized");
					if (node != null)
						if (bool.TryParse(node.InnerText, out tempBool))
							HideSpecificProgramMaximized = tempBool;
					node = document.SelectSingleNode(@"/MinibarSettings/VisiblePowerPoint");
					if (node != null)
						if (bool.TryParse(node.InnerText, out tempBool))
							VisiblePowerPoint = tempBool;
					node = document.SelectSingleNode(@"/MinibarSettings/VisiblePowerPointMaximaized");
					if (node != null)
						if (bool.TryParse(node.InnerText, out tempBool))
							VisiblePowerPointMaximaized = tempBool;

					node = document.SelectSingleNode(@"/MinibarSettings/CloseShutdown");
					if (node != null)
						if (bool.TryParse(node.InnerText, out tempBool))
							CloseShutdown = tempBool;
					node = document.SelectSingleNode(@"/MinibarSettings/CloseHide");
					if (node != null)
						if (bool.TryParse(node.InnerText, out tempBool))
							CloseHide = tempBool;
					node = document.SelectSingleNode(@"/MinibarSettings/CloseFloat");
					if (node != null)
						if (bool.TryParse(node.InnerText, out tempBool))
							CloseFloat = tempBool;
					node = document.SelectSingleNode(@"/MinibarSettings/Applications");
					if (node != null)
					{
						if (node.ChildNodes.Count > 0)
							PrimaryApplications.Clear();
						foreach (XmlNode childNode in node.ChildNodes)
							switch (childNode.Name)
							{
								case "Application":
									PrimaryApplications.Add(childNode.InnerText);
									break;
							}
					}
				}
			}
		}

		public void SaveMinibarSettings()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<MinibarSettings>");
			xml.AppendLine(@"<LastSync>" + LastSync.ToString() + @"</LastSync>");
			xml.AppendLine(@"<SyncHourly>" + SyncHourly.ToString() + @"</SyncHourly>");
			xml.AppendLine(@"<OwnControl>" + OwnControl.ToString() + @"</OwnControl>");
			xml.AppendLine(@"<OnPrimaryScreen>" + OnPrimaryScreen.ToString() + @"</OnPrimaryScreen>");
			xml.AppendLine(@"<FloaterTop>" + FloaterTop.ToString() + @"</FloaterTop>");
			xml.AppendLine(@"<FloaterLeft>" + FloaterLeft.ToString() + @"</FloaterLeft>");
			xml.AppendLine(@"<QuickRetraction>" + QuickRetraction.ToString() + @"</QuickRetraction>");
			xml.AppendLine(@"</MinibarSettings>");

			using (var sw = new StreamWriter(_minibarSettingsFile, false))
			{
				sw.Write(xml);
				sw.Flush();
			}

			if (OwnControl)
			{
				xml.Clear();
				xml.AppendLine(@"<MinibarSettings>");
				xml.AppendLine(@"<AutoRunNormal>" + AutoRunNormal.ToString() + @"</AutoRunNormal>");
				xml.AppendLine(@"<AutoRunHidden>" + AutoRunHidden.ToString() + @"</AutoRunHidden>");
				xml.AppendLine(@"<AutoRunFloat>" + AutoRunFloat.ToString() + @"</AutoRunFloat>");

				xml.AppendLine(@"<HideAll>" + HideAll.ToString() + @"</HideAll>");
				xml.AppendLine(@"<HideSpecificProgram>" + HideSpecificProgram.ToString() + @"</HideSpecificProgram>");
				xml.AppendLine(@"<HideSpecificProgramMaximized>" + HideSpecificProgramMaximized.ToString() + @"</HideSpecificProgramMaximized>");
				xml.AppendLine(@"<VisiblePowerPoint>" + VisiblePowerPoint.ToString() + @"</VisiblePowerPoint>");
				xml.AppendLine(@"<VisiblePowerPointMaximaized>" + VisiblePowerPointMaximaized.ToString() + @"</VisiblePowerPointMaximaized>");

				xml.AppendLine(@"<CloseShutdown>" + CloseShutdown.ToString() + @"</CloseShutdown>");
				xml.AppendLine(@"<CloseHide>" + CloseHide.ToString() + @"</CloseHide>");
				xml.AppendLine(@"<CloseFloat>" + CloseFloat.ToString() + @"</CloseFloat>");

				xml.AppendLine(@"<Applications>");
				foreach (string application in PrimaryApplications)
					xml.AppendLine(@"<Application>" + application.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Application>");
				xml.AppendLine(@"</Applications>");
				xml.AppendLine(@"</MinibarSettings>");

				_minibarAppSettingsFile = string.Format(@"{0}\newlocaldirect.com\xml\app\MinibarAppSettings.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
				using (var sw = new StreamWriter(_minibarAppSettingsFile, false))
				{
					sw.Write(xml);
					sw.Flush();
				}
			}
		}

		public void LoadWebcastSettings()
		{
			Location = string.Empty;
			MeetingIDs = new List<string>();

			XmlNode node;
			if (File.Exists(_webcastSettingsFile))
			{
				var document = new XmlDocument();
				try
				{
					document.Load(_webcastSettingsFile);
				}
				catch { }
				node = document.SelectSingleNode(@"/Webcast");
				if (node != null)
				{
					foreach (XmlNode childNode in node.ChildNodes)
						switch (childNode.Name)
						{
							case "Location":
								Location = childNode.InnerText;
								break;
							case "MeetingID":
								MeetingIDs.Add(childNode.InnerText);
								break;
						}
				}
			}
		}
	}
}