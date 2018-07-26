using System;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Closers
{
	public class ClosersTabInfo : StarChildTabsContainer
	{
		public override StarTopTabType TabType => StarTopTabType.Closers;

		protected override StarChildTabInfo CreatChildTab(String tabId)
		{
			switch (tabId)
			{
				case "a":
					return new ClosersTabAInfo();
				case "b":
					return new ClosersTabBInfo();
				case "c":
					return new ClosersTabCInfo();
				default:
					throw new ArgumentOutOfRangeException("Star tab type is not defined");
			}
		}
	}
}
