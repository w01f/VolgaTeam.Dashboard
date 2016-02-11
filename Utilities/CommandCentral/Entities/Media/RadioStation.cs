using System.Collections.Generic;
using Asa.Business.Media.Entities.NonPersistent.Schedule;

namespace CommandCentral.Entities.Media
{
	internal class RadioStation
	{
		public RadioStation()
		{
			Name = string.Empty;
			Programs = new List<RadioProgram>();
			DaypartRotatiions = new List<DaypartRotation>();
			Demos = new List<Demo>();
		}

		public string Name { get; set; }
		public List<RadioProgram> Programs { get; set; }
		public List<DaypartRotation> DaypartRotatiions { get; set; }
		public List<Demo> Demos { get; set; }
	}
}