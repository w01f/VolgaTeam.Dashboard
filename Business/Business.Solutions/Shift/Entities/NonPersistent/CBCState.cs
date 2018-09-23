using System.Collections.Generic;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;

namespace Asa.Business.Solutions.Shift.Entities.NonPersistent
{
	public class CBCState
	{
		public TabAState TabA { get; }
		public TabBState TabB { get; }
		public TabCState TabC { get; }
		public TabDState TabD { get; }
		public TabEState TabE { get; }
		public TabFState TabF { get; }
		public SlidesTabState TabK { get; }
		public SlidesTabState TabL { get; }
		public SlidesTabState TabM { get; }
		public SlidesTabState TabN { get; }
		public SlidesTabState TabO { get; }
		public SlidesTabState TabU { get; }
		public SlidesTabState TabV { get; }
		public SlidesTabState TabW { get; }

		public CBCState()
		{
			TabA = new TabAState();
			TabB = new TabBState();
			TabC = new TabCState();
			TabD = new TabDState();
			TabE = new TabEState();
			TabF = new TabFState();
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
			public ClipartObject Clipart1 { get; set; }
			public ClipartObject Clipart2 { get; set; }
			public ClipartObject Clipart3 { get; set; }
			public ListDataItem MemoPopup1 { get; set; }
		}

		public class TabBState
		{
			public ListDataItem SlideHeader { get; set; }
			public bool? EnableOutput { get; set; }
			public SubTabState Tab1State { get; }
			public SubTabState Tab2State { get; }
			public SubTabState Tab3State { get; }
			public SubTabState Tab4State { get; }
			public SubTabState Tab5State { get; }

			public TabBState()
			{
				Tab1State = new SubTabState();
				Tab2State = new SubTabState();
				Tab3State = new SubTabState();
				Tab4State = new SubTabState();
				Tab5State = new SubTabState();
			}
		}

		public class TabCState
		{
			public ListDataItem SlideHeader { get; set; }
			public bool? EnableOutput { get; set; }
			public SubTabState Tab1State { get; }
			public SubTabState Tab2State { get; }

			public TabCState()
			{
				Tab1State = new SubTabState();
				Tab2State = new SubTabState();
			}
		}

		public class TabDState
		{
			public ListDataItem SlideHeader { get; set; }
			public bool? EnableOutput { get; set; }
			public SubTabState Tab3State { get; }
			public SubTabState Tab4State { get; }

			public TabDState()
			{
				Tab3State = new SubTabState();
				Tab4State = new SubTabState();
			}
		}

		public class TabEState
		{
			public ListDataItem SlideHeader { get; set; }
			public bool? EnableOutput { get; set; }
			public SubTabState Tab5State { get; }

			public TabEState()
			{
				Tab5State = new SubTabState();
			}
		}

		public class TabFState
		{
			public ListDataItem SlideHeader { get; set; }
			public bool? EnableOutput { get; set; }
			public ClipartObject Clipart1 { get; set; }
			public ClipartObject Clipart2 { get; set; }
			public ListDataItem Combo1 { get; set; }
			public string Subheader1 { get; set; }
			public string Subheader2 { get; set; }
			public string Subheader3 { get; set; }
			public string Subheader4 { get; set; }
			public string Subheader5 { get; set; }
		}

		public class SubTabState
		{
			public List<ListDataItem> ComboStates { get; }

			public SubTabState()
			{
				ComboStates = new List<ListDataItem>();
			}
		}
	}
}
