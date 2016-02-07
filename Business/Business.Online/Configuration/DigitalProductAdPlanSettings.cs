using System.Drawing;

namespace Asa.Business.Online.Configuration
{
	public class DigitalProductAdPlanSettings
	{
		public bool EditName { get; set; }
		public bool EditInvestment { get; set; }

		public bool ShowInvestment { get; set; }
		public bool ShowFlightDates { get; set; }
		public bool ShowComments { get; set; }

		public bool ShowWebsites { get; set; }
		public bool ShowDimensions { get; set; }
		public bool ShowMonthlyImpressions { get; set; }
		public bool ShowMonthlyCPM { get; set; }
		public bool ShowTotalImpressions { get; set; }
		public bool ShowTotalCPM { get; set; }
		public bool ShowComment1 { get; set; }
		public bool ShowComment2 { get; set; }
		public bool ShowComment3 { get; set; }

		public bool NotOutput { get; set; }

		public string Name { get; set; }
		public Image Logo { get; set; }
		public decimal? Investment { get; set; }
		public string Comments { get; set; }

		public DigitalProductAdPlanSettings()
		{
			ResetItemsToDefault();
		}

		public void ResetItemsToDefault()
		{
			ShowInvestment = false;
			ShowFlightDates = true;
			ShowComments = false;

			ShowWebsites = false;
			ShowDimensions = true;
			ShowMonthlyImpressions = false;
			ShowMonthlyCPM = false;
			ShowTotalImpressions = false;
			ShowTotalCPM = true;
			ShowComment1 = false;
			ShowComment2 = false;
			ShowComment3 = false;
		}

		public void Dispose()
		{
			if (Logo != null)
				Logo.Dispose();
		}
	}
}
