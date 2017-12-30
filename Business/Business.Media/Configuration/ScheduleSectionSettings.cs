using System;
using System.Text;
using System.Xml;

namespace Asa.Business.Media.Configuration
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
		public bool UniversalToggles { get; set; }

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
					case "ShowLenght":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowLenght = temp;
						}
						break;
					case "ShowDaypart":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowDaypart = temp;
						}
						break;
					case "ShowSpots":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowSpots = temp;
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
					case "ShowCost":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowCost = temp;
						}
						break;
					case "ShowLogo":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowLogo = temp;
						}
						break;
					case "UseGenericDateColumns":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								UseGenericDateColumns = temp;
						}
						break;
					case "UseDecimalRates":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								UseDecimalRates = temp;
						}
						break;
					case "OutputNoBrackets":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								OutputNoBrackets = temp;
						}
						break;
					case "ShowTotalPeriods":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowTotalPeriods = temp;
						}
						break;
					case "ShowTotalSpots":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowTotalSpots = temp;
						}
						break;
					case "ShowAverageRate":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowAverageRate = temp;
						}
						break;
					case "ShowTotalRate":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowTotalRate = temp;
						}
						break;
					case "ShowNetRate":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowNetRate = temp;
						}
						break;
					case "ShowDiscount":
						{
							if (Boolean.TryParse(childNode.InnerText, out var temp))
								ShowDiscount = temp;
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
				case "Length":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowLenght = temp;
					}
					break;
				case "Daypart":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowDaypart = temp;
					}
					break;
				case "Weeks":
				case "Months":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowSpots = temp;
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
				case "Total Cost":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowCost = temp;
					}
					break;
				case "Logo":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowLogo = temp;
					}
					break;
				case "Generic Dates":
					{
						if (Boolean.TryParse(value, out var temp))
							UseGenericDateColumns = temp;
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
				case "No Brackets for Station on slide":
					{
						if (Boolean.TryParse(value, out var temp))
							OutputNoBrackets = temp;
					}
					break;
				case "Info-Total Weeks":
				case "Info-Total Months":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowTotalPeriods = temp;
					}
					break;
				case "Info-Total Spots":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowTotalSpots = temp;
					}
					break;
				case "Info-Average Rate":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowAverageRate = temp;
					}
					break;
				case "Info-Gross Investment":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowTotalRate = temp;
					}
					break;
				case "Info-Net Investment":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowNetRate = temp;
					}
					break;
				case "Info-Agency Discount":
					{
						if (Boolean.TryParse(value, out var temp))
							ShowDiscount = temp;
					}
					break;
			}
		}
	}
}
