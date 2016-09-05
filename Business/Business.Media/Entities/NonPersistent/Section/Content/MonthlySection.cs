using Asa.Business.Media.Configuration;
using Asa.Business.Media.Enums;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Section.Content
{
	public class MonthlySection : ScheduleSection
	{
		[JsonConstructor]
		private MonthlySection() { }

		public MonthlySection(ProgramScheduleContent parent)
			: base(parent)
		{
			SpotType = SpotType.Month;

			ShowTime = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowTime;
			ShowDaypart = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowDaypart;
			ShowDay = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowDay;
			ShowStation = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowStation;
			ShowProgram = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowProgram;
			ShowLenght = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowLenght;
			ShowRate = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowRate;
			ShowSpots = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowSpots;
			ShowCost = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowCost;
			ShowLogo = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowLogo;

			ShowTotalPeriods = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowTotalPeriods;
			ShowTotalSpots = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowTotalSpots;
			ShowAverageRate = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowAverageRate;
			ShowTotalRate = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowTotalRate;
			ShowNetRate = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowNetRate;
			ShowDiscount = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowDiscount;

			OutputNoBrackets = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.OutputNoBrackets;
			UseDecimalRates = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.UseDecimalRates;
			UseGenericDateColumns = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.UseGenericDateColumns;
		}
	}
}
