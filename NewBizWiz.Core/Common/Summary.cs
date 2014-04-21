using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NewBizWiz.Core.Common
{
	public interface ISummarySchedule : ISchedule
	{
		SummarySettings Summary { get; }
		IEnumerable<ISummaryProduct> ProductSummaries { get; }
	}

	public class SummarySettings
	{
		private readonly ISummarySchedule _parent;

		public SummarySettings(ISummarySchedule parent)
		{
			_parent = parent;
			ShowAdvertiser = true;
			ShowDecisionMaker = true;
			ShowPresentationDate = true;
			ShowFlightDates = true;
			ShowMonthly = false;
			ShowTotal = false;
			ShowSignature = true;

			SlideHeader = string.Empty;
		}

		public bool ShowAdvertiser { get; set; }
		public bool ShowDecisionMaker { get; set; }
		public bool ShowPresentationDate { get; set; }
		public bool ShowFlightDates { get; set; }
		public bool ShowMonthly { get; set; }
		public bool ShowTotal { get; set; }
		public bool ShowSignature { get; set; }

		public string SlideHeader { get; set; }
		public decimal? MonthlyValue { get; set; }
		public decimal? TotalValue { get; set; }

		public decimal TotalMonthly
		{
			get
			{
				var items = _parent.ProductSummaries.Select(ps => ps.SummaryItem).Where(si => si.ShowMonthly);
				return items.Any() ? items.Sum(it => it.Monthly) : 0;
			}
		}

		public decimal TotalTotal
		{
			get
			{
				var items = _parent.ProductSummaries.Select(ps => ps.SummaryItem).Where(si => si.ShowTotal);
				return items.Any() ? items.Sum(it => it.Total) : 0;
			}
		}

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<ShowAdvertiser>" + ShowAdvertiser + @"</ShowAdvertiser>");
			result.AppendLine(@"<ShowDecisionMaker>" + ShowDecisionMaker + @"</ShowDecisionMaker>");
			result.AppendLine(@"<ShowPresentationDate>" + ShowPresentationDate + @"</ShowPresentationDate>");
			result.AppendLine(@"<ShowFlightDates>" + ShowFlightDates + @"</ShowFlightDates>");
			result.AppendLine(@"<ShowMonthly>" + ShowMonthly + @"</ShowMonthly>");
			result.AppendLine(@"<ShowTotal>" + ShowTotal + @"</ShowTotal>");
			result.AppendLine(@"<ShowSignature>" + ShowSignature + @"</ShowSignature>");

			result.AppendLine(@"<SlideHeader>" + SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");
			result.AppendLine(@"<MonthlyValue>" + MonthlyValue + @"</MonthlyValue>");
			result.AppendLine(@"<TotalValue>" + TotalValue + @"</TotalValue>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				decimal tempDecimal;
				bool tempBool;
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
					case "ShowSignature":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSignature = tempBool;
						break;
					case "SlideHeader":
						SlideHeader = childNode.InnerText;
						break;
					case "MonthlyValue":
						if (decimal.TryParse(childNode.InnerText, out tempDecimal) && tempDecimal > 0)
							MonthlyValue = tempDecimal; break;
					case "TotalValue":
						if (decimal.TryParse(childNode.InnerText, out tempDecimal) && tempDecimal > 0)
							TotalValue = tempDecimal;
						break;
				}
			}
		}
	}

	public interface ISummaryProduct
	{
		Guid UniqueID { get; }
		string SummaryTitle { get; }
		string SummaryInfo { get; }
		SummaryItem SummaryItem { get; }
	}

	public class SummaryItem
	{
		public SummaryItem(ISummaryProduct parent)
		{
			Parent = parent;

			ShowValue = true;
			ShowDescription = false;
			ShowMonthly = false;
			ShowTotal = false;
		}

		public ISummaryProduct Parent { get; private set; }
		public bool ShowValue { get; set; }
		public bool ShowDescription { get; set; }
		public bool ShowMonthly { get; set; }
		public bool ShowTotal { get; set; }

		private string _description;
		public string Description
		{
			get { return !String.IsNullOrEmpty(_description) ? _description : Parent.SummaryInfo; }
			set { _description = value != Parent.SummaryInfo ? value : null; }
		}
		public decimal Monthly { get; set; }
		public decimal Total { get; set; }

		public bool Commited { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<ShowDescription>" + ShowDescription + @"</ShowDescription>");
			result.AppendLine(@"<ShowMonthly>" + ShowMonthly + @"</ShowMonthly>");
			result.AppendLine(@"<ShowTotal>" + ShowTotal + @"</ShowTotal>");
			result.AppendLine(@"<ShowValue>" + ShowValue + @"</ShowValue>");

			if (!String.IsNullOrEmpty(_description))
				result.AppendLine(@"<Description>" + _description.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Description>");
			result.AppendLine(@"<Monthly>" + Monthly + @"</Monthly>");
			result.AppendLine(@"<Total>" + Total + @"</Total>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				bool tempBool;
				decimal tempDecimal;
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
					case "Monthly":
						if (decimal.TryParse(childNode.InnerText, out tempDecimal))
							Monthly = tempDecimal;
						break;
					case "Total":
						if (decimal.TryParse(childNode.InnerText, out tempDecimal))
							Total = tempDecimal;
						break;
					case "Description":
						_description = childNode.InnerText;
						break;
				}
			}
			Commited = true;
		}
	}
}
