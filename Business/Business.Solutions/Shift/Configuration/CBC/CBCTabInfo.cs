using System;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.CBC
{
	public class CBCTabInfo : ShiftChildTabsContainer
	{
		public CBCTabInfo() : base(ShiftTopTabType.CBC) { }

		protected override ShiftChildTabInfo CreatChildTab(string tabId)
		{
			switch (tabId)
			{
				case "a":
					return new CBCTabAInfo();
				case "b":
					return new CBCTabBInfo();
				case "c":
					return new CBCTabCInfo();
				case "d":
					return new CBCTabDInfo();
				case "e":
					return new CBCTabEInfo();
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
