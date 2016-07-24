using System;
using System.Xml;

namespace Asa.Legacy.Common.Entities.Summary
{
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
		public virtual Guid Id => _id;

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
}
