using System;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.IntegratedSolution
{
	public class IntegratedSolutionTabInfo : ShiftChildTabsContainer
	{
		public IntegratedSolutionTabInfo() : base(ShiftTopTabType.IntegratedSolution) { }

		protected override ShiftChildTabInfo CreatChildTab(string tabId)
		{
			switch (tabId)
			{
				case "a":
					return new IntegratedSolutionTabAInfo();
				case "b":
					return new CommonChildTabInfo(ShiftChildTabType.B, TabType);
				case "c":
					return new CommonChildTabInfo(ShiftChildTabType.C, TabType);
				case "d":
					return new CommonChildTabInfo(ShiftChildTabType.D, TabType);
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
