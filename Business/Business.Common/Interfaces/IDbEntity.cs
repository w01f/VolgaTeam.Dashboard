using System.Data.Entity;

namespace Asa.Business.Common.Interfaces
{
	public interface IDbEntity<TContext> where TContext : DbContext
	{
		void BeforeSave();
		void Save(TContext context, IDbEntity<TContext> current, bool withCommit);
		void Add(TContext context);
		void Delete(TContext context);
	}
}
