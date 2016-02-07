using System;
using System.Text;
using System.Xml;

namespace Asa.Business.Online.Configuration
{
	public class DigitalProductSettings
	{
		public bool EnableCategory { get; set; }
		public bool EnableFlightDates { get; set; }
		public bool EnableDuration { get; set; }

		public bool ShowFlightDates { get; set; }
		public bool ShowDuration { get; set; }

		public int DefaultPricing { get; set; }

		public DigitalProductSettings()
		{
			EnableCategory = true;
			EnableFlightDates = true;
			EnableDuration = true;

			ShowFlightDates = true;
			ShowDuration = true;

			DefaultPricing = 0;
		}

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<EnableCategory>" + EnableCategory + @"</EnableCategory>");
			result.AppendLine(@"<EnableFlightDates>" + EnableFlightDates + @"</EnableFlightDates>");
			result.AppendLine(@"<EnableDuration>" + EnableDuration + @"</EnableDuration>");

			result.AppendLine(@"<ShowFlightDates>" + ShowFlightDates + @"</ShowFlightDates>");
			result.AppendLine(@"<ShowDuration>" + ShowDuration + @"</ShowDuration>");

			result.AppendLine(@"<DefaultPricing>" + DefaultPricing + @"</DefaultPricing>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "EnableCategory":
						{
							bool temp;
							if (bool.TryParse(childNode.InnerText, out temp))
								EnableCategory = temp;
						}
						break;
					case "EnableFlightDates":
						{
							bool temp;
							if (bool.TryParse(childNode.InnerText, out temp))
								EnableFlightDates = temp;
						}
						break;
					case "EnableDuration":
						{
							bool temp;
							if (bool.TryParse(childNode.InnerText, out temp))
								EnableDuration = temp;
						}
						break;

					case "ShowFlightDates":
						{
							bool temp;
							if (bool.TryParse(childNode.InnerText, out temp))
								ShowFlightDates = temp;
						}
						break;
					case "ShowDuration":
						{
							bool temp;
							if (bool.TryParse(childNode.InnerText, out temp))
								ShowDuration = temp;
						}
						break;

					case "DefaultPricing":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								DefaultPricing = temp;
						}
						break;
				}
			}

			ShowFlightDates &= EnableFlightDates;
			ShowDuration &= EnableDuration;
		}
	}
}
