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
		public IntroState IntroState { get; }

		protected ShiftContent()
		{
			CoverState = new CoverState();
			IntroState = new IntroState();
		}

		protected override void AfterCreate()
		{
			base.AfterCreate();
		}
	}
}
