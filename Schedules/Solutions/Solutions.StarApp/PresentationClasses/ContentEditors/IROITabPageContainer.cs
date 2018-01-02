namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	interface IROITabPageContainer
	{
		ROITabBaseControl ContentControl { get; }
		void LoadContent();
	}
}
