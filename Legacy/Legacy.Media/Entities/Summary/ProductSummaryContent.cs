using Asa.Legacy.Common.Entities.Summary;

namespace Asa.Legacy.Media.Entities.Summary
{
	public class ProductSummaryContent : BaseSummarySettings, ISectionSummaryContent
	{
		public SectionSummary Parent { get; private set; }

		public ProductSummaryContent(SectionSummary parent)
		{
			Parent = parent;
		}
	}
}
