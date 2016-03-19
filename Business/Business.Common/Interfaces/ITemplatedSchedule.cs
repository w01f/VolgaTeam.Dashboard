using Asa.Business.Common.Entities.NonPersistent.ScheduleTemplates;

namespace Asa.Business.Common.Interfaces
{
	public interface ITemplatedSchedule
	{
		ScheduleTemplate GetTemplate(string name);
		void LoadFromTemplate(ScheduleTemplate sourceTemplate);
	}
}
