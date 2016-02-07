using Asa.Common.GUI.Preview;

namespace Asa.Online.Controls.PresentationClasses.Products
{
	public interface IDigitalSlideControl
	{
		string SlideName { get; }
		PreviewGroup GetPreviewGroup();
		void Output();
		void Release();
	}
}
