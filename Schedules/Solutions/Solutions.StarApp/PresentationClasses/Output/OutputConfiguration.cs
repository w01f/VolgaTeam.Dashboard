namespace Asa.Solutions.StarApp.PresentationClasses.Output
{
	public class OutputConfiguration
	{
		public StarAppOutputType OutputType { get; }
		public string DisplayName { get; }
		public int SlidesCount { get; }
		

		public OutputConfiguration(StarAppOutputType outputType, string displayName, int slidesCount)
		{
			OutputType = outputType;
			DisplayName = displayName;
			SlidesCount = slidesCount;
		}
	}
}
