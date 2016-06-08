using System.Text;
using System.Xml;
using Asa.Business.Online.Dictionaries;

namespace Asa.Business.Online.Configuration
{
	public class DigitalProductListViewSettings
	{
		public bool EnableDigitalDimensions { get; set; }
		public bool EnableDigitalStrategy { get; set; }
		public bool EnableDigitalLocation { get; set; }
		public bool EnableDigitalTargeting { get; set; }
		public bool EnableDigitalRichMedia { get; set; }

		public bool ShowDigitalDimensions { get; set; }
		public bool ShowDigitalStrategy { get; set; }
		public bool ShowDigitalLocation { get; set; }
		public bool ShowDigitalTargeting { get; set; }
		public bool ShowDigitalRichMedia { get; set; }

		public DigitalProductListViewSettings()
		{
			EnableDigitalDimensions = true;
			EnableDigitalStrategy = true;
			EnableDigitalLocation = true;
			EnableDigitalTargeting = true;
			EnableDigitalRichMedia = true;

			ShowDigitalDimensions = true;
			ShowDigitalStrategy = true;
			ShowDigitalLocation = true;
			ShowDigitalTargeting = true;
			ShowDigitalRichMedia = true;
		}

		public void ResetToDefault()
		{
			var defaultSettings = new XmlDocument();
			defaultSettings.LoadXml(@"<DefaultSettings>" + ListManager.Instance.DefaultHomeViewSettings.Serialize() + @"</DefaultSettings>");
			Deserialize(defaultSettings.SelectSingleNode(@"/DefaultSettings"));
		}

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<EnableDigitalDimensions>" + EnableDigitalDimensions + @"</EnableDigitalDimensions>");
			result.AppendLine(@"<EnableDigitalStrategy>" + EnableDigitalStrategy + @"</EnableDigitalStrategy>");
			result.AppendLine(@"<EnableDigitalLocation>" + EnableDigitalLocation + @"</EnableDigitalLocation>");
			result.AppendLine(@"<EnableDigitalTargeting>" + EnableDigitalTargeting + @"</EnableDigitalTargeting>");
			result.AppendLine(@"<EnableDigitalRichMedia>" + EnableDigitalRichMedia + @"</EnableDigitalRichMedia>");

			result.AppendLine(@"<ShowDigitalDimensions>" + ShowDigitalDimensions + @"</ShowDigitalDimensions>");
			result.AppendLine(@"<ShowDigitalStrategy>" + ShowDigitalStrategy + @"</ShowDigitalStrategy>");
			result.AppendLine(@"<ShowDigitalLocation>" + ShowDigitalLocation + @"</ShowDigitalLocation>");
			result.AppendLine(@"<ShowDigitalTargeting>" + ShowDigitalTargeting + @"</ShowDigitalTargeting>");
			result.AppendLine(@"<ShowDigitalRichMedia>" + ShowDigitalRichMedia + @"</ShowDigitalRichMedia>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				bool tempBool;
				switch (childNode.Name)
				{
					case "EnableDigitalDimensions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDigitalDimensions = tempBool;
						break;
					case "EnableDigitalStrategy":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDigitalStrategy = tempBool;
						break;
					case "EnableDigitalLocation":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDigitalLocation = tempBool;
						break;
					case "EnableDigitalTargeting":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDigitalTargeting = tempBool;
						break;
					case "EnableDigitalRichMedia":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDigitalRichMedia = tempBool;
						break;

					case "ShowDigitalDimensions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDigitalDimensions = tempBool;
						break;
					case "ShowDigitalStrategy":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDigitalStrategy = tempBool;
						break;
					case "ShowDigitalLocation":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDigitalLocation = tempBool;
						break;
					case "ShowDigitalTargeting":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDigitalTargeting = tempBool;
						break;
					case "ShowDigitalRichMedia":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDigitalRichMedia = tempBool;
						break;
				}
			}

			ShowDigitalDimensions &= EnableDigitalDimensions;
			ShowDigitalStrategy &= EnableDigitalStrategy;
			ShowDigitalLocation &= EnableDigitalLocation;
			ShowDigitalTargeting &= EnableDigitalTargeting;
			ShowDigitalRichMedia &= EnableDigitalRichMedia;
		}
	}
}
