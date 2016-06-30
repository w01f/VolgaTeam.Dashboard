using System;
using System.Collections.Generic;
using Asa.Business.Media.Entities.NonPersistent.Schedule;

namespace Asa.Business.Media.Entities.NonPersistent.Common
{
	public class SourceProgram
	{
		public SourceProgram()
		{
			Id = Guid.NewGuid().ToString();
			Name = string.Empty;
			Station = string.Empty;
			Daypart = string.Empty;
			Day = string.Empty;
			Time = string.Empty;
			Demos = new List<Demo>();
		}

		public string Id { get; set; }
		public string Name { get; set; }
		public string Station { get; set; }
		public string Daypart { get; set; }
		public string Day { get; set; }
		public string Time { get; set; }
		public List<Demo> Demos { get; set; }
	}
}
