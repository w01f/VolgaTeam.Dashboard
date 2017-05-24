namespace Asa.Business.Solutions.StarApp.Entities.NonPersistent
{
	public class CoverState
	{
		public string SlideHeader { get; set; }
		public bool AddAsPageOne { get; set; }

		public CoverState()
		{
			AddAsPageOne = true;
		}
	}
}
