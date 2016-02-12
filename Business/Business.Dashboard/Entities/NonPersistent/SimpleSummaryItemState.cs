using System;
using System.Text;
using System.Xml;

namespace Asa.Business.Dashboard.Entities.NonPersistent
{
	public class SimpleSummaryItemState
	{
		public bool ShowValue { get; set; }
		public bool ShowDescription { get; set; }
		public bool ShowMonthly { get; set; }
		public bool ShowTotal { get; set; }

		public int Order { get; set; }
		public string Value { get; set; }
		public string Description { get; set; }
		public decimal? Monthly { get; set; }
		public decimal? Total { get; set; }

		public SimpleSummaryItemState()
		{
			ShowValue = true;
			ShowDescription = false;
			ShowMonthly = false;
			ShowTotal = false;

			Order = 0;
			Value = string.Empty;
			Description = string.Empty;
		}

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<ShowDescription>" + ShowDescription + @"</ShowDescription>");
			result.AppendLine(@"<ShowMonthly>" + ShowMonthly + @"</ShowMonthly>");
			result.AppendLine(@"<ShowTotal>" + ShowTotal + @"</ShowTotal>");
			result.AppendLine(@"<ShowValue>" + ShowValue + @"</ShowValue>");

			result.AppendLine(@"<Order>" + Order + @"</Order>");
			result.AppendLine(@"<Value>" + Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Value>");
			result.AppendLine(@"<Description>" + Description.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Description>");
			if (Monthly.HasValue)
				result.AppendLine(@"<Monthly>" + Monthly + @"</Monthly>");
			if (Total.HasValue)
				result.AppendLine(@"<Total>" + Total + @"</Total>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool = false;
			int tempInt = 0;
			decimal tempDecimal = 0;

			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "ShowDescription":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDescription = tempBool;
						break;
					case "ShowMonthly":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowMonthly = tempBool;
						break;
					case "ShowTotal":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotal = tempBool;
						break;
					case "ShowValue":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowValue = tempBool;
						break;
					case "Order":
						if (int.TryParse(childNode.InnerText, out tempInt))
							Order = tempInt;
						break;
					case "Monthly":
						if (Decimal.TryParse(childNode.InnerText, out tempDecimal))
							Monthly = tempDecimal;
						break;
					case "Total":
						if (Decimal.TryParse(childNode.InnerText, out tempDecimal))
							Total = tempDecimal;
						break;
					case "Value":
						Value = childNode.InnerText;
						break;
					case "Description":
						Description = childNode.InnerText;
						break;
				}
			}
		}
	}
}
