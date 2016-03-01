using System;
using System.Linq;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Section.Summary
{
	public class DigitalInfoSummaryItem : ProductInfoSummaryItem
	{
		public override string Title => "Digital Summary";

		[JsonConstructor]
		private DigitalInfoSummaryItem() { }

		public DigitalInfoSummaryItem(CustomSummaryContent summaryContent) :base(summaryContent){ }

		protected override void LoadProductInfo()
		{
			ShowValue = true;
			Value = "Digital Campaign";
			Description = String.Join(", ", _summaryContent.Parent.Parent.ParentSchedule.DigitalProductsContent.DigitalProducts.Select(dp =>
				String.Format("({0}){1} - {2}",
				dp.Category,
				!String.IsNullOrEmpty(dp.SubCategory) ? (String.Format(" {0}", dp.SubCategory)) : String.Empty,
				dp.Name)));
			ShowDescription = true;
		}
	}
}
