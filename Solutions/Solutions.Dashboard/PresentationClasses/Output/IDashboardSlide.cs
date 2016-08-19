using Asa.Common.Core.Enums;
using Asa.Common.GUI.Preview;

namespace Asa.Solutions.Dashboard.PresentationClasses.Output
{
	public interface IDashboardSlide
	{
		string SlideName { get; }
		void GenerateOutput();
		PreviewGroup GeneratePreview();
	}
}
