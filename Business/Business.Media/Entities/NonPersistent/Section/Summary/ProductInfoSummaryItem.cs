using System;
using System.Linq;
using Asa.Business.Common.Entities.NonPersistent.Summary;
using Asa.Business.Media.Enums;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Section.Summary
{
	public class ProductInfoSummaryItem : CustomSummaryItem
	{
		private CustomSummaryContent _summaryContent;

		public SummaryItemDataSourceType DataSourceType { get; set; }

		public bool IsFirstInCollection => Order == 0;
		public bool IsLastInCollection => (_summaryContent.Items.Count - 1) == Order;

		[JsonConstructor]
		private ProductInfoSummaryItem() { }

		public ProductInfoSummaryItem(CustomSummaryContent summaryContent)
		{
			_summaryContent = summaryContent;
		}

		public override void Dispose()
		{
			_summaryContent = null;
			base.Dispose();
		}

		public void Synchronize()
		{
			switch (DataSourceType)
			{
				case SummaryItemDataSourceType.Media:
					ShowDescription = true;
					Description = String.Format("Audience Reach Strategies: {0} {1}s ({2}) {3}",
						_summaryContent.Parent.Parent.TotalActivePeriods,
						_summaryContent.Parent.Parent.ParentScheduleSettings.SelectedSpotType,
						String.Join(", ", _summaryContent.Parent.Parent.Programs.Select(p =>
						{
							string summaryItem;
							if (p.Parent.ShowProgram && !String.IsNullOrEmpty(p.Name))
								summaryItem = p.Name;
							else if (p.Parent.ShowDaypart && !String.IsNullOrEmpty(p.Daypart))
								summaryItem = p.Daypart;
							else if (p.Parent.ShowStation && !String.IsNullOrEmpty(p.Station))
								summaryItem = p.Station;
							else if (p.Parent.ShowTime && !String.IsNullOrEmpty(p.Time))
								summaryItem = p.Time;
							else
								summaryItem = String.Empty;
							return summaryItem;
						}).Distinct()),
						String.Format("{0}x", _summaryContent.Parent.Parent.TotalSpots));
					break;
				case SummaryItemDataSourceType.Digital:
					ShowDescription = true;
					Description = String.Format("Digital Marketing Startegies: {0}",
						String.Join(" ", _summaryContent.Parent.Parent.DigitalInfo.Products
						.Select(p => String.Format("({0}{1})",
							p.Category,
							!String.IsNullOrEmpty(p.Info) ? String.Format(" - {0}", p.Info) : String.Empty))));
					break;
			}
		}

		public void ResetToDefault()
		{
			DataSourceType = SummaryItemDataSourceType.None;
			Description = null;
		}
	}
}
