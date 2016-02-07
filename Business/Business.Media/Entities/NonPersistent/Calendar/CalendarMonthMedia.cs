using System;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Business.Calendar.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Calendar
{
	public abstract class CalendarMonthMedia : CalendarMonth
	{
		[JsonConstructor]
		protected CalendarMonthMedia() { }

		protected CalendarMonthMedia(ICalendarContent parent)
			: base(parent)
		{
			OutputData = new MediaCalendarOutputData(this);
		}

		public override DateTime Date
		{
			get { return _date; }
			set { _date = value; }
		}
	}
}
