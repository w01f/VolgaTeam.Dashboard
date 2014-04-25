using NewBizWiz.Core.Common;

namespace NewBizWiz.CommonGUI.Summary
{
	public interface ISummaryControl
	{
		int ItemsCount { get; }
		string Title { get; }
		string Advertiser { get; }
		string DecisionMaker { get; }
		string PresentationDate { get; }
		string CampaignDates { get; }
		string[] ItemTitles { get; }
		string[] ItemDetails { get; }
		string[] MonthlyValues { get; }
		string[] TotalValues { get; }
		string TotalMonthlyValue { get; }
		string TotalTotalValue { get; }
		bool ShowMonthlyHeader { get; }
		bool ShowTotalHeader { get; }
		Theme SelectedTheme { get; }
	}
}
