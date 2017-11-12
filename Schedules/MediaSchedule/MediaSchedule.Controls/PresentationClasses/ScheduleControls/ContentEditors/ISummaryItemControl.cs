using System;
using Asa.Business.Common.Entities.NonPersistent.Summary;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	public interface ISummaryItemControl
	{
		CustomSummaryItem Data { get; set; }
		bool ShowMonthly { get; }
		bool ShowTotal { get; }
		bool ShowValueOutput { get; }
		bool ShowDescriptionOutput { get; }
		bool ShowMonthlyOutput { get; }
		bool ShowTotalOutput { get; }
		string ItemTitle { get; }
		string ItemIcon { get; }
		string OutputItemTitle { get; }
		string ItemDetailOutput { get; }
		decimal? MonthlyValue { get; }
		decimal? TotalValue { get; }
		decimal? OutputMonthlyValue { get; }
		decimal? OutputTotalValue { get; }
		bool Complited { get; }

		event EventHandler<EventArgs> DataChanged;
		event EventHandler<EventArgs> InvestmentChanged;

		event EventHandler<SummaryItemEventArgs> ItemPositionChanged;
		event EventHandler<SummaryItemEventArgs> ItemDeleted;

		void LoadData();
		void Release();
		void UpdateNumber();
		void UpdatePositionButtons();
	}

	public class SummaryItemEventArgs : EventArgs
	{
		public ISummaryItemControl SummaryItem { get; private set; }

		public SummaryItemEventArgs(ISummaryItemControl summaryItem)
		{
			SummaryItem = summaryItem;
		}
	}
}
