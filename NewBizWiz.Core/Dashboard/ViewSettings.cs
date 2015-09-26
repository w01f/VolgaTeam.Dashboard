using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using NewBizWiz.Core.Common;

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

	public abstract class BaseSlideState
	{
		public bool IsNewSolution { get; set; }

		protected BaseSlideState()
		{
			IsNewSolution = true;
		}

		protected virtual string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<IsNewSolution>" + IsNewSolution + @"</IsNewSolution>");
			return result.ToString();
		}

		protected virtual void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "IsNewSolution":
						bool tempBool;
						if (bool.TryParse(childNode.InnerText, out tempBool))
							IsNewSolution = tempBool;
						break;
				}
			}
		}
	}

	public class CoverState : BaseSlideState
	{
		public CoverState()
			: base()
		{
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

		protected override string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(base.Serialize());
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

		protected override void Deserialize(XmlNode node)
		{
			base.Deserialize(node);
			PresentationDate = DateTime.MinValue;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				bool tempBool;
				switch (childNode.Name)
				{
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
						DateTime tempDateTime;
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
			if (!File.Exists(filePath)) return;
			var document = new XmlDocument();
			document.Load(filePath);
			node = document.SelectSingleNode(@"/CoverState");
			if (node != null)
				Deserialize(node);
		}

		public void Save(string fileName = "")
		{
			string filePath;
			if (!Directory.Exists(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "cover", "templates")))
				Directory.CreateDirectory(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "cover", "templates"));
			if (String.IsNullOrEmpty(fileName))
			{
				var now = DateTime.Now;
				fileName = "cover-" + now.ToString("MMddyy") + "-" + now.ToString("hmmsstt");
				filePath = Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "cover", fileName + ".xml");
			}
			else
			{
				filePath = Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "cover", "templates", fileName + ".xml");
			}
			using (var sw = new StreamWriter(filePath, false))
			{
				sw.Write("<CoverState>" + Serialize() + " </CoverState>");
				sw.Flush();
			}
		}

		public bool AllowToLoad()
		{
			var result = false;
			var filesFolder = new DirectoryInfo(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "cover"));
			if (filesFolder.Exists)
				result = filesFolder.GetFiles("*.xml").Length > 0;
			var templatesFolder = new DirectoryInfo(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "cover", "templates"));
			if (templatesFolder.Exists)
				result |= templatesFolder.GetFiles("*.xml").Length > 0;
			return result;
		}
	}

	public class LeadoffStatementState : BaseSlideState
	{
		public LeadoffStatementState()
			: base()
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


		protected override string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(base.Serialize());
			result.AppendLine(@"<ShowStatement1>" + ShowStatement1 + @"</ShowStatement1>");
			result.AppendLine(@"<ShowStatement2>" + ShowStatement2 + @"</ShowStatement2>");
			result.AppendLine(@"<ShowStatement3>" + ShowStatement3 + @"</ShowStatement3>");
			result.AppendLine(@"<SlideHeader>" + SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");
			result.AppendLine(@"<Statement1>" + Statement1.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Statement1>");
			result.AppendLine(@"<Statement2>" + Statement2.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Statement2>");
			result.AppendLine(@"<Statement3>" + Statement3.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Statement3>");
			return result.ToString();
		}

		protected override void Deserialize(XmlNode node)
		{
			base.Deserialize(node);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				bool tempBool;
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

		public void Save(string fileName = "")
		{
			string filePath;
			if (!Directory.Exists(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "intro", "templates")))
				Directory.CreateDirectory(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "intro", "templates"));
			if (String.IsNullOrEmpty(fileName))
			{
				var now = DateTime.Now;
				fileName = "intro-" + now.ToString("MMddyy") + "-" + now.ToString("hmmsstt");
				filePath = Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "intro", fileName + ".xml");
			}
			else
			{
				filePath = Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "intro", "templates", fileName + ".xml");
			}
			using (var sw = new StreamWriter(filePath, false))
			{
				sw.Write("<LeadoffStatementState>" + Serialize() + " </LeadoffStatementState> ");
				sw.Flush();
			}
		}

		public bool AllowToLoad()
		{
			var result = false;
			var filesFolder = new DirectoryInfo(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "intro"));
			if (filesFolder.Exists)
				result = filesFolder.GetFiles("*.xml").Length > 0;
			var templatesFolder = new DirectoryInfo(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "intro", "templates"));
			if (templatesFolder.Exists)
				result |= templatesFolder.GetFiles("*.xml").Length > 0;
			return result;
		}
	}

	public class ClientGoalsState : BaseSlideState
	{
		public ClientGoalsState()
			: base()
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


		protected override string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(base.Serialize());
			result.AppendLine(@"<SlideHeader>" + SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");
			result.AppendLine(@"<Goal1>" + Goal1.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Goal1>");
			result.AppendLine(@"<Goal2>" + Goal2.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Goal2>");
			result.AppendLine(@"<Goal3>" + Goal3.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Goal3>");
			result.AppendLine(@"<Goal4>" + Goal4.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Goal4>");
			result.AppendLine(@"<Goal5>" + Goal5.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Goal5>");
			return result.ToString();
		}

		protected override void Deserialize(XmlNode node)
		{
			base.Deserialize(node);
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
			if (File.Exists(filePath))
			{
				var document = new XmlDocument();
				document.Load(filePath);
				var node = document.SelectSingleNode(@"/ClientGoalsState");
				if (node != null)
					Deserialize(node);
			}
		}

		public void Save(string fileName = "")
		{
			string filePath;
			if (!Directory.Exists(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "needsgoals", "templates")))
				Directory.CreateDirectory(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "needsgoals", "templates"));
			if (String.IsNullOrEmpty(fileName))
			{
				var now = DateTime.Now;
				fileName = "needsgoals-" + now.ToString("MMddyy") + "-" + now.ToString("hmmsstt");
				filePath = Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "needsgoals", fileName + ".xml");
			}
			else
			{
				filePath = Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "needsgoals", "templates", fileName + ".xml");
			}
			using (var sw = new StreamWriter(filePath, false))
			{
				sw.Write("<ClientGoalsState>" + Serialize() + " </ClientGoalsState> ");
				sw.Flush();
			}
		}

		public bool AllowToLoad()
		{
			var result = false;
			var filesFolder = new DirectoryInfo(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "needsgoals"));
			if (filesFolder.Exists)
				result = filesFolder.GetFiles("*.xml").Length > 0;
			var templatesFolder = new DirectoryInfo(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "needsgoals", "templates"));
			if (templatesFolder.Exists)
				result |= templatesFolder.GetFiles("*.xml").Length > 0;
			return result;
		}
	}

	public class TargetCustomersState : BaseSlideState
	{
		public TargetCustomersState()
			: base()
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


		protected override string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(base.Serialize());
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

		protected override void Deserialize(XmlNode node)
		{
			base.Deserialize(node);
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

		public void Save(string fileName = "")
		{
			string filePath;
			if (!Directory.Exists(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "target", "templates")))
				Directory.CreateDirectory(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "target", "templates"));
			if (String.IsNullOrEmpty(fileName))
			{
				var now = DateTime.Now;
				fileName = "target-" + now.ToString("MMddyy") + "-" + now.ToString("hmmsstt");
				filePath = Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "target", fileName + ".xml");
			}
			else
			{
				filePath = Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "target", "templates", fileName + ".xml");
			}
			using (var sw = new StreamWriter(filePath, false))
			{
				sw.Write("<TargetCustomersState>" + Serialize() + " </TargetCustomersState> ");
				sw.Flush();
			}
		}

		public bool AllowToLoad()
		{
			var result = false;
			var filesFolder = new DirectoryInfo(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "target"));
			if (filesFolder.Exists)
				result = filesFolder.GetFiles("*.xml").Length > 0;
			var templatesFolder = new DirectoryInfo(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "target", "templates"));
			if (templatesFolder.Exists)
				result |= templatesFolder.GetFiles("*.xml").Length > 0;
			return result;
		}
	}

	public class SimpleSummaryState : BaseSlideState
	{
		public SimpleSummaryState()
			: base()
		{
			ShowAdvertiser = true;
			ShowDecisionMaker = true;
			ShowPresentationDate = true;
			ShowFlightDates = true;
			ShowMonthly = false;
			ShowTotal = false;

			SlideHeader = string.Empty;
			Advertiser = string.Empty;
			DecisionMaker = string.Empty;
			PresentationDate = DateTime.MinValue;
			FlightDatesStart = DateTime.MinValue;
			FlightDatesEnd = DateTime.MinValue;

			ItemsState = new List<SimpleSummaryItemState>();

			ContractSettings = new ContractSettings();
		}

		public bool ShowAdvertiser { get; set; }
		public bool ShowDecisionMaker { get; set; }
		public bool ShowPresentationDate { get; set; }
		public bool ShowFlightDates { get; set; }
		public bool ShowMonthly { get; set; }
		public bool ShowTotal { get; set; }
		public bool TableOutput { get; set; }

		public string SlideHeader { get; set; }
		public string Advertiser { get; set; }
		public string DecisionMaker { get; set; }
		public DateTime PresentationDate { get; set; }
		public DateTime FlightDatesStart { get; set; }
		public DateTime FlightDatesEnd { get; set; }
		public decimal? MonthlyValue { get; set; }
		public decimal? TotalValue { get; set; }

		public List<SimpleSummaryItemState> ItemsState { get; set; }

		public ContractSettings ContractSettings { get; private set; }

		protected override string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(base.Serialize());
			result.AppendLine(@"<ShowAdvertiser>" + ShowAdvertiser + @"</ShowAdvertiser>");
			result.AppendLine(@"<ShowDecisionMaker>" + ShowDecisionMaker + @"</ShowDecisionMaker>");
			result.AppendLine(@"<ShowPresentationDate>" + ShowPresentationDate + @"</ShowPresentationDate>");
			result.AppendLine(@"<ShowFlightDates>" + ShowFlightDates + @"</ShowFlightDates>");
			result.AppendLine(@"<ShowMonthly>" + ShowMonthly + @"</ShowMonthly>");
			result.AppendLine(@"<ShowTotal>" + ShowTotal + @"</ShowTotal>");
			result.AppendLine(@"<TableOutput>" + TableOutput + @"</TableOutput>");

			result.AppendLine(@"<SlideHeader>" + SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");
			result.AppendLine(@"<Advertiser>" + Advertiser.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Advertiser>");
			result.AppendLine(@"<DecisionMaker>" + DecisionMaker.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DecisionMaker>");
			result.AppendLine(@"<PresentationDate>" + PresentationDate + @"</PresentationDate>");
			result.AppendLine(@"<FlightDatesStart>" + FlightDatesStart + @"</FlightDatesStart>");
			result.AppendLine(@"<FlightDatesEnd>" + FlightDatesEnd + @"</FlightDatesEnd>");
			if (MonthlyValue.HasValue)
				result.AppendLine(@"<MonthlyValue>" + MonthlyValue + @"</MonthlyValue>");
			if (TotalValue.HasValue)
				result.AppendLine(@"<TotalValue>" + TotalValue + @"</TotalValue>");

			result.AppendLine(@"<Items>");
			foreach (SimpleSummaryItemState item in ItemsState)
				result.AppendLine(@"<Item>" + item.Serialize() + @"</Item>");
			result.AppendLine(@"</Items>");

			if (ContractSettings.IsConfigured)
				result.AppendLine(String.Format("<ContractSettings>{0}</ContractSettings>", ContractSettings.Serialize()));

			return result.ToString();
		}

		protected override void Deserialize(XmlNode node)
		{
			base.Deserialize(node);
			PresentationDate = DateTime.MinValue;
			FlightDatesStart = DateTime.MinValue;
			FlightDatesEnd = DateTime.MinValue;

			foreach (XmlNode childNode in node.ChildNodes)
			{
				bool tempBool = false;
				DateTime tempDateTime;
				decimal tempDecimal = 0;
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
					case "TableOutput":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							TableOutput = tempBool;
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
						if (Decimal.TryParse(childNode.InnerText, out tempDecimal))
							MonthlyValue = tempDecimal;
						break;
					case "TotalValue":
						if (Decimal.TryParse(childNode.InnerText, out tempDecimal))
							TotalValue = tempDecimal;
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
					case "ContractSettings":
						ContractSettings.Deserialize(childNode);
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

		public void Save(string fileName = "")
		{
			string filePath;
			if (!Directory.Exists(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "summary", "templates")))
				Directory.CreateDirectory(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "summary", "templates"));
			if (String.IsNullOrEmpty(fileName))
			{
				var now = DateTime.Now;
				fileName = "summary-" + now.ToString("MMddyy") + "-" + now.ToString("hmmsstt");
				filePath = Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "summary", fileName + ".xml");
			}
			else
			{
				filePath = Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "summary", "templates", fileName + ".xml");
			}
			using (var sw = new StreamWriter(filePath, false))
			{
				sw.Write("<SimpleSummaryState>" + Serialize() + " </SimpleSummaryState>");
				sw.Flush();
			}
		}

		public bool AllowToLoad()
		{
			var result = false;
			var filesFolder = new DirectoryInfo(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "summary"));
			if (filesFolder.Exists)
				result = filesFolder.GetFiles("*.xml").Length > 0;
			var templatesFolder = new DirectoryInfo(Path.Combine(SettingsManager.Instance.DashboardSaveFolder, "summary", "templates"));
			if (templatesFolder.Exists)
				result |= templatesFolder.GetFiles("*.xml").Length > 0;
			return result;
		}
	}

	public class SimpleSummaryItemState
	{
		public SimpleSummaryItemState()
		{
			ShowValue = true;
			ShowDescription = false;
			ShowMonthly = false;
			ShowTotal = false;

			Order = 0;
			Value = string.Empty;
			Description = string.Empty;
		}

		public bool ShowValue { get; set; }
		public bool ShowDescription { get; set; }
		public bool ShowMonthly { get; set; }
		public bool ShowTotal { get; set; }

		public int Order { get; set; }
		public string Value { get; set; }
		public string Description { get; set; }
		public decimal? Monthly { get; set; }
		public decimal? Total { get; set; }

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
			if (Monthly.HasValue)
				result.AppendLine(@"<Monthly>" + Monthly + @"</Monthly>");
			if (Total.HasValue)
				result.AppendLine(@"<Total>" + Total + @"</Total>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool = false;
			int tempInt = 0;
			decimal tempDecimal = 0;

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
						if (Decimal.TryParse(childNode.InnerText, out tempDecimal))
							Monthly = tempDecimal;
						break;
					case "Total":
						if (Decimal.TryParse(childNode.InnerText, out tempDecimal))
							Total = tempDecimal;
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