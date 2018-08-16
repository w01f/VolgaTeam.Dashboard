using Asa.Business.Solutions.Shift.Configuration.NeedsSolutions;
using DevComponents.DotNetBar;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.NeedsSolutions.TabC
{
	public class ItemButton : ButtonX
	{
		public SolutionsItemInfo ItemInfo { get; }
		public int RowOrder { get; set; }
		public int ColumnOrder { get; set; }

		public ItemButton(SolutionsItemInfo itemInfo)
		{
			ItemInfo = itemInfo;
		}
	}
}
