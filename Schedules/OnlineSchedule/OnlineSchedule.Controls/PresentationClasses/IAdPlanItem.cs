namespace NewBizWiz.OnlineSchedule.Controls.PresentationClasses
{
	public interface IAdPlanItem
	{
		AdPlanControl Container { get; set; }
		string LogoFile { get; }
		string Product { get; }
		string Details { get; }
		string Investment { get; }
		bool NotOutput { get; }
	}
}
