using System;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Investment
{
	public class InvestmentTabInfo : ShiftChildTabsContainer
	{
		public InvestmentTabInfo() : base(ShiftTopTabType.Investment) { }

		protected override ShiftChildTabInfo CreatChildTab(string tabId)
		{
			switch (tabId)
			{
				case "a":
					return new InvestmentTabAInfo();
				case "b":
					return new InvestmentTabBInfo();
				case "c":
					return new InvestmentTabCInfo();
				case "d":
					return new InvestmentTabDInfo();
				case "e":
					return new InvestmentTabEInfo();
				case "f":
					return new InvestmentTabFInfo();
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
