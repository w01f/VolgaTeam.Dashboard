using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Solutions.Common.Entities.NonPersistent;

namespace Asa.Business.Solutions.Dashboard.Entities.NonPersistent
{
	public abstract class DashboardContent : BaseSolutionContent
	{
		public abstract BaseScheduleSettings ScheduleSettings { get; }

		public CoverState CoverState { get; private set; }
		public LeadoffStatementState LeadoffStatementState { get; private set; }
		public ClientGoalsState ClientGoalsState { get; private set; }
		public TargetCustomersState TargetCustomersState { get; private set; }
		public SimpleSummaryState SimpleSummaryState { get; private set; }

		public DashboardContent()
		{
			CoverState = new CoverState();
			LeadoffStatementState = new LeadoffStatementState();
			ClientGoalsState = new ClientGoalsState();
			TargetCustomersState = new TargetCustomersState();
			SimpleSummaryState = new SimpleSummaryState();
		}

		protected override void AfterCreate()
		{
			base.AfterCreate();
			SimpleSummaryState.AfterCreate();
		}
	}
}
