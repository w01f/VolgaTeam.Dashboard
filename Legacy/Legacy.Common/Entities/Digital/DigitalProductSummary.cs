using System;
using System.Xml;

namespace Asa.Legacy.Common.Entities.Digital
{
	public class DigitalProductSummary
	{
		public string Statement { get; set; }
		public decimal? MonthlyInvestment { get; set; }
		public decimal? TotalInvestment { get; set; }

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Statement":
						Statement = childNode.InnerText;
						break;
					case "MonthlyInvestment":
						{
							decimal temp;
							if (Decimal.TryParse(childNode.InnerText, out temp))
								MonthlyInvestment = temp;
						}
						break;
					case "TotalInvestment":
						{
							decimal temp;
							if (Decimal.TryParse(childNode.InnerText, out temp))
								TotalInvestment = temp;
						}
						break;

				}
			}
		}
	}
}
