using System;
using System.IO;
using System.Text;
using System.Xml;

namespace NewBizWiz.Core.Calendar
{
	public class LocalSettings
	{
		public LocalSettings()
		{
			LocalSettingsPath = Path.Combine(Common.SettingsManager.Instance.SettingsPath, "CalendarSettings.xml");
			GraphicCalendarSettings = new CalendarSettings();
			Load();
		}

		public string LocalSettingsPath { get; set; }
		public CalendarSettings GraphicCalendarSettings { get; private set; }

		private void Load()
		{
			XmlNode node;
			if (File.Exists(LocalSettingsPath))
			{
				var document = new XmlDocument();
				document.Load(LocalSettingsPath);

				node = document.SelectSingleNode(@"/LocalSettings/GraphicCalendarSettings");
				if (node != null)
					GraphicCalendarSettings.Deserialize(node);
			}
		}

		public void Save()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<LocalSettings>");
			xml.AppendLine(@"<GraphicCalendarSettings>" + GraphicCalendarSettings.Serialize() + @"</GraphicCalendarSettings>");
			xml.AppendLine(@"</LocalSettings>");

			string userConfigurationPath = LocalSettingsPath;
			using (var sw = new StreamWriter(userConfigurationPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}
	}

	public class CalendarSettings
	{
		public DateTime SelectedMonth { get; set; }

		#region Slide Info
		public bool SlideInfoVisible { get; set; }
		#endregion

		public string Serialize()
		{
			var result = new StringBuilder();

			#region Slide Info Properties
			result.AppendLine(@"<SlideInfoVisible>" + SlideInfoVisible + @"</SlideInfoVisible>");
			#endregion

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					#region Slide Info Properties
					case "SlideInfoVisible":
						bool tempBool = false;
						if (bool.TryParse(childNode.InnerText, out tempBool))
							SlideInfoVisible = tempBool;
						break;
					#endregion
				}
			}
		}
	}
}