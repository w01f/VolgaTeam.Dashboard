namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors
{
	public interface IChildTabPageContainer
	{
		MultiTabControl ParentControl { get; }
		ChildTabBaseControl ContentControl { get; }
		void LoadContent();
	}
}
