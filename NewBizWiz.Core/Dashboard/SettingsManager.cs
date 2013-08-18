using System;
using System.IO;
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

		private SettingsManager()
		{
			_dashboardSettingsFile = string.Format(@"{0}\newlocaldirect.com\xml\app\DashboardSettings.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			IconPath = string.Format(@"{0}\newlocaldirect.com\app\tab2icon.ico", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			HelpLinksPath = string.Format(@"{0}\newlocaldirect.com\app\HelpUrls\DashboardHelp.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			LastUsedLogoIndex = 0;
			DashboardCode = String.Empty;
			DashboardSaveFolder = string.Empty;
		}

		public string HelpLinksPath { get; set; }
		public string DashboardSaveFolder { get; set; }
		public int LastUsedLogoIndex { get; set; }

		public string DashboardCode { get; set; }
		public string IconPath { get; set; }

		public static SettingsManager Instance
		{
			get { return _instance; }
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
			XmlNode node;
			if (File.Exists(_dashboardSettingsFile))
			{
				var document = new XmlDocument();
				document.Load(_dashboardSettingsFile);

				node = document.SelectSingleNode(@"/DashboardSettings/LastUsedLogoIndex");
				if (node != null)
				{
					int temp = 0;
					int.TryParse(node.InnerText, out temp);
					LastUsedLogoIndex = temp;
				}
				node = document.SelectSingleNode(@"/DashboardSettings/SalesRepState");
				if (node != null)
				{
					ViewSettingsManager.Instance.CoverState.DeserializeSalesRep(node);
				}
			}
		}

		public void SaveDashboardSettings()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<DashboardSettings>");
			xml.AppendLine(@"<LastUsedLogoIndex>" + LastUsedLogoIndex.ToString() + @"</LastUsedLogoIndex>");
			xml.AppendLine(@"<SalesRepState>" + ViewSettingsManager.Instance.CoverState.SerializeSalesRep() + @"</SalesRepState>");
			xml.AppendLine(@"</DashboardSettings>");

			string userConfigurationPath = Path.Combine(Application.StartupPath, _dashboardSettingsFile);
			using (var sw = new StreamWriter(userConfigurationPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}

		private void InitDashboardSaveFolder()
		{
			DashboardSaveFolder = Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Outgoing\AppID-" + Core.Common.SettingsManager.Instance.AppID.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)), "saved_dashboard");
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