using System.Collections.Generic;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public abstract class StarTabWithHeaderInfo : StarChildTabInfo
	{
		public override bool IsRegularChildTab => true;
		public List<ListDataItem> HeadersItems { get; set; }

		protected StarTabWithHeaderInfo(StarTopTabType topTabType) : base(topTabType)
		{
			HeadersItems = new List<ListDataItem>();
		}
	}
}
