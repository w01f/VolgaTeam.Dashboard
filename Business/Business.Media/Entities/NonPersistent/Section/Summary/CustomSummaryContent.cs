﻿using System;
using System.Collections.Generic;
using System.Linq;
using Asa.Business.Common.Entities.NonPersistent.Summary;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Enums;
using Asa.Business.Media.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Section.Summary
{
	public class CustomSummaryContent : CustomSummarySettings, ISectionSummaryContent
	{
		public SectionSummary Parent { get; private set; }
		public SectionSummaryTypeEnum SummaryType => SectionSummaryTypeEnum.Custom;
		public bool IsDefaultSate { get; set; }

		[JsonConstructor]
		private CustomSummaryContent() { }

		public CustomSummaryContent(SectionSummary parent)
		{
			Parent = parent;
			AddItem();
			AddItem();
			IsDefaultSate = true;
		}

		public void SynchronizeSectionContent()
		{
			if (!IsDefaultSate) return;
			if (Items.Count != 2) return;

			{
				var summaryItem = Items[0];
				summaryItem.ShowValue = true;
				summaryItem.Value = String.Format("Local {0} Campaign", MediaMetaData.Instance.DataTypeString);
				var description = new List<string>();
				var programs = Parent.Parent.Programs.ToList();
				if (Parent.Parent.ShowStation)
					description.Add(String.Format("Stations: {0}", String.Join(", ", programs.Select(p => p.Station).Distinct())));
				if(Parent.Parent.ShowDaypart)
					description.Add(String.Format("Dayparts: {0}", String.Join(", ", programs.Select(p => p.Daypart).Distinct())));
				description.Add(String.Format("Total Spots: {0}x", Parent.Parent.TotalSpots));
				if (programs.Any(p => p.Rate.HasValue))
					description.Add(String.Format("Avg Rate: {0}", Parent.Parent.AvgRate.ToString("$#,##0")));
				summaryItem.Description = String.Join("  ", description);
				summaryItem.ShowDescription = true;
				summaryItem.ShowMonthly = false;
				summaryItem.Monthly = null;
				summaryItem.ShowTotal = false;
				summaryItem.Total = null;
			}
			{
				var summaryItem = Items[1];
				summaryItem.ShowValue = true;
				summaryItem.Value = "Digital Campaign";
				summaryItem.Description = String.Join(", ", Parent.Parent.ParentSchedule.DigitalProductsContent.DigitalProducts.Select(dp =>
					String.Format("({0}){1} - {2}",
					dp.Category,
					!String.IsNullOrEmpty(dp.SubCategory) ? (String.Format(" {0}", dp.SubCategory)) : String.Empty,
					dp.Name)));
				summaryItem.ShowDescription = true;
				summaryItem.ShowMonthly = false;
				summaryItem.Monthly = null;
				summaryItem.ShowTotal = false;
				summaryItem.Total = null;
			}
		}

		public override void Dispose()
		{
			base.Dispose();
			Parent = null;
		}
	}
}
