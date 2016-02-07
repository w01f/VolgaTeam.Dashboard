using System.Data.Entity.ModelConfiguration;
using Asa.Business.Common.Contexts;
using Asa.Business.Common.Entities.Persistent;

namespace Asa.Business.Common.Mappers
{
	public class ScheduleMap<TSchedule, TContext> : EntityTypeConfiguration<TSchedule>
		where TSchedule : BaseSchedule<TContext>
		where TContext : ScheduleContext
	{
		public ScheduleMap()
		{
			ToTable("Schedule");
		}
	}
}
