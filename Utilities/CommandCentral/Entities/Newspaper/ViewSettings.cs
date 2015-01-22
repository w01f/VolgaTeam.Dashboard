using System.Text;
using System.Xml;

namespace CommandCentral.Entities.Newspaper
{
	public class HomeViewSettings
	{
		public HomeViewSettings()
		{
			EnableAccountNumber = true;
			EnableDelivery = true;
			EnableReadership = true;
			EnableLogo = true;
			EnableCode = true;

			ShowAccountNumber = false;
			ShowDelivery = false;
			ShowReadership = false;
			ShowLogo = true;
			ShowCode = true;
		}

		public bool EnableAccountNumber { get; set; }
		public bool EnableDelivery { get; set; }
		public bool EnableReadership { get; set; }
		public bool EnableLogo { get; set; }
		public bool EnableCode { get; set; }

		public bool ShowAccountNumber { get; set; }
		public bool ShowDelivery { get; set; }
		public bool ShowReadership { get; set; }
		public bool ShowLogo { get; set; }
		public bool ShowCode { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<EnableAccountNumber>" + EnableAccountNumber + @"</EnableAccountNumber>");
			result.AppendLine(@"<EnableCode>" + EnableCode + @"</EnableCode>");
			result.AppendLine(@"<EnableDelivery>" + EnableDelivery + @"</EnableDelivery>");
			result.AppendLine(@"<EnableLogo>" + EnableLogo + @"</EnableLogo>");
			result.AppendLine(@"<EnableReadership>" + EnableReadership + @"</EnableReadership>");

			result.AppendLine(@"<ShowAccountNumber>" + ShowAccountNumber + @"</ShowAccountNumber>");
			result.AppendLine(@"<ShowCode>" + ShowCode + @"</ShowCode>");
			result.AppendLine(@"<ShowDelivery>" + ShowDelivery + @"</ShowDelivery>");
			result.AppendLine(@"<ShowLogo>" + ShowLogo + @"</ShowLogo>");
			result.AppendLine(@"<ShowReadership>" + ShowReadership + @"</ShowReadership>");

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
					case "EnableCode":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableCode = tempBool;
						break;
					case "EnableDelivery":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDelivery = tempBool;
						break;
					case "EnableLogo":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableLogo = tempBool;
						break;
					case "EnableReadership":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableReadership = tempBool;
						break;

					case "ShowAccountNumber":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAccountNumber = tempBool;
						break;
					case "ShowCode":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowCode = tempBool;
						break;
					case "ShowDelivery":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDelivery = tempBool;
						break;
					case "ShowLogo":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLogo = tempBool;
						break;
					case "ShowReadership":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowReadership = tempBool;
						break;
				}
			}

			ShowAccountNumber &= EnableAccountNumber;
			ShowDelivery &= EnableDelivery;
			ShowReadership &= EnableReadership;
			ShowLogo &= EnableLogo;
			ShowCode &= EnableCode;
		}
	}

	public class PrintScheduleViewSettings
	{
		public PrintScheduleViewSettings()
		{
			EnablePCI = true;
			EnableFlat = true;
			EnableShare = true;
			EnableBlackWhite = true;
			EnableSpotColor = true;
			EnableFullColor = true;
			EnableCostPerAd = true;
			EnablePercentOfAd = true;
			EnableColorIncluded = true;
			EnableCostPerInch = true;
			EnableMechanicals = true;

			DefaultPCI = false;
			DefaultFlat = false;
			DefaultShare = true;
			DefaultBlackWhite = false;
			DefaultSpotColor = false;
			DefaultFullColor = true;
			DefaultCostPerAd = false;
			DefaultPercentOfAd = false;
			DefaultColorIncluded = true;
			DefaultCostPerInch = false;
			DefaultMechanicals = true;
			CalcDiscountBeforeColor = true;
		}

		public bool EnablePCI { get; set; }
		public bool EnableFlat { get; set; }
		public bool EnableShare { get; set; }
		public bool EnableBlackWhite { get; set; }
		public bool EnableSpotColor { get; set; }
		public bool EnableFullColor { get; set; }
		public bool EnableCostPerAd { get; set; }
		public bool EnablePercentOfAd { get; set; }
		public bool EnableColorIncluded { get; set; }
		public bool EnableCostPerInch { get; set; }
		public bool EnableMechanicals { get; set; }

		public bool DefaultPCI { get; set; }
		public bool DefaultFlat { get; set; }
		public bool DefaultShare { get; set; }
		public bool DefaultBlackWhite { get; set; }
		public bool DefaultSpotColor { get; set; }
		public bool DefaultFullColor { get; set; }
		public bool DefaultCostPerAd { get; set; }
		public bool DefaultPercentOfAd { get; set; }
		public bool DefaultColorIncluded { get; set; }
		public bool DefaultCostPerInch { get; set; }
		public bool DefaultMechanicals { get; set; }
		public bool CalcDiscountBeforeColor { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<EnablePCI>" + EnablePCI + @"</EnablePCI>");
			result.AppendLine(@"<EnableFlat>" + EnableFlat + @"</EnableFlat>");
			result.AppendLine(@"<EnableShare>" + EnableShare + @"</EnableShare>");
			result.AppendLine(@"<EnableBlackWhite>" + EnableBlackWhite + @"</EnableBlackWhite>");
			result.AppendLine(@"<EnableSpotColor>" + EnableSpotColor + @"</EnableSpotColor>");
			result.AppendLine(@"<EnableFullColor>" + EnableFullColor + @"</EnableFullColor>");
			result.AppendLine(@"<EnableCostPerAd>" + EnableCostPerAd + @"</EnableCostPerAd>");
			result.AppendLine(@"<EnablePercentOfAd>" + EnablePercentOfAd + @"</EnablePercentOfAd>");
			result.AppendLine(@"<EnableColorIncluded>" + EnableColorIncluded + @"</EnableColorIncluded>");
			result.AppendLine(@"<EnableCostPerInch>" + EnableCostPerInch + @"</EnableCostPerInch>");
			result.AppendLine(@"<EnableMechanicals>" + EnableMechanicals + @"</EnableMechanicals>");

			result.AppendLine(@"<DefaultPCI>" + DefaultPCI + @"</DefaultPCI>");
			result.AppendLine(@"<DefaultFlat>" + DefaultFlat + @"</DefaultFlat>");
			result.AppendLine(@"<DefaultShare>" + DefaultShare + @"</DefaultShare>");
			result.AppendLine(@"<DefaultBlackWhite>" + DefaultBlackWhite + @"</DefaultBlackWhite>");
			result.AppendLine(@"<DefaultSpotColor>" + DefaultSpotColor + @"</DefaultSpotColor>");
			result.AppendLine(@"<DefaultFullColor>" + DefaultFullColor + @"</DefaultFullColor>");
			result.AppendLine(@"<DefaultCostPerAd>" + DefaultCostPerAd + @"</DefaultCostPerAd>");
			result.AppendLine(@"<DefaultPercentOfAd>" + DefaultPercentOfAd + @"</DefaultPercentOfAd>");
			result.AppendLine(@"<DefaultColorIncluded>" + DefaultColorIncluded + @"</DefaultColorIncluded>");
			result.AppendLine(@"<DefaultCostPerInch>" + DefaultCostPerInch + @"</DefaultCostPerInch>");
			result.AppendLine(@"<DefaultMechanicals>" + DefaultMechanicals + @"</DefaultMechanicals>");
			result.AppendLine(@"<CalcDiscountBeforeColor>" + CalcDiscountBeforeColor + @"</CalcDiscountBeforeColor>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				bool tempBool;
				switch (childNode.Name)
				{
					case "EnablePCI":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnablePCI = tempBool;
						break;
					case "EnableFlat":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableFlat = tempBool;
						break;
					case "EnableShare":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableShare = tempBool;
						break;
					case "EnableBlackWhite":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableBlackWhite = tempBool;
						break;
					case "EnableSpotColor":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableSpotColor = tempBool;
						break;
					case "EnableFullColor":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableFullColor = tempBool;
						break;
					case "EnableCostPerAd":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableCostPerAd = tempBool;
						break;
					case "EnablePercentOfAd":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnablePercentOfAd = tempBool;
						break;
					case "EnableColorIncluded":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableColorIncluded = tempBool;
						break;
					case "EnableCostPerInch":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableCostPerInch = tempBool;
						break;
					case "EnableMechanicals":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableMechanicals = tempBool;
						break;

					case "DefaultPCI":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							DefaultPCI = tempBool;
						break;
					case "DefaultFlat":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							DefaultFlat = tempBool;
						break;
					case "DefaultShare":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							DefaultShare = tempBool;
						break;
					case "DefaultBlackWhite":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							DefaultBlackWhite = tempBool;
						break;
					case "DefaultSpotColor":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							DefaultSpotColor = tempBool;
						break;
					case "DefaultFullColor":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							DefaultFullColor = tempBool;
						break;
					case "DefaultCostPerAd":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							DefaultCostPerAd = tempBool;
						break;
					case "DefaultPercentOfAd":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							DefaultPercentOfAd = tempBool;
						break;
					case "DefaultColorIncluded":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							DefaultColorIncluded = tempBool;
						break;
					case "DefaultCostPerInch":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							DefaultCostPerInch = tempBool;
						break;
					case "DefaultMechanicals":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							DefaultMechanicals = tempBool;
						break;
					case "CalcDiscountBeforeColor":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							CalcDiscountBeforeColor = tempBool;
						break;
				}
			}

			DefaultPCI &= EnablePCI;
			DefaultFlat &= EnableFlat;
			DefaultShare &= EnableShare;
			DefaultBlackWhite &= EnableBlackWhite;
			DefaultSpotColor &= EnableSpotColor;
			DefaultFullColor &= EnableFullColor;
			DefaultCostPerAd &= EnableCostPerAd;
			DefaultPercentOfAd &= EnablePercentOfAd;
			DefaultColorIncluded &= EnableColorIncluded;
			DefaultCostPerInch &= EnableCostPerInch;
			DefaultMechanicals &= EnableMechanicals;
		}
	}

	public class PublicationBasicOverviewSettings
	{
		public PublicationBasicOverviewSettings()
		{
			ShowName = true;
			ShowLogo = true;
			ShowFlightDates = true;
			ShowSlideHeader = true;
			ShowPresentationDate = true;
			ShowAdvertiser = true;
			ShowDecisionMaker = true;

			EnableDimensions = true;
			EnablePageSize = true;
			EnablePercentOfPage = true;
			EnableSquare = true;
			EnableColor = true;
			ShowAdSizeDetails = true;
			ShowDimensions = false;
			ShowPageSize = true;
			ShowPercentOfPage = true;
			ShowSquare = true;
			ShowColor = true;
			ShowMechanicals = true;

			EnableTotalInserts = true;
			EnableTotalSquare = true;
			ShowTotalDetails = true;
			ShowTotalInserts = true;
			ShowTotalSquare = true;

			EnableAvgAdCost = true;
			EnableAvgPCI = true;
			EnableDiscounts = true;
			EnableInvestment = true;
			ShowInvestmentDetails = true;
			ShowAvgAdCost = true;
			ShowAvgPCI = true;
			ShowDiscounts = false;
			ShowInvestment = true;

			EnableFlightDates2 = true;
			EnableDates = true;
			EnableComments = true;
			ShowDateDetails = true;
			ShowFlightDates2 = true;
			ShowDates = true;
			ShowComments = false;

			Comments = string.Empty;
			SlideHeader = string.Empty;
		}

		public bool ShowName { get; set; }
		public bool ShowLogo { get; set; }
		public bool ShowFlightDates { get; set; }
		public bool ShowSlideHeader { get; set; }
		public bool ShowPresentationDate { get; set; }
		public bool ShowAdvertiser { get; set; }
		public bool ShowDecisionMaker { get; set; }

		public bool EnableDimensions { get; set; }
		public bool EnablePageSize { get; set; }
		public bool EnablePercentOfPage { get; set; }
		public bool EnableSquare { get; set; }
		public bool EnableColor { get; set; }
		public bool ShowAdSizeDetails { get; set; }
		public bool ShowDimensions { get; set; }
		public bool ShowPageSize { get; set; }
		public bool ShowPercentOfPage { get; set; }
		public bool ShowSquare { get; set; }
		public bool ShowColor { get; set; }
		public bool ShowMechanicals { get; set; }

		public bool EnableTotalInserts { get; set; }
		public bool EnableTotalSquare { get; set; }
		public bool ShowTotalDetails { get; set; }
		public bool ShowTotalInserts { get; set; }
		public bool ShowTotalSquare { get; set; }

		public bool EnableAvgAdCost { get; set; }
		public bool EnableAvgPCI { get; set; }
		public bool EnableDiscounts { get; set; }
		public bool EnableInvestment { get; set; }
		public bool ShowInvestmentDetails { get; set; }
		public bool ShowAvgAdCost { get; set; }
		public bool ShowAvgPCI { get; set; }
		public bool ShowDiscounts { get; set; }
		public bool ShowInvestment { get; set; }

		public bool EnableFlightDates2 { get; set; }
		public bool EnableDates { get; set; }
		public bool EnableComments { get; set; }
		public bool ShowDateDetails { get; set; }
		public bool ShowFlightDates2 { get; set; }
		public bool ShowDates { get; set; }
		public bool ShowComments { get; set; }

		public string Comments { get; set; }
		public string SlideHeader { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<ShowName>" + ShowName + @"</ShowName>");
			result.AppendLine(@"<ShowLogo>" + ShowLogo + @"</ShowLogo>");
			result.AppendLine(@"<ShowFlightDates>" + ShowFlightDates + @"</ShowFlightDates>");
			result.AppendLine(@"<ShowAdvertiser>" + ShowAdvertiser + @"</ShowAdvertiser>");
			result.AppendLine(@"<ShowDecisionMaker>" + ShowDecisionMaker + @"</ShowDecisionMaker>");
			result.AppendLine(@"<ShowPresentationDate>" + ShowPresentationDate + @"</ShowPresentationDate>");
			result.AppendLine(@"<ShowSlideHeader>" + ShowSlideHeader + @"</ShowSlideHeader>");
			result.AppendLine(@"<SlideHeader>" + SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");

			result.AppendLine(@"<EnableDimensions>" + EnableDimensions + @"</EnableDimensions>");
			result.AppendLine(@"<EnablePageSize>" + EnablePageSize + @"</EnablePageSize>");
			result.AppendLine(@"<EnablePercentOfPage>" + EnablePercentOfPage + @"</EnablePercentOfPage>");
			result.AppendLine(@"<EnableSquare>" + EnableSquare + @"</EnableSquare>");
			result.AppendLine(@"<EnableColor>" + EnableColor + @"</EnableColor>");
			result.AppendLine(@"<ShowAdSizeDetails>" + ShowAdSizeDetails + @"</ShowAdSizeDetails>");
			result.AppendLine(@"<ShowDimensions>" + ShowDimensions + @"</ShowDimensions>");
			result.AppendLine(@"<ShowPageSize>" + ShowPageSize + @"</ShowPageSize>");
			result.AppendLine(@"<ShowPercentOfPage>" + ShowPercentOfPage + @"</ShowPercentOfPage>");
			result.AppendLine(@"<ShowSquare>" + ShowSquare + @"</ShowSquare>");
			result.AppendLine(@"<ShowColor>" + ShowColor + @"</ShowColor>");
			result.AppendLine(@"<ShowMechanicals>" + ShowMechanicals + @"</ShowMechanicals>");

			result.AppendLine(@"<EnableTotalInserts>" + EnableTotalInserts + @"</EnableTotalInserts>");
			result.AppendLine(@"<EnableTotalSquare>" + EnableTotalSquare + @"</EnableTotalSquare>");
			result.AppendLine(@"<ShowTotalDetails>" + ShowTotalDetails + @"</ShowTotalDetails>");
			result.AppendLine(@"<ShowTotalInserts>" + ShowTotalInserts + @"</ShowTotalInserts>");
			result.AppendLine(@"<ShowTotalSquare>" + ShowTotalSquare + @"</ShowTotalSquare>");

			result.AppendLine(@"<EnableAvgAdCost>" + EnableAvgAdCost + @"</EnableAvgAdCost>");
			result.AppendLine(@"<EnableAvgPCI>" + EnableAvgPCI + @"</EnableAvgPCI>");
			result.AppendLine(@"<EnableDiscounts>" + EnableDiscounts + @"</EnableDiscounts>");
			result.AppendLine(@"<EnableInvestment>" + EnableInvestment + @"</EnableInvestment>");
			result.AppendLine(@"<ShowInvestmentDetails>" + ShowInvestmentDetails + @"</ShowInvestmentDetails>");
			result.AppendLine(@"<ShowAvgAdCost>" + ShowAvgAdCost + @"</ShowAvgAdCost>");
			result.AppendLine(@"<ShowAvgPCI>" + ShowAvgPCI + @"</ShowAvgPCI>");
			result.AppendLine(@"<ShowDiscounts>" + ShowDiscounts + @"</ShowDiscounts>");
			result.AppendLine(@"<ShowInvestment>" + ShowInvestment + @"</ShowInvestment>");

			result.AppendLine(@"<EnableFlightDates2>" + EnableFlightDates2 + @"</EnableFlightDates2>");
			result.AppendLine(@"<EnableDates>" + EnableDates + @"</EnableDates>");
			result.AppendLine(@"<EnableComments>" + EnableComments + @"</EnableComments>");
			result.AppendLine(@"<ShowDateDetails>" + ShowDateDetails + @"</ShowDateDetails>");
			result.AppendLine(@"<ShowFlightDates2>" + ShowFlightDates2 + @"</ShowFlightDates2>");
			result.AppendLine(@"<ShowDates>" + ShowDates + @"</ShowDates>");
			result.AppendLine(@"<ShowComments>" + ShowComments + @"</ShowComments>");
			result.AppendLine(@"<Comments>" + Comments.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Comments>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool = false;
			;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "ShowName":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowName = tempBool;
						break;
					case "ShowLogo":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLogo = tempBool;
						break;
					case "ShowFlightDates":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowFlightDates = tempBool;
						break;
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
					case "ShowSlideHeader":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSlideHeader = tempBool;
						break;
					case "SlideHeader":
						SlideHeader = childNode.InnerText;
						break;

					case "EnablePageSize":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnablePageSize = tempBool;
						break;
					case "EnableDimensions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDimensions = tempBool;
						break;
					case "EnableSquare":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableSquare = tempBool;
						break;
					case "EnableColor":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableColor = tempBool;
						break;
					case "EnablePercentOfPage":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnablePercentOfPage = tempBool;
						break;
					case "ShowAdSizeDetails":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAdSizeDetails = tempBool;
						break;
					case "ShowPageSize":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPageSize = tempBool;
						break;
					case "ShowDimensions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDimensions = tempBool;
						break;
					case "ShowSquare":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSquare = tempBool;
						break;
					case "ShowColor":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowColor = tempBool;
						break;
					case "ShowPercentOfPage":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPercentOfPage = tempBool;
						break;
					case "ShowMechanicals":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowMechanicals = tempBool;
						break;

					case "EnableTotalInserts":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableTotalInserts = tempBool;
						break;
					case "EnableTotalSquare":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableTotalSquare = tempBool;
						break;
					case "ShowTotalDetails":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalDetails = tempBool;
						break;
					case "ShowTotalInserts":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalInserts = tempBool;
						break;
					case "ShowTotalSquare":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalSquare = tempBool;
						break;

					case "EnableAvgAdCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableAvgAdCost = tempBool;
						break;
					case "EnableAvgPCI":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableAvgPCI = tempBool;
						break;
					case "EnableDiscounts":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDiscounts = tempBool;
						break;
					case "EnableInvestment":
						tempBool = false;
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableInvestment = tempBool;
						break;
					case "ShowInvestmentDetails":
						tempBool = false;
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowInvestmentDetails = tempBool;
						break;
					case "ShowAvgAdCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAvgAdCost = tempBool;
						break;
					case "ShowAvgPCI":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAvgPCI = tempBool;
						break;
					case "ShowDiscounts":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDiscounts = tempBool;
						break;
					case "ShowInvestment":
						tempBool = false;
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowInvestment = tempBool;
						break;

					case "EnableFlightDates2":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableFlightDates2 = tempBool;
						break;
					case "EnableDates":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDates = tempBool;
						break;
					case "EnableComments":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableComments = tempBool;
						break;
					case "ShowDateDetails":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDateDetails = tempBool;
						break;
					case "ShowFlightDates2":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowFlightDates2 = tempBool;
						break;
					case "ShowDates":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDates = tempBool;
						break;
					case "ShowComments":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowComments = tempBool;
						break;
					case "Comments":
						Comments = childNode.InnerText;
						break;
				}
			}

			ShowDimensions &= EnableDimensions;
			ShowPageSize &= EnablePageSize;
			ShowPercentOfPage &= EnablePercentOfPage;
			ShowSquare &= EnableSquare;
			ShowColor &= EnableColor;

			ShowTotalInserts &= EnableTotalInserts;
			ShowTotalSquare &= EnableTotalSquare;

			ShowAvgAdCost &= EnableAvgAdCost;
			ShowAvgPCI &= EnableAvgPCI;
			ShowDiscounts &= EnableDiscounts;
			ShowInvestment &= EnableInvestment;

			ShowFlightDates2 &= EnableFlightDates2;
			ShowDates &= EnableDates;
			ShowComments &= EnableComments;
		}
	}

	public class PublicationMultiSummarySettings
	{
		public PublicationMultiSummarySettings()
		{
			ShowName = true;
			ShowLogo = true;
			ShowInvestment = true;

			EnableFlightDates = true;
			EnableDates = true;
			EnableComments = true;
			ShowFlightDates = true;
			ShowDates = true;
			ShowComments = false;

			EnableTotalInserts = true;
			EnableDimensions = true;
			EnablePageSize = true;
			EnablePercentOfPage = true;
			EnableTotalColor = true;
			EnableAvgAdCost = true;
			EnableAvgFinalCost = true;
			EnableDiscounts = true;
			EnableSection = true;
			ShowTotalInserts = true;
			ShowDimensions = false;
			ShowPageSize = true;
			ShowPercentOfPage = false;
			ShowTotalColor = false;
			ShowAvgAdCost = false;
			ShowAvgFinalCost = false;
			ShowDiscounts = false;
			ShowSection = false;

			ShowAvgPCI = false;
			ShowTotalSquare = false;
			ShowSquare = false;
			ShowMechanicals = false;

			InvestmentType = "Total";
			Comments = string.Empty;
		}

		public bool ShowName { get; set; }
		public bool ShowLogo { get; set; }
		public bool ShowInvestment { get; set; }

		public bool EnableFlightDates { get; set; }
		public bool EnableDates { get; set; }
		public bool EnableComments { get; set; }
		public bool ShowFlightDates { get; set; }
		public bool ShowDates { get; set; }
		public bool ShowComments { get; set; }

		public bool EnableTotalInserts { get; set; }
		public bool EnableDimensions { get; set; }
		public bool EnablePageSize { get; set; }
		public bool EnablePercentOfPage { get; set; }
		public bool EnableTotalColor { get; set; }
		public bool EnableAvgAdCost { get; set; }
		public bool EnableAvgFinalCost { get; set; }
		public bool EnableDiscounts { get; set; }
		public bool EnableSection { get; set; }
		public bool ShowTotalInserts { get; set; }
		public bool ShowDimensions { get; set; }
		public bool ShowPageSize { get; set; }
		public bool ShowPercentOfPage { get; set; }
		public bool ShowTotalColor { get; set; }
		public bool ShowAvgAdCost { get; set; }
		public bool ShowAvgFinalCost { get; set; }
		public bool ShowDiscounts { get; set; }
		public bool ShowSection { get; set; }

		public bool ShowAvgPCI { get; set; }
		public bool ShowTotalSquare { get; set; }
		public bool ShowSquare { get; set; }
		public bool ShowMechanicals { get; set; }

		public string InvestmentType { get; set; }
		public string Comments { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<ShowName>" + ShowName + @"</ShowName>");
			result.AppendLine(@"<ShowLogo>" + ShowLogo + @"</ShowLogo>");
			result.AppendLine(@"<ShowInvestment>" + ShowInvestment + @"</ShowInvestment>");

			result.AppendLine(@"<EnableFlightDates>" + EnableFlightDates + @"</EnableFlightDates>");
			result.AppendLine(@"<EnableDates>" + EnableDates + @"</EnableDates>");
			result.AppendLine(@"<EnableComments>" + EnableComments + @"</EnableComments>");
			result.AppendLine(@"<ShowFlightDates>" + ShowFlightDates + @"</ShowFlightDates>");
			result.AppendLine(@"<ShowDates>" + ShowDates + @"</ShowDates>");
			result.AppendLine(@"<ShowComments>" + ShowComments + @"</ShowComments>");

			result.AppendLine(@"<EnableTotalInserts>" + EnableTotalInserts + @"</EnableTotalInserts>");
			result.AppendLine(@"<EnableDimensions>" + EnableDimensions + @"</EnableDimensions>");
			result.AppendLine(@"<EnablePageSize>" + EnablePageSize + @"</EnablePageSize>");
			result.AppendLine(@"<EnablePercentOfPage>" + EnablePercentOfPage + @"</EnablePercentOfPage>");
			result.AppendLine(@"<EnableTotalColor>" + EnableTotalColor + @"</EnableTotalColor>");
			result.AppendLine(@"<EnableAvgAdCost>" + EnableAvgAdCost + @"</EnableAvgAdCost>");
			result.AppendLine(@"<EnableAvgFinalCost>" + EnableAvgFinalCost + @"</EnableAvgFinalCost>");
			result.AppendLine(@"<EnableDiscounts>" + EnableDiscounts + @"</EnableDiscounts>");
			result.AppendLine(@"<EnableSection>" + EnableSection + @"</EnableSection>");
			result.AppendLine(@"<ShowTotalInserts>" + ShowTotalInserts + @"</ShowTotalInserts>");
			result.AppendLine(@"<ShowDimensions>" + ShowDimensions + @"</ShowDimensions>");
			result.AppendLine(@"<ShowPageSize>" + ShowPageSize + @"</ShowPageSize>");
			result.AppendLine(@"<ShowPercentOfPage>" + ShowPercentOfPage + @"</ShowPercentOfPage>");
			result.AppendLine(@"<ShowTotalColor>" + ShowTotalColor + @"</ShowTotalColor>");
			result.AppendLine(@"<ShowAvgAdCost>" + ShowAvgAdCost + @"</ShowAvgAdCost>");
			result.AppendLine(@"<ShowAvgFinalCost>" + ShowAvgFinalCost + @"</ShowAvgFinalCost>");
			result.AppendLine(@"<ShowDiscounts>" + ShowDiscounts + @"</ShowDiscounts>");
			result.AppendLine(@"<ShowSection>" + ShowSection + @"</ShowSection>");

			result.AppendLine(@"<ShowAvgPCI>" + ShowAvgPCI + @"</ShowAvgPCI>");
			result.AppendLine(@"<ShowTotalSquare>" + ShowTotalSquare + @"</ShowTotalSquare>");
			result.AppendLine(@"<ShowSquare>" + ShowSquare + @"</ShowSquare>");
			result.AppendLine(@"<ShowMechanicals>" + ShowMechanicals + @"</ShowMechanicals>");

			result.AppendLine(@"<InvestmentType>" + InvestmentType + @"</InvestmentType>");
			result.AppendLine(@"<Comments>" + Comments.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Comments>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool = false;
			;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "ShowName":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowName = tempBool;
						break;
					case "ShowLogo":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLogo = tempBool;
						break;
					case "ShowInvestment":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowInvestment = tempBool;
						break;

					case "EnableFlightDates":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableFlightDates = tempBool;
						break;
					case "EnableDates":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDates = tempBool;
						break;
					case "EnableComments":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableComments = tempBool;
						break;
					case "ShowFlightDates":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowFlightDates = tempBool;
						break;
					case "ShowDates":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDates = tempBool;
						break;
					case "ShowComments":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowComments = tempBool;
						break;

					case "EnableTotalInserts":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableTotalInserts = tempBool;
						break;
					case "EnableDimensions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDimensions = tempBool;
						break;
					case "EnablePageSize":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnablePageSize = tempBool;
						break;
					case "EnablePercentOfPage":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnablePercentOfPage = tempBool;
						break;
					case "EnableTotalColor":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableTotalColor = tempBool;
						break;
					case "EnableAvgAdCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableAvgAdCost = tempBool;
						break;
					case "EnableAvgFinalCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableAvgFinalCost = tempBool;
						break;
					case "EnableDiscounts":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDiscounts = tempBool;
						break;
					case "EnableSection":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableSection = tempBool;
						break;
					case "ShowTotalInserts":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalInserts = tempBool;
						break;
					case "ShowDimensions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDimensions = tempBool;
						break;
					case "ShowPageSize":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPageSize = tempBool;
						break;
					case "ShowPercentOfPage":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPercentOfPage = tempBool;
						break;
					case "ShowTotalColor":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalColor = tempBool;
						break;
					case "ShowAvgAdCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAvgAdCost = tempBool;
						break;
					case "ShowAvgFinalCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAvgFinalCost = tempBool;
						break;
					case "ShowDiscounts":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDiscounts = tempBool;
						break;
					case "ShowSection":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSection = tempBool;
						break;

					case "ShowAvgPCI":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAvgPCI = tempBool;
						break;
					case "ShowTotalSquare":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalSquare = tempBool;
						break;
					case "ShowSquare":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSquare = tempBool;
						break;
					case "ShowMechanicals":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowMechanicals = tempBool;
						break;

					case "InvestmentType":
						InvestmentType = childNode.InnerText;
						break;
					case "Comments":
						Comments = childNode.InnerText;
						break;
				}
			}

			ShowFlightDates &= EnableFlightDates;
			ShowDates &= EnableDates;
			ShowComments &= EnableComments;

			ShowTotalInserts &= EnableTotalInserts;
			ShowDimensions &= EnableDimensions;
			ShowPageSize &= EnablePageSize;
			ShowPercentOfPage &= EnablePercentOfPage;
			ShowTotalColor &= EnableTotalColor;
			ShowAvgAdCost &= EnableAvgAdCost;
			ShowAvgFinalCost &= EnableAvgFinalCost;
			ShowDiscounts &= EnableDiscounts;
			ShowSection &= EnableSection;
		}
	}

	public class SnapshotViewSettings
	{
		public SnapshotViewSettings()
		{
			ShowSlideHeader = true;
			ShowPresentationDate = true;
			ShowAdvertiser = true;
			ShowDecisionMaker = true;
			ShowFlightDates = true;
			ShowOptions = true;
			SelectedOptionChapterIndex = 0;

			EnableLogo = true;
			EnableTotalInserts = true;
			EnableTotalFinalCost = true;
			EnablePageSize = true;
			EnableDimensions = true;
			EnableSquare = true;
			EnableTotalSquare = true;
			EnableAvgPCI = true;
			EnableAvgCost = true;
			EnableAvgFinalCost = true;
			EnableTotalColor = true;
			EnableTotalDiscounts = true;
			EnableReadership = true;
			EnableDelivery = true;
			EnablePercentOfPage = true;
			ShowLogo = true;
			ShowTotalInserts = true;
			ShowTotalFinalCost = true;
			ShowPageSize = false;
			ShowDimensions = false;
			ShowSquare = false;
			ShowTotalSquare = false;
			ShowAvgPCI = false;
			ShowAvgCost = false;
			ShowAvgFinalCost = false;
			ShowTotalColor = false;
			ShowTotalDiscounts = false;
			ShowReadership = false;
			ShowDelivery = false;
			ShowPercentOfPage = false;

			SlideHeader = string.Empty;
		}

		public bool ShowSlideHeader { get; set; }
		public bool ShowPresentationDate { get; set; }
		public bool ShowAdvertiser { get; set; }
		public bool ShowDecisionMaker { get; set; }
		public bool ShowFlightDates { get; set; }
		public bool ShowOptions { get; set; }
		public int SelectedOptionChapterIndex { get; set; }

		public bool EnableLogo { get; set; }
		public bool EnableTotalInserts { get; set; }
		public bool EnableTotalFinalCost { get; set; }
		public bool EnablePageSize { get; set; }
		public bool EnableDimensions { get; set; }
		public bool EnableSquare { get; set; }
		public bool EnableTotalSquare { get; set; }
		public bool EnableAvgPCI { get; set; }
		public bool EnableAvgCost { get; set; }
		public bool EnableAvgFinalCost { get; set; }
		public bool EnableTotalColor { get; set; }
		public bool EnableTotalDiscounts { get; set; }
		public bool EnableReadership { get; set; }
		public bool EnableDelivery { get; set; }
		public bool EnablePercentOfPage { get; set; }
		public bool ShowLogo { get; set; }
		public bool ShowTotalInserts { get; set; }
		public bool ShowTotalFinalCost { get; set; }
		public bool ShowPageSize { get; set; }
		public bool ShowDimensions { get; set; }
		public bool ShowSquare { get; set; }
		public bool ShowTotalSquare { get; set; }
		public bool ShowAvgPCI { get; set; }
		public bool ShowAvgCost { get; set; }
		public bool ShowAvgFinalCost { get; set; }
		public bool ShowTotalColor { get; set; }
		public bool ShowTotalDiscounts { get; set; }
		public bool ShowReadership { get; set; }
		public bool ShowDelivery { get; set; }
		public bool ShowPercentOfPage { get; set; }

		public string SlideHeader { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<ShowAdvertiser>" + ShowAdvertiser + @"</ShowAdvertiser>");
			result.AppendLine(@"<ShowDecisionMaker>" + ShowDecisionMaker + @"</ShowDecisionMaker>");
			result.AppendLine(@"<ShowFlightDates>" + ShowFlightDates + @"</ShowFlightDates>");
			result.AppendLine(@"<ShowPresentationDate>" + ShowPresentationDate + @"</ShowPresentationDate>");
			result.AppendLine(@"<ShowSlideHeader>" + ShowSlideHeader + @"</ShowSlideHeader>");
			result.AppendLine(@"<ShowOptions>" + ShowOptions + @"</ShowOptions>");
			result.AppendLine(@"<SelectedOptionChapterIndex>" + SelectedOptionChapterIndex + @"</SelectedOptionChapterIndex>");

			result.AppendLine(@"<EnableLogo>" + EnableLogo + @"</EnableLogo>");
			result.AppendLine(@"<EnableTotalInserts>" + EnableTotalInserts + @"</EnableTotalInserts>");
			result.AppendLine(@"<EnableTotalFinalCost>" + EnableTotalFinalCost + @"</EnableTotalFinalCost>");
			result.AppendLine(@"<EnablePageSize>" + EnablePageSize + @"</EnablePageSize>");
			result.AppendLine(@"<EnableDimensions>" + EnableDimensions + @"</EnableDimensions>");
			result.AppendLine(@"<EnableSquare>" + EnableSquare + @"</EnableSquare>");
			result.AppendLine(@"<EnableTotalSquare>" + EnableTotalSquare + @"</EnableTotalSquare>");
			result.AppendLine(@"<EnableAvgPCI>" + EnableAvgPCI + @"</EnableAvgPCI>");
			result.AppendLine(@"<EnableAvgCost>" + EnableAvgCost + @"</EnableAvgCost>");
			result.AppendLine(@"<EnableAvgFinalCost>" + EnableAvgFinalCost + @"</EnableAvgFinalCost>");
			result.AppendLine(@"<EnableTotalColor>" + EnableTotalColor + @"</EnableTotalColor>");
			result.AppendLine(@"<EnableTotalDiscounts>" + EnableTotalDiscounts + @"</EnableTotalDiscounts>");
			result.AppendLine(@"<EnableReadership>" + EnableReadership + @"</EnableReadership>");
			result.AppendLine(@"<EnableDelivery>" + EnableDelivery + @"</EnableDelivery>");
			result.AppendLine(@"<EnablePercentOfPage>" + EnablePercentOfPage + @"</EnablePercentOfPage>");
			result.AppendLine(@"<ShowLogo>" + ShowLogo + @"</ShowLogo>");
			result.AppendLine(@"<ShowTotalInserts>" + ShowTotalInserts + @"</ShowTotalInserts>");
			result.AppendLine(@"<ShowTotalFinalCost>" + ShowTotalFinalCost + @"</ShowTotalFinalCost>");
			result.AppendLine(@"<ShowPageSize>" + ShowPageSize + @"</ShowPageSize>");
			result.AppendLine(@"<ShowDimensions>" + ShowDimensions + @"</ShowDimensions>");
			result.AppendLine(@"<ShowSquare>" + ShowSquare + @"</ShowSquare>");
			result.AppendLine(@"<ShowTotalSquare>" + ShowTotalSquare + @"</ShowTotalSquare>");
			result.AppendLine(@"<ShowAvgPCI>" + ShowAvgPCI + @"</ShowAvgPCI>");
			result.AppendLine(@"<ShowAvgCost>" + ShowAvgCost + @"</ShowAvgCost>");
			result.AppendLine(@"<ShowAvgFinalCost>" + ShowAvgFinalCost + @"</ShowAvgFinalCost>");
			result.AppendLine(@"<ShowTotalColor>" + ShowTotalColor + @"</ShowTotalColor>");
			result.AppendLine(@"<ShowTotalDiscounts>" + ShowTotalDiscounts + @"</ShowTotalDiscounts>");
			result.AppendLine(@"<ShowReadership>" + ShowReadership + @"</ShowReadership>");
			result.AppendLine(@"<ShowDelivery>" + ShowDelivery + @"</ShowDelivery>");
			result.AppendLine(@"<ShowPercentOfPage>" + ShowPercentOfPage + @"</ShowPercentOfPage>");

			result.AppendLine(@"<SlideHeader>" + SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool = false;
			int tempInt;

			foreach (XmlNode childNode in node.ChildNodes)
			{
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
					case "ShowFlightDates":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowFlightDates = tempBool;
						break;
					case "ShowPresentationDate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPresentationDate = tempBool;
						break;
					case "ShowSlideHeader":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSlideHeader = tempBool;
						break;
					case "ShowOptions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowOptions = tempBool;
						break;
					case "SelectedOptionChapterIndex":
						if (int.TryParse(childNode.InnerText, out tempInt))
							SelectedOptionChapterIndex = tempInt;
						break;

					case "EnableLogo":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableLogo = tempBool;
						break;
					case "EnableTotalInserts":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableTotalInserts = tempBool;
						break;
					case "EnableTotalFinalCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableTotalFinalCost = tempBool;
						break;
					case "EnablePageSize":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnablePageSize = tempBool;
						break;
					case "EnableDimensions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDimensions = tempBool;
						break;
					case "EnableSquare":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableSquare = tempBool;
						break;
					case "EnableTotalSquare":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableTotalSquare = tempBool;
						break;
					case "EnableAvgPCI":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableAvgPCI = tempBool;
						break;
					case "EnableAvgCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableAvgCost = tempBool;
						break;
					case "EnableAvgFinalCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableAvgFinalCost = tempBool;
						break;
					case "EnableTotalColor":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableTotalColor = tempBool;
						break;
					case "EnableTotalDiscounts":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableTotalDiscounts = tempBool;
						break;
					case "EnableReadership":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableReadership = tempBool;
						break;
					case "EnableDelivery":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDelivery = tempBool;
						break;
					case "EnablePercentOfPage":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnablePercentOfPage = tempBool;
						break;
					case "ShowLogo":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLogo = tempBool;
						break;
					case "ShowTotalInserts":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalInserts = tempBool;
						break;
					case "ShowTotalFinalCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalFinalCost = tempBool;
						break;
					case "ShowPageSize":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPageSize = tempBool;
						break;
					case "ShowDimensions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDimensions = tempBool;
						break;
					case "ShowSquare":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSquare = tempBool;
						break;
					case "ShowTotalSquare":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalSquare = tempBool;
						break;
					case "ShowAvgPCI":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAvgPCI = tempBool;
						break;
					case "ShowAvgCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAvgCost = tempBool;
						break;
					case "ShowAvgFinalCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAvgFinalCost = tempBool;
						break;
					case "ShowTotalColor":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalColor = tempBool;
						break;
					case "ShowTotalDiscounts":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalDiscounts = tempBool;
						break;
					case "ShowReadership":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowReadership = tempBool;
						break;
					case "ShowDelivery":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDelivery = tempBool;
						break;
					case "ShowPercentOfPage":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPercentOfPage = tempBool;
						break;

					case "SlideHeader":
						SlideHeader = childNode.InnerText;
						break;
				}
			}

			ShowLogo &= EnableLogo;
			ShowTotalInserts &= EnableTotalInserts;
			ShowTotalFinalCost &= EnableTotalFinalCost;
			ShowPageSize &= EnablePageSize;
			ShowDimensions &= EnableDimensions;
			ShowSquare &= EnableSquare;
			ShowTotalSquare &= EnableTotalSquare;
			ShowAvgPCI &= EnableAvgPCI;
			ShowAvgCost &= EnableAvgCost;
			ShowAvgFinalCost &= EnableAvgFinalCost;
			ShowTotalColor &= EnableTotalColor;
			ShowTotalDiscounts &= EnableTotalDiscounts;
			ShowReadership &= EnableReadership;
			ShowDelivery &= EnableDelivery;
			ShowPercentOfPage &= EnablePercentOfPage;
		}
	}

	public class GridColumnsState
	{
		#region Enable
		public bool EnableID { get; set; }
		public bool EnableIndex { get; set; }
		public bool EnableDate { get; set; }
		public bool EnableColor { get; set; }
		public bool EnableSection { get; set; }
		public bool EnablePCI { get; set; }
		public bool EnableFinalCost { get; set; }
		public bool EnablePublication { get; set; }
		public bool EnablePercentOfPage { get; set; }
		public bool EnableCost { get; set; }
		public bool EnableDimensions { get; set; }
		public bool EnableMechanicals { get; set; }
		public bool EnableDelivery { get; set; }
		public bool EnableDiscount { get; set; }
		public bool EnablePageSize { get; set; }
		public bool EnableSquare { get; set; }
		public bool EnableDeadline { get; set; }
		public bool EnableReadership { get; set; }
		public bool EnableAdNotes { get; set; }
		#endregion

		#region Show
		public bool ShowID { get; set; }
		public bool ShowIndex { get; set; }
		public bool ShowDate { get; set; }
		public bool ShowColor { get; set; }
		public bool ShowSection { get; set; }
		public bool ShowPCI { get; set; }
		public bool ShowFinalCost { get; set; }
		public bool ShowPublication { get; set; }
		public bool ShowPercentOfPage { get; set; }
		public bool ShowCost { get; set; }
		public bool ShowDimensions { get; set; }
		public bool ShowMechanicals { get; set; }
		public bool ShowDelivery { get; set; }
		public bool ShowDiscount { get; set; }
		public bool ShowPageSize { get; set; }
		public bool ShowSquare { get; set; }
		public bool ShowDeadline { get; set; }
		public bool ShowReadership { get; set; }
		public bool ShowAdNotes { get; set; }
		#endregion

		#region Position
		public int IDPosition { get; set; }
		public int IndexPosition { get; set; }
		public int DatePosition { get; set; }
		public int ColorPosition { get; set; }
		public int SectionPosition { get; set; }
		public int PCIPosition { get; set; }
		public int FinalCostPosition { get; set; }
		public int PublicationPosition { get; set; }
		public int PercentOfPagePosition { get; set; }
		public int CostPosition { get; set; }
		public int DimensionsPosition { get; set; }
		public int MechanicalsPosition { get; set; }
		public int DeliveryPosition { get; set; }
		public int DiscountPosition { get; set; }
		public int PageSizePosition { get; set; }
		public int SquarePosition { get; set; }
		public int DeadlinePosition { get; set; }
		public int ReadershipPosition { get; set; }
		#endregion

		#region Width
		public int IDWidth { get; set; }
		public int IndexWidth { get; set; }
		public int DateWidth { get; set; }
		public int PCIWidth { get; set; }
		public int CostWidth { get; set; }
		public int FinalCostWidth { get; set; }
		public int DiscountWidth { get; set; }
		public int ColorWidth { get; set; }
		public int PublicationWidth { get; set; }
		public int SquareWidth { get; set; }
		public int PageSizeWidth { get; set; }
		public int PercentOfPageWidth { get; set; }
		public int DimensionsWidth { get; set; }
		public int MechanicalsWidth { get; set; }
		public int SectionWidth { get; set; }
		public int DeliveryWidth { get; set; }
		public int ReadershipWidth { get; set; }
		public int DeadlineWidth { get; set; }
		#endregion

		#region Caption
		public string IDCaption { get; set; }
		public string IndexCaption { get; set; }
		public string DateCaption { get; set; }
		public string PCICaption { get; set; }
		public string CostCaption { get; set; }
		public string FinalCostCaption { get; set; }
		public string DiscountCaption { get; set; }
		public string ColorCaption { get; set; }
		public string PublicationCaption { get; set; }
		public string SquareCaption { get; set; }
		public string PageSizeCaption { get; set; }
		public string PercentOfPageCaption { get; set; }
		public string DimensionsCaption { get; set; }
		public string MechanicalsCaption { get; set; }
		public string SectionCaption { get; set; }
		public string DeliveryCaption { get; set; }
		public string ReadershipCaption { get; set; }
		public string DeadlineCaption { get; set; }
		#endregion

		public GridColumnsState()
		{
			#region Enable
			EnableID = true;
			EnableIndex = true;
			EnableDate = true;
			EnableColor = true;
			EnableSection = true;
			EnablePCI = true;
			EnableFinalCost = true;
			EnablePublication = true;
			EnablePercentOfPage = true;
			EnableCost = true;
			EnableDimensions = true;
			EnableMechanicals = true;
			EnableDelivery = true;
			EnableDiscount = true;
			EnablePageSize = true;
			EnableSquare = true;
			EnableDeadline = true;
			EnableReadership = true;
			EnableAdNotes = true;
			#endregion

			#region Show
			ShowID = true;
			ShowIndex = true;
			ShowDate = true;
			ShowColor = false;
			ShowSection = false;
			ShowPCI = false;
			ShowFinalCost = false;
			ShowPublication = true;
			ShowPercentOfPage = false;
			ShowCost = false;
			ShowDimensions = false;
			ShowMechanicals = false;
			ShowDelivery = false;
			ShowDiscount = false;
			ShowPageSize = false;
			ShowSquare = false;
			ShowDeadline = false;
			ShowReadership = false;
			ShowAdNotes = true;
			#endregion

			#region Position
			IDPosition = 0;
			IndexPosition = 1;
			DatePosition = 2;
			ColorPosition = 4;
			SectionPosition = 5;
			PCIPosition = 6;
			FinalCostPosition = 7;
			PublicationPosition = 3;
			PercentOfPagePosition = 8;
			CostPosition = 9;
			DimensionsPosition = 10;
			MechanicalsPosition = 11;
			DeliveryPosition = 12;
			DiscountPosition = 13;
			PageSizePosition = 14;
			SquarePosition = 15;
			DeadlinePosition = 16;
			ReadershipPosition = 17;
			#endregion

			#region Width
			IDWidth = 50;
			IndexWidth = 50;
			DateWidth = 155;
			PCIWidth = 110;
			CostWidth = 110;
			FinalCostWidth = 110;
			DiscountWidth = 110;
			ColorWidth = 110;
			PublicationWidth = 160;
			SquareWidth = 110;
			PageSizeWidth = 110;
			PercentOfPageWidth = 110;
			DimensionsWidth = 110;
			MechanicalsWidth = 110;
			SectionWidth = 110;
			DeliveryWidth = 110;
			ReadershipWidth = 110;
			DeadlineWidth = 110;
			#endregion

			#region Caption
			IDCaption = @"ID";
			IndexCaption = @"INS #";
			DateCaption = @"Day/Date";
			PCICaption = @"PCI";
			CostCaption = @"Cost (B&W)";
			FinalCostCaption = @"Total Cost";
			DiscountCaption = @"Discounts";
			ColorCaption = @"Color";
			PublicationCaption = @"Publication";
			SquareCaption = @"Total Col. In.";
			PageSizeCaption = @"Page Size";
			PercentOfPageCaption = @"% of Page";
			DimensionsCaption = @"Col. x Inches";
			MechanicalsCaption = @"Mechanicals";
			SectionCaption = @"Section";
			DeliveryCaption = @"Delivery";
			ReadershipCaption = @"Readership";
			DeadlineCaption = @"Deadline";
			#endregion
		}

		public string Serialize()
		{
			var result = new StringBuilder();

			#region Enable
			result.AppendLine(@"<EnableID>" + EnableID + @"</EnableID>");
			result.AppendLine(@"<EnableIndex>" + EnableIndex + @"</EnableIndex>");
			result.AppendLine(@"<EnableDate>" + EnableDate + @"</EnableDate>");
			result.AppendLine(@"<EnableColor>" + EnableColor + @"</EnableColor>");
			result.AppendLine(@"<EnableSection>" + EnableSection + @"</EnableSection>");
			result.AppendLine(@"<EnablePCI>" + EnablePCI + @"</EnablePCI>");
			result.AppendLine(@"<EnableFinalCost>" + EnableFinalCost + @"</EnableFinalCost>");
			result.AppendLine(@"<EnablePublication>" + EnablePublication + @"</EnablePublication>");
			result.AppendLine(@"<EnablePercentOfPage>" + EnablePercentOfPage + @"</EnablePercentOfPage>");
			result.AppendLine(@"<EnableCost>" + EnableCost + @"</EnableCost>");
			result.AppendLine(@"<EnableDimensions>" + EnableDimensions + @"</EnableDimensions>");
			result.AppendLine(@"<EnableMechanicals>" + EnableMechanicals + @"</EnableMechanicals>");
			result.AppendLine(@"<EnableDelivery>" + EnableDelivery + @"</EnableDelivery>");
			result.AppendLine(@"<EnableDiscount>" + EnableDiscount + @"</EnableDiscount>");
			result.AppendLine(@"<EnablePageSize>" + EnablePageSize + @"</EnablePageSize>");
			result.AppendLine(@"<EnableSquare>" + EnableSquare + @"</EnableSquare>");
			result.AppendLine(@"<EnableDeadline>" + EnableDeadline + @"</EnableDeadline>");
			result.AppendLine(@"<EnableReadership>" + EnableReadership + @"</EnableReadership>");
			result.AppendLine(@"<EnableAdNotes>" + EnableAdNotes + @"</EnableAdNotes>");
			#endregion

			#region Show
			result.AppendLine(@"<ShowID>" + ShowID + @"</ShowID>");
			result.AppendLine(@"<ShowIndex>" + ShowIndex + @"</ShowIndex>");
			result.AppendLine(@"<ShowDate>" + ShowDate + @"</ShowDate>");
			result.AppendLine(@"<ShowColor>" + ShowColor + @"</ShowColor>");
			result.AppendLine(@"<ShowSection>" + ShowSection + @"</ShowSection>");
			result.AppendLine(@"<ShowPCI>" + ShowPCI + @"</ShowPCI>");
			result.AppendLine(@"<ShowFinalCost>" + ShowFinalCost + @"</ShowFinalCost>");
			result.AppendLine(@"<ShowPublication>" + ShowPublication + @"</ShowPublication>");
			result.AppendLine(@"<ShowPercentOfPage>" + ShowPercentOfPage + @"</ShowPercentOfPage>");
			result.AppendLine(@"<ShowCost>" + ShowCost + @"</ShowCost>");
			result.AppendLine(@"<ShowDimensions>" + ShowDimensions + @"</ShowDimensions>");
			result.AppendLine(@"<ShowMechanicals>" + ShowMechanicals + @"</ShowMechanicals>");
			result.AppendLine(@"<ShowDelivery>" + ShowDelivery + @"</ShowDelivery>");
			result.AppendLine(@"<ShowDiscount>" + ShowDiscount + @"</ShowDiscount>");
			result.AppendLine(@"<ShowPageSize>" + ShowPageSize + @"</ShowPageSize>");
			result.AppendLine(@"<ShowSquare>" + ShowSquare + @"</ShowSquare>");
			result.AppendLine(@"<ShowDeadline>" + ShowDeadline + @"</ShowDeadline>");
			result.AppendLine(@"<ShowReadership>" + ShowReadership + @"</ShowReadership>");
			result.AppendLine(@"<ShowAdNotes>" + ShowAdNotes + @"</ShowAdNotes>");
			#endregion

			#region Position
			result.AppendLine(@"<IDPosition>" + IDPosition + @"</IDPosition>");
			result.AppendLine(@"<IndexPosition>" + IndexPosition + @"</IndexPosition>");
			result.AppendLine(@"<DatePosition>" + DatePosition + @"</DatePosition>");
			result.AppendLine(@"<ColorPosition>" + ColorPosition + @"</ColorPosition>");
			result.AppendLine(@"<SectionPosition>" + SectionPosition + @"</SectionPosition>");
			result.AppendLine(@"<PCIPosition>" + PCIPosition + @"</PCIPosition>");
			result.AppendLine(@"<FinalCostPosition>" + FinalCostPosition + @"</FinalCostPosition>");
			result.AppendLine(@"<PublicationPosition>" + PublicationPosition + @"</PublicationPosition>");
			result.AppendLine(@"<PercentOfPagePosition>" + PercentOfPagePosition + @"</PercentOfPagePosition>");
			result.AppendLine(@"<CostPosition>" + CostPosition + @"</CostPosition>");
			result.AppendLine(@"<DimensionsPosition>" + DimensionsPosition + @"</DimensionsPosition>");
			result.AppendLine(@"<MechanicalsPosition>" + MechanicalsPosition + @"</MechanicalsPosition>");
			result.AppendLine(@"<DeliveryPosition>" + DeliveryPosition + @"</DeliveryPosition>");
			result.AppendLine(@"<DiscountPosition>" + DiscountPosition + @"</DiscountPosition>");
			result.AppendLine(@"<PageSizePosition>" + PageSizePosition + @"</PageSizePosition>");
			result.AppendLine(@"<SquarePosition>" + SquarePosition + @"</SquarePosition>");
			result.AppendLine(@"<DeadlinePosition>" + DeadlinePosition + @"</DeadlinePosition>");
			result.AppendLine(@"<ReadershipPosition>" + ReadershipPosition + @"</ReadershipPosition>");
			#endregion

			#region Width
			result.AppendLine(@"<IDWidth>" + IDWidth + @"</IDWidth>");
			result.AppendLine(@"<IndexWidth>" + IndexWidth + @"</IndexWidth>");
			result.AppendLine(@"<DateWidth>" + DateWidth + @"</DateWidth>");
			result.AppendLine(@"<PCIWidth>" + PCIWidth + @"</PCIWidth>");
			result.AppendLine(@"<CostWidth>" + CostWidth + @"</CostWidth>");
			result.AppendLine(@"<FinalCostWidth>" + FinalCostWidth + @"</FinalCostWidth>");
			result.AppendLine(@"<DiscountWidth>" + DiscountWidth + @"</DiscountWidth>");
			result.AppendLine(@"<ColorWidth>" + ColorWidth + @"</ColorWidth>");
			result.AppendLine(@"<PublicationWidth>" + PublicationWidth + @"</PublicationWidth>");
			result.AppendLine(@"<SquareWidth>" + SquareWidth + @"</SquareWidth>");
			result.AppendLine(@"<PageSizeWidth>" + PageSizeWidth + @"</PageSizeWidth>");
			result.AppendLine(@"<PercentOfPageWidth>" + PercentOfPageWidth + @"</PercentOfPageWidth>");
			result.AppendLine(@"<DimensionsWidth>" + DimensionsWidth + @"</DimensionsWidth>");
			result.AppendLine(@"<MechanicalsWidth>" + MechanicalsWidth + @"</MechanicalsWidth>");
			result.AppendLine(@"<SectionWidth>" + SectionWidth + @"</SectionWidth>");
			result.AppendLine(@"<DeliveryWidth>" + DeliveryWidth + @"</DeliveryWidth>");
			result.AppendLine(@"<ReadershipWidth>" + ReadershipWidth + @"</ReadershipWidth>");
			result.AppendLine(@"<DeadlineWidth>" + DeadlineWidth + @"</DeadlineWidth>");
			#endregion

			#region Caption
			result.AppendLine(@"<IDCaption>" + IDCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</IDCaption>");
			result.AppendLine(@"<IndexCaption>" + IndexCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</IndexCaption>");
			result.AppendLine(@"<DateCaption>" + DateCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DateCaption>");
			result.AppendLine(@"<PCICaption>" + PCICaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</PCICaption>");
			result.AppendLine(@"<CostCaption>" + CostCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</CostCaption>");
			result.AppendLine(@"<FinalCostCaption>" + FinalCostCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</FinalCostCaption>");
			result.AppendLine(@"<DiscountCaption>" + DiscountCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DiscountCaption>");
			result.AppendLine(@"<ColorCaption>" + ColorCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</ColorCaption>");
			result.AppendLine(@"<PublicationCaption>" + PublicationCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</PublicationCaption>");
			result.AppendLine(@"<SquareCaption>" + SquareCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SquareCaption>");
			result.AppendLine(@"<PageSizeCaption>" + PageSizeCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</PageSizeCaption>");
			result.AppendLine(@"<PercentOfPageCaption>" + PercentOfPageCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</PercentOfPageCaption>");
			result.AppendLine(@"<DimensionsCaption>" + DimensionsCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DimensionsCaption>");
			result.AppendLine(@"<MechanicalsCaption>" + MechanicalsCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</MechanicalsCaption>");
			result.AppendLine(@"<SectionCaption>" + SectionCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SectionCaption>");
			result.AppendLine(@"<DeliveryCaption>" + DeliveryCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DeliveryCaption>");
			result.AppendLine(@"<ReadershipCaption>" + ReadershipCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</ReadershipCaption>");
			result.AppendLine(@"<DeadlineCaption>" + DeadlineCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DeadlineCaption>");
			#endregion

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool = false;
			int tempInt = -1;

			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					#region Enable
					case "EnableID":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableID = tempBool;
						break;
					case "EnableIndex":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableIndex = tempBool;
						break;
					case "EnableDate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDate = tempBool;
						break;
					case "EnableColor":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableColor = tempBool;
						break;
					case "EnableSection":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableSection = tempBool;
						break;
					case "EnablePCI":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnablePCI = tempBool;
						break;
					case "EnableFinalCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableFinalCost = tempBool;
						break;
					case "EnablePublication":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnablePublication = tempBool;
						break;
					case "EnablePercentOfPage":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnablePercentOfPage = tempBool;
						break;
					case "EnableCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableCost = tempBool;
						break;
					case "EnableDimensions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDimensions = tempBool;
						break;
					case "EnableMechanicals":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableMechanicals = tempBool;
						break;
					case "EnableDelivery":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDelivery = tempBool;
						break;
					case "EnableDiscount":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDiscount = tempBool;
						break;
					case "EnablePageSize":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnablePageSize = tempBool;
						break;
					case "EnableSquare":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableSquare = tempBool;
						break;
					case "EnableDeadline":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDeadline = tempBool;
						break;
					case "EnableReadership":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableReadership = tempBool;
						break;
					case "EnableAdNotes":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableAdNotes = tempBool;
						break;
					#endregion

					#region Show
					case "ShowID":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowID = tempBool;
						break;
					case "ShowIndex":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowIndex = tempBool;
						break;
					case "ShowDate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDate = tempBool;
						break;
					case "ShowColor":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowColor = tempBool;
						break;
					case "ShowSection":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSection = tempBool;
						break;
					case "ShowPCI":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPCI = tempBool;
						break;
					case "ShowFinalCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowFinalCost = tempBool;
						break;
					case "ShowPublication":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPublication = tempBool;
						break;
					case "ShowPercentOfPage":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPercentOfPage = tempBool;
						break;
					case "ShowCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowCost = tempBool;
						break;
					case "ShowDimensions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDimensions = tempBool;
						break;
					case "ShowMechanicals":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowMechanicals = tempBool;
						break;
					case "ShowDelivery":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDelivery = tempBool;
						break;
					case "ShowDiscount":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDiscount = tempBool;
						break;
					case "ShowPageSize":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPageSize = tempBool;
						break;
					case "ShowSquare":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSquare = tempBool;
						break;
					case "ShowDeadline":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDeadline = tempBool;
						break;
					case "ShowReadership":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowReadership = tempBool;
						break;
					case "ShowAdNotes":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAdNotes = tempBool;
						break;
					#endregion

					#region Position
					case "IDPosition":
						if (int.TryParse(childNode.InnerText, out tempInt))
							IDPosition = tempInt;
						break;
					case "IndexPosition":
						if (int.TryParse(childNode.InnerText, out tempInt))
							IndexPosition = tempInt;
						break;
					case "DatePosition":
						if (int.TryParse(childNode.InnerText, out tempInt))
							DatePosition = tempInt;
						break;
					case "ColorPosition":
						if (int.TryParse(childNode.InnerText, out tempInt))
							ColorPosition = tempInt;
						break;
					case "SectionPosition":
						if (int.TryParse(childNode.InnerText, out tempInt))
							SectionPosition = tempInt;
						break;
					case "PCIPosition":
						if (int.TryParse(childNode.InnerText, out tempInt))
							PCIPosition = tempInt;
						break;
					case "FinalCostPosition":
						if (int.TryParse(childNode.InnerText, out tempInt))
							FinalCostPosition = tempInt;
						break;
					case "PublicationPosition":
						if (int.TryParse(childNode.InnerText, out tempInt))
							PublicationPosition = tempInt;
						break;
					case "PercentOfPagePosition":
						if (int.TryParse(childNode.InnerText, out tempInt))
							PercentOfPagePosition = tempInt;
						break;
					case "CostPosition":
						if (int.TryParse(childNode.InnerText, out tempInt))
							CostPosition = tempInt;
						break;
					case "DimensionsPosition":
						if (int.TryParse(childNode.InnerText, out tempInt))
							DimensionsPosition = tempInt;
						break;
					case "MechanicalsPosition":
						if (int.TryParse(childNode.InnerText, out tempInt))
							MechanicalsPosition = tempInt;
						break;
					case "DeliveryPosition":
						if (int.TryParse(childNode.InnerText, out tempInt))
							DeliveryPosition = tempInt;
						break;
					case "DiscountPosition":
						if (int.TryParse(childNode.InnerText, out tempInt))
							DiscountPosition = tempInt;
						break;
					case "PageSizePosition":
						if (int.TryParse(childNode.InnerText, out tempInt))
							PageSizePosition = tempInt;
						break;
					case "SquarePosition":
						if (int.TryParse(childNode.InnerText, out tempInt))
							SquarePosition = tempInt;
						break;
					case "DeadlinePosition":
						if (int.TryParse(childNode.InnerText, out tempInt))
							DeadlinePosition = tempInt;
						break;
					case "ReadershipPosition":
						if (int.TryParse(childNode.InnerText, out tempInt))
							ReadershipPosition = tempInt;
						break;
					#endregion

					#region Width
					case "ColorWidth":
						if (int.TryParse(childNode.InnerText, out tempInt))
							ColorWidth = tempInt;
						break;
					case "CostWidth":
						if (int.TryParse(childNode.InnerText, out tempInt))
							CostWidth = tempInt;
						break;
					case "DateWidth":
						if (int.TryParse(childNode.InnerText, out tempInt))
							DateWidth = tempInt;
						break;
					case "DeadlineWidth":
						if (int.TryParse(childNode.InnerText, out tempInt))
							DeadlineWidth = tempInt;
						break;
					case "DeliveryWidth":
						if (int.TryParse(childNode.InnerText, out tempInt))
							DeliveryWidth = tempInt;
						break;
					case "DimensionsWidth":
						if (int.TryParse(childNode.InnerText, out tempInt))
							DimensionsWidth = tempInt;
						break;
					case "DiscountWidth":
						if (int.TryParse(childNode.InnerText, out tempInt))
							DiscountWidth = tempInt;
						break;
					case "FinalCostWidth":
						if (int.TryParse(childNode.InnerText, out tempInt))
							FinalCostWidth = tempInt;
						break;
					case "IDWidth":
						if (int.TryParse(childNode.InnerText, out tempInt))
							IDWidth = tempInt;
						break;
					case "IndexWidth":
						if (int.TryParse(childNode.InnerText, out tempInt))
							IndexWidth = tempInt;
						break;
					case "MechanicalsWidth":
						if (int.TryParse(childNode.InnerText, out tempInt))
							MechanicalsWidth = tempInt;
						break;
					case "PageSizeWidth":
						if (int.TryParse(childNode.InnerText, out tempInt))
							PageSizeWidth = tempInt;
						break;
					case "PercentOfPageWidth":
						if (int.TryParse(childNode.InnerText, out tempInt))
							PercentOfPageWidth = tempInt;
						break;
					case "PCIWidth":
						if (int.TryParse(childNode.InnerText, out tempInt))
							PCIWidth = tempInt;
						break;
					case "PublicationWidth":
						if (int.TryParse(childNode.InnerText, out tempInt))
							PublicationWidth = tempInt;
						break;
					case "ReadershipWidth":
						if (int.TryParse(childNode.InnerText, out tempInt))
							ReadershipWidth = tempInt;
						break;
					case "SectionWidth":
						if (int.TryParse(childNode.InnerText, out tempInt))
							SectionWidth = tempInt;
						break;
					case "SquareWidth":
						if (int.TryParse(childNode.InnerText, out tempInt))
							SquareWidth = tempInt;
						break;
					#endregion

					#region Caption
					case "ColorCaption":
						ColorCaption = childNode.InnerText;
						break;
					case "CostCaption":
						CostCaption = childNode.InnerText.Replace("&&", "&");
						break;
					case "DateCaption":
						DateCaption = childNode.InnerText;
						break;
					case "DeadlineCaption":
						DeadlineCaption = childNode.InnerText;
						break;
					case "DeliveryCaption":
						DeliveryCaption = childNode.InnerText;
						break;
					case "DimensionsCaption":
						DimensionsCaption = childNode.InnerText;
						break;
					case "DiscountCaption":
						DiscountCaption = childNode.InnerText;
						break;
					case "FinalCostCaption":
						FinalCostCaption = childNode.InnerText;
						break;
					case "IDCaption":
						IDCaption = childNode.InnerText;
						break;
					case "IndexCaption":
						IndexCaption = childNode.InnerText;
						break;
					case "MechanicalsCaption":
						MechanicalsCaption = childNode.InnerText;
						break;
					case "PageSizeCaption":
						PageSizeCaption = childNode.InnerText;
						break;
					case "PercentOfPageCaption":
						PercentOfPageCaption = childNode.InnerText;
						break;
					case "PCICaption":
						PCICaption = childNode.InnerText;
						break;
					case "PublicationCaption":
						PublicationCaption = childNode.InnerText;
						break;
					case "ReadershipCaption":
						ReadershipCaption = childNode.InnerText;
						break;
					case "SectionCaption":
						SectionCaption = childNode.InnerText;
						break;
					case "SquareCaption":
						SquareCaption = childNode.InnerText;
						break;
					#endregion
				}
			}

			ShowID &= EnableID;
			ShowIndex &= EnableIndex;
			ShowDate &= EnableDate;
			ShowColor &= EnableColor;
			ShowSection &= EnableSection;
			ShowPCI &= EnablePCI;
			ShowFinalCost &= EnableFinalCost;
			ShowPublication &= EnablePublication;
			ShowPercentOfPage &= EnablePercentOfPage;
			ShowCost &= EnableCost;
			ShowDimensions &= EnableDimensions;
			ShowMechanicals &= EnableMechanicals;
			ShowDelivery &= EnableDelivery;
			ShowDiscount &= EnableDiscount;
			ShowPageSize &= EnablePageSize;
			ShowSquare &= EnableSquare;
			ShowDeadline &= EnableDeadline;
			ShowReadership &= EnableReadership;
			ShowAdNotes &= EnableAdNotes;
		}
	}

	public class AdNotesState
	{
		#region Enable
		public bool EnableComments { get; set; }
		public bool EnableSection { get; set; }
		public bool EnableMechanicals { get; set; }
		public bool EnableDimensions { get; set; }
		public bool EnableDelivery { get; set; }
		public bool EnablePublication { get; set; }
		public bool EnableSquare { get; set; }
		public bool EnablePageSize { get; set; }
		public bool EnablePercentOfPage { get; set; }
		public bool EnableReadership { get; set; }
		public bool EnableDeadline { get; set; }
		#endregion

		#region Show
		public bool ShowComments { get; set; }
		public bool ShowSection { get; set; }
		public bool ShowMechanicals { get; set; }
		public bool ShowDimensions { get; set; }
		public bool ShowDelivery { get; set; }
		public bool ShowPublication { get; set; }
		public bool ShowSquare { get; set; }
		public bool ShowPageSize { get; set; }
		public bool ShowPercentOfPage { get; set; }
		public bool ShowReadership { get; set; }
		public bool ShowDeadline { get; set; }
		#endregion

		#region Position
		public int PositionComments { get; set; }
		public int PositionSection { get; set; }
		public int PositionMechanicals { get; set; }
		public int PositionDimensions { get; set; }
		public int PositionDelivery { get; set; }
		public int PositionPublication { get; set; }
		public int PositionSquare { get; set; }
		public int PositionPageSize { get; set; }
		public int PositionPercentOfPage { get; set; }
		public int PositionReadership { get; set; }
		public int PositionDeadline { get; set; }
		#endregion

		public AdNotesState()
		{
			#region Enable
			EnableComments = true;
			EnableSection = true;
			EnableMechanicals = true;
			EnableDimensions = true;
			EnableDelivery = true;
			EnablePublication = true;
			EnableSquare = true;
			EnablePageSize = true;
			EnablePercentOfPage = true;
			EnableReadership = true;
			EnableDeadline = true;
			#endregion

			#region Show
			ShowComments = true;
			ShowSection = true;
			ShowMechanicals = false;
			ShowDimensions = false;
			ShowDelivery = false;
			ShowPublication = false;
			ShowSquare = false;
			ShowPageSize = false;
			ShowPercentOfPage = false;
			ShowReadership = false;
			ShowDeadline = false;
			#endregion

			#region Position
			PositionComments = 1;
			PositionSection = 2;
			PositionMechanicals = 3;
			PositionDimensions = 4;
			PositionDelivery = 5;
			PositionPublication = 6;
			PositionSquare = 7;
			PositionPageSize = 8;
			PositionPercentOfPage = 9;
			PositionReadership = 10;
			PositionDeadline = 11;
			#endregion
		}

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<EnableComments>" + EnableComments + @"</EnableComments>");
			result.AppendLine(@"<EnableDeadline>" + EnableDeadline + @"</EnableDeadline>");
			result.AppendLine(@"<EnableDelivery>" + EnableDelivery + @"</EnableDelivery>");
			result.AppendLine(@"<EnableDimensions>" + EnableDimensions + @"</EnableDimensions>");
			result.AppendLine(@"<EnableMechanicals>" + EnableMechanicals + @"</EnableMechanicals>");
			result.AppendLine(@"<EnablePageSize>" + EnablePageSize + @"</EnablePageSize>");
			result.AppendLine(@"<EnablePercentOfPage>" + EnablePercentOfPage + @"</EnablePercentOfPage>");
			result.AppendLine(@"<EnablePublication>" + EnablePublication + @"</EnablePublication>");
			result.AppendLine(@"<EnableReadership>" + EnableReadership + @"</EnableReadership>");
			result.AppendLine(@"<EnableSection>" + EnableSection + @"</EnableSection>");
			result.AppendLine(@"<EnableSquare>" + EnableSquare + @"</EnableSquare>");

			result.AppendLine(@"<ShowComments>" + ShowComments + @"</ShowComments>");
			result.AppendLine(@"<ShowDeadline>" + ShowDeadline + @"</ShowDeadline>");
			result.AppendLine(@"<ShowDelivery>" + ShowDelivery + @"</ShowDelivery>");
			result.AppendLine(@"<ShowDimensions>" + ShowDimensions + @"</ShowDimensions>");
			result.AppendLine(@"<ShowMechanicals>" + ShowMechanicals + @"</ShowMechanicals>");
			result.AppendLine(@"<ShowPageSize>" + ShowPageSize + @"</ShowPageSize>");
			result.AppendLine(@"<ShowPercentOfPage>" + ShowPercentOfPage + @"</ShowPercentOfPage>");
			result.AppendLine(@"<ShowPublication>" + ShowPublication + @"</ShowPublication>");
			result.AppendLine(@"<ShowReadership>" + ShowReadership + @"</ShowReadership>");
			result.AppendLine(@"<ShowSection>" + ShowSection + @"</ShowSection>");
			result.AppendLine(@"<ShowSquare>" + ShowSquare + @"</ShowSquare>");

			result.AppendLine(@"<PositionComments>" + PositionComments + @"</PositionComments>");
			result.AppendLine(@"<PositionDeadline>" + PositionDeadline + @"</PositionDeadline>");
			result.AppendLine(@"<PositionDelivery>" + PositionDelivery + @"</PositionDelivery>");
			result.AppendLine(@"<PositionDimensions>" + PositionDimensions + @"</PositionDimensions>");
			result.AppendLine(@"<PositionMechanicals>" + PositionMechanicals + @"</PositionMechanicals>");
			result.AppendLine(@"<PositionPageSize>" + PositionPageSize + @"</PositionPageSize>");
			result.AppendLine(@"<PositionPercentOfPage>" + PositionPercentOfPage + @"</PositionPercentOfPage>");
			result.AppendLine(@"<PositionPublication>" + PositionPublication + @"</PositionPublication>");
			result.AppendLine(@"<PositionReadership>" + PositionReadership + @"</PositionReadership>");
			result.AppendLine(@"<PositionSection>" + PositionSection + @"</PositionSection>");
			result.AppendLine(@"<PositionSquare>" + PositionSquare + @"</PositionSquare>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool = false;
			int tempInt = 0;

			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "EnableComments":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableComments = tempBool;
						break;
					case "EnableDeadline":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDeadline = tempBool;
						break;
					case "EnableDelivery":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDelivery = tempBool;
						break;
					case "EnableDimensions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDimensions = tempBool;
						break;
					case "EnableMechanicals":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableMechanicals = tempBool;
						break;
					case "EnablePageSize":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnablePageSize = tempBool;
						break;
					case "EnablePercentOfPage":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnablePercentOfPage = tempBool;
						break;
					case "EnablePublication":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnablePublication = tempBool;
						break;
					case "EnableSection":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableSection = tempBool;
						break;
					case "EnableReadership":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableReadership = tempBool;
						break;
					case "EnableSquare":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableSquare = tempBool;
						break;

					case "ShowComments":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowComments = tempBool;
						break;
					case "ShowDeadline":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDeadline = tempBool;
						break;
					case "ShowDelivery":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDelivery = tempBool;
						break;
					case "ShowDimensions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDimensions = tempBool;
						break;
					case "ShowMechanicals":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowMechanicals = tempBool;
						break;
					case "ShowPageSize":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPageSize = tempBool;
						break;
					case "ShowPercentOfPage":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPercentOfPage = tempBool;
						break;
					case "ShowPublication":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPublication = tempBool;
						break;
					case "ShowSection":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSection = tempBool;
						break;
					case "ShowReadership":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowReadership = tempBool;
						break;
					case "ShowSquare":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSquare = tempBool;
						break;

					case "PositionComments":
						if (int.TryParse(childNode.InnerText, out tempInt))
							PositionComments = tempInt;
						break;
					case "PositionDeadline":
						if (int.TryParse(childNode.InnerText, out tempInt))
							PositionDeadline = tempInt;
						break;
					case "PositionDelivery":
						if (int.TryParse(childNode.InnerText, out tempInt))
							PositionDelivery = tempInt;
						break;
					case "PositionDimensions":
						if (int.TryParse(childNode.InnerText, out tempInt))
							PositionDimensions = tempInt;
						break;
					case "PositionMechanicals":
						if (int.TryParse(childNode.InnerText, out tempInt))
							PositionMechanicals = tempInt;
						break;
					case "PositionPageSize":
						if (int.TryParse(childNode.InnerText, out tempInt))
							PositionPageSize = tempInt;
						break;
					case "PositionPercentOfPage":
						if (int.TryParse(childNode.InnerText, out tempInt))
							PositionPercentOfPage = tempInt;
						break;
					case "PositionPublication":
						if (int.TryParse(childNode.InnerText, out tempInt))
							PositionPublication = tempInt;
						break;
					case "PositionSection":
						if (int.TryParse(childNode.InnerText, out tempInt))
							PositionSection = tempInt;
						break;
					case "PositionReadership":
						if (int.TryParse(childNode.InnerText, out tempInt))
							PositionReadership = tempInt;
						break;
					case "PositionSquare":
						if (int.TryParse(childNode.InnerText, out tempInt))
							PositionSquare = tempInt;
						break;
				}
			}

			ShowComments &= EnableComments;
			ShowSection &= EnableSection;
			ShowMechanicals &= EnableMechanicals;
			ShowDimensions &= EnableDimensions;
			ShowDelivery &= EnableDelivery;
			ShowPublication &= EnablePublication;
			ShowSquare &= EnableSquare;
			ShowPageSize &= EnablePageSize;
			ShowPercentOfPage &= EnablePercentOfPage;
			ShowReadership &= EnableReadership;
			ShowDeadline &= EnableDeadline;
		}
	}

	public class SlideBulletsState
	{
		public SlideBulletsState()
		{
			EnableSlideBullets = true;
			EnableOnlyOnLastSlide = true;
			EnableTotalInserts = true;
			EnableTotalFinalCost = true;
			EnablePageSize = true;
			EnableDimensions = true;
			EnablePercentOfPage = true;
			EnableColumnInches = true;
			EnableTotalSquare = true;
			EnableAvgAdCost = true;
			EnableAvgFinalCost = true;
			EnableAvgPCI = true;
			EnableTotalColor = true;
			EnableDiscounts = true;
			EnableDelivery = true;
			EnableReadership = true;
			EnableSignature = true;

			ShowSlideBullets = true;
			ShowOnlyOnLastSlide = true;
			ShowTotalInserts = true;
			ShowTotalFinalCost = true;
			ShowPageSize = false;
			ShowDimensions = false;
			ShowPercentOfPage = false;
			ShowColumnInches = false;
			ShowTotalSquare = false;
			ShowAvgAdCost = false;
			ShowAvgFinalCost = false;
			ShowAvgPCI = false;
			ShowTotalColor = false;
			ShowDiscounts = false;
			ShowDelivery = false;
			ShowReadership = false;
			ShowSignature = true;
		}

		public bool EnableSlideBullets { get; set; }
		public bool EnableOnlyOnLastSlide { get; set; }
		public bool EnableTotalInserts { get; set; }
		public bool EnableTotalFinalCost { get; set; }
		public bool EnablePageSize { get; set; }
		public bool EnableDimensions { get; set; }
		public bool EnablePercentOfPage { get; set; }
		public bool EnableColumnInches { get; set; }
		public bool EnableTotalSquare { get; set; }
		public bool EnableAvgAdCost { get; set; }
		public bool EnableAvgFinalCost { get; set; }
		public bool EnableAvgPCI { get; set; }
		public bool EnableTotalColor { get; set; }
		public bool EnableDiscounts { get; set; }
		public bool EnableDelivery { get; set; }
		public bool EnableReadership { get; set; }
		public bool EnableSignature { get; set; }

		public bool ShowSlideBullets { get; set; }
		public bool ShowOnlyOnLastSlide { get; set; }
		public bool ShowTotalInserts { get; set; }
		public bool ShowTotalFinalCost { get; set; }
		public bool ShowPageSize { get; set; }
		public bool ShowDimensions { get; set; }
		public bool ShowPercentOfPage { get; set; }
		public bool ShowColumnInches { get; set; }
		public bool ShowTotalSquare { get; set; }
		public bool ShowAvgAdCost { get; set; }
		public bool ShowAvgFinalCost { get; set; }
		public bool ShowAvgPCI { get; set; }
		public bool ShowTotalColor { get; set; }
		public bool ShowDiscounts { get; set; }
		public bool ShowDelivery { get; set; }
		public bool ShowReadership { get; set; }
		public bool ShowSignature { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<EnableSlideBullets>" + EnableSlideBullets + @"</EnableSlideBullets>");
			result.AppendLine(@"<EnableOnlyOnLastSlide>" + EnableOnlyOnLastSlide + @"</EnableOnlyOnLastSlide>");
			result.AppendLine(@"<EnableTotalInserts>" + EnableTotalInserts + @"</EnableTotalInserts>");
			result.AppendLine(@"<EnableTotalFinalCost>" + EnableTotalFinalCost + @"</EnableTotalFinalCost>");
			result.AppendLine(@"<EnablePageSize>" + EnablePageSize + @"</EnablePageSize>");
			result.AppendLine(@"<EnableDimensions>" + EnableDimensions + @"</EnableDimensions>");
			result.AppendLine(@"<EnablePercentOfPage>" + EnablePercentOfPage + @"</EnablePercentOfPage>");
			result.AppendLine(@"<EnableColumnInches>" + EnableColumnInches + @"</EnableColumnInches>");
			result.AppendLine(@"<EnableTotalSquare>" + EnableTotalSquare + @"</EnableTotalSquare>");
			result.AppendLine(@"<EnableAvgAdCost>" + EnableAvgAdCost + @"</EnableAvgAdCost>");
			result.AppendLine(@"<EnableAvgFinalCost>" + EnableAvgFinalCost + @"</EnableAvgFinalCost>");
			result.AppendLine(@"<EnableAvgPCI>" + EnableAvgPCI + @"</EnableAvgPCI>");
			result.AppendLine(@"<EnableTotalColor>" + EnableTotalColor + @"</EnableTotalColor>");
			result.AppendLine(@"<EnableDiscounts>" + EnableDiscounts + @"</EnableDiscounts>");
			result.AppendLine(@"<EnableDelivery>" + EnableDelivery + @"</EnableDelivery>");
			result.AppendLine(@"<EnableReadership>" + EnableReadership + @"</EnableReadership>");
			result.AppendLine(@"<EnableSignature>" + EnableSignature + @"</EnableSignature>");

			result.AppendLine(@"<ShowSlideBullets>" + ShowSlideBullets + @"</ShowSlideBullets>");
			result.AppendLine(@"<ShowOnlyOnLastSlide>" + ShowOnlyOnLastSlide + @"</ShowOnlyOnLastSlide>");
			result.AppendLine(@"<ShowTotalInserts>" + ShowTotalInserts + @"</ShowTotalInserts>");
			result.AppendLine(@"<ShowTotalFinalCost>" + ShowTotalFinalCost + @"</ShowTotalFinalCost>");
			result.AppendLine(@"<ShowPageSize>" + ShowPageSize + @"</ShowPageSize>");
			result.AppendLine(@"<ShowDimensions>" + ShowDimensions + @"</ShowDimensions>");
			result.AppendLine(@"<ShowPercentOfPage>" + ShowPercentOfPage + @"</ShowPercentOfPage>");
			result.AppendLine(@"<ShowColumnInches>" + ShowColumnInches + @"</ShowColumnInches>");
			result.AppendLine(@"<ShowTotalSquare>" + ShowTotalSquare + @"</ShowTotalSquare>");
			result.AppendLine(@"<ShowAvgAdCost>" + ShowAvgAdCost + @"</ShowAvgAdCost>");
			result.AppendLine(@"<ShowAvgFinalCost>" + ShowAvgFinalCost + @"</ShowAvgFinalCost>");
			result.AppendLine(@"<ShowAvgPCI>" + ShowAvgPCI + @"</ShowAvgPCI>");
			result.AppendLine(@"<ShowTotalColor>" + ShowTotalColor + @"</ShowTotalColor>");
			result.AppendLine(@"<ShowDiscounts>" + ShowDiscounts + @"</ShowDiscounts>");
			result.AppendLine(@"<ShowDelivery>" + ShowDelivery + @"</ShowDelivery>");
			result.AppendLine(@"<ShowReadership>" + ShowReadership + @"</ShowReadership>");
			result.AppendLine(@"<ShowSignature>" + ShowSignature + @"</ShowSignature>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool = false;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "EnableSlideBullets":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableSlideBullets = tempBool;
						break;
					case "EnableOnlyOnLastSlide":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableOnlyOnLastSlide = tempBool;
						break;
					case "EnableTotalInserts":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableTotalInserts = tempBool;
						break;
					case "EnableTotalFinalCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableTotalFinalCost = tempBool;
						break;
					case "EnablePageSize":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnablePageSize = tempBool;
						break;
					case "EnableDimensions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDimensions = tempBool;
						break;
					case "EnablePercentOfPage":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnablePercentOfPage = tempBool;
						break;
					case "EnableColumnInches":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableColumnInches = tempBool;
						break;
					case "EnableTotalSquare":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableTotalSquare = tempBool;
						break;
					case "EnableAvgAdCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableAvgAdCost = tempBool;
						break;
					case "EnableAvgFinalCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableAvgFinalCost = tempBool;
						break;
					case "EnableAvgPCI":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableAvgPCI = tempBool;
						break;
					case "EnableTotalColor":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableTotalColor = tempBool;
						break;
					case "EnableDiscounts":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDiscounts = tempBool;
						break;
					case "EnableDelivery":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDelivery = tempBool;
						break;
					case "EnableReadership":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableReadership = tempBool;
						break;
					case "EnableSignature":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableSignature = tempBool;
						break;

					case "ShowSlideBullets":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSlideBullets = tempBool;
						break;
					case "ShowOnlyOnLastSlide":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowOnlyOnLastSlide = tempBool;
						break;
					case "ShowTotalInserts":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalInserts = tempBool;
						break;
					case "ShowTotalFinalCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalFinalCost = tempBool;
						break;
					case "ShowPageSize":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPageSize = tempBool;
						break;
					case "ShowDimensions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDimensions = tempBool;
						break;
					case "ShowPercentOfPage":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPercentOfPage = tempBool;
						break;
					case "ShowColumnInches":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowColumnInches = tempBool;
						break;
					case "ShowTotalSquare":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalSquare = tempBool;
						break;
					case "ShowAvgAdCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAvgAdCost = tempBool;
						break;
					case "ShowAvgFinalCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAvgFinalCost = tempBool;
						break;
					case "ShowAvgPCI":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAvgPCI = tempBool;
						break;
					case "ShowTotalColor":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalColor = tempBool;
						break;
					case "ShowDiscounts":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDiscounts = tempBool;
						break;
					case "ShowDelivery":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDelivery = tempBool;
						break;
					case "ShowReadership":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowReadership = tempBool;
						break;
					case "ShowSignature":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSignature = tempBool;
						break;
				}
			}
			ShowSlideBullets &= EnableSlideBullets;
			ShowOnlyOnLastSlide &= EnableOnlyOnLastSlide;
			ShowTotalInserts &= EnableTotalInserts;
			ShowTotalFinalCost &= EnableTotalFinalCost;
			ShowPageSize &= EnablePageSize;
			ShowDimensions &= EnableDimensions;
			ShowPercentOfPage &= EnablePercentOfPage;
			ShowColumnInches &= EnableColumnInches;
			ShowTotalSquare &= EnableTotalSquare;
			ShowAvgAdCost &= EnableAvgAdCost;
			ShowAvgFinalCost &= EnableAvgFinalCost;
			ShowAvgPCI &= EnableAvgPCI;
			ShowTotalColor &= EnableTotalColor;
			ShowDiscounts &= EnableDiscounts;
			ShowDelivery &= EnableDelivery;
			ShowReadership &= EnableReadership;
			ShowSignature &= EnableSignature;
		}
	}

	public class SlideHeaderState
	{
		public SlideHeaderState()
		{
			EnableSlideInfo = true;
			EnableSlideHeader = true;
			EnableAdvertiser = true;
			EnableDecisionMaker = true;
			EnablePresentationDate = true;
			EnableFlightDates = true;
			EnableName = true;
			EnableLogo1 = true;
			EnableLogo2 = false;
			EnableLogo3 = false;
			EnableLogo4 = false;

			ShowSlideInfo = true;
			ShowSlideHeader = true;
			ShowAdvertiser = true;
			ShowDecisionMaker = false;
			ShowPresentationDate = false;
			ShowFlightDates = false;
			ShowName = true;
			ShowLogo1 = true;
			ShowLogo2 = false;
			ShowLogo3 = false;
			ShowLogo4 = false;
		}

		public bool EnableSlideInfo { get; set; }
		public bool EnableSlideHeader { get; set; }
		public bool EnableAdvertiser { get; set; }
		public bool EnableDecisionMaker { get; set; }
		public bool EnablePresentationDate { get; set; }
		public bool EnableFlightDates { get; set; }
		public bool EnableName { get; set; }
		public bool EnableLogo1 { get; set; }
		public bool EnableLogo2 { get; set; }
		public bool EnableLogo3 { get; set; }
		public bool EnableLogo4 { get; set; }

		public bool ShowSlideInfo { get; set; }
		public bool ShowSlideHeader { get; set; }
		public bool ShowAdvertiser { get; set; }
		public bool ShowDecisionMaker { get; set; }
		public bool ShowPresentationDate { get; set; }
		public bool ShowFlightDates { get; set; }
		public bool ShowName { get; set; }
		public bool ShowLogo1 { get; set; }
		public bool ShowLogo2 { get; set; }
		public bool ShowLogo3 { get; set; }
		public bool ShowLogo4 { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<EnableSlideInfo>" + EnableSlideInfo + @"</EnableSlideInfo>");
			result.AppendLine(@"<EnableSlideHeader>" + EnableSlideHeader + @"</EnableSlideHeader>");
			result.AppendLine(@"<EnableAdvertiser>" + EnableAdvertiser + @"</EnableAdvertiser>");
			result.AppendLine(@"<EnableDecisionMaker>" + EnableDecisionMaker + @"</EnableDecisionMaker>");
			result.AppendLine(@"<EnablePresentationDate>" + EnablePresentationDate + @"</EnablePresentationDate>");
			result.AppendLine(@"<EnableFlightDates>" + EnableFlightDates + @"</EnableFlightDates>");
			result.AppendLine(@"<EnableName>" + EnableName + @"</EnableName>");
			result.AppendLine(@"<EnableLogo1>" + EnableLogo1 + @"</EnableLogo1>");
			result.AppendLine(@"<EnableLogo2>" + EnableLogo2 + @"</EnableLogo2>");
			result.AppendLine(@"<EnableLogo3>" + EnableLogo3 + @"</EnableLogo3>");
			result.AppendLine(@"<EnableLogo4>" + EnableLogo4 + @"</EnableLogo4>");

			result.AppendLine(@"<ShowSlideInfo>" + ShowSlideInfo + @"</ShowSlideInfo>");
			result.AppendLine(@"<ShowSlideHeader>" + ShowSlideHeader + @"</ShowSlideHeader>");
			result.AppendLine(@"<ShowAdvertiser>" + ShowAdvertiser + @"</ShowAdvertiser>");
			result.AppendLine(@"<ShowDecisionMaker>" + ShowDecisionMaker + @"</ShowDecisionMaker>");
			result.AppendLine(@"<ShowPresentationDate>" + ShowPresentationDate + @"</ShowPresentationDate>");
			result.AppendLine(@"<ShowFlightDates>" + ShowFlightDates + @"</ShowFlightDates>");
			result.AppendLine(@"<ShowName>" + ShowName + @"</ShowName>");
			result.AppendLine(@"<ShowLogo1>" + ShowLogo1 + @"</ShowLogo1>");
			result.AppendLine(@"<ShowLogo2>" + ShowLogo2 + @"</ShowLogo2>");
			result.AppendLine(@"<ShowLogo3>" + ShowLogo3 + @"</ShowLogo3>");
			result.AppendLine(@"<ShowLogo4>" + ShowLogo4 + @"</ShowLogo4>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool = false;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "EnableSlideInfo":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableSlideInfo = tempBool;
						break;
					case "EnableSlideHeader":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableSlideHeader = tempBool;
						break;
					case "EnableAdvertiser":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableAdvertiser = tempBool;
						break;
					case "EnableDecisionMaker":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableDecisionMaker = tempBool;
						break;
					case "EnablePresentationDate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnablePresentationDate = tempBool;
						break;
					case "EnableFlightDates":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableFlightDates = tempBool;
						break;
					case "EnableName":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableName = tempBool;
						break;
					case "EnableLogo1":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableLogo1 = tempBool;
						break;
					case "EnableLogo2":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableLogo2 = tempBool;
						break;
					case "EnableLogo3":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableLogo3 = tempBool;
						break;
					case "EnableLogo4":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableLogo4 = tempBool;
						break;

					case "ShowSlideInfo":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSlideInfo = tempBool;
						break;
					case "ShowSlideHeader":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSlideHeader = tempBool;
						break;
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
					case "ShowName":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowName = tempBool;
						break;
					case "ShowLogo1":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLogo1 = tempBool;
						break;
					case "ShowLogo2":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLogo2 = tempBool;
						break;
					case "ShowLogo3":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLogo3 = tempBool;
						break;
					case "ShowLogo4":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLogo4 = tempBool;
						break;
				}
			}
		}
	}

	public class CalendarViewSettings
	{
		public CalendarViewSettings()
		{
			EnableSection = true;
			EnableCost = true;
			EnableColor = true;
			EnableAbbreviationOnly = true;
			EnableAdSize = true;
			EnablePageSize = true;
			EnablePercentOfPage = true;
			EnableBigDate = true;

			ShowSection = false;
			ShowCost = true;
			ShowColor = false;
			ShowAbbreviationOnly = false;
			ShowAdSize = false;
			ShowPageSize = false;
			ShowPercentOfPage = false;
			ShowBigDate = true;

			EnableLogo = true;
			EnableTotalCost = true;
			EnableComments = true;
			EnableTotalAds = true;
			EnableActiveDays = true;

			ShowLogo = true;
			ShowTotalCost = false;
			ShowComments = false;
			ShowTotalAds = false;
			ShowActiveDays = false;

			EnableGray = true;
			EnableBlack = true;
			EnableBlue = true;
			EnableTeal = true;
			EnableOrange = true;
			EnableGreen = true;
			SlideColor = "gray";
		}

		public bool EnableSection { get; set; }
		public bool EnableCost { get; set; }
		public bool EnableColor { get; set; }
		public bool EnableAbbreviationOnly { get; set; }
		public bool EnableAdSize { get; set; }
		public bool EnablePageSize { get; set; }
		public bool EnablePercentOfPage { get; set; }
		public bool EnableBigDate { get; set; }

		public bool ShowSection { get; set; }
		public bool ShowCost { get; set; }
		public bool ShowColor { get; set; }
		public bool ShowAbbreviationOnly { get; set; }
		public bool ShowAdSize { get; set; }
		public bool ShowPageSize { get; set; }
		public bool ShowPercentOfPage { get; set; }
		public bool ShowBigDate { get; set; }

		public bool EnableLogo { get; set; }
		public bool EnableTotalCost { get; set; }
		public bool EnableComments { get; set; }
		public bool EnableTotalAds { get; set; }
		public bool EnableActiveDays { get; set; }

		public bool ShowLogo { get; set; }
		public bool ShowTotalCost { get; set; }
		public bool ShowComments { get; set; }
		public bool ShowTotalAds { get; set; }
		public bool ShowActiveDays { get; set; }

		public bool EnableGray { get; set; }
		public bool EnableBlack { get; set; }
		public bool EnableBlue { get; set; }
		public bool EnableTeal { get; set; }
		public bool EnableOrange { get; set; }
		public bool EnableGreen { get; set; }
		public string SlideColor { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<EnableSection>" + EnableSection + @"</EnableSection>");
			result.AppendLine(@"<EnableCost>" + EnableCost + @"</EnableCost>");
			result.AppendLine(@"<EnableColor>" + EnableColor + @"</EnableColor>");
			result.AppendLine(@"<EnableAbbreviationOnly>" + EnableAbbreviationOnly + @"</EnableAbbreviationOnly>");
			result.AppendLine(@"<EnableAdSize>" + EnableAdSize + @"</EnableAdSize>");
			result.AppendLine(@"<EnablePageSize>" + EnablePageSize + @"</EnablePageSize>");
			result.AppendLine(@"<EnablePercentOfPage>" + EnablePercentOfPage + @"</EnablePercentOfPage>");
			result.AppendLine(@"<EnableBigDate>" + EnableBigDate + @"</EnableBigDate>");

			result.AppendLine(@"<ShowSection>" + ShowSection + @"</ShowSection>");
			result.AppendLine(@"<ShowCost>" + ShowCost + @"</ShowCost>");
			result.AppendLine(@"<ShowColor>" + ShowColor + @"</ShowColor>");
			result.AppendLine(@"<ShowAbbreviationOnly>" + ShowAbbreviationOnly + @"</ShowAbbreviationOnly>");
			result.AppendLine(@"<ShowAdSize>" + ShowAdSize + @"</ShowAdSize>");
			result.AppendLine(@"<ShowPageSize>" + ShowPageSize + @"</ShowPageSize>");
			result.AppendLine(@"<ShowPercentOfPage>" + ShowPercentOfPage + @"</ShowPercentOfPage>");
			result.AppendLine(@"<ShowBigDate>" + ShowBigDate + @"</ShowBigDate>");

			result.AppendLine(@"<EnableLogo>" + EnableLogo + @"</EnableLogo>");
			result.AppendLine(@"<EnableTotalCost>" + EnableTotalCost + @"</EnableTotalCost>");
			result.AppendLine(@"<EnableComments>" + EnableComments + @"</EnableComments>");
			result.AppendLine(@"<EnableTotalAds>" + EnableTotalAds + @"</EnableTotalAds>");
			result.AppendLine(@"<EnableActiveDays>" + EnableActiveDays + @"</EnableActiveDays>");

			result.AppendLine(@"<ShowLogo>" + ShowLogo + @"</ShowLogo>");
			result.AppendLine(@"<ShowTotalCost>" + ShowTotalCost + @"</ShowTotalCost>");
			result.AppendLine(@"<ShowComments>" + ShowComments + @"</ShowComments>");
			result.AppendLine(@"<ShowTotalAds>" + ShowTotalAds + @"</ShowTotalAds>");
			result.AppendLine(@"<ShowActiveDays>" + ShowActiveDays + @"</ShowActiveDays>");

			result.AppendLine(@"<EnableGray>" + EnableGray + @"</EnableGray>");
			result.AppendLine(@"<EnableBlack>" + EnableBlack + @"</EnableBlack>");
			result.AppendLine(@"<EnableBlue>" + EnableBlue + @"</EnableBlue>");
			result.AppendLine(@"<EnableTeal>" + EnableTeal + @"</EnableTeal>");
			result.AppendLine(@"<EnableOrange>" + EnableOrange + @"</EnableOrange>");
			result.AppendLine(@"<EnableGreen>" + EnableGreen + @"</EnableGreen>");
			result.AppendLine(@"<SlideColor>" + SlideColor.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideColor>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				bool tempBool;
				switch (childNode.Name)
				{
					case "EnableSection":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableSection = tempBool;
						break;
					case "EnableCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableCost = tempBool;
						break;
					case "EnableColor":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableColor = tempBool;
						break;
					case "EnableAbbreviationOnly":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableAbbreviationOnly = tempBool;
						break;
					case "EnableAdSize":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableAdSize = tempBool;
						break;
					case "EnablePageSize":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnablePageSize = tempBool;
						break;
					case "EnablePercentOfPage":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnablePercentOfPage = tempBool;
						break;
					case "EnableBigDate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableBigDate = tempBool;
						break;

					case "ShowSection":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSection = tempBool;
						break;
					case "ShowCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowCost = tempBool;
						break;
					case "ShowColor":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowColor = tempBool;
						break;
					case "ShowAbbreviationOnly":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAbbreviationOnly = tempBool;
						break;
					case "ShowAdSize":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAdSize = tempBool;
						break;
					case "ShowPageSize":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPageSize = tempBool;
						break;
					case "ShowPercentOfPage":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPercentOfPage = tempBool;
						break;
					case "ShowBigDate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowBigDate = tempBool;
						break;

					case "EnableLogo":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableLogo = tempBool;
						break;
					case "EnableTotalCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableTotalCost = tempBool;
						break;
					case "EnableComments":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableComments = tempBool;
						break;
					case "EnableTotalAds":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableTotalAds = tempBool;
						break;
					case "EnableActiveDays":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableActiveDays = tempBool;
						break;

					case "ShowLogo":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLogo = tempBool;
						break;
					case "ShowTotalCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalCost = tempBool;
						break;
					case "ShowComments":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowComments = tempBool;
						break;
					case "ShowTotalAds":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalAds = tempBool;
						break;
					case "ShowActiveDays":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowActiveDays = tempBool;
						break;

					case "EnableGray":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableGray = tempBool;
						break;
					case "EnableBlack":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableBlack = tempBool;
						break;
					case "EnableBlue":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableBlue = tempBool;
						break;
					case "EnableTeal":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableTeal = tempBool;
						break;
					case "EnableOrange":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableOrange = tempBool;
						break;
					case "EnableGreen":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableGreen = tempBool;
						break;
					case "SlideColor":
						SlideColor = childNode.InnerText;
						break;
				}
			}

			ShowSection &= EnableSection;
			ShowCost &= EnableCost;
			ShowColor &= EnableColor;
			ShowAbbreviationOnly &= EnableAbbreviationOnly;
			ShowAdSize &= EnableAdSize;
			ShowPageSize &= EnablePageSize;
			ShowPercentOfPage &= EnablePercentOfPage;
			ShowBigDate &= EnableBigDate;

			ShowLogo &= EnableLogo;
			ShowTotalCost &= EnableTotalCost;
			ShowComments &= EnableComments;
			ShowTotalAds &= EnableTotalAds;
			ShowActiveDays &= EnableActiveDays;
		}
	}
}