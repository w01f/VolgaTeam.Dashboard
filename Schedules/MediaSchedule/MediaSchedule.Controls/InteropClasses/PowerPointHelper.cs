using NewBizWiz.MediaSchedule.Controls.BusinessClasses;

namespace NewBizWiz.MediaSchedule.Controls.InteropClasses
{
	public abstract partial class MediaSchedulePowerPointHelper<T> : CommonGUI.Interop.CommonPowerPointHelper<T> where T : class, new()
	{
	}

	public class RegularMediaSchedulePowerPointHelper : MediaSchedulePowerPointHelper<RegularMediaSchedulePowerPointHelper> { }
}
