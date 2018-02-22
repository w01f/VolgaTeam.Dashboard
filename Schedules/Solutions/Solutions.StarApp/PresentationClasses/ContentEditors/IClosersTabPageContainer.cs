namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	interface IClosersTabPageContainer
	{
		ClosersTabBaseControl ContentControl { get; }
		void LoadContent();
	}
}
