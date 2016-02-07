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
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowLineId = temp;
						}
						break;
					case "ShowLogo":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowLogo = temp;
						}
						break;
					case "ShowCampaign":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowCampaign = temp;
						}
						break;
					case "ShowComments":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowComments = temp;
						}
						break;
					case "ShowWeeklySpots":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowWeeklySpots = temp;
						}
						break;
					case "ShowWeeklyCost":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowWeeklyCost = temp;
						}
						break;
					case "ShowTotalWeeks":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowTotalWeeks = temp;
						}
						break;
					case "ShowMonthlySpots":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowMonthlySpots = temp;
						}
						break;
					case "ShowMonthlyCost":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowMonthlyCost = temp;
						}
						break;
					case "ShowTotalMonths":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowTotalMonths = temp;
						}
						break;
					case "ShowTotalSpots":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowTotalSpots = temp;
						}
						break;
					case "ShowTotalCost":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowTotalCost = temp;
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
				case "Line ID":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowLineId = temp;
					}
					break;
				case "Logo":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowLogo = temp;
					}
					break;
				case "Campaign":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowCampaign = temp;
					}
					break;
				case "Comments":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowComments = temp;
					}
					break;
				case "If Weekly Spots":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowWeeklySpots = temp;
					}
					break;
				case "If Weekly Cost":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowWeeklyCost = temp;
					}
					break;
				case "If Total Weeks":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowTotalWeeks = temp;
					}
					break;
				case "If Monthly Spots":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowMonthlySpots = temp;
					}
					break;
				case "If Monthly Cost":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowMonthlyCost = temp;
					}
					break;
				case "If Total Months":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowTotalMonths = temp;
					}
					break;
				case "If Total Spots":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowTotalSpots = temp;
					}
					break;
				case "Cost Column":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowTotalCost = temp;
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
