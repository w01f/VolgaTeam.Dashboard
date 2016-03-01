using System;
using Newtonsoft.Json;

namespace Asa.Business.Common.Entities.NonPersistent.Summary
{
	public class CustomSummaryItem
	{
		public decimal Order { get; set; }

		public bool ShowValue { get; set; }
		public bool ShowDescription { get; set; }
		public bool ShowMonthly { get; set; }
		public bool ShowTotal { get; set; }

		protected Guid _id;
		public virtual Guid Id
		{
			get { return _id; }
		}

		public string Value { get; set; }

		protected string _description;
		[JsonIgnore]
		public virtual string Description
		{
			get { return _description; }
			set { _description = value; }
		}
		public decimal? Monthly { get; set; }
		public decimal? Total { get; set; }

		[JsonIgnore]
		public bool Commited { get; set; }

		public CustomSummaryItem()
		{
			_id = Guid.NewGuid();
			ShowValue = true;
			ShowDescription = false;
			ShowMonthly = false;
			ShowTotal = false;
		}

		public virtual void Dispose()
		{
		}
	}
}
