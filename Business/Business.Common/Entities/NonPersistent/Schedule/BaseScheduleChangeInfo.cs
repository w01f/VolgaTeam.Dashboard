namespace Asa.Business.Common.Entities.NonPersistent.Schedule
{
	public class BaseScheduleChangeInfo
	{
		public bool WholeScheduleChanged { get; set; }
		public bool ScheduleDatesChanged { get; set; }

		public virtual void Reset()
		{
			WholeScheduleChanged = false;
			ScheduleDatesChanged = false;
		}

		public virtual void Merge(BaseScheduleChangeInfo newInfo)
		{
			WholeScheduleChanged |= newInfo.WholeScheduleChanged;
			ScheduleDatesChanged |= newInfo.ScheduleDatesChanged;
		}
	}
}
