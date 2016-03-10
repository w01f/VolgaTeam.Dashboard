using System;
using System.Xml;
using Asa.Legacy.Common.Entities.Summary;

namespace Asa.Legacy.Media.Entities.Summary
{
	public class CustomSummaryContent : CustomSummarySettings, ISectionSummaryContent
	{
		public SectionSummary Parent { get; private set; }
		public bool IsDefaultSate { get; set; }

		public CustomSummaryContent(SectionSummary parent)
		{
			Parent = parent;
			IsDefaultSate = true;
		}

		public override void Deserialize(XmlNode node)
		{
			base.Deserialize(node);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "IsDefaultSate":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								IsDefaultSate = temp;
						}
						break;
				}
			}
		}
	}
}
