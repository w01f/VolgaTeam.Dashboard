using System;
using Asa.Business.Online.Enums;
using Newtonsoft.Json;

namespace Asa.Business.Online.Entities.NonPersistent
{
	public class ProductPackageRecord
	{
		private string _category;
		private string _subCategory;
		private string _name;
		private string _info;
		private string _comments;
		private decimal? _rate;
		private decimal? _investment;
		private decimal? _impressions;
		private decimal? _cpm;

		public DigitalProduct Parent { get; private set; }
		public bool UseFormula { get; set; }

		[JsonIgnore]
		public string Category
		{
			get
			{
				if (_category == null)
					return Parent.Category;
				return _category;
			}
			set
			{
				if (String.IsNullOrEmpty(Parent.Category) || !Parent.Category.Equals(value))
					_category = value;
			}
		}

		[JsonIgnore]
		public string SubCategory
		{
			get
			{
				if (_subCategory == null)
					return Parent.SubCategory;
				return _subCategory;
			}
			set
			{
				if (String.IsNullOrEmpty(Parent.SubCategory) || !Parent.SubCategory.Equals(value))
					_subCategory = value;
			}
		}

		[JsonIgnore]
		public string Name
		{
			get
			{
				if (_name == null)
					return Parent.Name;
				return _name;
			}
			set
			{
				if (String.IsNullOrEmpty(Parent.Name) || !Parent.Name.Equals(value))
					_name = value;
			}
		}

		[JsonIgnore]
		public string Info
		{
			get
			{
				return _info;
			}
			set
			{
				_info = value;
			}
		}

		[JsonIgnore]
		public string Comments
		{
			get
			{
				return _comments;
			}
			set
			{
				_comments = value;
			}
		}

		[JsonIgnore]
		public decimal? Rate
		{
			get
			{
				return _rate;
			}
			set
			{
				_rate = value;
			}
		}

		[JsonIgnore]
		public decimal? Investment
		{
			get
			{
				return _investment;
			}
			set
			{
				_investment = value;
			}
		}

		[JsonIgnore]
		public decimal? InvestmentCalculated
		{
			get
			{
				if (UseFormula &&
					(Parent.Settings.DigitalPackageSettings.ShowInvestment &&
						Parent.Settings.DigitalPackageSettings.ShowImpressions &&
						Parent.Settings.DigitalPackageSettings.ShowCPM) &&
					Parent.Settings.DigitalPackageSettings.Formula == FormulaType.Investment)
					Investment = CPMCalculated.HasValue && ImpressionsCalculated.HasValue ? Math.Round(CPMCalculated.Value * (ImpressionsCalculated.Value / 1000), 2) : (decimal?)null;
				return Investment;
			}
			set
			{
				Investment = value;
			}
		}

		[JsonIgnore]
		public decimal? Impressions
		{
			get
			{
				return _impressions;
			}
			set
			{
				_impressions = value;
			}
		}

		[JsonIgnore]
		public decimal? ImpressionsCalculated
		{
			get
			{
				if (UseFormula &&
					(Parent.Settings.DigitalPackageSettings.ShowInvestment &&
						Parent.Settings.DigitalPackageSettings.ShowImpressions &&
						Parent.Settings.DigitalPackageSettings.ShowCPM) &&
					Parent.Settings.DigitalPackageSettings.Formula == FormulaType.Impressions)
					Impressions = InvestmentCalculated.HasValue && CPMCalculated.HasValue && CPMCalculated.Value != 0 ? Math.Round(((InvestmentCalculated.Value * 1000) / CPMCalculated.Value), 0) : (decimal?)null;
				return Impressions;
			}
			set
			{
				Impressions = value;
			}
		}

		[JsonIgnore]
		public decimal? CPM
		{
			get
			{
				return _cpm;
			}
			set
			{
				_cpm = value;
			}
		}

		[JsonIgnore]
		public decimal? CPMCalculated
		{
			get
			{
				if (UseFormula &&
					(Parent.Settings.DigitalPackageSettings.ShowInvestment &&
						Parent.Settings.DigitalPackageSettings.ShowImpressions &&
						Parent.Settings.DigitalPackageSettings.ShowCPM) &&
					Parent.Settings.DigitalPackageSettings.Formula == FormulaType.CPM)
					CPM = InvestmentCalculated.HasValue && ImpressionsCalculated.HasValue && ImpressionsCalculated.Value != 0 ? Math.Round((InvestmentCalculated.Value / (ImpressionsCalculated.Value / 1000)), 2) : (decimal?)null;
				return CPM;
			}
			set
			{
				CPM = value;
			}
		}

		[JsonConstructor]
		private ProductPackageRecord() { }

		public ProductPackageRecord(DigitalProduct parent)
		{
			Parent = parent;
			ResetToDefault();
		}

		public void Dispose()
		{
			Parent = null;
		}

		public void ResetToDefault()
		{
			_category = null;
			_subCategory = null;
			_name = null;
			_info = null;
			_comments = null;
			_rate = null;
			_investment = null;
			_impressions = null;
			_cpm = null;
			UseFormula = true;
		}
	}
}
