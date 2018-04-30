using Asa.Common.GUI.OutputSelector;
using Asa.Common.GUI.Preview;

namespace Asa.Online.Controls.PresentationClasses.Products
{
	public interface IDigitalOutputItem : ISlideItem
	{
		void GenerateOutput();
		PreviewGroup GeneratePreview();
	}
}
