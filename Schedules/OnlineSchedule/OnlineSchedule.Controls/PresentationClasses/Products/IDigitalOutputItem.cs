using Asa.Common.GUI.Preview;

namespace Asa.Online.Controls.PresentationClasses.Products
{
	public interface IDigitalOutputItem
	{
		string SlideName { get; }
		void GenerateOutput();
		PreviewGroup GeneratePreview();
	}
}
