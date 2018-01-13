using System.Linq;
using Asa.Business.Common.Helpers;
using Asa.Business.Media.Enums;

namespace Asa.Business.Media.Entities.NonPersistent.Calendar
{
	public class BroadcastCalendar : MediaCalendar
	{
		public BroadcastDataTypeEnum DataSourceType { get; set; }

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

		protected override void InitSections()
		{
			if (Sections.Any()) return;

			Sections.Add(new ScheduleCalendarSection(this));
			Sections.Add(new SnapshotCalendarSection(this));
			Sections.Add(new CustomDataCalendarSection(this));

			foreach (var calendarSection in Sections)
				calendarSection.AfterConstraction();
		}

		private void OnSchedulePartitionContentChanged(object sender, PartitionContentChangedEventArgs e)
		{
			SetDefaultDataSource();
			foreach (var calendarSection in Sections.OfType<DataLinkedCalendarSection>().ToList())
				calendarSection.UpdateNotesCollection();
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
