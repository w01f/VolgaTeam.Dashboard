using System;
using System.Collections.Generic;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration.Contract.TabD;
using Newtonsoft.Json;

namespace Asa.Business.Solutions.Shift.Entities.NonPersistent
{
	public class ContractState
	{
		public TabAState TabA { get; }
		public TabBState TabB { get; }
		public TabCState TabC { get; }
		public TabDState TabD { get; }
		public SlidesTabState TabK { get; }
		public SlidesTabState TabL { get; }
		public SlidesTabState TabM { get; }
		public SlidesTabState TabN { get; }
		public SlidesTabState TabO { get; }
		public SlidesTabState TabU { get; }
		public SlidesTabState TabV { get; }
		public SlidesTabState TabW { get; }

		public ContractState()
		{
			TabA = new TabAState();
			TabB = new TabBState();
			TabC = new TabCState();
			TabD = new TabDState();
			TabK = new SlidesTabState();
			TabL = new SlidesTabState();
			TabM = new SlidesTabState();
			TabN = new SlidesTabState();
			TabO = new SlidesTabState();
			TabU = new SlidesTabState();
			TabV = new SlidesTabState();
			TabW = new SlidesTabState();
		}

		public class TabAState
		{
			public ListDataItem SlideHeader { get; set; }
			public bool? EnableOutput { get; set; }
			public ListDataItem Combo1 { get; set; }
			public ListDataItem Combo2 { get; set; }

			public List<ProductItemState> Products { get; }

			public TabAState()
			{
				Products = new List<ProductItemState>();
			}
		}

		public class TabBState
		{
			public ListDataItem SlideHeader { get; set; }
			public bool? EnableOutput { get; set; }

			public List<ListDataItem> Table1Column1Values { get; }
			public List<decimal?> Table1Column2Values { get; }
			public List<ListDataItem> Table1Column3Values { get; }

			public List<ListDataItem> Table2Column1Values { get; }
			public List<decimal?> Table2Column2Values { get; }
			public List<ListDataItem> Table2Column3Values { get; }
			
			public bool? SummaryCheckbox1 { get; set; }
			public bool? SummaryCheckbox3 { get; set; }
			public ListDataItem SummaryCombo1 { get; set; }

			public TabBState()
			{
				Table1Column1Values = new List<ListDataItem>();
				Table1Column2Values = new List<decimal?>();
				Table1Column3Values = new List<ListDataItem>();

				Table2Column1Values = new List<ListDataItem>();
				Table2Column2Values = new List<decimal?>();
				Table2Column3Values = new List<ListDataItem>();
			}
		}

		public class TabCState
		{
			public ListDataItem SlideHeader { get; set; }
			public bool? EnableOutput { get; set; }
			public ClipartObject Clipart1 { get; set; }
			public ClipartObject Clipart2 { get; set; }
			public ClipartObject Clipart3 { get; set; }
			public ListDataItem Combo1 { get; set; }
			public ListDataItem Combo2 { get; set; }
			public ListDataItem Combo3 { get; set; }
			public ListDataItem Combo4 { get; set; }
			public ListDataItem Combo5 { get; set; }
			public string Subheader1 { get; set; }
			public string Subheader2 { get; set; }
			public string Subheader3 { get; set; }
			public string Subheader4 { get; set; }
			public string Subheader5 { get; set; }
			public string Subheader6 { get; set; }
		}

		public class TabDState
		{
			public bool? EnableOutput { get; set; }

			public ClipartObject Clipart2 { get; set; }
			public ClipartObject Clipart3 { get; set; }

			public UserData User { get; set; }
		}

		public class ProductItemState
		{
			public Guid ItemId { get; private set; }
			public string ProductId { get; private set; }

			public ListDataItem MemoPopup1 { get; set; }
			public ListDataItem Combo1 { get; set; }
			public ListDataItem Combo2 { get; set; }
			public ListDataItem Combo3 { get; set; }

			[JsonConstructor]
			private ProductItemState() { }

			public ProductItemState(string productId) : this()
			{
				ItemId = Guid.NewGuid();
				ProductId = productId;
			}

			public ProductItemState Clone()
			{
				var clonedItem = new ProductItemState(ProductId);

				clonedItem.MemoPopup1 = ListDataItem.Clone(MemoPopup1);
				clonedItem.Combo1 = ListDataItem.Clone(Combo1);
				clonedItem.Combo2 = ListDataItem.Clone(Combo2);
				clonedItem.Combo3 = ListDataItem.Clone(Combo3);

				return clonedItem;
			}
		}
	}
}
