namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public interface IClosersTabPageContainer
	{
		ClosersControl ParentControl { get; }
		ClosersTabBaseControl ContentControl { get; }
		void LoadContent();
	}
}
