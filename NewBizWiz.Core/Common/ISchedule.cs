using System;
using System.Collections.Generic;
using NewBizWiz.Core.OnlineSchedule;

namespace NewBizWiz.Core.Common
{
	public interface ISchedule
	{
		string BusinessName { get; set; }
		string DecisionMaker { get; set; }
		string AccountNumber { get; set; }
		string FlightDates { get; }
		DateTime PresentationDate { get; set; }
		DateTime FlightDateStart { get; set; }
		DateTime FlightDateEnd { get; set; }
		IScheduleViewSettings CommonViewSettings { get; }
		List<DigitalProduct> DigitalProducts { get; }

		void RebuildDigitalProductIndexes();
	}
}
