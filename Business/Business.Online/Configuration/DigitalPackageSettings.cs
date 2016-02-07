using System;
using System.Text;
using System.Xml;
using Asa.Business.Online.Dictionaries;
using Asa.Business.Online.Enums;

namespace Asa.Business.Online.Configuration
{
	public class DigitalPackageSettings
	{
		public bool ShowOptions { get; set; }
		public FormulaType Formula { get; set; }

		public bool EnableCategory { get; set; }
		public bool EnableGroup { get; set; }
		public bool EnableProduct { get; set; }
		public bool EnableImpressions { get; set; }
		public bool EnableCPM { get; set; }
		public bool EnableRate { get; set; }
		public bool EnableInvestment { get; set; }
		public bool EnableInfo { get; set; }
		public bool EnableComments { get; set; }
		public bool EnableScreenshot { get; set; }

		public bool ShowCategory { get; set; }
		public bool ShowGroup { get; set; }
		public bool ShowProduct { get; set; }
		public bool ShowImpressions { get; set; }
		public bool ShowCPM { get; set; }
		public bool ShowRate { get; set; }
		public bool ShowInvestment { get; set; }
		public bool ShowInfo { get; set; }
		public bool ShowComments { get; set; }
		public bool ShowScreenshot { get; set; }

		public DigitalPackageSettings()
		{
			ShowOptions = true;
			Formula = FormulaType.CPM;

			EnableCategory = true;
			EnableGroup = true;
			EnableProduct = true;
			EnableImpressions = true;
			EnableCPM = true;
			EnableRate = true;
			EnableInvestment = true;
			EnableInfo = true;
			EnableComments = true;
			EnableScreenshot = true;

			ShowCategory = true;
			ShowGroup = true;
			ShowProduct = true;
			ShowImpressions = true;
			ShowCPM = true;
			ShowRate = true;
			ShowInvestment = true;
			ShowInfo = true;
			ShowComments = true;
			ShowScreenshot = true;
		}

		public void ResetToDefault()
		{
			var defaultSettings = new XmlDocument();
			defaultSettings.LoadXml(@"<DefaultSettings>" + ListManager.Instance.DefaultDigitalPackageSettings.Serialize() + @"</DefaultSettings>");
			Deserialize(defaultSettings.SelectSingleNode(@"/DefaultSettings"));
		}

		public string Serialize()
		{
			var xml = new StringBuilder();
			xml.AppendLine(@"<ShowOptions>" + ShowOptions + @"</ShowOptions>");
			xml.AppendLine(@"<Formula>" + (int)Formula + @"</Formula>");

			xml.AppendLine(@"<EnableCategory>" + EnableCategory + @"</EnableCategory>");
			xml.AppendLine(@"<EnableGroup>" + EnableGroup + @"</EnableGroup>");
			xml.AppendLine(@"<EnableProduct>" + EnableProduct + @"</EnableProduct>");
			xml.AppendLine(@"<EnableImpressions>" + EnableImpressions + @"</EnableImpressions>");
			xml.AppendLine(@"<EnableCPM>" + EnableCPM + @"</EnableCPM>");
			xml.AppendLine(@"<EnableRate>" + EnableRate + @"</EnableRate>");
			xml.AppendLine(@"<EnableInvestment>" + EnableInvestment + @"</EnableInvestment>");
			xml.AppendLine(@"<EnableInfo>" + EnableInfo + @"</EnableInfo>");
			xml.AppendLine(@"<EnableComments>" + EnableComments + @"</EnableComments>");
			xml.AppendLine(@"<EnableScreenshot>" + EnableScreenshot + @"</EnableScreenshot>");

			xml.AppendLine(@"<ShowCategory>" + ShowCategory + @"</ShowCategory>");
			xml.AppendLine(@"<ShowGroup>" + ShowGroup + @"</ShowGroup>");
			xml.AppendLine(@"<ShowProduct>" + ShowProduct + @"</ShowProduct>");
			xml.AppendLine(@"<ShowImpressions>" + ShowImpressions + @"</ShowImpressions>");
			xml.AppendLine(@"<ShowCPM>" + ShowCPM + @"</ShowCPM>");
			xml.AppendLine(@"<ShowRate>" + ShowRate + @"</ShowRate>");
			xml.AppendLine(@"<ShowInvestment>" + ShowInvestment + @"</ShowInvestment>");
			xml.AppendLine(@"<ShowInfo>" + ShowInfo + @"</ShowInfo>");
			xml.AppendLine(@"<ShowComments>" + ShowComments + @"</ShowComments>");
			xml.AppendLine(@"<ShowScreenshot>" + ShowScreenshot + @"</ShowScreenshot>");

			return xml.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool;
			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "ShowOptions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowOptions = tempBool;
						break;
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
					case "EnableComments":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableComments = tempBool;
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
					case "ShowComments":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowComments = tempBool;
						break;
					case "ShowScreenshot":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowScreenshot = tempBool;
						break;
				}
			ShowCategory &= EnableCategory;
			ShowGroup &= EnableGroup;
			ShowProduct &= EnableProduct;
			ShowImpressions &= EnableImpressions;
			ShowCPM &= EnableCPM;
			ShowRate &= EnableRate;
			ShowInvestment &= EnableInvestment;
			ShowInfo &= EnableInfo;
			ShowComments &= EnableComments;
			ShowScreenshot &= EnableScreenshot;
		}
	}
}
