using System;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Solution
{
	public class SolutionTabInfo : StarChildTabsContainer
	{
		public override StarTopTabType TabType => StarTopTabType.Solution;

		protected override StarChildTabInfo CreatChildTab(String tabId)
		{
			switch (tabId)
			{
				case "a":
					return new SolutionTabAInfo();
				case "b":
					return new SolutionTabBInfo();
				case "c":
					return new SolutionTabCInfo();
				case "d":
					return new SolutionTabDInfo();
				default:
					throw new ArgumentOutOfRangeException("Star tab type is not defined");
			}
		}
	}
}
