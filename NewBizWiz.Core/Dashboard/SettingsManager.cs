using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Core.Dashboard
{
	public class SettingsManager
	{
		private const string DashboardCodeFile = @"dashboard.xml";
		private static readonly SettingsManager _instance = new SettingsManager();

		private readonly string _dashboardSettingsFile = string.Empty;
		private readonly ThemeSaveHelper _themeSaveHelper;

		private SettingsManager()
		{
			_dashboardSettingsFile = string.Format(@"{0}\newlocaldirect.com\xml\app\DashboardSettings.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			IconPath = string.Format(@"{0}\newlocaldirect.com\app\tab2icon.ico", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			HelpLinksPath = string.Format(@"{0}\newlocaldirect.com\app\HelpUrls\DashboardHelp.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			DashboardCode = String.Empty;
			DashboardSaveFolder = string.Empty;
			ThemeManager = new ThemeManager(Path.Combine(Common.SettingsManager.Instance.ThemeCollectionPath, Common.SettingsManager.Instance.SlideMasterFolder));
			SlideManager = new SlideManager(Common.SettingsManager.Instance.SlideMastersPath);
			_themeSaveHelper = new ThemeSaveHelper(ThemeManager);
		}

		public string HelpLinksPath { get; set; }
		public string DashboardSaveFolder { get; set; }
		public string DashboardCode { get; set; }
		public string IconPath { get; set; }
		public ThemeManager ThemeManager { get; private set; }
		public SlideManager SlideManager { get; private set; }

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
			LoadDashdoardCode();
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
			DashboardSaveFolder = Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Outgoing\AppID-" + Common.SettingsManager.Instance.AppID.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)), "saved_dashboard");
			if (!Directory.Exists(DashboardSaveFolder))
				Directory.CreateDirectory(DashboardSaveFolder);
		}

		private void LoadDashdoardCode()
		{
			string dashboardCodePath = Path.Combine(Application.StartupPath, DashboardCodeFile);
			if (File.Exists(dashboardCodePath))
			{
				var document = new XmlDocument();
				document.Load(dashboardCodePath);

				XmlNode node = document.SelectSingleNode(@"/Settings/dashboard/DashboardCode");
				if (node != null)
				{
					DashboardCode = node.InnerText.Trim().ToLower();
				}
			}
		}
	}
}