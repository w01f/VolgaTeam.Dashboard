using Asa.Business.Solutions.Shift.Configuration;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors
{
	public interface IChildTabPageContainer
	{
		ShiftChildTabInfo TabInfo { get; }
		bool OutputEnabled { get; }
		MultiTabControl ParentControl { get; }
		ChildTabBaseControl ContentControl { get; }
		void LoadContent();
		void FormatSlideHeader();
	}
}
