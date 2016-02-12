namespace Asa.Business.Dashboard.Entities.NonPersistent
{
	public class ViewSettingsManager
	{
		public static ViewSettingsManager Instance { get; } = new ViewSettingsManager();

		public CoverState CoverState { get; set; }
		public LeadoffStatementState LeadoffStatementState { get; set; }
		public ClientGoalsState ClientGoalsState { get; set; }
		public TargetCustomersState TargetCustomersState { get; set; }
		public SimpleSummaryState SimpleSummaryState { get; set; }

		private ViewSettingsManager()
		{
			CoverState = new CoverState();
			LeadoffStatementState = new LeadoffStatementState();
			ClientGoalsState = new ClientGoalsState();
			TargetCustomersState = new TargetCustomersState();
			SimpleSummaryState = new SimpleSummaryState();
		}
	}
}
