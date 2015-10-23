using System.Windows.Forms;

namespace Asa.OnlineSchedule.Controls.PresentationClasses
{
	public interface IAdPlanItem
	{
		AdPlanControl Container { get; set; }
		Control SettingsContainer { get; }
		string LogoFile { get; }
		string Product { get; }
		string Details { get; }
		decimal? Investment { get; }
		string InvestmentFormatted { get; }
		bool NotOutput { get; }
	}
}
