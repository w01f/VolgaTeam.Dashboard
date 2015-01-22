using System.Collections.Generic;

namespace CommandCentral.Entities.Media
{
	internal class DaypartRotation
	{
		public DaypartRotation()
		{
			Name = string.Empty;
			Day = string.Empty;
			Time = string.Empty;
			DemoValues = new List<string>();
		}

		public string Name { get; set; }
		public string Day { get; set; }
		public string Time { get; set; }
		public List<string> DemoValues { get; set; }
	}
}