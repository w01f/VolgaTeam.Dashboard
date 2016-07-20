namespace Asa.Business.Solutions.Dashboard.Entities.NonPersistent
{
	public class ClientGoalsState
	{
		public string SlideHeader { get; set; }
		public string Goal1 { get; set; }
		public string Goal2 { get; set; }
		public string Goal3 { get; set; }
		public string Goal4 { get; set; }
		public string Goal5 { get; set; }

		public ClientGoalsState()
		{
			SlideHeader = string.Empty;
		}
	}
}
