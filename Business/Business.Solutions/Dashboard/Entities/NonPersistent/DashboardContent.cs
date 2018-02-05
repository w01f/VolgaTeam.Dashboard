using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Solutions.Common.Entities.NonPersistent;

namespace Asa.Business.Solutions.Dashboard.Entities.NonPersistent
{
	public abstract class DashboardContent : BaseSolutionContent
	{
		public abstract BaseScheduleSettings ScheduleSettings { get; }

		public CoverState CoverState { get; }
		public LeadoffStatementState LeadoffStatementState { get; }
		public ClientGoalsState ClientGoalsState { get; }
		public TargetCustomersState TargetCustomersState { get; }
		public SimpleSummaryState SimpleSummaryState { get; }

		protected DashboardContent()
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
