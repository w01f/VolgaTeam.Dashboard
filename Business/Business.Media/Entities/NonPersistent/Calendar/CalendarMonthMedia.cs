using System;
using Asa.Business.Calendar.Entities.NonPersistent;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Calendar
{
	public abstract class CalendarMonthMedia : CalendarMonth
	{
		[JsonConstructor]
		protected CalendarMonthMedia() { }

		protected CalendarMonthMedia(CalendarSection parent)
			: base(parent)
		{
			OutputData = new MediaCalendarOutputData(this);
		}

		public override DateTime Date
		{
			get => _date;
			set => _date = value;
		}
	}
}
