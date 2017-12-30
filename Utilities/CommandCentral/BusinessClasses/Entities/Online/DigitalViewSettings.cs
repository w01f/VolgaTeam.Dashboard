using System.Text;

namespace CommandCentral.BusinessClasses.Entities.Online
{
	public class HomeViewSettings
	{
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

		public HomeViewSettings()
		{
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

		public string Serialize()
		{
			var result = new StringBuilder();

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
	}

	public class DigitalProductSettings
	{
		public bool EnableCategory { get; set; }
		public bool EnableFlightDates { get; set; }
		public bool EnableDuration { get; set; }

		public bool ShowCategory { get; set; }
		public bool ShowFlightDates { get; set; }
		public bool ShowDuration { get; set; }

		public int DefaultPricing { get; set; }

		public DigitalProductSettings()
		{
			EnableCategory = true;
			EnableFlightDates = true;
			EnableDuration = true;

			ShowCategory = true;
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

			result.AppendLine(@"<ShowCategory>" + ShowCategory + @"</ShowCategory>");
			result.AppendLine(@"<ShowFlightDates>" + ShowFlightDates + @"</ShowFlightDates>");
			result.AppendLine(@"<ShowDuration>" + ShowDuration + @"</ShowDuration>");

			result.AppendLine(@"<DefaultPricing>" + DefaultPricing + @"</DefaultPricing>");

			return result.ToString();
		}
	}

	public class DigitalPackageSettings
	{
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

		public DigitalPackageSettings()
		{
			ResetToDefault();
		}

		public void ResetToDefault()
		{
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

		public string Serialize()
		{
			var xml = new StringBuilder();
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
	}
}