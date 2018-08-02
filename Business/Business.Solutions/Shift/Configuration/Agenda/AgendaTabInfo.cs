using System;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Agenda
{
	public class AgendaTabInfo : ShiftChildTabsContainer
	{
		public AgendaTabInfo() : base(ShiftTopTabType.Agenda) { }

		protected override ShiftChildTabInfo CreatChildTab(string tabId)
		{
			switch (tabId)
			{
				case "a":
					return new AgendaTabAInfo();
				case "b":
					return new AgendaTabBInfo();
				case "c":
					return new AgendaTabCInfo();
				case "d":
					return new AgendaTabDInfo();
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
