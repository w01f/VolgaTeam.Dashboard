using System;
using System.Collections.Generic;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration.NeedsSolutions;

namespace Asa.Business.Solutions.Shift.Entities.NonPersistent
{
	public class NeedsSolutionsState
	{
		public TabAState TabA { get; }
		public TabBState TabB { get; }
		public TabCState TabC { get; }
		public TabDState TabD { get; }
		public TabEState TabE { get; }
		public TabFState TabF { get; }
		public TabUState TabU { get; }
		public TabVState TabV { get; }
		public TabWState TabW { get; }

		public NeedsSolutionsState()
		{
			TabA = new TabAState();
			TabB = new TabBState();
			TabC = new TabCState();
			TabD = new TabDState();
			TabE = new TabEState();
			TabF = new TabFState();
			TabU = new TabUState();
			TabV = new TabVState();
			TabW = new TabWState();
		}

		public class TabAState
		{
			public ListDataItem SlideHeader { get; set; }
			public bool? EnableOutput { get; set; }
			public ListDataItem Combo1 { get; set; }

			public List<NeedsItemState> Items { get; }

			public TabAState()
			{
				Items = new List<NeedsItemState>();
			}
		}

		public class TabBState
		{
			public ListDataItem SlideHeader { get; set; }
			public bool? EnableOutput { get; set; }
			public ClipartObject Clipart1 { get; set; }
			public ClipartObject Clipart2 { get; set; }
			public ClipartObject Clipart3 { get; set; }
			public NeedsItemState ItemState1 { get; set; }
			public NeedsItemState ItemState2 { get; set; }
			public NeedsItemState ItemState3 { get; set; }
			public NeedsItemState ItemState4 { get; set; }
		}

		public class TabCState
		{
			public ListDataItem SlideHeader { get; set; }
			public bool? EnableOutput { get; set; }
			public ListDataItem Combo1 { get; set; }

			public List<SolutionsItemState> Items { get; }

			public TabCState()
			{
				Items = new List<SolutionsItemState>();
			}
		}

		public class TabDState
		{
			public ListDataItem SlideHeader { get; set; }
			public bool? EnableOutput { get; set; }
			public ClipartObject Clipart1 { get; set; }
			public ClipartObject Clipart2 { get; set; }
			public ClipartObject Clipart3 { get; set; }
			public SolutionsItemState ItemState1 { get; set; }
			public SolutionsItemState ItemState2 { get; set; }
			public SolutionsItemState ItemState3 { get; set; }
			public SolutionsItemState ItemState4 { get; set; }
		}

		public class TabEState
		{
			public ListDataItem SlideHeader { get; set; }
			public bool? EnableOutput { get; set; }
			public ListDataItem Combo1 { get; set; }

			public NeedsItemState ItemState1 { get; set; }
			public NeedsItemState ItemState2 { get; set; }
			public NeedsItemState ItemState3 { get; set; }
			public NeedsItemState ItemState4 { get; set; }
		}

		public class TabFState
		{
			public ListDataItem SlideHeader { get; set; }
			public bool? EnableOutput { get; set; }
			public ListDataItem Combo1 { get; set; }

			public SolutionsItemState ItemState1 { get; set; }
			public SolutionsItemState ItemState2 { get; set; }
			public SolutionsItemState ItemState3 { get; set; }
			public SolutionsItemState ItemState4 { get; set; }
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

		public class NeedsItemState
		{
			public string Id { get; set; }
			public int Index { get; set; }
			public ClipartObject Clipart { get; set; }
			public string Title { get; set; }
			public string Subheader { get; set; }

			public bool IsEmpty()
			{
				return Clipart == null || String.IsNullOrWhiteSpace(Title);
			}

			public static NeedsItemState FromItemInfo(NeedsItemInfo itemInfo)
			{
				var itemState = new NeedsItemState();
				if (itemInfo != null)
				{
					itemState.Id = itemInfo.Id;
					itemState.Clipart = ImageClipartObject.FromFile(itemInfo.ImagePath);
					itemState.Title = itemInfo.Title;
					itemState.Subheader = itemInfo.SubHeaderDefaultValue;
				}
				return itemState;
			}

			public static NeedsItemState Empty()
			{
				var itemState = new NeedsItemState();
				return itemState;
			}
		}

		public class SolutionsItemState
		{
			public string Id { get; set; }
			public int Index { get; set; }
			public ClipartObject Clipart { get; set; }
			public string Title { get; set; }
			public string Subheader { get; set; }

			public bool IsEmpty()
			{
				return Clipart == null || String.IsNullOrWhiteSpace(Title);
			}

			public static SolutionsItemState FromItemInfo(SolutionsItemInfo itemInfo)
			{
				var itemState = new SolutionsItemState();
				if (itemInfo != null)
				{
					itemState.Id = itemInfo.Id;
					itemState.Clipart = ImageClipartObject.FromFile(itemInfo.ImagePath);
					itemState.Title = itemInfo.Title;
					itemState.Subheader = itemInfo.SubHeaderDefaultValue;
				}
				return itemState;
			}

			public static SolutionsItemState Empty()
			{
				var itemState = new SolutionsItemState();
				return itemState;
			}
		}
	}
}
