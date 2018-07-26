using System;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Market
{
	public class MarketTabInfo : StarChildTabsContainer
	{
		public override StarTopTabType TabType => StarTopTabType.Market;

		protected override StarChildTabInfo CreatChildTab(String tabId)
		{
			switch (tabId)
			{
				case "a":
					return new MarketTabAInfo();
				case "b":
					return new MarketTabBInfo();
				case "c":
					return new MarketTabCInfo();
				default:
					throw new ArgumentOutOfRangeException("Star tab type is not defined");
			}
		}
	}
}
