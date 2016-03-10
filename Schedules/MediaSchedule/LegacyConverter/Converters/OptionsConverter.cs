using System;
using Asa.Legacy.Media.Entities.Schedule;

namespace Asa.Media.LegacyConverter.Converters
{
	static class OptionsConverter
	{
		public static void ImportData(
			this Business.Media.Entities.NonPersistent.Option.OptionsContent target,
			RegularSchedule source)
		{
			foreach (var oldOptionSet in source.Options)
			{
				var optionSet = new Business.Media.Entities.NonPersistent.Option.OptionSet(target);
				optionSet.ImportData(oldOptionSet);
				target.Options.Add(optionSet);
			}
			target.OptionsSummary.ImportData(source.OptionsSummary);
		}

		private static void ImportData(
			this Business.Media.Entities.NonPersistent.Option.OptionSet target,
			Legacy.Media.Entities.Options.OptionSet source)
		{
			target.Index = source.Index;
			target.Name = source.Name;
			target.Comment = source.Comment;
			target.Logo.ImportData(source.Logo);
			target.TotalPeriods = source.TotalPeriods;

			target.ShowLineId = source.ShowLineId;
			target.ShowLogo = source.ShowLogo;
			target.ShowStation = source.ShowStation;
			target.ShowProgram = source.ShowProgram;
			target.ShowDay = source.ShowDay;
			target.ShowTime = source.ShowTime;
			target.ShowRate = source.ShowRate;
			target.ShowLenght = source.ShowLenght;
			target.ShowSpots = source.ShowSpots;
			target.ShowCost = source.ShowCost;
			target.ShowSpotsX = source.ShowSpotsX;
			target.UseDecimalRates = source.UseDecimalRates;
			target.ShowTotalSpots = source.ShowTotalSpots;
			target.ShowTotalCost = source.ShowTotalCost;
			target.ShowAverageRate = source.ShowAverageRate;
			target.SpotType = (Business.Media.Enums.SpotType)(Int32)source.SpotType;

			target.ContractSettings.ImportData(source.ContractSettings);

			foreach (var oldProgram in source.Programs)
			{
				var program = new Business.Media.Entities.NonPersistent.Option.OptionProgram(target);
				program.ImportData(oldProgram);
				target.Programs.Add(program);
			}
		}

		private static void ImportData(
			this Business.Media.Entities.NonPersistent.Option.OptionProgram target,
			Legacy.Media.Entities.Options.OptionProgram source)
		{
			target.Name = source.Name;
			target.Index = source.Index;
			target.Station = source.Station;
			target.Logo.ImportData(source.Logo);
			target.Day = source.Day;
			target.Time = source.Time;
			target.Length = source.Length;
			target.Rate = source.Rate;
			target.Spot = source.Spot;
		}

		private static void ImportData(
			this Business.Media.Entities.NonPersistent.Option.OptionSummary target,
			Legacy.Media.Entities.Options.OptionSummary source)
		{
			target.SpotType = (Business.Media.Enums.SpotType)(Int32)source.SpotType;
			target.ApplySettingsForAll = source.ApplySettingsForAll;
			target.ShowLineId = source.ShowLineId;
			target.ShowLogo = source.ShowLogo;
			target.ShowCampaign = source.ShowCampaign;
			target.ShowComments = source.ShowComments;
			target.ShowSpots = source.ShowSpots;
			target.ShowCost = source.ShowCost;
			target.ShowTotalPeriods = source.ShowTotalPeriods;
			target.ShowTotalCost = source.ShowTotalCost;
			target.ShowTallySpots = source.ShowTallySpots;
			target.ShowTallyCost = source.ShowTallyCost;
			target.ShowSpotsX = source.ShowSpotsX;
			target.UseDecimalRates = source.UseDecimalRates;

			target.ContractSettings.ImportData(source.ContractSettings);
		}
	}
}
