namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	interface IStarAppTabPageContainer
	{
		StarAppControl ContentControl { get; }
		void LoadContent();
	}
}
