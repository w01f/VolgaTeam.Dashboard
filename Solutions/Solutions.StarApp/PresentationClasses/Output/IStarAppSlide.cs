using Asa.Common.GUI.Preview;

namespace Asa.Solutions.StarApp.PresentationClasses.Output
{
	public interface IStarAppSlide
	{
		string SlideName { get; }
		void GenerateOutput();
		PreviewGroup GeneratePreview();
	}
}
