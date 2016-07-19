namespace Asa.Business.Solutions.Dashboard.Entities.NonPersistent
{
	public class LeadoffStatementState : DasboardDataState
	{
		public bool ShowStatement1 { get; set; }
		public bool ShowStatement2 { get; set; }
		public bool ShowStatement3 { get; set; }
		public string SlideHeader { get; set; }
		public string Statement1 { get; set; }
		public string Statement2 { get; set; }
		public string Statement3 { get; set; }

		public LeadoffStatementState()
		{
			ShowStatement1 = true;
		}
	}
}
