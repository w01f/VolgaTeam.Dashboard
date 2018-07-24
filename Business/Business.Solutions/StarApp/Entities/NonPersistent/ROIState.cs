using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;

namespace Asa.Business.Solutions.StarApp.Entities.NonPersistent
{
	public class ROIState
	{
		public TabAState TabA { get; }
		public TabBState TabB { get; }
		public TabCState TabC { get; }
		public TabDState TabD { get; }
		public TabUState TabU { get; }
		public TabVState TabV { get; }
		public TabWState TabW { get; }

		public ROIState()
		{
			TabA = new TabAState();
			TabB = new TabBState();
			TabC = new TabCState();
			TabD = new TabDState();
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
			public bool Group5Toggle { get; set; }
			public bool Group6Toggle { get; set; }
			public bool Subheader14Toggle { get; set; }
			public bool Subheader15Toggle { get; set; }
			public string Subheader1 { get; set; }
			public decimal? Subheader2 { get; set; }
			public string Subheader3 { get; set; }
			public string Subheader4 { get; set; }
			public decimal? Subheader5 { get; set; }
			public string Subheader6 { get; set; }
			public string Subheader7 { get; set; }
			public decimal? Subheader8 { get; set; }
			public string Subheader9 { get; set; }
			public string Subheader10 { get; set; }
			public string Subheader11 { get; set; }
			public string Subheader12 { get; set; }
			public string Subheader13 { get; set; }
			public decimal? Subheader14 { get; set; }
			public string Subheader15 { get; set; }

			public TabAState()
			{
				Group1Toggle = true;
				Group2Toggle = true;
				Group3Toggle = true;
				Group4Toggle = true;
				Group5Toggle = true;
				Group6Toggle = true;
				Subheader14Toggle = true;
				Subheader15Toggle = true;
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
			public bool Group6Toggle { get; set; }
			public bool Group7Toggle { get; set; }
			public bool Group8Toggle { get; set; }
			public bool Group9Toggle { get; set; }
			public bool Group10Toggle { get; set; }
			public bool Group11Toggle { get; set; }
			public bool Subheader24Toggle { get; set; }
			public bool Subheader25Toggle { get; set; }
			public string Subheader1 { get; set; }
			public decimal? Subheader2 { get; set; }
			public string Subheader3 { get; set; }
			public string Subheader4 { get; set; }
			public decimal? Subheader5 { get; set; }
			public string Subheader6 { get; set; }
			public string Subheader7 { get; set; }
			public decimal? Subheader8 { get; set; }
			public string Subheader9 { get; set; }
			public string Subheader10 { get; set; }
			public decimal? Subheader11 { get; set; }
			public string Subheader12 { get; set; }
			public string Subheader13 { get; set; }
			public string Subheader14 { get; set; }
			public string Subheader15 { get; set; }
			public string Subheader16 { get; set; }
			public decimal? Subheader17 { get; set; }
			public string Subheader18 { get; set; }
			public string Subheader19 { get; set; }
			public string Subheader20 { get; set; }
			public string Subheader21 { get; set; }
			public string Subheader22 { get; set; }
			public string Subheader23 { get; set; }
			public decimal? Subheader24 { get; set; }
			public string Subheader25 { get; set; }

			public TabBState()
			{
				Group1Toggle = true;
				Group2Toggle = true;
				Group3Toggle = true;
				Group4Toggle = true;
				Group5Toggle = true;
				Group6Toggle = true;
				Group7Toggle = true;
				Group8Toggle = true;
				Group9Toggle = true;
				Group10Toggle = true;
				Group11Toggle = true;
				Subheader24Toggle = true;
				Subheader25Toggle = true;
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
			public bool Group7Toggle { get; set; }
			public bool Group8Toggle { get; set; }
			public bool Subheader2Toggle { get; set; }
			public bool Subheader4Toggle { get; set; }
			public bool Subheader5Toggle { get; set; }
			public bool Subheader7Toggle { get; set; }
			public bool Subheader8Toggle { get; set; }
			public bool Subheader10Toggle { get; set; }
			public bool Subheader14Toggle { get; set; }
			public bool Formula1Toggle { get; set; }
			public bool Formula2Toggle { get; set; }
			public bool Formula3Toggle { get; set; }
			public string Subheader1 { get; set; }
			public decimal? Subheader2 { get; set; }
			public string Subheader3 { get; set; }
			public decimal? Subheader4 { get; set; }
			public string Subheader5 { get; set; }
			public string Subheader6 { get; set; }
			public decimal? Subheader7 { get; set; }
			public string Subheader8 { get; set; }
			public string Subheader9 { get; set; }
			public string Subheader10 { get; set; }
			public string Subheader11 { get; set; }
			public string Subheader12 { get; set; }
			public decimal? Subheader13 { get; set; }
			public string Subheader14 { get; set; }
			public string Subheader15 { get; set; }

			public TabCState()
			{
				Group1Toggle = true;
				Group2Toggle = true;
				Group3Toggle = true;
				Group4Toggle = true;
				Group5Toggle = true;
				Group6Toggle = true;
				Group7Toggle = true;
				Group8Toggle = true;
				Subheader2Toggle = true;
				Subheader4Toggle = true;
				Subheader5Toggle = true;
				Subheader7Toggle = true;
				Subheader8Toggle = true;
				Subheader10Toggle = true;
				Subheader14Toggle = true;
				Formula1Toggle = true;
				Formula2Toggle = true;
				Formula3Toggle = true;
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
			public bool Group8Toggle { get; set; }
			public bool Group9Toggle { get; set; }
			public bool Group10Toggle { get; set; }
			public bool Subheader2Toggle { get; set; }
			public bool Subheader4Toggle { get; set; }
			public bool Subheader6Toggle { get; set; }
			public bool Subheader8Toggle { get; set; }
			public bool Subheader10Toggle { get; set; }
			public bool Subheader12Toggle { get; set; }
			public bool Subheader15Toggle { get; set; }
			public bool Formula1Toggle { get; set; }
			public bool Formula2Toggle { get; set; }
			public bool Formula3Toggle { get; set; }
			public string Subheader1 { get; set; }
			public decimal? Subheader2 { get; set; }
			public string Subheader3 { get; set; }
			public decimal? Subheader4 { get; set; }
			public string Subheader5 { get; set; }
			public decimal? Subheader6 { get; set; }
			public string Subheader7 { get; set; }
			public decimal? Subheader8 { get; set; }
			public string Subheader9 { get; set; }
			public decimal? Subheader10 { get; set; }
			public string Subheader11 { get; set; }
			public decimal? Subheader12 { get; set; }
			public string Subheader13 { get; set; }
			public string Subheader14 { get; set; }
			public decimal? Subheader15 { get; set; }
			public string Subheader16 { get; set; }
			public string Subheader17 { get; set; }

			public TabDState()
			{
				Group1Toggle = true;
				Group2Toggle = true;
				Group3Toggle = true;
				Group4Toggle = true;
				Group5Toggle = true;
				Group6Toggle = true;
				Group7Toggle = true;
				Group8Toggle = true;
				Group9Toggle = true;
				Group10Toggle = true;
				Subheader2Toggle = true;
				Subheader4Toggle = true;
				Subheader6Toggle = true;
				Subheader8Toggle = true;
				Subheader10Toggle = true;
				Subheader12Toggle = true;
				Subheader15Toggle = true;
				Formula1Toggle = true;
				Formula2Toggle = true;
				Formula3Toggle = true;
			}
		}

		public class TabUState { }

		public class TabVState { }

		public class TabWState { }
	}
}
