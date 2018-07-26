using System;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Customer
{
	public class CustomerTabInfo : StarChildTabsContainer
	{
		public override StarTopTabType TabType => StarTopTabType.Customer;

		protected override StarChildTabInfo CreatChildTab(String tabId)
		{
			switch (tabId)
			{
				case "a":
					return new CustomerTabAInfo();
				case "b":
					return new CustomerTabBInfo();
				case "c":
					return new CustomerTabCInfo();
				default:
					throw new ArgumentOutOfRangeException("Star tab type is not defined");
			}
		}
	}
}
