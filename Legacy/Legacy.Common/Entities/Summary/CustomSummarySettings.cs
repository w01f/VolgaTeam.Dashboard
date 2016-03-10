using System.Collections.Generic;
using System.Xml;

namespace Asa.Legacy.Common.Entities.Summary
{
	public class CustomSummarySettings : BaseSummarySettings
	{
		public List<CustomSummaryItem> Items { get; private set; }

		public CustomSummarySettings()
		{
			Items = new List<CustomSummaryItem>();
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
		}
	}
}
