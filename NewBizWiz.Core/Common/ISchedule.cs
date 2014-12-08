using System;
using System.Collections.Generic;
using NewBizWiz.Core.OnlineSchedule;

namespace NewBizWiz.Core.Common
{
	public interface ISchedule
	{
		bool IsNew { get; set; }
		string Name { get; set; }
		string BusinessName { get; set; }
		string DecisionMaker { get; set; }
		string AccountNumber { get; set; }
		string FlightDates { get; }
		DateTime? PresentationDate { get; set; }
		DateTime? FlightDateStart { get; set; }
		DateTime? FlightDateEnd { get; set; }
		IScheduleViewSettings SharedViewSettings { get; }
	}

	public interface IDigitalSchedule : ISchedule
	{
		List<DigitalProduct> DigitalProducts { get; }
		DigitalProductSummary DigitalProductSummary { get; }

		void AddDigital(string categoryName);
		void UpDigital(int position);
		void DownDigital(int position);
		void ChangeDigitalProductPosition(int position, int newPosition);
		void RebuildDigitalProductIndexes();
	}
}
