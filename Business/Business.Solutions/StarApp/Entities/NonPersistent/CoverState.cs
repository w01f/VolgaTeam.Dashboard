using System;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Dictionaries;
using Asa.Business.Solutions.Common.Entities.NonPersistent;

namespace Asa.Business.Solutions.StarApp.Entities.NonPersistent
{
	public class CoverState
	{
		public TabAState TabA { get; }
		public TabUState TabU { get; }
		public TabVState TabV { get; }
		public TabWState TabW { get; }

		public CoverState()
		{
			TabA = new TabAState();
			TabU = new TabUState();
			TabV = new TabVState();
			TabW = new TabWState();
		}

		public class TabAState
		{
			public ListDataItem SlideHeader { get; set; }
			public bool AddAsPageOne { get; set; }
			public ClipartObject Clipart1 { get; set; }
			public string Subheader1 { get; set; }
			public DateTime? Calendar1 { get; set; }
			public User Combo1 { get; set; }

			public TabAState()
			{
				AddAsPageOne = true;
			}
		}

		public class TabUState { }

		public class TabVState { }

		public class TabWState { }
	}
}
