using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Solutions.Common.Entities.NonPersistent;

namespace Asa.Business.Solutions.StarApp.Entities.NonPersistent
{
	public abstract class StarAppContent : BaseSolutionContent
	{
		public abstract BaseScheduleSettings ScheduleSettings { get; }

		public CoverState CoverState { get; private set; }
		public CNAState CNAState { get; private set; }
		public FishingState FishingState { get; private set; }
		public CustomerState CustomerState { get; private set; }
		public ShareState ShareState { get; private set; }
		public ROIState ROIState { get; private set; }
		public MarketState MarketState { get; private set; }
		public VideoState VideoState { get; private set; }
		public AudienceState AudienceState { get; private set; }
		public SolutionState SolutionState { get; private set; }
		public ClosersState ClosersState { get; private set; }

		protected StarAppContent()
		{
			CoverState = new CoverState();
			CNAState = new CNAState();
			FishingState = new FishingState();
			CustomerState = new CustomerState();
			ShareState = new ShareState();
			ROIState = new ROIState();
			MarketState = new MarketState();
			VideoState = new VideoState();
			AudienceState = new AudienceState();
			SolutionState = new SolutionState();
			ClosersState = new ClosersState();
		}

		protected override void AfterCreate()
		{
			base.AfterCreate();
		}
	}
}
