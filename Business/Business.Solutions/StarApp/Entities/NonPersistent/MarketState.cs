﻿using System.Drawing;
using Asa.Business.Solutions.StarApp.Configuration;

namespace Asa.Business.Solutions.StarApp.Entities.NonPersistent
{
	public class MarketState
	{
		public TabAState TabA { get; }
		public TabBState TabB { get; }
		public TabCState TabC { get; }

		public MarketState()
		{
			TabA = new TabAState();
			TabB = new TabBState();
			TabC = new TabCState();
		}

		public class TabAState
		{
			public ListDataItem SlideHeader { get; set; }
			public Image Clipart1 { get; set; }
			public string Subheader1 { get; set; }
		}

		public class TabBState
		{
			public ListDataItem SlideHeader { get; set; }
			public Image Clipart1 { get; set; }
			public Image Clipart2 { get; set; }
			public Image Clipart3 { get; set; }
			public Image Clipart4 { get; set; }
			public Image Clipart5 { get; set; }
			public string Subheader1 { get; set; }
			public string Subheader2 { get; set; }
		}

		public class TabCState
		{
			public ListDataItem SlideHeader { get; set; }
			public Image Clipart1 { get; set; }
			public Image Clipart2 { get; set; }
			public Image Clipart3 { get; set; }
			public Image Clipart4 { get; set; }
			public ListDataItem Combo1 { get; set; }
		}
	}
}
