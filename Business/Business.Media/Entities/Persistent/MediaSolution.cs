using Asa.Business.Common.Entities.Persistent;
using Asa.Business.Media.Contexts;

namespace Asa.Business.Media.Entities.Persistent
{
	public abstract class MediaSolution : BaseScheduleSolution<MediaContext>
	{
		public virtual MediaSchedule Schedule { get; set; }

		public override void ResetParent()
		{
			Schedule = null;
		}

		public override void MarkAsModified()
		{
			base.MarkAsModified();
			Schedule.MarkAsModified();
		}
	}
}
