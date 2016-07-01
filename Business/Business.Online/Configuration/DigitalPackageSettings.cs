using System;
using System.Text;
using System.Xml;
using Asa.Business.Online.Enums;

namespace Asa.Business.Online.Configuration
{
	public abstract class DigitalPackageSettings
	{
		public FormulaType Formula { get; set; }

		public bool EnableCategory { get; set; }
		public bool EnableGroup { get; set; }
		public bool EnableProduct { get; set; }
		public bool EnableLocation { get; set; }
		public bool EnableImpressions { get; set; }
		public bool EnableCPM { get; set; }
		public bool EnableRate { get; set; }
		public bool EnableInvestment { get; set; }
		public bool EnableInfo { get; set; }
		public bool EnableScreenshot { get; set; }

		public bool ShowCategory { get; set; }
		public bool ShowGroup { get; set; }
		public bool ShowProduct { get; set; }
		public bool ShowLocation { get; set; }
		public bool ShowImpressions { get; set; }
		public bool ShowCPM { get; set; }
		public bool ShowRate { get; set; }
		public bool ShowInvestment { get; set; }
		public bool ShowInfo { get; set; }

		public bool ShowScreenshot { get; set; }

		protected DigitalPackageSettings()
		{
			Formula = FormulaType.CPM;

			EnableCategory = true;
			EnableGroup = true;
			EnableProduct = true;
			EnableLocation = true;
			EnableImpressions = true;
			EnableCPM = true;
			EnableRate = true;
			EnableInvestment = true;
			EnableInfo = true;
			EnableScreenshot = true;

			ShowCategory = true;
			ShowGroup = true;
			ShowProduct = true;
			ShowLocation = true;
			ShowImpressions = true;
			ShowCPM = true;
			ShowRate = true;
			ShowInvestment = true;
			ShowInfo = true;
			ShowScreenshot = true;
		}

		public abstract void ResetToDefault();

		public string Serialize()
		{
			var xml = new StringBuilder();
			xml.AppendLine(@"<Formula>" + (int)Formula + @"</Formula>");

			xml.AppendLine(@"<EnableCategory>" + EnableCategory + @"</EnableCategory>");
			xml.AppendLine(@"<EnableGroup>" + EnableGroup + @"</EnableGroup>");
			xml.AppendLine(@"<EnableProduct>" + EnableProduct + @"</EnableProduct>");
			xml.AppendLine(@"<EnableLocation>" + EnableLocation + @"</EnableLocation>");
			xml.AppendLine(@"<EnableImpressions>" + EnableImpressions + @"</EnableImpressions>");
			xml.AppendLine(@"<EnableCPM>" + EnableCPM + @"</EnableCPM>");
			xml.AppendLine(@"<EnableRate>" + EnableRate + @"</EnableRate>");
			xml.AppendLine(@"<EnableInvestment>" + EnableInvestment + @"</EnableInvestment>");
			xml.AppendLine(@"<EnableInfo>" + EnableInfo + @"</EnableInfo>");
			xml.AppendLine(@"<EnableScreenshot>" + EnableScreenshot + @"</EnableScreenshot>");

			xml.AppendLine(@"<ShowCategory>" + ShowCategory + @"</ShowCategory>");
			xml.AppendLine(@"<ShowGroup>" + ShowGroup + @"</ShowGroup>");
			xml.AppendLine(@"<ShowProduct>" + ShowProduct + @"</ShowProduct>");
			xml.AppendLine(@"<ShowLocation>" + ShowLocation + @"</ShowLocation>");
			xml.AppendLine(@"<ShowImpressions>" + ShowImpressions + @"</ShowImpressions>");
			xml.AppendLine(@"<ShowCPM>" + ShowCPM + @"</ShowCPM>");
			xml.AppendLine(@"<ShowRate>" + ShowRate + @"</ShowRate>");
			xml.AppendLine(@"<ShowInvestment>" + ShowInvestment + @"</ShowInvestment>");
			xml.AppendLine(@"<ShowInfo>" + ShowInfo + @"</ShowInfo>");
			xml.AppendLine(@"<ShowScreenshot>" + ShowScreenshot + @"</ShowScreenshot>");

			return xml.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool;
			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "Formula":
						int tempInt;
						if (Int32.TryParse(childNode.InnerText, out tempInt))
							Formula = (FormulaType)tempInt;
						break;

					case "EnableCategory":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableCategory = tempBool;
						break;
					case "EnableGroup":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableGroup = tempBool;
						break;
					case "EnableProduct":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableProduct = tempBool;
						break;
					case "EnableLocation":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableLocation = tempBool;
						break;
					case "EnableImpressions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableImpressions = tempBool;
						break;
					case "EnableCPM":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableCPM = tempBool;
						break;
					case "EnableRate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableRate = tempBool;
						break;
					case "EnableInvestment":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableInvestment = tempBool;
						break;
					case "EnableInfo":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableInfo = tempBool;
						break;
					case "EnableScreenshot":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableScreenshot = tempBool;
						break;

					case "ShowCategory":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowCategory = tempBool;
						break;
					case "ShowGroup":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowGroup = tempBool;
						break;
					case "ShowProduct":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowProduct = tempBool;
						break;
					case "ShowLocation":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLocation = tempBool;
						break;
					case "ShowImpressions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowImpressions = tempBool;
						break;
					case "ShowCPM":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowCPM = tempBool;
						break;
					case "ShowRate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowRate = tempBool;
						break;
					case "ShowInvestment":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowInvestment = tempBool;
						break;
					case "ShowInfo":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowInfo = tempBool;
						break;
					case "ShowScreenshot":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowScreenshot = tempBool;
						break;
				}
			ShowCategory &= EnableCategory;
			ShowGroup &= EnableGroup;
			ShowProduct &= EnableProduct;
			ShowLocation &= EnableLocation;
			ShowImpressions &= EnableImpressions;
			ShowCPM &= EnableCPM;
			ShowRate &= EnableRate;
			ShowInvestment &= EnableInvestment;
			ShowInfo &= EnableInfo;
			ShowScreenshot &= EnableScreenshot;
		}
	}
}
