using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Core.Dashboard
{
	public class SettingsManager
	{
		private static readonly SettingsManager _instance = new SettingsManager();

		private readonly string _dashboardSettingsFile = string.Empty;
		private readonly ThemeSaveHelper _themeSaveHelper;

		private SettingsManager()
		{
			_dashboardSettingsFile = Path.Combine(Common.SettingsManager.Instance.SettingsPath, "DashboardSettings.xml");
			HelpLinksPath = String.Format(@"{0}\newlocaldirect.com\app\HelpUrls\DashboardHelp.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			DashboardSaveFolder = String.Empty;
			ThemeManager = new ThemeManager(Path.Combine(Common.SettingsManager.Instance.ThemeCollectionPath, Common.SettingsManager.Instance.SlideMasterFolder));
			SlideManager = new SlideManager(Common.SettingsManager.Instance.SlideMastersPath);
			_themeSaveHelper = new ThemeSaveHelper(ThemeManager);

			AdSchedulePath = String.Format(@"{0}\newlocaldirect.com\app\spadloader\SPADLOAD.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			DigitalSchedulePath = String.Format(@"{0}\newlocaldirect.com\app\spdgloader\SPDGLOAD.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			TVSchedulePath = String.Format(@"{0}\newlocaldirect.com\app\sptvloader\SPTVLOAD.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			RadioSchedulePath = String.Format(@"{0}\newlocaldirect.com\app\sprdloader\SPRDLOAD.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
		}

		public string HelpLinksPath { get; set; }
		public string DashboardSaveFolder { get; set; }
		public ThemeManager ThemeManager { get; private set; }
		public SlideManager SlideManager { get; private set; }

		public string AdSchedulePath { get; private set; }
		public string DigitalSchedulePath { get; private set; }
		public string TVSchedulePath { get; private set; }
		public string RadioSchedulePath { get; private set; }

		public static SettingsManager Instance
		{
			get { return _instance; }
		}

		public Theme GetSelectedTheme(SlideType slideType)
		{
			return _themeSaveHelper.GetSelectedTheme(slideType);
		}

		public void SetSelectedTheme(SlideType slideType, string themeName)
		{
			_themeSaveHelper.SetSelectedTheme(slideType, themeName);
		}

		public void LoadSettings()
		{
			InitDashboardSaveFolder();
			LoadDashboardSettings();
			MasterWizardManager.Instance.SetMasterWizard();
		}

		public void LoadDashboardSettings()
		{
			if (!File.Exists(_dashboardSettingsFile)) return;
			var document = new XmlDocument();
			document.Load(_dashboardSettingsFile);
			var node = document.SelectSingleNode(@"/DashboardSettings/SalesRepState");
			if (node != null)
			{
				ViewSettingsManager.Instance.CoverState.DeserializeSalesRep(node);
			}
			_themeSaveHelper.Deserialize(document.SelectNodes(@"//DashboardSettings/SelectedTheme").OfType<XmlNode>());
		}

		public void SaveDashboardSettings()
		{
			var xml = new StringBuilder();
			xml.AppendLine(@"<DashboardSettings>");
			xml.AppendLine(@"<SalesRepState>" + ViewSettingsManager.Instance.CoverState.SerializeSalesRep() + @"</SalesRepState>");
			xml.AppendLine(_themeSaveHelper.Serialize());
			xml.AppendLine(@"</DashboardSettings>");
			string userConfigurationPath = Path.Combine(_dashboardSettingsFile);
			using (var sw = new StreamWriter(userConfigurationPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}

		private void InitDashboardSaveFolder()
		{
			DashboardSaveFolder = Path.Combine(Common.SettingsManager.Instance.OutgoingFolderPath, "saved_dashboard");
			if (!Directory.Exists(DashboardSaveFolder))
				Directory.CreateDirectory(DashboardSaveFolder);
		}
	}
}