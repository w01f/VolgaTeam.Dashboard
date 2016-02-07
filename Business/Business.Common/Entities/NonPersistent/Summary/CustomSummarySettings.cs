using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Asa.Business.Common.Entities.NonPersistent.Summary
{
	public class CustomSummarySettings : BaseSummarySettings
	{
		public List<CustomSummaryItem> Items { get; private set; }

		public decimal? TotalMonthly
		{
			get { return Items.Where(it => it.ShowMonthly && it.Monthly.HasValue).Sum(it => it.Monthly); }
		}

		public decimal? TotalTotal
		{
			get { return Items.Where(it => it.ShowTotal && it.Total.HasValue).Sum(it => it.Total); }
		}

		[JsonConstructor]
		protected CustomSummarySettings()
		{
			Items = new List<CustomSummaryItem>();
		}

		public static CustomSummarySettings Create()
		{
			var customSummary = new CustomSummarySettings();
			customSummary.AddItem();
			customSummary.AddItem();
			return customSummary;
		}

		[OnDeserialized]
		public void AfterDeserialize(StreamingContext context)
		{
			ReorderItems();
		}

		public override void Dispose()
		{
			base.Dispose();
			Items.Clear();
		}

		public CustomSummaryItem AddItem()
		{
			var item = new CustomSummaryItem();
			item.Order = Items.Any() ? Items.Max(it => it.Order) + 1 : 0;
			Items.Add(item);
			return item;
		}

		public void DeleteItem(CustomSummaryItem item)
		{
			Items.Remove(item);
			ReorderItems();
		}

		public void ReorderItems()
		{
			var i = 0;
			foreach (var item in Items.OrderBy(it => it.Order))
			{
				item.Order = i;
				i++;
			}
		}
	}
}
