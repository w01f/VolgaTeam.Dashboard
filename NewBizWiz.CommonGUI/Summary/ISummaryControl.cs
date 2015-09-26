using System.Collections.Generic;
using NewBizWiz.Core.Common;

namespace NewBizWiz.CommonGUI.Summary
{
	public interface ISummaryControl
	{
		int ItemsCount { get; }
		int SlidesCount { get; }
		string Title { get; }
		string SummaryData { get; }
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

		string ContractTemplatePath { get; }
		ContractSettings ContractSettings { get; }
		
		Theme SelectedTheme { get; }

		bool TableOutput { get; }
		int ItemsPerTable { get; }
		bool ShowIcons { get; }
		string[] TableIcons { get; }
		List<Dictionary<string, string>> OutputReplacementsLists { get; }
		void PopulateReplacementsList();
	}
}
