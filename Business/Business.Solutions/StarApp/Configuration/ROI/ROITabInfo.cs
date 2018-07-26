using System;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.ROI
{
	public class ROITabInfo : StarChildTabsContainer
	{
		public override StarTopTabType TabType => StarTopTabType.ROI;

		protected override StarChildTabInfo CreatChildTab(String tabId)
		{
			switch (tabId)
			{
				case "a":
					return new ROITabAInfo();
				case "b":
					return new ROITabBInfo();
				case "c":
					return new ROITabCInfo();
				case "d":
					return new ROITabDInfo();
				default:
					throw new ArgumentOutOfRangeException("Star tab type is not defined");
			}
		}
	}
}
