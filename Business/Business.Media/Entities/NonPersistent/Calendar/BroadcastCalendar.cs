using System;
using System.Collections.Generic;
using System.Linq;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Business.Common.Enums;
using Asa.Business.Common.Helpers;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Objects.Output;

namespace Asa.Business.Media.Entities.NonPersistent.Calendar
{
	public class BroadcastCalendar : MediaCalendar
	{
		public BroadcastDataTypeEnum DataSourceType { get; set; }

		public override bool AllowCustomNotes => !(DataSourceType == BroadcastDataTypeEnum.Schedule || DataSourceType == BroadcastDataTypeEnum.Snapshots);

		protected override void AfterConstruction()
		{
			SetDefaultDataSource();
			base.AfterConstruction();
		}

		protected override void AfterCreate()
		{
			base.AfterCreate();
			Schedule.PartitionContentChanged += OnSchedulePartitionContentChanged;
		}

		public override void Dispose()
		{
			Schedule.PartitionContentChanged -= OnSchedulePartitionContentChanged;
			base.Dispose();
		}

		public override void UpdateNotesCollection()
		{
			var noteSeparator = String.Empty;
			var notes = new List<MediaDataNote>();

			if (DataSourceType == BroadcastDataTypeEnum.Schedule || DataSourceType == BroadcastDataTypeEnum.Snapshots)
			{
				switch (DataSourceType)
				{
					case BroadcastDataTypeEnum.Schedule:
						noteSeparator = "   ";
						notes.AddRange(GetNotesFromSchedule());
						break;
					case BroadcastDataTypeEnum.Snapshots:
						noteSeparator = "  |  ";
						notes.AddRange(GetNotesFromSnapshots());
						break;
				}

				bool needToSplit;
				notes.ForEach(n => n.Splitted = false);
				var splittedNotes = new List<MediaDataNote>(notes);
				do
				{
					needToSplit = false;
					foreach (var calendarNote in notes.OrderByDescending(n => n.Length))
					{
						if (calendarNote.Splitted) continue;
						var intersectedNote = splittedNotes.Where(sn => sn != calendarNote).OrderBy(n => n.Length).FirstOrDefault(mn =>
							(mn.StartDay >= calendarNote.StartDay && mn.StartDay <= calendarNote.FinishDay) ||
							(mn.FinishDay >= calendarNote.StartDay && mn.FinishDay <= calendarNote.FinishDay));
						if (intersectedNote == null) continue;
						needToSplit = true;
						if ((intersectedNote.StartDay >= calendarNote.StartDay && intersectedNote.StartDay <= calendarNote.FinishDay) &&
						    (intersectedNote.FinishDay >= calendarNote.StartDay && intersectedNote.FinishDay <= calendarNote.FinishDay))
						{
							calendarNote.MediaData = new[] {calendarNote.MediaData, intersectedNote.MediaData}.Join(noteSeparator);
							splittedNotes.Remove(intersectedNote);
							intersectedNote.Splitted = true;
						}
						else if (intersectedNote.StartDay >= calendarNote.StartDay && intersectedNote.StartDay <= calendarNote.FinishDay)
						{
							splittedNotes.Add(new MediaDataNote(this)
							{
								StartDay = calendarNote.StartDay,
								FinishDay = intersectedNote.FinishDay,
								MediaData = new[] {calendarNote.MediaData, intersectedNote.MediaData}.Join(noteSeparator)
							});
							splittedNotes.Remove(calendarNote);
							splittedNotes.Remove(intersectedNote);
							intersectedNote.Splitted = true;
						}
						else if (intersectedNote.FinishDay >= calendarNote.StartDay &&
						         intersectedNote.FinishDay <= calendarNote.FinishDay)
						{
							splittedNotes.Add(new MediaDataNote(this)
							{
								StartDay = intersectedNote.StartDay,
								FinishDay = calendarNote.FinishDay,
								MediaData = new[] {calendarNote.MediaData, intersectedNote.MediaData}.Join(noteSeparator)
							});
							splittedNotes.Remove(calendarNote);
							splittedNotes.Remove(intersectedNote);
							intersectedNote.Splitted = true;
						}
					}
					notes.Clear();
					notes.AddRange(splittedNotes);
					notes.ForEach(n => n.Splitted = false);
				} while (needToSplit);

				foreach (var calendarNote in Notes.OfType<MediaDataNote>().Where(n => n.EditedByUser))
				{
					notes.Where(n => n.StartDay.Date == calendarNote.StartDay.Date && n.FinishDay.Date == calendarNote.FinishDay.Date)
						.ToList().ForEach(n =>
						{
							n.Note = calendarNote.Note;
							n.BackgroundColor = calendarNote.BackgroundColor;
						});
				}
			}

			Notes.Clear();
			Notes.AddRange(notes);

			UpdateDayAndNoteLinks();
		}

		private IEnumerable<MediaDataNote> GetNotesFromSchedule()
		{
			const string noteSeparator = "   ";
			var notes = new List<MediaDataNote>();
			var programs = Schedule.ProgramSchedule.Sections.SelectMany(s => s.Programs).ToList();
			if (ScheduleSettings.FlightDateStart.HasValue && ScheduleSettings.FlightDateEnd.HasValue)
			{
				notes.AddRange(programs.SelectMany(p => p.Spots)
					.Where(s => s.Count > 0 && s.StartDate.HasValue && s.EndDate.HasValue)
					.GroupBy(g => new { g.StartDate, g.EndDate })
					.Select(g => new MediaDataNote(this)
					{
						StartDay = g.Key.StartDate.Value,
						FinishDay = g.Key.EndDate.Value,
						MediaData = g.Select(sp => sp.FormattedString).Join(noteSeparator)
					}));
			}
			return notes;
		}

		private IEnumerable<MediaDataNote> GetNotesFromSnapshots()
		{
			var notes = new List<MediaDataNote>();

			if (ScheduleSettings.FlightDateStart.HasValue && ScheduleSettings.FlightDateEnd.HasValue)
			{
				var startDate = ScheduleSettings.FlightDateStart.Value;
				var endDate = ScheduleSettings.FlightDateEnd.Value;
				while (startDate < endDate)
				{
					var noteEndDate = startDate.AddDays(6);

					notes.AddRange(Schedule.SnapshotContent.Snapshots
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

		protected override void ApplyDefaultMonthSettings(CalendarMonth targetMonth)
		{
			targetMonth.OutputData.ShowLogo = MediaMetaData.Instance.ListManager.DefaultBroadcastCalendarSettings.ShowLogo;
			targetMonth.OutputData.ShowBigDate = MediaMetaData.Instance.ListManager.DefaultBroadcastCalendarSettings.ShowBigDate;
		}

		private void OnSchedulePartitionContentChanged(object sender, PartitionContentChangedEventArgs e)
		{
			var needToUpdateNotes = false;
			switch (e.PartitionType)
			{
				case SchedulePartitionType.WeeklySchedule:
				case SchedulePartitionType.MonthlySchedule:
					SetDefaultDataSource();
					needToUpdateNotes = DataSourceType == BroadcastDataTypeEnum.Schedule;
					break;
				case SchedulePartitionType.Snapshots:
					SetDefaultDataSource();
					needToUpdateNotes = DataSourceType == BroadcastDataTypeEnum.Snapshots;
					break;
			}
			if (needToUpdateNotes)
				UpdateNotesCollection();
		}

		private void SetDefaultDataSource()
		{
			if (DataSourceType != BroadcastDataTypeEnum.Undefined)
				return;
			if (ScheduleSettings.SelectedSpotType == SpotType.Week && Schedule.ProgramSchedule.TotalSpots > 0)
			{
				DataSourceType = BroadcastDataTypeEnum.Schedule;
				return;
			}
			if (Schedule.SnapshotContent.Snapshots.Any(s => s.Programs.Count > 0))
				DataSourceType = BroadcastDataTypeEnum.Snapshots;
		}
	}
}
