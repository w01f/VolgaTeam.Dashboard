using System;
using System.Text;
using System.Xml;

namespace Asa.Business.Media.Configuration
{
	public class SnapshotSettings
	{
		public bool ShowStation { get; set; }
		public bool ShowLenght { get; set; }
		public bool ShowProgram { get; set; }
		public bool ShowDaypart { get; set; }
		public bool ShowTime { get; set; }
		public bool ShowRate { get; set; }
		public bool ShowTotalSpots { get; set; }
		public bool ShowTotalCost { get; set; }
		public bool ShowWeeklySpots { get; set; }
		public bool ShowWeeklyCost { get; set; }
		public bool ShowLogo { get; set; }
		public bool ShowAverageRate { get; set; }
		public bool ShowLineId { get; set; }
		public bool ShowTotalRow { get; set; }
		public bool UseDecimalRates { get; set; }
		public bool ShowSpotsX { get; set; }
		public bool ShowSpotsPerWeek { get; set; }
		public bool UniversalToggles { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<ShowStation>" + ShowStation + @"</ShowStation>");
			result.AppendLine(@"<ShowLenght>" + ShowLenght + @"</ShowLenght>");
			result.AppendLine(@"<ShowProgram>" + ShowProgram + @"</ShowProgram>");
			result.AppendLine(@"<ShowDaypart>" + ShowDaypart + @"</ShowDaypart>");
			result.AppendLine(@"<ShowTime>" + ShowTime + @"</ShowTime>");
			result.AppendLine(@"<ShowRate>" + ShowRate + @"</ShowRate>");
			result.AppendLine(@"<ShowWeeklySpots>" + ShowWeeklySpots + @"</ShowWeeklySpots>");
			result.AppendLine(@"<ShowWeeklyCost>" + ShowWeeklyCost + @"</ShowWeeklyCost>");
			result.AppendLine(@"<ShowTotalSpots>" + ShowTotalSpots + @"</ShowTotalSpots>");
			result.AppendLine(@"<ShowTotalCost>" + ShowTotalCost + @"</ShowTotalCost>");
			result.AppendLine(@"<ShowLogo>" + ShowLogo + @"</ShowLogo>");
			result.AppendLine(@"<ShowAverageRate>" + ShowAverageRate + @"</ShowAverageRate>");
			result.AppendLine(@"<ShowLineId>" + ShowLineId + @"</ShowLineId>");
			result.AppendLine(@"<ShowTotalRow>" + ShowTotalRow + @"</ShowTotalRow>");
			result.AppendLine(@"<UseDecimalRates>" + UseDecimalRates + @"</UseDecimalRates>");
			result.AppendLine(@"<ShowSpotsX>" + ShowSpotsX + @"</ShowSpotsX>");
			result.AppendLine(@"<ShowSpotsPerWeek>" + ShowSpotsPerWeek + @"</ShowSpotsPerWeek>");
			result.AppendLine(@"<UniversalToggles>" + UniversalToggles + @"</UniversalToggles>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "ShowStation":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowStation = temp;
						}
						break;
					case "ShowLenght":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowLenght = temp;
						}
						break;
					case "ShowProgram":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowProgram = temp;
						}
						break;
					case "ShowDaypart":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowDaypart = temp;
						}
						break;
					case "ShowTime":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowTime = temp;
						}
						break;
					case "ShowRate":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowRate = temp;
						}
						break;
					case "ShowWeeklySpots":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowWeeklySpots = temp;
						}
						break;
					case "ShowWeeklyCost":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowWeeklyCost = temp;
						}
						break;
					case "ShowTotalSpots":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowTotalSpots = temp;
						}
						break;
					case "ShowTotalCost":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowTotalCost = temp;
						}
						break;
					case "ShowLogo":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowLogo = temp;
						}
						break;
					case "ShowAverageRate":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowAverageRate = temp;
						}
						break;
					case "ShowLineId":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowLineId = temp;
						}
						break;
					case "UseDecimalRates":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								UseDecimalRates = temp;
						}
						break;
					case "ShowTotalRow":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowTotalRow = temp;
						}
						break;
					case "ShowSpotsX":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowSpotsX = temp;
						}
						break;
					case "ShowSpotsPerWeek":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowSpotsPerWeek = temp;
						}
						break;
					case "UniversalToggles":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								UniversalToggles = temp;
						}
						break;
				}
		}

		public void ApplyValue(string propertyName, string value)
		{
			switch (propertyName)
			{
				case "Station":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowStation = temp;
					}
					break;
				case "Length":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowLenght = temp;
					}
					break;
				case "Program":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowProgram = temp;
					}
					break;
				case "Daypart":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowDaypart = temp;
					}
					break;
				case "Time":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowTime = temp;
					}
					break;
				case "Rate":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowRate = temp;
					}
					break;
				case "Weekly Spots":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowWeeklySpots = temp;
					}
					break;
				case "Weekly Cost":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowWeeklyCost = temp;
					}
					break;
				case "Total Spots":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowTotalSpots = temp;
					}
					break;
				case "Total Cost":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowTotalCost = temp;
					}
					break;
				case "Logo":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowLogo = temp;
					}
					break;
				case "Average Rate":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowAverageRate = temp;
					}
					break;
				case "Line ID":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowLineId = temp;
					}
					break;
				case "Total Row":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowTotalRow = temp;
					}
					break;
				case "Use Decimals with Rates":
					{
						if (Boolean.TryParse(value, out var temp))
							UseDecimalRates = temp;
					}
					break;
				case "Universal Toggles":
					{
						if (Boolean.TryParse(value, out var temp))
							UniversalToggles = temp;
					}
					break;
				case "Show X in spot #s":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowSpotsX = temp;
					}
					break;
			}
		}
	}
}
