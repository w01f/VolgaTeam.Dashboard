using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration
{
	public abstract class ShiftTopTabInfo : ShiftTabInfo
	{
		public ShiftTopTabType TabType { get; }

		protected ShiftTopTabInfo(ShiftTopTabType tabType)
		{
			TabType = tabType;
		}
	}
}
