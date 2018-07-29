using System;
using System.Collections.Generic;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Enums;
using Asa.Common.Core.Enums;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Customer
{
	public class CustomerControl : MultiTabControl
	{
		public override SlideType SlideType => SlideType.StarAppCustomer;

		public CustomerControl(BaseStarAppContainer slideContainer, StarChildTabsContainer tabInfo) : base(slideContainer,
			tabInfo)
		{
		}

		protected override IList<IChildTabPageContainer> GetChildTabPages()
		{
			var tabPages = new List<IChildTabPageContainer>();
			foreach (var tabInfo in TabContainerInfo.ChildTabs)
			{
				switch (tabInfo.TabType)
				{
					case StarChildTabType.A:
						tabPages.Add(new ChildTabPageContainerControl<CustomerTabAControl>(this, tabInfo));
						break;
					case StarChildTabType.B:
						tabPages.Add(new ChildTabPageContainerControl<CustomerTabBControl>(this, tabInfo));
						break;
					case StarChildTabType.C:
						tabPages.Add(new ChildTabPageContainerControl<CustomerTabCControl>(this, tabInfo));
						break;
					case StarChildTabType.U:
					case StarChildTabType.V:
					case StarChildTabType.W:
						tabPages.Add(new ChildTabPageContainerControl<SlidesTabControl>(this, tabInfo));
						break;
					default:
						throw new ArgumentOutOfRangeException("Star tab type is not defined");
				}
			}
			return tabPages;
		}
	}
}
