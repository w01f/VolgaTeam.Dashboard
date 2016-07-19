using System.Data.Entity.ModelConfiguration;
using Asa.Business.Common.Contexts;
using Asa.Business.Common.Entities.Persistent;

namespace Asa.Business.Common.Mappers
{
	public class ScheduleSolutionMap<TSolution, TContext> : EntityTypeConfiguration<TSolution>
		where TSolution : BaseScheduleSolution<TContext>
		where TContext : ScheduleContext
	{
		public ScheduleSolutionMap()
		{
			ToTable("ScheduleSolution");
		}
	}
}
