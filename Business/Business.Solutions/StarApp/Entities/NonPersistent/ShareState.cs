using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;

namespace Asa.Business.Solutions.StarApp.Entities.NonPersistent
{
	public class ShareState
	{
		public TabAState TabA { get; }
		public TabBState TabB { get; }
		public TabCState TabC { get; }
		public TabDState TabD { get; }
		public TabEState TabE { get; }
		public TabUState TabU { get; }
		public TabVState TabV { get; }
		public TabWState TabW { get; }

		public ShareState()
		{
			TabA = new TabAState();
			TabB = new TabBState();
			TabC = new TabCState();
			TabD = new TabDState();
			TabE = new TabEState();
			TabU = new TabUState();
			TabV = new TabVState();
			TabW = new TabWState();
		}

		public class TabAState
		{
			public ListDataItem SlideHeader { get; set; }
			public ClipartObject Clipart1 { get; set; }
			public ClipartObject Clipart2 { get; set; }
			public ClipartObject Clipart3 { get; set; }
			public bool Group1Toggle { get; set; }
			public bool Group2Toggle { get; set; }
			public bool Group3Toggle { get; set; }
			public bool Group4Toggle { get; set; }
			public bool Subheader3Toggle { get; set; }
			public bool Subheader5Toggle { get; set; }
			public bool Formula1Toggle { get; set; }
			public bool Formula2Toggle { get; set; }
			public ListDataItem Combo1 { get; set; }
			public ListDataItem Combo2 { get; set; }
			public ListDataItem Combo3 { get; set; }
			public ListDataItem Combo4 { get; set; }
			public string Subheader1 { get; set; }
			public decimal? Subheader2 { get; set; }
			public string Subheader3 { get; set; }
			public string Subheader4 { get; set; }
			public string Subheader5 { get; set; }
			public string Subheader6 { get; set; }
			public string Subheader7 { get; set; }

			public TabAState()
			{
				Group1Toggle = true;
				Group2Toggle = true;
				Group3Toggle = true;
				Group4Toggle = true;
				Subheader3Toggle = true;
				Subheader5Toggle = true;
				Formula1Toggle = true;
				Formula2Toggle = true;
			}
		}

		public class TabBState
		{
			public ListDataItem SlideHeader { get; set; }
			public ClipartObject Clipart1 { get; set; }
			public ClipartObject Clipart2 { get; set; }
			public ClipartObject Clipart3 { get; set; }
			public bool Group1Toggle { get; set; }
			public bool Group2Toggle { get; set; }
			public bool Group3Toggle { get; set; }
			public bool Group4Toggle { get; set; }
			public bool Group5Toggle { get; set; }
			public bool Subheader2Toggle { get; set; }
			public bool Subheader7Toggle { get; set; }
			public ListDataItem Combo1 { get; set; }
			public ListDataItem Combo2 { get; set; }
			public string Subheader1 { get; set; }
			public string Subheader2 { get; set; }
			public string Subheader3 { get; set; }
			public decimal? Subheader4 { get; set; }
			public string Subheader5 { get; set; }
			public decimal? Subheader6 { get; set; }
			public string Subheader7 { get; set; }
			public string Subheader8 { get; set; }

			public TabBState()
			{
				Group1Toggle = true;
				Group2Toggle = true;
				Group3Toggle = true;
				Group4Toggle = true;
				Group5Toggle = true;
				Subheader2Toggle = true;
				Subheader7Toggle = true;
			}
		}

		public class TabCState
		{
			public ListDataItem SlideHeader { get; set; }
			public ClipartObject Clipart1 { get; set; }
			public ClipartObject Clipart2 { get; set; }
			public ClipartObject Clipart3 { get; set; }
			public bool Group1Toggle { get; set; }
			public bool Group2Toggle { get; set; }
			public bool Group3Toggle { get; set; }
			public bool Group4Toggle { get; set; }
			public bool Group5Toggle { get; set; }
			public bool Group6Toggle { get; set; }
			public bool Subheader3Toggle { get; set; }
			public ListDataItem Combo1 { get; set; }
			public ListDataItem Combo2 { get; set; }
			public ListDataItem Combo3 { get; set; }
			public ListDataItem Combo4 { get; set; }
			public ListDataItem Combo5 { get; set; }
			public ListDataItem Combo6 { get; set; }
			public decimal? Subheader1 { get; set; }
			public string Subheader2 { get; set; }
			public string Subheader3 { get; set; }
			public string Subheader4 { get; set; }

			public TabCState()
			{
				Group1Toggle = true;
				Group2Toggle = true;
				Group3Toggle = true;
				Group4Toggle = true;
				Group5Toggle = true;
				Group6Toggle = true;
				Subheader3Toggle = true;
			}
		}

		public class TabDState
		{
			public ListDataItem SlideHeader { get; set; }
			public ClipartObject Clipart1 { get; set; }
			public ClipartObject Clipart2 { get; set; }
			public ClipartObject Clipart3 { get; set; }
			public bool Group1Toggle { get; set; }
			public bool Group2Toggle { get; set; }
			public bool Group3Toggle { get; set; }
			public bool Group4Toggle { get; set; }
			public bool Group5Toggle { get; set; }
			public bool Group6Toggle { get; set; }
			public bool Group7Toggle { get; set; }
			public ListDataItem Combo1 { get; set; }
			public ListDataItem Combo2 { get; set; }
			public ListDataItem Combo3 { get; set; }
			public string Subheader1 { get; set; }
			public string Subheader2 { get; set; }
			public string Subheader3 { get; set; }
			public decimal? Subheader4 { get; set; }
			public string Subheader5 { get; set; }
			public string Subheader6 { get; set; }
			public string Subheader7 { get; set; }
			public string Subheader8 { get; set; }
			public string Subheader9 { get; set; }

			public TabDState()
			{
				Group1Toggle = true;
				Group2Toggle = true;
				Group3Toggle = true;
				Group4Toggle = true;
				Group5Toggle = true;
				Group6Toggle = true;
				Group7Toggle = true;
			}
		}

		public class TabEState
		{
			public ListDataItem SlideHeader { get; set; }
			public ClipartObject Clipart1 { get; set; }
			public ClipartObject Clipart2 { get; set; }
			public ClipartObject Clipart3 { get; set; }
			public bool Group1Toggle { get; set; }
			public bool Group2Toggle { get; set; }
			public bool Group3Toggle { get; set; }
			public bool Group4Toggle { get; set; }
			public bool Group5Toggle { get; set; }
			public bool Group6Toggle { get; set; }
			public bool Group7Toggle { get; set; }
			public bool Subheader4Toggle { get; set; }
			public ListDataItem Combo1 { get; set; }
			public ListDataItem Combo2 { get; set; }
			public ListDataItem Combo3 { get; set; }
			public ListDataItem Combo4 { get; set; }
			public string Subheader1 { get; set; }
			public string Subheader2 { get; set; }
			public string Subheader3 { get; set; }
			public string Subheader4 { get; set; }
			public string Subheader5 { get; set; }
			public string Subheader6 { get; set; }
			public decimal? Subheader7 { get; set; }
			public string Subheader8 { get; set; }
			public string Subheader9 { get; set; }
			public string Subheader10 { get; set; }

			public TabEState()
			{
				Group1Toggle = true;
				Group2Toggle = true;
				Group3Toggle = true;
				Group4Toggle = true;
				Group5Toggle = true;
				Group6Toggle = true;
				Group7Toggle = true;
				Subheader4Toggle = true;
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
	}
}
