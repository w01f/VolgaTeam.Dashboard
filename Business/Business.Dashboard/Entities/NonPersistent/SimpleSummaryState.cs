using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Output;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Dashboard.Entities.NonPersistent
{
	public class SimpleSummaryState : BaseSlideState
	{
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

		public ContractSettings ContractSettings { get; }

		public SimpleSummaryState()
		{
			SaveFolder = new StorageDirectory(AppProfileManager.Instance.AppSaveFolder.RelativePathParts.Merge(new[] { "closing summary" }));
			TemplatesFolder = new StorageDirectory(AppProfileManager.Instance.AppSaveFolder.RelativePathParts.Merge(new[] { "closing summary", "templates" }));

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

		protected override String FileNamePrefix => "summary";

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

		public async Task Save(string fileName = "")
		{
			var file = GetSaveFile(fileName);
			using (var sw = new StreamWriter(file.LocalPath, false))
			{
				sw.Write("<SimpleSummaryState>" + Serialize() + " </SimpleSummaryState>");
				sw.Flush();
			}
			//await file.Upload();
		}
	}
}
