namespace CommandCentral.BusinessClasses.Entities.Common
{
	public class ListDataItem
	{
		public string Value { get; set; }
		public bool IsDefault { get; set; }
		public int DefaultOrder { get; set; }

		public ListDataItem()
		{
			Value = string.Empty;
			IsDefault = false;
		}
	}
}