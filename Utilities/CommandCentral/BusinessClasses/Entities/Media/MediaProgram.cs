using System.Collections.Generic;
using Asa.Business.Media.Entities.NonPersistent.Schedule;

namespace CommandCentral.BusinessClasses.Entities.Media
{
	class MediaProgram
	{
		public string Name { get; set; }
		public string Station { get; set; }
		public string Daypart { get; set; }
		public string Day { get; set; }
		public string Time { get; set; }
		public List<Demo> Demos { get; set; }

		public MediaProgram()
		{
			Name = string.Empty;
			Station = string.Empty;
			Daypart = string.Empty;
			Day = string.Empty;
			Time = string.Empty;
			Demos = new List<Demo>();
		}
	}
}