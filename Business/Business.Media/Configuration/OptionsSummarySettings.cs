using System;
using System.Text;
using System.Xml;

namespace Asa.Business.Media.Configuration
{
	public class OptionsSummarySettings
	{
		public bool ShowLineId { get; set; }
		public bool ShowLogo { get; set; }
		public bool ShowCampaign { get; set; }
		public bool ShowComments { get; set; }
		public bool ShowWeeklySpots { get; set; }
		public bool ShowWeeklyCost { get; set; }
		public bool ShowTotalWeeks { get; set; }
		public bool ShowMonthlySpots { get; set; }
		public bool ShowMonthlyCost { get; set; }
		public bool ShowTotalMonths { get; set; }
		public bool ShowTotalSpots { get; set; }
		public bool ShowTotalCost { get; set; }
		public bool ShowTallySpots { get; set; }
		public bool ShowTallyCost { get; set; }
		public bool UseDecimalRates { get; set; }
		public bool ShowSpotsX { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<ShowLineId>" + ShowLineId + @"</ShowLineId>");
			result.AppendLine(@"<ShowLogo>" + ShowLogo + @"</ShowLogo>");
			result.AppendLine(@"<ShowCampaign>" + ShowCampaign + @"</ShowCampaign>");
			result.AppendLine(@"<ShowComments>" + ShowComments + @"</ShowComments>");
			result.AppendLine(@"<ShowWeeklySpots>" + ShowWeeklySpots + @"</ShowWeeklySpots>");
			result.AppendLine(@"<ShowWeeklyCost>" + ShowWeeklyCost + @"</ShowWeeklyCost>");
			result.AppendLine(@"<ShowTotalWeeks>" + ShowTotalWeeks + @"</ShowTotalWeeks>");
			result.AppendLine(@"<ShowMonthlySpots>" + ShowMonthlySpots + @"</ShowMonthlySpots>");
			result.AppendLine(@"<ShowMonthlyCost>" + ShowMonthlyCost + @"</ShowMonthlyCost>");
			result.AppendLine(@"<ShowTotalMonths>" + ShowTotalMonths + @"</ShowTotalMonths>");
			result.AppendLine(@"<ShowTotalSpots>" + ShowTotalSpots + @"</ShowTotalSpots>");
			result.AppendLine(@"<ShowTotalCost>" + ShowTotalCost + @"</ShowTotalCost>");
			result.AppendLine(@"<ShowTallySpots>" + ShowTallySpots + @"</ShowTallySpots>");
			result.AppendLine(@"<ShowTallyCost>" + ShowTallyCost + @"</ShowTallyCost>");
			result.AppendLine(@"<UseDecimalRates>" + UseDecimalRates + @"</UseDecimalRates>");
			result.AppendLine(@"<ShowSpotsX>" + ShowSpotsX + @"</ShowSpotsX>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "ShowLineId":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowLineId = temp;
						}
						break;
					case "ShowLogo":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowLogo = temp;
						}
						break;
					case "ShowCampaign":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowCampaign = temp;
						}
						break;
					case "ShowComments":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowComments = temp;
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
					case "ShowTotalWeeks":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowTotalWeeks = temp;
						}
						break;
					case "ShowMonthlySpots":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowMonthlySpots = temp;
						}
						break;
					case "ShowMonthlyCost":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowMonthlyCost = temp;
						}
						break;
					case "ShowTotalMonths":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowTotalMonths = temp;
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
				}
		}

		public void ApplyValue(string propertyName, string value)
		{
			switch (propertyName)
			{
				case "Line ID":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowLineId = temp;
					}
					break;
				case "Logo":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowLogo = temp;
					}
					break;
				case "Campaign":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowCampaign = temp;
					}
					break;
				case "Comments":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowComments = temp;
					}
					break;
				case "If Weekly Spots":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowWeeklySpots = temp;
					}
					break;
				case "If Weekly Cost":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowWeeklyCost = temp;
					}
					break;
				case "If Total Weeks":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowTotalWeeks = temp;
					}
					break;
				case "If Monthly Spots":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowMonthlySpots = temp;
					}
					break;
				case "If Monthly Cost":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowMonthlyCost = temp;
					}
					break;
				case "If Total Months":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowTotalMonths = temp;
					}
					break;
				case "If Total Spots":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowTotalSpots = temp;
					}
					break;
				case "Cost Column":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowTotalCost = temp;
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
				case "Use Decimals with Rates":
					{
						if (Boolean.TryParse(value, out var temp))
							UseDecimalRates = temp;
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
