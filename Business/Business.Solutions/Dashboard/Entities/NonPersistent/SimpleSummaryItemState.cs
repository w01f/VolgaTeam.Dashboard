namespace Asa.Business.Solutions.Dashboard.Entities.NonPersistent
{
	public class SimpleSummaryItemState
	{
		public bool ShowValue { get; set; }
		public bool ShowDescription { get; set; }
		public bool ShowMonthly { get; set; }
		public bool ShowTotal { get; set; }

		public int Order { get; set; }
		public string Value { get; set; }
		public string Description { get; set; }
		public decimal? Monthly { get; set; }
		public decimal? Total { get; set; }

		public SimpleSummaryItemState()
		{
			ShowValue = true;
		}
	}
}
