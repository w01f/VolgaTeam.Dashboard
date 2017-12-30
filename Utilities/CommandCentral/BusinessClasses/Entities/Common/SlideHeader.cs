namespace CommandCentral.BusinessClasses.Entities.Common
{
	public class SlideHeader
	{
		public string Value { get; set; }
		public bool IsDefault { get; set; }

		public SlideHeader()
		{
			Value = string.Empty;
			IsDefault = false;
		}
	}
}