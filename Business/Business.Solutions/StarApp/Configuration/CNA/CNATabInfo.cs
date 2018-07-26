using System;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.CNA
{
	public class CNATabInfo : StarChildTabsContainer
	{
		public override StarTopTabType TabType => StarTopTabType.CNA;

		protected override StarChildTabInfo CreatChildTab(String tabId)
		{
			switch (tabId)
			{
				case "a":
					return new CNATabAInfo();
				case "b":
					return new CNATabBInfo();
				default:
					throw new ArgumentOutOfRangeException("Star tab type is not defined");
			}
		}
	}
}
