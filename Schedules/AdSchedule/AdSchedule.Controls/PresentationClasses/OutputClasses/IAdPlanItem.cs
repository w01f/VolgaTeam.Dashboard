using System.Drawing;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses
{
	public interface IAdPlanItem
	{
		string LogoFile { get; }
		string Product { get; }
		string Details { get; }
		string Investment { get; }
		bool NotOutput { get; }
	}
}
