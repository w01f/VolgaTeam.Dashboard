using System.Collections.Generic;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Intro
{
	class IntroControl : MultiTabControl
	{
		public IntroControl(BaseShiftContainer slideContainer, ShiftChildTabsContainer tabInfo) : base(slideContainer, tabInfo) { }

		protected override IList<IChildTabPageContainer> GetChildTabPages()
		{
			var tabPages = new List<IChildTabPageContainer>();
			foreach (var tabInfo in TabContainerInfo.ChildTabs)
			{
				switch (tabInfo.TabType)
				{
					case ShiftChildTabType.A:
						tabPages.Add(new ChildTabPageContainerControl<IntroTabAControl>(this, tabInfo));
						break;
					case ShiftChildTabType.B:
						tabPages.Add(new ChildTabPageContainerControl<IntroTabBControl>(this, tabInfo));
						break;
					case ShiftChildTabType.C:
						tabPages.Add(new ChildTabPageContainerControl<IntroTabCControl>(this, tabInfo));
						break;
					case ShiftChildTabType.D:
						tabPages.Add(new ChildTabPageContainerControl<IntroTabDControl>(this, tabInfo));
						break;
					case ShiftChildTabType.U:
					case ShiftChildTabType.V:
					case ShiftChildTabType.W:
						tabPages.Add(new ChildTabPageContainerControl<SlidesTabControl>(this, tabInfo));
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
