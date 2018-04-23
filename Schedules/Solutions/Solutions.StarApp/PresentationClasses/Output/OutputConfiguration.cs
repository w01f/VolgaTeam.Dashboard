namespace Asa.Solutions.StarApp.PresentationClasses.Output
{
	public class OutputConfiguration
	{
		public StarAppOutputType OutputType { get; }
		public string DisplayName { get; }
		public int SlidesCount { get; }
		public bool IsCurrent { get; set; }

		public OutputConfiguration(StarAppOutputType outputType, string displayName, int slidesCount, bool isCurrent = false)
		{
			OutputType = outputType;
			DisplayName = displayName;
			SlidesCount = slidesCount;
			IsCurrent = isCurrent;
		}
	}
}
