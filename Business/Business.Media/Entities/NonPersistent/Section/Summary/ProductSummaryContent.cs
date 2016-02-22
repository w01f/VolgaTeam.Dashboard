using System.Collections.Generic;
using Asa.Business.Common.Entities.NonPersistent.Summary;
using Asa.Business.Common.Interfaces;
using Asa.Business.Media.Enums;
using Asa.Business.Media.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Section.Summary
{
	public class ProductSummaryContent : BaseSummarySettings, ISectionSummaryContent
	{
		public SectionSummary Parent { get; private set; }
		public SectionSummaryTypeEnum SummaryType => SectionSummaryTypeEnum.Product;

		public List<ISummaryProduct> Items
		{
			get
			{
				var result = new List<ISummaryProduct>();
				result.AddRange(Parent.Parent.Programs);
				result.AddRange(Parent.Parent.ParentSchedule.DigitalProductsContent.DigitalProducts);
				return result;
			}
		}

		[JsonConstructor]
		private ProductSummaryContent() { }

		public ProductSummaryContent(SectionSummary parent)
		{
			Parent = parent;
		}

		public void SynchronizeSectionContent() { }

		public override void Dispose()
		{
			base.Dispose();
			Items.Clear();
			Parent = null;
		}
	}
}
