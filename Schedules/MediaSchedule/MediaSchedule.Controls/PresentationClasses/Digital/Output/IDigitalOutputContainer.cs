using System.Collections.Generic;
using Asa.Online.Controls.PresentationClasses.Products;

namespace Asa.Media.Controls.PresentationClasses.Digital.Output
{
	public interface IDigitalOutputContainer
	{
		IList<IDigitalOutputItem> GetOutputItems();
	}
}
