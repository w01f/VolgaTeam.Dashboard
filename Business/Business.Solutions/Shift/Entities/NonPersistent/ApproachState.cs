using System;
using System.Collections.Generic;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Shift.Configuration.Approach;

namespace Asa.Business.Solutions.Shift.Entities.NonPersistent
{
	public class ApproachState
	{
		public TabAState TabA { get; }
		public TabBState TabB { get; }
		public TabCState TabC { get; }
		public TabUState TabU { get; }
		public TabVState TabV { get; }
		public TabWState TabW { get; }

		public ApproachState()
		{
			TabA = new TabAState();
			TabB = new TabBState();
			TabC = new TabCState();
			TabU = new TabUState();
			TabV = new TabVState();
			TabW = new TabWState();
		}

		public class TabAState
		{
			public ListDataItem SlideHeader { get; set; }
			public bool? EnableOutput { get; set; }
			public ListDataItem Combo1 { get; set; }

			public List<ApproachItemState> Items { get; }

			public TabAState()
			{
				Items = new List<ApproachItemState>();
			}
		}

		public class TabBState
		{
			public ListDataItem SlideHeader { get; set; }
			public bool? EnableOutput { get; set; }
			public ClipartObject Clipart1 { get; set; }
			public ClipartObject Clipart2 { get; set; }
			public ClipartObject Clipart3 { get; set; }
			public ApproachItemState ItemState1 { get; set; }
			public ApproachItemState ItemState2 { get; set; }
			public ApproachItemState ItemState3 { get; set; }
			public ApproachItemState ItemState4 { get; set; }
		}

		public class TabCState
		{
			public ListDataItem SlideHeader { get; set; }
			public bool? EnableOutput { get; set; }
			public ListDataItem Combo1 { get; set; }

			public ApproachItemState ItemState1 { get; set; }
			public ApproachItemState ItemState2 { get; set; }
			public ApproachItemState ItemState3 { get; set; }
			public ApproachItemState ItemState4 { get; set; }
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

		public class ApproachItemState
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

			public static ApproachItemState FromItemInfo(ApproachItemInfo itemInfo)
			{
				var itemState = new ApproachItemState();
				if (itemInfo != null)
				{
					itemState.Id = itemInfo.Id;
					itemState.Clipart = ImageClipartObject.FromFile(itemInfo.ImagePath);
					itemState.Title = itemInfo.Title;
					itemState.Subheader = itemInfo.SubHeaderDefaultValue;
				}
				return itemState;
			}

			public static ApproachItemState Empty()
			{
				var itemState = new ApproachItemState();
				return itemState;
			}
		}
	}
}
