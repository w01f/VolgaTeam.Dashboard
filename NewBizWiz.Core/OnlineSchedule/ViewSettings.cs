using System;
using System.Text;
using System.Xml;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Core.OnlineSchedule
{
	public class ScheduleBuilderViewSettings : IScheduleViewSettings
	{
		public ScheduleBuilderViewSettings()
		{
			HomeViewSettings = new HomeViewSettings();
			DigitalPackageSettings = new DigitalPackageSettings();
			AdPlanViewSettings = new AdPlanViewSettings();
		}

		public HomeViewSettings HomeViewSettings { get; set; }
		public DigitalPackageSettings DigitalPackageSettings { get; private set; }
		public AdPlanViewSettings AdPlanViewSettings { get; private set; }

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<HomeViewSettings>" + HomeViewSettings.Serialize() + @"</HomeViewSettings>");
			result.AppendLine(@"<DigitalPackageSettings>" + DigitalPackageSettings.Serialize() + @"</DigitalPackageSettings>");
			result.AppendLine(@"<AdPlanViewSettings>" + AdPlanViewSettings.Serialize() + @"</AdPlanViewSettings>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "HomeViewSettings":
						HomeViewSettings.Deserialize(childNode);
						break;
					case "DigitalPackageSettings":
						DigitalPackageSettings.Deserialize(childNode);
						break;
					case "AdPlanViewSettings":
						AdPlanViewSettings.Deserialize(childNode);
						break;
				}
			}
		}
	}

	public class HomeViewSettings
	{
		public HomeViewSettings()
		{
			EnableAccountNumber = true;
			EnableDigitalDimensions = true;
			EnableDigitalStrategy = true;

			ShowAccountNumber = false;
			ShowDigitalDimensions = true;
			ShowDigitalStrategy = true;
		}

		public bool EnableAccountNumber { get; set; }
		public bool EnableDigitalDimensions { get; set; }
		public bool EnableDigitalStrategy { get; set; }

		public bool ShowAccountNumber { get; set; }
		public bool ShowDigitalDimensions { get; set; }
		public bool ShowDigitalStrategy { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<EnableAccountNumber>" + EnableAccountNumber + @"</EnableAccountNumber>");
			result.AppendLine(@"<EnableDigitalDimensions>" + EnableDigitalDimensions + @"</EnableDigitalDimensions>");
			result.AppendLine(@"<EnableDigitalStrategy>" + EnableDigitalStrategy + @"</EnableDigitalStrategy>");

			result.AppendLine(@"<ShowAccountNumber>" + ShowAccountNumber + @"</ShowAccountNumber>");
			result.AppendLine(@"<ShowDigitalDimensions>" + ShowDigitalDimensions + @"</ShowDigitalDimensions>");
			result.AppendLine(@"<ShowDigitalStrategy>" + ShowDigitalStrategy + @"</ShowDigitalStrategy>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool = false;

			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "EnableAccountNumber":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableAccountNumber = tempBool;
						break;
					case "EnableDigitalDimensions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDigitalDimensions = tempBool;
						break;
					case "EnableDigitalStrategy":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDigitalStrategy = tempBool;
						break;

					case "ShowAccountNumber":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAccountNumber = tempBool;
						break;
					case "ShowDigitalDimensions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDigitalDimensions = tempBool;
						break;
					case "ShowDigitalStrategy":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDigitalStrategy = tempBool;
						break;
				}
			}

			ShowAccountNumber &= EnableAccountNumber;
			ShowDigitalDimensions &= EnableDigitalDimensions;
			ShowDigitalStrategy &= EnableDigitalStrategy;
		}
	}

	public class DigitalPackageSettings
	{
		public bool ShowOptions { get; set; }
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
		public FormulaType Formula { get; set; }

		public DigitalPackageSettings()
		{
			ShowOptions = true;
			ResetToDefault();
		}

		public void ResetToDefault()
		{
			ShowCategory = false;
			ShowGroup = true;
			ShowProduct = true;
			ShowInfo = true;
			ShowComments = false;
			ShowImpressions = true;
			ShowCPM = true;
			ShowInvestment = true;
			ShowRate = false;
			ShowScreenshot = false;
			Formula = FormulaType.CPM;
		}

		public string Serialize()
		{
			var xml = new StringBuilder();
			xml.AppendLine(@"<ShowOptions>" + ShowOptions + @"</ShowOptions>");
			xml.AppendLine(@"<ShowCategory>" + ShowCategory + @"</ShowCategory>");
			xml.AppendLine(@"<ShowGroup>" + ShowGroup + @"</ShowGroup>");
			xml.AppendLine(@"<ShowProduct>" + ShowProduct + @"</ShowProduct>");
			xml.AppendLine(@"<ShowImpressions>" + ShowImpressions + @"</ShowImpressions>");
			xml.AppendLine(@"<ShowCPM>" + ShowCPM + @"</ShowCPM>");
			xml.AppendLine(@"<ShowInvestment>" + ShowInvestment + @"</ShowInvestment>");
			xml.AppendLine(@"<ShowRate>" + ShowRate + @"</ShowRate>");
			xml.AppendLine(@"<ShowInfo>" + ShowInfo + @"</ShowInfo>");
			xml.AppendLine(@"<ShowComments>" + ShowComments + @"</ShowComments>");
			xml.AppendLine(@"<ShowScreenshot>" + ShowScreenshot + @"</ShowScreenshot>");
			xml.AppendLine(@"<Formula>" + (int)Formula + @"</Formula>");
			return xml.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool;
			int tempInt;
			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "ShowOptions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowOptions = tempBool;
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
					case "Formula":
						if (Int32.TryParse(childNode.InnerText, out tempInt))
							Formula = (FormulaType)tempInt;
						break;
				}
		}
	}

	public class AdPlanViewSettings
	{
		public AdPlanViewSettings()
		{
			MoreSlides = true;
		}

		public bool MoreSlides { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<MoreSlides>" + MoreSlides + @"</MoreSlides>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool = false;

			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "MoreSlides":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							MoreSlides = tempBool;
						break;
				}
			}
		}
	}
}
