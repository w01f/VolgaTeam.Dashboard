using Asa.Common.Core.Enums;
using Asa.Common.GUI.Preview;

namespace Asa.Solutions.StarApp.PresentationClasses.Output
{
	public interface IStarAppSlideContainer
	{
		bool ReadyForOutput { get; }
		SlideType SlideType { get; }
		OutputGroup GetOutputGroup();
	}
}
