using Asa.Business.Common.Entities.NonPersistent.ScheduleResources;

namespace Asa.Solutions.Common.PresentationClasses
{
	public interface IScheduleResourceHolder
	{
		BaseScheduleResourceContainer ResourceContainer { get; }
	}
}
