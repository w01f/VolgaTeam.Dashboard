using Asa.Business.Solutions.Shift.Configuration.Approach;
using DevComponents.DotNetBar;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Approach.TabA
{
	public class ItemButton : ButtonX
	{
		public ApproachItemInfo ItemInfo { get; }
		public int RowOrder { get; set; }
		public int ColumnOrder { get; set; }

		public ItemButton(ApproachItemInfo itemInfo)
		{
			ItemInfo = itemInfo;
		}
	}
}
