using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Asa.Legacy.Media.Entities.Summary
{
	public class StrategySummaryContent : ISectionSummaryContent
	{
		public SectionSummary Parent { get; private set; }
		public bool ShowStation { get; set; }
		public bool ShowDescription { get; set; }

		public List<ProgramStrategyItem> Items { get; private set; }

		public IEnumerable<ProgramStrategyItem> EnabledItems
		{
			get { return Items.Where(i => i.Enabled).OrderBy(i => i.Order); }
		}

		public StrategySummaryContent(SectionSummary parent)
		{
			Parent = parent;
			Items = new List<ProgramStrategyItem>();

			ShowStation = true;
			ShowDescription = true;
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "ShowStation":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowStation = temp;
							break;
						}
					case "ShowDescription":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowDescription = temp;
							break;
						}
					case "Item":
						{
							var item = new ProgramStrategyItem(this);
							item.Deserialize(childNode);
							Items.Add(item);
							break;
						}
				}
			}
		}
	}
}
