using System.Linq;
using Asa.Business.Common.Entities.NonPersistent.Summary;
using Asa.Business.Media.Enums;
using Asa.Business.Media.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Section.Summary
{
	public class CustomSummaryContent : CustomSummarySettings, ISectionSummaryContent
	{
		public SectionSummary Parent { get; private set; }
		public SectionSummaryTypeEnum SummaryType => SectionSummaryTypeEnum.Custom;


		[JsonConstructor]
		private CustomSummaryContent() { }

		public CustomSummaryContent(SectionSummary parent)
		{
			Parent = parent;
			AddItem<MediaInfoSummaryItem>(this);
			AddItem<DigitalInfoSummaryItem>(this);
		}

		public void SynchronizeSectionContent()
		{
			Items.OfType<ProductInfoSummaryItem>().ToList().ForEach(item => item.Synchronize());
		}

		public override void Dispose()
		{
			base.Dispose();
			Parent = null;
		}
	}
}
