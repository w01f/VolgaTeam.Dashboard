﻿using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace NewBizWiz.Core.Common
{
	public class SettingsManager
	{
		private static SettingsManager _instance;
		private readonly string _appIDFile = string.Empty;
		private readonly string _dashboardNamePath = string.Empty;
		private readonly string _sharedSettingsFile = string.Empty;

		private SettingsManager()
		{
			_sharedSettingsFile = string.Format(@"{0}\newlocaldirect.com\xml\app\SharedSettings.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			_appIDFile = string.Format(@"{0}\newlocaldirect.com\xml\app\AppID.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			_dashboardNamePath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\Tab2Name.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			TempPath = string.Format(@"{0}\newlocaldirect.com\Sync\Temp", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			if (!Directory.Exists(TempPath))
				Directory.CreateDirectory(TempPath);
			SharedListFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Data", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			MinibarApplicationPath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\MiniBar.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			OneDomainApplicationPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\applications\APP_One_Domain\OneDomain.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			SalesDepotApplicationPath = string.Format(@"{0}\newlocaldirect.com\Sales Depot\SalesDepot.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			SelectedWizard = string.Empty;
			DashboardName = "Schedule APP";
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

		public string TempPath { get; set; }
		public string MinibarApplicationPath { get; set; }
		public string OneDomainApplicationPath { get; set; }
		public string SalesDepotApplicationPath { get; set; }
		public string SharedListFolder { get; set; }

		public Guid AppID { get; set; }

		public string SelectedWizard { get; set; }
		public double SizeHeght { get; set; }
		public double SizeWidth { get; set; }
		public string Orientation { get; set; }
		public bool SlideTemplateEnabled { get; set; }
		public string DashboardName { get; set; }

		public string Size
		{
			get
			{
				switch (Orientation)
				{
					case "Landscape":
						if (SizeWidth == 10 && SizeHeght == 7.5)
							return "4 x 3";
						else if (SizeWidth == 10.75 && SizeHeght == 8.25)
							return "5 x 4";
						if (SizeWidth == 10 && SizeHeght == 5.63)
							return "16 x 9";
						else
							return "4 x 3";
					case "Portrait":
						if (SizeWidth == 10 && SizeHeght == 7.5)
							return "3 x 4";
						else if (SizeWidth == 10.75 && SizeHeght == 8.25)
							return "4 x 5";
						if (SizeWidth == 10 && SizeHeght == 5.63)
							return "9 x 16";
						else
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

		private void LoadSharedSettings()
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
			LoadAppID();
			LoadDashboardName();
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

			xml.AppendLine(@"<AppID>" + AppID.ToString() + @"</AppID>");

			string appIDPath = Path.Combine(Application.StartupPath, _appIDFile);
			using (var sw = new StreamWriter(appIDPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}

		private void LoadDashboardName()
		{
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
			catch {}
		}

		private void CheckAppIdFolders()
		{
			try
			{
				string appIDFolder = string.Format(@"{0}\newlocaldirect.com\sync\Outgoing\AppID-" + AppID.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
				if (!Directory.Exists(appIDFolder))
					Directory.CreateDirectory(appIDFolder);

				string appIDFile = string.Format(@"{0}\newlocaldirect.com\sync\Outgoing\AppID-" + AppID.ToString() + ".xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
				if (!File.Exists(appIDFile))
					File.Create(appIDFile);

				if (!Directory.Exists(Path.Combine(appIDFolder, "power_points")))
					Directory.CreateDirectory(Path.Combine(appIDFolder, "power_points"));
				if (!Directory.Exists(Path.Combine(appIDFolder, "saved_schedules")))
					Directory.CreateDirectory(Path.Combine(appIDFolder, "saved_schedules"));
				if (!Directory.Exists(Path.Combine(appIDFolder, "user_data")))
					Directory.CreateDirectory(Path.Combine(appIDFolder, "user_data"));
			}
			catch {}
		}
	}
}