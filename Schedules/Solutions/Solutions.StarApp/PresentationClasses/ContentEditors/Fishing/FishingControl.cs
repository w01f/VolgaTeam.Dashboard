using System;
using System.Collections.Generic;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Enums;
using Asa.Common.Core.Enums;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Fishing
{
	public class FishingControl : MultiTabControl
	{
		public override SlideType SlideType => SlideType.StarAppFishing;

		public FishingControl(BaseStarAppContainer slideContainer, StarChildTabsContainer tabInfo) : base(slideContainer,
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
						tabPages.Add(new ChildTabPageContainerControl<FishingTabAControl>(this, tabInfo));
						break;
					case StarChildTabType.B:
						tabPages.Add(new ChildTabPageContainerControl<FishingTabBControl>(this, tabInfo));
						break;
					case StarChildTabType.C:
						tabPages.Add(new ChildTabPageContainerControl<FishingTabCControl>(this, tabInfo));
						break;
					case StarChildTabType.U:
					case StarChildTabType.V:
					case StarChildTabType.W:
						tabPages.Add(new ChildTabPageContainerControl<SlidesTabControl>(this, tabInfo));
						break;
					case StarChildTabType.X:
					case StarChildTabType.Y:
					case StarChildTabType.Z:
						tabPages.Add(new ChildTabPageContainerControl<TilesTabControl>(this, tabInfo));
						break;
					default:
						throw new ArgumentOutOfRangeException("Star tab type is not defined");
				}
			}
			return tabPages;
		}
	}
}
