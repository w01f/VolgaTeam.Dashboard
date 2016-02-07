using System;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Business.Media.Enums;
using Asa.Business.Media.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Section.Summary
{
	public class SectionSummary
	{
		public ScheduleSection Parent { get; private set; }
		public SectionSummaryTypeEnum SummaryType { get; private set; }
		public ISectionSummaryContent Content { get; private set; }

		[JsonConstructor]
		private SectionSummary() { }

		public SectionSummary(ScheduleSection parent)
		{
			Parent = parent;
			SummaryType = SectionSummaryTypeEnum.Custom;
			Content = CreateContentBySummaryType();
			Content.SynchronizeSectionContent(); 
		}

		public void Dispose()
		{
			Content.Dispose();
			Content = null;

			Parent = null;
		}

		public void ChangeSummaryType(SectionSummaryTypeEnum newType)
		{
			if (newType == SummaryType) return;
			SummaryType = newType;
			Content = CreateContentBySummaryType();
			Content.SynchronizeSectionContent();
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
