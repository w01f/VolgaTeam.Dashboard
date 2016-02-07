using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Common.Interfaces;

namespace Asa.Common.GUI.ContentEditors.Controls
{
	public abstract class BaseScheduleSettingsEditControl<TScheduleSettings, TChangeInfo> : BaseContentEditControl<TChangeInfo>
		where TScheduleSettings : IBaseScheduleSettings
		where TChangeInfo : BaseScheduleChangeInfo
	{
		protected TScheduleSettings EditedSettings { get; set; }
	}
}
