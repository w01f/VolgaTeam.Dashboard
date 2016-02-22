using System.Collections.Generic;
using System.Linq;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Business.Media.Enums;
using Asa.Business.Media.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Section.Summary
{
	public class SectionSummary
	{
		public ScheduleSection Parent { get; private set; }
		public List<ISectionSummaryContent> Items { get; private set; }

		public CustomSummaryContent CustomSummary => (CustomSummaryContent)Items.First(i => i.SummaryType == SectionSummaryTypeEnum.Custom);
		public ProductSummaryContent ProductSummary => (ProductSummaryContent)Items.First(i => i.SummaryType == SectionSummaryTypeEnum.Product);
		public StrategySummaryContent StrategySummary => (StrategySummaryContent)Items.First(i => i.SummaryType == SectionSummaryTypeEnum.Strategy);

		[JsonConstructor]
		private SectionSummary()
		{
		}

		public SectionSummary(ScheduleSection parent)
		{
			Parent = parent;
			InitSummariesContent();
			SynchronizeSectionContent();
		}

		public void Dispose()
		{
			Items.ForEach(content => content.Dispose());
			Items.Clear();

			Parent = null;
		}

		public void SynchronizeSectionContent()
		{
			Items.ForEach(i => i.SynchronizeSectionContent());
		}

		private void InitSummariesContent()
		{
			if (Items == null || !Items.Any())
			{
				Items = new List<ISectionSummaryContent>();
				Items.AddRange(new ISectionSummaryContent[]
					{
					new CustomSummaryContent(this),
					new ProductSummaryContent(this),
					new StrategySummaryContent(this)
					});
				SynchronizeSectionContent();
			}
		}

		public void AfterCreate()
		{
			InitSummariesContent();
		}
	}
}
