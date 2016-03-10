using System.Xml;

namespace Asa.Legacy.Common.Entities.Summary
{
	public class BaseSummarySettings
	{
		public bool ShowAdvertiser { get; set; }
		public bool ShowDecisionMaker { get; set; }
		public bool ShowPresentationDate { get; set; }
		public bool ShowFlightDates { get; set; }
		public bool ShowMonthly { get; set; }
		public bool ShowTotal { get; set; }
		public bool TableOutput { get; set; }

		public string SlideHeader { get; set; }
		public decimal? MonthlyValue { get; set; }
		public decimal? TotalValue { get; set; }

		public BaseSummarySettings()
		{
			ShowAdvertiser = true;
			ShowDecisionMaker = true;
			ShowPresentationDate = true;
			ShowFlightDates = true;
			ShowMonthly = false;
			ShowTotal = false;

			SlideHeader = string.Empty;
		}

		public virtual void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				decimal tempDecimal;
				bool tempBool;
				switch (childNode.Name)
				{
					case "ShowAdvertiser":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAdvertiser = tempBool;
						break;
					case "ShowDecisionMaker":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDecisionMaker = tempBool;
						break;
					case "ShowPresentationDate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPresentationDate = tempBool;
						break;
					case "ShowFlightDates":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowFlightDates = tempBool;
						break;
					case "ShowMonthly":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowMonthly = tempBool;
						break;
					case "ShowTotal":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotal = tempBool;
						break;
					case "TableOutput":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							TableOutput = tempBool;
						break;
					case "SlideHeader":
						SlideHeader = childNode.InnerText;
						break;
					case "MonthlyValue":
						if (decimal.TryParse(childNode.InnerText, out tempDecimal) && tempDecimal > 0)
							MonthlyValue = tempDecimal; break;
					case "TotalValue":
						if (decimal.TryParse(childNode.InnerText, out tempDecimal) && tempDecimal > 0)
							TotalValue = tempDecimal;
						break;
				}
			}
		}
	}
}
