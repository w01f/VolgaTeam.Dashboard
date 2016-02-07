using System;
using System.Collections.Generic;
using Asa.Business.Calendar.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Calendar.Entities.NonPersistent
{
	public abstract class CalendarMonth
	{
		protected DateTime _date;

		public ICalendarContent Parent { get; private set; }
		public DateTime DaysRangeBegin { get; set; }
		public DateTime DaysRangeEnd { get; set; }
		public List<CalendarDay> Days { get; private set; }
		public CalendarOutputData OutputData { get; protected set; }

		public abstract DateTime Date { get; set; }

		[JsonConstructor]
		protected CalendarMonth() { }

		protected CalendarMonth(ICalendarContent parent)
		{
			Parent = parent;
			Days = new List<CalendarDay>();
		}

		public virtual void Dispose()
		{
			Days.Clear();

			OutputData.Dispose();
			OutputData = null;

			Parent = null;
		}
	}
}
