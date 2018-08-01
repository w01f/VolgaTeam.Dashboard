using System;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Intro
{
	public class IntroTabInfo : ShiftChildTabsContainer
	{
		public IntroTabInfo() : base(ShiftTopTabType.Intro) { }

		protected override ShiftChildTabInfo CreatChildTab(string tabId)
		{
			switch (tabId)
			{
				case "a":
					return new IntroTabAInfo();
				case "b":
					return new IntroTabBInfo();
				case "c":
					return new IntroTabCInfo();
				case "d":
					return new IntroTabDInfo();
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
