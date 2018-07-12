namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors
{
	interface IShiftTabPageContainer
	{
		BaseShiftControl ContentControl { get; }
		void LoadContent();
	}
}
