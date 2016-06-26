using Asa.Business.Common.Interfaces;
using Asa.Business.Online.Configuration;

namespace Asa.Business.Online.Interfaces
{
	public interface IDigitalScheduleSettings: IBaseScheduleSettings, IDigitalPackageSettingsContainer
	{
		DigitalProductListViewSettings DigitalProductListViewSettings { get; }
	}
}
