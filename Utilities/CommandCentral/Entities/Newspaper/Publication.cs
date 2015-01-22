namespace CommandCentral.Entities.Newspaper
{
	internal class Publication
	{
		public Publication()
		{
			Name = string.Empty;
			SortOrder = string.Empty;
			Abbreviation = string.Empty;
			BigLogo = string.Empty;
			LittleLogo = string.Empty;
			TinyLogo = string.Empty;
			DailyCirculation = string.Empty;
			DailyReadership = string.Empty;
			SundayCirculation = string.Empty;
			SundayReadership = string.Empty;
		}

		public string Name { get; set; }
		public string SortOrder { get; set; }
		public string Abbreviation { get; set; }
		public string BigLogo { get; set; }
		public string LittleLogo { get; set; }
		public string TinyLogo { get; set; }
		public string DailyCirculation { get; set; }
		public string DailyReadership { get; set; }
		public string SundayCirculation { get; set; }
		public string SundayReadership { get; set; }

		public bool AllowSundaySelect { get; set; }
		public bool AllowMondaySelect { get; set; }
		public bool AllowTuesdaySelect { get; set; }
		public bool AllowWednesdaySelect { get; set; }
		public bool AllowThursdaySelect { get; set; }
		public bool AllowFridaySelect { get; set; }
		public bool AllowSaturdaySelect { get; set; }
	}
}