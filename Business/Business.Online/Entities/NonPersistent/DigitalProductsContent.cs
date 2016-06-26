using System;
using System.Collections.Generic;
using System.Linq;
using Asa.Business.Online.Interfaces;

namespace Asa.Business.Online.Entities.NonPersistent
{
	public abstract class DigitalProductsContent : DigitalScheduleContent, IDigitalProductsContent
	{
		public List<DigitalProduct> DigitalProducts { get; private set; }
		public StandaloneDigitalPackage StandalonePackage { get; private set; }
		public DigitalProductSummary DigitalProductSummary { get; private set; }

		protected DigitalProductsContent()
		{
			DigitalProducts = new List<DigitalProduct>();
			StandalonePackage = new StandaloneDigitalPackage();
			DigitalProductSummary = new DigitalProductSummary();
		}

		protected override void AfterCreate()
		{
			base.AfterCreate();
			if (StandalonePackage == null)
				StandalonePackage = new StandaloneDigitalPackage();
			foreach (var digitalProduct in DigitalProducts)
				digitalProduct.AfterCreate();
		}

		public override void Dispose()
		{
			DigitalProducts.ForEach(o => o.Dispose());
			DigitalProducts.Clear();

			StandalonePackage.Dispose();
			StandalonePackage = null;

			DigitalProductSummary = null;

			base.Dispose();
		}

		public void AddDigital(string categoryName)
		{
			var digitalProduct = new DigitalProduct(this)
			{
				Index = DigitalProducts.Count + 1,
				Category = categoryName
			};
			var subCategories = Dictionaries.ListManager.Instance.ProductSources
				.Where(productSource =>
					productSource.Category != null &&
					productSource.Category.Name.Equals(categoryName) &&
					!String.IsNullOrEmpty(productSource.SubCategory))
				.Select(x => x.SubCategory)
				.Distinct()
				.ToList();
			if (subCategories.Count <= 1)
				digitalProduct.SubCategory = subCategories.FirstOrDefault();
			DigitalProducts.Add(digitalProduct);
		}

		public void UpDigital(int position)
		{
			if (position <= 0) return;
			DigitalProducts[position].Index--;
			DigitalProducts[position - 1].Index++;
			DigitalProducts.Sort((x, y) => x.Index.CompareTo(y.Index));
		}

		public void DownDigital(int position)
		{
			if (position >= DigitalProducts.Count - 1) return;
			DigitalProducts[position].Index++;
			DigitalProducts[position + 1].Index--;
			DigitalProducts.Sort((x, y) => x.Index.CompareTo(y.Index));
		}

		public void ChangeDigitalProductPosition(int position, int newPosition)
		{
			if (position < 0 || position >= DigitalProducts.Count) return;
			var product = DigitalProducts[position];
			product.Index = newPosition + 0.5;
			DigitalProducts.Sort((x, y) => x.Index.CompareTo(y.Index));
			RebuildDigitalProductIndexes();
		}

		public void RebuildDigitalProductIndexes()
		{
			for (int i = 0; i < DigitalProducts.Count; i++)
				DigitalProducts[i].Index = i + 1;
		}
	}
}
