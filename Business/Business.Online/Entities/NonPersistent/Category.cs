using System.Drawing;

namespace Asa.Business.Online.Entities.NonPersistent
{
	public class Category
	{
		public string Name { get; set; }
		public Image Logo { get; set; }
		public string TooltipTitle { get; set; }
		public string TooltipValue { get; set; }
	}
}
