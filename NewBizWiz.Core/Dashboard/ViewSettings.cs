﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace NewBizWiz.Core.Dashboard
{
	public class ViewSettingsManager
	{
		private static readonly ViewSettingsManager _instance = new ViewSettingsManager();

		private ViewSettingsManager()
		{
			CoverState = new CoverState();
			LeadoffStatementState = new LeadoffStatementState();
			ClientGoalsState = new ClientGoalsState();
			TargetCustomersState = new TargetCustomersState();
			SimpleSummaryState = new SimpleSummaryState();
		}

		public CoverState CoverState { get; set; }
		public LeadoffStatementState LeadoffStatementState { get; set; }
		public ClientGoalsState ClientGoalsState { get; set; }
		public TargetCustomersState TargetCustomersState { get; set; }
		public SimpleSummaryState SimpleSummaryState { get; set; }


		public static ViewSettingsManager Instance
		{
			get { return _instance; }
		}
	}

	public class CoverState
	{
		public CoverState()
		{
			IsNewSolution = true;
			ShowSalesRep = false;
			ShowPresentationDate = false;
			AddAsPageOne = true;
			UseGenericCover = false;

			SlideHeader = string.Empty;
			Advertiser = string.Empty;
			DecisionMaker = string.Empty;
			SalesRep = string.Empty;
			PresentationDate = DateTime.MinValue;
			Quote = new Quote();
		}

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
		public Quote Quote { get; set; }

		private string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<IsNewSolution>" + IsNewSolution + @"</IsNewSolution>");
			result.AppendLine(@"<AddAsPageOne>" + AddAsPageOne + @"</AddAsPageOne>");
			result.AppendLine(@"<UseGenericCover>" + UseGenericCover + @"</UseGenericCover>");
			result.AppendLine(@"<ShowSalesRep>" + ShowSalesRep + @"</ShowSalesRep>");
			result.AppendLine(@"<ShowPresentationDate>" + ShowPresentationDate + @"</ShowPresentationDate>");
			result.AppendLine(@"<SlideHeader>" + SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");
			result.AppendLine(@"<Advertiser>" + Advertiser.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Advertiser>");
			result.AppendLine(@"<DecisionMaker>" + DecisionMaker.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DecisionMaker>");
			result.AppendLine(@"<SalesRep>" + SalesRep.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SalesRep>");
			result.AppendLine(@"<PresentationDate>" + PresentationDate + @"</PresentationDate>");
			result.AppendLine(@"<Quote>" + Quote.Serialize() + @"</Quote>");

			return result.ToString();
		}

		private void Deserialize(XmlNode node)
		{
			bool tempBool = false;
			DateTime tempDateTime = DateTime.Now;

			PresentationDate = DateTime.MinValue;

			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "IsNewSolution":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							IsNewSolution = tempBool;
						break;
					case "AddAsPageOne":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							AddAsPageOne = tempBool;
						break;
					case "UseGenericCover":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							UseGenericCover = tempBool;
						break;
					case "ShowPresentationDate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPresentationDate = tempBool;
						break;
					case "ShowSalesRep":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSalesRep = tempBool;
						break;
					case "SlideHeader":
						SlideHeader = childNode.InnerText;
						break;
					case "Advertiser":
						Advertiser = childNode.InnerText;
						break;
					case "DecisionMaker":
						DecisionMaker = childNode.InnerText;
						break;
					case "SalesRep":
						SalesRep = childNode.InnerText;
						break;
					case "PresentationDate":
						if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
							PresentationDate = tempDateTime;
						break;
					case "Quote":
						Quote.Deserialize(childNode);
						break;
				}
			}
		}

		public string SerializeSalesRep()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<ShowSalesRep>" + ShowSalesRep + @"</ShowSalesRep>");
			result.AppendLine(@"<ValueSalesRep>" + SalesRep.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</ValueSalesRep>");

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
							ShowSalesRep = tempBool;
						break;
					case "ValueSalesRep":
						SalesRep = childNode.InnerText;
						break;
				}
			}
		}

		public void Load(string filePath)
		{
			XmlNode node;
			if (File.Exists(filePath))
			{
				var document = new XmlDocument();
				document.Load(filePath);
				node = document.SelectSingleNode(@"/CoverState");
				if (node != null)
					Deserialize(node);
			}
		}

		public void Save()
		{
			DateTime now = DateTime.Now;
			if (!Directory.Exists(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "cover")))
				Directory.CreateDirectory(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "cover"));
			string fileName = Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "cover", "cover-" + now.ToString("MMddyy") + "-" + now.ToString("hmmsstt") + ".xml");
			using (var sw = new StreamWriter(fileName, false))
			{
				sw.Write("<CoverState>" + Serialize() + " </CoverState>");
				sw.Flush();
			}
		}

		public bool AllowToLoad()
		{
			bool result = false;
			var saveFolder = new DirectoryInfo(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "cover"));
			if (saveFolder.Exists)
				result = saveFolder.GetFiles("*.xml").Length > 0;
			return result;
		}
	}

	public class LeadoffStatementState
	{
		public LeadoffStatementState()
		{
			ShowStatement1 = true;
			ShowStatement2 = false;
			ShowStatement3 = false;
			SlideHeader = string.Empty;
			Statement1 = string.Empty;
			Statement2 = string.Empty;
			Statement3 = string.Empty;
		}

		public bool ShowStatement1 { get; set; }
		public bool ShowStatement2 { get; set; }
		public bool ShowStatement3 { get; set; }
		public string SlideHeader { get; set; }
		public string Statement1 { get; set; }
		public string Statement2 { get; set; }
		public string Statement3 { get; set; }


		private string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<ShowStatement1>" + ShowStatement1 + @"</ShowStatement1>");
			result.AppendLine(@"<ShowStatement2>" + ShowStatement2 + @"</ShowStatement2>");
			result.AppendLine(@"<ShowStatement3>" + ShowStatement3 + @"</ShowStatement3>");
			result.AppendLine(@"<SlideHeader>" + SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");
			result.AppendLine(@"<Statement1>" + Statement1.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Statement1>");
			result.AppendLine(@"<Statement2>" + Statement2.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Statement2>");
			result.AppendLine(@"<Statement3>" + Statement3.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Statement3>");

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
							ShowStatement1 = tempBool;
						break;
					case "ShowStatement2":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowStatement2 = tempBool;
						break;
					case "ShowStatement3":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowStatement3 = tempBool;
						break;
					case "SlideHeader":
						SlideHeader = childNode.InnerText;
						break;
					case "Statement1":
						Statement1 = childNode.InnerText;
						break;
					case "Statement2":
						Statement2 = childNode.InnerText;
						break;
					case "Statement3":
						Statement3 = childNode.InnerText;
						break;
				}
			}
		}

		public void Load(string filePath)
		{
			XmlNode node;
			if (File.Exists(filePath))
			{
				var document = new XmlDocument();
				document.Load(filePath);
				node = document.SelectSingleNode(@"/LeadoffStatementState");
				if (node != null)
					Deserialize(node);
			}
		}

		public void Save()
		{
			DateTime now = DateTime.Now;
			if (!Directory.Exists(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "intro")))
				Directory.CreateDirectory(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "intro"));
			string fileName = Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "intro", "intro-" + now.ToString("MMddyy") + "-" + now.ToString("hmmsstt") + ".xml");
			using (var sw = new StreamWriter(fileName, false))
			{
				sw.Write("<LeadoffStatementState>" + Serialize() + " </LeadoffStatementState> ");
				sw.Flush();
			}
		}

		public bool AllowToLoad()
		{
			bool result = false;
			var saveFolder = new DirectoryInfo(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "intro"));
			if (saveFolder.Exists)
				result = saveFolder.GetFiles("*.xml").Length > 0;
			return result;
		}
	}

	public class ClientGoalsState
	{
		public ClientGoalsState()
		{
			SlideHeader = string.Empty;
			Goal1 = string.Empty;
			Goal2 = string.Empty;
			Goal3 = string.Empty;
			Goal4 = string.Empty;
			Goal5 = string.Empty;
		}

		public string SlideHeader { get; set; }
		public string Goal1 { get; set; }
		public string Goal2 { get; set; }
		public string Goal3 { get; set; }
		public string Goal4 { get; set; }
		public string Goal5 { get; set; }


		private string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<SlideHeader>" + SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");
			result.AppendLine(@"<Goal1>" + Goal1.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Goal1>");
			result.AppendLine(@"<Goal2>" + Goal2.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Goal2>");
			result.AppendLine(@"<Goal3>" + Goal3.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Goal3>");
			result.AppendLine(@"<Goal4>" + Goal4.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Goal4>");
			result.AppendLine(@"<Goal5>" + Goal5.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Goal5>");

			return result.ToString();
		}

		private void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "SlideHeader":
						SlideHeader = childNode.InnerText;
						break;
					case "Goal1":
						Goal1 = childNode.InnerText;
						break;
					case "Goal2":
						Goal2 = childNode.InnerText;
						break;
					case "Goal3":
						Goal3 = childNode.InnerText;
						break;
					case "Goal4":
						Goal4 = childNode.InnerText;
						break;
					case "Goal5":
						Goal5 = childNode.InnerText;
						break;
				}
			}
		}

		public void Load(string filePath)
		{
			XmlNode node;
			if (File.Exists(filePath))
			{
				var document = new XmlDocument();
				document.Load(filePath);
				node = document.SelectSingleNode(@"/ClientGoalsState");
				if (node != null)
					Deserialize(node);
			}
		}

		public void Save()
		{
			DateTime now = DateTime.Now;
			if (!Directory.Exists(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "needsgoals")))
				Directory.CreateDirectory(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "needsgoals"));
			string fileName = Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "needsgoals", "needsgoals-" + now.ToString("MMddyy") + "-" + now.ToString("hmmsstt") + ".xml");
			using (var sw = new StreamWriter(fileName, false))
			{
				sw.Write("<ClientGoalsState>" + Serialize() + " </ClientGoalsState> ");
				sw.Flush();
			}
		}

		public bool AllowToLoad()
		{
			bool result = false;
			var saveFolder = new DirectoryInfo(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "needsgoals"));
			if (saveFolder.Exists)
				result = saveFolder.GetFiles("*.xml").Length > 0;
			return result;
		}
	}

	public class TargetCustomersState
	{
		public TargetCustomersState()
		{
			SlideHeader = string.Empty;
			Demo = new List<string>();
			Income = new List<string>();
			Geographic = new List<string>();
		}

		public string SlideHeader { get; set; }
		public List<string> Demo { get; set; }
		public List<string> Income { get; set; }
		public List<string> Geographic { get; set; }


		private string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<SlideHeader>" + SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");
			result.AppendLine(@"<Demo>");
			foreach (string demo in Demo)
				result.AppendLine(@"<Value>" + demo.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Value>");
			result.AppendLine(@"</Demo>");
			result.AppendLine(@"<Income>");
			foreach (string income in Income)
				result.AppendLine(@"<Value>" + income.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Value>");
			result.AppendLine(@"</Income>");
			result.AppendLine(@"<Geographic>");
			foreach (string geographic in Geographic)
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
						SlideHeader = childNode.InnerText;
						break;
					case "Demo":
						Demo.Clear();
						foreach (XmlNode valueNode in childNode.ChildNodes)
							Demo.Add(valueNode.InnerText);
						break;
					case "Income":
						Income.Clear();
						foreach (XmlNode valueNode in childNode.ChildNodes)
							Income.Add(valueNode.InnerText);
						break;
					case "Geographic":
						Geographic.Clear();
						foreach (XmlNode valueNode in childNode.ChildNodes)
							Geographic.Add(valueNode.InnerText);
						break;
				}
			}
		}

		public void Load(string filePath)
		{
			XmlNode node;
			if (File.Exists(filePath))
			{
				var document = new XmlDocument();
				document.Load(filePath);
				node = document.SelectSingleNode(@"/TargetCustomersState");
				if (node != null)
					Deserialize(node);
			}
		}

		public void Save()
		{
			DateTime now = DateTime.Now;
			if (!Directory.Exists(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "target")))
				Directory.CreateDirectory(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "target"));
			string fileName = Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "target", "target-" + now.ToString("MMddyy") + "-" + now.ToString("hmmsstt") + ".xml");
			using (var sw = new StreamWriter(fileName, false))
			{
				sw.Write("<TargetCustomersState>" + Serialize() + " </TargetCustomersState> ");
				sw.Flush();
			}
		}

		public bool AllowToLoad()
		{
			bool result = false;
			var saveFolder = new DirectoryInfo(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "target"));
			if (saveFolder.Exists)
				result = saveFolder.GetFiles("*.xml").Length > 0;
			return result;
		}
	}

	public class SimpleSummaryState
	{
		public SimpleSummaryState()
		{
			ShowAdvertiser = true;
			ShowDecisionMaker = true;
			ShowPresentationDate = true;
			ShowFlightDates = true;
			ShowMonthly = true;
			ShowTotal = true;
			EnableTotalsEdit = false;

			SlideHeader = string.Empty;
			Advertiser = string.Empty;
			DecisionMaker = string.Empty;
			PresentationDate = DateTime.MinValue;
			FlightDatesStart = DateTime.MinValue;
			FlightDatesEnd = DateTime.MinValue;
			MonthlyValue = 0;
			TotalValue = 0;

			ItemsState = new List<SimpleSummaryItemState>();
		}

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

		private string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<ShowAdvertiser>" + ShowAdvertiser + @"</ShowAdvertiser>");
			result.AppendLine(@"<ShowDecisionMaker>" + ShowDecisionMaker + @"</ShowDecisionMaker>");
			result.AppendLine(@"<ShowPresentationDate>" + ShowPresentationDate + @"</ShowPresentationDate>");
			result.AppendLine(@"<ShowFlightDates>" + ShowFlightDates + @"</ShowFlightDates>");
			result.AppendLine(@"<ShowMonthly>" + ShowMonthly + @"</ShowMonthly>");
			result.AppendLine(@"<ShowTotal>" + ShowTotal + @"</ShowTotal>");
			result.AppendLine(@"<EnableTotalsEdit>" + EnableTotalsEdit + @"</EnableTotalsEdit>");

			result.AppendLine(@"<SlideHeader>" + SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");
			result.AppendLine(@"<Advertiser>" + Advertiser.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Advertiser>");
			result.AppendLine(@"<DecisionMaker>" + DecisionMaker.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DecisionMaker>");
			result.AppendLine(@"<PresentationDate>" + PresentationDate + @"</PresentationDate>");
			result.AppendLine(@"<FlightDatesStart>" + FlightDatesStart + @"</FlightDatesStart>");
			result.AppendLine(@"<FlightDatesEnd>" + FlightDatesEnd + @"</FlightDatesEnd>");
			result.AppendLine(@"<MonthlyValue>" + MonthlyValue + @"</MonthlyValue>");
			result.AppendLine(@"<TotalValue>" + TotalValue + @"</TotalValue>");

			result.AppendLine(@"<Items>");
			foreach (SimpleSummaryItemState item in ItemsState)
				result.AppendLine(@"<Item>" + item.Serialize() + @"</Item>");
			result.AppendLine(@"</Items>");

			return result.ToString();
		}

		private void Deserialize(XmlNode node)
		{
			bool tempBool = false;
			DateTime tempDateTime = DateTime.Now;
			double tempDouble = 0;

			PresentationDate = DateTime.MinValue;
			FlightDatesStart = DateTime.MinValue;
			FlightDatesEnd = DateTime.MinValue;

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
					case "ShowPresentationDate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPresentationDate = tempBool;
						break;
					case "ShowFlightDates":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowFlightDates = tempBool;
						break;
					case "ShowMonthly":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowMonthly = tempBool;
						break;
					case "ShowTotal":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotal = tempBool;
						break;
					case "EnableTotalsEdit":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableTotalsEdit = tempBool;
						break;
					case "SlideHeader":
						SlideHeader = childNode.InnerText;
						break;
					case "Advertiser":
						Advertiser = childNode.InnerText;
						break;
					case "DecisionMaker":
						DecisionMaker = childNode.InnerText;
						break;
					case "PresentationDate":
						if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
							PresentationDate = tempDateTime;
						break;
					case "FlightDatesStart":
						if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
							FlightDatesStart = tempDateTime;
						break;
					case "FlightDatesEnd":
						if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
							FlightDatesEnd = tempDateTime;
						break;
					case "MonthlyValue":
						if (double.TryParse(childNode.InnerText, out tempDouble))
							MonthlyValue = tempDouble;
						break;
					case "TotalValue":
						if (double.TryParse(childNode.InnerText, out tempDouble))
							TotalValue = tempDouble;
						break;
					case "Items":
						ItemsState.Clear();
						foreach (XmlNode itemNode in childNode.ChildNodes)
						{
							var item = new SimpleSummaryItemState();
							item.Deserialize(itemNode);
							ItemsState.Add(item);
						}
						ItemsState.Sort((x, y) => x.Order.CompareTo(y.Order));
						break;
				}
			}
		}

		public void Load(string filePath)
		{
			XmlNode node;
			if (File.Exists(filePath))
			{
				var document = new XmlDocument();
				document.Load(filePath);
				node = document.SelectSingleNode(@"/SimpleSummaryState");
				if (node != null)
					Deserialize(node);
			}
		}

		public void Save()
		{
			DateTime now = DateTime.Now;
			if (!Directory.Exists(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "summary")))
				Directory.CreateDirectory(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "summary"));
			string fileName = Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "summary", "summary-" + now.ToString("MMddyy") + "-" + now.ToString("hmmsstt") + ".xml");
			using (var sw = new StreamWriter(fileName, false))
			{
				sw.Write("<SimpleSummaryState>" + Serialize() + " </SimpleSummaryState>");
				sw.Flush();
			}
		}

		public bool AllowToLoad()
		{
			bool result = false;
			var saveFolder = new DirectoryInfo(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "summary"));
			if (saveFolder.Exists)
				result = saveFolder.GetFiles("*.xml").Length > 0;
			return result;
		}
	}

	public class SimpleSummaryItemState
	{
		public SimpleSummaryItemState()
		{
			ShowValue = true;
			ShowDescription = true;
			ShowMonthly = true;
			ShowTotal = true;

			Order = 0;
			Value = string.Empty;
			Description = string.Empty;
			Monthly = 0;
			Total = 0;
		}

		public bool ShowValue { get; set; }
		public bool ShowDescription { get; set; }
		public bool ShowMonthly { get; set; }
		public bool ShowTotal { get; set; }

		public int Order { get; set; }
		public string Value { get; set; }
		public string Description { get; set; }
		public double Monthly { get; set; }
		public double Total { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<ShowDescription>" + ShowDescription + @"</ShowDescription>");
			result.AppendLine(@"<ShowMonthly>" + ShowMonthly + @"</ShowMonthly>");
			result.AppendLine(@"<ShowTotal>" + ShowTotal + @"</ShowTotal>");
			result.AppendLine(@"<ShowValue>" + ShowValue + @"</ShowValue>");

			result.AppendLine(@"<Order>" + Order + @"</Order>");
			result.AppendLine(@"<Value>" + Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Value>");
			result.AppendLine(@"<Description>" + Description.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Description>");
			result.AppendLine(@"<Monthly>" + Monthly + @"</Monthly>");
			result.AppendLine(@"<Total>" + Total + @"</Total>");

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
							ShowDescription = tempBool;
						break;
					case "ShowMonthly":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowMonthly = tempBool;
						break;
					case "ShowTotal":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotal = tempBool;
						break;
					case "ShowValue":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowValue = tempBool;
						break;
					case "Order":
						if (int.TryParse(childNode.InnerText, out tempInt))
							Order = tempInt;
						break;
					case "Monthly":
						if (double.TryParse(childNode.InnerText, out tempDouble))
							Monthly = tempDouble;
						break;
					case "Total":
						if (double.TryParse(childNode.InnerText, out tempDouble))
							Total = tempDouble;
						break;
					case "Value":
						Value = childNode.InnerText;
						break;
					case "Description":
						Description = childNode.InnerText;
						break;
				}
			}
		}
	}
}