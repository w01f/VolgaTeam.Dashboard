using System;
using System.Xml;
using Asa.Legacy.Media.Entities.Section;

namespace Asa.Legacy.Media.Entities.Summary
{
	public class SectionSummary
	{
		public ScheduleSection Parent { get; private set; }
		public SectionSummaryTypeEnum SummaryType { get; private set; }
		public ISectionSummaryContent Content { get; private set; }

		public SectionSummary(ScheduleSection parent)
		{
			Parent = parent;
			SummaryType = SectionSummaryTypeEnum.Custom;
			Content = CreateContentBySummaryType();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "SummaryType":
						{
							SectionSummaryTypeEnum temp;
							if (Enum.TryParse(childNode.InnerText, out temp))
								SummaryType = temp;
						}
						break;
				}

			Content = CreateContentBySummaryType();
			var contentNode = node.SelectSingleNode("Content");
			if (contentNode != null)
				Content.Deserialize(contentNode);
		}

		public void ChangeSummaryType(SectionSummaryTypeEnum newType)
		{
			if (newType == SummaryType) return;
			SummaryType = newType;
			Content = CreateContentBySummaryType();
		}

		private ISectionSummaryContent CreateContentBySummaryType()
		{
			switch (SummaryType)
			{
				case SectionSummaryTypeEnum.Product:
					return new ProductSummaryContent(this);
				case SectionSummaryTypeEnum.Custom:
					{
						var content = new CustomSummaryContent(this);
						return content;
					}
				case SectionSummaryTypeEnum.Strategy:
					{
						var content = new StrategySummaryContent(this);
						return content;
					}
			}
			throw new ArgumentOutOfRangeException("Summary Type is undefined");
		}
	}
}
