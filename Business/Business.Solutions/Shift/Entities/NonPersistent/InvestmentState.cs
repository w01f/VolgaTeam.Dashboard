﻿using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;

namespace Asa.Business.Solutions.Shift.Entities.NonPersistent
{
	public class InvestmentState
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

		public InvestmentState()
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
			public ClipartObject Clipart4 { get; set; }
			public ClipartObject Clipart5 { get; set; }
			public ListDataItem Combo1 { get; set; }
			public ListDataItem Combo2 { get; set; }
			public ListDataItem Combo3 { get; set; }
			public ListDataItem Combo4 { get; set; }
			public ListDataItem Combo5 { get; set; }
			public ListDataItem Combo6 { get; set; }
			public string Subheader1 { get; set; }
			public string Subheader2 { get; set; }
		}

		public class TabBState
		{
			public ListDataItem SlideHeader { get; set; }
			public bool? EnableOutput { get; set; }
			public ClipartObject Clipart1 { get; set; }
			public ClipartObject Clipart2 { get; set; }
			public ClipartObject Clipart3 { get; set; }
			public ClipartObject Clipart4 { get; set; }
			public ClipartObject Clipart5 { get; set; }
			public ListDataItem Combo1 { get; set; }
			public ListDataItem Combo2 { get; set; }
			public ListDataItem Combo3 { get; set; }
			public ListDataItem Combo4 { get; set; }
			public ListDataItem Combo5 { get; set; }
			public ListDataItem Combo6 { get; set; }
			public string Subheader1 { get; set; }
			public string Subheader2 { get; set; }
		}

		public class TabCState
		{
			public ListDataItem SlideHeader { get; set; }
			public bool? EnableOutput { get; set; }
			public ClipartObject Clipart1 { get; set; }
			public ClipartObject Clipart2 { get; set; }
			public ClipartObject Clipart3 { get; set; }
			public ClipartObject Clipart4 { get; set; }
			public ClipartObject Clipart5 { get; set; }
			public ListDataItem Combo1 { get; set; }
			public ListDataItem Combo2 { get; set; }
			public ListDataItem Combo3 { get; set; }
			public ListDataItem Combo4 { get; set; }
			public ListDataItem Combo5 { get; set; }
			public ListDataItem Combo6 { get; set; }
			public string Subheader1 { get; set; }
			public string Subheader2 { get; set; }
		}

		public class TabDState
		{
			public ListDataItem SlideHeader { get; set; }
			public bool? EnableOutput { get; set; }
			public ClipartObject Clipart1 { get; set; }
			public ClipartObject Clipart2 { get; set; }
			public ClipartObject Clipart3 { get; set; }
			public ClipartObject Clipart4 { get; set; }
			public ClipartObject Clipart5 { get; set; }
			public ListDataItem Combo1 { get; set; }
			public ListDataItem Combo2 { get; set; }
			public ListDataItem Combo3 { get; set; }
			public ListDataItem Combo4 { get; set; }
			public ListDataItem Combo5 { get; set; }
			public ListDataItem Combo6 { get; set; }
			public string Subheader1 { get; set; }
			public string Subheader2 { get; set; }
		}

		public class TabEState
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
			public ListDataItem Combo6 { get; set; }
			public ListDataItem Combo7 { get; set; }
			public ListDataItem Combo8 { get; set; }
			public string Subheader1 { get; set; }
		}

		public class TabFState
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
			public ListDataItem Combo6 { get; set; }
			public ListDataItem Combo7 { get; set; }
			public ListDataItem Combo8 { get; set; }
			public string Subheader1 { get; set; }
		}
	}
}