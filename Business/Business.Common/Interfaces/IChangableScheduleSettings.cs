using Asa.Business.Common.Entities.NonPersistent.Schedule;

namespace Asa.Business.Common.Interfaces
{
	public interface IChangableScheduleSettings<in TScheduleSettings, in TScheduleChangeInfo>
		where TScheduleSettings : IBaseScheduleSettings
		where TScheduleChangeInfo: BaseScheduleChangeInfo
	{
		void CompareChanges(TScheduleSettings changedInstance, TScheduleChangeInfo changeInfo);
	}
}
