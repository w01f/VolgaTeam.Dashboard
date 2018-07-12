using Asa.Common.GUI.OutputSelector;

namespace Asa.Solutions.Shift.PresentationClasses.Output
{
	public class OutputConfiguration : ISlideItem
	{
		public ShiftOutputType OutputType { get; }
		public string DisplayName { get; }
		public int SlidesCount { get; }
		public bool SelectedForOutput { get; set; } = true;
		public bool IsCurrent { get; set; }

		public ISlideItem[] SlideItems
		{
			get => new ISlideItem[] { };
			set { }
		}

		public OutputConfiguration(ShiftOutputType outputType, string displayName, int slidesCount, bool isCurrent = false)
		{
			OutputType = outputType;
			DisplayName = displayName;
			SlidesCount = slidesCount;
			IsCurrent = isCurrent;
		}
	}
}
