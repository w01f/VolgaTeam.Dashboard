using Asa.Common.Core.Objects.Output;

namespace Asa.Business.Common.Entities.NonPersistent.Summary
{
	public class BaseSummarySettings
	{
		public bool ShowAdvertiser { get; set; }
		public bool ShowDecisionMaker { get; set; }
		public bool ShowPresentationDate { get; set; }
		public bool ShowFlightDates { get; set; }
		public bool ShowMonthly { get; set; }
		public bool ShowTotal { get; set; }
		public bool TableOutput { get; set; }

		public string SlideHeader { get; set; }
		public decimal? MonthlyValue { get; set; }
		public decimal? TotalValue { get; set; }

		public ContractSettings ContractSettings { get; private set; }

		public BaseSummarySettings()
		{
			ShowAdvertiser = true;
			ShowDecisionMaker = true;
			ShowPresentationDate = true;
			ShowFlightDates = true;
			ShowMonthly = false;
			ShowTotal = false;

			SlideHeader = string.Empty;

			ContractSettings = new ContractSettings();
		}

		public virtual void Dispose()
		{
			ContractSettings = null;
		}
	}
}
