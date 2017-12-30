using System;
using System.Text;
using System.Xml;

namespace Asa.Business.Media.Configuration
{
	public class OptionsSettings
	{
		public bool ShowStation { get; set; }
		public bool ShowProgram { get; set; }
		public bool ShowDay { get; set; }
		public bool ShowTime { get; set; }
		public bool ShowRate { get; set; }
		public bool ShowLenght { get; set; }
		public bool ShowLogo { get; set; }
		public bool ShowWeeklySpots { get; set; }
		public bool ShowMonthlySpots { get; set; }
		public bool ShowTotalSpots { get; set; }
		public bool ShowLineId { get; set; }
		public bool ShowCost { get; set; }
		public bool ShowTallySpots { get; set; }
		public bool ShowTallyCost { get; set; }
		public bool ShowAverageRate { get; set; }
		public bool UseDecimalRates { get; set; }
		public bool ShowSpotsX { get; set; }
		public bool UniversalToggles { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<ShowStation>" + ShowStation + @"</ShowStation>");
			result.AppendLine(@"<ShowProgram>" + ShowProgram + @"</ShowProgram>");
			result.AppendLine(@"<ShowDay>" + ShowDay + @"</ShowDay>");
			result.AppendLine(@"<ShowTime>" + ShowTime + @"</ShowTime>");
			result.AppendLine(@"<ShowRate>" + ShowRate + @"</ShowRate>");
			result.AppendLine(@"<ShowLenght>" + ShowLenght + @"</ShowLenght>");
			result.AppendLine(@"<ShowLogo>" + ShowLogo + @"</ShowLogo>");
			result.AppendLine(@"<ShowWeeklySpots>" + ShowWeeklySpots + @"</ShowWeeklySpots>");
			result.AppendLine(@"<ShowMonthlySpots>" + ShowMonthlySpots + @"</ShowMonthlySpots>");
			result.AppendLine(@"<ShowTotalSpots>" + ShowTotalSpots + @"</ShowTotalSpots>");
			result.AppendLine(@"<ShowLineId>" + ShowLineId + @"</ShowLineId>");
			result.AppendLine(@"<ShowCost>" + ShowCost + @"</ShowCost>");
			result.AppendLine(@"<ShowTallySpots>" + ShowTallySpots + @"</ShowTallySpots>");
			result.AppendLine(@"<ShowTallyCost>" + ShowTallyCost + @"</ShowTallyCost>");
			result.AppendLine(@"<ShowAverageRate>" + ShowAverageRate + @"</ShowAverageRate>");
			result.AppendLine(@"<UseDecimalRates>" + UseDecimalRates + @"</UseDecimalRates>");
			result.AppendLine(@"<ShowSpotsX>" + ShowSpotsX + @"</ShowSpotsX>");
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
					case "ShowProgram":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowProgram = temp;
						}
						break;
					case "ShowDay":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowDay = temp;
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
					case "ShowLenght":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowLenght = temp;
						}
						break;
					case "ShowLogo":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowLogo = temp;
						}
						break;
					case "ShowWeeklySpots":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowWeeklySpots = temp;
						}
						break;
					case "ShowMonthlySpots":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowMonthlySpots = temp;
						}
						break;
					case "ShowTotalSpots":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowTotalSpots = temp;
						}
						break;
					case "ShowLineId":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowLineId = temp;
						}
						break;
					case "ShowCost":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowCost = temp;
						}
						break;
					case "ShowTallySpots":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowTallySpots = temp;
						}
						break;
					case "ShowTallyCost":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowTallyCost = temp;
						}
						break;
					case "ShowAverageRate":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowAverageRate = temp;
						}
						break;
					case "UseDecimalRates":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								UseDecimalRates = temp;
						}
						break;
					case "ShowSpotsX":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowSpotsX = temp;
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
				case "Program":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowProgram = temp;
					}
					break;
				case "Day":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowDay = temp;
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
				case "Length":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowLenght = temp;
					}
					break;
				case "Logo":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowLogo = temp;
					}
					break;
				case "Weekly Spots":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowWeeklySpots = temp;
					}
					break;
				case "Monthly Spots":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowMonthlySpots = temp;
					}
					break;
				case "Total Spots":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowTotalSpots = temp;
					}
					break;
				case "Line ID":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowLineId = temp;
					}
					break;
				case "Cost Column":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowCost = temp;
					}
					break;
				case "Tally Spots":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowTallySpots = temp;
					}
					break;
				case "Tally Cost":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowTallyCost = temp;
					}
					break;
				case "Average Rate":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowAverageRate = temp;
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
