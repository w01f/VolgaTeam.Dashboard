using Asa.Legacy.Media.Entities.Schedule;

namespace Asa.Media.LegacyConverter.Converters
{
	static class SnapshotsConverter
	{
		public static void ImportData(
			this Business.Media.Entities.NonPersistent.Snapshot.SnapshotContent target,
			RegularSchedule source)
		{
			foreach (var oldSnapshot in source.Snapshots)
			{
				var snapshot = new Business.Media.Entities.NonPersistent.Snapshot.Snapshot(target);
				snapshot.ImportData(oldSnapshot);
				target.Snapshots.Add(snapshot);
			}
			target.SnapshotSummary.ImportData(source.SnapshotSummary);
		}

		private static void ImportData(
			this Business.Media.Entities.NonPersistent.Snapshot.Snapshot target,
			Legacy.Media.Entities.Snapshot.Snapshot source)
		{
			target.Index = source.Index;
			target.Name = source.Name;
			target.Comment = source.Comment;
			target.Logo.ImportData(source.Logo);
			if (source.TotalWeeks.HasValue)
				target.TotalWeeks = source.TotalWeeks.Value;

			target.ShowLineId = source.ShowLineId;
			target.ShowLogo = source.ShowLogo;
			target.ShowStation = source.ShowStation;
			target.ShowProgram = source.ShowProgram;
			target.ShowDaypart = source.ShowDaypart;
			target.ShowLenght = source.ShowLenght;
			target.ShowTime = source.ShowTime;
			target.ShowRate = source.ShowRate;
			target.ShowWeeklyCost = source.ShowCost;
			target.ShowSpotsX = source.ShowSpotsX;
			target.ShowTotalRow = source.ShowTotalRow;
			target.UseDecimalRates = source.UseDecimalRates;
			target.ShowSpotsPerWeek = source.ShowSpotsPerWeek;

			target.ShowWeeklySpots = source.ShowTotalSpots;
			target.ShowAverageRate = source.ShowAverageRate;

			target.ContractSettings.ImportData(source.ContractSettings);

			foreach (var oldProgram in source.Programs)
			{
				var program = new Business.Media.Entities.NonPersistent.Snapshot.SnapshotProgram(target);
				program.ImportData(oldProgram);
				target.Programs.Add(program);
			}
			target.RebuildProgramIndexes();

			foreach (var oldActiveWeek in source.ActiveWeeks)
			{
				var activeWeek = new Business.Common.Entities.NonPersistent.Common.DateRange();
				activeWeek.ImportData(oldActiveWeek);
				target.ActiveWeeks.Add(activeWeek);
			}
		}

		private static void ImportData(
			this Business.Media.Entities.NonPersistent.Snapshot.SnapshotProgram target,
			Legacy.Media.Entities.Snapshot.SnapshotProgram source)
		{
			target.Name = source.Name;
			target.Index = source.Index;
			target.Station = source.Station;
			target.Logo.ImportData(source.Logo);
			target.Daypart = source.Daypart;
			target.Length = source.Length;
			target.Time = source.Time;
			target.Rate = source.Rate;

			target.MondaySpot = source.MondaySpot;
			target.TuesdaySpot = source.TuesdaySpot;
			target.WednesdaySpot = source.WednesdaySpot;
			target.ThursdaySpot = source.ThursdaySpot;
			target.FridaySpot = source.FridaySpot;
			target.SaturdaySpot = source.SaturdaySpot;
			target.SundaySpot = source.SundaySpot;
		}

		private static void ImportData(
			this Business.Media.Entities.NonPersistent.Snapshot.SnapshotSummary target,
			Legacy.Media.Entities.Snapshot.SnapshotSummary source)
		{
			target.ApplySettingsForAll = source.ApplySettingsForAll;
			target.ShowLineId = source.ShowLineId;
			target.ShowLogo = source.ShowLogo;
			target.ShowCampaign = source.ShowCampaign;
			target.ShowComments = source.ShowComments;
			target.ShowSpots = source.ShowSpots;
			target.ShowCost = source.ShowCost;
			target.ShowTotalWeeks = source.ShowTotalWeeks;
			target.ShowTotalCost = source.ShowTotalCost;
			target.ShowTallySpots = source.ShowTallySpots;
			target.ShowTallyCost = source.ShowTallyCost;
			target.ShowSpotsX = source.ShowSpotsX;
			target.UseDecimalRates = source.UseDecimalRates;

			target.ContractSettings.ImportData(source.ContractSettings);
		}
	}
}
