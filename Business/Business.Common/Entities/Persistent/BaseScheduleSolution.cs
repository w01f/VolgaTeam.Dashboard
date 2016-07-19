using System;
using System.ComponentModel.DataAnnotations;
using Asa.Business.Common.Contexts;
using Asa.Business.Common.Interfaces;

namespace Asa.Business.Common.Entities.Persistent
{
	public abstract class BaseScheduleSolution<TContext> : ChangeTrackedEntity<TContext>, IExtKeyHolder
		where TContext : ScheduleContext
	{
		#region Persistent Properties
		[Required]
		public Guid ExtId { get; set; }
		[Required]
		public int Type { get; set; }
		public string ContentEncoded { get; set; }
		#endregion

		protected BaseScheduleSolution()
		{
			ExtId = Guid.NewGuid();
		}

		public void Save()
		{
			MarkAsModified();
		}

		public virtual void CloneData(BaseScheduleSolution<TContext> target)
		{
			target.Type = Type;
			target.ContentEncoded = ContentEncoded;
		}
	}
}
