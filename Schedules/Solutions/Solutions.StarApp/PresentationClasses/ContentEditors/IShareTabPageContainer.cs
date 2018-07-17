namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public interface IShareTabPageContainer
	{
		ShareControl ParentControl { get; }
		ShareTabBaseControl ContentControl { get; }
		void LoadContent();
	}
}
