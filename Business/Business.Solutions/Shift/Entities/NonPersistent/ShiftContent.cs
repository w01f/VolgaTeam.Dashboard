using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Common.Entities.NonPersistent.ScheduleResources;
using Asa.Business.Solutions.Common.Entities.NonPersistent;

namespace Asa.Business.Solutions.Shift.Entities.NonPersistent
{
	public abstract class ShiftContent : BaseSolutionContent
	{
		public abstract BaseScheduleSettings ScheduleSettings { get; }
		public abstract BaseScheduleResourceContainer ScheduleResources { get; }

		public CoverState CoverState { get; }

		protected ShiftContent()
		{
			CoverState = new CoverState();
		}

		protected override void AfterCreate()
		{
			base.AfterCreate();
		}
	}
}
