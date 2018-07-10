﻿using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.StarApp.Configuration;

namespace Asa.Business.Solutions.StarApp.Entities.NonPersistent
{
	public class VideoState
	{
		public TabAState TabA { get; }
		public TabBState TabB { get; }
		public TabCState TabC { get; }
		public TabDState TabD { get; }

		public VideoState()
		{
			TabA = new TabAState();
			TabB = new TabBState();
			TabC = new TabCState();
			TabD = new TabDState();
		}

		public class TabAState
		{
			public ListDataItem SlideHeader { get; set; }
			public ClipartObject Clipart1 { get; set; }
			public string Subheader1 { get; set; }
		}

		public class TabBState
		{
			public ListDataItem SlideHeader { get; set; }
			public ClipartObject Clipart1 { get; set; }
			public string Subheader1 { get; set; }
		}

		public class TabCState
		{
			public ListDataItem SlideHeader { get; set; }
			public ClipartObject Clipart1 { get; set; }
			public string Subheader1 { get; set; }
		}

		public class TabDState
		{
			public ListDataItem SlideHeader { get; set; }
			public ClipartObject Clipart1 { get; set; }
			public string Subheader1 { get; set; }
		}
	}
}
