using Asa.Business.Common.Entities.Persistent;
using Asa.Business.Media.Contexts;
using Asa.Business.Solutions.Common.Entities.NonPersistent;

namespace Asa.Business.Media.Entities.Persistent
{
	public abstract class MediaSolution : BaseScheduleSolution<MediaContext>
	{
		public virtual MediaSchedule Schedule { get; set; }

		public override void ResetParent()
		{
			Schedule = null;
		}

		public abstract void InitSolutionInfo(BaseSolutionInfo solutionInfo);

		public override void MarkAsModified()
		{
			base.MarkAsModified();
			Schedule.MarkAsModified();
		}
	}
}
