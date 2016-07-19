using Asa.Business.Solutions.Common.Entities.NonPersistent;

namespace Asa.Business.Solutions.Common.Interfaces
{
	public interface IScheduleSolution<TSolutionContent> where TSolutionContent : BaseSolutionContent
	{
		int Type { get; set; }
		TSolutionContent Content { get; set; }
	}
}
