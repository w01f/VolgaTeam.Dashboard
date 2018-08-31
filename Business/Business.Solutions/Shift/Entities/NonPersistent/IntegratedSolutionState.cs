using System;
using System.Collections.Generic;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration.IntegratedSolution;
using Newtonsoft.Json;

namespace Asa.Business.Solutions.Shift.Entities.NonPersistent
{

	public class IntegratedSolutionState
	{
		public TabAState TabA { get; }

		public TabUState TabU { get; }
		public TabVState TabV { get; }
		public TabWState TabW { get; }

		public IntegratedSolutionState()
		{
			TabA = new TabAState();

			TabU = new TabUState();
			TabV = new TabVState();
			TabW = new TabWState();
		}

		public class TabAState
		{
			public bool? EnableOutput { get; set; }

			public List<ProductItemState> Products { get; }

			public TabAState()
			{
				Products = new List<ProductItemState>();
			}
		}

		public class TabUState
		{
			public SlideObject Slide { get; }

			public TabUState()
			{
				Slide = new SlideObject();
			}
		}

		public class TabVState
		{
			public SlideObject Slide { get; }

			public TabVState()
			{
				Slide = new SlideObject();
			}
		}

		public class TabWState
		{
			public SlideObject Slide { get; }

			public TabWState()
			{
				Slide = new SlideObject();
			}
		}

		public class ProductItemState
		{
			public string ProductId { get; private set; }

			public ListDataItem Header { get; set; }
			public ListDataItem Combo1 { get; set; }

			public PositioningToggleState PositionToggle { get; }
			public ResearchToggleState ResearchToggle { get; }
			public StyleToggleState StyleToggle { get; }

			[JsonConstructor]
			private ProductItemState()
			{
				PositionToggle = new PositioningToggleState();
				ResearchToggle = new ResearchToggleState();
				StyleToggle = new StyleToggleState();
			}

			public ProductItemState(string productId) : this()
			{
				ProductId = productId;
			}
		}

		public abstract class ProductToggleState
		{
			public bool Toggled { get; set; }
		}

		public class PositioningToggleState : ProductToggleState
		{
			public StatementsTabState Statements { get; }
			public BulletsTabState Bullets { get; }

			public PositioningToggleState()
			{
				Statements = new StatementsTabState();
				Bullets = new BulletsTabState();
			}
		}

		public class ResearchToggleState : ProductToggleState
		{
			public ResearchDataTabState Data { get; }

			public ResearchToggleState()
			{
				Data = new ResearchDataTabState();
			}
		}

		public class StyleToggleState : ProductToggleState
		{
			public ImageTabState ImageTab { get; }
			public ImageTabState LayoutTab { get; }

			public StyleToggleState()
			{
				ImageTab = new ImageTabState();
				LayoutTab = new ImageTabState();
			}
		}

		public class StatementsTabState
		{
			public List<StatementItemState> Items { get; }

			public StatementsTabState()
			{
				Items = new List<StatementItemState>();
			}
		}

		public class BulletsTabState
		{
			public bool Toggled { get; set; }

			public ListDataItem Combo1 { get; set; }

			public List<ListDataItem> Bullets { get; }

			public BulletsTabState()
			{
				Toggled = true;
				Bullets = new List<ListDataItem>();
			}
		}

		public class ResearchDataTabState
		{
			public bool Toggled { get; set; }

			public ResearchBundleState BundleState { get; set; }
		}

		public class ImageTabState
		{
			public ClipartObject Clipart { get; set; }
		}

		public class StatementItemState
		{
			public ListDataItem Combo { get; set; }
			public ListDataItem MemoPopup { get; set; }
		}

		public class ResearchBundleState
		{
			public string Item1 { get; set; }
			public string Item2 { get; set; }
			public string Item3 { get; set; }

			public ResearchInfo.BundleListItem ToLitsItem()
			{
				return new ResearchInfo.BundleListItem
				{
					Value1 = Item1,
					Value2 = Item2,
					Value3 = Item3,
				};
			}

			public bool IsEmpty()
			{
				return String.IsNullOrWhiteSpace(Item1) &&
					String.IsNullOrWhiteSpace(Item2) &&
					String.IsNullOrWhiteSpace(Item3);
			}
		}
	}
}
