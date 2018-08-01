using System.Collections.Generic;
using Asa.Business.Solutions.Common.Configuration;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public abstract class StarTabWithHeaderInfo : StarChildTabInfo
	{
		public override bool IsRegularChildTab => true;
		public List<ListDataItem> HeadersItems { get; set; }

		protected StarTabWithHeaderInfo()
		{
			HeadersItems = new List<ListDataItem>();
		}
	}
}
