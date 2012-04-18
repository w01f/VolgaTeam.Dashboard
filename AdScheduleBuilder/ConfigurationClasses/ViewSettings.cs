using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace AdScheduleBuilder.ConfigurationClasses
{
    public class ScheduleViewSettings
    {
        public HomeViewSettings HomeViewSettings { get; set; }

        public MultiSummaryViewSettings MultiSummaryViewSettings { get; set; }
        public SnapshotViewSettings SnapshotViewSettings { get; set; }

        public DetailedGridViewSettings DetailedGridViewSettings { get; set; }
        public MultiGridViewSettings MultiGridViewSettings { get; set; }
        public ChronoGridViewSettings ChronoGridViewSettings { get; set; }
        public bool ShowGridDetails { get; set; }

        public CalendarViewSettings CalendarViewSettings { get; set; }

        public ScheduleViewSettings()
        {
            this.HomeViewSettings = new HomeViewSettings();

            this.MultiSummaryViewSettings = new MultiSummaryViewSettings();
            this.SnapshotViewSettings = new SnapshotViewSettings();

            this.DetailedGridViewSettings = new DetailedGridViewSettings();
            this.MultiGridViewSettings = new MultiGridViewSettings();
            this.ChronoGridViewSettings = new ChronoGridViewSettings();

            this.CalendarViewSettings = new CalendarViewSettings();

            LoadDefaultSettings();
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<HomeViewSettings>" + this.HomeViewSettings.Serialize() + @"</HomeViewSettings>");

            result.AppendLine(@"<MultiSummaryViewSettings>" + this.MultiSummaryViewSettings.Serialize() + @"</MultiSummaryViewSettings>");
            result.AppendLine(@"<SnapshotViewSettings>" + this.SnapshotViewSettings.Serialize() + @"</SnapshotViewSettings>");

            result.AppendLine(@"<DetailedGridViewSettings>" + this.DetailedGridViewSettings.Serialize() + @"</DetailedGridViewSettings>");
            result.AppendLine(@"<MultiGridViewSettings>" + this.MultiGridViewSettings.Serialize() + @"</MultiGridViewSettings>");
            result.AppendLine(@"<ChronoGridViewSettings>" + this.ChronoGridViewSettings.Serialize() + @"</ChronoGridViewSettings>");
            result.AppendLine(@"<ShowGridDetails>" + this.ShowGridDetails.ToString() + @"</ShowGridDetails>");

            result.AppendLine(@"<CalendarViewSettings>" + this.CalendarViewSettings.Serialize() + @"</CalendarViewSettings>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool;
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "HomeViewSettings":
                        this.HomeViewSettings.Deserialize(childNode);
                        break;
                    case "MultiSummaryViewSettings":
                        this.MultiSummaryViewSettings.Deserialize(childNode);
                        break;
                    case "SnapshotViewSettings":
                        this.SnapshotViewSettings.Deserialize(childNode);
                        break;
                    case "DetailedGridViewSettings":
                        this.DetailedGridViewSettings.Deserialize(childNode);
                        break;
                    case "MultiGridViewSettings":
                        this.MultiGridViewSettings.Deserialize(childNode);
                        break;
                    case "ChronoGridViewSettings":
                        this.ChronoGridViewSettings.Deserialize(childNode);
                        break;
                    case "ShowGridDetails":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowGridDetails = tempBool;
                        break;
                    case "CalendarViewSettings":
                        this.CalendarViewSettings.Deserialize(childNode);
                        break;
                }
            }
        }

        private void LoadDefaultSettings()
        {
            XmlNode node;
            bool tempBool;
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.LocalSettingsPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(ConfigurationClasses.SettingsManager.Instance.LocalSettingsPath);

                node = document.SelectSingleNode(@"/AdScheduleSettings/ShowGridDetails");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.ShowGridDetails = tempBool;
            }
        }

        public void SaveDefaultViewSettings()
        {
            StringBuilder xml = new StringBuilder();

            xml.AppendLine(@"<AdScheduleSettings>");
            xml.AppendLine(this.Serialize());
            xml.AppendLine(@"</AdScheduleSettings>");

            using (StreamWriter sw = new StreamWriter(ConfigurationClasses.SettingsManager.Instance.LocalSettingsPath, false))
            {
                sw.Write(xml);
                sw.Flush();
            }
        }
    }

    public class PublicationViewSettings
    {
        public PublicationBasicOverviewSettings BasicOverviewSettings { get; set; }
        public PublicationMultiSummarySettings MultiSummarySettings { get; set; }
        public PublicationDetailedGridSettings DetailedGridSettings { get; set; }

        public PublicationViewSettings()
        {
            this.BasicOverviewSettings = new PublicationBasicOverviewSettings();
            this.MultiSummarySettings = new PublicationMultiSummarySettings();
            this.DetailedGridSettings = new PublicationDetailedGridSettings();
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<BasicOverviewSettings>" + this.BasicOverviewSettings.Serialize() + @"</BasicOverviewSettings>");
            result.AppendLine(@"<MultiSummarySettings>" + this.MultiSummarySettings.Serialize() + @"</MultiSummarySettings>");
            result.AppendLine(@"<DetailedGridSettings>" + this.DetailedGridSettings.Serialize() + @"</DetailedGridSettings>");

            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "BasicOverviewSettings":
                        this.BasicOverviewSettings.Deserialize(childNode);
                        break;
                    case "MultiSummarySettings":
                        this.MultiSummarySettings.Deserialize(childNode);
                        break;
                    case "DetailedGridSettings":
                        this.DetailedGridSettings.Deserialize(childNode);
                        break;
                }
            }
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

        public bool ShowAdSizeDetails { get; set; }
        public bool ShowDimensions { get; set; }
        public bool ShowPageSize { get; set; }
        public bool ShowPercentOfPage { get; set; }
        public bool ShowSquare { get; set; }
        public bool ShowColor { get; set; }
        public bool ShowMechanicals { get; set; }

        public bool ShowTotalDetails { get; set; }
        public bool ShowTotalInserts { get; set; }
        public bool ShowTotalSquare { get; set; }

        public bool ShowInvestmentDetails { get; set; }
        public bool ShowAvgAdCost { get; set; }
        public bool ShowAvgPCI { get; set; }
        public bool ShowDiscounts { get; set; }
        public bool ShowInvestment { get; set; }

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

            this.ShowAdSizeDetails = true;
            this.ShowDimensions = true;
            this.ShowPageSize = true;
            this.ShowPercentOfPage = true;
            this.ShowSquare = true;
            this.ShowColor = true;
            this.ShowMechanicals = true;

            this.ShowTotalDetails = true;
            this.ShowTotalInserts = true;
            this.ShowTotalSquare = true;

            this.ShowInvestmentDetails = true;
            this.ShowAvgAdCost = true;
            this.ShowAvgPCI = true;
            this.ShowDiscounts = true;
            this.ShowInvestment = true;

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

            result.AppendLine(@"<ShowAdSizeDetails>" + this.ShowAdSizeDetails + @"</ShowAdSizeDetails>");
            result.AppendLine(@"<ShowDimensions>" + this.ShowDimensions + @"</ShowDimensions>");
            result.AppendLine(@"<ShowPageSize>" + this.ShowPageSize + @"</ShowPageSize>");
            result.AppendLine(@"<ShowPercentOfPage>" + this.ShowPercentOfPage + @"</ShowPercentOfPage>");
            result.AppendLine(@"<ShowSquare>" + this.ShowSquare + @"</ShowSquare>");
            result.AppendLine(@"<ShowColor>" + this.ShowColor + @"</ShowColor>");
            result.AppendLine(@"<ShowMechanicals>" + this.ShowMechanicals + @"</ShowMechanicals>");

            result.AppendLine(@"<ShowTotalDetails>" + this.ShowTotalDetails + @"</ShowTotalDetails>");
            result.AppendLine(@"<ShowTotalInserts>" + this.ShowTotalInserts + @"</ShowTotalInserts>");
            result.AppendLine(@"<ShowTotalSquare>" + this.ShowTotalSquare + @"</ShowTotalSquare>");

            result.AppendLine(@"<ShowInvestmentDetails>" + this.ShowInvestmentDetails + @"</ShowInvestmentDetails>");
            result.AppendLine(@"<ShowAvgAdCost>" + this.ShowAvgAdCost + @"</ShowAvgAdCost>");
            result.AppendLine(@"<ShowAvgPCI>" + this.ShowAvgPCI + @"</ShowAvgPCI>");
            result.AppendLine(@"<ShowDiscounts>" + this.ShowDiscounts + @"</ShowDiscounts>");
            result.AppendLine(@"<ShowInvestment>" + this.ShowInvestment + @"</ShowInvestment>");

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
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowName = tempBool;
                        break;
                    case "ShowLogo":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowLogo = tempBool;
                        break;
                    case "ShowFlightDates":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowFlightDates = tempBool;
                        break;
                    case "ShowAdvertiser":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowAdvertiser = tempBool;
                        break;
                    case "ShowDecisionMaker":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowDecisionMaker = tempBool;
                        break;
                    case "ShowPresentationDate":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowPresentationDate = tempBool;
                        break;
                    case "ShowSlideHeader":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowSlideHeader = tempBool;
                        break;
                    case "SlideHeader":
                        this.SlideHeader = childNode.InnerText;
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
                    case "ShowMechanicals":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowMechanicals = tempBool;
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

                    case "ShowInvestmentDetails":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
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
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowInvestment = tempBool;
                        break;

                    case "ShowDateDetails":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowDateDetails = tempBool;
                        break;
                    case "ShowFlightDates2":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowFlightDates2 = tempBool;
                        break;
                    case "ShowDates":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowDates = tempBool;
                        break;
                    case "ShowComments":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowComments = tempBool;
                        break;
                    case "Comments":
                        this.Comments = childNode.InnerText;
                        break;
                }
            }
        }
    }

    public class PublicationMultiSummarySettings
    {
        public bool ShowName { get; set; }
        public bool ShowLogo { get; set; }
        public bool ShowInvestment { get; set; }
        public bool ShowFlightDates { get; set; }
        public bool ShowDates { get; set; }
        public bool ShowComments { get; set; }

        public bool ShowTotalInserts { get; set; }
        public bool ShowTotalSquare { get; set; }
        public bool ShowSquare { get; set; }
        public bool ShowDimensions { get; set; }
        public bool ShowTotalColor { get; set; }
        public bool ShowAvgPCI { get; set; }
        public bool ShowAvgAdCost { get; set; }
        public bool ShowAvgFinalCost { get; set; }
        public bool ShowDiscounts { get; set; }
        public bool ShowPageSize { get; set; }
        public bool ShowPercentOfPage { get; set; }
        public bool ShowMechanicals { get; set; }
        public bool ShowSection { get; set; }

        public string InvestmentType { get; set; }
        public string Comments { get; set; }

        public PublicationMultiSummarySettings()
        {
            this.ShowName = true;
            this.ShowLogo = true;
            this.ShowInvestment = true;
            this.ShowFlightDates = true;
            this.ShowDates = true;
            this.ShowComments = false;

            this.ShowTotalInserts = true;
            this.ShowTotalSquare = true;
            this.ShowSquare = true;
            this.ShowDimensions = true;
            this.ShowPageSize = true;
            this.ShowPercentOfPage = true;
            this.ShowTotalColor = false;
            this.ShowAvgAdCost = false;
            this.ShowAvgFinalCost = false;
            this.ShowAvgPCI = false;
            this.ShowDiscounts = false;
            this.ShowMechanicals = false;
            this.ShowSection = false;

            this.InvestmentType = "Total";
            this.Comments = string.Empty;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<ShowName>" + this.ShowName + @"</ShowName>");
            result.AppendLine(@"<ShowLogo>" + this.ShowLogo + @"</ShowLogo>");
            result.AppendLine(@"<ShowInvestment>" + this.ShowInvestment + @"</ShowInvestment>");
            result.AppendLine(@"<ShowFlightDates>" + this.ShowFlightDates + @"</ShowFlightDates>");
            result.AppendLine(@"<ShowDates>" + this.ShowDates + @"</ShowDates>");
            result.AppendLine(@"<ShowComments>" + this.ShowComments + @"</ShowComments>");

            result.AppendLine(@"<ShowAvgAdCost>" + this.ShowAvgAdCost + @"</ShowAvgAdCost>");
            result.AppendLine(@"<ShowAvgFinalCost>" + this.ShowAvgFinalCost + @"</ShowAvgFinalCost>");
            result.AppendLine(@"<ShowAvgPCI>" + this.ShowAvgPCI + @"</ShowAvgPCI>");
            result.AppendLine(@"<ShowSquare>" + this.ShowSquare + @"</ShowSquare>");
            result.AppendLine(@"<ShowPageSize>" + this.ShowPageSize + @"</ShowPageSize>");
            result.AppendLine(@"<ShowPercentOfPage>" + this.ShowPercentOfPage + @"</ShowPercentOfPage>");
            result.AppendLine(@"<ShowDimensions>" + this.ShowDimensions + @"</ShowDimensions>");
            result.AppendLine(@"<ShowDiscounts>" + this.ShowDiscounts + @"</ShowDiscounts>");
            result.AppendLine(@"<ShowMechanicals>" + this.ShowMechanicals + @"</ShowMechanicals>");
            result.AppendLine(@"<ShowSection>" + this.ShowSection + @"</ShowSection>");
            result.AppendLine(@"<ShowTotalColor>" + this.ShowTotalColor + @"</ShowTotalColor>");
            result.AppendLine(@"<ShowTotalInserts>" + this.ShowTotalInserts + @"</ShowTotalInserts>");
            result.AppendLine(@"<ShowTotalSquare>" + this.ShowTotalSquare + @"</ShowTotalSquare>");

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
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowName = tempBool;
                        break;
                    case "ShowLogo":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowLogo = tempBool;
                        break;
                    case "ShowInvestment":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowInvestment = tempBool;
                        break;
                    case "ShowFlightDates":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowFlightDates = tempBool;
                        break;
                    case "ShowDates":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowDates = tempBool;
                        break;
                    case "ShowComments":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowComments = tempBool;
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
                    case "ShowSquare":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSquare = tempBool;
                        break;
                    case "ShowPercentOfPage":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPercentOfPage = tempBool;
                        break;
                    case "ShowPageSize":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPageSize = tempBool;
                        break;
                    case "ShowDimensions":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDimensions = tempBool;
                        break;
                    case "ShowDiscounts":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDiscounts = tempBool;
                        break;
                    case "ShowMechanicals":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowMechanicals = tempBool;
                        break;
                    case "ShowSection":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSection = tempBool;
                        break;
                    case "ShowTotalColor":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalColor = tempBool;
                        break;
                    case "ShowTotalInserts":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalInserts = tempBool;
                        break;
                    case "ShowTotalSquare":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalSquare = tempBool;
                        break;

                    case "InvestmentType":
                        this.InvestmentType = childNode.InnerText;
                        break;
                    case "Comments":
                        this.Comments = childNode.InnerText;
                        break;
                }
            }
        }
    }

    public class PublicationDetailedGridSettings
    {
        public string SlideHeader { get; set; }

        public PublicationDetailedGridSettings()
        {
            this.SlideHeader = string.Empty;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<SlideHeader>" + this.SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "SlideHeader":
                        this.SlideHeader = childNode.InnerText;
                        break;
                }
            }
        }
    }

    public class HomeViewSettings
    {
        public bool ShowDelivery { get; set; }
        public bool ShowReadership { get; set; }
        public bool ShowLogo { get; set; }
        public bool ShowCode { get; set; }

        public HomeViewSettings()
        {
            this.ShowDelivery = false;
            this.ShowReadership = false;
            this.ShowLogo = true;
            this.ShowCode = true;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

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
        }
    }

    public class MultiSummaryViewSettings
    {
        public bool ShowSlideHeader { get; set; }
        public bool ShowPresentationDate { get; set; }
        public bool ShowAdvertiser { get; set; }
        public bool ShowDecisionMaker { get; set; }
        public bool ShowFlightDates { get; set; }
        public bool ShowOnePublicationPerSlide { get; set; }

        public string SlideHeader { get; set; }

        public MultiSummaryViewSettings()
        {
            this.ShowSlideHeader = true;
            this.ShowPresentationDate = true;
            this.ShowAdvertiser = true;
            this.ShowDecisionMaker = true;
            this.ShowFlightDates = true;
            this.ShowOnePublicationPerSlide = true;

            this.SlideHeader = string.Empty;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<ShowAdvertiser>" + this.ShowAdvertiser + @"</ShowAdvertiser>");
            result.AppendLine(@"<ShowDecisionMaker>" + this.ShowDecisionMaker + @"</ShowDecisionMaker>");
            result.AppendLine(@"<ShowFlightDates>" + this.ShowFlightDates + @"</ShowFlightDates>");
            result.AppendLine(@"<ShowPresentationDate>" + this.ShowPresentationDate + @"</ShowPresentationDate>");
            result.AppendLine(@"<ShowOnePublicationPerSlide>" + this.ShowOnePublicationPerSlide + @"</ShowOnePublicationPerSlide>");
            result.AppendLine(@"<ShowSlideHeader>" + this.ShowSlideHeader + @"</ShowSlideHeader>");

            result.AppendLine(@"<SlideHeader>" + this.SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");

            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool = false;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "ShowAdvertiser":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowAdvertiser = tempBool;
                        break;
                    case "ShowDecisionMaker":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowDecisionMaker = tempBool;
                        break;
                    case "ShowFlightDates":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowFlightDates = tempBool;
                        break;
                    case "ShowPresentationDate":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowPresentationDate = tempBool;
                        break;
                    case "ShowOnePublicationPerSlide":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowOnePublicationPerSlide = tempBool;
                        break;
                    case "ShowSlideHeader":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowSlideHeader = tempBool;
                        break;
                    case "SlideHeader":
                        this.SlideHeader = childNode.InnerText;
                        break;
                }
            }
        }
    }

    public class SnapshotViewSettings
    {
        public bool ShowSlideHeader { get; set; }
        public bool ShowPresentationDate { get; set; }
        public bool ShowAdvertiser { get; set; }
        public bool ShowDecisionMaker { get; set; }
        public bool ShowFlightDates { get; set; }

        public bool ShowLogo { get; set; }
        public bool ShowTotalInserts { get; set; }
        public bool ShowPageSize { get; set; }
        public bool ShowPercentOfPage { get; set; }
        public bool ShowDimensions { get; set; }
        public bool ShowSquare { get; set; }
        public bool ShowTotalSquare { get; set; }
        public bool ShowAvgPCI { get; set; }
        public bool ShowAvgCost { get; set; }
        public bool ShowAvgFinalCost { get; set; }
        public bool ShowTotalFinalCost { get; set; }
        public bool ShowTotalColor { get; set; }
        public bool ShowTotalDiscounts { get; set; }
        public bool ShowReadership { get; set; }
        public bool ShowDelivery { get; set; }

        public string SlideHeader { get; set; }

        public SnapshotViewSettings()
        {
            this.ShowSlideHeader = true;
            this.ShowPresentationDate = true;
            this.ShowAdvertiser = true;
            this.ShowDecisionMaker = true;
            this.ShowFlightDates = true;

            this.ShowLogo = true;
            this.ShowTotalInserts = true;
            this.ShowPageSize = false;
            this.ShowDimensions = false;
            this.ShowSquare = false;
            this.ShowPercentOfPage = false;
            this.ShowTotalSquare = false;
            this.ShowAvgPCI = false;
            this.ShowAvgCost = false;
            this.ShowAvgFinalCost = true;
            this.ShowTotalFinalCost = true;
            this.ShowTotalColor = false;
            this.ShowTotalDiscounts = false;
            this.ShowReadership = false;
            this.ShowDelivery = false;

            this.SlideHeader = string.Empty;

            LoadDefaultSettings();
        }

        private void LoadDefaultSettings()
        {
            XmlNode node;
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.LocalSettingsPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(ConfigurationClasses.SettingsManager.Instance.LocalSettingsPath);

                node = document.SelectSingleNode(@"/AdScheduleSettings/SnapshotViewSettings");
                if (node != null)
                    this.Deserialize(node);
            }
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<ShowAdvertiser>" + this.ShowAdvertiser + @"</ShowAdvertiser>");
            result.AppendLine(@"<ShowDecisionMaker>" + this.ShowDecisionMaker + @"</ShowDecisionMaker>");
            result.AppendLine(@"<ShowFlightDates>" + this.ShowFlightDates + @"</ShowFlightDates>");
            result.AppendLine(@"<ShowPresentationDate>" + this.ShowPresentationDate + @"</ShowPresentationDate>");
            result.AppendLine(@"<ShowSlideHeader>" + this.ShowSlideHeader + @"</ShowSlideHeader>");
            result.AppendLine(@"<ShowAvgCost>" + this.ShowAvgCost + @"</ShowAvgCost>");
            result.AppendLine(@"<ShowAvgFinalCost>" + this.ShowAvgFinalCost + @"</ShowAvgFinalCost>");
            result.AppendLine(@"<ShowAvgPCI>" + this.ShowAvgPCI + @"</ShowAvgPCI>");
            result.AppendLine(@"<ShowDelivery>" + this.ShowDelivery + @"</ShowDelivery>");
            result.AppendLine(@"<ShowDimensions>" + this.ShowDimensions + @"</ShowDimensions>");
            result.AppendLine(@"<ShowLogo>" + this.ShowLogo + @"</ShowLogo>");
            result.AppendLine(@"<ShowPageSize>" + this.ShowPageSize + @"</ShowPageSize>");
            result.AppendLine(@"<ShowPercentOfPage>" + this.ShowPercentOfPage + @"</ShowPercentOfPage>");
            result.AppendLine(@"<ShowReadership>" + this.ShowReadership + @"</ShowReadership>");
            result.AppendLine(@"<ShowSquare>" + this.ShowSquare + @"</ShowSquare>");
            result.AppendLine(@"<ShowTotalColor>" + this.ShowTotalColor + @"</ShowTotalColor>");
            result.AppendLine(@"<ShowTotalDiscounts>" + this.ShowTotalDiscounts + @"</ShowTotalDiscounts>");
            result.AppendLine(@"<ShowTotalFinalCost>" + this.ShowTotalFinalCost + @"</ShowTotalFinalCost>");
            result.AppendLine(@"<ShowTotalInserts>" + this.ShowTotalInserts + @"</ShowTotalInserts>");
            result.AppendLine(@"<ShowTotalSquare>" + this.ShowTotalSquare + @"</ShowTotalSquare>");

            result.AppendLine(@"<SlideHeader>" + this.SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");

            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool = false;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "ShowAdvertiser":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowAdvertiser = tempBool;
                        break;
                    case "ShowDecisionMaker":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowDecisionMaker = tempBool;
                        break;
                    case "ShowFlightDates":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowFlightDates = tempBool;
                        break;
                    case "ShowPresentationDate":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowPresentationDate = tempBool;
                        break;
                    case "ShowSlideHeader":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowSlideHeader = tempBool;
                        break;
                    case "ShowAvgCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAvgCost = tempBool;
                        break;
                    case "ShowAvgFinalCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAvgFinalCost = tempBool;
                        break;
                    case "ShowAvgPCI":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAvgPCI = tempBool;
                        break;
                    case "ShowDelivery":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDelivery = tempBool;
                        break;
                    case "ShowDimensions":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDimensions = tempBool;
                        break;
                    case "ShowLogo":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowLogo = tempBool;
                        break;
                    case "ShowPageSize":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPageSize = tempBool;
                        break;
                    case "ShowPercentOfPage":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPercentOfPage = tempBool;
                        break;
                    case "ShowReadership":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowReadership = tempBool;
                        break;
                    case "ShowSquare":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSquare = tempBool;
                        break;
                    case "ShowTotalColor":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalColor = tempBool;
                        break;
                    case "ShowTotalDiscounts":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalDiscounts = tempBool;
                        break;
                    case "ShowTotalFinalCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalFinalCost = tempBool;
                        break;
                    case "ShowTotalInserts":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalInserts = tempBool;
                        break;
                    case "ShowTotalSquare":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalSquare = tempBool;
                        break;
                    case "SlideHeader":
                        this.SlideHeader = childNode.InnerText;
                        break;
                }
            }
        }
    }

    public class DetailedGridViewSettings
    {
        public GridColumnsState GridColumnsState { get; set; }
        public AdNotesState AdNotesState { get; set; }
        public SlideBulletsState SlideBulletsState { get; set; }
        public SlideHeaderState SlideHeaderState { get; set; }

        public DetailedGridViewSettings()
        {
            this.GridColumnsState = new GridColumnsState();
            this.AdNotesState = new AdNotesState();
            this.SlideBulletsState = new SlideBulletsState();
            this.SlideHeaderState = new SlideHeaderState();
            this.GridColumnsState.ShowID = true;
            LoadDefaultSettings();
        }

        private void LoadDefaultSettings()
        {
            XmlNode node;
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.LocalSettingsPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(ConfigurationClasses.SettingsManager.Instance.LocalSettingsPath);

                node = document.SelectSingleNode(@"/AdScheduleSettings/DetailedGridViewSettings");
                if (node != null)
                    this.Deserialize(node);
            }
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<GridColumnsState>" + this.GridColumnsState.Serialize() + @"</GridColumnsState>");
            result.AppendLine(@"<AdNotesState>" + this.AdNotesState.Serialize() + @"</AdNotesState>");
            result.AppendLine(@"<SlideBulletsState>" + this.SlideBulletsState.Serialize() + @"</SlideBulletsState>");
            result.AppendLine(@"<SlideHeaderState>" + this.SlideHeaderState.Serialize() + @"</SlideHeaderState>");

            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "GridColumnsState":
                        this.GridColumnsState.Deserialize(childNode);
                        break;
                    case "AdNotesState":
                        this.AdNotesState.Deserialize(childNode);
                        break;
                    case "SlideBulletsState":
                        this.SlideBulletsState.Deserialize(childNode);
                        break;
                    case "SlideHeaderState":
                        this.SlideHeaderState.Deserialize(childNode);
                        break;
                }
            }
        }
    }

    public class MultiGridViewSettings
    {
        public GridColumnsState GridColumnsState { get; set; }
        public AdNotesState AdNotesState { get; set; }
        public SlideBulletsState SlideBulletsState { get; set; }
        public SlideHeaderState SlideHeaderState { get; set; }


        public string SlideHeader { get; set; }

        public MultiGridViewSettings()
        {
            this.GridColumnsState = new GridColumnsState();
            this.AdNotesState = new AdNotesState();
            this.SlideBulletsState = new SlideBulletsState();
            this.SlideHeaderState = new SlideHeaderState();

            this.GridColumnsState.ShowID = true;
            this.SlideHeader = string.Empty;

            LoadDefaultSettings();
        }

        private void LoadDefaultSettings()
        {
            XmlNode node;
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.LocalSettingsPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(ConfigurationClasses.SettingsManager.Instance.LocalSettingsPath);

                node = document.SelectSingleNode(@"/AdScheduleSettings/MultiGridViewSettings");
                if (node != null)
                    this.Deserialize(node);
            }
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<GridColumnsState>" + this.GridColumnsState.Serialize() + @"</GridColumnsState>");
            result.AppendLine(@"<AdNotesState>" + this.AdNotesState.Serialize() + @"</AdNotesState>");
            result.AppendLine(@"<SlideBulletsState>" + this.SlideBulletsState.Serialize() + @"</SlideBulletsState>");
            result.AppendLine(@"<SlideHeaderState>" + this.SlideHeaderState.Serialize() + @"</SlideHeaderState>");
            result.AppendLine(@"<SlideHeader>" + this.SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");

            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "GridColumnsState":
                        this.GridColumnsState.Deserialize(childNode);
                        break;
                    case "AdNotesState":
                        this.AdNotesState.Deserialize(childNode);
                        break;
                    case "SlideBulletsState":
                        this.SlideBulletsState.Deserialize(childNode);
                        break;
                    case "SlideHeaderState":
                        this.SlideHeaderState.Deserialize(childNode);
                        break;
                    case "SlideHeader":
                        this.SlideHeader = childNode.InnerText;
                        break;
                }
            }
        }
    }

    public class ChronoGridViewSettings
    {
        public GridColumnsState GridColumnsState { get; set; }
        public AdNotesState AdNotesState { get; set; }
        public SlideBulletsState SlideBulletsState { get; set; }
        public SlideHeaderState SlideHeaderState { get; set; }

        public string SlideHeader { get; set; }
        public Image Logo1 { get; set; }
        public Image Logo2 { get; set; }
        public Image Logo3 { get; set; }
        public Image Logo4 { get; set; }

        public ChronoGridViewSettings()
        {
            this.GridColumnsState = new GridColumnsState();
            this.AdNotesState = new AdNotesState();
            this.SlideBulletsState = new SlideBulletsState();
            this.SlideHeaderState = new SlideHeaderState();
            this.GridColumnsState.ShowPublication = true;
            this.GridColumnsState.IDPosition = -1;
            this.GridColumnsState.DatePosition = 0;
            this.GridColumnsState.PublicationPosition = 1;

            this.SlideHeader = string.Empty;

            LoadDefaultSettings();
        }

        private void LoadDefaultSettings()
        {
            XmlNode node;
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.LocalSettingsPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(ConfigurationClasses.SettingsManager.Instance.LocalSettingsPath);

                node = document.SelectSingleNode(@"/AdScheduleSettings/ChronoGridViewSettings");
                if (node != null)
                    this.Deserialize(node);
            }
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));

            result.AppendLine(@"<GridColumnsState>" + this.GridColumnsState.Serialize() + @"</GridColumnsState>");
            result.AppendLine(@"<AdNotesState>" + this.AdNotesState.Serialize() + @"</AdNotesState>");
            result.AppendLine(@"<SlideBulletsState>" + this.SlideBulletsState.Serialize() + @"</SlideBulletsState>");
            result.AppendLine(@"<SlideHeaderState>" + this.SlideHeaderState.Serialize() + @"</SlideHeaderState>");
            result.AppendLine(@"<SlideHeader>" + this.SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");
            result.AppendLine(@"<Logo1>" + Convert.ToBase64String((byte[])converter.ConvertTo(this.Logo1, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Logo1>");
            result.AppendLine(@"<Logo2>" + Convert.ToBase64String((byte[])converter.ConvertTo(this.Logo2, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Logo2>");
            result.AppendLine(@"<Logo3>" + Convert.ToBase64String((byte[])converter.ConvertTo(this.Logo3, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Logo3>");
            result.AppendLine(@"<Logo4>" + Convert.ToBase64String((byte[])converter.ConvertTo(this.Logo4, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Logo4>");

            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "GridColumnsState":
                        this.GridColumnsState.Deserialize(childNode);
                        break;
                    case "AdNotesState":
                        this.AdNotesState.Deserialize(childNode);
                        break;
                    case "SlideBulletsState":
                        this.SlideBulletsState.Deserialize(childNode);
                        break;
                    case "SlideHeaderState":
                        this.SlideHeaderState.Deserialize(childNode);
                        break;
                    case "SlideHeader":
                        this.SlideHeader = childNode.InnerText;
                        break;
                    case "Logo1":
                        if (string.IsNullOrEmpty(childNode.InnerText))
                            this.Logo1 = null;
                        else
                            this.Logo1 = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
                        break;
                    case "Logo2":
                        if (string.IsNullOrEmpty(childNode.InnerText))
                            this.Logo2 = null;
                        else
                            this.Logo2 = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
                        break;
                    case "Logo3":
                        if (string.IsNullOrEmpty(childNode.InnerText))
                            this.Logo3 = null;
                        else
                            this.Logo3 = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
                        break;
                    case "Logo4":
                        if (string.IsNullOrEmpty(childNode.InnerText))
                            this.Logo4 = null;
                        else
                            this.Logo4 = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
                        break;
                }
            }
        }
    }

    public class GridColumnsState
    {
        #region Show
        public bool ShowID { get; set; }
        public bool ShowIndex { get; set; }
        public bool ShowDate { get; set; }
        public bool ShowPCI { get; set; }
        public bool ShowCost { get; set; }
        public bool ShowFinalCost { get; set; }
        public bool ShowDiscount { get; set; }
        public bool ShowColor { get; set; }
        public bool ShowPublication { get; set; }
        public bool ShowAdNotes { get; set; }
        public bool ShowSquare { get; set; }
        public bool ShowPageSize { get; set; }
        public bool ShowPercentOfPage { get; set; }
        public bool ShowDimensions { get; set; }
        public bool ShowMechanicals { get; set; }
        public bool ShowSection { get; set; }
        public bool ShowDelivery { get; set; }
        public bool ShowReadership { get; set; }
        public bool ShowDeadline { get; set; }
        #endregion

        #region Position
        public int IDPosition { get; set; }
        public int IndexPosition { get; set; }
        public int DatePosition { get; set; }
        public int PCIPosition { get; set; }
        public int CostPosition { get; set; }
        public int FinalCostPosition { get; set; }
        public int DiscountPosition { get; set; }
        public int ColorPosition { get; set; }
        public int PublicationPosition { get; set; }
        public int SquarePosition { get; set; }
        public int PageSizePosition { get; set; }
        public int PercentOfPagePosition { get; set; }
        public int DimensionsPosition { get; set; }
        public int MechanicalsPosition { get; set; }
        public int SectionPosition { get; set; }
        public int DeliveryPosition { get; set; }
        public int ReadershipPosition { get; set; }
        public int DeadlinePosition { get; set; }
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
            #region Show
            this.ShowID = false;
            this.ShowIndex = false;
            this.ShowDate = true;
            this.ShowPCI = false;
            this.ShowCost = false;
            this.ShowFinalCost = true;
            this.ShowDiscount = false;
            this.ShowColor = false;
            this.ShowPublication = false;
            this.ShowAdNotes = false;
            this.ShowSquare = false;
            this.ShowPageSize = false;
            this.ShowPercentOfPage = true;
            this.ShowDimensions = true;
            this.ShowMechanicals = false;
            this.ShowSection = false;
            this.ShowDelivery = false;
            this.ShowReadership = false;
            this.ShowDeadline = false;
            #endregion

            #region Position
            this.IDPosition = 0;
            this.DatePosition = 1;
            this.DimensionsPosition = 2;
            this.PercentOfPagePosition = 3;
            this.FinalCostPosition = 4;
            this.PCIPosition = 5;
            this.CostPosition = 6;
            this.ColorPosition = 7;
            this.IndexPosition = 8;
            this.DiscountPosition = 9;
            this.PublicationPosition = 10;
            this.SquarePosition = 11;
            this.PageSizePosition = 12;
            this.MechanicalsPosition = 13;
            this.SectionPosition = 14;
            this.DeliveryPosition = 15;
            this.ReadershipPosition = 16;
            this.DeadlinePosition = 17;
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

            #region Show
            result.AppendLine(@"<ShowID>" + this.ShowID + @"</ShowID>");
            result.AppendLine(@"<ShowIndex>" + this.ShowIndex + @"</ShowIndex>");
            result.AppendLine(@"<ShowDate>" + this.ShowDate + @"</ShowDate>");
            result.AppendLine(@"<ShowPCI>" + this.ShowPCI + @"</ShowPCI>");
            result.AppendLine(@"<ShowCost>" + this.ShowCost + @"</ShowCost>");
            result.AppendLine(@"<ShowFinalCost>" + this.ShowFinalCost + @"</ShowFinalCost>");
            result.AppendLine(@"<ShowDiscount>" + this.ShowDiscount + @"</ShowDiscount>");
            result.AppendLine(@"<ShowColor>" + this.ShowColor + @"</ShowColor>");
            result.AppendLine(@"<ShowPublication>" + this.ShowPublication + @"</ShowPublication>");
            result.AppendLine(@"<ShowAdNotes>" + this.ShowAdNotes + @"</ShowAdNotes>");
            result.AppendLine(@"<ShowSquare>" + this.ShowSquare + @"</ShowSquare>");
            result.AppendLine(@"<ShowPageSize>" + this.ShowPageSize + @"</ShowPageSize>");
            result.AppendLine(@"<ShowPercentOfPage>" + this.ShowPercentOfPage + @"</ShowPercentOfPage>");
            result.AppendLine(@"<ShowDimensions>" + this.ShowDimensions + @"</ShowDimensions>");
            result.AppendLine(@"<ShowMechanicals>" + this.ShowMechanicals + @"</ShowMechanicals>");
            result.AppendLine(@"<ShowSection>" + this.ShowSection + @"</ShowSection>");
            result.AppendLine(@"<ShowDeadline>" + this.ShowDeadline + @"</ShowDeadline>");
            result.AppendLine(@"<ShowDelivery>" + this.ShowDelivery + @"</ShowDelivery>");
            result.AppendLine(@"<ShowReadership>" + this.ShowReadership + @"</ShowReadership>");
            #endregion

            #region Position
            result.AppendLine(@"<IDPosition>" + this.IDPosition + @"</IDPosition>");
            result.AppendLine(@"<IndexPosition>" + this.IndexPosition + @"</IndexPosition>");
            result.AppendLine(@"<DatePosition>" + this.DatePosition + @"</DatePosition>");
            result.AppendLine(@"<PCIPosition>" + this.PCIPosition + @"</PCIPosition>");
            result.AppendLine(@"<CostPosition>" + this.CostPosition + @"</CostPosition>");
            result.AppendLine(@"<FinalCostPosition>" + this.FinalCostPosition + @"</FinalCostPosition>");
            result.AppendLine(@"<DiscountPosition>" + this.DiscountPosition + @"</DiscountPosition>");
            result.AppendLine(@"<ColorPosition>" + this.ColorPosition + @"</ColorPosition>");
            result.AppendLine(@"<PublicationPosition>" + this.PublicationPosition + @"</PublicationPosition>");
            result.AppendLine(@"<SquarePosition>" + this.SquarePosition + @"</SquarePosition>");
            result.AppendLine(@"<PageSizePosition>" + this.PageSizePosition + @"</PageSizePosition>");
            result.AppendLine(@"<PercentOfPagePosition>" + this.PercentOfPagePosition + @"</PercentOfPagePosition>");
            result.AppendLine(@"<DimensionsPosition>" + this.DimensionsPosition + @"</DimensionsPosition>");
            result.AppendLine(@"<MechanicalsPosition>" + this.MechanicalsPosition + @"</MechanicalsPosition>");
            result.AppendLine(@"<SectionPosition>" + this.SectionPosition + @"</SectionPosition>");
            result.AppendLine(@"<DeliveryPosition>" + this.DeliveryPosition + @"</DeliveryPosition>");
            result.AppendLine(@"<ReadershipPosition>" + this.ReadershipPosition + @"</ReadershipPosition>");
            result.AppendLine(@"<DeadlinePosition>" + this.DeadlinePosition + @"</DeadlinePosition>");
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
                    #region Show
                    case "ShowAdNotes":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAdNotes = tempBool;
                        break;
                    case "ShowColor":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowColor = tempBool;
                        break;
                    case "ShowCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowCost = tempBool;
                        break;
                    case "ShowDate":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDate = tempBool;
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
                    case "ShowDiscount":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDiscount = tempBool;
                        break;
                    case "ShowFinalCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowFinalCost = tempBool;
                        break;
                    case "ShowID":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowID = tempBool;
                        break;
                    case "ShowIndex":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowIndex = tempBool;
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
                    case "ShowPCI":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPCI = tempBool;
                        break;
                    case "ShowPublication":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPublication = tempBool;
                        break;
                    case "ShowReadership":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowReadership = tempBool;
                        break;
                    case "ShowSection":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSection = tempBool;
                        break;
                    case "ShowSquare":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSquare = tempBool;
                        break;
                    #endregion

                    #region Position
                    case "ColorPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.ColorPosition = tempInt;
                        break;
                    case "CostPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.CostPosition = tempInt;
                        break;
                    case "DatePosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.DatePosition = tempInt;
                        break;
                    case "DeadlinePosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.DeadlinePosition = tempInt;
                        break;
                    case "DeliveryPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.DeliveryPosition = tempInt;
                        break;
                    case "DimensionsPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.DimensionsPosition = tempInt;
                        break;
                    case "DiscountPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.DiscountPosition = tempInt;
                        break;
                    case "FinalCostPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.FinalCostPosition = tempInt;
                        break;
                    case "IDPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.IDPosition = tempInt;
                        break;
                    case "IndexPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.IndexPosition = tempInt;
                        break;
                    case "MechanicalsPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.MechanicalsPosition = tempInt;
                        break;
                    case "PageSizePosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.PageSizePosition = tempInt;
                        break;
                    case "PercentOfPagePosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.PercentOfPagePosition = tempInt;
                        break;
                    case "PCIPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.PCIPosition = tempInt;
                        break;
                    case "PublicationPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.PublicationPosition = tempInt;
                        break;
                    case "ReadershipPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.ReadershipPosition = tempInt;
                        break;
                    case "SectionPosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.SectionPosition = tempInt;
                        break;
                    case "SquarePosition":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.SquarePosition = tempInt;
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
        }
    }

    public class AdNotesState
    {
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
            #region Show
            this.ShowComments = false;
            this.ShowSection = false;
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
        }
    }

    public class SlideBulletsState
    {
        public bool EnableSlideBullets { get; set; }
        public bool ShowTotalInserts { get; set; }
        public bool ShowAvgAdCost { get; set; }
        public bool ShowAvgFinalCost { get; set; }
        public bool ShowAvgPCI { get; set; }
        public bool ShowTotalColor { get; set; }
        public bool ShowDiscounts { get; set; }
        public bool ShowTotalFinalCost { get; set; }
        public bool ShowTotalSquare { get; set; }
        public bool ShowPageSize { get; set; }
        public bool ShowPercentOfPage { get; set; }
        public bool ShowDimensions { get; set; }
        public bool ShowColumnInches { get; set; }
        public bool ShowDelivery { get; set; }
        public bool ShowReadership { get; set; }
        public bool ShowSignature { get; set; }
        public bool ShowOnlyOnLastSlide { get; set; }

        public SlideBulletsState()
        {
            this.EnableSlideBullets = true;
            this.ShowTotalInserts = true;
            this.ShowAvgAdCost = false;
            this.ShowAvgFinalCost = false;
            this.ShowAvgPCI = false;
            this.ShowTotalColor = false;
            this.ShowDiscounts = false;
            this.ShowTotalFinalCost = true;
            this.ShowTotalSquare = false;
            this.ShowPageSize = false;
            this.ShowPercentOfPage = false;
            this.ShowDimensions = false;
            this.ShowColumnInches = false;
            this.ShowDelivery = false;
            this.ShowReadership = false;
            this.ShowSignature = true;
            this.ShowOnlyOnLastSlide = true;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<EnableSlideBullets>" + this.EnableSlideBullets + @"</EnableSlideBullets>");
            result.AppendLine(@"<ShowAvgAdCost>" + this.ShowAvgAdCost + @"</ShowAvgAdCost>");
            result.AppendLine(@"<ShowAvgFinalCost>" + this.ShowAvgFinalCost + @"</ShowAvgFinalCost>");
            result.AppendLine(@"<ShowAvgPCI>" + this.ShowAvgPCI + @"</ShowAvgPCI>");
            result.AppendLine(@"<ShowColumnInches>" + this.ShowColumnInches + @"</ShowColumnInches>");
            result.AppendLine(@"<ShowDelivery>" + this.ShowDelivery + @"</ShowDelivery>");
            result.AppendLine(@"<ShowDimensions>" + this.ShowDimensions + @"</ShowDimensions>");
            result.AppendLine(@"<ShowDiscounts>" + this.ShowDiscounts + @"</ShowDiscounts>");
            result.AppendLine(@"<ShowOnlyOnLastSlide>" + this.ShowOnlyOnLastSlide + @"</ShowOnlyOnLastSlide>");
            result.AppendLine(@"<ShowPageSize>" + this.ShowPageSize + @"</ShowPageSize>");
            result.AppendLine(@"<ShowPercentOfPage>" + this.ShowPercentOfPage + @"</ShowPercentOfPage>");
            result.AppendLine(@"<ShowReadership>" + this.ShowReadership + @"</ShowReadership>");
            result.AppendLine(@"<ShowSignature>" + this.ShowSignature + @"</ShowSignature>");
            result.AppendLine(@"<ShowTotalColor>" + this.ShowTotalColor + @"</ShowTotalColor>");
            result.AppendLine(@"<ShowTotalFinalCost>" + this.ShowTotalFinalCost + @"</ShowTotalFinalCost>");
            result.AppendLine(@"<ShowTotalInserts>" + this.ShowTotalInserts + @"</ShowTotalInserts>");
            result.AppendLine(@"<ShowTotalSquare>" + this.ShowTotalSquare + @"</ShowTotalSquare>");

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
                    case "ShowColumnInches":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowColumnInches = tempBool;
                        break;
                    case "ShowDelivery":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDelivery = tempBool;
                        break;
                    case "ShowDimensions":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDimensions = tempBool;
                        break;
                    case "ShowDiscounts":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDiscounts = tempBool;
                        break;
                    case "ShowOnlyOnLastSlide":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowOnlyOnLastSlide = tempBool;
                        break;
                    case "ShowPageSize":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPageSize = tempBool;
                        break;
                    case "ShowPercentOfPage":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPercentOfPage = tempBool;
                        break;
                    case "ShowReadership":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowReadership = tempBool;
                        break;
                    case "ShowTotalColor":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalColor = tempBool;
                        break;
                    case "ShowTotalFinalCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalFinalCost = tempBool;
                        break;
                    case "ShowTotalInserts":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalInserts = tempBool;
                        break;
                    case "ShowTotalSquare":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalSquare = tempBool;
                        break;
                    case "ShowSignature":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSignature = tempBool;
                        break;
                }
            }
        }
    }

    public class SlideHeaderState
    {
        public bool EnableSlideHeader { get; set; }
        public bool ShowName { get; set; }
        public bool ShowLogo1 { get; set; }
        public bool ShowLogo2 { get; set; }
        public bool ShowLogo3 { get; set; }
        public bool ShowLogo4 { get; set; }
        public bool ShowSlideHeader { get; set; }
        public bool ShowPresentationDate { get; set; }
        public bool ShowAdvertiser { get; set; }
        public bool ShowDecisionMaker { get; set; }
        public bool ShowFlightDates { get; set; }

        public SlideHeaderState()
        {
            this.EnableSlideHeader = true;
            this.ShowName = true;
            this.ShowLogo1 = true;
            this.ShowLogo2 = true;
            this.ShowLogo3 = true;
            this.ShowLogo4 = true;
            this.ShowSlideHeader = true;
            this.ShowPresentationDate = true;
            this.ShowAdvertiser = true;
            this.ShowDecisionMaker = true;
            this.ShowFlightDates = true;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<EnableSlideHeader>" + this.EnableSlideHeader + @"</EnableSlideHeader>");
            result.AppendLine(@"<ShowName>" + this.ShowName + @"</ShowName>");
            result.AppendLine(@"<ShowLogo1>" + this.ShowLogo1 + @"</ShowLogo1>");
            result.AppendLine(@"<ShowLogo2>" + this.ShowLogo2 + @"</ShowLogo2>");
            result.AppendLine(@"<ShowLogo3>" + this.ShowLogo3 + @"</ShowLogo3>");
            result.AppendLine(@"<ShowLogo4>" + this.ShowLogo4 + @"</ShowLogo4>");
            result.AppendLine(@"<ShowAdvertiser>" + this.ShowAdvertiser + @"</ShowAdvertiser>");
            result.AppendLine(@"<ShowDecisionMaker>" + this.ShowDecisionMaker + @"</ShowDecisionMaker>");
            result.AppendLine(@"<ShowFlightDates>" + this.ShowFlightDates + @"</ShowFlightDates>");
            result.AppendLine(@"<ShowPresentationDate>" + this.ShowPresentationDate + @"</ShowPresentationDate>");
            result.AppendLine(@"<ShowSlideHeader>" + this.ShowSlideHeader + @"</ShowSlideHeader>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool = false;
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "EnableSlideHeader":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.EnableSlideHeader = tempBool;
                        break;
                    case "ShowName":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowName = tempBool;
                        break;
                    case "ShowLogo1":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowLogo1 = tempBool;
                        break;
                    case "ShowLogo2":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowLogo2 = tempBool;
                        break;
                    case "ShowLogo3":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowLogo3 = tempBool;
                        break;
                    case "ShowLogo4":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowLogo4 = tempBool;
                        break;
                    case "ShowAdvertiser":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowAdvertiser = tempBool;
                        break;
                    case "ShowDecisionMaker":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowDecisionMaker = tempBool;
                        break;
                    case "ShowFlightDates":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowFlightDates = tempBool;
                        break;
                    case "ShowPresentationDate":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowPresentationDate = tempBool;
                        break;
                    case "ShowSlideHeader":
                        tempBool = false;
                        bool.TryParse(childNode.InnerText, out tempBool);
                        this.ShowSlideHeader = tempBool;
                        break;
                }
            }
        }
    }

    public class CalendarViewSettings
    {
        public bool ShowAbbreviationOnly { get; set; }
        public bool ShowSection { get; set; }
        public bool ShowAdSize { get; set; }
        public bool ShowPageSize { get; set; }
        public bool ShowPercentOfPage { get; set; }
        public bool ShowColor { get; set; }
        public bool ShowCost { get; set; }
        public bool ShowBigDate { get; set; }

        public bool ShowLegend { get; set; }
        public bool ShowTitle { get; set; }
        public bool ShowDate { get; set; }
        public bool ShowBusinessName { get; set; }
        public bool ShowDecisionMaker { get; set; }
        public bool ShowTotalCost { get; set; }
        public bool ShowAvgCost { get; set; }
        public bool ShowTotalAds { get; set; }
        public bool ShowActiveDays { get; set; }
        public bool ShowComments { get; set; }
        public bool ShowLogo { get; set; }

        public string SlideColor { get; set; }

        public List<MonthCalendarViewSettings> MonthCalendarViewSettingsList { get; set; }
        public List<CalendarDayInfo> DayCustomNotes { get; private set; }
        public List<CalendarDayInfo> DayDeadlines { get; private set; }

        public CalendarViewSettings()
        {
            this.ShowAbbreviationOnly = false;
            this.ShowSection = true;
            this.ShowAdSize = true;
            this.ShowPageSize = false;
            this.ShowPercentOfPage = false;
            this.ShowColor = true;
            this.ShowCost = true;
            this.ShowBigDate = true;
            this.ShowLegend = false;
            this.ShowTitle = true;
            this.ShowDate = true;
            this.ShowBusinessName = true;
            this.ShowDecisionMaker = true;
            this.ShowLogo = true;
            this.ShowTotalCost = false;
            this.ShowAvgCost = false;
            this.ShowTotalAds = false;
            this.ShowActiveDays = false;
            this.ShowComments = false;

            this.SlideColor = "gray";
            this.MonthCalendarViewSettingsList = new List<MonthCalendarViewSettings>();
            this.DayCustomNotes = new List<CalendarDayInfo>();
            this.DayDeadlines = new List<CalendarDayInfo>();
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));

            result.AppendLine(@"<ShowAbbreviationOnly>" + this.ShowAbbreviationOnly + @"</ShowAbbreviationOnly>");
            result.AppendLine(@"<ShowAdSize>" + this.ShowAdSize + @"</ShowAdSize>");
            result.AppendLine(@"<ShowPageSize>" + this.ShowPageSize + @"</ShowPageSize>");
            result.AppendLine(@"<ShowPercentOfPage>" + this.ShowPercentOfPage + @"</ShowPercentOfPage>");
            result.AppendLine(@"<ShowColor>" + this.ShowColor + @"</ShowColor>");
            result.AppendLine(@"<ShowCost>" + this.ShowCost + @"</ShowCost>");
            result.AppendLine(@"<ShowSection>" + this.ShowSection + @"</ShowSection>");
            result.AppendLine(@"<SlideColor>" + this.SlideColor.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideColor>");
            result.AppendLine(@"<ShowBigDate>" + this.ShowBigDate + @"</ShowBigDate>");
            result.AppendLine(@"<ShowLegend>" + this.ShowLegend + @"</ShowLegend>");
            result.AppendLine(@"<ShowActiveDays>" + this.ShowActiveDays + @"</ShowActiveDays>");
            result.AppendLine(@"<ShowAvgCost>" + this.ShowAvgCost + @"</ShowAvgCost>");
            result.AppendLine(@"<ShowComments>" + this.ShowComments + @"</ShowComments>");
            result.AppendLine(@"<ShowDate>" + this.ShowDate + @"</ShowDate>");
            result.AppendLine(@"<ShowTitle>" + this.ShowTitle + @"</ShowTitle>");
            result.AppendLine(@"<ShowBusinessName>" + this.ShowBusinessName + @"</ShowBusinessName>");
            result.AppendLine(@"<ShowDecisionMaker>" + this.ShowDecisionMaker + @"</ShowDecisionMaker>");
            result.AppendLine(@"<ShowLogo>" + this.ShowLogo + @"</ShowLogo>");
            result.AppendLine(@"<ShowTotalAds>" + this.ShowTotalAds + @"</ShowTotalAds>");
            result.AppendLine(@"<ShowTotalCost>" + this.ShowTotalCost + @"</ShowTotalCost>");
            result.AppendLine(@"<MonthCalendarViewSettings>");
            foreach (var calendarSettings in this.MonthCalendarViewSettingsList)
            {
                result.AppendLine(@"<MonthCalendar>" + calendarSettings.Serialize() + @"</MonthCalendar>");
            }
            result.AppendLine(@"</MonthCalendarViewSettings>");

            result.AppendLine(@"<DayCustomNotes>");
            foreach (CalendarDayInfo dayCustomNote in this.DayCustomNotes)
                result.AppendLine(@"<DayCustomNote>" + dayCustomNote.Serialize() + @"</DayCustomNote>");
            result.AppendLine(@"</DayCustomNotes>");
            result.AppendLine(@"<DayDeadlines>");
            foreach (CalendarDayInfo dayDeadline in this.DayDeadlines)
                result.AppendLine(@"<DayDeadline>" + dayDeadline.Serialize() + @"</DayDeadline>");
            result.AppendLine(@"</DayDeadlines>");

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
                    case "ShowColor":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowColor = tempBool;
                        break;
                    case "ShowCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowCost = tempBool;
                        break;
                    case "ShowSection":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSection = tempBool;
                        break;
                    case "SlideColor":
                        this.SlideColor = childNode.InnerText;
                        break;
                    case "ShowBigDate":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowBigDate = tempBool;
                        break;
                    case "ShowLegend":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowLegend = tempBool;
                        break;
                    case "ShowActiveDays":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowActiveDays = tempBool;
                        break;
                    case "ShowTotalCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalCost = tempBool;
                        break;
                    case "ShowAvgCost":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowAvgCost = tempBool;
                        break;
                    case "ShowComments":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowComments = tempBool;
                        break;
                    case "ShowDate":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDate = tempBool;
                        break;
                    case "ShowTitle":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTitle = tempBool;
                        break;
                    case "ShowBusinessName":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowBusinessName = tempBool;
                        break;
                    case "ShowDecisionMaker":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDecisionMaker = tempBool;
                        break;
                    case "ShowLogo":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowLogo = tempBool;
                        break;
                    case "ShowTotalAds":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotalAds = tempBool;
                        break;
                    case "MonthCalendarViewSettings":
                        this.MonthCalendarViewSettingsList.Clear();
                        foreach (XmlNode calendarNode in childNode.ChildNodes)
                        {
                            switch (calendarNode.Name)
                            {
                                case "MonthCalendar":
                                    MonthCalendarViewSettings calendarSettings = new MonthCalendarViewSettings(this);
                                    calendarSettings.Deserialize(calendarNode);
                                    this.MonthCalendarViewSettingsList.Add(calendarSettings);
                                    break;
                            }
                        }
                        break;
                    case "DayCustomNotes":
                        this.DayCustomNotes.Clear();
                        foreach (XmlNode dayCustomNoteNode in childNode.ChildNodes)
                        {
                            CalendarDayInfo dayCustomNote = new CalendarDayInfo();
                            dayCustomNote.Deserialize(dayCustomNoteNode);
                            this.DayCustomNotes.Add(dayCustomNote);
                        }
                        break;
                    case "DayDeadlines":
                        this.DayDeadlines.Clear();
                        foreach (XmlNode dayDeadlineNode in childNode.ChildNodes)
                        {
                            CalendarDayInfo dayDeadline = new CalendarDayInfo();
                            dayDeadline.Deserialize(dayDeadlineNode);
                            this.DayDeadlines.Add(dayDeadline);
                        }
                        break;
                }
            }
        }
    }

    public class MonthCalendarViewSettings
    {
        public CalendarViewSettings Parent { get; private set; }
        public OutputClasses.OutputControls.MonthViewControl MonthView { get; set; }
        public DateTime Month { get; set; }
        public string Title { get; set; }
        public Image Logo { get; set; }
        public string Comments { get; set; }

        public List<CalendarLegend> Legend { get; private set; }

        public MonthCalendarViewSettings(CalendarViewSettings parent)
        {
            this.Parent = parent;
            this.Comments = string.Empty;
            this.Legend = new List<CalendarLegend>();

            this.Title = "Monthly Advertising Planner";
            this.Comments = string.Empty;

            string filePath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.BigImageFolder.FullName, ConfigurationClasses.SettingsManager.DefaultBigLogoFileName);
            if (File.Exists(filePath))
                this.Logo = new Bitmap(filePath);
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));

            result.AppendLine(@"<Title>" + this.Title.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Title>");
            result.AppendLine(@"<Comments>" + this.Comments.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Comments>");
            result.AppendLine(@"<Month>" + this.Month.ToString() + @"</Month>");
            result.AppendLine(@"<Logo>" + Convert.ToBase64String((byte[])converter.ConvertTo(this.Logo, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Logo>");
            result.AppendLine(@"<Legends>");
            foreach (CalendarLegend legend in this.Legend)
                result.AppendLine(@"<Legend>" + legend.Serialize() + @"</Legend>");
            result.AppendLine(@"</Legends>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            DateTime tempDate = DateTime.MinValue;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Title":
                        this.Title = childNode.InnerText;
                        break;
                    case "Comments":
                        this.Comments = childNode.InnerText;
                        break;
                    case "Logo":
                        if (string.IsNullOrEmpty(childNode.InnerText))
                            this.Logo = null;
                        else
                            this.Logo = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
                        break;
                    case "Month":
                        if (DateTime.TryParse(childNode.InnerText, out tempDate))
                            this.Month = tempDate;
                        break;
                    case "Legends":
                        this.Legend.Clear();
                        foreach (XmlNode legendNode in childNode.ChildNodes)
                        {
                            CalendarLegend legend = new CalendarLegend();
                            legend.Deserialize(legendNode);
                            this.Legend.Add(legend);
                        }
                        break;
                }
            }
        }

        public MonthCalendarViewSettings Clone()
        {
            MonthCalendarViewSettings result = new MonthCalendarViewSettings(this.Parent);
            result.Comments = this.Comments;
            result.Logo = this.Logo;
            result.Month = this.Month;
            result.MonthView = this.MonthView;
            result.Title = this.Title;
            foreach (ConfigurationClasses.CalendarLegend legend in this.Legend)
                result.Legend.Add(legend.Clone());
            return result;
        }

        public string GetLegendCodeByDescription(string description)
        {
            string result = string.Empty;
            CalendarLegend legend = this.Legend.Where(x => x.Description.Equals(description)).FirstOrDefault();
            if (legend != null)
                result = legend.Code;
            return result;
        }
    }

    public class CalendarLegend
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public bool Visible { get; set; }

        public string StringRepresentation
        {
            get
            {
                return this.Code + " = " + this.Description;
            }
        }

        public CalendarLegend()
        {
            this.Code = string.Empty;
            this.Description = string.Empty;
            this.Visible = true;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<Code>" + this.Code.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Code>");
            result.AppendLine(@"<Description>" + this.Description.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Description>");
            result.AppendLine(@"<Visible>" + this.Visible.ToString() + @"</Visible>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool = false;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Code":
                        this.Code = childNode.InnerText;
                        break;
                    case "Description":
                        this.Description = childNode.InnerText;
                        break;
                    case "Visible":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.Visible = tempBool;
                        break;
                }
            }
        }

        public CalendarLegend Clone()
        {
            CalendarLegend result = new CalendarLegend();
            result.Code = this.Code;
            result.Description = this.Description;
            result.Visible = this.Visible;
            return result;
        }
    }

    public class CalendarDayInfo
    {
        public DateTime Day { get; set; }
        public string Info { get; set; }

        public CalendarDayInfo()
        {
            this.Info = string.Empty;
        }
        
        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Day>" + this.Day.ToString() + @"</Day>");
            result.AppendLine(@"<Info>" + this.Info.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Info>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            DateTime tempDate;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Info":
                        this.Info = childNode.InnerText;
                        break;
                    case "Day":
                        if (DateTime.TryParse(childNode.InnerText, out tempDate))
                            this.Day = tempDate;
                        break;
                }
            }
        }
    }
}
