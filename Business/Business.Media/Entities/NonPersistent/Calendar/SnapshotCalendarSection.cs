using System;
using System.Collections.Generic;
using System.Linq;
using Asa.Business.Calendar.Interfaces;
using Asa.Common.Core.Objects.Output;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Calendar
{
	public class SnapshotCalendarSection : DataLinkedCalendarSection
	{
		protected override String DataSourceNoteSeparator => "  |  ";

		[JsonConstructor]
		protected SnapshotCalendarSection() { }

		public SnapshotCalendarSection(ICalendarContent parent) : base(parent) { }
		
		protected override IList<MediaDataNote> LoadNotesFromDataSource()
		{
			var notes = new List<MediaDataNote>();

			if (MediaScheduleSettings.FlightDateStart.HasValue && MediaScheduleSettings.FlightDateEnd.HasValue)
			{
				var startDate = MediaScheduleSettings.FlightDateStart.Value;
				var endDate = MediaScheduleSettings.FlightDateEnd.Value;
				while (startDate < endDate)
				{
					var noteEndDate = startDate.AddDays(6);

					notes.AddRange(MediaSchedule.SnapshotContent.Snapshots
						.Where(s => !s.ActiveWeeks.Any() || s.ActiveWeeks.Any(w => w.StartDate == startDate && w.FinishDate == noteEndDate))
						.Select(s =>
						{
							var textGroup = new TextGroup();
							textGroup.Items.Add(new TextItem(String.Format("{0} - ", s.Name), true));
							var programsText = String.Join(",   ", s.Programs.Select(program =>
								String.Format("{0}{1}  {2}x  {3}",
									s.ShowStation ? String.Format("{0}  ", program.Station) : String.Empty,
									(s.ShowProgram && !String.IsNullOrEmpty(program.Name)) || (s.ShowTime && !String.IsNullOrEmpty(program.Time)) ?
										String.Format("({0}{1})",
											s.ShowProgram && !String.IsNullOrEmpty(program.Name) ? String.Format("{0} ", program.Name) : String.Empty,
											s.ShowTime && !String.IsNullOrEmpty(program.Time) ? String.Format("{0}", program.Time) : String.Empty) :
										String.Empty,
									program.TotalSpots,
									program.StartDayLetter == program.EndDayLetter ?
										program.StartDayLetter :
										String.Format("{0}-{1}", program.StartDayLetter, program.EndDayLetter)
								)
							));
							textGroup.Items.Add(new TextItem(programsText, false));
							return new MediaDataNote(this)
							{
								StartDay = startDate,
								FinishDay = noteEndDate,
								MediaData = textGroup
							};
						})
					);
					startDate = startDate.AddDays(7);
				}
			}
			return notes;
		}
	}
}
