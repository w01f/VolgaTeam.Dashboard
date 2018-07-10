using Asa.Business.Common.Entities.NonPersistent.ScheduleResources;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public interface IScheduleResourceHolder
	{
		BaseScheduleResourceContainer ResourceContainer { get; }
	}
}
