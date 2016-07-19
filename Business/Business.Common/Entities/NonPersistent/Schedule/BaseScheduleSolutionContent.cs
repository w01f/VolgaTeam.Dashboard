using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Common.Interfaces;
using Asa.Common.Core.Interfaces;

namespace Asa.Business.Common.Entities.NonPersistent.Schedule
{
	public abstract class BaseScheduleSolutionContent<TSchedule, TScheduleSettings> : SettingsContainer, IScheduleSolutionContent, IJsonCloneable<BaseScheduleSolutionContent<TSchedule, TScheduleSettings>>
		where TSchedule : ISchedule<TScheduleSettings>
		where TScheduleSettings : IBaseScheduleSettings
	{
		public abstract TSchedule Schedule { get; }

		public TScheduleSettings ScheduleSettings => Schedule.Settings;

		public virtual void AfterClone(BaseScheduleSolutionContent<TSchedule, TScheduleSettings> source, bool fullClone = true)
		{
			Parent = source.Parent;
			AfterCreate();
		}

		public virtual void Dispose()
		{
			Parent = null;
		}
	}
}
