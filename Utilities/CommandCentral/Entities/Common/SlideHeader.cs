namespace CommandCentral.Entities.Common
{
	public class SlideHeader
	{
		public SlideHeader()
		{
			Value = string.Empty;
			IsDefault = false;
		}

		public string Value { get; set; }
		public bool IsDefault { get; set; }
	}
}