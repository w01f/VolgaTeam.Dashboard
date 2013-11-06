using System;
using System.IO;
using System.Text;
using System.Xml;

namespace AdBar.Plugins.Slides
{
	public class SettingsManager
	{
		private static SettingsManager _instance;
		private readonly string _settingsFileName = String.Format(@"{0}\newlocaldirect.com\xml\app\AdBar-Plugins-Slides.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

		public string SelectedSlideGroup { get; set; }
		public string SelectedSlideMaster { get; set; }

		public static SettingsManager Instance
		{
			get { return _instance ?? (_instance = new SettingsManager()); }
		}

		private SettingsManager()
		{
			LoadSettings();
		}

		private void LoadSettings()
		{
			if (File.Exists(_settingsFileName))
			{
				var document = new XmlDocument();
				document.Load(_settingsFileName);
				var node = document.SelectSingleNode(@"/Settings/SelectedSlideGroup");
				if (node != null)
					SelectedSlideGroup = node.InnerText;
				node = document.SelectSingleNode(@"/Settings/SelectedSlideMaster");
				if (node != null)
					SelectedSlideMaster = node.InnerText;
			}
		}

		public void SaveSettings()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<Settings>");
			if (!String.IsNullOrEmpty(SelectedSlideGroup))
				xml.AppendLine(@"<SelectedSlideGroup>" + SelectedSlideGroup.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedSlideGroup>");
			if (!String.IsNullOrEmpty(SelectedSlideMaster))
				xml.AppendLine(@"<SelectedSlideMaster>" + SelectedSlideMaster.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedSlideMaster>");
			xml.AppendLine(@"</Settings>");

			using (var sw = new StreamWriter(_settingsFileName, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}
	}
}