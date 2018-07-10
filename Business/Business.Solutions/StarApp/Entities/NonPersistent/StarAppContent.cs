using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Common.Entities.NonPersistent.ScheduleResources;
using Asa.Business.Solutions.Common.Entities.NonPersistent;

namespace Asa.Business.Solutions.StarApp.Entities.NonPersistent
{
	public abstract class StarAppContent : BaseSolutionContent
	{
		public abstract BaseScheduleSettings ScheduleSettings { get; }
		public abstract BaseScheduleResourceContainer ScheduleResources { get; }

		public CoverState CoverState { get; }
		public CNAState CNAState { get; }
		public FishingState FishingState { get; }
		public CustomerState CustomerState { get; }
		public ShareState ShareState { get; }
		public ROIState ROIState { get; }
		public MarketState MarketState { get; }
		public VideoState VideoState { get; }
		public AudienceState AudienceState { get; }
		public SolutionState SolutionState { get; }
		public ClosersState ClosersState { get; }

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
