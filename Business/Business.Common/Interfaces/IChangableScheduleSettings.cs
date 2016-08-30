using Asa.Business.Common.Entities.NonPersistent.Schedule;

namespace Asa.Business.Common.Interfaces
{
	public interface IChangableScheduleSettings<in TScheduleSettings, out TScheduleChangeInfo>
		where TScheduleSettings : IBaseScheduleSettings
		where TScheduleChangeInfo: BaseScheduleChangeInfo
	{
		TScheduleChangeInfo GetChangeInfo(TScheduleSettings changedInstance);
	}
}
