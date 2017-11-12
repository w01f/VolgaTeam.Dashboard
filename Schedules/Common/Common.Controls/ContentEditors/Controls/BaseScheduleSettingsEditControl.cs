using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Common.Interfaces;

namespace Asa.Schedules.Common.Controls.ContentEditors.Controls
{
	public abstract class BaseScheduleSettingsEditControl<TScheduleSettings, TChangeInfo> : BaseContentEditControl<TChangeInfo>
		where TScheduleSettings : IBaseScheduleSettings
		where TChangeInfo : BaseScheduleChangeInfo
	{
		public TScheduleSettings EditedSettings { get; set; }
	}
}
