using System;
using System.Text;
using System.Xml;

namespace Asa.Core.MediaSchedule
{
	public class ScheduleSectionSettings
	{
		public bool ShowStation { get; set; }
		public bool ShowProgram { get; set; }
		public bool ShowLenght { get; set; }
		public bool ShowDaypart { get; set; }
		public bool ShowSpots { get; set; }
		public bool ShowDay { get; set; }
		public bool ShowTime { get; set; }
		public bool ShowRate { get; set; }
		public bool ShowCost { get; set; }
		public bool ShowLogo { get; set; }
		public bool UseDecimalRates { get; set; }
		public bool OutputNoBrackets { get; set; }
		public bool ShowTotalPeriods { get; set; }
		public bool ShowTotalSpots { get; set; }
		public bool ShowAverageRate { get; set; }
		public bool ShowTotalRate { get; set; }
		public bool ShowNetRate { get; set; }
		public bool ShowDiscount { get; set; }
		public bool UseGenericDateColumns { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<ShowStation>" + ShowStation + @"</ShowStation>");
			result.AppendLine(@"<ShowProgram>" + ShowProgram + @"</ShowProgram>");
			result.AppendLine(@"<ShowLenght>" + ShowLenght + @"</ShowLenght>");
			result.AppendLine(@"<ShowDaypart>" + ShowDaypart + @"</ShowDaypart>");
			result.AppendLine(@"<ShowSpots>" + ShowSpots + @"</ShowSpots>");
			result.AppendLine(@"<ShowDay>" + ShowDay + @"</ShowDay>");
			result.AppendLine(@"<ShowTime>" + ShowTime + @"</ShowTime>");
			result.AppendLine(@"<ShowRate>" + ShowRate + @"</ShowRate>");
			result.AppendLine(@"<ShowCost>" + ShowCost + @"</ShowCost>");
			result.AppendLine(@"<ShowLogo>" + ShowLogo + @"</ShowLogo>");
			result.AppendLine(@"<UseGenericDateColumns>" + UseGenericDateColumns + @"</UseGenericDateColumns>");
			result.AppendLine(@"<UseDecimalRates>" + UseDecimalRates + @"</UseDecimalRates>");
			result.AppendLine(@"<OutputNoBrackets>" + OutputNoBrackets + @"</OutputNoBrackets>");
			result.AppendLine(@"<ShowTotalPeriods>" + ShowTotalPeriods + @"</ShowTotalPeriods>");
			result.AppendLine(@"<ShowTotalSpots>" + ShowTotalSpots + @"</ShowTotalSpots>");
			result.AppendLine(@"<ShowAverageRate>" + ShowAverageRate + @"</ShowAverageRate>");
			result.AppendLine(@"<ShowTotalRate>" + ShowTotalRate + @"</ShowTotalRate>");
			result.AppendLine(@"<ShowNetRate>" + ShowNetRate + @"</ShowNetRate>");
			result.AppendLine(@"<ShowDiscount>" + ShowDiscount + @"</ShowDiscount>");
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
					case "ShowLenght":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowLenght = temp;
						}
						break;
					case "ShowDaypart":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowDaypart = temp;
						}
						break;
					case "ShowSpots":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowSpots = temp;
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
					case "ShowCost":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowCost = temp;
						}
						break;
					case "ShowLogo":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowLogo = temp;
						}
						break;
					case "UseGenericDateColumns":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								UseGenericDateColumns = temp;
						}
						break;
					case "UseDecimalRates":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								UseDecimalRates = temp;
						}
						break;
					case "OutputNoBrackets":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								OutputNoBrackets = temp;
						}
						break;
					case "ShowTotalPeriods":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowTotalPeriods = temp;
						}
						break;
					case "ShowTotalSpots":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowTotalSpots = temp;
						}
						break;
					case "ShowAverageRate":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowAverageRate = temp;
						}
						break;
					case "ShowTotalRate":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowTotalRate = temp;
						}
						break;
					case "ShowNetRate":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowNetRate = temp;
						}
						break;
					case "ShowDiscount":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowDiscount = temp;
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
				case "Length":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowLenght = temp;
					}
					break;
				case "Daypart":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowDaypart = temp;
					}
					break;
				case "Weeks":
				case "Months":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowSpots = temp;
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
				case "Total Cost":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowCost = temp;
					}
					break;
				case "Logo":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowLogo = temp;
					}
					break;
				case "Generic Dates":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							UseGenericDateColumns = temp;
					}
					break;
				case "Use Decimals with Rates":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							UseDecimalRates = temp;
					}
					break;
				case "No Brackets for Station on slide":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							OutputNoBrackets = temp;
					}
					break;
				case "Info-Total Weeks":
				case "Info-Total Months":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowTotalPeriods = temp;
					}
					break;
				case "Info-Total Spots":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowTotalSpots = temp;
					}
					break;
				case "Info-Average Rate":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowAverageRate = temp;
					}
					break;
				case "Info-Gross Investment":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowTotalRate = temp;
					}
					break;
				case "Info-Net Investment":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowNetRate = temp;
					}
					break;
				case "Info-Agency Discount":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowDiscount = temp;
					}
					break;
			}
		}
	}

	public class SnapshotSettings
	{
		public bool ShowStation { get; set; }
		public bool ShowLenght { get; set; }
		public bool ShowProgram { get; set; }
		public bool ShowDaypart { get; set; }
		public bool ShowTime { get; set; }
		public bool ShowRate { get; set; }
		public bool ShowCost { get; set; }
		public bool ShowLogo { get; set; }
		public bool ShowTotalSpots { get; set; }
		public bool ShowAverageRate { get; set; }
		public bool ShowLineId { get; set; }
		public bool ShowTotalRow { get; set; }
		public bool UseDecimalRates { get; set; }
		public bool ShowSpotsX { get; set; }
		public bool ShowSpotsPerWeek { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<ShowStation>" + ShowStation + @"</ShowStation>");
			result.AppendLine(@"<ShowLenght>" + ShowLenght + @"</ShowLenght>");
			result.AppendLine(@"<ShowProgram>" + ShowProgram + @"</ShowProgram>");
			result.AppendLine(@"<ShowDaypart>" + ShowDaypart + @"</ShowDaypart>");
			result.AppendLine(@"<ShowTime>" + ShowTime + @"</ShowTime>");
			result.AppendLine(@"<ShowRate>" + ShowRate + @"</ShowRate>");
			result.AppendLine(@"<ShowCost>" + ShowCost + @"</ShowCost>");
			result.AppendLine(@"<ShowLogo>" + ShowLogo + @"</ShowLogo>");
			result.AppendLine(@"<ShowTotalSpots>" + ShowTotalSpots + @"</ShowTotalSpots>");
			result.AppendLine(@"<ShowAverageRate>" + ShowAverageRate + @"</ShowAverageRate>");
			result.AppendLine(@"<ShowLineId>" + ShowLineId + @"</ShowLineId>");
			result.AppendLine(@"<ShowTotalRow>" + ShowTotalRow + @"</ShowTotalRow>");
			result.AppendLine(@"<UseDecimalRates>" + UseDecimalRates + @"</UseDecimalRates>");
			result.AppendLine(@"<ShowSpotsX>" + ShowSpotsX + @"</ShowSpotsX>");
			result.AppendLine(@"<ShowSpotsPerWeek>" + ShowSpotsPerWeek + @"</ShowSpotsPerWeek>");
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
					case "ShowLenght":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowLenght = temp;
						}
						break;
					case "ShowProgram":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowProgram = temp;
						}
						break;
					case "ShowDaypart":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowDaypart = temp;
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
					case "ShowCost":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowCost = temp;
						}
						break;
					case "ShowLogo":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowLogo = temp;
						}
						break;
					case "ShowTotalSpots":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowTotalSpots = temp;
						}
						break;
					case "ShowAverageRate":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowAverageRate = temp;
						}
						break;
					case "ShowLineId":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowLineId = temp;
						}
						break;
					case "UseDecimalRates":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								UseDecimalRates = temp;
						}
						break;
					case "ShowTotalRow":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowTotalRow = temp;
						}
						break;
					case "ShowSpotsX":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowSpotsX = temp;
						}
						break;
					case "ShowSpotsPerWeek":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowSpotsPerWeek = temp;
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
				case "Length":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowLenght = temp;
					}
					break;
				case "Program":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowProgram = temp;
					}
					break;
				case "Daypart":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowDaypart = temp;
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
				case "Total Cost":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowCost = temp;
					}
					break;
				case "Logo":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowLogo = temp;
					}
					break;
				case "Total Spots":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowTotalSpots = temp;
					}
					break;
				case "Average Rate":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowAverageRate = temp;
					}
					break;
				case "Line ID":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowLineId = temp;
					}
					break;
				case "Total Row":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowTotalRow = temp;
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

	public class CalendarToggleSettings
	{
		public bool ShowLogo { get; set; }
		public bool ShowBigDate { get; set; }


		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<ShowLogo>" + ShowLogo + @"</ShowLogo>");
			result.AppendLine(@"<ShowBigDate>" + ShowBigDate + @"</ShowBigDate>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "ShowLogo":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowLogo = temp;
						}
						break;
					case "ShowBigDate":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowBigDate = temp;
						}
						break;
				}
		}

		public void ApplyValue(string propertyName, string value)
		{
			switch (propertyName)
			{
				case "Show Logo at Top of Slide":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowLogo = temp;
					}
					break;
				case "Show BIG Date numbers":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowBigDate = temp;
					}
					break;
			}
		}
	}
}