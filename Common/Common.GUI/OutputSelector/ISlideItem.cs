namespace Asa.Common.GUI.OutputSelector
{
	public interface ISlideItem : IOutputItem
	{
		int SlidesCount { get; }
		bool SelectedForOutput { get; set; }
	}
}
