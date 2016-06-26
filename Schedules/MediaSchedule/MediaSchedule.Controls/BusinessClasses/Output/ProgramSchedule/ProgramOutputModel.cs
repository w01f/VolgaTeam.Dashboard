using System.Collections.Generic;
using Asa.Common.Core.Objects.Images;

namespace Asa.Media.Controls.BusinessClasses.Output.ProgramSchedule
{
	public class ProgramOutputModel
	{
		public ProgramScheduleOutputModel Parent { get; private set; }
		public string Name { get; set; }
		public string LineID { get; set; }
		public string Station { get; set; }
		public string Time { get; set; }
		public string Days { get; set; }
		public string Length { get; set; }
		public string Rate { get; set; }
		public string Rating { get; set; }
		public string TotalRate { get; set; }
		public string TotalSpots { get; set; }
		public string CPP { get; set; }
		public string GRP { get; set; }
		public ImageSource Logo { get; set; }
		public List<string> Spots { get; set; }

		public ProgramOutputModel(ProgramScheduleOutputModel parent)
		{
			Parent = parent;
			Name = string.Empty;
			LineID = string.Empty;
			Station = string.Empty;
			Time = string.Empty;
			Days = string.Empty;
			Length = string.Empty;
			Rate = string.Empty;
			Rating = string.Empty;
			TotalRate = string.Empty;
			TotalSpots = string.Empty;
			CPP = string.Empty;
			GRP = string.Empty;
			Spots = new List<string>();
		}
	}
}
