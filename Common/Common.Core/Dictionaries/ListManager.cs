namespace Asa.Common.Core.Dictionaries
{
	public class ListManager
	{
		public const string DefaultBigLogoFileName = @"Default.png";
		public const string DefaultSmallLogoFileName = @"Default2.png";
		public const string DefaultTinyLogoFileName = @"Default3.png";

		public AdvertisersManager Advertisers { get; }
		public DecisionMakersManager DecisionMakers { get; }
		public SimpleSummaryLists SimpleSummaryLists { get; set; }

		public static ListManager Instance { get; } = new ListManager();

		private ListManager()
		{
			Advertisers = new AdvertisersManager();
			DecisionMakers = new DecisionMakersManager();
			SimpleSummaryLists = new SimpleSummaryLists();
		}

		public void Load()
		{
			Advertisers.Load();
			DecisionMakers.Load();
			SimpleSummaryLists.Load();
		}
	}
}
