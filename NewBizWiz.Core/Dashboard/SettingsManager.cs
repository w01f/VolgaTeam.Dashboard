using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Core.Dashboard
{
	public class SettingsManager
	{
		private static readonly SettingsManager _instance = new SettingsManager();

		private ThemeSaveHelper _themeSaveHelper;

		private SettingsManager()
		{
			AdSchedulePath = String.Format(@"{0}\newlocaldirect.com\app\spadloader\SPADLOAD.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			DigitalSchedulePath = String.Format(@"{0}\newlocaldirect.com\app\spdgloader\SPDGLOAD.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			TVSchedulePath = String.Format(@"{0}\newlocaldirect.com\app\sptvloader\SPTVLOAD.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			RadioSchedulePath = String.Format(@"{0}\newlocaldirect.com\app\sprdloader\SPRDLOAD.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
		}

		public ThemeManager ThemeManager { get; private set; }
		public SlideManager SlideManager { get; private set; }

		public string AdSchedulePath { get; private set; }
		public string DigitalSchedulePath { get; private set; }
		public string TVSchedulePath { get; private set; }
		public string RadioSchedulePath { get; private set; }

		public string SalesRep { get; set; }

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

		public async Task LoadSettings()
		{
			Common.SettingsManager.Instance.LoadSharedSettings();

			ThemeManager = new ThemeManager();
			await ThemeManager.Load();
			_themeSaveHelper = new ThemeSaveHelper(ThemeManager);


			SlideManager = new SlideManager();
			await SlideManager.Load();


			LoadDashboardSettings();

			MasterWizardManager.Instance.SetMasterWizard();
		}

		public void LoadDashboardSettings()
		{
			if (!Common.ResourceManager.Instance.AppSettingsFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(Common.ResourceManager.Instance.AppSettingsFile.LocalPath);
			var node = document.SelectSingleNode(@"/DashboardSettings/SalesRep");
			if (node != null)
				SalesRep = node.InnerText;
			_themeSaveHelper.Deserialize(document.SelectNodes(@"//DashboardSettings/SelectedTheme").OfType<XmlNode>());
		}

		public void SaveDashboardSettings()
		{
			var xml = new StringBuilder();
			xml.AppendLine(@"<DashboardSettings>");
			if (!String.IsNullOrEmpty(SalesRep))
				xml.AppendLine(@"<SalesRep>" + SalesRep.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SalesRep>");
			xml.AppendLine(_themeSaveHelper.Serialize());
			xml.AppendLine(@"</DashboardSettings>");
			using (var sw = new StreamWriter(Common.ResourceManager.Instance.AppSettingsFile.LocalPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}
	}
}