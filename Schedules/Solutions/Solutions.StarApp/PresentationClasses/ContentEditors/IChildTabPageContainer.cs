using Asa.Business.Solutions.StarApp.Configuration;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public interface IChildTabPageContainer
	{
		StarChildTabInfo TabInfo { get; }
		bool OutputEnabled { get; }
		MultiTabControl ParentControl { get; }
		ChildTabBaseControl ContentControl { get; }
		void LoadContent();
		void FormatSlideHeader();
	}
}
