using System;
using System.Collections.Generic;
using System.Linq;
using Asa.Business.Media.Configuration;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Section.Summary
{
	public class MediaInfoSummaryItem : ProductInfoSummaryItem
	{
		public override string Title => "Media Summary";

		[JsonConstructor]
		private MediaInfoSummaryItem() { }

		public MediaInfoSummaryItem(CustomSummaryContent summaryContent) : base(summaryContent) { }

		protected override void LoadProductInfo()
		{
			ShowValue = true;
			Value = String.Format("Local {0} Campaign", MediaMetaData.Instance.DataTypeString);
			var description = new List<string>();
			var programs = _summaryContent.Parent.Parent.Programs.ToList();
			if (_summaryContent.Parent.Parent.ShowStation)
				description.Add(String.Format("Stations: {0}", String.Join(", ", programs.Select(p => p.Station).Distinct())));
			if (_summaryContent.Parent.Parent.ShowDaypart)
				description.Add(String.Format("Dayparts: {0}", String.Join(", ", programs.Select(p => p.Daypart).Distinct())));
			description.Add(String.Format("Total Spots: {0}x", _summaryContent.Parent.Parent.TotalSpots));
			if (programs.Any(p => p.Rate.HasValue))
				description.Add(String.Format("Avg Rate: {0}", _summaryContent.Parent.Parent.AvgRate.ToString("$#,##0")));
			Description = String.Join("  ", description);
			ShowDescription = true;
		}
	}
}
