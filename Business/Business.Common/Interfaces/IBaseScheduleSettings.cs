using System;
using System.Collections.Generic;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Common.Enums;

namespace Asa.Business.Common.Interfaces
{
	public interface IBaseScheduleSettings
	{
		ScheduleEditMode EditMode { get; set; }
		string BusinessName { get; set; }
		string DecisionMaker { get; set; }
		string Status { get; set; }
		DateTime? PresentationDate { get; set; }
		DateTime? FlightDateStart { get; set; }
		DateTime? FlightDateEnd { get; set; }
		string FlightDates { get; }
		int TotalWeeks { get; }
		IEnumerable<DateRange> GetWeeks();
	}
}