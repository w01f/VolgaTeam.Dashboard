﻿using System.Collections.Generic;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Investment
{
	class InvestmentControl : MultiTabControl
	{
		public InvestmentControl(BaseShiftContainer slideContainer, ShiftChildTabsContainer tabInfo) : base(slideContainer, tabInfo) { }

		protected override IList<IChildTabPageContainer> GetChildTabPages()
		{
			var tabPages = new List<IChildTabPageContainer>();
			foreach (var tabInfo in TabContainerInfo.ChildTabs)
			{
				switch (tabInfo.TabType)
				{
					case ShiftChildTabType.A:
						tabPages.Add(new ChildTabPageContainerControl<InvestmentTabAControl>(this, tabInfo));
						break;
					case ShiftChildTabType.B:
						tabPages.Add(new ChildTabPageContainerControl<InvestmentTabBControl>(this, tabInfo));
						break;
					case ShiftChildTabType.C:
						tabPages.Add(new ChildTabPageContainerControl<InvestmentTabCControl>(this, tabInfo));
						break;
					case ShiftChildTabType.D:
						tabPages.Add(new ChildTabPageContainerControl<InvestmentTabDControl>(this, tabInfo));
						break;
					case ShiftChildTabType.E:
						tabPages.Add(new ChildTabPageContainerControl<InvestmentTabEControl>(this, tabInfo));
						break;
					case ShiftChildTabType.F:
						tabPages.Add(new ChildTabPageContainerControl<InvestmentTabFControl>(this, tabInfo));
						break;
					case ShiftChildTabType.K:
					case ShiftChildTabType.L:
					case ShiftChildTabType.M:
					case ShiftChildTabType.N:
					case ShiftChildTabType.O:
						tabPages.Add(new ChildTabPageContainerControl<SingleSlidesTabControl>(this, tabInfo));
						break;
					case ShiftChildTabType.U:
					case ShiftChildTabType.V:
					case ShiftChildTabType.W:
						tabPages.Add(new ChildTabPageContainerControl<MultiSlidesTabControl>(this, tabInfo));
						break;
					case ShiftChildTabType.X:
					case ShiftChildTabType.Y:
					case ShiftChildTabType.Z:
						tabPages.Add(new ChildTabPageContainerControl<TilesTabControl>(this, tabInfo));
						break;
					default:
						tabPages.Add(new ChildTabPageContainerControl<CommonChildTabControl>(this, tabInfo));
						break;
				}
			}
			return tabPages;
		}
	}
}
