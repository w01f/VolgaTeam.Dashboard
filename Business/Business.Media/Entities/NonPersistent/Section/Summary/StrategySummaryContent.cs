using System;
using System.Collections.Generic;
using System.Linq;
using Asa.Business.Media.Enums;
using Asa.Business.Media.Interfaces;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Images;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Section.Summary
{
	public class StrategySummaryContent : ISectionSummaryContent
	{
		public SectionSummary Parent { get; private set; }
		public SectionSummaryTypeEnum SummaryType => SectionSummaryTypeEnum.Strategy;
		public bool ShowStation { get; set; }
		public bool ShowDescription { get; set; }

		public List<ProgramStrategyItem> Items { get; private set; }

		public IEnumerable<ProgramStrategyItem> EnabledItems
		{
			get { return Items.Where(i => i.Enabled).OrderBy(i => i.Order); }
		}

		[JsonConstructor]
		private StrategySummaryContent() { }

		public StrategySummaryContent(SectionSummary parent)
		{
			Parent = parent;
			Items = new List<ProgramStrategyItem>();

			ShowStation = true;
			ShowDescription = true;
		}

		public void SynchronizeSectionContent()
		{
			var sourceCollection = Parent.Parent.Programs.ToList();
			var maxOrder = Items.Any() ? Items.Max(i => i.Order) : 0;
			var groupedPrograms = sourceCollection
				.GroupBy(
					p => p.Name,
					(key, g) => new
					{
						Name = key,
						Logo = g.Select(i => i.Logo).FirstOrDefault()?.Clone<ImageSource, ImageSource>(),
						Station = String.Join(", ", g.Select(i => i.Station)),
						Spots = g.SelectMany(i => i.Spots).Sum(s => s.Count)
					})
				.ToList();
			foreach (var program in groupedPrograms)
			{
				var strategyItem = Items.FirstOrDefault(si => si.Name == program.Name);
				if (strategyItem == null)
				{
					strategyItem = new ProgramStrategyItem(this)
					{
						Enabled = true,
						Name = program.Name,
						Order = maxOrder
					};
					Items.Add(strategyItem);
					maxOrder++;
				}
				strategyItem.DefaultLogo = Parent.Parent.ShowLogo ? program.Logo : null;
				strategyItem.Station = program.Station;
				strategyItem.Description = String.Format("{0}x", program.Spots);
			}
			Items.RemoveAll(i => groupedPrograms.All(gp => gp.Name != i.Name));
			ReorderItems();
		}

		public void Dispose()
		{
			Items.ForEach(i => i.Dispose());
			Items.Clear();
			Parent = null;
		}

		private void ReorderItems()
		{
			var index = 0;
			foreach (var item in Items.OrderBy(i => i.Order).ToList())
			{
				item.Order = index;
				index++;
			}
			Items.Sort((x, y) => x.Order.CompareTo(y.Order));
		}

		public void ChangeItemsOrder(int sourceRowOrder, int targetRowOrder)
		{
			if (!(sourceRowOrder >= 0 && sourceRowOrder < Items.Count)) return;
			var item = Items[sourceRowOrder];
			item.Order = targetRowOrder - (Decimal)0.5;
			ReorderItems();
		}
	}
}
