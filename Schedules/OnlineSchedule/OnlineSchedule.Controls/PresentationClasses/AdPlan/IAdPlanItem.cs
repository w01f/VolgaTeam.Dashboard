namespace NewBizWiz.OnlineSchedule.Controls.PresentationClasses
{
	public interface IAdPlanItem
	{
		AdPlanControl Container { get; set; }
		string LogoFile { get; }
		string Product { get; }
		string Details { get; }
		decimal? Investment { get; }
		string InvestmentFormatted { get; }
		bool NotOutput { get; }
	}
}
