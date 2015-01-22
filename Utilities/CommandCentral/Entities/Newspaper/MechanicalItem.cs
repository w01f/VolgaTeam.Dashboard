namespace CommandCentral.Entities.Newspaper
{
	internal class MechanicalItem
	{
		public MechanicalItem()
		{
			Name = string.Empty;
			Value = string.Empty;
		}

		public string Name { get; set; }
		public string Value { get; set; }
	}
}