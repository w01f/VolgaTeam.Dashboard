using System;
using System.Xml;

namespace Asa.Legacy.Common.Entities.Digital
{
	public class DigitalLegend
	{
		public DigitalLegend()
		{
			Enabled = false;
			ShowWebsites = true;
			ShowProduct = true;
			ShowDimensions = false;
			ShowDates = false;
			ShowImpressions = false;
			ShowCPM = false;
			ShowInvestment = false;
		}

		public bool Enabled { get; set; }
		public bool AllowEdit { get; set; }
		public bool ApplyForAll { get; set; }
		public bool OutputOnlyOnce { get; set; }

		public bool ShowWebsites { get; set; }
		public bool ShowProduct { get; set; }
		public bool ShowDimensions { get; set; }
		public bool ShowDates { get; set; }
		public bool ShowImpressions { get; set; }
		public bool ShowCPM { get; set; }
		public bool ShowInvestment { get; set; }
		public string Info1 { get; set; }
		public string Info2 { get; set; }
		public string Info3 { get; set; }
		public decimal? Total { get; set; }
		public decimal? Monthly { get; set; }

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				bool tempBool;
				switch (childNode.Name)
				{
					case "Enabled":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							Enabled = tempBool;
						break;
					case "AllowEdit":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							AllowEdit = tempBool;
						break;
					case "ApplyForAll":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ApplyForAll = tempBool;
						break;
					case "OutputOnlyOnce":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							OutputOnlyOnce = tempBool;
						break;

					case "ShowWebsites":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowWebsites = tempBool;
						break;
					case "ShowProduct":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowProduct = tempBool;
						break;
					case "ShowDimensions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDimensions = tempBool;
						break;
					case "ShowDates":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDates = tempBool;
						break;
					case "ShowImpressions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowImpressions = tempBool;
						break;
					case "ShowCPM":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowCPM = tempBool;
						break;
					case "ShowInvestment":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowInvestment = tempBool;
						break;
					case "Info1":
						Info1 = childNode.InnerText;
						break;
					case "Info2":
						Info2 = childNode.InnerText;
						break;
					case "Info3":
						Info3 = childNode.InnerText;
						break;
					case "Total":
						decimal total;
						if (Decimal.TryParse(childNode.InnerText, out total))
							Total = total;
						break;
					case "Monthly":
						decimal monthly;
						if (Decimal.TryParse(childNode.InnerText, out monthly))
							Monthly = monthly;
						break;
				}
			}
		}
	}
}
