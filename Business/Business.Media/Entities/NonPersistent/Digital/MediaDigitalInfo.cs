using System.Collections.Generic;
using System.Linq;
using Asa.Common.Core.Helpers;

namespace Asa.Business.Media.Entities.NonPersistent.Digital
{
	public class MediaDigitalInfo
	{
		public List<MediaDigitalInfoRecord> Records { get; private set; }

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

		public MediaDigitalInfo()
		{
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
			Records.Clear();
		}

		private void InitProducts()
		{
			if (Records == null || !Records.Any())
				Records = new List<MediaDigitalInfoRecord>();
		}

		public void AfterCreate()
		{
			InitProducts();
		}

		public void AddProduct()
		{
			var product = new MediaDigitalInfoRecord(this);
			Records.Add(product);
		}

		public void DeleteProduct(MediaDigitalInfoRecord digitalInfoRecord)
		{
			Records.Remove(digitalInfoRecord);
			RebuildProductIndexes();
		}

		public void CloneProduct(int productIndex)
		{
			if (productIndex < 0 || productIndex >= Records.Count) return;
			var digitalProduct = Records[productIndex];
			var newDigitalProduct = digitalProduct.Clone<MediaDigitalInfoRecord, MediaDigitalInfoRecord>();
			Records.Add(newDigitalProduct);
			newDigitalProduct.Index = Records.Count;
			RebuildProductIndexes();
		}

		public void ChangeProductPosition(int productIndex, int newIndex)
		{
			if (productIndex < 0 || productIndex >= Records.Count) return;
			var digitalProduct = Records[productIndex];
			digitalProduct.Index = newIndex + 0.5m;
			RebuildProductIndexes();
		}

		private void RebuildProductIndexes()
		{
			Records.Sort((x, y) => x.Index.CompareTo(y.Index));
			for (int i = 0; i < Records.Count; i++)
				Records[i].Index = i + 1;
		}
	}
}
