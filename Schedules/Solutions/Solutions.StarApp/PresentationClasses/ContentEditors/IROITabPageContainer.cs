namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public interface IROITabPageContainer
	{
		ROIControl ParentControl { get; }
		ROITabBaseControl ContentControl { get; }
		void LoadContent();
	}
}
