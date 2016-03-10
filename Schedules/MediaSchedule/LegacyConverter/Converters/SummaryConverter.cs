namespace Asa.Media.LegacyConverter.Converters
{
	static class SummaryConverter
	{
		public static void ImportData(
			this Business.Common.Entities.NonPersistent.Summary.BaseSummarySettings target,
			Legacy.Common.Entities.Summary.BaseSummarySettings source)
		{
			target.ShowAdvertiser = source.ShowAdvertiser;
			target.ShowDecisionMaker = source.ShowDecisionMaker;
			target.ShowPresentationDate = source.ShowPresentationDate;
			target.ShowFlightDates = source.ShowFlightDates;
			target.ShowMonthly = source.ShowMonthly;
			target.ShowTotal = source.ShowTotal;
			target.TableOutput = source.TableOutput;
			target.SlideHeader = source.SlideHeader;
			target.MonthlyValue = source.MonthlyValue;
			target.TotalValue = source.TotalValue;
		}

		public static void ImportData(
			this Business.Common.Entities.NonPersistent.Summary.CustomSummaryItem target,
			Legacy.Common.Entities.Summary.CustomSummaryItem source)
		{
			target.Order = source.Order;
			target.ShowValue = source.ShowValue;
			target.ShowDescription = source.ShowDescription;
			target.ShowMonthly = source.ShowMonthly;
			target.ShowTotal = source.ShowTotal;
			target.Value = source.Value;
			target.Description = source.Description;
			target.Monthly = source.Monthly;
			target.Total = source.Total;
		}
	}
}
