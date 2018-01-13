using System;
using System.Collections.Generic;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.Persistent;

namespace Asa.Business.Media.Entities.NonPersistent.Calendar
{
	public abstract class MediaCalendar : CalendarContent<MediaSchedule, MediaScheduleSettings>
	{
		protected bool? _isMondayBased;

		public override MediaSchedule Schedule => ((MediaPartition)Parent).Schedule;

		protected override void AfterConstruction()
		{
			base.AfterConstruction();

			_isMondayBased = ScheduleSettings.MondayBased;

			InitSections();
		}

		protected override void AfterCreate()
		{
			base.AfterCreate();

			InitSections();

			Schedule.CalendarTypeChanged += OnCalendarTypeChanged;
			OnCalendarTypeChanged(this, EventArgs.Empty);
		}

		protected abstract void InitSections();

		public override void Dispose()
		{
			Schedule.CalendarTypeChanged -= OnCalendarTypeChanged;
			base.Dispose();
		}

		private void OnCalendarTypeChanged(object sender, EventArgs e)
		{
			if (_isMondayBased.HasValue && _isMondayBased == ScheduleSettings.MondayBased) return;
			_isMondayBased = ScheduleSettings.MondayBased;

			foreach (var calendarSection in Sections)
				calendarSection.Reset();
		}

		public override IEnumerable<DateRange> CalculateDateRange(IEnumerable<DateTime> dates)
		{
			return CalculateDateRange(dates, ScheduleSettings.StartDayOfWeek, ScheduleSettings.EndDayOfWeek);
		}

		public override DateTime[][] GetDaysByWeek(DateTime start, DateTime end)
		{
			return GetDaysByWeek(start, end, ScheduleSettings.EndDayOfWeek);
		}
	}
}
