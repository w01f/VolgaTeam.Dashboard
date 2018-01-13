using System.Collections.Generic;
using System.Linq;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Business.Calendar.Interfaces;
using Asa.Business.Media.Configuration;
using Asa.Common.Core.Extensions;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Calendar
{
	public abstract class DataLinkedCalendarSection : MediaCalendarSection
	{
		public override bool AllowCustomNotes => false;

		protected abstract string DataSourceNoteSeparator { get; }

		[JsonConstructor]
		protected DataLinkedCalendarSection() { }

		protected DataLinkedCalendarSection(ICalendarContent parent) : base(parent) { }

		protected override void ApplyDefaultMonthSettings(CalendarMonth targetMonth)
		{
			targetMonth.OutputData.ShowLogo = MediaMetaData.Instance.ListManager.DefaultBroadcastCalendarSettings.ShowLogo;
			targetMonth.OutputData.ShowBigDate = MediaMetaData.Instance.ListManager.DefaultBroadcastCalendarSettings.ShowBigDate;
		}

		public override void UpdateNotesCollection()
		{
			bool needToSplit;
			var notes = new List<MediaDataNote>(LoadNotesFromDataSource());
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
						calendarNote.MediaData = new[] { calendarNote.MediaData, intersectedNote.MediaData }.Join(DataSourceNoteSeparator);
						splittedNotes.Remove(intersectedNote);
						intersectedNote.Splitted = true;
					}
					else if (intersectedNote.StartDay >= calendarNote.StartDay && intersectedNote.StartDay <= calendarNote.FinishDay)
					{
						splittedNotes.Add(new MediaDataNote(this)
						{
							StartDay = calendarNote.StartDay,
							FinishDay = intersectedNote.FinishDay,
							MediaData = new[] { calendarNote.MediaData, intersectedNote.MediaData }.Join(DataSourceNoteSeparator)
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
							MediaData = new[] { calendarNote.MediaData, intersectedNote.MediaData }.Join(DataSourceNoteSeparator)
						});
						splittedNotes.Remove(calendarNote);
						splittedNotes.Remove(intersectedNote);
						intersectedNote.Splitted = true;
					}
				}
				notes.Clear();
				notes.AddRange(splittedNotes);
				notes.ForEach(n => n.Splitted = false);
			}
			while (needToSplit);

			foreach (var calendarNote in Notes.OfType<MediaDataNote>().Where(n => n.EditedByUser))
			{
				notes.Where(n => n.StartDay.Date == calendarNote.StartDay.Date && n.FinishDay.Date == calendarNote.FinishDay.Date)
					.ToList().ForEach(n =>
					{
						n.Note = calendarNote.Note;
						n.BackgroundColor = calendarNote.BackgroundColor;
					});
			}

			Notes.Clear();
			Notes.AddRange(notes);

			UpdateDayAndNoteLinks();
		}

		protected abstract IList<MediaDataNote> LoadNotesFromDataSource();
	}
}
