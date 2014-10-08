using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using NewBizWiz.Core.Common;

namespace NewBizWiz.OnlineSchedule.DigitalPackage.BusinessClasses
{
	public class SettingsManager
	{
		private static readonly SettingsManager _instance = new SettingsManager();
		protected ThemeSaveHelper _themeSaveHelper;
		private readonly string _settingsFileName = String.Format(@"{0}\newlocaldirect.com\xml\app_web_quick\Settings.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

		private SettingsManager()
		{
			HelpLinksPath = string.Format(@"{0}\newlocaldirect.com\app\HelpUrls\WebQuickHelp.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			SaveFolder = Path.Combine(Core.Common.SettingsManager.Instance.OutgoingFolderPath, @"Saved_Schedules\Web Quick");
			if (!Directory.Exists(SaveFolder))
				Directory.CreateDirectory(SaveFolder);
		}

		public string SaveFolder { get; set; }
		public string HelpLinksPath { get; set; }

		#region Local Settings
		public string LastOpenSchedule { get; set; }
		#endregion

		public static SettingsManager Instance
		{
			get { return _instance; }
		}

		private void LoadSettings()
		{
			if (!File.Exists(_settingsFileName)) return;
			var document = new XmlDocument();
			document.Load(_settingsFileName);
			var node = document.SelectSingleNode(@"/Settings/LastOpenSchedule");
			if (node != null)
				LastOpenSchedule = node.InnerText;
			_themeSaveHelper.Deserialize(document.SelectNodes(@"//Settings/SelectedTheme").OfType<XmlNode>());
		}

		public void SaveSettings()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<Settings>");
			xml.AppendLine(_themeSaveHelper.Serialize());
			if (!String.IsNullOrEmpty(LastOpenSchedule))
				xml.AppendLine(@"<LastOpenSchedule>" + LastOpenSchedule.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</LastOpenSchedule>");
			xml.AppendLine(@"</Settings>");

			var settingsFolder = Path.GetDirectoryName(_settingsFileName);
			if (!Directory.Exists(settingsFolder))
				Directory.CreateDirectory(settingsFolder);
			using (var sw = new StreamWriter(_settingsFileName, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}

		public void InitThemeHelper(ThemeManager themeManager)
		{
			_themeSaveHelper = new ThemeSaveHelper(themeManager);
			LoadSettings();
		}

		public string GetSelectedTheme(SlideType slideType)
		{
			return _themeSaveHelper.GetSelectedTheme(slideType).Name;
		}

		public void SetSelectedTheme(SlideType slideType, string themeName)
		{
			_themeSaveHelper.SetSelectedTheme(slideType, themeName);
		}
	}
}