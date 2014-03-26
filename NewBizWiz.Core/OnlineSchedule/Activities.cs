using System;
using System.Xml.Linq;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Core.OnlineSchedule
{
	public class DigitalProductEditActivity : PropertyEditActivity
	{
		public string CategoryName { get; private set; }
		public string GroupName { get; private set; }

		public DigitalProductEditActivity(string categoryName, string groupName, string productName)
			: base("Product Name", productName, "Added", String.Empty)
		{
			CategoryName = categoryName;
			GroupName = groupName;
		}

		public override XElement Serialize()
		{
			var element = base.Serialize();
			element.Add(new XAttribute("Category", CategoryName));
			if (!String.IsNullOrEmpty(GroupName))
				element.Add(new XAttribute("Group", GroupName));
			return element;
		}
	}

	public class DigitalProductOutputActivity : OutputActivity
	{
		public string ProductName { get; private set; }

		public DigitalProductOutputActivity(string slideName, string advertiser, string productName, decimal? dollarValue)
			: base(slideName, advertiser, dollarValue)
		{
			ProductName = productName;
		}

		public override XElement Serialize()
		{
			var element = base.Serialize();
			element.Add(new XAttribute("Product", ProductName));
			return element;
		}
	}
}
