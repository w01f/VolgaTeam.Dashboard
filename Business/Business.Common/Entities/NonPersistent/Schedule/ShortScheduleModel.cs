using System;
using Asa.Business.Common.Enums;
using Asa.Business.Common.Interfaces;

namespace Asa.Business.Common.Entities.NonPersistent.Schedule
{
	public class ShortScheduleModel<TSchedule, TScheduleSettings>
		where TSchedule : ISchedule<TScheduleSettings>
		where TScheduleSettings : IBaseScheduleSettings
	{
		public TSchedule Parent { get; set; }
		public ScheduleEditMode EditMode { get; set; }
		public string Name { get; set; }
		public string Advertiser { get; set; }
		public DateTime? LastModified { get; set; }
		public string Status { get; set; }
	}
}
