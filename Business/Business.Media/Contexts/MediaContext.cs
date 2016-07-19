using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using Asa.Business.Common.Contexts;
using Asa.Business.Common.Interfaces;
using Asa.Business.Common.Mappers;
using Asa.Business.Common.Schema.Schedule;
using Asa.Business.Media.Entities.Persistent;

namespace Asa.Business.Media.Contexts
{
	public class MediaContext : ScheduleContext, IScheduleDBSetContainer<MediaSchedule, MediaContext>
	{
		public DbSet<MediaSchedule> Schedules { get; set; }

		public override int SaveChanges()
		{
			try
			{
				ChangeTracker.DetectChanges();
				return base.SaveChanges();
			}
			catch (DbEntityValidationException e)
			{
				throw e;
			}
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new ScheduleMap<MediaSchedule, MediaContext>());
			modelBuilder.Configurations.Add(new SchedulePartitionMap<MediaPartition, MediaContext>());
			modelBuilder.Configurations.Add(new ScheduleSolutionMap<MediaSolution, MediaContext>());
			base.OnModelCreating(modelBuilder);
		}

		protected void OnObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
		{
			if (e.Entity is MediaSchedule)
			{
				var schedule = ((MediaSchedule)e.Entity);
				schedule.Context = this;
			}
		}

		public MediaContext(string storagePath)
			: base(storagePath)
		{
			Database.SetInitializer(new ScheduleInitializer<MediaContext>());
			Database.Initialize(true);
			ObjectContext.ObjectMaterialized += OnObjectMaterialized;
		}
	}
}
