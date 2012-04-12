using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace NewBizWizForm.ConfigurationClasses
{
    public class ViewSettingsManager
    {
        private static ViewSettingsManager _instance = new ViewSettingsManager();

        public CoverState CoverState { get; set; }
        public LeadoffStatementState LeadoffStatementState { get; set; }
        public ClientGoalsState ClientGoalsState { get; set; }
        public TargetCustomersState TargetCustomersState { get; set; }
        public SimpleSummaryState SimpleSummaryState { get; set; }


        public static ViewSettingsManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private ViewSettingsManager()
        {
            this.CoverState = new CoverState();
            this.LeadoffStatementState = new LeadoffStatementState();
            this.ClientGoalsState = new ClientGoalsState();
            this.TargetCustomersState = new TargetCustomersState();
            this.SimpleSummaryState = new SimpleSummaryState();
        }
    }

    public class CoverState
    {
        public bool IsNewSolution { get; set; }
        public bool AddAsPageOne { get; set; }
        public bool UseGenericCover { get; set; }
        public bool ShowSalesRep { get; set; }
        public bool ShowPresentationDate { get; set; }


        public string SlideHeader { get; set; }
        public string Advertiser { get; set; }
        public string DecisionMaker { get; set; }
        public string SalesRep { get; set; }
        public DateTime PresentationDate { get; set; }
        public BusinessClasses.Quote Quote { get; set; }

        public CoverState()
        {
            this.IsNewSolution = true;
            this.ShowSalesRep = false;
            this.ShowPresentationDate = false;
            this.AddAsPageOne = true;
            this.UseGenericCover = false;

            this.SlideHeader = string.Empty;
            this.Advertiser = string.Empty;
            this.DecisionMaker = string.Empty;
            this.SalesRep = string.Empty;
            this.PresentationDate = DateTime.MinValue;
            this.Quote = new BusinessClasses.Quote();
        }

        private string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<IsNewSolution>" + this.IsNewSolution + @"</IsNewSolution>");
            result.AppendLine(@"<AddAsPageOne>" + this.AddAsPageOne + @"</AddAsPageOne>");
            result.AppendLine(@"<UseGenericCover>" + this.UseGenericCover + @"</UseGenericCover>");
            result.AppendLine(@"<ShowSalesRep>" + this.ShowSalesRep + @"</ShowSalesRep>");
            result.AppendLine(@"<ShowPresentationDate>" + this.ShowPresentationDate + @"</ShowPresentationDate>");
            result.AppendLine(@"<SlideHeader>" + this.SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");
            result.AppendLine(@"<Advertiser>" + this.Advertiser.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Advertiser>");
            result.AppendLine(@"<DecisionMaker>" + this.DecisionMaker.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DecisionMaker>");
            result.AppendLine(@"<SalesRep>" + this.SalesRep.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SalesRep>");
            result.AppendLine(@"<PresentationDate>" + this.PresentationDate + @"</PresentationDate>");
            result.AppendLine(@"<Quote>" + this.Quote.Serialize() + @"</Quote>");

            return result.ToString();
        }

        private void Deserialize(XmlNode node)
        {
            bool tempBool = false;
            DateTime tempDateTime = DateTime.Now;

            this.PresentationDate = DateTime.MinValue;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "IsNewSolution":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.IsNewSolution = tempBool;
                        break;
                    case "AddAsPageOne":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.AddAsPageOne = tempBool;
                        break;
                    case "UseGenericCover":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.UseGenericCover = tempBool;
                        break;
                    case "ShowPresentationDate":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPresentationDate = tempBool;
                        break;
                    case "ShowSalesRep":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSalesRep = tempBool;
                        break;
                    case "SlideHeader":
                        this.SlideHeader = childNode.InnerText;
                        break;
                    case "Advertiser":
                        this.Advertiser = childNode.InnerText;
                        break;
                    case "DecisionMaker":
                        this.DecisionMaker = childNode.InnerText;
                        break;
                    case "SalesRep":
                        this.SalesRep = childNode.InnerText;
                        break;
                    case "PresentationDate":
                        if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
                            this.PresentationDate = tempDateTime;
                        break;
                    case "Quote":
                        this.Quote.Deserialize(childNode);
                        break;
                }
            }
        }

        public string SerializeSalesRep()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<ShowSalesRep>" + this.ShowSalesRep + @"</ShowSalesRep>");
            result.AppendLine(@"<ValueSalesRep>" + this.SalesRep.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</ValueSalesRep>");

            return result.ToString();
        }

        public void DeserializeSalesRep(XmlNode node)
        {
            bool tempBool = false;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "ShowSalesRep":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowSalesRep = tempBool;
                        break;
                    case "ValueSalesRep":
                        this.SalesRep = childNode.InnerText;
                        break;
                }
            }
        }

        public void Load(string filePath)
        {
            XmlNode node;
            if (File.Exists(filePath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(filePath);
                node = document.SelectSingleNode(@"/CoverState");
                if (node != null)
                    Deserialize(node);
            }
        }

        public void Save()
        {
            DateTime now = DateTime.Now;
            if (!Directory.Exists(Path.Combine(ConfigurationClasses.SettingsManager.Instance.DashboardSaveFolder, "cover")))
                Directory.CreateDirectory(Path.Combine(ConfigurationClasses.SettingsManager.Instance.DashboardSaveFolder, "cover"));
            string fileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.DashboardSaveFolder, "cover", "cover-" + now.ToString("MMddyy") + "-" + now.ToString("hmmsstt") + ".xml");
            using (StreamWriter sw = new StreamWriter(fileName, false))
            {
                sw.Write("<CoverState>" + Serialize() + " </CoverState>");
                sw.Flush();
            }
        }

        public bool AllowToLoad()
        {
            bool result = false;
            DirectoryInfo saveFolder = new DirectoryInfo(Path.Combine(ConfigurationClasses.SettingsManager.Instance.DashboardSaveFolder, "cover"));
            if (saveFolder.Exists)
                result = saveFolder.GetFiles("*.xml").Length > 0;
            return result;
        }
    }

    public class LeadoffStatementState
    {
        public bool ShowStatement1 { get; set; }
        public bool ShowStatement2 { get; set; }
        public bool ShowStatement3 { get; set; }
        public string SlideHeader { get; set; }
        public string Statement1 { get; set; }
        public string Statement2 { get; set; }
        public string Statement3 { get; set; }


        public LeadoffStatementState()
        {
            this.ShowStatement1 = true;
            this.ShowStatement2 = false;
            this.ShowStatement3 = false;
            this.SlideHeader = string.Empty;
            this.Statement1 = string.Empty;
            this.Statement2 = string.Empty;
            this.Statement3 = string.Empty;
        }

        private string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<ShowStatement1>" + this.ShowStatement1 + @"</ShowStatement1>");
            result.AppendLine(@"<ShowStatement2>" + this.ShowStatement2 + @"</ShowStatement2>");
            result.AppendLine(@"<ShowStatement3>" + this.ShowStatement3 + @"</ShowStatement3>");
            result.AppendLine(@"<SlideHeader>" + this.SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");
            result.AppendLine(@"<Statement1>" + this.Statement1.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Statement1>");
            result.AppendLine(@"<Statement2>" + this.Statement2.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Statement2>");
            result.AppendLine(@"<Statement3>" + this.Statement3.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Statement3>");

            return result.ToString();
        }

        private void Deserialize(XmlNode node)
        {
            bool tempBool = false;
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "ShowStatement1":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowStatement1 = tempBool;
                        break;
                    case "ShowStatement2":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowStatement2 = tempBool;
                        break;
                    case "ShowStatement3":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowStatement3 = tempBool;
                        break;
                    case "SlideHeader":
                        this.SlideHeader = childNode.InnerText;
                        break;
                    case "Statement1":
                        this.Statement1 = childNode.InnerText;
                        break;
                    case "Statement2":
                        this.Statement2 = childNode.InnerText;
                        break;
                    case "Statement3":
                        this.Statement3 = childNode.InnerText;
                        break;
                }
            }
        }

        public void Load(string filePath)
        {
            XmlNode node;
            if (File.Exists(filePath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(filePath);
                node = document.SelectSingleNode(@"/LeadoffStatementState");
                if (node != null)
                    Deserialize(node);
            }
        }

        public void Save()
        {
            DateTime now = DateTime.Now;
            if (!Directory.Exists(Path.Combine(ConfigurationClasses.SettingsManager.Instance.DashboardSaveFolder, "intro")))
                Directory.CreateDirectory(Path.Combine(ConfigurationClasses.SettingsManager.Instance.DashboardSaveFolder, "intro"));
            string fileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.DashboardSaveFolder, "intro", "intro-" + now.ToString("MMddyy") + "-" + now.ToString("hmmsstt") + ".xml");
            using (StreamWriter sw = new StreamWriter(fileName, false))
            {
                sw.Write("<LeadoffStatementState>" + Serialize() + " </LeadoffStatementState> ");
                sw.Flush();
            }
        }

        public bool AllowToLoad()
        {
            bool result = false;
            DirectoryInfo saveFolder = new DirectoryInfo(Path.Combine(ConfigurationClasses.SettingsManager.Instance.DashboardSaveFolder, "intro"));
            if (saveFolder.Exists)
                result = saveFolder.GetFiles("*.xml").Length > 0;
            return result;
        }
    }

    public class ClientGoalsState
    {
        public string SlideHeader { get; set; }
        public string Goal1 { get; set; }
        public string Goal2 { get; set; }
        public string Goal3 { get; set; }
        public string Goal4 { get; set; }
        public string Goal5 { get; set; }


        public ClientGoalsState()
        {
            this.SlideHeader = string.Empty;
            this.Goal1 = string.Empty;
            this.Goal2 = string.Empty;
            this.Goal3 = string.Empty;
            this.Goal4 = string.Empty;
            this.Goal5 = string.Empty;
        }

        private string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<SlideHeader>" + this.SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");
            result.AppendLine(@"<Goal1>" + this.Goal1.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Goal1>");
            result.AppendLine(@"<Goal2>" + this.Goal2.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Goal2>");
            result.AppendLine(@"<Goal3>" + this.Goal3.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Goal3>");
            result.AppendLine(@"<Goal4>" + this.Goal4.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Goal4>");
            result.AppendLine(@"<Goal5>" + this.Goal5.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Goal5>");

            return result.ToString();
        }

        private void Deserialize(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "SlideHeader":
                        this.SlideHeader = childNode.InnerText;
                        break;
                    case "Goal1":
                        this.Goal1 = childNode.InnerText;
                        break;
                    case "Goal2":
                        this.Goal2 = childNode.InnerText;
                        break;
                    case "Goal3":
                        this.Goal3 = childNode.InnerText;
                        break;
                    case "Goal4":
                        this.Goal4 = childNode.InnerText;
                        break;
                    case "Goal5":
                        this.Goal5 = childNode.InnerText;
                        break;
                }
            }
        }

        public void Load(string filePath)
        {
            XmlNode node;
            if (File.Exists(filePath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(filePath);
                node = document.SelectSingleNode(@"/ClientGoalsState");
                if (node != null)
                    Deserialize(node);
            }
        }

        public void Save()
        {
            DateTime now = DateTime.Now;
            if (!Directory.Exists(Path.Combine(ConfigurationClasses.SettingsManager.Instance.DashboardSaveFolder, "needsgoals")))
                Directory.CreateDirectory(Path.Combine(ConfigurationClasses.SettingsManager.Instance.DashboardSaveFolder, "needsgoals"));
            string fileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.DashboardSaveFolder, "needsgoals", "needsgoals-" + now.ToString("MMddyy") + "-" + now.ToString("hmmsstt") + ".xml");
            using (StreamWriter sw = new StreamWriter(fileName, false))
            {
                sw.Write("<ClientGoalsState>" + Serialize() + " </ClientGoalsState> ");
                sw.Flush();
            }
        }

        public bool AllowToLoad()
        {
            bool result = false;
            DirectoryInfo saveFolder = new DirectoryInfo(Path.Combine(ConfigurationClasses.SettingsManager.Instance.DashboardSaveFolder, "needsgoals"));
            if (saveFolder.Exists)
                result = saveFolder.GetFiles("*.xml").Length > 0;
            return result;
        }
    }

    public class TargetCustomersState
    {
        public string SlideHeader { get; set; }
        public List<string> Demo { get; set; }
        public List<string> Income { get; set; }
        public List<string> Geographic { get; set; }


        public TargetCustomersState()
        {
            this.SlideHeader = string.Empty;
            this.Demo = new List<string>();
            this.Income = new List<string>();
            this.Geographic = new List<string>();
        }

        private string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<SlideHeader>" + this.SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");
            result.AppendLine(@"<Demo>");
            foreach (string demo in this.Demo)
                result.AppendLine(@"<Value>" + demo.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Value>");
            result.AppendLine(@"</Demo>");
            result.AppendLine(@"<Income>");
            foreach (string income in this.Income)
                result.AppendLine(@"<Value>" + income.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Value>");
            result.AppendLine(@"</Income>");
            result.AppendLine(@"<Geographic>");
            foreach (string geographic in this.Geographic)
                result.AppendLine(@"<Value>" + geographic.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Value>");
            result.AppendLine(@"</Geographic>");

            return result.ToString();
        }

        private void Deserialize(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "SlideHeader":
                        this.SlideHeader = childNode.InnerText;
                        break;
                    case "Demo":
                        this.Demo.Clear();
                        foreach (XmlNode valueNode in childNode.ChildNodes)
                            this.Demo.Add(valueNode.InnerText);
                        break;
                    case "Income":
                        this.Income.Clear();
                        foreach (XmlNode valueNode in childNode.ChildNodes)
                            this.Income.Add(valueNode.InnerText);
                        break;
                    case "Geographic":
                        this.Geographic.Clear();
                        foreach (XmlNode valueNode in childNode.ChildNodes)
                            this.Geographic.Add(valueNode.InnerText);
                        break;
                }
            }
        }

        public void Load(string filePath)
        {
            XmlNode node;
            if (File.Exists(filePath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(filePath);
                node = document.SelectSingleNode(@"/TargetCustomersState");
                if (node != null)
                    Deserialize(node);
            }
        }

        public void Save()
        {
            DateTime now = DateTime.Now;
            if (!Directory.Exists(Path.Combine(ConfigurationClasses.SettingsManager.Instance.DashboardSaveFolder, "target")))
                Directory.CreateDirectory(Path.Combine(ConfigurationClasses.SettingsManager.Instance.DashboardSaveFolder, "target"));
            string fileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.DashboardSaveFolder, "target", "target-" + now.ToString("MMddyy") + "-" + now.ToString("hmmsstt") + ".xml");
            using (StreamWriter sw = new StreamWriter(fileName, false))
            {
                sw.Write("<TargetCustomersState>" + Serialize() + " </TargetCustomersState> ");
                sw.Flush();
            }
        }

        public bool AllowToLoad()
        {
            bool result = false;
            DirectoryInfo saveFolder = new DirectoryInfo(Path.Combine(ConfigurationClasses.SettingsManager.Instance.DashboardSaveFolder, "target"));
            if (saveFolder.Exists)
                result = saveFolder.GetFiles("*.xml").Length > 0;
            return result;
        }
    }

    public class SimpleSummaryState
    {
        public bool ShowAdvertiser { get; set; }
        public bool ShowDecisionMaker { get; set; }
        public bool ShowPresentationDate { get; set; }
        public bool ShowFlightDates { get; set; }
        public bool ShowMonthly { get; set; }
        public bool ShowTotal { get; set; }
        public bool EnableTotalsEdit { get; set; }

        public string SlideHeader { get; set; }
        public string Advertiser { get; set; }
        public string DecisionMaker { get; set; }
        public DateTime PresentationDate { get; set; }
        public DateTime FlightDatesStart { get; set; }
        public DateTime FlightDatesEnd { get; set; }
        public double MonthlyValue { get; set; }
        public double TotalValue { get; set; }

        public List<SimpleSummaryItemState> ItemsState { get; set; }

        public SimpleSummaryState()
        {
            this.ShowAdvertiser = true;
            this.ShowDecisionMaker = true;
            this.ShowPresentationDate = true;
            this.ShowFlightDates = true;
            this.ShowMonthly = true;
            this.ShowTotal = true;
            this.EnableTotalsEdit = false;

            this.SlideHeader = string.Empty;
            this.Advertiser = string.Empty;
            this.DecisionMaker = string.Empty;
            this.PresentationDate = DateTime.MinValue;
            this.FlightDatesStart = DateTime.MinValue;
            this.FlightDatesEnd = DateTime.MinValue;
            this.MonthlyValue = 0;
            this.TotalValue = 0;

            this.ItemsState = new List<SimpleSummaryItemState>();
        }

        private string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<ShowAdvertiser>" + this.ShowAdvertiser + @"</ShowAdvertiser>");
            result.AppendLine(@"<ShowDecisionMaker>" + this.ShowDecisionMaker + @"</ShowDecisionMaker>");
            result.AppendLine(@"<ShowPresentationDate>" + this.ShowPresentationDate + @"</ShowPresentationDate>");
            result.AppendLine(@"<ShowFlightDates>" + this.ShowFlightDates + @"</ShowFlightDates>");
            result.AppendLine(@"<ShowMonthly>" + this.ShowMonthly + @"</ShowMonthly>");
            result.AppendLine(@"<ShowTotal>" + this.ShowTotal + @"</ShowTotal>");
            result.AppendLine(@"<EnableTotalsEdit>" + this.EnableTotalsEdit + @"</EnableTotalsEdit>");

            result.AppendLine(@"<SlideHeader>" + this.SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");
            result.AppendLine(@"<Advertiser>" + this.Advertiser.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Advertiser>");
            result.AppendLine(@"<DecisionMaker>" + this.DecisionMaker.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DecisionMaker>");
            result.AppendLine(@"<PresentationDate>" + this.PresentationDate + @"</PresentationDate>");
            result.AppendLine(@"<FlightDatesStart>" + this.FlightDatesStart + @"</FlightDatesStart>");
            result.AppendLine(@"<FlightDatesEnd>" + this.FlightDatesEnd + @"</FlightDatesEnd>");
            result.AppendLine(@"<MonthlyValue>" + this.MonthlyValue + @"</MonthlyValue>");
            result.AppendLine(@"<TotalValue>" + this.TotalValue + @"</TotalValue>");

            result.AppendLine(@"<Items>");
            foreach (SimpleSummaryItemState item in this.ItemsState)
                result.AppendLine(@"<Item>" + item.Serialize() + @"</Item>");
            result.AppendLine(@"</Items>");

            return result.ToString();
        }

        private void Deserialize(XmlNode node)
        {
            bool tempBool = false;
            DateTime tempDateTime = DateTime.Now;
            double tempDouble = 0;

            this.PresentationDate = DateTime.MinValue;
            this.FlightDatesStart = DateTime.MinValue;
            this.FlightDatesEnd = DateTime.MinValue;

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
                    case "ShowPresentationDate":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowPresentationDate = tempBool;
                        break;
                    case "ShowFlightDates":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowFlightDates = tempBool;
                        break;
                    case "ShowMonthly":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowMonthly = tempBool;
                        break;
                    case "ShowTotal":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotal = tempBool;
                        break;
                    case "EnableTotalsEdit":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.EnableTotalsEdit = tempBool;
                        break;
                    case "SlideHeader":
                        this.SlideHeader = childNode.InnerText;
                        break;
                    case "Advertiser":
                        this.Advertiser = childNode.InnerText;
                        break;
                    case "DecisionMaker":
                        this.DecisionMaker = childNode.InnerText;
                        break;
                    case "PresentationDate":
                        if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
                            this.PresentationDate = tempDateTime;
                        break;
                    case "FlightDatesStart":
                        if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
                            this.FlightDatesStart = tempDateTime;
                        break;
                    case "FlightDatesEnd":
                        if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
                            this.FlightDatesEnd = tempDateTime;
                        break;
                    case "MonthlyValue":
                        if (double.TryParse(childNode.InnerText, out tempDouble))
                            this.MonthlyValue = tempDouble;
                        break;
                    case "TotalValue":
                        if (double.TryParse(childNode.InnerText, out tempDouble))
                            this.TotalValue = tempDouble;
                        break;
                    case "Items":
                        this.ItemsState.Clear();
                        foreach (XmlNode itemNode in childNode.ChildNodes)
                        {
                            SimpleSummaryItemState item = new SimpleSummaryItemState();
                            item.Deserialize(itemNode);
                            this.ItemsState.Add(item);
                        }
                        this.ItemsState.Sort((x, y) => x.Order.CompareTo(y.Order));
                        break;
                }
            }
        }

        public void Load(string filePath)
        {
            XmlNode node;
            if (File.Exists(filePath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(filePath);
                node = document.SelectSingleNode(@"/SimpleSummaryState");
                if (node != null)
                    Deserialize(node);
            }
        }

        public void Save()
        {
            DateTime now = DateTime.Now;
            if (!Directory.Exists(Path.Combine(ConfigurationClasses.SettingsManager.Instance.DashboardSaveFolder, "summary")))
                Directory.CreateDirectory(Path.Combine(ConfigurationClasses.SettingsManager.Instance.DashboardSaveFolder, "summary"));
            string fileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.DashboardSaveFolder, "summary", "summary-" + now.ToString("MMddyy") + "-" + now.ToString("hmmsstt") + ".xml");
            using (StreamWriter sw = new StreamWriter(fileName, false))
            {
                sw.Write("<SimpleSummaryState>" + Serialize() + " </SimpleSummaryState>");
                sw.Flush();
            }
        }

        public bool AllowToLoad()
        {
            bool result = false;
            DirectoryInfo saveFolder = new DirectoryInfo(Path.Combine(ConfigurationClasses.SettingsManager.Instance.DashboardSaveFolder, "summary"));
            if (saveFolder.Exists)
                result = saveFolder.GetFiles("*.xml").Length > 0;
            return result;
        }
    }

    public class SimpleSummaryItemState
    {
        public bool ShowValue { get; set; }
        public bool ShowDescription { get; set; }
        public bool ShowMonthly { get; set; }
        public bool ShowTotal { get; set; }

        public int Order { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public double Monthly { get; set; }
        public double Total { get; set; }

        public SimpleSummaryItemState()
        {
            this.ShowValue = true;
            this.ShowDescription = true;
            this.ShowMonthly = true;
            this.ShowTotal = true;

            this.Order = 0;
            this.Value = string.Empty;
            this.Description = string.Empty;
            this.Monthly = 0;
            this.Total = 0;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<ShowDescription>" + this.ShowDescription + @"</ShowDescription>");
            result.AppendLine(@"<ShowMonthly>" + this.ShowMonthly + @"</ShowMonthly>");
            result.AppendLine(@"<ShowTotal>" + this.ShowTotal + @"</ShowTotal>");
            result.AppendLine(@"<ShowValue>" + this.ShowValue + @"</ShowValue>");

            result.AppendLine(@"<Order>" + this.Order + @"</Order>");
            result.AppendLine(@"<Value>" + this.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Value>");
            result.AppendLine(@"<Description>" + this.Description.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Description>");
            result.AppendLine(@"<Monthly>" + this.Monthly + @"</Monthly>");
            result.AppendLine(@"<Total>" + this.Total + @"</Total>");

            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool = false;
            int tempInt = 0;
            double tempDouble = 0;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "ShowDescription":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowDescription = tempBool;
                        break;
                    case "ShowMonthly":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowMonthly = tempBool;
                        break;
                    case "ShowTotal":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowTotal = tempBool;
                        break;
                    case "ShowValue":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.ShowValue = tempBool;
                        break;
                    case "Order":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.Order = tempInt;
                        break;
                    case "Monthly":
                        if (double.TryParse(childNode.InnerText, out tempDouble))
                            this.Monthly = tempDouble;
                        break;
                    case "Total":
                        if (double.TryParse(childNode.InnerText, out tempDouble))
                            this.Total = tempDouble;
                        break;
                    case "Value":
                        this.Value = childNode.InnerText;
                        break;
                    case "Description":
                        this.Description = childNode.InnerText;
                        break;
                }
            }
        }
    }
}
