using System;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Audience
{
	public class AudienceTabInfo : StarChildTabsContainer
	{
		public override StarTopTabType TabType => StarTopTabType.Audience;

		protected override StarChildTabInfo CreatChildTab(String tabId)
		{
			switch (tabId)
			{
				case "a":
					return new AudienceTabAInfo();
				case "b":
					return new AudienceTabBInfo();
				case "c":
					return new AudienceTabCInfo();
				default:
					throw new ArgumentOutOfRangeException("Star tab type is not defined");
			}
		}
	}
}
