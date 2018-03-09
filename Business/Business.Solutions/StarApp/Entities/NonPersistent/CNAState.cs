using System.Drawing;
using Asa.Business.Solutions.StarApp.Configuration;

namespace Asa.Business.Solutions.StarApp.Entities.NonPersistent
{
	public class CNAState
	{
		public TabAState TabA { get; }
		public TabBState TabB { get; }

		public CNAState()
		{
			TabA = new TabAState();
			TabB = new TabBState();
		}

		public class TabAState
		{
			public ListDataItem SlideHeader { get; set; }
			public Image Clipart1 { get; set; }
			public string Subheader1 { get; set; }
			public string Subheader2 { get; set; }
		}

		public class TabBState
		{
			public ListDataItem SlideHeader { get; set; }
			public Image Clipart1 { get; set; }
			public Image Clipart2 { get; set; }
			public ListDataItem Combo1 { get; set; }
			public ListDataItem Combo2 { get; set; }
			public ListDataItem Combo3 { get; set; }
			public ListDataItem Combo4 { get; set; }
			public ListDataItem Combo5 { get; set; }
		}
	}
}
