namespace CommandCentral.Entities.Newspaper
{
	internal class ShareUnit
	{
		public ShareUnit()
		{
			RateCard = string.Empty;
			PercentOfPage = string.Empty;
			Width = string.Empty;
			WidthMeasureUnit = string.Empty;
			Height = string.Empty;
			HeightMeasureUnit = string.Empty;
		}

		public string RateCard { get; set; }
		public string PercentOfPage { get; set; }
		public string Width { get; set; }
		public string WidthMeasureUnit { get; set; }
		public string Height { get; set; }
		public string HeightMeasureUnit { get; set; }
	}
}