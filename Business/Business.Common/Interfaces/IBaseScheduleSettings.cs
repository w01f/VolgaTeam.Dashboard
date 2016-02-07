using System;
using System.Collections.Generic;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Common.Core.Interfaces;

namespace Asa.Business.Common.Interfaces
{
	public interface IBaseScheduleSettings
	{
		string BusinessName { get; set; }
		string DecisionMaker { get; set; }
		string ClientType { get; set; }
		string AccountNumber { get; set; }
		string Status { get; set; }
		DateTime? PresentationDate { get; set; }
		DateTime? FlightDateStart { get; set; }
		DateTime? FlightDateEnd { get; set; }
		string FlightDates { get; }
		int TotalWeeks { get; }
		IEnumerable<DateRange> GetWeeks();
	}
}