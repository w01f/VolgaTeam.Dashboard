using Asa.Business.Media.Configuration;
using Asa.Business.Media.Enums;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Section.Content
{
	public class WeeklySection : ScheduleSection
	{
		[JsonConstructor]
		private WeeklySection() { }

		public WeeklySection(ProgramScheduleContent parent)
			: base(parent)
		{
			SpotType = SpotType.Week;

			ShowTime = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowTime;
			ShowDaypart = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowDaypart;
			ShowDay = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowDay;
			ShowStation = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowStation;
			ShowProgram = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowProgram;
			ShowLenght = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowLenght;
			ShowRate = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowRate;
			ShowSpots = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowSpots;
			ShowCost = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowCost;
			ShowLogo = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowLogo;

			ShowTotalPeriods = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowTotalPeriods;
			ShowTotalSpots = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowTotalSpots;
			ShowAverageRate = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowAverageRate;
			ShowTotalRate = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowTotalRate;
			ShowNetRate = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowNetRate;
			ShowDiscount = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowDiscount;

			OutputNoBrackets = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.OutputNoBrackets;
			UseDecimalRates = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.UseDecimalRates;
			UseGenericDateColumns = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.UseGenericDateColumns;
		}
	}
}
