using Asa.Business.Common.Interfaces;
using Asa.Business.Online.Configuration;

namespace Asa.Business.Online.Interfaces
{
	public interface IDigitalScheduleSettings: IBaseScheduleSettings
	{
		DigitalProductListViewSettings DigitalProductListViewSettings { get; }
		DigitalPackageSettings DigitalPackageSettings { get; }
		AdPlanViewSettings AdPlanViewSettings { get; }
	}
}
