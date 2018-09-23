using System;
using System.Collections.Generic;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration.IntegratedSolution;
using Newtonsoft.Json;

namespace Asa.Business.Solutions.Shift.Entities.NonPersistent
{

	public class IntegratedSolutionState
	{
		public SubTabState TabA { get; }
		public SubTabState TabB { get; }
		public SubTabState TabC { get; }
		public SubTabState TabD { get; }
		public SubTabState TabE { get; }
		public SlidesTabState TabK { get; }
		public SlidesTabState TabL { get; }
		public SlidesTabState TabM { get; }
		public SlidesTabState TabN { get; }
		public SlidesTabState TabO { get; }
		public SlidesTabState TabU { get; }
		public SlidesTabState TabV { get; }
		public SlidesTabState TabW { get; }
		
		public IntegratedSolutionState()
		{
			TabA = new SubTabState();
			TabB = new SubTabState();
			TabC = new SubTabState();
			TabD = new SubTabState();
			TabE = new SubTabState();
			TabK = new SlidesTabState();
			TabL = new SlidesTabState();
			TabM = new SlidesTabState();
			TabN = new SlidesTabState();
			TabO = new SlidesTabState();
			TabU = new SlidesTabState();
			TabV = new SlidesTabState();
			TabW = new SlidesTabState();
		}

		public class SubTabState
		{
			public bool? EnableOutput { get; set; }

			public List<ProductItemState> Products { get; }

			public SubTabState()
			{
				Products = new List<ProductItemState>();
			}
		}

		public class ProductItemState
		{
			public Guid ItemId { get; private set; }
			public string ProductId { get; private set; }

			public bool? EnableOutput { get; set; }

			public ListDataItem Header { get; set; }
			public ListDataItem Combo1 { get; set; }

			public PositioningToggleState PositionToggle { get; private set; }
			public ResearchToggleState ResearchToggle { get; private set; }
			public StyleToggleState StyleToggle { get; private set; }

			[JsonConstructor]
			private ProductItemState()
			{
				PositionToggle = new PositioningToggleState();
				ResearchToggle = new ResearchToggleState();
				StyleToggle = new StyleToggleState();
			}

			public ProductItemState(string productId) : this()
			{
				ItemId = Guid.NewGuid();
				ProductId = productId;
			}

			public ProductItemState Clone()
			{
				var clonedItem = new ProductItemState(ProductId);

				clonedItem.Header = ListDataItem.Clone(Header);
				clonedItem.Combo1 = ListDataItem.Clone(Combo1);

				clonedItem.PositionToggle = PositionToggle.Clone();
				clonedItem.ResearchToggle = ResearchToggle.Clone();
				clonedItem.StyleToggle = StyleToggle.Clone();

				return clonedItem;
			}
		}

		public abstract class ProductToggleState
		{
			public bool Toggled { get; set; }
		}

		public class PositioningToggleState : ProductToggleState
		{
			public StatementsTabState Statements { get; private set; }
			public BulletsTabState Bullets { get; private set; }

			public PositioningToggleState()
			{
				Statements = new StatementsTabState();
				Bullets = new BulletsTabState();
			}

			public PositioningToggleState Clone()
			{
				var cloned = new PositioningToggleState();

				cloned.Statements = Statements.Clone();
				cloned.Bullets = Bullets.Clone();

				return cloned;
			}
		}

		public class ResearchToggleState : ProductToggleState
		{
			public ResearchDataTabState Data { get; private set; }

			public ResearchToggleState()
			{
				Data = new ResearchDataTabState();
			}

			public ResearchToggleState Clone()
			{
				var cloned = new ResearchToggleState();
				cloned.Data = Data.Clone();
				return cloned;
			}
		}

		public class StyleToggleState : ProductToggleState
		{
			public ImageTabState ImageTab { get; private set; }
			public LayoutTabState LayoutTab { get; private set; }

			public StyleToggleState()
			{
				ImageTab = new ImageTabState();
				LayoutTab = new LayoutTabState();
			}

			public StyleToggleState Clone()
			{
				var cloned = new StyleToggleState();
				cloned.ImageTab = ImageTab.Clone();
				cloned.LayoutTab = LayoutTab.Clone();
				return cloned;
			}
		}

		public class StatementsTabState
		{
			public List<StatementItemState> Items { get; }

			public StatementsTabState()
			{
				Items = new List<StatementItemState>();
			}

			public StatementsTabState Clone()
			{
				var cloned = new StatementsTabState();
				cloned.Items.AddRange(Items.Select(item => item?.Clone()));
				return cloned;
			}
		}

		public class BulletsTabState
		{
			public bool? Toggled { get; set; }

			public ListDataItem Combo1 { get; set; }

			public List<ListDataItem> Bullets { get; }

			public BulletsTabState()
			{
				Bullets = new List<ListDataItem>();
			}

			public BulletsTabState Clone()
			{
				var cloned = new BulletsTabState();
				cloned.Toggled = Toggled;
				cloned.Combo1 = ListDataItem.Clone(Combo1);
				cloned.Bullets.AddRange(Bullets.Select(ListDataItem.Clone));
				return cloned;
			}
		}

		public class ResearchDataTabState
		{
			public bool? Toggled { get; set; }

			public ResearchBundleState BundleState { get; set; }

			public ResearchDataTabState Clone()
			{
				var cloned = new ResearchDataTabState();
				cloned.Toggled = Toggled;
				cloned.BundleState = BundleState?.Clone();
				return cloned;
			}
		}

		public class ImageTabState
		{
			public ClipartObject Clipart { get; set; }

			public ImageTabState Clone()
			{
				var cloned = new ImageTabState();
				cloned.Clipart = Clipart?.Clone();
				return cloned;
			}
		}

		public class LayoutTabState
		{
			public LayoutItem SavedLayout { get; set; }

			public LayoutTabState Clone()
			{
				var cloned = new LayoutTabState();
				cloned.SavedLayout = SavedLayout?.Clone();
				return cloned;
			}
		}

		public class StatementItemState
		{
			public ListDataItem Combo { get; set; }
			public ListDataItem MemoPopup { get; set; }

			public StatementItemState Clone()
			{
				var cloned = new StatementItemState();
				cloned.Combo = ListDataItem.Clone(Combo);
				cloned.MemoPopup = ListDataItem.Clone(MemoPopup);
				return cloned;
			}
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

			public ResearchBundleState Clone()
			{
				var cloned = new ResearchBundleState();
				cloned.Item1 = Item1;
				cloned.Item2 = Item2;
				cloned.Item3 = Item3;
				return cloned;
			}
		}
	}
}
