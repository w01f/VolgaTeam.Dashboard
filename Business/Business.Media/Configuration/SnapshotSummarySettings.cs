using System;
using System.Text;
using System.Xml;

namespace Asa.Business.Media.Configuration
{
	public class SnapshotSummarySettings
	{
		public bool ShowLineId { get; set; }
		public bool ShowLogo { get; set; }
		public bool ShowCampaign { get; set; }
		public bool ShowComments { get; set; }
		public bool ShowSpots { get; set; }
		public bool ShowCost { get; set; }
		public bool ShowTotalWeeks { get; set; }
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
			result.AppendLine(@"<ShowSpots>" + ShowSpots + @"</ShowSpots>");
			result.AppendLine(@"<ShowCost>" + ShowCost + @"</ShowCost>");
			result.AppendLine(@"<ShowTotalWeeks>" + ShowTotalWeeks + @"</ShowTotalWeeks>");
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
					#region Options
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
					case "ShowSpots":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowSpots = temp;
						}
						break;
					case "ShowCost":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowCost = temp;
						}
						break;
					case "ShowTotalWeeks":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowTotalWeeks = temp;
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
					#endregion
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
				case "Weekly Spots":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowSpots = temp;
					}
					break;
				case "Weekly Cost":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowCost = temp;
					}
					break;
				case "Total Weeks":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowTotalWeeks = temp;
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
