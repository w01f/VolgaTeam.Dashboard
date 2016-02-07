namespace Asa.Common.Core.Interfaces
{
	public interface IJsonCloneable<in TCloneSource> : IJsonCloneSource where TCloneSource : IJsonCloneSource
	{
		void AfterClone(TCloneSource source, bool fullClone = true);
	}
}
