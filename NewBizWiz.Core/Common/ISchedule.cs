using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewBizWiz.Core.Common
{
	public interface ISchedule
	{
		string BusinessName { get; set; }
		string DecisionMaker { get; set; }
		string FlightDates { get; }
		DateTime PresentationDate { get; set; }
		DateTime FlightDateStart { get; set; }
		DateTime FlightDateEnd { get; set; }
	}
}
