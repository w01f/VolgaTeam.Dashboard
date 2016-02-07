using System.Data.Entity;
using Asa.Business.Common.Entities.Persistent;

namespace Asa.Business.Common.Contexts
{
	public abstract class ScheduleContext : SqLiteContext
	{
		public string StoragePath { get; private set; }
		public DbSet<DBVersion> Versions { get; set; }

		protected ScheduleContext(string storagePath)
			: base(storagePath)
		{
			StoragePath = storagePath;;
		}
	}
}
