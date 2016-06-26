using Asa.Business.Media.Entities.NonPersistent.Digital;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Common.Core.Objects.Output;

namespace Asa.Business.Media.Interfaces
{
	public interface IDigitalInfoContainer
	{
		MediaDigitalInfo DigitalInfo { get; }
		MediaScheduleSettings ParentScheduleSettings { get; }
		ContractSettings ContractSettings { get; }
	}
}
