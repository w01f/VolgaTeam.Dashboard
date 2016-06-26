using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asa.Media.Controls.BusinessClasses.Output.ProgramSchedule
{
	public class TotalSpotOutputModel
	{
		public string Month { get; set; }
		public string Day { get; set; }
		public string Value { get; set; }

		public string HeaderTagText => Month + (!string.IsNullOrEmpty(Day) ? (((char)13) + Day) : string.Empty);

		public TotalSpotOutputModel()
		{
			Month = string.Empty;
			Day = string.Empty;
			Value = string.Empty;
		}
	}
}
