namespace Asa.Common.GUI.OutputSelector
{
	public interface IOutputItem
	{
		string DisplayName { get; }
		bool IsCurrent { get; }
		ISlideItem[] SlideItems { get; set; }
	}
}
