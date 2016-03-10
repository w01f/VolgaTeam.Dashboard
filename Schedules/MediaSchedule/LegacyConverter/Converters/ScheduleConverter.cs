using System.Windows.Forms;
using Asa.Business.Common.Enums;
using Asa.Business.Media.Entities.NonPersistent.Calendar;
using Asa.Business.Media.Entities.NonPersistent.Option;
using Asa.Business.Media.Entities.Persistent;
using Asa.Legacy.Media.Entities.Schedule;

namespace Asa.Media.LegacyConverter.Converters
{
	static class ScheduleConverter
	{
		public static void ImportData(this MediaSchedule target, RegularSchedule source)
		{
			target.Settings.ImportData(source);
			Application.DoEvents();
			target.ProgramSchedule.ImportData(source.ProgramSchedule);
			Application.DoEvents();
			target.DigitalProductsContent.ImportData(source);
			Application.DoEvents();
			target.SnapshotContent.ImportData(source);
			Application.DoEvents();
			target.GetSchedulePartitionContent<OptionsContent>(SchedulePartitionType.Options)
				.ImportData(source);
			Application.DoEvents();
			target.GetSchedulePartitionContent<BroadcastCalendar>(SchedulePartitionType.BroadcastCalendar)
				.ImportData(source.BroadcastCalendar);
			Application.DoEvents();
			target.GetSchedulePartitionContent<CustomCalendar>(SchedulePartitionType.CustomCalendar)
				.ImportData(source.CustomCalendar);
			Application.DoEvents();
		}
	}
}
