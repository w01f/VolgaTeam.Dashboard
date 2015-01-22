namespace CommandCentral.Entities.Dashboard
{
	public class Quote
	{
		public Quote(string quote, string author)
		{
			Value = quote;
			Author = author;
		}

		public string Value { get; set; }
		public string Author { get; set; }
	}
}