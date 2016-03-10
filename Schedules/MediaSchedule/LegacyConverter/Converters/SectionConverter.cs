using System;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Legacy.Media.Entities.Section;

namespace Asa.Media.LegacyConverter.Converters
{
	static class SectionConverter
	{
		public static void ImportData(this ProgramScheduleContent target, ProgramSchedule source)
		{
			target.SelectedQuarter = source.SelectedQuarter;
			target.ApplySettingsForAll = source.ApplySettingsForAll;
			target.DigitalLegend.ImportData(source.DigitalLegend);
			foreach (var oldScheduleSection in source.Sections)
			{
				var targetSection = target.CreateSection();
				targetSection.ImportData(oldScheduleSection);
				target.Sections.Add(targetSection);
			}
		}

		private static void ImportData(
			this Business.Online.Entities.NonPersistent.DigitalLegend target,
			Legacy.Common.Entities.Digital.DigitalLegend source)
		{
			target.Enabled = source.Enabled;
			target.AllowEdit = source.AllowEdit;
			target.ApplyForAll = source.ApplyForAll;
			target.OutputOnlyOnce = source.OutputOnlyOnce;

			target.ShowWebsites = source.ShowWebsites;
			target.ShowProduct = source.ShowProduct;
			target.ShowDimensions = source.ShowDimensions;
			target.ShowDates = source.ShowDates;
			target.ShowImpressions = source.ShowImpressions;
			target.ShowCPM = source.ShowCPM;
			target.ShowInvestment = source.ShowInvestment;

			if (!String.IsNullOrEmpty(source.Info1))
				target.Info = source.Info1;
			else if (!String.IsNullOrEmpty(source.Info2))
				target.Info = source.Info2;
			else if (!String.IsNullOrEmpty(source.Info3))
				target.Info = source.Info3;
		}

		private static void ImportData(
			this Business.Media.Entities.NonPersistent.Section.Content.ScheduleSection target,
			Legacy.Media.Entities.Section.ScheduleSection source)
		{
			target.Name = source.Name;
			target.Index = source.Index;

			target.ShowRate = source.ShowRate;
			target.ShowRating = source.ShowRating;
			target.ShowTime = source.ShowTime;
			target.ShowDay = source.ShowDay;
			target.ShowDaypart = source.ShowDaypart;
			target.ShowStation = source.ShowStation;
			target.ShowProgram = source.ShowProgram;
			target.ShowLenght = source.ShowLenght;
			target.ShowCPP = source.ShowCPP;
			target.ShowGRP = source.ShowGRP;
			target.ShowSpots = source.ShowSpots;
			target.ShowEmptySpots = source.ShowEmptySpots;
			target.ShowCost = source.ShowCost;
			target.ShowLogo = source.ShowLogo;

			target.ShowTotalPeriods = source.ShowTotalPeriods;
			target.ShowTotalSpots = source.ShowTotalSpots;
			target.ShowTotalGRP = source.ShowTotalGRP;
			target.ShowTotalCPP = source.ShowTotalCPP;
			target.ShowAverageRate = source.ShowAverageRate;
			target.ShowTotalRate = source.ShowTotalRate;
			target.ShowNetRate = source.ShowNetRate;
			target.ShowDiscount = source.ShowDiscount;

			target.OutputPerQuater = source.OutputPerQuater;
			target.OutputMaxPeriods = source.OutputMaxPeriods;
			target.OutputNoBrackets = source.OutputNoBrackets;
			target.UseDecimalRates = source.UseDecimalRates;
			target.UseGenericDateColumns = source.UseGenericDateColumns;

			foreach (var oldProgram in source.Programs)
			{
				var program = new Business.Media.Entities.NonPersistent.Section.Content.Program(target);
				program.ImportData(oldProgram);
				target.Programs.Add(program);
			}
			target.Summary.ImportData(source.Summary);
			target.ContractSettings.ImportData(source.ContractSettings);
		}

		private static void ImportData(
			this Business.Media.Entities.NonPersistent.Section.Content.Program target,
			Legacy.Media.Entities.Section.Program source)
		{
			target.Name = source.Name;
			target.Day = source.Day;
			target.Index = source.Index;
			target.Logo.ImportData(source.Logo);
			target.Station = source.Station;
			target.Daypart = source.Daypart;
			target.Time = source.Time;
			target.Length = source.Length;
			target.Rate = source.Rate;
			target.Rating = source.Rating;
			target.SummaryItem.ImportData(source.SummaryItem);
			foreach (var oldSpot in source.Spots)
			{
				var spot = new Business.Media.Entities.NonPersistent.Section.Content.Spot(target);
				spot.ImportData(oldSpot);
				target.Spots.Add(spot);
			}
		}

		private static void ImportData(
			this Business.Media.Entities.NonPersistent.Section.Content.Spot target,
			Legacy.Media.Entities.Section.Spot source)
		{
			target.Date = source.Date;
			target.Count = source.Count;
		}

		private static void ImportData(
			this Business.Media.Entities.NonPersistent.Section.Summary.SectionSummary target,
			Legacy.Media.Entities.Summary.SectionSummary source)
		{
			switch (source.SummaryType)
			{
				case Legacy.Media.Entities.Summary.SectionSummaryTypeEnum.Custom:
					target.CustomSummary.ImportData((Legacy.Media.Entities.Summary.CustomSummaryContent)source.Content);
					break;
				case Legacy.Media.Entities.Summary.SectionSummaryTypeEnum.Product:
					target.ProductSummary.ImportData((Legacy.Media.Entities.Summary.ProductSummaryContent)source.Content);
					break;
				case Legacy.Media.Entities.Summary.SectionSummaryTypeEnum.Strategy:
					target.StrategySummary.ImportData((Legacy.Media.Entities.Summary.StrategySummaryContent)source.Content);
					break;
			}
		}

		private static void ImportData(
			this Business.Media.Entities.NonPersistent.Section.Summary.CustomSummaryContent target,
			Legacy.Media.Entities.Summary.CustomSummaryContent source)
		{
			((Business.Common.Entities.NonPersistent.Summary.BaseSummarySettings)target).ImportData(source);

			if (source.IsDefaultSate) return;

			target.Items.Clear();
			foreach (var oldSummaryItem in source.Items)
			{
				var summaryItem = target.AddItem();
				summaryItem.ImportData(oldSummaryItem);
				target.Items.Add(summaryItem);
			}
		}

		private static void ImportData(
			this Business.Media.Entities.NonPersistent.Section.Summary.ProductSummaryContent target,
			Legacy.Media.Entities.Summary.ProductSummaryContent source)
		{
			((Business.Common.Entities.NonPersistent.Summary.BaseSummarySettings)target).ImportData(source);
		}

		private static void ImportData(
			this Business.Media.Entities.NonPersistent.Section.Summary.StrategySummaryContent target,
			Legacy.Media.Entities.Summary.StrategySummaryContent source)
		{
			target.ShowDescription = source.ShowDescription;
			target.ShowStation = source.ShowStation;
			foreach (var oldSummaryItem in source.Items)
			{
				var summaryItem = new Business.Media.Entities.NonPersistent.Section.Summary.ProgramStrategyItem(target);
				summaryItem.ImportData(oldSummaryItem);
				target.Items.Add(summaryItem);
			}
		}

		private static void ImportData(
			this Business.Media.Entities.NonPersistent.Section.Summary.ProgramStrategyItem target,
			Legacy.Media.Entities.Summary.ProgramStrategyItem source)
		{
			target.Enabled = source.Enabled;
			target.Name = source.Name;
			target.Station = source.Station;
			target.Description = source.Description;
			target.Order = source.Order;
			target.Logo.ImportData(source.Logo);
		}
	}
}
