namespace CommandCentral.Entities.Newspaper
{
	internal class Section
	{
		public Section()
		{
			Name = string.Empty;
			Abbreviation = string.Empty;
		}

		public string Name { get; set; }
		public string Abbreviation { get; set; }
	}
}