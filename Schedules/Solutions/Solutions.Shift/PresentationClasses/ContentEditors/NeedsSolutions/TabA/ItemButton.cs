using Asa.Business.Solutions.Shift.Configuration.NeedsSolutions;
using DevComponents.DotNetBar;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.NeedsSolutions.TabA
{
	public class ItemButton : ButtonX
	{
		public NeedsItemInfo ItemInfo { get; }
		public int RowOrder { get; set; }
		public int ColumnOrder { get; set; }

		public ItemButton(NeedsItemInfo itemInfo)
		{
			ItemInfo = itemInfo;
		}
	}
}
