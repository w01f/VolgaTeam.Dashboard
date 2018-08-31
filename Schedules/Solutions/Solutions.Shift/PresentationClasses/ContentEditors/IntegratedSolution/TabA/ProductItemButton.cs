using Asa.Business.Solutions.Shift.Configuration.IntegratedSolution;
using DevComponents.DotNetBar;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution.TabA
{
	public class ProductItemButton : ButtonX
	{
		public ProductInfo ItemInfo { get; }
		public int RowOrder { get; set; }
		public int ColumnOrder { get; set; }

		public ProductItemButton(ProductInfo itemInfo)
		{
			ItemInfo = itemInfo;
		}
	}
}
