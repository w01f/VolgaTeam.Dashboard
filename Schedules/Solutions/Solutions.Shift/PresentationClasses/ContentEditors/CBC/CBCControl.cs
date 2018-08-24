﻿using System.Collections.Generic;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.CBC
{
	class CBCControl : MultiTabControl
	{
		public CBCControl(BaseShiftContainer slideContainer, ShiftChildTabsContainer tabInfo) : base(slideContainer, tabInfo) { }

		protected override IList<IChildTabPageContainer> GetChildTabPages()
		{
			var tabPages = new List<IChildTabPageContainer>();
			foreach (var tabInfo in TabContainerInfo.ChildTabs)
			{
				switch (tabInfo.TabType)
				{
					case ShiftChildTabType.A:
						tabPages.Add(new ChildTabPageContainerControl<CBCTabAControl>(this, tabInfo));
						break;
					case ShiftChildTabType.B:
						tabPages.Add(new ChildTabPageContainerControl<CBCTabBControl>(this, tabInfo));
						break;
					case ShiftChildTabType.C:
						tabPages.Add(new ChildTabPageContainerControl<CBCTabCControl>(this, tabInfo));
						break;
					case ShiftChildTabType.D:
						tabPages.Add(new ChildTabPageContainerControl<CBCTabDControl>(this, tabInfo));
						break;
					case ShiftChildTabType.E:
						tabPages.Add(new ChildTabPageContainerControl<CBCTabEControl>(this, tabInfo));
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
