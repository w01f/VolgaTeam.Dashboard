using Asa.Common.GUI.Interop;

namespace Asa.Media.Controls.InteropClasses
{
	public abstract partial class MediaSchedulePowerPointHelper<T> : CommonPowerPointHelper<T> where T : class, new() { }

	public class RegularMediaSchedulePowerPointHelper : MediaSchedulePowerPointHelper<RegularMediaSchedulePowerPointHelper> { }
}
