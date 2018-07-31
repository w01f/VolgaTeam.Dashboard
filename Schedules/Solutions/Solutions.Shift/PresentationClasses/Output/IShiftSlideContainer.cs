using Asa.Common.GUI.Preview;

namespace Asa.Solutions.Shift.PresentationClasses.Output
{
	public interface IShiftSlideContainer
	{
		bool ReadyForOutput { get; }
		OutputGroup GetOutputGroup();
	}
}
