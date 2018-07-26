using System;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Video
{
	public class VideoTabInfo : StarChildTabsContainer
	{
		public override StarTopTabType TabType => StarTopTabType.Video;

		protected override StarChildTabInfo CreatChildTab(String tabId)
		{
			switch (tabId)
			{
				case "a":
					return new VideoTabAInfo();
				case "b":
					return new VideoTabBInfo();
				case "c":
					return new VideoTabCInfo();
				case "d":
					return new VideoTabDInfo();
				default:
					throw new ArgumentOutOfRangeException("Star tab type is not defined");
			}
		}
	}
}
