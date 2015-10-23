using System;
using System.Text;
using System.Xml;
using Asa.Core.Common;

namespace Asa.Core.OnlineSchedule
{
	public class ScheduleBuilderViewSettings : IScheduleViewSettings
	{
		public ScheduleBuilderViewSettings()
		{
			HomeViewSettings = new HomeViewSettings();
			HomeViewSettings.ResetToDefault();
			DigitalPackageSettings = new DigitalPackageSettings();
			DigitalPackageSettings.ResetToDefault();
			AdPlanViewSettings = new AdPlanViewSettings();
		}

		public HomeViewSettings HomeViewSettings { get; set; }
		public IHomeViewSettings SharedHomeViewSettings
		{
			get { return HomeViewSettings; }
		}
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

	public class HomeViewSettings : IHomeViewSettings
	{
		public HomeViewSettings()
		{
			EnableAccountNumber = true;
			EnableDigitalDimensions = true;
			EnableDigitalStrategy = true;
			EnableDigitalLocation = true;
			EnableDigitalTargeting = true;
			EnableDigitalRichMedia = true;

			ShowAccountNumber = false;
			ShowDigitalDimensions = true;
			ShowDigitalStrategy = true;
			ShowDigitalLocation = true;
			ShowDigitalTargeting = true;
			ShowDigitalRichMedia = true;
		}

		public bool EnableAccountNumber { get; set; }
		public bool EnableDigitalDimensions { get; set; }
		public bool EnableDigitalStrategy { get; set; }
		public bool EnableDigitalLocation { get; set; }
		public bool EnableDigitalTargeting { get; set; }
		public bool EnableDigitalRichMedia { get; set; }

		public bool ShowAccountNumber { get; set; }
		public bool ShowDigitalDimensions { get; set; }
		public bool ShowDigitalStrategy { get; set; }
		public bool ShowDigitalLocation { get; set; }
		public bool ShowDigitalTargeting { get; set; }
		public bool ShowDigitalRichMedia { get; set; }

		public void ResetToDefault()
		{
			var defaultSettings = new XmlDocument();
			defaultSettings.LoadXml(@"<DefaultSettings>" + ListManager.Instance.DefaultHomeViewSettings.Serialize() + @"</DefaultSettings>");
			Deserialize(defaultSettings.SelectSingleNode(@"/DefaultSettings"));
		}

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<EnableAccountNumber>" + EnableAccountNumber + @"</EnableAccountNumber>");
			result.AppendLine(@"<EnableDigitalDimensions>" + EnableDigitalDimensions + @"</EnableDigitalDimensions>");
			result.AppendLine(@"<EnableDigitalStrategy>" + EnableDigitalStrategy + @"</EnableDigitalStrategy>");
			result.AppendLine(@"<EnableDigitalLocation>" + EnableDigitalLocation + @"</EnableDigitalLocation>");
			result.AppendLine(@"<EnableDigitalTargeting>" + EnableDigitalTargeting + @"</EnableDigitalTargeting>");
			result.AppendLine(@"<EnableDigitalRichMedia>" + EnableDigitalRichMedia + @"</EnableDigitalRichMedia>");

			result.AppendLine(@"<ShowAccountNumber>" + ShowAccountNumber + @"</ShowAccountNumber>");
			result.AppendLine(@"<ShowDigitalDimensions>" + ShowDigitalDimensions + @"</ShowDigitalDimensions>");
			result.AppendLine(@"<ShowDigitalStrategy>" + ShowDigitalStrategy + @"</ShowDigitalStrategy>");
			result.AppendLine(@"<ShowDigitalLocation>" + ShowDigitalLocation + @"</ShowDigitalLocation>");
			result.AppendLine(@"<ShowDigitalTargeting>" + ShowDigitalTargeting + @"</ShowDigitalTargeting>");
			result.AppendLine(@"<ShowDigitalRichMedia>" + ShowDigitalRichMedia + @"</ShowDigitalRichMedia>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				bool tempBool;
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
					case "EnableDigitalLocation":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDigitalLocation = tempBool;
						break;
					case "EnableDigitalTargeting":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDigitalTargeting = tempBool;
						break;
					case "EnableDigitalRichMedia":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDigitalRichMedia = tempBool;
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
					case "ShowDigitalLocation":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDigitalLocation = tempBool;
						break;
					case "ShowDigitalTargeting":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDigitalTargeting = tempBool;
						break;
					case "ShowDigitalRichMedia":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDigitalRichMedia = tempBool;
						break;
				}
			}

			ShowAccountNumber &= EnableAccountNumber;
			ShowDigitalDimensions &= EnableDigitalDimensions;
			ShowDigitalStrategy &= EnableDigitalStrategy;
			ShowDigitalLocation &= EnableDigitalLocation;
			ShowDigitalTargeting &= EnableDigitalTargeting;
			ShowDigitalRichMedia &= EnableDigitalRichMedia;
		}
	}

	public class DigitalProductSettings
	{
		public bool EnableCategory { get; set; }
		public bool EnableFlightDates { get; set; }
		public bool EnableDuration { get; set; }

		public bool ShowFlightDates { get; set; }
		public bool ShowDuration { get; set; }

		public int DefaultPricing { get; set; }

		public DigitalProductSettings()
		{
			EnableCategory = true;
			EnableFlightDates = true;
			EnableDuration = true;

			ShowFlightDates = true;
			ShowDuration = true;

			DefaultPricing = 0;
		}

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<EnableCategory>" + EnableCategory + @"</EnableCategory>");
			result.AppendLine(@"<EnableFlightDates>" + EnableFlightDates + @"</EnableFlightDates>");
			result.AppendLine(@"<EnableDuration>" + EnableDuration + @"</EnableDuration>");

			result.AppendLine(@"<ShowFlightDates>" + ShowFlightDates + @"</ShowFlightDates>");
			result.AppendLine(@"<ShowDuration>" + ShowDuration + @"</ShowDuration>");

			result.AppendLine(@"<DefaultPricing>" + DefaultPricing + @"</DefaultPricing>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "EnableCategory":
						{
							bool temp;
							if (bool.TryParse(childNode.InnerText, out temp))
								EnableCategory = temp;
						}
						break;
					case "EnableFlightDates":
						{
							bool temp;
							if (bool.TryParse(childNode.InnerText, out temp))
								EnableFlightDates = temp;
						}
						break;
					case "EnableDuration":
						{
							bool temp;
							if (bool.TryParse(childNode.InnerText, out temp))
								EnableDuration = temp;
						}
						break;

					case "ShowFlightDates":
						{
							bool temp;
							if (bool.TryParse(childNode.InnerText, out temp))
								ShowFlightDates = temp;
						}
						break;
					case "ShowDuration":
						{
							bool temp;
							if (bool.TryParse(childNode.InnerText, out temp))
								ShowDuration = temp;
						}
						break;

					case "DefaultPricing":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								DefaultPricing = temp;
						}
						break;
				}
			}

			ShowFlightDates &= EnableFlightDates;
			ShowDuration &= EnableDuration;
		}
	}

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

	public class DigitalProductSummary
	{
		public string Statement { get; set; }
		public decimal? MonthlyInvestment { get; set; }
		public decimal? TotalInvestment { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();

			if (!String.IsNullOrEmpty(Statement))
				result.AppendLine(@"<Statement>" + Statement.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Statement>");
			if (MonthlyInvestment.HasValue)
				result.AppendLine(@"<MonthlyInvestment>" + MonthlyInvestment.Value + @"</MonthlyInvestment>");
			if (TotalInvestment.HasValue)
				result.AppendLine(@"<TotalInvestment>" + TotalInvestment.Value + @"</TotalInvestment>");

			return result.ToString();
		}

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
