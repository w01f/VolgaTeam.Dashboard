using System.Collections.Generic;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration
{
	public abstract class ShiftTabWithHeaderInfo : ShiftChildTabInfo
	{
		public override bool IsRegularChildTab => true;

		public List<ListDataItem> HeadersItems { get; set; }
		public TextEditorConfiguration HeadersEditorConfiguration { get; set; }

		protected ShiftTabWithHeaderInfo(ShiftChildTabType tabType, ShiftTopTabType topTabInfo) : base(tabType, topTabInfo)
		{
			HeadersItems = new List<ListDataItem>();
			HeadersEditorConfiguration = TextEditorConfiguration.Empty();
		}
	}
}
