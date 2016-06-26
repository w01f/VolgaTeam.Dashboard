using System;
using Newtonsoft.Json;

namespace Asa.Business.Online.Entities.NonPersistent
{
	public class ProductPackageRecord : BasePackageRecord
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

		[JsonIgnore]
		public override string Category
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
		public override string SubCategory
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
		public override string Name
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
		public override string Info
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
		public override string Comments
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
		public override decimal? Rate
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
		public override decimal? Investment
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
		public override decimal? Impressions
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
		public override decimal? CPM
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

		[JsonConstructor]
		private ProductPackageRecord() { }

		public ProductPackageRecord(DigitalProduct parent) : base(parent.Parent.ScheduleSettings)
		{
			Parent = parent;
			ResetToDefault();
		}

		public override void Dispose()
		{
			base.Dispose();
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
