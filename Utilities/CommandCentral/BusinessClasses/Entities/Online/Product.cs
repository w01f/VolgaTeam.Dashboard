namespace CommandCentral.BusinessClasses.Entities.Online
{
	internal class Product
	{
		public string Name { get; set; }
		public string RateType { get; set; }
		public string Rate { get; set; }
		public string Width { get; set; }
		public string Height { get; set; }
		public string Overview { get; set; }
		public string DefaultWebsite { get; set; }
		public Category Category { get; set; }
		public string SubCategory { get; set; }
		public bool EnableLocation { get; set; }
		public bool EnableTarget { get; set; }
		public bool EnableRichMedia { get; set; }
		public bool EnableRate { get; set; }

		public Product()
		{
			Name = string.Empty;
			RateType = string.Empty;
			Rate = string.Empty;
			Width = string.Empty;
			Height = string.Empty;
			Overview = string.Empty;
			SubCategory = string.Empty;
			EnableLocation = true;
			EnableTarget = true;
			EnableRichMedia = true;
			EnableRate = true;
		}
	}
}