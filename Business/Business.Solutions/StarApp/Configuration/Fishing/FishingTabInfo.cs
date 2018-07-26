using System;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Fishing
{
	public class FishingTabInfo : StarChildTabsContainer
	{
		public override StarTopTabType TabType => StarTopTabType.Fishing;

		protected override StarChildTabInfo CreatChildTab(String tabId)
		{
			switch (tabId)
			{
				case "a":
					return new FishingTabAInfo();
				case "b":
					return new FishingTabBInfo();
				case "c":
					return new FishingTabCInfo();
				default:
					throw new ArgumentOutOfRangeException("Star tab type is not defined");
			}
		}
	}
}
