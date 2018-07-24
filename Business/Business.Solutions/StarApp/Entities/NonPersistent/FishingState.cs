using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Entities.NonPersistent;

namespace Asa.Business.Solutions.StarApp.Entities.NonPersistent
{
	public class FishingState
	{
		public TabAState TabA { get; }
		public TabBState TabB { get; }
		public TabCState TabC { get; }
		public TabUState TabU { get; }
		public TabVState TabV { get; }
		public TabWState TabW { get; }

		public FishingState()
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
			public ClipartObject Clipart1 { get; set; }
			public string Subheader1 { get; set; }
			public string Subheader2 { get; set; }
		}

		public class TabBState
		{
			public ListDataItem SlideHeader { get; set; }
			public ClipartObject Clipart1 { get; set; }
			public ClipartObject Clipart2 { get; set; }
			public ListDataItem Combo1 { get; set; }
			public ListDataItem Combo2 { get; set; }
			public ListDataItem Combo3 { get; set; }
			public ListDataItem Combo4 { get; set; }
		}

		public class TabCState
		{
			public ListDataItem SlideHeader { get; set; }
			public string Subheader1 { get; set; }
			public string Subheader2 { get; set; }
			public string Subheader3 { get; set; }
		}

		public class TabUState { }

		public class TabVState { }

		public class TabWState { }
	}
}
