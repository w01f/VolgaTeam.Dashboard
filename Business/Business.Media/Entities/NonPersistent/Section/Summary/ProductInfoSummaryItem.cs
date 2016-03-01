using Asa.Business.Common.Entities.NonPersistent.Summary;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Section.Summary
{
	public abstract class ProductInfoSummaryItem : CustomSummaryItem
	{
		protected CustomSummaryContent _summaryContent;

		public bool IsDefaultSate { get; set; }

		public abstract string Title { get; }

		[JsonConstructor]
		protected ProductInfoSummaryItem() { }

		protected ProductInfoSummaryItem(CustomSummaryContent summaryContent)
		{
			_summaryContent = summaryContent;
			IsDefaultSate = true;
		}

		public override void Dispose()
		{
			_summaryContent = null;
			base.Dispose();
		}

		public virtual void Synchronize()
		{
			if(!IsDefaultSate) return;
			LoadProductInfo();
		}

		public void ResetToDefault()
		{
			IsDefaultSate = true;
			Synchronize();
		}

		protected abstract void LoadProductInfo();
	}
}
