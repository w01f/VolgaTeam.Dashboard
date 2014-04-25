using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NewBizWiz.Core.Common
{
	public interface ISummarySchedule : ISchedule
	{
		BaseSummarySettings ProductSummary { get; }
		CustomSummarySettings CustomSummary { get; }
		IEnumerable<ISummaryProduct> ProductSummaries { get; }
	}

	public class BaseSummarySettings
	{
		public BaseSummarySettings()
		{
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

		public virtual string Serialize()
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
			if (MonthlyValue.HasValue)
				result.AppendLine(@"<MonthlyValue>" + MonthlyValue + @"</MonthlyValue>");
			if (TotalValue.HasValue)
				result.AppendLine(@"<TotalValue>" + TotalValue + @"</TotalValue>");

			return result.ToString();
		}

		public virtual void Deserialize(XmlNode node)
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

	public class CustomSummarySettings : BaseSummarySettings
	{
		public List<CustomSummaryItem> Items { get; private set; }

		public decimal? TotalMonthly
		{
			get { return Items.Where(it => it.ShowMonthly && it.Monthly.HasValue).Sum(it => it.Monthly); }
		}

		public decimal? TotalTotal
		{
			get { return Items.Where(it => it.ShowTotal && it.Total.HasValue).Sum(it => it.Total); }
		}

		public CustomSummarySettings()
		{
			Items = new List<CustomSummaryItem>();
			AddItem();
			AddItem();
		}

		public override string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(base.Serialize());
			foreach (var item in Items)
				result.AppendLine(@"<SummaryItem>" + item.Serialize() + @"</SummaryItem>");
			return result.ToString();
		}

		public override void Deserialize(XmlNode node)
		{
			base.Deserialize(node);
			Items.Clear();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "SummaryItem":
						var item = new CustomSummaryItem();
						item.Deserialize(childNode);
						Items.Add(item);
						break;
				}
			}
			ReorderItems();
		}

		public CustomSummaryItem AddItem()
		{
			var item = new CustomSummaryItem();
			item.Order = Items.Any() ? Items.Max(it => it.Order) + 1 : 0;
			Items.Add(item);
			return item;
		}

		public void DeleteItem(CustomSummaryItem item)
		{
			Items.Remove(item);
			ReorderItems();
		}

		public void ReorderItems()
		{
			var i = 0;
			foreach (var item in Items.OrderBy(it => it.Order))
			{
				item.Order = i;
				i++;
			}
		}
	}

	public interface ISummaryProduct
	{
		Guid UniqueID { get; }
		decimal SummaryOrder { get; }
		string SummaryTitle { get; }
		string SummaryInfo { get; }
		CustomSummaryItem SummaryItem { get; }
	}

	public class CustomSummaryItem
	{
		public CustomSummaryItem()
		{
			_id = Guid.NewGuid();
			ShowValue = true;
			ShowDescription = false;
			ShowMonthly = false;
			ShowTotal = false;
		}

		public decimal Order { get; set; }

		public bool ShowValue { get; set; }
		public bool ShowDescription { get; set; }
		public bool ShowMonthly { get; set; }
		public bool ShowTotal { get; set; }

		protected Guid _id;
		public virtual Guid Id
		{
			get { return _id; }
		}

		public string Value { get; set; }
		protected string _description;
		public virtual string Description
		{
			get { return _description; }
			set { _description = value; }
		}
		public decimal? Monthly { get; set; }
		public decimal? Total { get; set; }

		public bool Commited { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<Id>" + _id + @"</Id>");
			result.AppendLine(@"<Order>" + Order + @"</Order>");
			result.AppendLine(@"<ShowDescription>" + ShowDescription + @"</ShowDescription>");
			result.AppendLine(@"<ShowMonthly>" + ShowMonthly + @"</ShowMonthly>");
			result.AppendLine(@"<ShowTotal>" + ShowTotal + @"</ShowTotal>");
			result.AppendLine(@"<ShowValue>" + ShowValue + @"</ShowValue>");

			if (!String.IsNullOrEmpty(Value))
				result.AppendLine(@"<Value>" + Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Value>");
			if (!String.IsNullOrEmpty(_description))
				result.AppendLine(@"<Description>" + _description.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Description>");
			if (Monthly.HasValue)
				result.AppendLine(@"<Monthly>" + Monthly + @"</Monthly>");
			if (Total.HasValue)
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
					case "Id":
						Guid tempGuid;
						if (Guid.TryParse(childNode.InnerText, out tempGuid))
							_id = tempGuid;
						break;
					case "Order":
						if (Decimal.TryParse(childNode.InnerText, out tempDecimal))
							Order = tempDecimal;
						break;
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
						_description = childNode.InnerText;
						break;
				}
			}
			Commited = true;
		}
	}

	public class ProductSummaryItem : CustomSummaryItem
	{
		public ISummaryProduct Parent { get; private set; }

		public override Guid Id
		{
			get { return Parent.UniqueID; }
		}

		public override string Description
		{
			get { return !String.IsNullOrEmpty(_description) ? _description : Parent.SummaryInfo; }
			set { _description = value != Parent.SummaryInfo ? value : null; }
		}

		public ProductSummaryItem(ISummaryProduct parent)
		{
			Parent = parent;
			Order = Parent.SummaryOrder;
		}
	}
}
