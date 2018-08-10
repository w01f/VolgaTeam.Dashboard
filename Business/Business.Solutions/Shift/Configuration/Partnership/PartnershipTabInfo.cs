using System;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Partnership
{
	public class PartnershipTabInfo : ShiftChildTabsContainer
	{
		public PartnershipTabInfo() : base(ShiftTopTabType.Partnership) { }

		protected override ShiftChildTabInfo CreatChildTab(string tabId)
		{
			switch (tabId)
			{
				case "a":
					return new PartnershipTabAInfo();
				case "b":
					return new PartnershipTabBInfo();
				case "c":
					return new PartnershipTabCInfo();
				case "d":
					return new PartnershipTabDInfo();
				case "e":
					return new CommonChildTabInfo(ShiftChildTabType.E, TabType);
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
