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
		}
	}
}
