using System;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Share
{
	public class ShareTabInfo : StarChildTabsContainer
	{
		public override StarTopTabType TabType => StarTopTabType.Share;

		protected override StarChildTabInfo CreatChildTab(String tabId)
		{
			switch (tabId)
			{
				case "a":
					return new ShareTabAInfo();
				case "b":
					return new ShareTabBInfo();
				case "c":
					return new ShareTabCInfo();
				case "d":
					return new ShareTabDInfo();
				case "e":
					return new ShareTabEInfo();
				default:
					throw new ArgumentOutOfRangeException("Star tab type is not defined");
			}
		}
	}
}
