using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Online.Interfaces;

namespace Asa.Business.Online.Entities.NonPersistent
{
	public abstract class DigitalScheduleContent : BaseSchedulePartitionContent<IDigitalSchedule<IDigitalScheduleSettings>, IDigitalScheduleSettings>
	{ }
}
