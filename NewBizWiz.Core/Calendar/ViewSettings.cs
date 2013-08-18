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
			LocalSettingsPath = string.Format(@"{0}\newlocaldirect.com\xml\app\CalendarSettings.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
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
		public bool SlideInfoDocked { get; set; }
		public int SlideInfoFloatLeft { get; set; }
		public int SlideInfoFloatTop { get; set; }
		#endregion

		public CalendarSettings()
		{
			SlideInfoDocked = true;
		}

		public string Serialize()
		{
			var result = new StringBuilder();

			#region Slide Info Properties
			result.AppendLine(@"<SlideInfoVisible>" + SlideInfoVisible + @"</SlideInfoVisible>");
			result.AppendLine(@"<SlideInfoDocked>" + SlideInfoDocked + @"</SlideInfoDocked>");
			result.AppendLine(@"<SlideInfoFloatLeft>" + SlideInfoFloatLeft + @"</SlideInfoFloatLeft>");
			result.AppendLine(@"<SlideInfoFloatTop>" + SlideInfoFloatTop + @"</SlideInfoFloatTop>");
			#endregion

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool = false;
			int tempInt;

			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					#region Slide Info Properties
					case "SlideInfoVisible":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							SlideInfoVisible = tempBool;
						break;
					case "SlideInfoDocked":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							SlideInfoDocked = tempBool;
						break;
					case "SlideInfoFloatLeft":
						if (int.TryParse(childNode.InnerText, out tempInt))
							SlideInfoFloatLeft = tempInt;
						break;
					case "SlideInfoFloatTop":
						if (int.TryParse(childNode.InnerText, out tempInt))
							SlideInfoFloatTop = tempInt;
						break;
					#endregion
				}
			}
		}
	}
}