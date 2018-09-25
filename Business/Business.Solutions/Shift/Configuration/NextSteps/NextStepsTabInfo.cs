using System;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.NextSteps
{
	public class NextStepsTabInfo : ShiftChildTabsContainer
	{
		public NextStepsTabInfo() : base(ShiftTopTabType.NextSteps) { }

		protected override ShiftChildTabInfo CreatChildTab(string tabId)
		{
			switch (tabId)
			{
				case "a":
					return new NextStepsTabAInfo();
				case "b":
					return new NextStepsTabBInfo();
				case "c":
					return new NextStepsTabCInfo();
				case "d":
					return new NextStepsTabDInfo();
				case "e":
					return new NextStepsTabEInfo();
				case "f":
					return new NextStepsTabFInfo();
				case "g":
					return new NextStepsTabGInfo();
				case "h":
					return new NextStepsTabHInfo();
				case "i":
					return new NextStepsTabIInfo();
				case "j":
					return new CommonChildTabInfo(ShiftChildTabType.J, TabType);
				default:
					throw new ArgumentOutOfRangeException("Star tab type is not defined");
			}
		}
	}
}
