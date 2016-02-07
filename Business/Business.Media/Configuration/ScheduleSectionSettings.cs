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
}
