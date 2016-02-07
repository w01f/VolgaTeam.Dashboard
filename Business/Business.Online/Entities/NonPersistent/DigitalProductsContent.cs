using System;
using System.Collections.Generic;
using System.Linq;
using Asa.Business.Online.Common;
using Asa.Business.Online.Interfaces;

namespace Asa.Business.Online.Entities.NonPersistent
{
	public abstract class DigitalProductsContent : DigitalScheduleContent, IDigitalProductsContent
	{
		public List<DigitalProduct> DigitalProducts { get; private set; }
		public DigitalProductSummary DigitalProductSummary { get; private set; }

		protected DigitalProductsContent()
		{
			DigitalProducts = new List<DigitalProduct>();
			DigitalProductSummary = new DigitalProductSummary();
		}

		public override void Dispose()
		{
			DigitalProducts.ForEach(o => o.Dispose());
			DigitalProducts.Clear();

			DigitalProductSummary = null;

			base.Dispose();
		}

		public void AddDigital(string categoryName)
		{
			DigitalProducts.Add(new DigitalProduct(this) { Index = DigitalProducts.Count + 1, Category = categoryName }); ;
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

		public string GetDigitalInfo(RequestDigitalInfoEventArgs args)
		{
			var result = new List<string>();
			if (args.ShowWebsites)
			{
				var compiledWebsites = String.Join(", ", DigitalProducts.SelectMany(p => p.Websites).Distinct());
				if (!String.IsNullOrEmpty(compiledWebsites))
					result.Add(String.Format("{0}", compiledWebsites));
			}
			foreach (var product in DigitalProducts)
			{
				var temp = new List<string>();
				if (args.ShowProduct && !String.IsNullOrEmpty(product.UserDefinedName))
					temp.Add(product.UserDefinedName);
				if (args.ShowDimensions && !String.IsNullOrEmpty(product.Dimensions))
					temp.Add(product.Dimensions);
				if (args.ShowDates && product.DurationValue.HasValue)
					temp.Add(ScheduleSettings.FlightDates);
				if (product.MonthlyImpressionsCalculated.HasValue || product.MonthlyCPMCalculated.HasValue || product.MonthlyInvestmentCalculated.HasValue)
				{
					var monthly = new List<string>();
					if (args.ShowImpressions && product.MonthlyImpressionsCalculated.HasValue)
						monthly.Add(String.Format("Imp: {0}", product.MonthlyImpressionsCalculated.Value.ToString("#,##0")));
					if (args.ShowInvestment && product.MonthlyInvestmentCalculated.HasValue)
						monthly.Add(String.Format("Inv: {0}", product.MonthlyInvestmentCalculated.Value.ToString("#,##0")));
					if (args.ShowCPM && product.MonthlyCPMCalculated.HasValue)
						monthly.Add(String.Format("CPM: {0}", product.MonthlyCPMCalculated.Value.ToString("#,##0")));
					if (monthly.Any())
						temp.Add(String.Format("(Monthly) {0}", String.Join(" ", monthly)));
				}
				if (product.TotalImpressionsCalculated.HasValue || product.TotalCPMCalculated.HasValue || product.TotalInvestmentCalculated.HasValue)
				{
					var total = new List<string>();
					if (args.ShowImpressions && product.TotalImpressionsCalculated.HasValue)
						total.Add(String.Format("Imp: {0}", product.TotalImpressionsCalculated.Value.ToString("#,##0")));
					if (args.ShowInvestment && product.TotalInvestmentCalculated.HasValue)
						total.Add(String.Format("Inv: {0}", product.TotalInvestmentCalculated.Value.ToString("#,##0")));
					if (args.ShowCPM && product.TotalCPMCalculated.HasValue)
						total.Add(String.Format("CPM: {0}", product.TotalCPMCalculated.Value.ToString("#,##0")));
					if (total.Any())
						temp.Add(String.Format("(Total) {0}", String.Join(" ", total)));
				}
				if (temp.Any())
					result.Add(String.Format("[{0}]", String.Join(", ", temp.ToArray())));
			}
			return String.Join(args.Separator, result);
		}
	}
}
