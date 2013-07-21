using System;
using System.Text;
using System.Xml;

namespace CommandCentral.CommonClasses
{
    public class HomeViewSettings
    {
        public bool EnableAccountNumber { get; set; }
        public bool EnableSalesStrategyPerson { get; set; }
        public bool EnableSalesStrategyEmail { get; set; }
        public bool EnableSalesStrategyFax { get; set; }
        public bool EnableDelivery { get; set; }
        public bool EnableReadership { get; set; }
        public bool EnableLogo { get; set; }
        public bool EnableCode { get; set; }

        public bool ShowAccountNumber { get; set; }
        public bool ShowSalesStrategyPerson { get; set; }
        public bool ShowSalesStrategyEmail { get; set; }
        public bool ShowSalesStrategyFax { get; set; }
        public bool ShowDelivery { get; set; }
        public bool ShowReadership { get; set; }
        public bool ShowLogo { get; set; }
        public bool ShowCode { get; set; }

        public HomeViewSettings()
        {
            this.EnableAccountNumber = true;
            this.EnableSalesStrategyPerson = true;
            this.EnableSalesStrategyEmail = true;
            this.EnableSalesStrategyFax = true;
            this.EnableDelivery = true;
            this.EnableReadership = true;
            this.EnableLogo = true;
            this.EnableCode = true;

            this.ShowAccountNumber = false;
            this.ShowSalesStrategyPerson = true;
            this.ShowSalesStrategyEmail = false;
            this.ShowSalesStrategyFax = false;
            this.ShowDelivery = false;
            this.ShowReadership = false;
            this.ShowLogo = true;
            this.ShowCode = true;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<EnableAccountNumber>" + this.EnableAccountNumber + @"</EnableAccountNumber>");
            result.AppendLine(@"<EnableSalesStrategyPerson>" + this.EnableSalesStrategyPerson + @"</EnableSalesStrategyPerson>");
            result.AppendLine(@"<EnableSalesStrategyEmail>" + this.EnableSalesStrategyEmail + @"</EnableSalesStrategyEmail>");
            result.AppendLine(@"<EnableSalesStrategyFax>" + this.EnableSalesStrategyFax + @"</EnableSalesStrategyFax>");
            result.AppendLine(@"<EnableCode>" + this.EnableCode + @"</EnableCode>");
            result.AppendLine(@"<EnableDelivery>" + this.EnableDelivery + @"</EnableDelivery>");
            result.AppendLine(@"<EnableLogo>" + this.EnableLogo + @"</EnableLogo>");
            result.AppendLine(@"<EnableReadership>" + this.EnableReadership + @"</EnableReadership>");

            result.AppendLine(@"<ShowAccountNumber>" + this.ShowAccountNumber + @"</ShowAccountNumber>");
            result.AppendLine(@"<ShowSalesStrategyPerson>" + this.ShowSalesStrategyPerson + @"</ShowSalesStrategyPerson>");
            result.AppendLine(@"<ShowSalesStrategyEmail>" + this.ShowSalesStrategyEmail + @"</ShowSalesStrategyEmail>");
            result.AppendLine(@"<ShowSalesStrategyFax>" + this.ShowSalesStrategyFax + @"</ShowSalesStrategyFax>");
            result.AppendLine(@"<ShowCode>" + this.ShowCode + @"</ShowCode>");
            result.AppendLine(@"<ShowDelivery>" + this.ShowDelivery + @"</ShowDelivery>");
            result.AppendLine(@"<ShowLogo>" + this.ShowLogo + @"</ShowLogo>");
            result.AppendLine(@"<ShowReadership>" + this.ShowReadership + @"</ShowReadership>");

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
                            this.EnableAccountNumber = tempBool;
                        break;
                    case "EnableSalesStrategyPerson":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableSalesStrategyPerson = tempBool;
                        break;
                    case "EnableSalesStrategyEmail":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableSalesStrategyEmail = tempBool;
                        break;
                    case "EnableSalesStrategyFax":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableSalesStrategyFax = tempBool;
                        break;
                    case "EnableCode":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableCode = tempBool;
                        break;
                    case "EnableDelivery":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableDelivery = tempBool;
                        break;
                    case "EnableLogo":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableLogo = tempBool;
                        break;
                    case "EnableReadership":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableReadership = tempBool;
                        break;

                    case "ShowAccountNumber":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAccountNumber = tempBool;
                        break;
                    case "ShowSalesStrategyPerson":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSalesStrategyPerson = tempBool;
                        break;
                    case "ShowSalesStrategyEmail":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSalesStrategyEmail = tempBool;
                        break;
                    case "ShowSalesStrategyFax":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSalesStrategyFax = tempBool;
                        break;
                    case "ShowCode":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowCode = tempBool;
                        break;
                    case "ShowDelivery":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDelivery = tempBool;
                        break;
                    case "ShowLogo":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowLogo = tempBool;
                        break;
                    case "ShowReadership":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowReadership = tempBool;
                        break;
                }
            }

            this.ShowAccountNumber &= this.EnableAccountNumber;
            this.ShowSalesStrategyPerson &= this.EnableSalesStrategyPerson;
            this.ShowSalesStrategyEmail &= this.EnableSalesStrategyEmail;
            this.ShowSalesStrategyFax &= this.EnableSalesStrategyFax;
            this.ShowDelivery &= this.EnableDelivery;
            this.ShowReadership &= this.EnableReadership;
            this.ShowLogo &= this.EnableLogo;
            this.ShowCode &= this.EnableCode;
        }
    }

    public class PrintScheduleViewSettings
    {
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

        public PrintScheduleViewSettings()
        {
            this.EnablePCI = true;
            this.EnableFlat = true;
            this.EnableShare = true;
            this.EnableBlackWhite = true;
            this.EnableSpotColor = true;
            this.EnableFullColor = true;
            this.EnableCostPerAd = true;
            this.EnablePercentOfAd = true;
            this.EnableColorIncluded = true;
            this.EnableCostPerInch = true;

            this.DefaultPCI = false;
            this.DefaultFlat = false;
            this.DefaultShare = true;
            this.DefaultBlackWhite = false;
            this.DefaultSpotColor = false;
            this.DefaultFullColor = true;
            this.DefaultCostPerAd = false;
            this.DefaultPercentOfAd = false;
            this.DefaultColorIncluded = true;
            this.DefaultCostPerInch = false;
        }

        private void LoadDefaultSettings()
        {
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<EnablePCI>" + this.EnablePCI + @"</EnablePCI>");
            result.AppendLine(@"<EnableFlat>" + this.EnableFlat + @"</EnableFlat>");
            result.AppendLine(@"<EnableShare>" + this.EnableShare + @"</EnableShare>");
            result.AppendLine(@"<EnableBlackWhite>" + this.EnableBlackWhite + @"</EnableBlackWhite>");
            result.AppendLine(@"<EnableSpotColor>" + this.EnableSpotColor + @"</EnableSpotColor>");
            result.AppendLine(@"<EnableFullColor>" + this.EnableFullColor + @"</EnableFullColor>");
            result.AppendLine(@"<EnableCostPerAd>" + this.EnableCostPerAd + @"</EnableCostPerAd>");
            result.AppendLine(@"<EnablePercentOfAd>" + this.EnablePercentOfAd + @"</EnablePercentOfAd>");
            result.AppendLine(@"<EnableColorIncluded>" + this.EnableColorIncluded + @"</EnableColorIncluded>");
            result.AppendLine(@"<EnableCostPerInch>" + this.EnableCostPerInch + @"</EnableCostPerInch>");

            result.AppendLine(@"<DefaultPCI>" + this.DefaultPCI + @"</DefaultPCI>");
            result.AppendLine(@"<DefaultFlat>" + this.DefaultFlat + @"</DefaultFlat>");
            result.AppendLine(@"<DefaultShare>" + this.DefaultShare + @"</DefaultShare>");
            result.AppendLine(@"<DefaultBlackWhite>" + this.DefaultBlackWhite + @"</DefaultBlackWhite>");
            result.AppendLine(@"<DefaultSpotColor>" + this.DefaultSpotColor + @"</DefaultSpotColor>");
            result.AppendLine(@"<DefaultFullColor>" + this.DefaultFullColor + @"</DefaultFullColor>");
            result.AppendLine(@"<DefaultCostPerAd>" + this.DefaultCostPerAd + @"</DefaultCostPerAd>");
            result.AppendLine(@"<DefaultPercentOfAd>" + this.DefaultPercentOfAd + @"</DefaultPercentOfAd>");
            result.AppendLine(@"<DefaultColorIncluded>" + this.DefaultColorIncluded + @"</DefaultColorIncluded>");
            result.AppendLine(@"<DefaultCostPerInch>" + this.DefaultCostPerInch + @"</DefaultCostPerInch>");

            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool = false;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "EnablePCI":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnablePCI = tempBool;
                        break;
                    case "EnableFlat":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableFlat = tempBool;
                        break;
                    case "EnableShare":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableShare = tempBool;
                        break;
                    case "EnableBlackWhite":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableBlackWhite = tempBool;
                        break;
                    case "EnableSpotColor":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableSpotColor = tempBool;
                        break;
                    case "EnableFullColor":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableFullColor = tempBool;
                        break;
                    case "EnableCostPerAd":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableCostPerAd = tempBool;
                        break;
                    case "EnablePercentOfAd":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnablePercentOfAd = tempBool;
                        break;
                    case "EnableColorIncluded":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableColorIncluded = tempBool;
                        break;
                    case "EnableCostPerInch":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableCostPerInch = tempBool;
                        break;

                    case "DefaultPCI":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.DefaultPCI = tempBool;
                        break;
                    case "DefaultFlat":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.DefaultFlat = tempBool;
                        break;
                    case "DefaultShare":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.DefaultShare = tempBool;
                        break;
                    case "DefaultBlackWhite":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.DefaultBlackWhite = tempBool;
                        break;
                    case "DefaultSpotColor":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.DefaultSpotColor = tempBool;
                        break;
                    case "DefaultFullColor":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.DefaultFullColor = tempBool;
                        break;
                    case "DefaultCostPerAd":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.DefaultCostPerAd = tempBool;
                        break;
                    case "DefaultPercentOfAd":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.DefaultPercentOfAd = tempBool;
                        break;
                    case "DefaultColorIncluded":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.DefaultColorIncluded = tempBool;
                        break;
                    case "DefaultCostPerInch":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.DefaultCostPerInch = tempBool;
                        break;
                }
            }

            this.DefaultPCI &= this.EnablePCI;
            this.DefaultFlat &= this.EnableFlat;
            this.DefaultShare &= this.EnableShare;
            this.DefaultBlackWhite &= this.EnableBlackWhite;
            this.DefaultSpotColor &= this.EnableSpotColor;
            this.DefaultFullColor &= this.EnableFullColor;
            this.DefaultCostPerAd &= this.EnableCostPerAd;
            this.DefaultPercentOfAd &= this.EnablePercentOfAd;
            this.DefaultColorIncluded &= this.EnableColorIncluded;
            this.DefaultCostPerInch &= this.EnableCostPerInch;
        }
    }

    public class PublicationBasicOverviewSettings
    {
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

        public PublicationBasicOverviewSettings()
        {
            this.ShowName = true;
            this.ShowLogo = true;
            this.ShowFlightDates = true;
            this.ShowSlideHeader = true;
            this.ShowPresentationDate = true;
            this.ShowAdvertiser = true;
            this.ShowDecisionMaker = true;

            this.EnableDimensions = true;
            this.EnablePageSize = true;
            this.EnablePercentOfPage = true;
            this.EnableSquare = true;
            this.EnableColor = true;
            this.ShowAdSizeDetails = true;
            this.ShowDimensions = false;
            this.ShowPageSize = true;
            this.ShowPercentOfPage = true;
            this.ShowSquare = true;
            this.ShowColor = true;
            this.ShowMechanicals = true;

            this.EnableTotalInserts = true;
            this.EnableTotalSquare = true;
            this.ShowTotalDetails = true;
            this.ShowTotalInserts = true;
            this.ShowTotalSquare = true;

            this.EnableAvgAdCost = true;
            this.EnableAvgPCI = true;
            this.EnableDiscounts = true;
            this.EnableInvestment = true;
            this.ShowInvestmentDetails = true;
            this.ShowAvgAdCost = true;
            this.ShowAvgPCI = true;
            this.ShowDiscounts = false;
            this.ShowInvestment = true;

            this.EnableFlightDates2 = true;
            this.EnableDates = true;
            this.EnableComments = true;
            this.ShowDateDetails = true;
            this.ShowFlightDates2 = true;
            this.ShowDates = true;
            this.ShowComments = false;

            this.Comments = string.Empty;
            this.SlideHeader = string.Empty;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<ShowName>" + this.ShowName + @"</ShowName>");
            result.AppendLine(@"<ShowLogo>" + this.ShowLogo + @"</ShowLogo>");
            result.AppendLine(@"<ShowFlightDates>" + this.ShowFlightDates + @"</ShowFlightDates>");
            result.AppendLine(@"<ShowAdvertiser>" + this.ShowAdvertiser + @"</ShowAdvertiser>");
            result.AppendLine(@"<ShowDecisionMaker>" + this.ShowDecisionMaker + @"</ShowDecisionMaker>");
            result.AppendLine(@"<ShowPresentationDate>" + this.ShowPresentationDate + @"</ShowPresentationDate>");
            result.AppendLine(@"<ShowSlideHeader>" + this.ShowSlideHeader + @"</ShowSlideHeader>");
            result.AppendLine(@"<SlideHeader>" + this.SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");

            result.AppendLine(@"<EnableDimensions>" + this.EnableDimensions + @"</EnableDimensions>");
            result.AppendLine(@"<EnablePageSize>" + this.EnablePageSize + @"</EnablePageSize>");
            result.AppendLine(@"<EnablePercentOfPage>" + this.EnablePercentOfPage + @"</EnablePercentOfPage>");
            result.AppendLine(@"<EnableSquare>" + this.EnableSquare + @"</EnableSquare>");
            result.AppendLine(@"<EnableColor>" + this.EnableColor + @"</EnableColor>");
            result.AppendLine(@"<ShowAdSizeDetails>" + this.ShowAdSizeDetails + @"</ShowAdSizeDetails>");
            result.AppendLine(@"<ShowDimensions>" + this.ShowDimensions + @"</ShowDimensions>");
            result.AppendLine(@"<ShowPageSize>" + this.ShowPageSize + @"</ShowPageSize>");
            result.AppendLine(@"<ShowPercentOfPage>" + this.ShowPercentOfPage + @"</ShowPercentOfPage>");
            result.AppendLine(@"<ShowSquare>" + this.ShowSquare + @"</ShowSquare>");
            result.AppendLine(@"<ShowColor>" + this.ShowColor + @"</ShowColor>");
            result.AppendLine(@"<ShowMechanicals>" + this.ShowMechanicals + @"</ShowMechanicals>");

            result.AppendLine(@"<EnableTotalInserts>" + this.EnableTotalInserts + @"</EnableTotalInserts>");
            result.AppendLine(@"<EnableTotalSquare>" + this.EnableTotalSquare + @"</EnableTotalSquare>");
            result.AppendLine(@"<ShowTotalDetails>" + this.ShowTotalDetails + @"</ShowTotalDetails>");
            result.AppendLine(@"<ShowTotalInserts>" + this.ShowTotalInserts + @"</ShowTotalInserts>");
            result.AppendLine(@"<ShowTotalSquare>" + this.ShowTotalSquare + @"</ShowTotalSquare>");

            result.AppendLine(@"<EnableAvgAdCost>" + this.EnableAvgAdCost + @"</EnableAvgAdCost>");
            result.AppendLine(@"<EnableAvgPCI>" + this.EnableAvgPCI + @"</EnableAvgPCI>");
            result.AppendLine(@"<EnableDiscounts>" + this.EnableDiscounts + @"</EnableDiscounts>");
            result.AppendLine(@"<EnableInvestment>" + this.EnableInvestment + @"</EnableInvestment>");
            result.AppendLine(@"<ShowInvestmentDetails>" + this.ShowInvestmentDetails + @"</ShowInvestmentDetails>");
            result.AppendLine(@"<ShowAvgAdCost>" + this.ShowAvgAdCost + @"</ShowAvgAdCost>");
            result.AppendLine(@"<ShowAvgPCI>" + this.ShowAvgPCI + @"</ShowAvgPCI>");
            result.AppendLine(@"<ShowDiscounts>" + this.ShowDiscounts + @"</ShowDiscounts>");
            result.AppendLine(@"<ShowInvestment>" + this.ShowInvestment + @"</ShowInvestment>");

            result.AppendLine(@"<EnableFlightDates2>" + this.EnableFlightDates2 + @"</EnableFlightDates2>");
            result.AppendLine(@"<EnableDates>" + this.EnableDates + @"</EnableDates>");
            result.AppendLine(@"<EnableComments>" + this.EnableComments + @"</EnableComments>");
            result.AppendLine(@"<ShowDateDetails>" + this.ShowDateDetails + @"</ShowDateDetails>");
            result.AppendLine(@"<ShowFlightDates2>" + this.ShowFlightDates2 + @"</ShowFlightDates2>");
            result.AppendLine(@"<ShowDates>" + this.ShowDates + @"</ShowDates>");
            result.AppendLine(@"<ShowComments>" + this.ShowComments + @"</ShowComments>");
            result.AppendLine(@"<Comments>" + this.Comments.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Comments>");

            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool = false; ;
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "ShowName":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowName = tempBool;
                        break;
                    case "ShowLogo":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowLogo = tempBool;
                        break;
                    case "ShowFlightDates":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowFlightDates = tempBool;
                        break;
                    case "ShowAdvertiser":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAdvertiser = tempBool;
                        break;
                    case "ShowDecisionMaker":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDecisionMaker = tempBool;
                        break;
                    case "ShowPresentationDate":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPresentationDate = tempBool;
                        break;
                    case "ShowSlideHeader":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSlideHeader = tempBool;
                        break;
                    case "SlideHeader":
                        this.SlideHeader = childNode.InnerText;
                        break;

                    case "EnablePageSize":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnablePageSize = tempBool;
                        break;
                    case "EnableDimensions":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableDimensions = tempBool;
                        break;
                    case "EnableSquare":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableSquare = tempBool;
                        break;
                    case "EnableColor":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableColor = tempBool;
                        break;
                    case "EnablePercentOfPage":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnablePercentOfPage = tempBool;
                        break;
                    case "ShowAdSizeDetails":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAdSizeDetails = tempBool;
                        break;
                    case "ShowPageSize":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPageSize = tempBool;
                        break;
                    case "ShowDimensions":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDimensions = tempBool;
                        break;
                    case "ShowSquare":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSquare = tempBool;
                        break;
                    case "ShowColor":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowColor = tempBool;
                        break;
                    case "ShowPercentOfPage":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPercentOfPage = tempBool;
                        break;
                    case "ShowMechanicals":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowMechanicals = tempBool;
                        break;

                    case "EnableTotalInserts":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableTotalInserts = tempBool;
                        break;
                    case "EnableTotalSquare":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableTotalSquare = tempBool;
                        break;
                    case "ShowTotalDetails":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalDetails = tempBool;
                        break;
                    case "ShowTotalInserts":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalInserts = tempBool;
                        break;
                    case "ShowTotalSquare":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalSquare = tempBool;
                        break;

                    case "EnableAvgAdCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableAvgAdCost = tempBool;
                        break;
                    case "EnableAvgPCI":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableAvgPCI = tempBool;
                        break;
                    case "EnableDiscounts":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableDiscounts = tempBool;
                        break;
                    case "EnableInvestment":
                        tempBool = false;
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableInvestment = tempBool;
                        break;
                    case "ShowInvestmentDetails":
                        tempBool = false;
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowInvestmentDetails = tempBool;
                        break;
                    case "ShowAvgAdCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAvgAdCost = tempBool;
                        break;
                    case "ShowAvgPCI":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAvgPCI = tempBool;
                        break;
                    case "ShowDiscounts":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDiscounts = tempBool;
                        break;
                    case "ShowInvestment":
                        tempBool = false;
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowInvestment = tempBool;
                        break;

                    case "EnableFlightDates2":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableFlightDates2 = tempBool;
                        break;
                    case "EnableDates":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableDates = tempBool;
                        break;
                    case "EnableComments":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableComments = tempBool;
                        break;
                    case "ShowDateDetails":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDateDetails = tempBool;
                        break;
                    case "ShowFlightDates2":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowFlightDates2 = tempBool;
                        break;
                    case "ShowDates":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDates = tempBool;
                        break;
                    case "ShowComments":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowComments = tempBool;
                        break;
                    case "Comments":
                        this.Comments = childNode.InnerText;
                        break;
                }
            }

            this.ShowDimensions &= this.EnableDimensions;
            this.ShowPageSize &= this.EnablePageSize;
            this.ShowPercentOfPage &= this.EnablePercentOfPage;
            this.ShowSquare &= this.EnableSquare;
            this.ShowColor &= this.EnableColor;

            this.ShowTotalInserts &= this.EnableTotalInserts;
            this.ShowTotalSquare &= this.EnableTotalSquare;

            this.ShowAvgAdCost &= this.EnableAvgAdCost;
            this.ShowAvgPCI &= this.EnableAvgPCI;
            this.ShowDiscounts &= this.EnableDiscounts;
            this.ShowInvestment &= this.EnableInvestment;

            this.ShowFlightDates2 &= this.EnableFlightDates2;
            this.ShowDates &= this.EnableDates;
            this.ShowComments &= this.EnableComments;
        }
    }

    public class PublicationMultiSummarySettings
    {
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

        public PublicationMultiSummarySettings()
        {
            this.ShowName = true;
            this.ShowLogo = true;
            this.ShowInvestment = true;

            this.EnableFlightDates = true;
            this.EnableDates = true;
            this.EnableComments = true;
            this.ShowFlightDates = true;
            this.ShowDates = true;
            this.ShowComments = false;

            this.EnableTotalInserts = true;
            this.EnableDimensions = true;
            this.EnablePageSize = true;
            this.EnablePercentOfPage = true;
            this.EnableTotalColor = true;
            this.EnableAvgAdCost = true;
            this.EnableAvgFinalCost = true;
            this.EnableDiscounts = true;
            this.EnableSection = true;
            this.ShowTotalInserts = true;
            this.ShowDimensions = false;
            this.ShowPageSize = true;
            this.ShowPercentOfPage = false;
            this.ShowTotalColor = false;
            this.ShowAvgAdCost = false;
            this.ShowAvgFinalCost = false;
            this.ShowDiscounts = false;
            this.ShowSection = false;

            this.ShowAvgPCI = false;
            this.ShowTotalSquare = false;
            this.ShowSquare = false;
            this.ShowMechanicals = false;

            this.InvestmentType = "Total";
            this.Comments = string.Empty;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<ShowName>" + this.ShowName + @"</ShowName>");
            result.AppendLine(@"<ShowLogo>" + this.ShowLogo + @"</ShowLogo>");
            result.AppendLine(@"<ShowInvestment>" + this.ShowInvestment + @"</ShowInvestment>");

            result.AppendLine(@"<EnableFlightDates>" + this.EnableFlightDates + @"</EnableFlightDates>");
            result.AppendLine(@"<EnableDates>" + this.EnableDates + @"</EnableDates>");
            result.AppendLine(@"<EnableComments>" + this.EnableComments + @"</EnableComments>");
            result.AppendLine(@"<ShowFlightDates>" + this.ShowFlightDates + @"</ShowFlightDates>");
            result.AppendLine(@"<ShowDates>" + this.ShowDates + @"</ShowDates>");
            result.AppendLine(@"<ShowComments>" + this.ShowComments + @"</ShowComments>");

            result.AppendLine(@"<EnableTotalInserts>" + this.EnableTotalInserts + @"</EnableTotalInserts>");
            result.AppendLine(@"<EnableDimensions>" + this.EnableDimensions + @"</EnableDimensions>");
            result.AppendLine(@"<EnablePageSize>" + this.EnablePageSize + @"</EnablePageSize>");
            result.AppendLine(@"<EnablePercentOfPage>" + this.EnablePercentOfPage + @"</EnablePercentOfPage>");
            result.AppendLine(@"<EnableTotalColor>" + this.EnableTotalColor + @"</EnableTotalColor>");
            result.AppendLine(@"<EnableAvgAdCost>" + this.EnableAvgAdCost + @"</EnableAvgAdCost>");
            result.AppendLine(@"<EnableAvgFinalCost>" + this.EnableAvgFinalCost + @"</EnableAvgFinalCost>");
            result.AppendLine(@"<EnableDiscounts>" + this.EnableDiscounts + @"</EnableDiscounts>");
            result.AppendLine(@"<EnableSection>" + this.EnableSection + @"</EnableSection>");
            result.AppendLine(@"<ShowTotalInserts>" + this.ShowTotalInserts + @"</ShowTotalInserts>");
            result.AppendLine(@"<ShowDimensions>" + this.ShowDimensions + @"</ShowDimensions>");
            result.AppendLine(@"<ShowPageSize>" + this.ShowPageSize + @"</ShowPageSize>");
            result.AppendLine(@"<ShowPercentOfPage>" + this.ShowPercentOfPage + @"</ShowPercentOfPage>");
            result.AppendLine(@"<ShowTotalColor>" + this.ShowTotalColor + @"</ShowTotalColor>");
            result.AppendLine(@"<ShowAvgAdCost>" + this.ShowAvgAdCost + @"</ShowAvgAdCost>");
            result.AppendLine(@"<ShowAvgFinalCost>" + this.ShowAvgFinalCost + @"</ShowAvgFinalCost>");
            result.AppendLine(@"<ShowDiscounts>" + this.ShowDiscounts + @"</ShowDiscounts>");
            result.AppendLine(@"<ShowSection>" + this.ShowSection + @"</ShowSection>");

            result.AppendLine(@"<ShowAvgPCI>" + this.ShowAvgPCI + @"</ShowAvgPCI>");
            result.AppendLine(@"<ShowTotalSquare>" + this.ShowTotalSquare + @"</ShowTotalSquare>");
            result.AppendLine(@"<ShowSquare>" + this.ShowSquare + @"</ShowSquare>");
            result.AppendLine(@"<ShowMechanicals>" + this.ShowMechanicals + @"</ShowMechanicals>");

            result.AppendLine(@"<InvestmentType>" + this.InvestmentType + @"</InvestmentType>");
            result.AppendLine(@"<Comments>" + this.Comments.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Comments>");

            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool = false; ;
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "ShowName":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowName = tempBool;
                        break;
                    case "ShowLogo":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowLogo = tempBool;
                        break;
                    case "ShowInvestment":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowInvestment = tempBool;
                        break;

                    case "EnableFlightDates":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableFlightDates = tempBool;
                        break;
                    case "EnableDates":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableDates = tempBool;
                        break;
                    case "EnableComments":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableComments = tempBool;
                        break;
                    case "ShowFlightDates":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowFlightDates = tempBool;
                        break;
                    case "ShowDates":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDates = tempBool;
                        break;
                    case "ShowComments":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowComments = tempBool;
                        break;

                    case "EnableTotalInserts":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableTotalInserts = tempBool;
                        break;
                    case "EnableDimensions":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableDimensions = tempBool;
                        break;
                    case "EnablePageSize":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnablePageSize = tempBool;
                        break;
                    case "EnablePercentOfPage":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnablePercentOfPage = tempBool;
                        break;
                    case "EnableTotalColor":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableTotalColor = tempBool;
                        break;
                    case "EnableAvgAdCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableAvgAdCost = tempBool;
                        break;
                    case "EnableAvgFinalCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableAvgFinalCost = tempBool;
                        break;
                    case "EnableDiscounts":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableDiscounts = tempBool;
                        break;
                    case "EnableSection":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableSection = tempBool;
                        break;
                    case "ShowTotalInserts":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalInserts = tempBool;
                        break;
                    case "ShowDimensions":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDimensions = tempBool;
                        break;
                    case "ShowPageSize":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPageSize = tempBool;
                        break;
                    case "ShowPercentOfPage":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPercentOfPage = tempBool;
                        break;
                    case "ShowTotalColor":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalColor = tempBool;
                        break;
                    case "ShowAvgAdCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAvgAdCost = tempBool;
                        break;
                    case "ShowAvgFinalCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAvgFinalCost = tempBool;
                        break;
                    case "ShowDiscounts":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDiscounts = tempBool;
                        break;
                    case "ShowSection":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSection = tempBool;
                        break;

                    case "ShowAvgPCI":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAvgPCI = tempBool;
                        break;
                    case "ShowTotalSquare":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalSquare = tempBool;
                        break;
                    case "ShowSquare":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSquare = tempBool;
                        break;
                    case "ShowMechanicals":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowMechanicals = tempBool;
                        break;

                    case "InvestmentType":
                        this.InvestmentType = childNode.InnerText;
                        break;
                    case "Comments":
                        this.Comments = childNode.InnerText;
                        break;
                }
            }

            this.ShowFlightDates &= this.EnableFlightDates;
            this.ShowDates &= this.EnableDates;
            this.ShowComments &= this.EnableComments;

            this.ShowTotalInserts &= this.EnableTotalInserts;
            this.ShowDimensions &= this.EnableDimensions;
            this.ShowPageSize &= this.EnablePageSize;
            this.ShowPercentOfPage &= this.EnablePercentOfPage;
            this.ShowTotalColor &= this.EnableTotalColor;
            this.ShowAvgAdCost &= this.EnableAvgAdCost;
            this.ShowAvgFinalCost &= this.EnableAvgFinalCost;
            this.ShowDiscounts &= this.EnableDiscounts;
            this.ShowSection &= this.EnableSection;
        }
    }

    public class SnapshotViewSettings
    {
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

        public SnapshotViewSettings()
        {
            this.ShowSlideHeader = true;
            this.ShowPresentationDate = true;
            this.ShowAdvertiser = true;
            this.ShowDecisionMaker = true;
            this.ShowFlightDates = true;
            this.ShowOptions = true;
            this.SelectedOptionChapterIndex = 0;

            this.EnableLogo = true;
            this.EnableTotalInserts = true;
            this.EnableTotalFinalCost = true;
            this.EnablePageSize = true;
            this.EnableDimensions = true;
            this.EnableSquare = true;
            this.EnableTotalSquare = true;
            this.EnableAvgPCI = true;
            this.EnableAvgCost = true;
            this.EnableAvgFinalCost = true;
            this.EnableTotalColor = true;
            this.EnableTotalDiscounts = true;
            this.EnableReadership = true;
            this.EnableDelivery = true;
            this.EnablePercentOfPage = true;
            this.ShowLogo = true;
            this.ShowTotalInserts = true;
            this.ShowTotalFinalCost = true;
            this.ShowPageSize = false;
            this.ShowDimensions = false;
            this.ShowSquare = false;
            this.ShowTotalSquare = false;
            this.ShowAvgPCI = false;
            this.ShowAvgCost = false;
            this.ShowAvgFinalCost = false;
            this.ShowTotalColor = false;
            this.ShowTotalDiscounts = false;
            this.ShowReadership = false;
            this.ShowDelivery = false;
            this.ShowPercentOfPage = false;

            this.SlideHeader = string.Empty;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<ShowAdvertiser>" + this.ShowAdvertiser + @"</ShowAdvertiser>");
            result.AppendLine(@"<ShowDecisionMaker>" + this.ShowDecisionMaker + @"</ShowDecisionMaker>");
            result.AppendLine(@"<ShowFlightDates>" + this.ShowFlightDates + @"</ShowFlightDates>");
            result.AppendLine(@"<ShowPresentationDate>" + this.ShowPresentationDate + @"</ShowPresentationDate>");
            result.AppendLine(@"<ShowSlideHeader>" + this.ShowSlideHeader + @"</ShowSlideHeader>");
            result.AppendLine(@"<ShowOptions>" + this.ShowOptions + @"</ShowOptions>");
            result.AppendLine(@"<SelectedOptionChapterIndex>" + this.SelectedOptionChapterIndex + @"</SelectedOptionChapterIndex>");

            result.AppendLine(@"<EnableLogo>" + this.EnableLogo + @"</EnableLogo>");
            result.AppendLine(@"<EnableTotalInserts>" + this.EnableTotalInserts + @"</EnableTotalInserts>");
            result.AppendLine(@"<EnableTotalFinalCost>" + this.EnableTotalFinalCost + @"</EnableTotalFinalCost>");
            result.AppendLine(@"<EnablePageSize>" + this.EnablePageSize + @"</EnablePageSize>");
            result.AppendLine(@"<EnableDimensions>" + this.EnableDimensions + @"</EnableDimensions>");
            result.AppendLine(@"<EnableSquare>" + this.EnableSquare + @"</EnableSquare>");
            result.AppendLine(@"<EnableTotalSquare>" + this.EnableTotalSquare + @"</EnableTotalSquare>");
            result.AppendLine(@"<EnableAvgPCI>" + this.EnableAvgPCI + @"</EnableAvgPCI>");
            result.AppendLine(@"<EnableAvgCost>" + this.EnableAvgCost + @"</EnableAvgCost>");
            result.AppendLine(@"<EnableAvgFinalCost>" + this.EnableAvgFinalCost + @"</EnableAvgFinalCost>");
            result.AppendLine(@"<EnableTotalColor>" + this.EnableTotalColor + @"</EnableTotalColor>");
            result.AppendLine(@"<EnableTotalDiscounts>" + this.EnableTotalDiscounts + @"</EnableTotalDiscounts>");
            result.AppendLine(@"<EnableReadership>" + this.EnableReadership + @"</EnableReadership>");
            result.AppendLine(@"<EnableDelivery>" + this.EnableDelivery + @"</EnableDelivery>");
            result.AppendLine(@"<EnablePercentOfPage>" + this.EnablePercentOfPage + @"</EnablePercentOfPage>");
            result.AppendLine(@"<ShowLogo>" + this.ShowLogo + @"</ShowLogo>");
            result.AppendLine(@"<ShowTotalInserts>" + this.ShowTotalInserts + @"</ShowTotalInserts>");
            result.AppendLine(@"<ShowTotalFinalCost>" + this.ShowTotalFinalCost + @"</ShowTotalFinalCost>");
            result.AppendLine(@"<ShowPageSize>" + this.ShowPageSize + @"</ShowPageSize>");
            result.AppendLine(@"<ShowDimensions>" + this.ShowDimensions + @"</ShowDimensions>");
            result.AppendLine(@"<ShowSquare>" + this.ShowSquare + @"</ShowSquare>");
            result.AppendLine(@"<ShowTotalSquare>" + this.ShowTotalSquare + @"</ShowTotalSquare>");
            result.AppendLine(@"<ShowAvgPCI>" + this.ShowAvgPCI + @"</ShowAvgPCI>");
            result.AppendLine(@"<ShowAvgCost>" + this.ShowAvgCost + @"</ShowAvgCost>");
            result.AppendLine(@"<ShowAvgFinalCost>" + this.ShowAvgFinalCost + @"</ShowAvgFinalCost>");
            result.AppendLine(@"<ShowTotalColor>" + this.ShowTotalColor + @"</ShowTotalColor>");
            result.AppendLine(@"<ShowTotalDiscounts>" + this.ShowTotalDiscounts + @"</ShowTotalDiscounts>");
            result.AppendLine(@"<ShowReadership>" + this.ShowReadership + @"</ShowReadership>");
            result.AppendLine(@"<ShowDelivery>" + this.ShowDelivery + @"</ShowDelivery>");
            result.AppendLine(@"<ShowPercentOfPage>" + this.ShowPercentOfPage + @"</ShowPercentOfPage>");

            result.AppendLine(@"<SlideHeader>" + this.SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");

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
                            this.ShowAdvertiser = tempBool;
                        break;
                    case "ShowDecisionMaker":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDecisionMaker = tempBool;
                        break;
                    case "ShowFlightDates":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowFlightDates = tempBool;
                        break;
                    case "ShowPresentationDate":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPresentationDate = tempBool;
                        break;
                    case "ShowSlideHeader":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSlideHeader = tempBool;
                        break;
                    case "ShowOptions":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowOptions = tempBool;
                        break;
                    case "SelectedOptionChapterIndex":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.SelectedOptionChapterIndex = tempInt;
                        break;

                    case "EnableLogo":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableLogo = tempBool;
                        break;
                    case "EnableTotalInserts":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableTotalInserts = tempBool;
                        break;
                    case "EnableTotalFinalCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableTotalFinalCost = tempBool;
                        break;
                    case "EnablePageSize":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnablePageSize = tempBool;
                        break;
                    case "EnableDimensions":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableDimensions = tempBool;
                        break;
                    case "EnableSquare":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableSquare = tempBool;
                        break;
                    case "EnableTotalSquare":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableTotalSquare = tempBool;
                        break;
                    case "EnableAvgPCI":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableAvgPCI = tempBool;
                        break;
                    case "EnableAvgCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableAvgCost = tempBool;
                        break;
                    case "EnableAvgFinalCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableAvgFinalCost = tempBool;
                        break;
                    case "EnableTotalColor":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableTotalColor = tempBool;
                        break;
                    case "EnableTotalDiscounts":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableTotalDiscounts = tempBool;
                        break;
                    case "EnableReadership":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableReadership = tempBool;
                        break;
                    case "EnableDelivery":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableDelivery = tempBool;
                        break;
                    case "EnablePercentOfPage":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnablePercentOfPage = tempBool;
                        break;
                    case "ShowLogo":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowLogo = tempBool;
                        break;
                    case "ShowTotalInserts":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalInserts = tempBool;
                        break;
                    case "ShowTotalFinalCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalFinalCost = tempBool;
                        break;
                    case "ShowPageSize":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPageSize = tempBool;
                        break;
                    case "ShowDimensions":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDimensions = tempBool;
                        break;
                    case "ShowSquare":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSquare = tempBool;
                        break;
                    case "ShowTotalSquare":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalSquare = tempBool;
                        break;
                    case "ShowAvgPCI":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAvgPCI = tempBool;
                        break;
                    case "ShowAvgCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAvgCost = tempBool;
                        break;
                    case "ShowAvgFinalCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAvgFinalCost = tempBool;
                        break;
                    case "ShowTotalColor":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalColor = tempBool;
                        break;
                    case "ShowTotalDiscounts":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalDiscounts = tempBool;
                        break;
                    case "ShowReadership":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowReadership = tempBool;
                        break;
                    case "ShowDelivery":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDelivery = tempBool;
                        break;
                    case "ShowPercentOfPage":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPercentOfPage = tempBool;
                        break;

                    case "SlideHeader":
                        this.SlideHeader = childNode.InnerText;
                        break;
                }
            }

            this.ShowLogo &= this.EnableLogo;
            this.ShowTotalInserts &= this.EnableTotalInserts;
            this.ShowTotalFinalCost &= this.EnableTotalFinalCost;
            this.ShowPageSize &= this.EnablePageSize;
            this.ShowDimensions &= this.EnableDimensions;
            this.ShowSquare &= this.EnableSquare;
            this.ShowTotalSquare &= this.EnableTotalSquare;
            this.ShowAvgPCI &= this.EnableAvgPCI;
            this.ShowAvgCost &= this.EnableAvgCost;
            this.ShowAvgFinalCost &= this.EnableAvgFinalCost;
            this.ShowTotalColor &= this.EnableTotalColor;
            this.ShowTotalDiscounts &= this.EnableTotalDiscounts;
            this.ShowReadership &= this.EnableReadership;
            this.ShowDelivery &= this.EnableDelivery;
            this.ShowPercentOfPage &= this.EnablePercentOfPage;
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
            this.EnableID = true;
            this.EnableIndex = true;
            this.EnableDate = true;
            this.EnableColor = true;
            this.EnableSection = true;
            this.EnablePCI = true;
            this.EnableFinalCost = true;
            this.EnablePublication = true;
            this.EnablePercentOfPage = true;
            this.EnableCost = true;
            this.EnableDimensions = true;
            this.EnableMechanicals = true;
            this.EnableDelivery = true;
            this.EnableDiscount = true;
            this.EnablePageSize = true;
            this.EnableSquare = true;
            this.EnableDeadline = true;
            this.EnableReadership = true;
            this.EnableAdNotes = true;
            #endregion

            #region Show
            this.ShowID = true;
            this.ShowIndex = true;
            this.ShowDate = true;
            this.ShowColor = false;
            this.ShowSection = false;
            this.ShowPCI = false;
            this.ShowFinalCost = false;
            this.ShowPublication = true;
            this.ShowPercentOfPage = false;
            this.ShowCost = false;
            this.ShowDimensions = false;
            this.ShowMechanicals = false;
            this.ShowDelivery = false;
            this.ShowDiscount = false;
            this.ShowPageSize = false;
            this.ShowSquare = false;
            this.ShowDeadline = false;
            this.ShowReadership = false;
            this.ShowAdNotes = true;
            #endregion

            #region Position
            this.IDPosition = 0;
            this.IndexPosition = 1;
            this.DatePosition = 2;
            this.ColorPosition = 4;
            this.SectionPosition = 5;
            this.PCIPosition = 6;
            this.FinalCostPosition = 7;
            this.PublicationPosition = 3;
            this.PercentOfPagePosition = 8;
            this.CostPosition = 9;
            this.DimensionsPosition = 10;
            this.MechanicalsPosition = 11;
            this.DeliveryPosition = 12;
            this.DiscountPosition = 13;
            this.PageSizePosition = 14;
            this.SquarePosition = 15;
            this.DeadlinePosition = 16;
            this.ReadershipPosition = 17;
            #endregion

            #region Width
            this.IDWidth = 50;
            this.IndexWidth = 50;
            this.DateWidth = 155;
            this.PCIWidth = 110;
            this.CostWidth = 110;
            this.FinalCostWidth = 110;
            this.DiscountWidth = 110;
            this.ColorWidth = 110;
            this.PublicationWidth = 160;
            this.SquareWidth = 110;
            this.PageSizeWidth = 110;
            this.PercentOfPageWidth = 110;
            this.DimensionsWidth = 110;
            this.MechanicalsWidth = 110;
            this.SectionWidth = 110;
            this.DeliveryWidth = 110;
            this.ReadershipWidth = 110;
            this.DeadlineWidth = 110;
            #endregion

            #region Caption
            this.IDCaption = @"ID";
            this.IndexCaption = @"INS #";
            this.DateCaption = @"Day/Date";
            this.PCICaption = @"PCI";
            this.CostCaption = @"Cost (B&W)";
            this.FinalCostCaption = @"Total Cost";
            this.DiscountCaption = @"Discounts";
            this.ColorCaption = @"Color";
            this.PublicationCaption = @"Publication";
            this.SquareCaption = @"Total Col. In.";
            this.PageSizeCaption = @"Page Size";
            this.PercentOfPageCaption = @"% of Page";
            this.DimensionsCaption = @"Col. x Inches";
            this.MechanicalsCaption = @"Mechanicals";
            this.SectionCaption = @"Section";
            this.DeliveryCaption = @"Delivery";
            this.ReadershipCaption = @"Readership";
            this.DeadlineCaption = @"Deadline";
            #endregion
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            #region Enable
            result.AppendLine(@"<EnableID>" + this.EnableID + @"</EnableID>");
            result.AppendLine(@"<EnableIndex>" + this.EnableIndex + @"</EnableIndex>");
            result.AppendLine(@"<EnableDate>" + this.EnableDate + @"</EnableDate>");
            result.AppendLine(@"<EnableColor>" + this.EnableColor + @"</EnableColor>");
            result.AppendLine(@"<EnableSection>" + this.EnableSection + @"</EnableSection>");
            result.AppendLine(@"<EnablePCI>" + this.EnablePCI + @"</EnablePCI>");
            result.AppendLine(@"<EnableFinalCost>" + this.EnableFinalCost + @"</EnableFinalCost>");
            result.AppendLine(@"<EnablePublication>" + this.EnablePublication + @"</EnablePublication>");
            result.AppendLine(@"<EnablePercentOfPage>" + this.EnablePercentOfPage + @"</EnablePercentOfPage>");
            result.AppendLine(@"<EnableCost>" + this.EnableCost + @"</EnableCost>");
            result.AppendLine(@"<EnableDimensions>" + this.EnableDimensions + @"</EnableDimensions>");
            result.AppendLine(@"<EnableMechanicals>" + this.EnableMechanicals + @"</EnableMechanicals>");
            result.AppendLine(@"<EnableDelivery>" + this.EnableDelivery + @"</EnableDelivery>");
            result.AppendLine(@"<EnableDiscount>" + this.EnableDiscount + @"</EnableDiscount>");
            result.AppendLine(@"<EnablePageSize>" + this.EnablePageSize + @"</EnablePageSize>");
            result.AppendLine(@"<EnableSquare>" + this.EnableSquare + @"</EnableSquare>");
            result.AppendLine(@"<EnableDeadline>" + this.EnableDeadline + @"</EnableDeadline>");
            result.AppendLine(@"<EnableReadership>" + this.EnableReadership + @"</EnableReadership>");
            result.AppendLine(@"<EnableAdNotes>" + this.EnableAdNotes + @"</EnableAdNotes>");
            #endregion

            #region Show
            result.AppendLine(@"<ShowID>" + this.ShowID + @"</ShowID>");
            result.AppendLine(@"<ShowIndex>" + this.ShowIndex + @"</ShowIndex>");
            result.AppendLine(@"<ShowDate>" + this.ShowDate + @"</ShowDate>");
            result.AppendLine(@"<ShowColor>" + this.ShowColor + @"</ShowColor>");
            result.AppendLine(@"<ShowSection>" + this.ShowSection + @"</ShowSection>");
            result.AppendLine(@"<ShowPCI>" + this.ShowPCI + @"</ShowPCI>");
            result.AppendLine(@"<ShowFinalCost>" + this.ShowFinalCost + @"</ShowFinalCost>");
            result.AppendLine(@"<ShowPublication>" + this.ShowPublication + @"</ShowPublication>");
            result.AppendLine(@"<ShowPercentOfPage>" + this.ShowPercentOfPage + @"</ShowPercentOfPage>");
            result.AppendLine(@"<ShowCost>" + this.ShowCost + @"</ShowCost>");
            result.AppendLine(@"<ShowDimensions>" + this.ShowDimensions + @"</ShowDimensions>");
            result.AppendLine(@"<ShowMechanicals>" + this.ShowMechanicals + @"</ShowMechanicals>");
            result.AppendLine(@"<ShowDelivery>" + this.ShowDelivery + @"</ShowDelivery>");
            result.AppendLine(@"<ShowDiscount>" + this.ShowDiscount + @"</ShowDiscount>");
            result.AppendLine(@"<ShowPageSize>" + this.ShowPageSize + @"</ShowPageSize>");
            result.AppendLine(@"<ShowSquare>" + this.ShowSquare + @"</ShowSquare>");
            result.AppendLine(@"<ShowDeadline>" + this.ShowDeadline + @"</ShowDeadline>");
            result.AppendLine(@"<ShowReadership>" + this.ShowReadership + @"</ShowReadership>");
            result.AppendLine(@"<ShowAdNotes>" + this.ShowAdNotes + @"</ShowAdNotes>");
            #endregion

            #region Position
            result.AppendLine(@"<IDPosition>" + this.IDPosition + @"</IDPosition>");
            result.AppendLine(@"<IndexPosition>" + this.IndexPosition + @"</IndexPosition>");
            result.AppendLine(@"<DatePosition>" + this.DatePosition + @"</DatePosition>");
            result.AppendLine(@"<ColorPosition>" + this.ColorPosition + @"</ColorPosition>");
            result.AppendLine(@"<SectionPosition>" + this.SectionPosition + @"</SectionPosition>");
            result.AppendLine(@"<PCIPosition>" + this.PCIPosition + @"</PCIPosition>");
            result.AppendLine(@"<FinalCostPosition>" + this.FinalCostPosition + @"</FinalCostPosition>");
            result.AppendLine(@"<PublicationPosition>" + this.PublicationPosition + @"</PublicationPosition>");
            result.AppendLine(@"<PercentOfPagePosition>" + this.PercentOfPagePosition + @"</PercentOfPagePosition>");
            result.AppendLine(@"<CostPosition>" + this.CostPosition + @"</CostPosition>");
            result.AppendLine(@"<DimensionsPosition>" + this.DimensionsPosition + @"</DimensionsPosition>");
            result.AppendLine(@"<MechanicalsPosition>" + this.MechanicalsPosition + @"</MechanicalsPosition>");
            result.AppendLine(@"<DeliveryPosition>" + this.DeliveryPosition + @"</DeliveryPosition>");
            result.AppendLine(@"<DiscountPosition>" + this.DiscountPosition + @"</DiscountPosition>");
            result.AppendLine(@"<PageSizePosition>" + this.PageSizePosition + @"</PageSizePosition>");
            result.AppendLine(@"<SquarePosition>" + this.SquarePosition + @"</SquarePosition>");
            result.AppendLine(@"<DeadlinePosition>" + this.DeadlinePosition + @"</DeadlinePosition>");
            result.AppendLine(@"<ReadershipPosition>" + this.ReadershipPosition + @"</ReadershipPosition>");
            #endregion

            #region Width
            result.AppendLine(@"<IDWidth>" + this.IDWidth + @"</IDWidth>");
            result.AppendLine(@"<IndexWidth>" + this.IndexWidth + @"</IndexWidth>");
            result.AppendLine(@"<DateWidth>" + this.DateWidth + @"</DateWidth>");
            result.AppendLine(@"<PCIWidth>" + this.PCIWidth + @"</PCIWidth>");
            result.AppendLine(@"<CostWidth>" + this.CostWidth + @"</CostWidth>");
            result.AppendLine(@"<FinalCostWidth>" + this.FinalCostWidth + @"</FinalCostWidth>");
            result.AppendLine(@"<DiscountWidth>" + this.DiscountWidth + @"</DiscountWidth>");
            result.AppendLine(@"<ColorWidth>" + this.ColorWidth + @"</ColorWidth>");
            result.AppendLine(@"<PublicationWidth>" + this.PublicationWidth + @"</PublicationWidth>");
            result.AppendLine(@"<SquareWidth>" + this.SquareWidth + @"</SquareWidth>");
            result.AppendLine(@"<PageSizeWidth>" + this.PageSizeWidth + @"</PageSizeWidth>");
            result.AppendLine(@"<PercentOfPageWidth>" + this.PercentOfPageWidth + @"</PercentOfPageWidth>");
            result.AppendLine(@"<DimensionsWidth>" + this.DimensionsWidth + @"</DimensionsWidth>");
            result.AppendLine(@"<MechanicalsWidth>" + this.MechanicalsWidth + @"</MechanicalsWidth>");
            result.AppendLine(@"<SectionWidth>" + this.SectionWidth + @"</SectionWidth>");
            result.AppendLine(@"<DeliveryWidth>" + this.DeliveryWidth + @"</DeliveryWidth>");
            result.AppendLine(@"<ReadershipWidth>" + this.ReadershipWidth + @"</ReadershipWidth>");
            result.AppendLine(@"<DeadlineWidth>" + this.DeadlineWidth + @"</DeadlineWidth>");
            #endregion

            #region Caption
            result.AppendLine(@"<IDCaption>" + this.IDCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</IDCaption>");
            result.AppendLine(@"<IndexCaption>" + this.IndexCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</IndexCaption>");
            result.AppendLine(@"<DateCaption>" + this.DateCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DateCaption>");
            result.AppendLine(@"<PCICaption>" + this.PCICaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</PCICaption>");
            result.AppendLine(@"<CostCaption>" + this.CostCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</CostCaption>");
            result.AppendLine(@"<FinalCostCaption>" + this.FinalCostCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</FinalCostCaption>");
            result.AppendLine(@"<DiscountCaption>" + this.DiscountCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DiscountCaption>");
            result.AppendLine(@"<ColorCaption>" + this.ColorCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</ColorCaption>");
            result.AppendLine(@"<PublicationCaption>" + this.PublicationCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</PublicationCaption>");
            result.AppendLine(@"<SquareCaption>" + this.SquareCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SquareCaption>");
            result.AppendLine(@"<PageSizeCaption>" + this.PageSizeCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</PageSizeCaption>");
            result.AppendLine(@"<PercentOfPageCaption>" + this.PercentOfPageCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</PercentOfPageCaption>");
            result.AppendLine(@"<DimensionsCaption>" + this.DimensionsCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DimensionsCaption>");
            result.AppendLine(@"<MechanicalsCaption>" + this.MechanicalsCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</MechanicalsCaption>");
            result.AppendLine(@"<SectionCaption>" + this.SectionCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SectionCaption>");
            result.AppendLine(@"<DeliveryCaption>" + this.DeliveryCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DeliveryCaption>");
            result.AppendLine(@"<ReadershipCaption>" + this.ReadershipCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</ReadershipCaption>");
            result.AppendLine(@"<DeadlineCaption>" + this.DeadlineCaption.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DeadlineCaption>");
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
                            this.EnableID = tempBool;
                        break;
                    case "EnableIndex":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableIndex = tempBool;
                        break;
                    case "EnableDate":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableDate = tempBool;
                        break;
                    case "EnableColor":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableColor = tempBool;
                        break;
                    case "EnableSection":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableSection = tempBool;
                        break;
                    case "EnablePCI":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnablePCI = tempBool;
                        break;
                    case "EnableFinalCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableFinalCost = tempBool;
                        break;
                    case "EnablePublication":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnablePublication = tempBool;
                        break;
                    case "EnablePercentOfPage":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnablePercentOfPage = tempBool;
                        break;
                    case "EnableCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableCost = tempBool;
                        break;
                    case "EnableDimensions":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableDimensions = tempBool;
                        break;
                    case "EnableMechanicals":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableMechanicals = tempBool;
                        break;
                    case "EnableDelivery":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableDelivery = tempBool;
                        break;
                    case "EnableDiscount":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableDiscount = tempBool;
                        break;
                    case "EnablePageSize":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnablePageSize = tempBool;
                        break;
                    case "EnableSquare":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableSquare = tempBool;
                        break;
                    case "EnableDeadline":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableDeadline = tempBool;
                        break;
                    case "EnableReadership":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableReadership = tempBool;
                        break;
                    case "EnableAdNotes":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableAdNotes = tempBool;
                        break;
                    #endregion

                    #region Show
                    case "ShowID":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowID = tempBool;
                        break;
                    case "ShowIndex":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowIndex = tempBool;
                        break;
                    case "ShowDate":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDate = tempBool;
                        break;
                    case "ShowColor":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowColor = tempBool;
                        break;
                    case "ShowSection":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSection = tempBool;
                        break;
                    case "ShowPCI":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPCI = tempBool;
                        break;
                    case "ShowFinalCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowFinalCost = tempBool;
                        break;
                    case "ShowPublication":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPublication = tempBool;
                        break;
                    case "ShowPercentOfPage":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPercentOfPage = tempBool;
                        break;
                    case "ShowCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowCost = tempBool;
                        break;
                    case "ShowDimensions":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDimensions = tempBool;
                        break;
                    case "ShowMechanicals":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowMechanicals = tempBool;
                        break;
                    case "ShowDelivery":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDelivery = tempBool;
                        break;
                    case "ShowDiscount":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDiscount = tempBool;
                        break;
                    case "ShowPageSize":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPageSize = tempBool;
                        break;
                    case "ShowSquare":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSquare = tempBool;
                        break;
                    case "ShowDeadline":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDeadline = tempBool;
                        break;
                    case "ShowReadership":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowReadership = tempBool;
                        break;
                    case "ShowAdNotes":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAdNotes = tempBool;
                        break;
                    #endregion

                    #region Position
                    case "IDPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.IDPosition = tempInt;
                        break;
                    case "IndexPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.IndexPosition = tempInt;
                        break;
                    case "DatePosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.DatePosition = tempInt;
                        break;
                    case "ColorPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.ColorPosition = tempInt;
                        break;
                    case "SectionPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.SectionPosition = tempInt;
                        break;
                    case "PCIPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.PCIPosition = tempInt;
                        break;
                    case "FinalCostPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.FinalCostPosition = tempInt;
                        break;
                    case "PublicationPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.PublicationPosition = tempInt;
                        break;
                    case "PercentOfPagePosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.PercentOfPagePosition = tempInt;
                        break;
                    case "CostPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.CostPosition = tempInt;
                        break;
                    case "DimensionsPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.DimensionsPosition = tempInt;
                        break;
                    case "MechanicalsPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.MechanicalsPosition = tempInt;
                        break;
                    case "DeliveryPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.DeliveryPosition = tempInt;
                        break;
                    case "DiscountPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.DiscountPosition = tempInt;
                        break;
                    case "PageSizePosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.PageSizePosition = tempInt;
                        break;
                    case "SquarePosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.SquarePosition = tempInt;
                        break;
                    case "DeadlinePosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.DeadlinePosition = tempInt;
                        break;
                    case "ReadershipPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.ReadershipPosition = tempInt;
                        break;
                    #endregion

                    #region Width
                    case "ColorWidth":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.ColorWidth = tempInt;
                        break;
                    case "CostWidth":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.CostWidth = tempInt;
                        break;
                    case "DateWidth":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.DateWidth = tempInt;
                        break;
                    case "DeadlineWidth":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.DeadlineWidth = tempInt;
                        break;
                    case "DeliveryWidth":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.DeliveryWidth = tempInt;
                        break;
                    case "DimensionsWidth":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.DimensionsWidth = tempInt;
                        break;
                    case "DiscountWidth":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.DiscountWidth = tempInt;
                        break;
                    case "FinalCostWidth":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.FinalCostWidth = tempInt;
                        break;
                    case "IDWidth":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.IDWidth = tempInt;
                        break;
                    case "IndexWidth":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.IndexWidth = tempInt;
                        break;
                    case "MechanicalsWidth":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.MechanicalsWidth = tempInt;
                        break;
                    case "PageSizeWidth":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.PageSizeWidth = tempInt;
                        break;
                    case "PercentOfPageWidth":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.PercentOfPageWidth = tempInt;
                        break;
                    case "PCIWidth":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.PCIWidth = tempInt;
                        break;
                    case "PublicationWidth":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.PublicationWidth = tempInt;
                        break;
                    case "ReadershipWidth":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.ReadershipWidth = tempInt;
                        break;
                    case "SectionWidth":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.SectionWidth = tempInt;
                        break;
                    case "SquareWidth":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.SquareWidth = tempInt;
                        break;
                    #endregion

                    #region Caption
                    case "ColorCaption":
                        this.ColorCaption = childNode.InnerText;
                        break;
                    case "CostCaption":
                        this.CostCaption = childNode.InnerText.Replace("&&", "&");
                        break;
                    case "DateCaption":
                        this.DateCaption = childNode.InnerText;
                        break;
                    case "DeadlineCaption":
                        this.DeadlineCaption = childNode.InnerText;
                        break;
                    case "DeliveryCaption":
                        this.DeliveryCaption = childNode.InnerText;
                        break;
                    case "DimensionsCaption":
                        this.DimensionsCaption = childNode.InnerText;
                        break;
                    case "DiscountCaption":
                        this.DiscountCaption = childNode.InnerText;
                        break;
                    case "FinalCostCaption":
                        this.FinalCostCaption = childNode.InnerText;
                        break;
                    case "IDCaption":
                        this.IDCaption = childNode.InnerText;
                        break;
                    case "IndexCaption":
                        this.IndexCaption = childNode.InnerText;
                        break;
                    case "MechanicalsCaption":
                        this.MechanicalsCaption = childNode.InnerText;
                        break;
                    case "PageSizeCaption":
                        this.PageSizeCaption = childNode.InnerText;
                        break;
                    case "PercentOfPageCaption":
                        this.PercentOfPageCaption = childNode.InnerText;
                        break;
                    case "PCICaption":
                        this.PCICaption = childNode.InnerText;
                        break;
                    case "PublicationCaption":
                        this.PublicationCaption = childNode.InnerText;
                        break;
                    case "ReadershipCaption":
                        this.ReadershipCaption = childNode.InnerText;
                        break;
                    case "SectionCaption":
                        this.SectionCaption = childNode.InnerText;
                        break;
                    case "SquareCaption":
                        this.SquareCaption = childNode.InnerText;
                        break;
                    #endregion
                }
            }

            this.ShowID &= this.EnableID;
            this.ShowIndex &= this.EnableIndex;
            this.ShowDate &= this.EnableDate;
            this.ShowColor &= this.EnableColor;
            this.ShowSection &= this.EnableSection;
            this.ShowPCI &= this.EnablePCI;
            this.ShowFinalCost &= this.EnableFinalCost;
            this.ShowPublication &= this.EnablePublication;
            this.ShowPercentOfPage &= this.EnablePercentOfPage;
            this.ShowCost &= this.EnableCost;
            this.ShowDimensions &= this.EnableDimensions;
            this.ShowMechanicals &= this.EnableMechanicals;
            this.ShowDelivery &= this.EnableDelivery;
            this.ShowDiscount &= this.EnableDiscount;
            this.ShowPageSize &= this.EnablePageSize;
            this.ShowSquare &= this.EnableSquare;
            this.ShowDeadline &= this.EnableDeadline;
            this.ShowReadership &= this.EnableReadership;
            this.ShowAdNotes &= this.EnableAdNotes;
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
            this.EnableComments = true;
            this.EnableSection = true;
            this.EnableMechanicals = true;
            this.EnableDimensions = true;
            this.EnableDelivery = true;
            this.EnablePublication = true;
            this.EnableSquare = true;
            this.EnablePageSize = true;
            this.EnablePercentOfPage = true;
            this.EnableReadership = true;
            this.EnableDeadline = true;
            #endregion

            #region Show
            this.ShowComments = true;
            this.ShowSection = true;
            this.ShowMechanicals = false;
            this.ShowDimensions = false;
            this.ShowDelivery = false;
            this.ShowPublication = false;
            this.ShowSquare = false;
            this.ShowPageSize = false;
            this.ShowPercentOfPage = false;
            this.ShowReadership = false;
            this.ShowDeadline = false;
            #endregion

            #region Position
            this.PositionComments = 1;
            this.PositionSection = 2;
            this.PositionMechanicals = 3;
            this.PositionDimensions = 4;
            this.PositionDelivery = 5;
            this.PositionPublication = 6;
            this.PositionSquare = 7;
            this.PositionPageSize = 8;
            this.PositionPercentOfPage = 9;
            this.PositionReadership = 10;
            this.PositionDeadline = 11;
            #endregion
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<EnableComments>" + this.EnableComments + @"</EnableComments>");
            result.AppendLine(@"<EnableDeadline>" + this.EnableDeadline + @"</EnableDeadline>");
            result.AppendLine(@"<EnableDelivery>" + this.EnableDelivery + @"</EnableDelivery>");
            result.AppendLine(@"<EnableDimensions>" + this.EnableDimensions + @"</EnableDimensions>");
            result.AppendLine(@"<EnableMechanicals>" + this.EnableMechanicals + @"</EnableMechanicals>");
            result.AppendLine(@"<EnablePageSize>" + this.EnablePageSize + @"</EnablePageSize>");
            result.AppendLine(@"<EnablePercentOfPage>" + this.EnablePercentOfPage + @"</EnablePercentOfPage>");
            result.AppendLine(@"<EnablePublication>" + this.EnablePublication + @"</EnablePublication>");
            result.AppendLine(@"<EnableReadership>" + this.EnableReadership + @"</EnableReadership>");
            result.AppendLine(@"<EnableSection>" + this.EnableSection + @"</EnableSection>");
            result.AppendLine(@"<EnableSquare>" + this.EnableSquare + @"</EnableSquare>");

            result.AppendLine(@"<ShowComments>" + this.ShowComments + @"</ShowComments>");
            result.AppendLine(@"<ShowDeadline>" + this.ShowDeadline + @"</ShowDeadline>");
            result.AppendLine(@"<ShowDelivery>" + this.ShowDelivery + @"</ShowDelivery>");
            result.AppendLine(@"<ShowDimensions>" + this.ShowDimensions + @"</ShowDimensions>");
            result.AppendLine(@"<ShowMechanicals>" + this.ShowMechanicals + @"</ShowMechanicals>");
            result.AppendLine(@"<ShowPageSize>" + this.ShowPageSize + @"</ShowPageSize>");
            result.AppendLine(@"<ShowPercentOfPage>" + this.ShowPercentOfPage + @"</ShowPercentOfPage>");
            result.AppendLine(@"<ShowPublication>" + this.ShowPublication + @"</ShowPublication>");
            result.AppendLine(@"<ShowReadership>" + this.ShowReadership + @"</ShowReadership>");
            result.AppendLine(@"<ShowSection>" + this.ShowSection + @"</ShowSection>");
            result.AppendLine(@"<ShowSquare>" + this.ShowSquare + @"</ShowSquare>");

            result.AppendLine(@"<PositionComments>" + this.PositionComments + @"</PositionComments>");
            result.AppendLine(@"<PositionDeadline>" + this.PositionDeadline + @"</PositionDeadline>");
            result.AppendLine(@"<PositionDelivery>" + this.PositionDelivery + @"</PositionDelivery>");
            result.AppendLine(@"<PositionDimensions>" + this.PositionDimensions + @"</PositionDimensions>");
            result.AppendLine(@"<PositionMechanicals>" + this.PositionMechanicals + @"</PositionMechanicals>");
            result.AppendLine(@"<PositionPageSize>" + this.PositionPageSize + @"</PositionPageSize>");
            result.AppendLine(@"<PositionPercentOfPage>" + this.PositionPercentOfPage + @"</PositionPercentOfPage>");
            result.AppendLine(@"<PositionPublication>" + this.PositionPublication + @"</PositionPublication>");
            result.AppendLine(@"<PositionReadership>" + this.PositionReadership + @"</PositionReadership>");
            result.AppendLine(@"<PositionSection>" + this.PositionSection + @"</PositionSection>");
            result.AppendLine(@"<PositionSquare>" + this.PositionSquare + @"</PositionSquare>");

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
                            this.EnableComments = tempBool;
                        break;
                    case "EnableDeadline":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableDeadline = tempBool;
                        break;
                    case "EnableDelivery":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableDelivery = tempBool;
                        break;
                    case "EnableDimensions":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableDimensions = tempBool;
                        break;
                    case "EnableMechanicals":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableMechanicals = tempBool;
                        break;
                    case "EnablePageSize":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnablePageSize = tempBool;
                        break;
                    case "EnablePercentOfPage":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnablePercentOfPage = tempBool;
                        break;
                    case "EnablePublication":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnablePublication = tempBool;
                        break;
                    case "EnableSection":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableSection = tempBool;
                        break;
                    case "EnableReadership":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableReadership = tempBool;
                        break;
                    case "EnableSquare":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableSquare = tempBool;
                        break;

                    case "ShowComments":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowComments = tempBool;
                        break;
                    case "ShowDeadline":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDeadline = tempBool;
                        break;
                    case "ShowDelivery":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDelivery = tempBool;
                        break;
                    case "ShowDimensions":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDimensions = tempBool;
                        break;
                    case "ShowMechanicals":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowMechanicals = tempBool;
                        break;
                    case "ShowPageSize":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPageSize = tempBool;
                        break;
                    case "ShowPercentOfPage":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPercentOfPage = tempBool;
                        break;
                    case "ShowPublication":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPublication = tempBool;
                        break;
                    case "ShowSection":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSection = tempBool;
                        break;
                    case "ShowReadership":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowReadership = tempBool;
                        break;
                    case "ShowSquare":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSquare = tempBool;
                        break;

                    case "PositionComments":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.PositionComments = tempInt;
                        break;
                    case "PositionDeadline":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.PositionDeadline = tempInt;
                        break;
                    case "PositionDelivery":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.PositionDelivery = tempInt;
                        break;
                    case "PositionDimensions":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.PositionDimensions = tempInt;
                        break;
                    case "PositionMechanicals":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.PositionMechanicals = tempInt;
                        break;
                    case "PositionPageSize":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.PositionPageSize = tempInt;
                        break;
                    case "PositionPercentOfPage":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.PositionPercentOfPage = tempInt;
                        break;
                    case "PositionPublication":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.PositionPublication = tempInt;
                        break;
                    case "PositionSection":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.PositionSection = tempInt;
                        break;
                    case "PositionReadership":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.PositionReadership = tempInt;
                        break;
                    case "PositionSquare":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.PositionSquare = tempInt;
                        break;
                }
            }

            this.ShowComments &= this.EnableComments;
            this.ShowSection &= this.EnableSection;
            this.ShowMechanicals &= this.EnableMechanicals;
            this.ShowDimensions &= this.EnableDimensions;
            this.ShowDelivery &= this.EnableDelivery;
            this.ShowPublication &= this.EnablePublication;
            this.ShowSquare &= this.EnableSquare;
            this.ShowPageSize &= this.EnablePageSize;
            this.ShowPercentOfPage &= this.EnablePercentOfPage;
            this.ShowReadership &= this.EnableReadership;
            this.ShowDeadline &= this.EnableDeadline;
        }
    }

    public class SlideBulletsState
    {
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

        public SlideBulletsState()
        {
            this.EnableSlideBullets = true;
            this.EnableOnlyOnLastSlide = true;
            this.EnableTotalInserts = true;
            this.EnableTotalFinalCost = true;
            this.EnablePageSize = true;
            this.EnableDimensions = true;
            this.EnablePercentOfPage = true;
            this.EnableColumnInches = true;
            this.EnableTotalSquare = true;
            this.EnableAvgAdCost = true;
            this.EnableAvgFinalCost = true;
            this.EnableAvgPCI = true;
            this.EnableTotalColor = true;
            this.EnableDiscounts = true;
            this.EnableDelivery = true;
            this.EnableReadership = true;
            this.EnableSignature = true;

            this.ShowSlideBullets = true;
            this.ShowOnlyOnLastSlide = true;
            this.ShowTotalInserts = true;
            this.ShowTotalFinalCost = true;
            this.ShowPageSize = false;
            this.ShowDimensions = false;
            this.ShowPercentOfPage = false;
            this.ShowColumnInches = false;
            this.ShowTotalSquare = false;
            this.ShowAvgAdCost = false;
            this.ShowAvgFinalCost = false;
            this.ShowAvgPCI = false;
            this.ShowTotalColor = false;
            this.ShowDiscounts = false;
            this.ShowDelivery = false;
            this.ShowReadership = false;
            this.ShowSignature = true;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<EnableSlideBullets>" + this.EnableSlideBullets + @"</EnableSlideBullets>");
            result.AppendLine(@"<EnableOnlyOnLastSlide>" + this.EnableOnlyOnLastSlide + @"</EnableOnlyOnLastSlide>");
            result.AppendLine(@"<EnableTotalInserts>" + this.EnableTotalInserts + @"</EnableTotalInserts>");
            result.AppendLine(@"<EnableTotalFinalCost>" + this.EnableTotalFinalCost + @"</EnableTotalFinalCost>");
            result.AppendLine(@"<EnablePageSize>" + this.EnablePageSize + @"</EnablePageSize>");
            result.AppendLine(@"<EnableDimensions>" + this.EnableDimensions + @"</EnableDimensions>");
            result.AppendLine(@"<EnablePercentOfPage>" + this.EnablePercentOfPage + @"</EnablePercentOfPage>");
            result.AppendLine(@"<EnableColumnInches>" + this.EnableColumnInches + @"</EnableColumnInches>");
            result.AppendLine(@"<EnableTotalSquare>" + this.EnableTotalSquare + @"</EnableTotalSquare>");
            result.AppendLine(@"<EnableAvgAdCost>" + this.EnableAvgAdCost + @"</EnableAvgAdCost>");
            result.AppendLine(@"<EnableAvgFinalCost>" + this.EnableAvgFinalCost + @"</EnableAvgFinalCost>");
            result.AppendLine(@"<EnableAvgPCI>" + this.EnableAvgPCI + @"</EnableAvgPCI>");
            result.AppendLine(@"<EnableTotalColor>" + this.EnableTotalColor + @"</EnableTotalColor>");
            result.AppendLine(@"<EnableDiscounts>" + this.EnableDiscounts + @"</EnableDiscounts>");
            result.AppendLine(@"<EnableDelivery>" + this.EnableDelivery + @"</EnableDelivery>");
            result.AppendLine(@"<EnableReadership>" + this.EnableReadership + @"</EnableReadership>");
            result.AppendLine(@"<EnableSignature>" + this.EnableSignature + @"</EnableSignature>");

            result.AppendLine(@"<ShowSlideBullets>" + this.ShowSlideBullets + @"</ShowSlideBullets>");
            result.AppendLine(@"<ShowOnlyOnLastSlide>" + this.ShowOnlyOnLastSlide + @"</ShowOnlyOnLastSlide>");
            result.AppendLine(@"<ShowTotalInserts>" + this.ShowTotalInserts + @"</ShowTotalInserts>");
            result.AppendLine(@"<ShowTotalFinalCost>" + this.ShowTotalFinalCost + @"</ShowTotalFinalCost>");
            result.AppendLine(@"<ShowPageSize>" + this.ShowPageSize + @"</ShowPageSize>");
            result.AppendLine(@"<ShowDimensions>" + this.ShowDimensions + @"</ShowDimensions>");
            result.AppendLine(@"<ShowPercentOfPage>" + this.ShowPercentOfPage + @"</ShowPercentOfPage>");
            result.AppendLine(@"<ShowColumnInches>" + this.ShowColumnInches + @"</ShowColumnInches>");
            result.AppendLine(@"<ShowTotalSquare>" + this.ShowTotalSquare + @"</ShowTotalSquare>");
            result.AppendLine(@"<ShowAvgAdCost>" + this.ShowAvgAdCost + @"</ShowAvgAdCost>");
            result.AppendLine(@"<ShowAvgFinalCost>" + this.ShowAvgFinalCost + @"</ShowAvgFinalCost>");
            result.AppendLine(@"<ShowAvgPCI>" + this.ShowAvgPCI + @"</ShowAvgPCI>");
            result.AppendLine(@"<ShowTotalColor>" + this.ShowTotalColor + @"</ShowTotalColor>");
            result.AppendLine(@"<ShowDiscounts>" + this.ShowDiscounts + @"</ShowDiscounts>");
            result.AppendLine(@"<ShowDelivery>" + this.ShowDelivery + @"</ShowDelivery>");
            result.AppendLine(@"<ShowReadership>" + this.ShowReadership + @"</ShowReadership>");
            result.AppendLine(@"<ShowSignature>" + this.ShowSignature + @"</ShowSignature>");

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
                            this.EnableSlideBullets = tempBool;
                        break;
                    case "EnableOnlyOnLastSlide":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableOnlyOnLastSlide = tempBool;
                        break;
                    case "EnableTotalInserts":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableTotalInserts = tempBool;
                        break;
                    case "EnableTotalFinalCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableTotalFinalCost = tempBool;
                        break;
                    case "EnablePageSize":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnablePageSize = tempBool;
                        break;
                    case "EnableDimensions":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableDimensions = tempBool;
                        break;
                    case "EnablePercentOfPage":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnablePercentOfPage = tempBool;
                        break;
                    case "EnableColumnInches":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableColumnInches = tempBool;
                        break;
                    case "EnableTotalSquare":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableTotalSquare = tempBool;
                        break;
                    case "EnableAvgAdCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableAvgAdCost = tempBool;
                        break;
                    case "EnableAvgFinalCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableAvgFinalCost = tempBool;
                        break;
                    case "EnableAvgPCI":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableAvgPCI = tempBool;
                        break;
                    case "EnableTotalColor":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableTotalColor = tempBool;
                        break;
                    case "EnableDiscounts":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableDiscounts = tempBool;
                        break;
                    case "EnableDelivery":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableDelivery = tempBool;
                        break;
                    case "EnableReadership":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableReadership = tempBool;
                        break;
                    case "EnableSignature":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableSignature = tempBool;
                        break;

                    case "ShowSlideBullets":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSlideBullets = tempBool;
                        break;
                    case "ShowOnlyOnLastSlide":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowOnlyOnLastSlide = tempBool;
                        break;
                    case "ShowTotalInserts":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalInserts = tempBool;
                        break;
                    case "ShowTotalFinalCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalFinalCost = tempBool;
                        break;
                    case "ShowPageSize":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPageSize = tempBool;
                        break;
                    case "ShowDimensions":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDimensions = tempBool;
                        break;
                    case "ShowPercentOfPage":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPercentOfPage = tempBool;
                        break;
                    case "ShowColumnInches":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowColumnInches = tempBool;
                        break;
                    case "ShowTotalSquare":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalSquare = tempBool;
                        break;
                    case "ShowAvgAdCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAvgAdCost = tempBool;
                        break;
                    case "ShowAvgFinalCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAvgFinalCost = tempBool;
                        break;
                    case "ShowAvgPCI":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAvgPCI = tempBool;
                        break;
                    case "ShowTotalColor":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalColor = tempBool;
                        break;
                    case "ShowDiscounts":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDiscounts = tempBool;
                        break;
                    case "ShowDelivery":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDelivery = tempBool;
                        break;
                    case "ShowReadership":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowReadership = tempBool;
                        break;
                    case "ShowSignature":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSignature = tempBool;
                        break;
                }
            }
            this.ShowSlideBullets &= this.EnableSlideBullets;
            this.ShowOnlyOnLastSlide &= this.EnableOnlyOnLastSlide;
            this.ShowTotalInserts &= this.EnableTotalInserts;
            this.ShowTotalFinalCost &= this.EnableTotalFinalCost;
            this.ShowPageSize &= this.EnablePageSize;
            this.ShowDimensions &= this.EnableDimensions;
            this.ShowPercentOfPage &= this.EnablePercentOfPage;
            this.ShowColumnInches &= this.EnableColumnInches;
            this.ShowTotalSquare &= this.EnableTotalSquare;
            this.ShowAvgAdCost &= this.EnableAvgAdCost;
            this.ShowAvgFinalCost &= this.EnableAvgFinalCost;
            this.ShowAvgPCI &= this.EnableAvgPCI;
            this.ShowTotalColor &= this.EnableTotalColor;
            this.ShowDiscounts &= this.EnableDiscounts;
            this.ShowDelivery &= this.EnableDelivery;
            this.ShowReadership &= this.EnableReadership;
            this.ShowSignature &= this.EnableSignature;
        }
    }

    public class SlideHeaderState
    {
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

        public SlideHeaderState()
        {
            this.EnableSlideInfo = true;
            this.EnableSlideHeader = true;
            this.EnableAdvertiser = true;
            this.EnableDecisionMaker = true;
            this.EnablePresentationDate = true;
            this.EnableFlightDates = true;
            this.EnableName = true;
            this.EnableLogo1 = true;
            this.EnableLogo2 = false;
            this.EnableLogo3 = false;
            this.EnableLogo4 = false;

            this.ShowSlideInfo = true;
            this.ShowSlideHeader = true;
            this.ShowAdvertiser = true;
            this.ShowDecisionMaker = false;
            this.ShowPresentationDate = false;
            this.ShowFlightDates = false;
            this.ShowName = true;
            this.ShowLogo1 = true;
            this.ShowLogo2 = false;
            this.ShowLogo3 = false;
            this.ShowLogo4 = false;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<EnableSlideInfo>" + this.EnableSlideInfo + @"</EnableSlideInfo>");
            result.AppendLine(@"<EnableSlideHeader>" + this.EnableSlideHeader + @"</EnableSlideHeader>");
            result.AppendLine(@"<EnableAdvertiser>" + this.EnableAdvertiser + @"</EnableAdvertiser>");
            result.AppendLine(@"<EnableDecisionMaker>" + this.EnableDecisionMaker + @"</EnableDecisionMaker>");
            result.AppendLine(@"<EnablePresentationDate>" + this.EnablePresentationDate + @"</EnablePresentationDate>");
            result.AppendLine(@"<EnableFlightDates>" + this.EnableFlightDates + @"</EnableFlightDates>");
            result.AppendLine(@"<EnableName>" + this.EnableName + @"</EnableName>");
            result.AppendLine(@"<EnableLogo1>" + this.EnableLogo1 + @"</EnableLogo1>");
            result.AppendLine(@"<EnableLogo2>" + this.EnableLogo2 + @"</EnableLogo2>");
            result.AppendLine(@"<EnableLogo3>" + this.EnableLogo3 + @"</EnableLogo3>");
            result.AppendLine(@"<EnableLogo4>" + this.EnableLogo4 + @"</EnableLogo4>");

            result.AppendLine(@"<ShowSlideInfo>" + this.ShowSlideInfo + @"</ShowSlideInfo>");
            result.AppendLine(@"<ShowSlideHeader>" + this.ShowSlideHeader + @"</ShowSlideHeader>");
            result.AppendLine(@"<ShowAdvertiser>" + this.ShowAdvertiser + @"</ShowAdvertiser>");
            result.AppendLine(@"<ShowDecisionMaker>" + this.ShowDecisionMaker + @"</ShowDecisionMaker>");
            result.AppendLine(@"<ShowPresentationDate>" + this.ShowPresentationDate + @"</ShowPresentationDate>");
            result.AppendLine(@"<ShowFlightDates>" + this.ShowFlightDates + @"</ShowFlightDates>");
            result.AppendLine(@"<ShowName>" + this.ShowName + @"</ShowName>");
            result.AppendLine(@"<ShowLogo1>" + this.ShowLogo1 + @"</ShowLogo1>");
            result.AppendLine(@"<ShowLogo2>" + this.ShowLogo2 + @"</ShowLogo2>");
            result.AppendLine(@"<ShowLogo3>" + this.ShowLogo3 + @"</ShowLogo3>");
            result.AppendLine(@"<ShowLogo4>" + this.ShowLogo4 + @"</ShowLogo4>");

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
                            this.EnableSlideInfo = tempBool;
                        break;
                    case "EnableSlideHeader":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableSlideHeader = tempBool;
                        break;
                    case "EnableAdvertiser":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableAdvertiser = tempBool;
                        break;
                    case "EnableDecisionMaker":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableDecisionMaker = tempBool;
                        break;
                    case "EnablePresentationDate":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnablePresentationDate = tempBool;
                        break;
                    case "EnableFlightDates":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableFlightDates = tempBool;
                        break;
                    case "EnableName":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableName = tempBool;
                        break;
                    case "EnableLogo1":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableLogo1 = tempBool;
                        break;
                    case "EnableLogo2":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableLogo2 = tempBool;
                        break;
                    case "EnableLogo3":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableLogo3 = tempBool;
                        break;
                    case "EnableLogo4":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableLogo4 = tempBool;
                        break;

                    case "ShowSlideInfo":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSlideInfo = tempBool;
                        break;
                    case "ShowSlideHeader":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSlideHeader = tempBool;
                        break;
                    case "ShowAdvertiser":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAdvertiser = tempBool;
                        break;
                    case "ShowDecisionMaker":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDecisionMaker = tempBool;
                        break;
                    case "ShowPresentationDate":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPresentationDate = tempBool;
                        break;
                    case "ShowFlightDates":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowFlightDates = tempBool;
                        break;
                    case "ShowName":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowName = tempBool;
                        break;
                    case "ShowLogo1":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowLogo1 = tempBool;
                        break;
                    case "ShowLogo2":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowLogo2 = tempBool;
                        break;
                    case "ShowLogo3":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowLogo3 = tempBool;
                        break;
                    case "ShowLogo4":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowLogo4 = tempBool;
                        break;
                }
            }
        }
    }

    public class CalendarViewSettings
    {
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

        public bool EnableTitle { get; set; }
        public bool EnableLogo { get; set; }
        public bool EnableBusinessName { get; set; }
        public bool EnableDecisionMaker { get; set; }
        public bool EnableTotalCost { get; set; }
        public bool EnableLegend { get; set; }
        public bool EnableAvgCost { get; set; }
        public bool EnableComments { get; set; }
        public bool EnableTotalAds { get; set; }
        public bool EnableActiveDays { get; set; }

        public bool ShowTitle { get; set; }
        public bool ShowLogo { get; set; }
        public bool ShowBusinessName { get; set; }
        public bool ShowDecisionMaker { get; set; }
        public bool ShowTotalCost { get; set; }
        public bool ShowLegend { get; set; }
        public bool ShowAvgCost { get; set; }
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

        public CalendarViewSettings()
        {
            this.EnableSection = true;
            this.EnableCost = true;
            this.EnableColor = true;
            this.EnableAbbreviationOnly = true;
            this.EnableAdSize = true;
            this.EnablePageSize = true;
            this.EnablePercentOfPage = true;
            this.EnableBigDate = true;

            this.ShowSection = false;
            this.ShowCost = true;
            this.ShowColor = false;
            this.ShowAbbreviationOnly = false;
            this.ShowAdSize = false;
            this.ShowPageSize = false;
            this.ShowPercentOfPage = false;
            this.ShowBigDate = true;

            this.EnableTitle = true;
            this.EnableLogo = true;
            this.EnableBusinessName = true;
            this.EnableDecisionMaker = true;
            this.EnableTotalCost = true;
            this.EnableLegend = true;
            this.EnableAvgCost = true;
            this.EnableComments = true;
            this.EnableTotalAds = true;
            this.EnableActiveDays = true;

            this.ShowTitle = true;
            this.ShowLogo = true;
            this.ShowBusinessName = true;
            this.ShowDecisionMaker = true;
            this.ShowTotalCost = false;
            this.ShowLegend = false;
            this.ShowAvgCost = false;
            this.ShowComments = false;
            this.ShowTotalAds = false;
            this.ShowActiveDays = false;

            this.EnableGray = true;
            this.EnableBlack = true;
            this.EnableBlue = true;
            this.EnableTeal = true;
            this.EnableOrange = true;
            this.EnableGreen = true;
            this.SlideColor = "gray";
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<EnableSection>" + this.EnableSection + @"</EnableSection>");
            result.AppendLine(@"<EnableCost>" + this.EnableCost + @"</EnableCost>");
            result.AppendLine(@"<EnableColor>" + this.EnableColor + @"</EnableColor>");
            result.AppendLine(@"<EnableAbbreviationOnly>" + this.EnableAbbreviationOnly + @"</EnableAbbreviationOnly>");
            result.AppendLine(@"<EnableAdSize>" + this.EnableAdSize + @"</EnableAdSize>");
            result.AppendLine(@"<EnablePageSize>" + this.EnablePageSize + @"</EnablePageSize>");
            result.AppendLine(@"<EnablePercentOfPage>" + this.EnablePercentOfPage + @"</EnablePercentOfPage>");
            result.AppendLine(@"<EnableBigDate>" + this.EnableBigDate + @"</EnableBigDate>");

            result.AppendLine(@"<ShowSection>" + this.ShowSection + @"</ShowSection>");
            result.AppendLine(@"<ShowCost>" + this.ShowCost + @"</ShowCost>");
            result.AppendLine(@"<ShowColor>" + this.ShowColor + @"</ShowColor>");
            result.AppendLine(@"<ShowAbbreviationOnly>" + this.ShowAbbreviationOnly + @"</ShowAbbreviationOnly>");
            result.AppendLine(@"<ShowAdSize>" + this.ShowAdSize + @"</ShowAdSize>");
            result.AppendLine(@"<ShowPageSize>" + this.ShowPageSize + @"</ShowPageSize>");
            result.AppendLine(@"<ShowPercentOfPage>" + this.ShowPercentOfPage + @"</ShowPercentOfPage>");
            result.AppendLine(@"<ShowBigDate>" + this.ShowBigDate + @"</ShowBigDate>");

            result.AppendLine(@"<EnableTitle>" + this.EnableTitle + @"</EnableTitle>");
            result.AppendLine(@"<EnableLogo>" + this.EnableLogo + @"</EnableLogo>");
            result.AppendLine(@"<EnableBusinessName>" + this.EnableBusinessName + @"</EnableBusinessName>");
            result.AppendLine(@"<EnableDecisionMaker>" + this.EnableDecisionMaker + @"</EnableDecisionMaker>");
            result.AppendLine(@"<EnableTotalCost>" + this.EnableTotalCost + @"</EnableTotalCost>");
            result.AppendLine(@"<EnableLegend>" + this.EnableLegend + @"</EnableLegend>");
            result.AppendLine(@"<EnableAvgCost>" + this.EnableAvgCost + @"</EnableAvgCost>");
            result.AppendLine(@"<EnableComments>" + this.EnableComments + @"</EnableComments>");
            result.AppendLine(@"<EnableTotalAds>" + this.EnableTotalAds + @"</EnableTotalAds>");
            result.AppendLine(@"<EnableActiveDays>" + this.EnableActiveDays + @"</EnableActiveDays>");

            result.AppendLine(@"<ShowTitle>" + this.ShowTitle + @"</ShowTitle>");
            result.AppendLine(@"<ShowLogo>" + this.ShowLogo + @"</ShowLogo>");
            result.AppendLine(@"<ShowBusinessName>" + this.ShowBusinessName + @"</ShowBusinessName>");
            result.AppendLine(@"<ShowDecisionMaker>" + this.ShowDecisionMaker + @"</ShowDecisionMaker>");
            result.AppendLine(@"<ShowTotalCost>" + this.ShowTotalCost + @"</ShowTotalCost>");
            result.AppendLine(@"<ShowLegend>" + this.ShowLegend + @"</ShowLegend>");
            result.AppendLine(@"<ShowAvgCost>" + this.ShowAvgCost + @"</ShowAvgCost>");
            result.AppendLine(@"<ShowComments>" + this.ShowComments + @"</ShowComments>");
            result.AppendLine(@"<ShowTotalAds>" + this.ShowTotalAds + @"</ShowTotalAds>");
            result.AppendLine(@"<ShowActiveDays>" + this.ShowActiveDays + @"</ShowActiveDays>");

            result.AppendLine(@"<EnableGray>" + this.EnableGray + @"</EnableGray>");
            result.AppendLine(@"<EnableBlack>" + this.EnableBlack + @"</EnableBlack>");
            result.AppendLine(@"<EnableBlue>" + this.EnableBlue + @"</EnableBlue>");
            result.AppendLine(@"<EnableTeal>" + this.EnableTeal + @"</EnableTeal>");
            result.AppendLine(@"<EnableOrange>" + this.EnableOrange + @"</EnableOrange>");
            result.AppendLine(@"<EnableGreen>" + this.EnableGreen + @"</EnableGreen>");
            result.AppendLine(@"<SlideColor>" + this.SlideColor.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideColor>");

            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            DateTime tempDate = DateTime.MinValue;
            bool tempBool = false;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "EnableSection":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableSection = tempBool;
                        break;
                    case "EnableCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableCost = tempBool;
                        break;
                    case "EnableColor":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableColor = tempBool;
                        break;
                    case "EnableAbbreviationOnly":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableAbbreviationOnly = tempBool;
                        break;
                    case "EnableAdSize":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableAdSize = tempBool;
                        break;
                    case "EnablePageSize":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnablePageSize = tempBool;
                        break;
                    case "EnablePercentOfPage":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnablePercentOfPage = tempBool;
                        break;
                    case "EnableBigDate":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableBigDate = tempBool;
                        break;

                    case "ShowSection":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSection = tempBool;
                        break;
                    case "ShowCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowCost = tempBool;
                        break;
                    case "ShowColor":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowColor = tempBool;
                        break;
                    case "ShowAbbreviationOnly":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAbbreviationOnly = tempBool;
                        break;
                    case "ShowAdSize":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAdSize = tempBool;
                        break;
                    case "ShowPageSize":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPageSize = tempBool;
                        break;
                    case "ShowPercentOfPage":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPercentOfPage = tempBool;
                        break;
                    case "ShowBigDate":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowBigDate = tempBool;
                        break;

                    case "EnableTitle":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableTitle = tempBool;
                        break;
                    case "EnableLogo":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableLogo = tempBool;
                        break;
                    case "EnableBusinessName":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableBusinessName = tempBool;
                        break;
                    case "EnableDecisionMaker":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableDecisionMaker = tempBool;
                        break;
                    case "EnableTotalCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableTotalCost = tempBool;
                        break;
                    case "EnableLegend":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableLegend = tempBool;
                        break;
                    case "EnableAvgCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableAvgCost = tempBool;
                        break;
                    case "EnableComments":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableComments = tempBool;
                        break;
                    case "EnableTotalAds":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableTotalAds = tempBool;
                        break;
                    case "EnableActiveDays":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableActiveDays = tempBool;
                        break;

                    case "ShowTitle":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTitle = tempBool;
                        break;
                    case "ShowLogo":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowLogo = tempBool;
                        break;
                    case "ShowBusinessName":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowBusinessName = tempBool;
                        break;
                    case "ShowDecisionMaker":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDecisionMaker = tempBool;
                        break;
                    case "ShowTotalCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalCost = tempBool;
                        break;
                    case "ShowLegend":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowLegend = tempBool;
                        break;
                    case "ShowAvgCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAvgCost = tempBool;
                        break;
                    case "ShowComments":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowComments = tempBool;
                        break;
                    case "ShowTotalAds":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalAds = tempBool;
                        break;
                    case "ShowActiveDays":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowActiveDays = tempBool;
                        break;

                    case "EnableGray":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableGray = tempBool;
                        break;
                    case "EnableBlack":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableBlack = tempBool;
                        break;
                    case "EnableBlue":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableBlue = tempBool;
                        break;
                    case "EnableTeal":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableTeal = tempBool;
                        break;
                    case "EnableOrange":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableOrange = tempBool;
                        break;
                    case "EnableGreen":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableGreen = tempBool;
                        break;
                    case "SlideColor":
                        this.SlideColor = childNode.InnerText;
                        break;
                }
            }

            this.ShowSection &= this.EnableSection;
            this.ShowCost &= this.EnableCost;
            this.ShowColor &= this.EnableColor;
            this.ShowAbbreviationOnly &= this.EnableAbbreviationOnly;
            this.ShowAdSize &= this.EnableAdSize;
            this.ShowPageSize &= this.EnablePageSize;
            this.ShowPercentOfPage &= this.EnablePercentOfPage;
            this.ShowBigDate &= this.EnableBigDate;

            this.ShowTitle &= this.EnableTitle;
            this.ShowLogo &= this.EnableLogo;
            this.ShowBusinessName &= this.EnableBusinessName;
            this.ShowDecisionMaker &= this.EnableDecisionMaker;
            this.ShowTotalCost &= this.EnableTotalCost;
            this.ShowLegend &= this.EnableLegend;
            this.ShowAvgCost &= this.EnableAvgCost;
            this.ShowComments &= this.EnableComments;
            this.ShowTotalAds &= this.EnableTotalAds;
            this.ShowActiveDays &= this.EnableActiveDays;
        }
    }
}
