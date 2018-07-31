using System.Collections.Generic;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Enums;
using Asa.Common.Core.Enums;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors
{
	class CommonTopTabControl : MultiTabControl
	{
		public CommonTopTabControl(BaseShiftContainer slideContainer, ShiftChildTabsContainer tabInfo) : base(slideContainer,
			tabInfo)
		{
		}

		protected override IList<IChildTabPageContainer> GetChildTabPages()
		{
			var tabPages = new List<IChildTabPageContainer>();
			foreach (var tabInfo in TabContainerInfo.ChildTabs)
				switch (tabInfo.TabType)
				{
					case ShiftChildTabType.U:
					case ShiftChildTabType.V:
					case ShiftChildTabType.W:
						tabPages.Add(new ChildTabPageContainerControl<SlidesTabControl>(this, tabInfo));
						break;
					default:
						tabPages.Add(new ChildTabPageContainerControl<CommonChildTabControl>(this, tabInfo));
						break;
				}

			return tabPages;
		}
	}
}
