using System;
using System.IO;
using System.Text;
using System.Xml;

namespace NewBizWiz.OnlineSchedule.DigitalPackage.BusinessClasses
{
	public class SettingsManager
	{
		private static readonly SettingsManager _instance = new SettingsManager();
		private readonly string _settingsFileName = String.Format(@"{0}\newlocaldirect.com\xml\app_web_quick\Settings.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

		private SettingsManager()
		{
			HelpLinksPath = string.Format(@"{0}\newlocaldirect.com\app\HelpUrls\WebQuickHelp.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			IconPath = Path.Combine(Path.GetDirectoryName(typeof(SettingsManager).Assembly.Location), "icon.ico");
			SaveFolder = Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Outgoing", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)), "AppID-" + Core.Common.SettingsManager.Instance.AppID, @"Saved_Schedules\Web Quick");
			if (!Directory.Exists(SaveFolder))
				Directory.CreateDirectory(SaveFolder);
			LoadSettings();
		}

		public string SaveFolder { get; set; }
		public string HelpLinksPath { get; set; }
		public string IconPath { get; set; }

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
		}

		public void SaveSettings()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<Settings>");
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
	}
}