using System;

namespace Asa.Business.Online.Entities.NonPersistent
{
	public class ProductSource
	{
		public string Name { get; set; }
		public Category Category { get; set; }
		public string SubCategory { get; set; }
		public string RateType { get; set; }
		public Decimal? Rate { get; set; }
		public int? Width { get; set; }
		public int? Height { get; set; }
		public bool EnableLocation { get; set; }
		public bool EnableTarget { get; set; }
		public bool EnableRichMedia { get; set; }
		public bool EnableRate { get; set; }
		public string Overview { get; set; }
		public string DefaultWebsite { get; set; }

		public ProductSource()
		{
			Name = string.Empty;
			SubCategory = string.Empty;
			Overview = string.Empty;
			RateType = "CPM";
			EnableLocation = true;
			EnableTarget = true;
			EnableRichMedia = true;
			EnableRate = true;
		}
	}
}
