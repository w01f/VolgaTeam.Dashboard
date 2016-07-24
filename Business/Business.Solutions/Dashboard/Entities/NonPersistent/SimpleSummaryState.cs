using System;
using System.Collections.Generic;
using System.Linq;
using Asa.Common.Core.Objects.Output;

namespace Asa.Business.Solutions.Dashboard.Entities.NonPersistent
{
	public class SimpleSummaryState
	{
		public bool ShowAdvertiser { get; set; }
		public bool ShowDecisionMaker { get; set; }
		public bool ShowPresentationDate { get; set; }
		public bool ShowFlightDates { get; set; }
		public bool ShowMonthly { get; set; }
		public bool ShowTotal { get; set; }
		public bool TableOutput { get; set; }

		public string SlideHeader { get; set; }
		public string Advertiser { get; set; }
		public string DecisionMaker { get; set; }
		public DateTime PresentationDate { get; set; }
		public DateTime FlightDatesStart { get; set; }
		public DateTime FlightDatesEnd { get; set; }
		public decimal? MonthlyValue { get; set; }
		public decimal? TotalValue { get; set; }

		public List<SimpleSummaryItemState> ItemsState { get; set; }

		public ContractSettings ContractSettings { get; }

		public SimpleSummaryState()
		{
			ShowAdvertiser = true;
			ShowDecisionMaker = true;
			ShowPresentationDate = true;
			ShowFlightDates = true;

			ItemsState = new List<SimpleSummaryItemState>();

			ContractSettings = new ContractSettings();
		}

		public void AfterCreate()
		{
			if (!ItemsState.Any())
			{
				ItemsState.Add(new SimpleSummaryItemState { Order = 1 });
				ItemsState.Add(new SimpleSummaryItemState { Order = 2 });
			}
		}
	}
}
