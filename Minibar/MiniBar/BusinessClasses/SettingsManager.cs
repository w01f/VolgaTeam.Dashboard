using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using NewBizWiz.Core.Common;

namespace NewBizWiz.MiniBar.BusinessClasses
{
	public class SettingsManager
	{
		#region Constant Names
		public const string RegularSyncName = @"adSync4.exe";
		public const string SilentSyncName = @"adSync5.exe";
		public const string SyncSettingsFileName = @"syncfile.xml";
		#endregion

		private static readonly SettingsManager _instance = new SettingsManager();

		#region Path Variables
		private readonly string _minibarSettingsFile = string.Empty;
		private readonly string _slidesFolderPath = string.Empty;
		private readonly string _webcastSettingsFile = string.Empty;
		private string _minibarAppSettingsFile = string.Empty;
		public TabPageSettings TabPageSettings { get; private set; }
		public string SyncFilesSourcePath { get; set; }
		public string NBWApplicationsRootPath { get; set; }
		public string DashboardPath { get; set; }
		public string DashboardIconPath { get; set; }
		public SalesDepotSettings SalesDepotSettings { get; private set; }
		public string SalesGalleryPath { get; set; }
		public ClipartSettings ClipartSettings { get; private set; }
		public string LibraryPath { get; set; }
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
			_minibarSettingsFile = string.Format(@"{0}\newlocaldirect.com\xml\app\MinibarSettings.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			_webcastSettingsFile = string.Format(@"{0}\newlocaldirect.com\app\Minibar\webcast.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			_slidesFolderPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\slides", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			SyncFilesSourcePath = string.Format(@"{0}\newlocaldirect.com\app\adsync_patch", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			NBWApplicationsRootPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\applications", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			DashboardPath = string.Format(@"{0}\newlocaldirect.com\app\adSALESapp.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			DashboardIconPath = string.Format(@"{0}\newlocaldirect.com\app\tab2icon.ico", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			SalesGalleryPath = string.Format(@"{0}\newlocaldirect.com\app\Sales Gallery\SalesGallery.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			LibraryPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\libraries", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			ResetPath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\Reset.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			SyncSettingsFolderPath = string.Format(@"{0}\newlocaldirect.com\!Update_Settings", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			HelpLinksPath = string.Format(@"{0}\newlocaldirect.com\app\HelpUrls\MinibarHelp.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			MinibarLoaderPath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\MiniBarLoader.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			PowerPointLoaderPath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\PowerPointLoader.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			TeamViewerQSPath = string.Format(@"{0}\newlocaldirect.com\app\TeamViewerQS_en.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			TeamViewerQJPath = string.Format(@"{0}\newlocaldirect.com\app\TeamViewerQJ_en.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

			TabPageSettings = new TabPageSettings();
			SalesDepotSettings = new SalesDepotSettings();
			ClipartSettings = new ClipartSettings(string.Format(@"{0}\newlocaldirect.com\app\Sales Gallery\minibar.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)));
		}


		public static SettingsManager Instance
		{
			get { return _instance; }
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
		public BrowserType SalesDepotBrowser { get; set; }
		public string SelectedSlideGroup { get; set; }
		public string SelectedSlideMaster { get; set; }
		#endregion

		#region WebcastSettings
		public string Location { get; set; }
		public List<string> MeetingIDs { get; set; }
		#endregion

		public void LoadSettings()
		{
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
			LoadWebcastSettings();
			ServiceDataFilePath = string.Format(@"{0}\newlocaldirect.com\sync\Outgoing\AppID-" + Core.Common.SettingsManager.Instance.AppID.ToString() + ".xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			ServiceDataManager.Instance.LoadData();
		}

		public void CreateStaticFolders()
		{
			try
			{
				var localSettingsFolder = string.Format(@"{0}\newlocaldirect.com\xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
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
				if (!Directory.Exists(Path.Combine(localSettingsFolder, "sales depot", "Settings")))
					Directory.CreateDirectory(Path.Combine(localSettingsFolder, "sales depot", "Settings"));

				var syncFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
				if (!Directory.Exists(Path.Combine(syncFolder, "applications")))
					Directory.CreateDirectory(Path.Combine(syncFolder, "applications"));
				if (!Directory.Exists(Path.Combine(syncFolder, "gallery")))
					Directory.CreateDirectory(Path.Combine(syncFolder, "gallery"));
				if (!Directory.Exists(Path.Combine(syncFolder, "libraries")))
					Directory.CreateDirectory(Path.Combine(syncFolder, "libraries"));
				if (!Directory.Exists(Path.Combine(syncFolder, "slides")))
					Directory.CreateDirectory(Path.Combine(syncFolder, "slides"));
				if (!Directory.Exists(Path.Combine(syncFolder, "update")))
					Directory.CreateDirectory(Path.Combine(syncFolder, "update"));
			}
			catch { }
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

				if (Utilities.Instance.ChromeInstalled)
					SalesDepotBrowser = BrowserType.Chrome;
				else if (Utilities.Instance.FirefoxInstalled)
					SalesDepotBrowser = BrowserType.Firefox;
				if (Utilities.Instance.OperaInstalled)
					SalesDepotBrowser = BrowserType.Opera;
				else
					SalesDepotBrowser = BrowserType.IE;

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
					node = document.SelectSingleNode(@"/MinibarSettings/SalesDepotBrowser");
					if (node != null)
						if (Int32.TryParse(node.InnerText, out tempInt))
							SalesDepotBrowser = (BrowserType)tempInt;
					node = document.SelectSingleNode(@"/MinibarSettings/SelectedSlideGroup");
					if (node != null)
						SelectedSlideGroup = node.InnerText;
					node = document.SelectSingleNode(@"/MinibarSettings/SelectedSlideMaster");
					if (node != null)
						SelectedSlideMaster = node.InnerText;
				}

				if (SalesDepotBrowser != BrowserType.IE && SalesDepotBrowser != BrowserType.Chrome && SalesDepotBrowser != BrowserType.Firefox && SalesDepotBrowser != BrowserType.Opera)
					SalesDepotBrowser = BrowserType.IE;

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
			xml.AppendLine(@"<LastSync>" + LastSync + @"</LastSync>");
			xml.AppendLine(@"<SyncHourly>" + SyncHourly + @"</SyncHourly>");
			xml.AppendLine(@"<OwnControl>" + OwnControl + @"</OwnControl>");
			xml.AppendLine(@"<OnPrimaryScreen>" + OnPrimaryScreen + @"</OnPrimaryScreen>");
			xml.AppendLine(@"<FloaterTop>" + FloaterTop + @"</FloaterTop>");
			xml.AppendLine(@"<FloaterLeft>" + FloaterLeft + @"</FloaterLeft>");
			xml.AppendLine(@"<QuickRetraction>" + QuickRetraction + @"</QuickRetraction>");
			xml.AppendLine(@"<SalesDepotBrowser>" + (int)SalesDepotBrowser + @"</SalesDepotBrowser>");
			if (!String.IsNullOrEmpty(SelectedSlideGroup))
				xml.AppendLine(@"<SelectedSlideGroup>" + SelectedSlideGroup.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedSlideGroup>");
			if (!String.IsNullOrEmpty(SelectedSlideMaster))
				xml.AppendLine(@"<SelectedSlideMaster>" + SelectedSlideMaster.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedSlideMaster>");
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
			if (!File.Exists(_webcastSettingsFile)) return;
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