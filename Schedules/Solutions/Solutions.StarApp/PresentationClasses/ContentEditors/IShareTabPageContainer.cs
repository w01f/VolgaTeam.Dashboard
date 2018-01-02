namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	interface IShareTabPageContainer
	{
		ShareTabBaseControl ContentControl { get; }
		void LoadContent();
	}
}
