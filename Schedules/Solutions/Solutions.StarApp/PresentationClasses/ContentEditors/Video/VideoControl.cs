using System;
using System.Collections.Generic;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Business.Solutions.StarApp.Enums;
using Asa.Common.Core.Enums;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors.Video
{
	public class VideoControl : MultiTabControl
	{
		public override SlideType SlideType => SlideType.StarAppVideo;

		public VideoControl(BaseStarAppContainer slideContainer, StarChildTabsContainer tabInfo) : base(slideContainer,
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
						tabPages.Add(new ChildTabPageContainerControl<VideoTabAControl>(this, tabInfo));
						break;
					case StarChildTabType.B:
						tabPages.Add(new ChildTabPageContainerControl<VideoTabBControl>(this, tabInfo));
						break;
					case StarChildTabType.C:
						tabPages.Add(new ChildTabPageContainerControl<VideoTabCControl>(this, tabInfo));
						break;
					case StarChildTabType.D:
						tabPages.Add(new ChildTabPageContainerControl<VideoTabDControl>(this, tabInfo));
						break;
					case StarChildTabType.K:
					case StarChildTabType.L:
					case StarChildTabType.M:
					case StarChildTabType.N:
					case StarChildTabType.O:
						tabPages.Add(new ChildTabPageContainerControl<SingleSlidesTabControl>(this, tabInfo));
						break;
					case StarChildTabType.U:
					case StarChildTabType.V:
					case StarChildTabType.W:
						tabPages.Add(new ChildTabPageContainerControl<MultiSlidesTabControl>(this, tabInfo));
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
