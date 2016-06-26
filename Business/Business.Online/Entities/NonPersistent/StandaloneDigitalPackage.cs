using System;
using System.Collections.Generic;
using System.Linq;
using Asa.Business.Online.Configuration;
using Asa.Business.Online.Interfaces;
using Asa.Common.Core.Helpers;

namespace Asa.Business.Online.Entities.NonPersistent
{
	public class StandaloneDigitalPackage : IDigitalPackageSettingsContainer
	{
		public List<StandalonePackageRecord> Items { get; private set; }

		public DigitalPackageSettings DigitalPackageSettings { get; private set; }

		public StandaloneDigitalPackage()
		{
			Items = new List<StandalonePackageRecord>();
			DigitalPackageSettings = new DigitalPackageSettings();
		}

		public void Dispose()
		{
			Items.ForEach(p => p.Dispose());
			Items.Clear();
			DigitalPackageSettings = null;
		}

		public void AddItem(Category category)
		{
			var packageRecord = new StandalonePackageRecord(this);
			packageRecord.Category = category.Name;
			var subCategories = Dictionaries.ListManager.Instance.ProductSources
				.Where(productSource =>
					productSource.Category != null &&
					productSource.Category.Name.Equals(category.Name) &&
					!String.IsNullOrEmpty(productSource.SubCategory))
				.Select(x => x.SubCategory)
				.Distinct()
				.ToList();
			if (subCategories.Count <= 1)
				packageRecord.SubCategory = subCategories.FirstOrDefault();
			Items.Add(packageRecord);
		}

		public void DeleteItem(int itemIndex)
		{
			if (itemIndex < 0 || itemIndex >= Items.Count) return;
			var packageRecord = Items[itemIndex];
			Items.Remove(packageRecord);
			RebuildProgramIndexes();
		}

		public void CloneItem(int itemIndex)
		{
			if (itemIndex < 0 || itemIndex >= Items.Count) return;
			var packageRecord = Items[itemIndex];
			var newPackageRecord = packageRecord.Clone<StandalonePackageRecord, StandalonePackageRecord>();
			Items.Add(newPackageRecord);
			newPackageRecord.Index = Items.Count;
			RebuildProgramIndexes();
		}

		public void ChangeItemPosition(int itemIndex, int newIndex)
		{
			if (itemIndex < 0 || itemIndex >= Items.Count) return;
			var packageRecord = Items[itemIndex];
			packageRecord.Index = newIndex + 0.5m;
			RebuildProgramIndexes();
		}

		public void RebuildProgramIndexes()
		{
			Items.Sort((x, y) => x.Index.CompareTo(y.Index));
			for (var i = 0; i < Items.Count; i++)
				Items[i].Index = i + 1;
		}
	}
}
