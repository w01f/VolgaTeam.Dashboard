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
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "ShowStation":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowStation = temp;
						}
						break;
					case "ShowProgram":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowProgram = temp;
						}
						break;
					case "ShowDay":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowDay = temp;
						}
						break;
					case "ShowTime":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowTime = temp;
						}
						break;
					case "ShowRate":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowRate = temp;
						}
						break;
					case "ShowLenght":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowLenght = temp;
						}
						break;
					case "ShowLogo":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowLogo = temp;
						}
						break;
					case "ShowWeeklySpots":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowWeeklySpots = temp;
						}
						break;
					case "ShowMonthlySpots":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowMonthlySpots = temp;
						}
						break;
					case "ShowTotalSpots":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowTotalSpots = temp;
						}
						break;
					case "ShowLineId":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowLineId = temp;
						}
						break;
					case "ShowCost":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowCost = temp;
						}
						break;
					case "ShowTallySpots":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowTallySpots = temp;
						}
						break;
					case "ShowTallyCost":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowTallyCost = temp;
						}
						break;
					case "ShowAverageRate":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowAverageRate = temp;
						}
						break;
					case "UseDecimalRates":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								UseDecimalRates = temp;
						}
						break;
					case "ShowSpotsX":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowSpotsX = temp;
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
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowStation = temp;
					}
					break;
				case "Program":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowProgram = temp;
					}
					break;
				case "Day":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowDay = temp;
					}
					break;
				case "Time":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowTime = temp;
					}
					break;
				case "Rate":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowRate = temp;
					}
					break;
				case "Length":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowLenght = temp;
					}
					break;
				case "Logo":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowLogo = temp;
					}
					break;
				case "Weekly Spots":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowWeeklySpots = temp;
					}
					break;
				case "Monthly Spots":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowMonthlySpots = temp;
					}
					break;
				case "Total Spots":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowTotalSpots = temp;
					}
					break;
				case "Line ID":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowLineId = temp;
					}
					break;
				case "Cost Column":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowCost = temp;
					}
					break;
				case "Tally Spots":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowTallySpots = temp;
					}
					break;
				case "Tally Cost":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowTallyCost = temp;
					}
					break;
				case "Average Rate":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowAverageRate = temp;
					}
					break;
				case "Use Decimals with Rates":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							UseDecimalRates = temp;
					}
					break;
				case "Show X in spot #s":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowSpotsX = temp;
					}
					break;
			}
		}
	}
}
