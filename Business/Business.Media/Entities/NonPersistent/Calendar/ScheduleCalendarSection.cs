using System;
using System.Collections.Generic;
using System.Linq;
using Asa.Business.Calendar.Interfaces;
using Asa.Common.Core.Extensions;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Calendar
{
	public class ScheduleCalendarSection : DataLinkedCalendarSection
	{
		protected override String DataSourceNoteSeparator => "   ";

		[JsonConstructor]
		protected ScheduleCalendarSection() { }

		public ScheduleCalendarSection(ICalendarContent parent) : base(parent) { }
		
		protected override IList<MediaDataNote> LoadNotesFromDataSource()
		{
			var notes = new List<MediaDataNote>();
			var programs = MediaSchedule.ProgramSchedule.Sections.SelectMany(s => s.Programs).ToList();
			if (MediaScheduleSettings.FlightDateStart.HasValue && MediaScheduleSettings.FlightDateEnd.HasValue)
			{
				notes.AddRange(programs.SelectMany(p => p.Spots)
					.Where(s => s.Count > 0 && s.StartDate.HasValue && s.EndDate.HasValue)
					.GroupBy(g => new { g.StartDate, g.EndDate })
					.Select(g => new MediaDataNote(this)
					{
						StartDay = g.Key.StartDate.Value,
						FinishDay = g.Key.EndDate.Value,
						MediaData = g.Select(sp => sp.FormattedString).Join(DataSourceNoteSeparator)
					}));
			}
			return notes;
		}
	}
}
