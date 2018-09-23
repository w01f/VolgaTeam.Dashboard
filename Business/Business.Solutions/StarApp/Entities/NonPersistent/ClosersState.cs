using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Dictionaries;
using Asa.Business.Solutions.Common.Entities.NonPersistent;

namespace Asa.Business.Solutions.StarApp.Entities.NonPersistent
{
	public class ClosersState
	{
		public TabAState TabA { get; }
		public TabBState TabB { get; }
		public TabCState TabC { get; }
		public SlidesTabState TabK { get; }
		public SlidesTabState TabL { get; }
		public SlidesTabState TabM { get; }
		public SlidesTabState TabN { get; }
		public SlidesTabState TabO { get; }
		public SlidesTabState TabU { get; }
		public SlidesTabState TabV { get; }
		public SlidesTabState TabW { get; }

		public ClosersState()
		{
			TabA = new TabAState();
			TabB = new TabBState();
			TabC = new TabCState();
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
			public string Subheader1 { get; set; }
			public User Combo1 { get; set; }
			public User Combo2 { get; set; }
			public User Combo3 { get; set; }
			public User Combo4 { get; set; }
			public User Combo5 { get; set; }
			public User Combo6 { get; set; }
			public User Combo7 { get; set; }
			public User Combo8 { get; set; }
			public User Combo9 { get; set; }
			public User Combo10 { get; set; }
			public User Combo11 { get; set; }
		}

		public class TabBState
		{
			public ListDataItem SlideHeader { get; set; }
			public bool? EnableOutput { get; set; }
			public ClipartObject Clipart1 { get; set; }
			public ClipartObject Clipart2 { get; set; }
			public string Subheader1 { get; set; }
			public string Subheader2 { get; set; }
			public string Subheader3 { get; set; }
			public ListDataItem Combo1 { get; set; }
			public ListDataItem Combo2 { get; set; }
			public ListDataItem Combo3 { get; set; }
			public ListDataItem Combo4 { get; set; }
		}

		public class TabCState
		{
			public ListDataItem SlideHeader { get; set; }
			public bool? EnableOutput { get; set; }
			public ClipartObject Clipart1 { get; set; }
			public ClipartObject Clipart2 { get; set; }
			public string Subheader1 { get; set; }
			public string Subheader2 { get; set; }
			public User Combo1 { get; set; }
			public User Combo2 { get; set; }
			public User Combo3 { get; set; }
			public User Combo4 { get; set; }
			public User Combo5 { get; set; }
			public User Combo6 { get; set; }
			public User Combo7 { get; set; }
			public User Combo8 { get; set; }
			public User Combo9 { get; set; }
			public User Combo10 { get; set; }
			public User Combo11 { get; set; }
		}
	}
}
