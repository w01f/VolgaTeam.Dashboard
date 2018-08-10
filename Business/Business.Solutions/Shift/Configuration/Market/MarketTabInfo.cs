using System;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Market
{
	public class MarketTabInfo : ShiftChildTabsContainer
	{
		public MarketTabInfo() : base(ShiftTopTabType.Market) { }

		protected override ShiftChildTabInfo CreatChildTab(string tabId)
		{
			switch (tabId)
			{
				case "a":
					return new MarketTabAInfo();
				case "b":
					return new MarketTabBInfo();
				case "c":
					return new MarketTabCInfo();
				case "d":
					return new MarketTabDInfo();
				case "e":
					return new MarketTabEInfo();
				case "f":
					return new CommonChildTabInfo(ShiftChildTabType.F, TabType);
				case "g":
					return new CommonChildTabInfo(ShiftChildTabType.G, TabType);
				case "h":
					return new CommonChildTabInfo(ShiftChildTabType.H, TabType);
				case "i":
					return new CommonChildTabInfo(ShiftChildTabType.I, TabType);
				case "j":
					return new CommonChildTabInfo(ShiftChildTabType.J, TabType);
				default:
					throw new ArgumentOutOfRangeException("Star tab type is not defined");
			}
		}
	}
}
