using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Common.Interfaces;

namespace Asa.Common.GUI.ContentEditors.Controls
{
	public abstract class BasePartitionEditControl<TPartitionContent, TSchedule, TScheduleSettings, TChangeInfo> : BaseContentOutputControl<TChangeInfo>
		where TPartitionContent : BaseSchedulePartitionContent<TSchedule, TScheduleSettings>
		where TSchedule : ISchedule<TScheduleSettings>
		where TScheduleSettings : IBaseScheduleSettings
		where TChangeInfo : BaseScheduleChangeInfo
	{
		public TPartitionContent EditedContent { get; set; }
	}
}
