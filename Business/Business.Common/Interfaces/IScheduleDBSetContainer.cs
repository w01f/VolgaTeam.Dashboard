using System.Data.Entity;
using Asa.Business.Common.Contexts;
using Asa.Business.Common.Entities.Persistent;

namespace Asa.Business.Common.Interfaces
{
	public interface IScheduleDBSetContainer<TSchedule,TContext> 
		where TSchedule: BaseSchedule<TContext> 
		where TContext : ScheduleContext
	{
		DbSet<TSchedule> Schedules { get; }
	}
}
