using System;
using Asa.Business.Common.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Common.Entities.NonPersistent.Summary
{
	public class ProductSummaryItem : CustomSummaryItem
	{
		public ISummaryProduct Parent { get; private set; }

		public override Guid Id
		{
			get { return Parent.UniqueID; }
		}

		[JsonIgnore]
		public override string Description
		{
			get { return !String.IsNullOrEmpty(_description) ? _description : Parent.SummaryInfo; }
			set { _description = value != Parent.SummaryInfo ? value : null; }
		}

		[JsonConstructor]
		private ProductSummaryItem() { }

		public ProductSummaryItem(ISummaryProduct parent)
		{
			Parent = parent;
			Order = Parent.SummaryOrder;
			ShowDescription = true;
		}
	}
}
