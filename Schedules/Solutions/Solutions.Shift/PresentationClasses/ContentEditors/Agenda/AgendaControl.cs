using System.Collections.Generic;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Agenda
{
	class AgendaControl : MultiTabControl
	{
		public AgendaControl(BaseShiftContainer slideContainer, ShiftChildTabsContainer tabInfo) : base(slideContainer, tabInfo) { }

		protected override IList<IChildTabPageContainer> GetChildTabPages()
		{
			var tabPages = new List<IChildTabPageContainer>();
			foreach (var tabInfo in TabContainerInfo.ChildTabs)
			{
				switch (tabInfo.TabType)
				{
					case ShiftChildTabType.A:
						tabPages.Add(new ChildTabPageContainerControl<AgendaTabAControl>(this, tabInfo));
						break;
					case ShiftChildTabType.B:
						tabPages.Add(new ChildTabPageContainerControl<AgendaTabBControl>(this, tabInfo));
						break;
					case ShiftChildTabType.C:
						tabPages.Add(new ChildTabPageContainerControl<AgendaTabCControl>(this, tabInfo));
						break;
					case ShiftChildTabType.D:
						tabPages.Add(new ChildTabPageContainerControl<AgendaTabDControl>(this, tabInfo));
						break;
					case ShiftChildTabType.E:
						tabPages.Add(new ChildTabPageContainerControl<AgendaTabEControl>(this, tabInfo));
						break;
					case ShiftChildTabType.U:
					case ShiftChildTabType.V:
					case ShiftChildTabType.W:
						tabPages.Add(new ChildTabPageContainerControl<SlidesTabControl>(this, tabInfo));
						break;
					case ShiftChildTabType.X:
					case ShiftChildTabType.Y:
					case ShiftChildTabType.Z:
						tabPages.Add(new ChildTabPageContainerControl<TilesTabControl>(this, tabInfo));
						break;
					default:
						tabPages.Add(new ChildTabPageContainerControl<CommonChildTabControl>(this, tabInfo));
						break;
						//default:
						//	throw new ArgumentOutOfRangeException("Shift tab type is not defined");
				}
			}
			return tabPages;
		}
	}
}
