using System.Data.Entity.ModelConfiguration;
using Asa.Business.Common.Contexts;
using Asa.Business.Common.Entities.Persistent;

namespace Asa.Business.Common.Mappers
{
	public class SchedulePartitionMap<TPartition, TContext> : EntityTypeConfiguration<TPartition>
		where TPartition : BaseSchedulePartition<TContext>
		where TContext : ScheduleContext
	{
		public SchedulePartitionMap()
		{
			ToTable("SchedulePartition");
		}
	}
}
