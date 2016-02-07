namespace Asa.Business.Common.Dictionaries
{
	public class ListManager
	{
		private static readonly ListManager _instance = new ListManager();
		public const string DefaultBigLogoFileName = @"Default.png";
		public const string DefaultSmallLogoFileName = @"Default2.png";
		public const string DefaultTinyLogoFileName = @"Default3.png";

		public AdvertisersManager Advertisers { get; private set; }
		public DecisionMakersManager DecisionMakers { get; private set; }
		public SimpleSummaryLists SimpleSummaryLists { get; set; }

		public static ListManager Instance
		{
			get { return _instance; }
		}

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
