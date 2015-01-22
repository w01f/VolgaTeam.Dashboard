namespace CommandCentral.Entities.Dashboard
{
	public class Step
	{
		public Step()
		{
			Value = string.Empty;
			Position = -1;
		}

		public string Value { get; set; }
		public int Position { get; set; }
	}
}