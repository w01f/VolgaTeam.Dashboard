namespace Asa.Business.Dashboard.Dictionaries
{
	public class ListManager
	{
		public static ListManager Instance { get; } = new ListManager();

		public Users UsersList { get; set; }
		public CoverLists CoverLists { get; set; }
		public ClientGoalsLists ClientGoalsLists { get; set; }
		public LeadoffStatementLists LeadoffStatementLists { get; set; }
		public TargetCustomersLists TargetCustomersLists { get; set; }

		private ListManager() { }

		public void Init()
		{
			Common.Dictionaries.ListManager.Instance.Load();

			UsersList = new Users();
			CoverLists = new CoverLists();
			ClientGoalsLists = new ClientGoalsLists();
			LeadoffStatementLists = new LeadoffStatementLists();
			TargetCustomersLists = new TargetCustomersLists();
		}
	}
}
