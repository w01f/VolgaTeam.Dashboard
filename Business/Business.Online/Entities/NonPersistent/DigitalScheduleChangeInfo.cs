using Asa.Business.Common.Entities.NonPersistent.Schedule;

namespace Asa.Business.Online.Entities.NonPersistent
{
	public class DigitalScheduleChangeInfo : BaseScheduleChangeInfo
	{
		public bool DigitalContentChanged { get; set; }

		public override void Reset()
		{
			base.Reset();
			DigitalContentChanged = false;
		}

		public override void Merge(BaseScheduleChangeInfo newInfo)
		{
			base.Merge(newInfo);

			var newMediaInfo = (DigitalScheduleChangeInfo)newInfo;
			DigitalContentChanged |= newMediaInfo.DigitalContentChanged;
		}
	}
}
