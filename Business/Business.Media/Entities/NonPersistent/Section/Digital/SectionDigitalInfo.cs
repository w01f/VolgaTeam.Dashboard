using System.Collections.Generic;
using System.Linq;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Common.Core.Helpers;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Section.Digital
{
	public class SectionDigitalInfo
	{
		public ScheduleSection Parent { get; private set; }
		public List<SectionDigitalProduct> Products { get; private set; }

		public decimal? MonthlyInvestment { get; set; }
		public decimal? TotalInvestment { get; set; }

		#region Options
		public bool ShowCategory { get; set; }
		public bool ShowSubCategory { get; set; }
		public bool ShowProduct { get; set; }
		public bool ShowInfo { get; set; }
		public bool ShowLogo { get; set; }
		public bool ShowMonthlyInvestemt { get; set; }
		public bool ShowTotalInvestemt { get; set; }
		#endregion

		[JsonConstructor]
		private SectionDigitalInfo() { }

		public SectionDigitalInfo(ScheduleSection parent)
		{
			Parent = parent;
			InitProducts();

			#region Options
			ShowCategory = true;
			ShowSubCategory = true;
			ShowProduct = true;
			ShowInfo = true;
			ShowLogo = true;
			ShowMonthlyInvestemt = true;
			ShowTotalInvestemt = true;
			#endregion
		}

		public void Dispose()
		{
			Products.Clear();
			Parent = null;
		}

		private void InitProducts()
		{
			if (Products == null || !Products.Any())
				Products = new List<SectionDigitalProduct>();
		}

		public void AfterCreate()
		{
			InitProducts();
		}

		public void AddProduct()
		{
			var product = new SectionDigitalProduct(this);
			Products.Add(product);
		}

		public void DeleteProduct(SectionDigitalProduct digitalProduct)
		{
			Products.Remove(digitalProduct);
			RebuildProductIndexes();
		}

		public void CloneProduct(int productIndex)
		{
			if (productIndex < 0 || productIndex >= Products.Count) return;
			var digitalProduct = Products[productIndex];
			var newDigitalProduct = digitalProduct.Clone<SectionDigitalProduct, SectionDigitalProduct>();
			Products.Add(newDigitalProduct);
			newDigitalProduct.Index = Products.Count;
			RebuildProductIndexes();
		}

		public void ChangeProductPosition(int productIndex, int newIndex)
		{
			if (productIndex < 0 || productIndex >= Products.Count) return;
			var digitalProduct = Products[productIndex];
			digitalProduct.Index = newIndex + 0.5m;
			RebuildProductIndexes();
		}

		private void RebuildProductIndexes()
		{
			Products.Sort((x, y) => x.Index.CompareTo(y.Index));
			for (int i = 0; i < Products.Count; i++)
				Products[i].Index = i + 1;
		}
	}
}
