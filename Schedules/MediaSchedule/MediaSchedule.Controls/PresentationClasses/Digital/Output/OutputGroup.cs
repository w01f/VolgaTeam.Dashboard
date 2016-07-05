using System.Collections.Generic;
using Asa.Online.Controls.PresentationClasses.Products;

namespace Asa.Media.Controls.PresentationClasses.Digital.Output
{
	public class OutputGroup
	{
		public string Name { get; set; }
		public bool AlwaysShowChildren { get; set; }
		public IList<IDigitalOutputItem> OutputItems { get; set; }
	}
}
