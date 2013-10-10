using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.OnlineSchedule;

namespace NewBizWiz.Core.Common
{
	public interface IScheduleViewSettings
	{
		DigitalPackageSettings DigitalPackageSettings { get; }
		AdPlanViewSettings AdPlanViewSettings { get; }
	}
}
