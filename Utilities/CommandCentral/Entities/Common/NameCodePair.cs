namespace CommandCentral.Entities.Common
{
	internal class NameCodePair
	{
		public NameCodePair()
		{
			Name = string.Empty;
			Code = string.Empty;
		}

		public string Name { get; set; }
		public string Code { get; set; }
		public int Index { get; set; }
	}
}