namespace CommandCentral.BusinessClasses.Entities.Common
{
	internal class NameCodePair
	{
		public string Name { get; set; }
		public string Code { get; set; }
		public int Index { get; set; }

		public NameCodePair()
		{
			Name = string.Empty;
			Code = string.Empty;
		}
	}
}