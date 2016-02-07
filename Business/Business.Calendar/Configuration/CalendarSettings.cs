using System;
using System.Text;
using System.Xml;

namespace Asa.Business.Calendar.Configuration
{
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
