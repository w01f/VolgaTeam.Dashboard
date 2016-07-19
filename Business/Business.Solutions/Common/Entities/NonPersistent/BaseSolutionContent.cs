using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Common.Core.Interfaces;

namespace Asa.Business.Solutions.Common.Entities.NonPersistent
{
	public abstract class BaseSolutionContent: SettingsContainer, IJsonCloneable<BaseSolutionContent>
	{
		public virtual void AfterClone(BaseSolutionContent source, bool fullClone = true)
		{
			Parent = source.Parent;
			AfterCreate();
		}

		public virtual void Dispose()
		{
			Parent = null;
		}
	}
}
