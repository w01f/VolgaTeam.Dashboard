using System.Collections.Generic;

namespace Asa.Business.Solutions.Dashboard.Entities.NonPersistent
{
	public class TargetCustomersState : DasboardDataState
	{
		public string SlideHeader { get; set; }
		public List<string> Demo { get; set; }
		public List<string> Income { get; set; }
		public List<string> Geographic { get; set; }

		public TargetCustomersState()
		{
			Demo = new List<string>();
			Income = new List<string>();
			Geographic = new List<string>();
		}
	}
}
