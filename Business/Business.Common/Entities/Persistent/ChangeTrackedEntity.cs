using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using Asa.Business.Common.Interfaces;

namespace Asa.Business.Common.Entities.Persistent
{
	public abstract class ChangeTrackedEntity<TContext> : BaseEntity<TContext>, IChangeTracked where TContext : DbContext
	{
		#region Persistent Properties
		[Required]
		public DateTime LastModified { get; set; }
		#endregion

		protected ChangeTrackedEntity()
		{
			LastModified = DateTime.Now;
		}

		public virtual bool IsModified(IChangeTracked latest)
		{
			return latest.LastModified > LastModified;
		}

		public virtual void MarkAsModified()
		{
			LastModified = DateTime.Now;
		}
	}
}
